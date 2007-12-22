using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a single Tibia Client. Contains wrapper methods 
    /// for memory, packet sending, battlelist, and slots. Also contains
    /// any "helper methods" that automate tasks, such as making a rune.
    /// </summary>
    public class Client
    {
        #region Windows API Import
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        private Process process;
        private IntPtr handle;
        private int startTime;

        /// <summary>
        /// Keep a local copy of battleList to speed up GetPlayer()
        /// </summary>
        private BattleList battleList;

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="p">the client's process object</param>
        public Client(Process p)
        {
            process = p;

            // Save the start time (it isn't changing)
            startTime = ReadInt(Addresses.Client.StartTime);

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            handle = Memory.OpenProcess(Memory.PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            // The client get's it's own battle list to speed up getPlayer()
            battleList = new BattleList(this);
        }

        /// <summary>
        /// Uninitialize this client, closing the handle.
        /// Call when you are no longer using a client.
        /// </summary>
        public void Uninitialize()
        {
            // Close the process handle
            Memory.CloseHandle(handle);
        }

        /** The following are all wrapper methods for Memory.Methods **/
        #region Memory Methods
        public byte[] ReadBytes(long address, uint bytesToRead)
        {
            return Memory.ReadBytes(handle, address, bytesToRead);
        }

        public int ReadInt(long address)
        {
            return Memory.ReadInt(handle, address);
        }

        public byte ReadByte(long address)
        {
            return Memory.ReadByte(handle, address);
        }

        public string ReadString(long address)
        {
            return Memory.ReadString(handle, address);
        }

        public bool WriteBytes(long address, byte[] bytes, uint length)
        {
            return Memory.WriteBytes(handle, address, bytes, length);
        }

        public bool WriteInt(long address, int value)
        {
            return Memory.WriteInt(handle, address, value);
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
        /// Check whether or not the client is logged in.
        /// </summary>
        /// <returns></returns>
        public bool LoggedIn()
        {
            return Status() == Constants.LoginStatus.LoggedIn;
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
        /// Wrapper method for Packets.Packet.SendPacket.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool Send(byte[] packet)
        {
            return Packet.SendPacket(this, packet);
        }

        /// <summary>
        /// Bring this Tibia window to the foreground. Wrapper for SetForegroundWindow.
        /// </summary>
        /// <returns></returns>
        public bool BringToFront()
        {
            return SetForegroundWindow(process.MainWindowHandle);
        }

        /// <summary>
        /// Return the character name.
        /// </summary>
        /// <returns>Character name</returns>
        public override string ToString()
        {
            if (!LoggedIn()) return "Not logged in.";
            return GetPlayer().Name;
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
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
            Creature creature = battleList.GetCreature(ReadInt(Addresses.Player.Id));
            return new Player(this, creature.Address);
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
        /// <returns></returns>
        public Process GetProcess()
        {
            return process;
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
        /// Get the client's battlelist.
        /// </summary>
        /// <returns></returns>
        public BattleList GetBattleList()
        {
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
            return battleList;
        }

        /// <summary>
        /// Get the content of a player's slot.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Item GetSlot(Constants.SlotNumber s)
        {
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
            return new Slot(this).GetSlot(s);
        }

        /// <summary>
        /// Make a rune with the specified id. Wrapper for makeRune(Rune).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the rune succeeded, false if the rune id doesn't exist or creation failed.</returns>
        public bool MakeRune(ushort id)
        {
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
            Rune rune = new Tibia.Constants.ItemList.Rune().Find(delegate(Rune r) { return r.Id == id; });
            if (rune == null) return false;
            return MakeRune(rune);
        }

        /// <summary>
        /// Eat food found in any container.
        /// </summary>
        /// <returns>True if eating succeeded, false if no food found or eating failed.</returns>
        public bool EatFood()
        {
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
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
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = 0x14;
            return Send(packet);
        }

        /// <summary>
        /// Set the RSA key, wrapper for Memory.WriteRSA
        /// </summary>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public bool SetRSA(string newKey)
        {
            return Memory.WriteRSA(handle, Addresses.Client.RSA, newKey);
        }

        /// <summary>
        /// Set the client to connect to a different server and port.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool SetServer(string ip, int port)
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
        public bool SetOT(string ip, int port)
        {
            bool result = SetServer(ip, port);
            
            result &= SetRSA(Constants.RSAKey.OpenTibia);

            return result;
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
            if (!LoggedIn()) throw new Exceptions.NotLoggedInException();
            Inventory inventory = new Inventory(this);
            Console console = new Console(this);
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
                    if (GetSlot(Tibia.Constants.SlotNumber.Left).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Left);
                    else if (GetSlot(Tibia.Constants.SlotNumber.Right).Found)
                        newLocation = new ItemLocation(Constants.SlotNumber.Right);
                    else if (!GetSlot(Tibia.Constants.SlotNumber.Ammo).Found)
                    {
                        // If no hands are open, but the ammo slot is, 
                        // move the right hand item to clear the ammo slot
                        newLocation = new ItemLocation(Constants.SlotNumber.Right);
                        itemMovedToAmmo = GetSlot(Tibia.Constants.SlotNumber.Right);
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
                    Item newRune = new Item(rune.Id, 1, new ItemLocation(Constants.SlotNumber.Right), true);

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
    }
}
