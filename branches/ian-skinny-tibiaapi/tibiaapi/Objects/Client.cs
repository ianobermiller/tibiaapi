using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
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
    public partial class Client
    {
        #region Variables

        private Process process;
        private IntPtr processHandle;

        private int startTime;

        internal Location playerLocation = Location.Invalid;

        // References to commonly used objects
        private BattleList battleList;
        private Map map;
        private Inventory inventory;
        private Console console;
        private Screen screen;
        private Util.PathFinder pathFinder;
        private ContextMenu contextMenu;
        private MemoryHelper memory;
        private WindowHelper window;
        private IOHelper io;
        private LoginHelper login;
        private DllHelper dll;
        private InputHelper input;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the Tibia client is exited.
        /// </summary>
        public event EventHandler Exited;

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

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            processHandle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            pathFinder = new Tibia.Util.PathFinder(this);
            contextMenu = new ContextMenu(this);

            memory = new MemoryHelper(this);
            window = new WindowHelper(this);
            io = new IOHelper(this);
            login = new LoginHelper(this);
            dll = new DllHelper(this);
            input = new InputHelper(this);

            // Save the start time (it isn't changing)
            startTime = Memory.ReadInt32(Addresses.Client.StartTime);
        }

        /// <summary>
        /// Finalize this client, closing the handle.
        /// Called before the object is garbage collected.
        /// </summary>
        ~Client()
        {
            // Close the process handle
            Util.WinApi.CloseHandle(ProcessHandle);
        }
        #endregion

        #region Properties

        public Location PlayerLocation
        {
            get
            {
                if (IO.UsingProxy /*||(IO.RawSocket != null && IO.RawSocket.Enabled)*/)
                    return playerLocation;
                else if (LoggedIn)
                    return new Location(
                        Memory.ReadInt32(Addresses.Player.X),
                        Memory.ReadInt32(Addresses.Player.Y),
                        Memory.ReadInt32(Addresses.Player.Z));
                else
                    return Location.Invalid;
            }
        }

        public bool HasExited
        {
            get { return process.HasExited; }
        }

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus Status
        {
            get { return (Constants.LoginStatus)Memory.ReadByte(Addresses.Client.Status); }
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
            get { return Memory.ReadString(Addresses.Client.Statusbar_Text); }
            set 
            { 
                Memory.WriteByte(Addresses.Client.Statusbar_Time, 50);
                Memory.WriteString(Addresses.Client.Statusbar_Text, value);
                Memory.WriteByte(Addresses.Client.Statusbar_Text + value.Length, 0x00); 
            }
        }

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort LastSeenId
        {
            get { return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.See_Id, 2), 0); }
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort LastSeenCount
        {
            get { return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.See_Count, 2), 0); }
        }

        /// <summary>
        /// Gets the text of the last seen item/tile.
        /// </summary>
        public string LastSeenText
        {
            get { return Memory.ReadString(Addresses.Client.See_Text); }
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
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public bool DialogIsOpened
        {
            get { return (Memory.ReadInt32(Addresses.Client.DialogBegin) != 0); }
        }

        /// <summary>
        /// Gets the position of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public System.Drawing.Point DialogPosition
        {
            get
            {
                int DialogB = Memory.ReadInt32(Addresses.Client.DialogBegin);
                if (DialogB == 0)
                    return new System.Drawing.Point(0, 0);

                return new System.Drawing.Point(Memory.ReadInt32(DialogB + Addresses.Client.DialogLeft), Memory.ReadInt32(DialogB + Addresses.Client.DialogTop));
            }
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
            Tibia.Memory.WriteByte(handle, (long)Tibia.Addresses.Client.DMultiClient, Tibia.Addresses.Client.DMultiClientJMP);
            Util.WinApi.ResumeThread(pi.hThread);
            p.WaitForInputIdle();
            Tibia.Memory.WriteByte(handle, (long)Tibia.Addresses.Client.DMultiClient, Tibia.Addresses.Client.DMultiClientJNZ);
            Util.WinApi.CloseHandle(handle);
            Util.WinApi.CloseHandle(pi.hProcess);
            Util.WinApi.CloseHandle(pi.hThread);

            return new Client(p);
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
            
            int playerId = Memory.ReadInt32(Addresses.Player.Id);

            return new Player(this, BattleList.GetCreatures().
                First(c => c.Id == playerId).Address);
        }

        public MemoryHelper Memory
        {
            get { return memory; }
        }

        public WindowHelper Window
        {
            get { return window; }
        }

        public IOHelper IO
        {
            get { return io; }
        }

        public LoginHelper Login
        {
            get { return login; }
        }

        public DllHelper Dll
        {
            get { return dll; }
        }

        public InputHelper Input
        {
            get { return input; }
        }

        /// <summary>
        /// Get the client's battlelist.
        /// </summary>
        /// <returns></returns>
        public BattleList BattleList
        {
            get
            {
                if (battleList == null) battleList = new BattleList(this);
                return battleList; 
            }
        }

        /// <summary>
        /// Get the client's map.
        /// </summary>
        /// <returns></returns>
        public Map Map
        {
            get
            {
                if (map == null) map = new Map(this);
                return map;
            }
        }

        /// <summary>
        /// Get the client's inventory.
        /// </summary>
        /// <returns></returns>
        public Inventory Inventory
        {
            get
            {
                if (inventory == null) inventory = new Inventory(this);
                return inventory;
            }
        }

        /// <summary>
        /// Get the client's console.
        /// </summary>
        /// <returns></returns>
        public Console Console
        {
            get
            {
                if (console == null) console = new Console(this);
                return console;
            }
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
        /// Get the client's screen (for displaying text)
        /// </summary>
        public Screen Screen
        {
            get { return screen; }
        }

        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        public Util.PathFinder PathFinder
        {
            get { return pathFinder; }
        }

        #endregion

        #region Client Functions
        /// <summary>
        /// Gets or sets the follow mode.
        /// </summary>
        /// <returns></returns>
        public Constants.Follow FollowMode
        {
            get { return (Constants.Follow)Memory.ReadByte(Addresses.Client.FollowMode); }
            set { Memory.WriteByte(Addresses.Client.FollowMode, (byte)value); }
        }

        /// <summary>
        /// Gets or sets the action state.
        /// </summary>
        /// <returns></returns>
        public Constants.ActionState ActionState
        {
            get { return (Constants.ActionState)Memory.ReadByte(Addresses.Client.ActionState); }
            set { Memory.WriteByte(Addresses.Client.ActionState, (byte)value); }
        }

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            return Packets.Outgoing.LogoutPacket.Send(this);
        }

        #endregion

        #region DLL Injection
        #endregion
    }
}
