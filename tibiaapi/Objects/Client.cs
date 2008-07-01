using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Tibia.Packets;
using System.IO;
using System.Drawing;
using Tibia.Constants;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a single Tibia Client. Contains wrapper methods 
    /// for memory, packet sending, battlelist, and slots. Also contains
    /// any "helper methods" that automate tasks, such as making a rune.
    /// </summary>
    public class Client
    {
        #region Variables
        private Process process;
        private IntPtr handle;
        private int startTime;
        private bool isVisible;
        private bool usingProxy = false;
        private LoginServer openTibiaServer = null;
        private AutoResetEvent pipeIsReady;

        // References to commonly used objects
        private BattleList battleList;
        private Map map;
        private Inventory inventory;
        private Random random;
        private Console console;
        private Util.DatReader dat;
        private Util.Proxy proxy;
        private Util.Pipe pipe = null; //For Displaying Text
        private Screen screen;
        #endregion

        #region Events
        /// <summary>
        /// Prototype for client notifications.
        /// </summary>
        public delegate void ClientNotification();

        /// <summary>
        /// Event raised when the Tibia client is exited.
        /// </summary>
        public ClientNotification OnExit;

        private void ClientExited(object sender, EventArgs e)
        {
            if (OnExit != null)
                OnExit();
        }
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="p">the client's process object</param>
        public Client(Process p)
        {
            process = p;
            p.Exited += new EventHandler(ClientExited);
            
            // Wait until we can really access the process
            p.Refresh();
            p.WaitForInputIdle();
            p.Refresh();

            // Save the start time (it isn't changing)
            startTime = ReadInt(Addresses.Client.StartTime);

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            handle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            pipeIsReady = new AutoResetEvent(false);

            // The client get's it's own battle list to speed up getPlayer()
            battleList = new BattleList(this);
            map = new Map(this);
            inventory = new Inventory(this);
            console = new Console(this);
            random = new Random();
            dat = new Util.DatReader(this);
            screen = new Screen(this);
        }

        /// <summary>
        /// Finalize this client, closing the handle.
        /// Called before the object is garbage collected.
        /// </summary>
        ~Client()
        {
            // Close the process handle
            Util.WinApi.CloseHandle(handle);
        }
        #endregion

        #region Properties
        public LoginServer OpenTibiaServer
        {
            get { return openTibiaServer; }
            set { openTibiaServer = value; }
        }

        /// <summary>
        /// Get or set the title of the client.
        /// </summary>
        public string Title
        {
            get
            {
                return process.MainWindowTitle;
            }
            set
            {
                Util.WinApi.SetWindowText(MainWindowHandle, value);
            }
        }

        /// <summary>
        /// Sets the Tibia client as the topmost application or not.
        /// </summary>
        public bool IsTopMost
        {
            set
            {
                Util.WinApi.SetWindowPos(MainWindowHandle, (value) ? Util.WinApi.HWND_TOPMOST : Util.WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, Util.WinApi.SWP_NOMOVE | Util.WinApi.SWP_NOSIZE);
            }
        }

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus Status
        {
            get { return (Constants.LoginStatus)ReadByte(Addresses.Client.Status); }
        }

        /// <summary>
        /// Check whether or not the client is logged in
        /// </summary>
        public bool LoggedIn
        {
            get
            {
                return Status == Constants.LoginStatus.LoggedIn;
            }
        }

        /// <summary>
        /// Get and set the Statusbar text (the white text above the console).
        /// </summary>
        public string Statusbar
        {
            get { return ReadString(Addresses.Client.Statusbar_Text); }
            set { WriteByte(Addresses.Client.Statusbar_Time, 50); WriteString(Addresses.Client.Statusbar_Text, value); WriteByte(Addresses.Client.Statusbar_Text + value.Length, 0x00); }
        }

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort LastSeenId
        {
            get
            {
                byte[] bytes = ReadBytes(Addresses.Client.See_Id, 2);
                return BitConverter.ToUInt16(bytes, 0);
            }
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort LastSeenCount
        {
            get
            {
                byte[] bytes = ReadBytes(Addresses.Client.See_Count, 2);
                return BitConverter.ToUInt16(bytes, 0);
            }
        }

        /// <summary>
        /// Gets the text of the last seen item/tile.
        /// </summary>
        public string LastSeenText
        {
            get { return ReadString(Addresses.Client.See_Text); }
        }

        /// <summary>
        /// Get if this client is the active window, or bring it to the foreground
        /// </summary>
        public bool IsActive
        {
            get
            {

                return MainWindowHandle == Util.WinApi.GetForegroundWindow();
            }
            set
            {
                if (value)
                    Util.WinApi.SetForegroundWindow(MainWindowHandle);
            }
        }

        /// <summary>
        /// Gets the client process' main window handle.
        /// </summary>
        public IntPtr MainWindowHandle
        {
            get {
                if (process.MainWindowHandle == IntPtr.Zero)
                    process.Refresh();
                return process.MainWindowHandle;
            }
        }

        /// <summary>
        /// Check if the client is minimized
        /// </summary>
        /// <returns></returns>
        public bool IsMinimized
        {
            get { return Util.WinApi.IsIconic(MainWindowHandle); }
        }

        /// <summary>
        /// Check if the client is maximized
        /// </summary>
        /// <returns></returns>
        public bool IsMaximized
        {
            get { return Util.WinApi.IsZoomed(MainWindowHandle); }
        }

        public bool IsVisible
        {
            set
            {
                Util.WinApi.ShowWindow(MainWindowHandle, (int)((value) ? Util.WinApi.SW_SHOW : Util.WinApi.SW_HIDE));
                isVisible = value;
            }
            get { return isVisible; }
        }

        /// <summary>
        /// Get the client's version
        /// </summary>
        /// <returns></returns>
        public string Version
        {
            get { return process.MainModule.FileVersionInfo.FileVersion; }
        }

        /// <summary>
        /// Get/Set the RSA key, wrapper for Memory.WriteRSA
        /// </summary>
        /// <returns></returns>
        public string RSA
        {
            get
            {
                return ReadString(Addresses.Client.RSA);
            }
            set
            {
                Memory.WriteRSA(handle, Addresses.Client.RSA, value);
            }
        }

        /// <summary>
        /// Get the current FPS of the client.
        /// </summary>
        public double FPSCurrent
        {
            get
            {
                int frameRateBegin = ReadInt(Addresses.Client.FrameRatePointer);
                return ReadDouble(frameRateBegin + Addresses.Client.FrameRateCurrentOffset);
            }
        }

        /// <summary>
        /// Get or set the FPS limit for the client.
        /// </summary>
        /// <returns></returns>
        public double FPSLimit
        {
            get
            {
                int frameRateBegin = ReadInt(Addresses.Client.FrameRatePointer);
                return ReadDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset);
            }
            set
            {
                if (value <= 0) value = 1;
                int frameRateBegin = ReadInt(Addresses.Client.FrameRatePointer);
                WriteDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset, Calculate.ConvertFPS(value));
            }
        }
        /// <summary>
        /// Gets the position of the client, and its outer boundaries
        /// </summary>
        public Rect Window
        {
            get
            {
                Util.WinApi.RECT r= new Tibia.Util.WinApi.RECT();
                Util.WinApi.GetWindowRect(MainWindowHandle, ref r);
                return new Rect(r);
            }
        }
        /// <summary>
        /// Wrapper for SendMessage function
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public void SendMessage(uint MessageId, int wParam, int lParam)
        {
            Util.WinApi.SendMessage(MainWindowHandle, MessageId, wParam, lParam);
        }
        /// <summary>
        /// Clicks with the mouse somewhere on the screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Click(int x, int y)
        {
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, 0);
                int lpara = Util.WinApi.MakeLParam(x, y);
                SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara);
        }

        /// <summary>
        /// Sets the account number.
        /// </summary>
        public int AccountNumber
        {
            set
            {
                WriteInt(Addresses.Client.LoginAccountNum, value);
                WriteString(Addresses.Client.LoginAccountStr, value.ToString());
            }
        }

        /// <summary>
        /// Sets the account password.
        /// </summary>
        public string AccountPassword
        {
            set
            {
                WriteString(Addresses.Client.LoginPassword, value);
            }
        }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public bool DialogIsOpened
        {
            get
            {
                return (ReadInt(Addresses.Client.DialogBegin) != 0);
            }
        }

        /// <summary>
        /// Gets the position of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public System.Drawing.Point DialogPosition
        {
            get
            {
                int DialogB = ReadInt(Addresses.Client.DialogBegin);
                if (DialogB == 0)
                    return new System.Drawing.Point(0, 0);
                return new System.Drawing.Point(ReadInt(DialogB + Addresses.Client.DialogLeft), ReadInt(DialogB + Addresses.Client.DialogTop));
            }
        }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public AutoResetEvent PipeIsReady
        {
            get { return pipeIsReady; }
        }
        #endregion

        #region Open Client
        /// <summary>
        /// Open a client at the default path
        /// </summary>
        /// <returns></returns>
        public static Client Open()
        {
            return Open(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Tibia\\tibia.exe");
        }

        /// <summary>
        /// Open a client from the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Client Open(string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo(path);
            psi.UseShellExecute = true; // to avoid opening currently running Tibia's
            psi.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
            return Open(psi);
        }

        /// <summary>
        /// Open a client from the specified path with arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Client Open(string path, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo(path, arguments);
            psi.UseShellExecute = true; // to avoid opening currently running Tibia's
            psi.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
            return Open(psi);
        }

        /// <summary>
        /// Opens a client given a process start info object.
        /// </summary>
        public static Client Open(ProcessStartInfo psi)
        {
            Process p = Process.Start(psi);
            p.EnableRaisingEvents = true;
            return new Client(p);
        }
        #endregion

        #region Memory Methods
        public byte[] ReadBytes(long address, uint bytesToRead)
        {
            return Memory.ReadBytes(handle, address, bytesToRead);
        }

        public byte ReadByte(long address)
        {
            return Memory.ReadByte(handle, address);
        }

        public short ReadShort(long address)
        {
            return Memory.ReadShort(handle, address);
        }

        public int ReadInt(long address)
        {
            return Memory.ReadInt(handle, address);
        }

        public double ReadDouble(long address)
        {
            return Memory.ReadDouble(handle, address);
        }

        public string ReadString(long address)
        {
            return Memory.ReadString(handle, address);
        }

        public string ReadString(long address, uint length)
        {
            return Memory.ReadString(handle, address, length);
        }

        public bool WriteBytes(long address, byte[] bytes, uint length)
        {
            return Memory.WriteBytes(handle, address, bytes, length);
        }

        public bool WriteInt(long address, int value)
        {
            return Memory.WriteInt(handle, address, value);
        }

        public bool WriteDouble(long address, double value)
        {
            return Memory.WriteDouble(handle, address, value);
        }

        public bool WriteByte(long address, byte value)
        {
            return Memory.WriteByte(handle, address, value);
        }

        public bool WriteString(long address, string str)
        {
            return Memory.WriteString(handle, address, str);
        }
        #endregion

        #region Override Functions
        /// <summary>
        /// String identifier for this client.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "[" + Version + "] ";
            if (!LoggedIn)
                s += "Not logged in.";
            else
                s += GetPlayer().Name;
            return s;
        }
        #endregion

        #region Client Processes
        /// <summary>
        /// Get a list of all the open clients. Class method.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                StringBuilder classname = new StringBuilder();
                Util.WinApi.GetClassName(process.MainWindowHandle, classname, 12);
                if (classname.ToString().Equals("TibiaClient", StringComparison.CurrentCultureIgnoreCase))
                {
                    clients.Add(new Client(process));
                }
            }
            return clients;
        }

        public void Close()
        {
            if (process != null && !process.HasExited)
                process.Kill();
        }
        #endregion

        #region Client's Objects
        /// <summary>
        /// Get the client's player.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Creature creature = battleList.GetCreature(ReadInt(Addresses.Player.Id));
            return new Player(this, creature.Address);
        }

        /// <summary>
        /// Get the client's battlelist.
        /// </summary>
        /// <returns></returns>
        public BattleList BattleList
        {
            get { return battleList; }
        }

        /// <summary>
        /// Get the client's map.
        /// </summary>
        /// <returns></returns>
        public Map Map
        {
            get { return map; }
        }

        /// <summary>
        /// Get the client's DatReader.
        /// </summary>
        /// <returns></returns>
        public Util.DatReader DatReader
        {
            get { return dat; }
        }

        /// <summary>
        /// Get the client's inventory.
        /// </summary>
        /// <returns></returns>
        public Inventory Inventory
        {
            get { return inventory; }
        }

        /// <summary>
        /// Get the time the client was started.
        /// </summary>
        /// <returns></returns>
        public int StartTime
        {
            get { return startTime; }
        }

        /// <summary>
        /// Get the client's process.
        /// </summary>
        public Process Process
        {
            get { return process; }
        }

        /// <summary>
        /// Get the client's screen (for displaying text)
        /// </summary>
        public Screen Screen
        {
            get { return screen; }
        }

        /// <summary>
        /// Get the pipe that connects this client to it's injected dll
        /// </summary>
        public Util.Pipe Pipe
        {
            get { return pipe; }
        }
        #endregion

        #region Client MultiFunctions
        /// <summary>
        /// Eat food found in any container.
        /// </summary>
        /// <returns>True if eating succeeded, false if no food found or eating failed.</returns>
        public bool EatFood()
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Inventory inventory = new Inventory(this);
            Item food = inventory.FindItem(Tibia.Constants.ItemLists.Foods.Values);
            if (food.Found)
                return food.Use();
            else
                return false;
        }

        #region Transform Items
        /// <summary>
        /// Transform the specified item with the default options.
        /// </summary>
        /// <param name="item">The item to make.</param>
        /// <returns></returns>
        public bool TransformItem(TransformingItem item)
        {
            return TransformItem(item, false);
        }

        /// <summary>
        /// Transform an item. Drags an original item to a free hand, casts the words, and moved the new item back.
        /// If no free hand is found, but the ammo is open, move the item in the right hand down to ammo.
        /// </summary>
        /// <param name="item">The item to make.</param>
        /// <param name="checkSoulPoints">Whether or not to check for soul points.</param>
        /// <returns>True if everything went well, false if no original item was found or part or all of the process failed</returns>
        public bool TransformItem(TransformingItem item, bool checkSoulPoints)
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Player player = GetPlayer();
            bool allClear = true; // Keeps a running total of success
            Item itemMovedToAmmo = null; // If we move an item from the ammo slot, store it here.

            // If wanted, check for soul points
            if (checkSoulPoints)
                if (player.Soul < item.SoulPoints) return false;

            // Make sure the player has enough mana
            if (player.Mana >= item.Spell.ManaPoints)
            {
                // Find the first original
                Item original = inventory.FindItem(item.OriginalItem);

                // Make sure an original was found
                if (original.Found)
                {
                    // Save the current location of the original
                    ItemLocation oldLocation = original.Loc;

                    // The location where the item will be made
                    ItemLocation newLocation;

                    // Determine the location to make the item
                    /*if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Left).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Left);
                    else*/
                    if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Right).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Right);
                    else if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Ammo).Found)
                    {
                        // If no hands are open, but the ammo slot is, 
                        // move the right hand item to clear the ammo slot
                        newLocation = new ItemLocation(Constants.SlotNumber.Right);
                        itemMovedToAmmo = inventory.GetSlot(Tibia.Constants.SlotNumber.Right);
                        itemMovedToAmmo.Move(new ItemLocation(Tibia.Constants.SlotNumber.Ammo));
                    }
                    else
                        return false; // No where to put the item!

                    // Move the original and say the magic words, make sure everything went well
                    allClear = allClear & original.Move(newLocation);
                    Thread.Sleep(200);
                    allClear = allClear & console.Say(item.Spell.Words);
                    Thread.Sleep(200);
                    // Don't bother continuing if both the above actions didn't work
                    if (!allClear) return false;

                    // Build an item object for the newly created item
                    // We don't use getSlot because it could execute too fast, returning a blank
                    // rune or nothing at all. If we just send a packet, the server will catch up.
                    Item newItem = new Item(item.Id, 0, new ItemLocation(Constants.SlotNumber.Right), this, true);

                    // Move the rune back to it's original location
                    allClear = allClear & newItem.Move(oldLocation);
                    // Check if we moved an item to the ammo slot
                    // If we did, move it back
                    if (itemMovedToAmmo != null)
                    {
                        itemMovedToAmmo.Loc = new ItemLocation(Tibia.Constants.SlotNumber.Ammo);
                        itemMovedToAmmo.Move(new ItemLocation(Tibia.Constants.SlotNumber.Right));
                    }
                    // Return true if everything worked well, false if it did not
                    return allClear;
                }
                else
                {
                    // No blanks found, return false
                    return false;
                }
            }
            else
            {
                // Not enough mana, return false
                return false;
            }
        }
        #endregion

        public bool Fish()
        {
            Player player = GetPlayer();
            List<Tile> fishes = map.GetFishTiles();
            if (fishes.Count > 0)
            {
                int tilenr = random.Next(fishes.Count - 1);
                inventory.UseItem(Tibia.Constants.Items.Tool.FishingRod, fishes[tilenr]);
                return true;
            }
            return false;
        }
        #endregion

        #region Client Functions
        /// <summary>
        /// Flashes the client's window and taskbar.
        /// </summary>
        public void Flash()
        {
            Util.WinApi.FlashWindow(MainWindowHandle, false);
        }

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            return Send(LogoutPacket.Create(this));
        }

        #region Account Info
        public void SetAccountInfo(int account, string password)
        {
            AccountNumber = account;
            AccountPassword = password;
            WriteBytes(Addresses.Client.LoginPatch, Tibia.Misc.CreateNopArray(5), 5);
        }

        public void ClearAccountInfo()
        {
            AccountNumber = 0;
            AccountPassword = string.Empty;
            WriteBytes(Addresses.Client.LoginPatch, Addresses.Client.LoginPatchOrig, 5);
            WriteBytes(Addresses.Client.LoginPatch2, Addresses.Client.LoginPatchOrig2, 5);
        }
        #endregion
        #endregion

        #region Login Server
        /// <summary>
        /// Get/Set the Login Servers
        /// </summary>
        public LoginServer[] LoginServers
        {
            get
            {
                LoginServer[] servers = new LoginServer[Addresses.Client.Max_LoginServers];
                long address = Addresses.Client.LoginServerStart;

                for (int i = 0; i < Addresses.Client.Max_LoginServers; i++)
                {
                    servers[i] = new LoginServer(
                        ReadString(address),
                        (short)ReadInt(address + Addresses.Client.Distance_Port)
                    );
                    address += Addresses.Client.Step_LoginServer;
                }
                return servers;
            }
            set
            {
                long address = Addresses.Client.LoginServerStart;
                if (value.Length == 1)
                {
                    string server = value[0].Server + (char)0;
                    for (int i = 0; i < Addresses.Client.Max_LoginServers; i++)
                    {
                        WriteString(address, value[0].Server);
                        WriteInt(address + Addresses.Client.Distance_Port, value[0].Port);
                        address += Addresses.Client.Step_LoginServer;
                    }
                }
                else if (value.Length > 1 && value.Length <= Addresses.Client.Max_LoginServers)
                {
                    string server = string.Empty;
                    for (int i = 0; i < value.Length; i++)
                    {
                        server = value[i].Server + (char)0;
                        WriteString(address, server);
                        WriteInt(address + Addresses.Client.Distance_Port, value[0].Port);
                        address += Addresses.Client.Step_LoginServer;
                    }
                }
            }
        }

        /// <summary>
        /// Set the client to connect to a different server and port.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool SetServer(string ip, short port)
        {
            bool result = true;
            long pointer = Addresses.Client.LoginServerStart;

            ip += (char)0;

            for (int i = 0; i < Addresses.Client.Max_LoginServers; i++)
            {
                result &= WriteString(pointer, ip);
                result &= WriteInt(pointer + Addresses.Client.Distance_Port, port);
                pointer += Addresses.Client.Step_LoginServer;
            }
            return result;
        }

        /// <summary>
        /// Set the client to connect to an OT server (changes IP, port, and RSA key).
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool SetOT(string ip, short port)
        {
            bool result = SetServer(ip, port);
            
            RSA = Constants.RSAKey.OpenTibia;

            return result;
        }

        /// <summary>
        /// Set the client to use the given OT server
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public bool SetOT(LoginServer ls)
        {
            return SetOT(ls.Server, ls.Port);
        }
        #endregion

        #region Sending Packets
        /// <summary>
        /// Send a packet to the server.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool Send(Packet packet)
        {
            if (packet.Destination == PacketDestination.Server)
            {
                return Send(packet.Data);
            }
            else
            {
                return SendToClient(packet.Data);
            }
        }

        /// <summary>
        /// Send a packet to the server.
        /// Uses the proxy if UsingProxy is true, and the dll otherwise.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool Send(byte[] packet)
        {
            if (UsingProxy)
            {
                if (proxy.Connected)
                {
                    proxy.SendToServer(packet);
                    return true;
                }
                else
                    throw new Exceptions.ProxyDisconnectedException();
            }
            else
            {
                return Packet.SendPacketWithDLL(this, packet);
            }
        }

        /// <summary>
        /// Send the specified packet to the client using the proxy.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        [Obsolete("Send filters by destination.")] 
        public bool SendToClient(Packet packet)
        {
            return SendToClient(packet.Data);
        }

        /// <summary>
        /// Sends a packet to the client using the proxy (not available if not using proxy).
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool SendToClient(byte[] packet)
        {
            if (proxy.Connected)
            {
                proxy.SendToClient(packet);
                return true;
            }
            else
                throw new Exceptions.ProxyRequiredException();
        }
        #endregion

        #region Proxy wrappers
        /// <summary>
        /// Whether or not the client is connected using a proxy.
        /// </summary>
        public bool UsingProxy
        {
            get { return usingProxy; }
            set { usingProxy = value; }
        }

        /// <summary>
        /// Start the proxy associated with this client.
        /// </summary>
        /// <returns>True if the proxy initialized correctly.</returns>
        public bool StartProxy()
        {
            if (openTibiaServer != null)
                proxy = new Util.Proxy(this, openTibiaServer);
            else
                proxy = new Util.Proxy(this);
            return UsingProxy;
        }

        /// <summary>
        /// Start the proxy using the given login server.
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public bool StartProxy(LoginServer ls)
        {
            proxy = new Util.Proxy(this, ls);
            return UsingProxy;
        }

        /// <summary>
        /// Get the proxy object associated with this client. 
        /// Will ruturn null unless StartProxy() is called first
        /// </summary>
        public Util.Proxy Proxy
        {
            get { return proxy; }
        }
        #endregion

        #region DLL Injection
        /// <summary>
        /// Inject a DLL into the process
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool InjectDLL(string filename)
        {
            if (!File.Exists(filename)) return false;
            // Get a block of memory to store the filename in the client
            IntPtr remoteAddress = Util.WinApi.VirtualAllocEx(process.Handle, IntPtr.Zero, (uint)filename.Length, Util.WinApi.MEM_COMMIT | Util.WinApi.MEM_RESERVE, Util.WinApi.PAGE_READWRITE);
            WriteString(remoteAddress.ToInt32(), filename);
            IntPtr thread = Util.WinApi.CreateRemoteThread(process.Handle, IntPtr.Zero, 0, Util.WinApi.GetProcAddress(Util.WinApi.GetModuleHandle("Kernel32"), "LoadLibraryA"), remoteAddress, 0, IntPtr.Zero);
            Util.WinApi.VirtualFreeEx(process.Handle, remoteAddress, (uint)filename.Length, Util.WinApi.MEM_RELEASE);
            return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
        }
        #endregion

        #region Pipe wrappers
        public void InitializePipe()
        {
            if (pipe != null)
                return;

            pipe = new Tibia.Util.Pipe(this, "TibiaAPI" + process.Id.ToString());
            pipe.OnConnected += new Tibia.Util.Pipe.PipeNotification(OnPipeConnect);

            if (!InjectDLL(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "TibiaAPI_Inject.dll")))
                throw new Tibia.Exceptions.InjectDLLNotFoundException();
        }

        private void OnPipeConnect()
        {
            //Set constants for displaying
            pipe.Send(Tibia.Packets.Pipes.SetConstantPacket.Create(this, "ptrPrintName", Tibia.Addresses.TextDisplay.PrintName));
            pipe.Send(Tibia.Packets.Pipes.SetConstantPacket.Create(this, "ptrPrintFPS", Tibia.Addresses.TextDisplay.PrintFPS));
            pipe.Send(Tibia.Packets.Pipes.SetConstantPacket.Create(this, "ptrShowFPS", Tibia.Addresses.TextDisplay.ShowFPS));
            pipe.Send(Tibia.Packets.Pipes.SetConstantPacket.Create(this, "ptrPrintTextFunc", Tibia.Addresses.TextDisplay.PrintTextFunc));
            pipe.Send(Tibia.Packets.Pipes.SetConstantPacket.Create(this, "ptrNopFPS", Tibia.Addresses.TextDisplay.NopFPS));

            //Hook Display functions
            pipe.Send(Tibia.Packets.Pipes.InjectDisplayPacket.Create(this, true));
            pipeIsReady.Set();
        }

        #endregion
    }
}
