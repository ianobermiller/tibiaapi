using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Tibia.Packets;
using System.IO;

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
        private Util.Proxy proxy;

        /// <summary>
        /// Keep a local copy of battleList to speed up GetPlayer()
        /// </summary>
        private BattleList battleList;
        private Map map;
        private Inventory inventory;
        private Random random;
        private Console console;
        private Util.DatReader dat;
        #endregion

        #region Events
        /// <summary>
        /// Prototype for client notifications.
        /// </summary>
        public delegate void ClientNotification();

        /// <summary>
        /// Event raised when the Tibia client is exited.
        /// </summary>
        public ClientNotification Exited;

        #endregion

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

            // The client get's it's own battle list to speed up getPlayer()
            battleList = new BattleList(this);
            map = new Map(this);
            inventory = new Inventory(this);
            console = new Console(this);
            random = new Random();
            dat = new Util.DatReader(this);
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

        public void Close()
        {
            if (process != null && !process.HasExited)
                process.Kill();
        }

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

      
        /** The following are all wrapper methods for Memory.Methods **/
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

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus Status()
        {
            return (Constants.LoginStatus)ReadByte(Addresses.Client.Status);
        }

        /// <summary>
        /// Check whether or not the client is logged in
        /// </summary>
        public bool LoggedIn
        {
            get
            {
                return Status() == Constants.LoginStatus.LoggedIn;
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
        /// Send a packet to the server.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool Send(Packet packet)
        {
            return Send(packet.Data);
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

        /// <summary>
        /// Get if this client is the active window, or bring it to the foreground
        /// </summary>
        public bool IsActive
        {
            get
            {
                return process.MainWindowHandle == Util.WinApi.GetForegroundWindow();
            }
            set
            {
                if (value)
                    Util.WinApi.SetForegroundWindow(process.MainWindowHandle);
            }
        }

        /// <summary>
        /// Check if the client is minimized
        /// </summary>
        /// <returns></returns>
        public bool IsMinimized()
        {
            return Util.WinApi.IsIconic(process.MainWindowHandle);
        }

        /// <summary>
        /// Check if the client is maximized
        /// </summary>
        /// <returns></returns>
        public bool IsMaximized()
        {
            return Util.WinApi.IsZoomed(process.MainWindowHandle);
        }

        public bool Visible
        {
            set
            {
                Util.WinApi.ShowWindow(process.MainWindowHandle, (int)((value) ? Util.WinApi.SW_SHOW : Util.WinApi.SW_HIDE));
                isVisible = value;
            }
            get { return isVisible; }
        }

        /// <summary>
        /// Return the character name.
        /// </summary>
        /// <returns>Character name</returns>
        public override string ToString()
        {
            string s = "[" + GetVersion() + "] ";
            if (!LoggedIn)
                s += "Not logged in.";
            else
                s += GetPlayer().Name;
            return s;
        }

        /// <summary>
        /// Get a list of all the open clients. Class method.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients()
        {
            Process[] processes = Process.GetProcessesByName("Tibia");
            List<Client> clients = new List<Client>(processes.Length);
            foreach (Process p in processes)
            {
                clients.Add(new Client(p));
            }
            return clients;
        }

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
        public BattleList GetBattleList()
        {
            return battleList;
        }

        /// <summary>
        /// Get the client's map.
        /// </summary>
        /// <returns></returns>
        public Map GetMap()
        {
            return map;
        }

        /// <summary>
        /// Get the client's DatReader.
        /// </summary>
        /// <returns></returns>
        public Util.DatReader GetDatReader()
        {
            return dat;
        }

        /// <summary>
        /// Get the client's inventory.
        /// </summary>
        /// <returns></returns>
        public Inventory GetInventory()
        {
            return inventory;
        }

        /// <summary>
        /// Get the time the client was started.
        /// </summary>
        /// <returns></returns>
        public int GetStartTime()
        {
            return startTime;
        }

        /// <summary>
        /// Get the client's process.
        /// </summary>
        public Process Process
        {
            get
            {
                return process;
            }
        }

        /// <summary>
        /// Get the client's version
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return process.MainModule.FileVersionInfo.FileVersion;
        }

        /// <summary>
        /// Eat food found in any container.
        /// </summary>
        /// <returns>True if eating succeeded, false if no food found or eating failed.</returns>
        public bool EatFood()
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Inventory inventory = new Inventory(this);
            Item food = inventory.FindItem(new Tibia.Constants.ItemList.Food());
            if (food.Found)
                return food.Use();
            else
                return false;
        }

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            return Send(LogoutPacket.Create(this));
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
        /// Make a rune with the specified id. Wrapper for makeRune(Rune).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the rune succeeded, false if the rune id doesn't exist or creation failed.</returns>
        public bool MakeRune(ushort id)
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Rune rune = new Tibia.Constants.ItemList.Rune().Find(delegate(Rune r) { return r.Id == id; });
            if (rune == null) return false;
            return MakeRune(rune);
        }

        /// <summary>
        /// Make the specified rune with the default options.
        /// </summary>
        /// <param name="rune">The rune to make.</param>
        /// <returns></returns>
        public bool MakeRune(Rune rune)
        {
            return MakeRune(rune, false);
        }

        /// <summary>
        /// Make a rune. Drags a blank to a free hand, casts the words, and moved the new rune back.
        /// If no free hand is found, but the ammo is open, it moved the item in the right hand down to ammo.
        /// </summary>
        /// <param name="rune">The rune to make.</param>
        /// <param name="checkSoulPoints">Whether or not to check for soul points.</param>
        /// <returns>True if everything went well, false if no blank was found or part or all of the process failed</returns>
        public bool MakeRune(Rune rune, bool checkSoulPoints)
        {
            if (!LoggedIn) throw new Exceptions.NotLoggedInException();
            Player player = GetPlayer();
            bool allClear = true; // Keeps a running total of success
            Item itemMovedToAmmo = null; // If we move an item from the ammo slot, store it here.

            // If wanted, check for soul points
            if (checkSoulPoints)
                if (player.Soul < rune.SoulPoints) return false;

            // Make sure the player has enough mana
            if (player.Mana >= rune.ManaPoints)
            {
                // Find the first blank rune
                Item blank = inventory.FindItem(Tibia.Constants.Items.Rune.Blank);

                // Make sure a blank rune was found
                if (blank.Found)
                {
                    // Save the current location of the blank rune
                    ItemLocation oldLocation = blank.Loc;

                    // The location where the rune will be made
                    ItemLocation newLocation;

                    // Determine the location to make the rune
                    if (inventory.GetSlot(Tibia.Constants.SlotNumber.Left).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Left);
                    else if (inventory.GetSlot(Tibia.Constants.SlotNumber.Right).Found)
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
                        return false; // No where to put the rune!

                    // Move the rune and say the magic words, make sure everything went well
                    allClear = allClear & blank.Move(newLocation);
                    allClear = allClear & console.Say(rune.Words);

                    // Don't bother continuing if both the above actions didn't work
                    if (!allClear) return false;

                    // Build a rune object for the newly created item
                    // We don't use getSlot because it could execute too fast, returning a blank
                    // rune or nothing at all. If we just send a packet, the server will catch up.
                    Item newRune = new Item(rune.Id, 1, new ItemLocation(Constants.SlotNumber.Right), this, true);

                    // Move the rune back to it's original location
                    allClear = allClear & newRune.Move(oldLocation);

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

        public bool Fish()
        {
            Player player = GetPlayer();
            List<Tile> fishes = map.GetFishTiles();
            if (fishes.Count > 0)
            {
                int tilenr = random.Next(fishes.Count - 1);
                Tile tilen = new Tile((uint)tilenr);
                tilen.Location = map.GetAbsoluteLocation(fishes[tilenr].Number);
                tilen.Id = fishes[tilenr].Id;
                if (Math.Abs(tilen.Location.X - player.Location.X) <= 7 &&
                    Math.Abs(tilen.Location.Y - player.Location.Y) <= 5 &&
                    tilen.Location.Z == player.Location.Z)
                {
                    inventory.UseItem(Tibia.Constants.Items.Tool.FishingRod, tilen);
                    return true;
                }
            }
            return false;
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
                Util.WinApi.SetWindowText(process.MainWindowHandle, value);
            }
        }

        /// <summary>
        /// Flashes the client's window and taskbar.
        /// </summary>
        public void Flash()
        {
            Util.WinApi.FlashWindow(process.MainWindowHandle, false);
        }

        /// <summary>
        /// Sets the Tibia client as the topmost application or not.
        /// </summary>
        public bool TopMost
        {
            set
            {
                Util.WinApi.SetWindowPos(process.MainWindowHandle, (value) ? Util.WinApi.HWND_TOPMOST : Util.WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, Util.WinApi.SWP_NOMOVE | Util.WinApi.SWP_NOSIZE);
            }
        }

        private void ClientExited(object sender, EventArgs e)
        {
            if (Exited != null)
                Exited();
        }

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
            proxy = new Util.Proxy(this);
            return UsingProxy;
        }

        /// <summary>
        /// Start the proxy using the given server and default prot.
        /// </summary>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public bool StartProxy(string serverIP)
        {
            proxy = new Util.Proxy(this, serverIP);
            return UsingProxy;
        }

        /// <summary>
        /// Start the proxy using the given server and port.
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        /// <returns></returns>
        public bool StartProxy(string serverIP, short serverPort)
        {
            proxy = new Util.Proxy(this, serverIP, serverPort);
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

    }
}
