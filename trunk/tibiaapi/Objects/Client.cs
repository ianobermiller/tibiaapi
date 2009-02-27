using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
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
        #region Constants
        public static readonly LoginServer[] DefaultLoginServers = 
        {
            new LoginServer("login01.tibia.com"),
            new LoginServer("login02.tibia.com"),
            new LoginServer("login03.tibia.com"),
            new LoginServer("login04.tibia.com"),
            new LoginServer("login05.tibia.com"),
            new LoginServer("tibia01.cipsoft.com"),
            new LoginServer("tibia02.cipsoft.com"),
            new LoginServer("tibia03.cipsoft.com"),
            new LoginServer("tibia04.cipsoft.com"),
            new LoginServer("tibia05.cipsoft.com")
        };
        #endregion

        #region Variables

        private Process process;
        private IntPtr processHandle;
        private IntPtr pSender;

        private int startTime;
        private bool isVisible;
        private bool usingProxy = false;
        private bool sendCodeWritten = false;
        private LoginServer openTibiaServer = null;
        private AutoResetEvent pipeIsReady;
        int defBarY, defRectX, defRectY, defRectW, defRectH;

        internal Location playerLocation = Location.Invalid;

        // References to commonly used objects
        private BattleList battleList;
        private Map map;
        private Inventory inventory;
        private Random random;
        private Console console;
        private Util.RawSocket rawsocket;
        private Util.Proxy proxy;
        private Util.Pipe pipe = null; //For Displaying Text
        private Screen screen;
        private Util.PathFinder pathFinder;
        private ContextMenu contextMenu;
       

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the Tibia client is exited.
        /// </summary>
        public event EventHandler Exited;
        public event EventHandler PipeInizialazed;

        private void process_Exited(object sender, EventArgs e)
        {
            if (Exited != null)
                Exited.BeginInvoke(this, e, null, null);
        }

        #endregion

        #region Constructor/Destructor

        /// <summary>
        /// "Support" constructor
        /// </summary>
        /// <param name="p">used when necessary to use classes such as packet builder when working clientless</param>
        public Client() { }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="p">the client's process object</param>
        public Client(Process p)
        {
            process = p;
            process.Exited += new EventHandler(process_Exited);
            process.EnableRaisingEvents = true;
            
            // Wait until we can really access the process
            process.WaitForInputIdle();

            while (process.MainWindowHandle == IntPtr.Zero)
            {
                process.Refresh();
                System.Threading.Thread.Sleep(5);
            }

            // Save the start time (it isn't changing)
            startTime = ReadInt32(Addresses.Client.StartTime);

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            processHandle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            pipeIsReady = new AutoResetEvent(false);

            // The client get's it's own battle list to speed up getPlayer()
            battleList = new BattleList(this);
            map = new Map(this);
            inventory = new Inventory(this);
            console = new Console(this);
            random = new Random();
            screen = new Screen(this);

            pathFinder = new Tibia.Util.PathFinder(this);
            contextMenu = new ContextMenu(this);
        }

        /// <summary>
        /// Finalize this client, closing the handle.
        /// Called before the object is garbage collected.
        /// </summary>
        ~Client()
        {
            // Close the process handle
            Util.WinApi.CloseHandle(processHandle);
        }
        #endregion

        #region Properties

        public Location PlayerLocation
        {
            get
            {
                if (UsingProxy || (RawSocket != null && RawSocket.Enabled))
                    return playerLocation;
                else if (LoggedIn)
                    return GetPlayer().Location;
                else
                    return Location.Invalid;
            }
        }

        public bool HasExited
        {
            get { return process.HasExited; }
        }

        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        public Util.PathFinder PathFinder
        {
            get { return pathFinder; }
        }

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
            get { return process.MainWindowTitle; }
            set { Util.WinApi.SetWindowText(MainWindowHandle, value); }
        }

        /// <summary>
        /// Sets the Tibia client as the topmost application or not.
        /// </summary>
        public bool IsTopMost
        {
            set { Util.WinApi.SetWindowPos(MainWindowHandle, (value) ? Util.WinApi.HWND_TOPMOST :
                Util.WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, Util.WinApi.SWP_NOMOVE | Util.WinApi.SWP_NOSIZE); }
        }

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus Status
        {
            get { return (Constants.LoginStatus)ReadByte(Addresses.Client.Status); }
        }        

        public uint[] XteaKey
        {
            get 
            {
                //if we are using proxy the xteakey is parsed from the first login msg
                //so we dont have to read it from the clients memory.
                if (UsingProxy)
                    return proxy.XteaKey;
                else
                    return ReadBytes(Tibia.Addresses.Client.XTeaKey, 16).ToUInt32Array(); 
            }
        }

        /// <summary>
        /// Check whether or not the client is logged in
        /// </summary>
        public bool LoggedIn
        {
            get { return Status == Constants.LoginStatus.LoggedIn; }
        }

        /// <summary>
        /// Get and set the Statusbar text (the white text above the console).
        /// </summary>
        public string Statusbar
        {
            get { return ReadString(Addresses.Client.Statusbar_Text); }
            set 
            { 
                WriteByte(Addresses.Client.Statusbar_Time, 50);
                WriteString(Addresses.Client.Statusbar_Text, value);
                WriteByte(Addresses.Client.Statusbar_Text + value.Length, 0x00); 
            }
        }

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort LastSeenId
        {
            get { return BitConverter.ToUInt16(ReadBytes(Addresses.Client.See_Id, 2), 0); }
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort LastSeenCount
        {
            get { return BitConverter.ToUInt16(ReadBytes(Addresses.Client.See_Count, 2), 0); }
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
            { return MainWindowHandle == Util.WinApi.GetForegroundWindow(); }
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
                Util.WinApi.ShowWindow(MainWindowHandle, value ? Util.WinApi.SW_SHOW : Util.WinApi.SW_HIDE);
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
            get { return ReadString(Addresses.Client.RSA, 309); }
            set { Memory.WriteRSA(processHandle, Addresses.Client.RSA, value); }
        }

        /// <summary>
        /// Get the current FPS of the client.
        /// </summary>
        public double FPSCurrent
        {
            get
            {
                int frameRateBegin = ReadInt32(Addresses.Client.FrameRatePointer);
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
                int frameRateBegin = ReadInt32(Addresses.Client.FrameRatePointer);
                double value = 1000 / ReadDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset);
                
                //FIX HERE: Does the .net have some function to do this?
                double valueL = Math.Floor(value);
                if (valueL + 0.5 < value)
                    return Math.Ceiling(value);
                else
                    return valueL;
            }
            set
            {
                if (value <= 0) value = 1;
                int frameRateBegin = ReadInt32(Addresses.Client.FrameRatePointer);
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
        /// Sends a string to the client
        /// </summary>
        /// <param name="s"></param>
        public void SendString(string s)
        {
            foreach (var c in s)
                SendKey(Convert.ToInt32(c));
        }

        /// <summary>
        /// Sends a key to the client
        /// </summary>
        /// <param name="key"></param>
        public void SendKey(Keys key)
        {
            SendMessage(Hooks.WM_KEYDOWN, (int)key, 0);
            SendMessage(Hooks.VM_CHAR, (int)key, 0);
            SendMessage(Hooks.WM_KEYUP, (int)key, 0);
        }

        /// <summary>
        /// Sends a key to the client
        /// </summary>
        /// <param name="key"></param>
        public void SendKey(int key)
        {
            SendMessage(Hooks.WM_KEYDOWN, key, 0);
            SendMessage(Hooks.VM_CHAR, key, 0);
            SendMessage(Hooks.WM_KEYUP, key, 0);
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
        public string AccountName
        {
            set { WriteString(Addresses.Client.LoginAccount, value); }
        }

        /// <summary>
        /// Sets the account password.
        /// </summary>
        public string AccountPassword
        {
            set { WriteString(Addresses.Client.LoginPassword, value); }
        }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public bool DialogIsOpened
        {
            get { return (ReadInt32(Addresses.Client.DialogBegin) != 0); }
        }

        /// <summary>
        /// Gets the position of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public System.Drawing.Point DialogPosition
        {
            get
            {
                int DialogB = ReadInt32(Addresses.Client.DialogBegin);
                if (DialogB == 0)
                    return new System.Drawing.Point(0, 0);

                return new System.Drawing.Point(ReadInt32(DialogB + Addresses.Client.DialogLeft), ReadInt32(DialogB + Addresses.Client.DialogTop));
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
            return Open(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Tibia\tibia.exe"));
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
            return new Client(p);
        }


        public static Client OpenMC()
        {
            return OpenMC(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Tibia\tibia.exe"), "");
        }

        /// <summary>
        /// Opens a client with dynamic multi-clienting support
        /// </summary>
        public static Client OpenMC(string path, string arguments)
        {
            Util.WinApi.PROCESS_INFORMATION pi = new Tibia.Util.WinApi.PROCESS_INFORMATION();
            Util.WinApi.STARTUPINFO si = new Tibia.Util.WinApi.STARTUPINFO();
            
            if (arguments == null)
                arguments = "";

            Util.WinApi.CreateProcess(path, " " + arguments, IntPtr.Zero, IntPtr.Zero,
                false, Util.WinApi.CREATE_SUSPENDED, IntPtr.Zero,
                System.IO.Path.GetDirectoryName(path), ref si, out pi);

            IntPtr handle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, pi.dwProcessId);
            Process p = Process.GetProcessById(Convert.ToInt32(pi.dwProcessId));
            Memory.WriteByte(handle, (long)Tibia.Addresses.Client.DMultiClient, Tibia.Addresses.Client.DMultiClientJMP);
            Util.WinApi.ResumeThread(pi.hThread);
            p.WaitForInputIdle();
            Memory.WriteByte(handle, (long)Tibia.Addresses.Client.DMultiClient, Tibia.Addresses.Client.DMultiClientJNZ);
            Util.WinApi.CloseHandle(handle);
            Util.WinApi.CloseHandle(pi.hProcess);
            Util.WinApi.CloseHandle(pi.hThread);

            return new Client(p);
        }           

        #endregion        

        #region Memory Methods

        public byte[] ReadBytes(long address, uint bytesToRead)
        {
            return Memory.ReadBytes(processHandle, address, bytesToRead);
        }

        public byte ReadByte(long address)
        {
            return Memory.ReadByte(processHandle, address);
        }

        public short ReadInt16(long address)
        {
            return Memory.ReadInt16(processHandle, address);
        }

        public ushort ReadUInt16(long address)
        {
            return Memory.ReadUInt16(processHandle, address);
        }

        [Obsolete("Please use ReadInt16")]
        public short ReadShort(long address)
        {
            return Memory.ReadInt16(processHandle, address);
        }

        public int ReadInt32(long address)
        {
            return Memory.ReadInt32(processHandle, address);
        }

        public uint ReadUInt32(long address)
        {
            return Memory.ReadUInt32(processHandle, address);
        }

        [Obsolete("Please use ReadInt32")]
        public int ReadInt(long address)
        {
            return Memory.ReadInt32(processHandle, address);
        }

        public double ReadDouble(long address)
        {
            return Memory.ReadDouble(processHandle, address);
        }

        public string ReadString(long address)
        {
            return Memory.ReadString(processHandle, address);
        }

        public string ReadString(long address, uint length)
        {
            return Memory.ReadString(processHandle, address, length);
        }

        public bool WriteBytes(long address, byte[] bytes, uint length)
        {
            return Memory.WriteBytes(processHandle, address, bytes, length);
        }

        public bool WriteInt16(long address, short value)
        {
            return Memory.WriteInt16(processHandle, address, value);
        }

        public bool WriteUInt16(long address, ushort value)
        {
            return Memory.WriteUInt16(processHandle, address, value);
        }

        public bool WriteInt32(long address, int value)
        {
            return Memory.WriteInt32(processHandle, address, value);
        }

        public bool WriteUInt32(long address, uint value)
        {
            return Memory.WriteUInt32(processHandle, address, value);
        }

        [Obsolete("Please use WriteInt32")]
        public bool WriteInt(long address, int value)
        {
            return Memory.WriteInt32(processHandle, address, value);
        }

        public bool WriteDouble(long address, double value)
        {
            return Memory.WriteDouble(processHandle, address, value);
        }

        public bool WriteByte(long address, byte value)
        {
            return Memory.WriteByte(processHandle, address, value);
        }

        public bool WriteString(long address, string str)
        {
            return Memory.WriteString(processHandle, address, str);
        }

        public bool WriteStringNoEncoding(long address, string str)
        {
            return Memory.WriteStringNoEncoding(processHandle, address, str);
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

            foreach (Process process in Process.GetProcesses())
            {
                StringBuilder classname = new StringBuilder();
                Util.WinApi.GetClassName(process.MainWindowHandle, classname, 12);

                if (classname.ToString().Equals("TibiaClient", StringComparison.CurrentCultureIgnoreCase))
                    clients.Add(new Client(process));
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
            if (!LoggedIn) 
                throw new Exceptions.NotLoggedInException();

            return new Player(this, battleList.GetCreature(ReadInt32(Addresses.Player.Id)).Address);
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
        /// Get the client's process handle
        /// </summary>
        public IntPtr ProcessHandle
        {
            get { return processHandle; }
        }

        /// <summary>
        /// Get the base address of our send function
        /// </summary>
        public IntPtr SenderAddress
        {
            get { return pSender; }
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
        ///
        /// </summary>
        /// <param name="login">The account name.</param>
        /// <param name="password">The account password.</param>
        /// <param name="charName">The character name.</param>
        /// <returns></returns>
        public bool AutoLogin(string login, string password, string charName)
        {
            //if the player is logged or the window is minimazed return false.
            if (LoggedIn || IsMinimized)
                return false;

            //sure the screen is clean, no dialog open
            SendKey(Keys.Escape);
            SendKey(Keys.Escape);

            //reset the selected char value..
            WriteUInt32(Tibia.Addresses.Client.LoginSelectedChar, 0);
            //reset the char count value..
            WriteInt32(Tibia.Addresses.Client.LoginCharListLength, 0);

            //click the enter the game button
            Click(120, Window.Height - 250);

            //wait the dialog open
            int waitTime = 4000;
            while (!DialogIsOpened && waitTime > 0)
            {
                Thread.Sleep(10);
                waitTime -= 10;
            }

            //4 sec and the dialog did not open..
            if (waitTime <= 0 && !DialogIsOpened)
                return false;

            //now we have to send the login and the password
            SendString(login);
            //press tab
            SendKey(Keys.Tab);
            //put the pass
            SendString(password);
            //press entrer..
            SendKey(Keys.Enter);

            //wait for the charlist dialog
            waitTime = 4000; // 2 sec
            while (CharListCount == 0 && waitTime > 0)
            {
                Thread.Sleep(10);
                waitTime -= 10;
            }

            //timeout
            if (waitTime <= 0 && CharListCount == 0)
                return false;

            //now we loop at the charlist to find the selected char..
            foreach (var ch in CharList)
            {

                Thread.Sleep(100); //make sure the client process the msg
                //we start at position 0
                if (ch.CharName.ToLower() == charName.ToLower())
                {
                    //we found the char
                    //lets press the entrer key
                    SendKey(Keys.Enter);
                    return true;
                }

                //move to the next char
                SendKey(Keys.Down);       
            }

            //char not found.
            return false;
        }


        /// <summary>
        /// This will set the FPSLimit with the value you give
        /// NOTE: The official value is 1000/fpsmax
        /// </summary>
        /// <param name="value"></param>
        public void SetFPSLimit(double value)
        {
            int frameRateBegin = ReadInt32(Addresses.Client.FrameRatePointer);
            WriteDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset, value);  
        }

        /// <summary>
        /// Eat food found in any container.
        /// </summary>
        /// <returns>True if eating succeeded, false if no food found or eating failed.</returns>
        public bool EatFood()
        {
            if (!LoggedIn) 
                throw new Exceptions.NotLoggedInException();

            Item food = inventory.FindItem(Tibia.Constants.ItemLists.Foods.Values);

            if (food.Found)
                return food.Use();
            else
                return false;
        }

        /// <summary>
        /// Gets or sets world only view.
        /// </summary>
        /// <returns></returns>
        public bool WorldOnlyView
        {
            get
            {
                int screenBar;
                screenBar = ReadInt32(Addresses.Client.GameWindowBar);
                return ReadInt32(screenBar + 0x70) == Window.Height;
            }
            set
            {
                int screenRect, screenBar;
                screenRect = ReadInt32(Addresses.Client.GameWindowRectPointer);
                screenRect = ReadInt32(screenRect + 0x18 + 0x04);
                screenBar = ReadInt32(Addresses.Client.GameWindowBar);

                if (value && ReadInt32(screenBar + 0x70) != Window.Height)
                {
                    defBarY = ReadInt32(screenBar + 0x70);
                    defRectX = ReadInt32(screenRect + 0x14);
                    defRectY = ReadInt32(screenRect + 0x18);
                    defRectW = ReadInt32(screenRect + 0x1C);
                    defRectH = ReadInt32(screenRect + 0x20);
                    WriteInt32(screenBar + 0x70, Window.Height);
                    WriteInt32(screenRect + 0x14, 0);
                    WriteInt32(screenRect + 0x18, 0);
                    WriteInt32(screenRect + 0x1C, Window.Width);
                    WriteInt32(screenRect + 0x20, Window.Height);
                }
                else if (!value && defBarY != 0 && defRectX!= 0 &&
                    defRectY != 0 && defRectW != 0 && defRectH != 0)
                {
                    WriteInt32(screenBar + 0x70, defBarY);
                    WriteInt32(screenRect + 0x14, defRectX);
                    WriteInt32(screenRect + 0x18, defRectY);
                    WriteInt32(screenRect + 0x1C, defRectW);
                    WriteInt32(screenRect + 0x20, defRectH);
                }
            }
        }

        /// <sumary>
        /// Gets or sets wide screen view
        /// </sumary>
        public bool WideScreenView
        {
            get
            {
                int screenRect, screenBar;
                screenRect = ReadInt32(Addresses.Client.GameWindowRectPointer);
                screenRect = ReadInt32(screenRect + 0x18 + 0x04);
                screenBar = ReadInt32(Addresses.Client.GameWindowBar);
                return this.ReadInt32(screenRect + 0x14) == 5;
            }
            set
            {
                int screenRect, screenBar;
                screenRect = this.ReadInt32(Tibia.Addresses.Client.GameWindowRectPointer);
                screenRect = this.ReadInt32(screenRect + 0x18 + 0x4);
                screenBar = this.ReadInt32(Tibia.Addresses.Client.GameWindowBar);

                if (value && !this.WideScreenView)
                {
                    defRectX = ReadInt32(screenRect + 0x14);
                    defRectW = ReadInt32(screenRect + 0x1C);
                    this.WriteInt32(screenRect + 0x14, 5);
                    this.WriteInt32(screenRect + 0x1C, this.ReadInt32(screenBar + 0x74) - 10);
                }
                else if (!value && defRectX != 0 && defRectW != 0)
                {
                    this.WriteInt32(screenRect + 0x14, defRectX);
                    this.WriteInt32(screenRect + 0x1C, defRectW);
                }
            }

        }

        /// <summary>
        /// Gets or sets the follow mode.
        /// </summary>
        /// <returns></returns>
        public Constants.Follow FollowMode
        {
            get { return (Constants.Follow)ReadByte(Addresses.Client.FollowMode); }
            set { WriteByte(Addresses.Client.FollowMode, (byte)value); }
        }

        /// <summary>
        /// Gets or sets the action state.
        /// </summary>
        /// <returns></returns>
        public Constants.ActionState ActionState
        {
            get { return (Constants.ActionState)ReadByte(Addresses.Client.ActionState); }
            set { WriteByte(Addresses.Client.ActionState, (byte)value); }
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
            if (!LoggedIn) 
                throw new Exceptions.NotLoggedInException();

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
                    ItemLocation newLocation = null;

                    // Determine the location to make the item
                    /*if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Left).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Left);
                    else*/
                    if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Right).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Right);
                    else if (!inventory.GetSlot(Tibia.Constants.SlotNumber.Left).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Left);
                    if (newLocation == null)
                        if(!inventory.GetSlot(Tibia.Constants.SlotNumber.Ammo).Found)
                        {
                            // If no hands are free, but the ammo slot is, 
                            // move the right hand item to clear the ammo slot
                            newLocation = new ItemLocation(Constants.SlotNumber.Right);
                            itemMovedToAmmo = inventory.GetSlot(Tibia.Constants.SlotNumber.Right);
                            itemMovedToAmmo.Move(new ItemLocation(Tibia.Constants.SlotNumber.Ammo));
                        }

                    else
                        return false; // No where to put the item!

                    // Move the original and say the magic words, make sure everything went well
                    Thread.Sleep(200);
                    allClear = allClear & original.Move(newLocation);
                    Thread.Sleep(200);
                    allClear = allClear & console.Say(item.Spell.Words);
                    Thread.Sleep(200);
                    // Don't bother continuing if both the above actions didn't work
                    if (!allClear) 
                        return false;

                    // Build an item object for the newly created item
                    // We don't use getSlot because it could execute too fast, returning a blank
                    // rune or nothing at all. If we just send a packet, the server will catch up.
                    Item newItem = new Item(this, item.Id, 0, "", newLocation, true);

                    // Move the rune back to it's original location
                    Thread.Sleep(300);
                    allClear = allClear & newItem.Move(oldLocation);
                    // Check if we moved an item to the ammo slot
                    // If we did, move it back
                    Thread.Sleep(200);
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
                inventory.UseItem(Tibia.Constants.Items.Tool.FishingRod, fishes[random.Next(fishes.Count - 1)]);
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
            return Packets.Outgoing.LogoutPacket.Send(this);
        }

        #region Account Info

        public void SetAccountInfo(string account, string password)
        {
            AccountName = account;
            AccountPassword = password;
            WriteBytes(Addresses.Client.LoginPatch, Tibia.Misc.CreateNopArray(5), 5);
        }

        public void ClearAccountInfo()
        {
            AccountName = "";
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
                        (short)ReadInt32(address + Addresses.Client.Distance_Port)
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
                        WriteInt32(address + Addresses.Client.Distance_Port, value[0].Port);
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
                        WriteInt32(address + Addresses.Client.Distance_Port, value[0].Port);
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
                result &= WriteInt32(pointer + Addresses.Client.Distance_Port, port);
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

        public void SetCharListServer(byte[] ipAddress, ushort port)
        {
            byte count = CharListCount;
            uint pointer = ReadUInt32(Addresses.Client.LoginCharList);

            for (int i = 0; i < count; i++)
            {
                pointer += 60;
                WriteBytes(pointer, ipAddress, 4);
                pointer += 4;
                WriteString(pointer, ipAddress.ToIPString());
                pointer += 16;
                WriteUInt16(pointer, port);
                pointer += 4; // 2 padding bytes..
            }
        }

        public bool SetCharListServer(CharacterLoginInfo[] charList)
        {
            byte count = CharListCount;

            if (count != charList.Length)
                return false;

            uint pointer = ReadUInt32(Addresses.Client.LoginCharList);

            for (int i = 0; i < count; i++)
            {
                pointer += 60;
                WriteUInt32(pointer, charList[i].WorldIP);
                pointer += 4;
                WriteString(pointer, BitConverter.GetBytes(charList[i].WorldIP).ToIPString());
                pointer += 16;
                WriteUInt16(pointer, charList[i].WorldPort);
                pointer += 4; // 2 padding bytes..
            }

            return true;
        }

        public CharacterLoginInfo[] CharList
        {
            get
            {
                CharacterLoginInfo[] charList = new CharacterLoginInfo[CharListCount];

                uint pointer = ReadUInt32(Addresses.Client.LoginCharList);

                for (int i = 0; i < charList.Length; i++)
                {
                    charList[i].CharName = ReadString(pointer);
                    pointer += 30;
                    charList[i].WorldName = ReadString(pointer);
                    pointer += 30;
                    charList[i].WorldIP = ReadUInt32(pointer);
                    pointer += 4;
                    charList[i].WorldIPString = ReadString(pointer);
                    pointer += 16;
                    charList[i].WorldPort = ReadUInt16(pointer);
                    pointer += 4; // 2 padding bytes..
                }

                return charList;
            }
        }

        public byte CharListCount
        {
            get { return ReadByte(Addresses.Client.LoginCharListLength); }
        }

        #endregion

        #region RawSocket wrappers

        public void StartRawSocket()
        {
            StartRawSocket(true);
        }

        public void StartRawSocket(bool Adler)
        {
            if (LoggedIn)
                playerLocation = GetPlayer().Location;

            if (rawsocket == null)
                rawsocket = new Tibia.Util.RawSocket(this, Adler);
            
            rawsocket.Enabled = true;
        }

        public void StartRawSocket(bool Adler, string localIp)
        {
            if (LoggedIn)
                playerLocation = GetPlayer().Location;

            if (rawsocket == null)
                rawsocket = new Tibia.Util.RawSocket(this, Adler, localIp);

            rawsocket.Enabled = true;
        }

        public void StopRawSocket()
        {
            if (rawsocket != null) 
                rawsocket.Enabled = false;
        }

        public Util.RawSocket RawSocket
        {
            get { return rawsocket; }
        }
        #endregion

        #region Proxy wrappers

        public bool SendToServer(byte[] packet)
        {
            if (UsingProxy)
            {
                Packets.NetworkMessage msg = new NetworkMessage(this);
                msg.AddBytes(packet);
                msg.PrepareToSend();

                proxy.SendToServer(msg);

                return true;
            }
            else
                return Packets.OutgoingPacket.SendPacketWithDLL(this, packet);
        }

        public bool SendToClient(byte[] packet)
        {
            if (UsingProxy)
            {
                Packets.NetworkMessage msg = new NetworkMessage(this);
                msg.AddBytes(packet);
                msg.PrepareToSend();

                proxy.SendToClient(msg);

                return true;
            }

            return false;
        }

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
            return StartProxy(false);
        }

        public bool StartProxy(bool debug)
        {
            proxy = new Tibia.Util.Proxy(this, debug);
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

        #region Socket.Send wrappers
        /// <summary>
        /// Checks if the code to call send functions has already been written to memory
        /// </summary>
        public bool IsSendCodeWritten
        {
            get
            {
                return sendCodeWritten;
            }
        }

        public bool WriteSocketSendCode()
        {
            byte[] OpCodes = new byte[]{
                		0x6A, 0x00,							//push	0						;_flag
		                0xFF, 0x33,							//push	dword ptr [ebx]			;_length
		                0x83, 0xC3, 0x04,					//add	ebx, 4					
		                0x53,								//push	ebx						;_buffer
		                0xA1, 0xFF, 0xFF, 0xFF, 0xFF,		//mov	eax, ds:SocketStruct	;_socketstruct
		                0xFF, 0x70, 0x04,					//push	dword ptr [eax+4]		;_socket
		                0xFF, 0x15, 0xFF, 0xFF, 0xFF, 0xFF,	//call	dword ptr ds:Send		;call send 
		                0xC3								//retn
	        };

            Array.Copy(BitConverter.GetBytes(Tibia.Addresses.Client.SocketStruct), 0, OpCodes, 9, 4);
            Array.Copy(BitConverter.GetBytes(Tibia.Addresses.Client.SendPointer), 0, OpCodes, 18, 4);

            if(pSender==IntPtr.Zero)    
                pSender = Tibia.Util.WinApi.VirtualAllocEx(processHandle, IntPtr.Zero, (uint)OpCodes.Length,
                Tibia.Util.WinApi.MEM_COMMIT | Tibia.Util.WinApi.MEM_RESERVE, Tibia.Util.WinApi.PAGE_EXECUTE_READWRITE);
            if (pSender != IntPtr.Zero)
            {

                if (WriteBytes(pSender.ToInt64(),OpCodes,(uint)OpCodes.Length))                    
                {
                    sendCodeWritten = true;
                    return true;
                }
                Tibia.Util.WinApi.VirtualFreeEx(processHandle, pSender, 0, Tibia.Util.WinApi.MEM_RELEASE);
                pSender = IntPtr.Zero;
            }
            sendCodeWritten = false;
            return false;

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
            WriteStringNoEncoding(remoteAddress.ToInt32(), filename);
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
            contextMenu.AddInternalEvents();

            if (!InjectDLL(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "TibiaAPI_Inject.dll")))
                throw new Tibia.Exceptions.InjectDLLNotFoundException();
        }

        private void OnPipeConnect()
        {
            //Set constants for displaying
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.PrintName, Tibia.Addresses.TextDisplay.PrintName);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.PrintFPS, Tibia.Addresses.TextDisplay.PrintFPS);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.ShowFPS, Tibia.Addresses.TextDisplay.ShowFPS);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.PrintTextFunc, Tibia.Addresses.TextDisplay.PrintTextFunc);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.NopFPS, Tibia.Addresses.TextDisplay.NopFPS);

            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.AddContextMenuFunc, Tibia.Addresses.ContextMenus.AddContextMenuPtr);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.OnClickContextMenu, Tibia.Addresses.ContextMenus.OnClickContextMenuPtr);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.SetOutfitContextMenu, Tibia.Addresses.ContextMenus.AddSetOutfitContextMenu);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.PartyActionContextMenu, Tibia.Addresses.ContextMenus.AddPartyActionContextMenu);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.CopyNameContextMenu, Tibia.Addresses.ContextMenus.AddCopyNameContextMenu);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.TradeWithContextMenu, Tibia.Addresses.ContextMenus.AddTradeWithContextMenu);
            Packets.Pipes.SetConstantPacket.Send(this, PipeConstantType.OnClickContextMenuVf, Tibia.Addresses.ContextMenus.OnClickContextMenuVf);

            //Hook Display functions
            Packets.Pipes.InjectDisplayPacket.Send(this, true);
            pipeIsReady.Set();

            if (PipeInizialazed != null)
                PipeInizialazed.BeginInvoke(this, new EventArgs(), null, null);

        }

        #endregion
    }
}
