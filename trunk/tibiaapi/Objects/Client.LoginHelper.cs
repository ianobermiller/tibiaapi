using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class LoginHelper
        {
            public readonly LoginServer[] DefaultServers = 
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

            private Client client;
            private LoginServer openTibiaServer = null;

            internal LoginHelper(Client client) { this.client = client; }

            #region Account Info

            public void SetAccountInfo(string account, string password)
            {
                AccountName = account;
                AccountPassword = password;
                client.Memory.WriteBytes(Addresses.Client.LoginPatch, Tibia.Misc.CreateNopArray(5), 5);
            }

            public void ClearAccountInfo()
            {
                AccountName = "";
                AccountPassword = string.Empty;
                client.Memory.WriteBytes(Addresses.Client.LoginPatch, Addresses.Client.LoginPatchOrig, 5);
                client.Memory.WriteBytes(Addresses.Client.LoginPatch2, Addresses.Client.LoginPatchOrig2, 5);
            }

            /// <summary>
            /// Sets the account number.
            /// </summary>
            public string AccountName
            {
                set { client.Memory.WriteString(Addresses.Client.LoginAccount, value); }
            }

            /// <summary>
            /// Sets the account password.
            /// </summary>
            public string AccountPassword
            {
                set { client.Memory.WriteString(Addresses.Client.LoginPassword, value); }
            }

            #endregion

            /// <summary>
            /// Get/Set the RSA key, wrapper for Memory.WriteRSA
            /// </summary>
            /// <returns></returns>
            public string RSA
            {
                get { return client.Memory.ReadString(Addresses.Client.RSA, 309); }
                set { Tibia.Memory.WriteRSA(client.ProcessHandle, Addresses.Client.RSA, value); }
            }

            public LoginServer OpenTibiaServer
            {
                get { return openTibiaServer; }
                set { openTibiaServer = value; }
            }

            public bool AutoLogin(string login, string password, string charName)
            {
                //if the player is logged or the window is minimazed return false.
                if (client.LoggedIn || client.Window.IsMinimized)
                    return false;

                //sure the screen is clean, no dialog open
                client.Input.SendKey(Keys.Escape);
                client.Input.SendKey(Keys.Escape);

                //reset the selected char value..
                client.Memory.WriteUInt32(Tibia.Addresses.Client.LoginSelectedChar, 0);
                //reset the char count value..
                client.Memory.WriteInt32(Tibia.Addresses.Client.LoginCharListLength, 0);

                //click the enter the game button
                client.Input.Click(120, client.Window.Size.Height - 250);

                //wait the dialog open
                int waitTime = 2000;
                while (!client.IsDialogOpen && client.DialogCaption != "Enter Game")
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    if (waitTime <= 0)
                        return false;
                }

                //now we have to send the login and the password
                client.Input.SendString(login);
                //press tab
                client.Input.SendKey(Keys.Tab);
                //put the pass
                client.Input.SendString(password);
                //press entrer..
                client.Input.SendKey(Keys.Enter);

                //wait for the charlist dialog
                waitTime = 4000;
                while (CharListCount == 0)
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    if (waitTime <= 0)
                        return false;
                }

                waitTime = 1000;
                while (client.DialogCaption == "Connecting")
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    if (waitTime <= 0)
                        return false;
                }

                Thread.Sleep(100);

                // Check if there is a message of the day
                if (client.DialogCaption != "Select Character")
                {
                    client.Input.SendKey(Keys.Enter);
                    Thread.Sleep(100);
                }

                //now we loop at the charlist to find the selected char..
                foreach (var ch in CharacterList)
                {

                    Thread.Sleep(100); //make sure the client process the msg
                    //we start at position 0
                    if (ch.CharName.ToLower() == charName.ToLower())
                    {
                        //we found the char
                        //lets press the entrer key
                        client.Input.SendKey(Keys.Enter);
                        return true;
                    }

                    //move to the next char
                    client.Input.SendKey(Keys.Down);
                }

                //char not found.
                return false;
            }

            /// <summary>
            /// Get/Set the Login Servers
            /// </summary>
            public LoginServer[] Servers
            {
                get
                {
                    LoginServer[] servers = new LoginServer[Addresses.Client.MaxLoginServers];
                    long address = Addresses.Client.LoginServerStart;

                    for (int i = 0; i < Addresses.Client.MaxLoginServers; i++)
                    {
                        servers[i] = new LoginServer(
                            client.Memory.ReadString(address),
                            (short)client.Memory.ReadInt32(address + Addresses.Client.DistancePort)
                        );
                        address += Addresses.Client.StepLoginServer;
                    }
                    return servers;
                }
                set
                {
                    long address = Addresses.Client.LoginServerStart;
                    if (value.Length == 1)
                    {
                        string server = value[0].Server + (char)0;
                        for (int i = 0; i < Addresses.Client.MaxLoginServers; i++)
                        {
                            client.Memory.WriteString(address, value[0].Server);
                            client.Memory.WriteInt32(address + Addresses.Client.DistancePort, value[0].Port);
                            address += Addresses.Client.StepLoginServer;
                        }
                    }
                    else if (value.Length > 1 && value.Length <= Addresses.Client.MaxLoginServers)
                    {
                        string server = string.Empty;
                        for (int i = 0; i < value.Length; i++)
                        {
                            server = value[i].Server + (char)0;
                            client.Memory.WriteString(address, server);
                            client.Memory.WriteInt32(address + Addresses.Client.DistancePort, value[0].Port);
                            address += Addresses.Client.StepLoginServer;
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

                for (int i = 0; i < Addresses.Client.MaxLoginServers; i++)
                {
                    result &= client.Memory.WriteString(pointer, ip);
                    result &= client.Memory.WriteInt32(pointer + Addresses.Client.DistancePort, port);
                    pointer += Addresses.Client.StepLoginServer;
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
                uint pointer = /*client.Memory.ReadUInt32(*/Addresses.Client.LoginCharList/*)*/;

                for (int i = 0; i < count; i++)
                {
                    pointer += 60;
                    client.Memory.WriteBytes(pointer, ipAddress, 4);
                    pointer += 4;
                    client.Memory.WriteString(pointer, ipAddress.ToIPString());
                    pointer += 16;
                    client.Memory.WriteUInt16(pointer, port);
                    pointer += 4; // 2 padding bytes..
                }
            }

            public bool SetCharListServer(CharacterLoginInfo[] charList)
            {
                byte count = CharListCount;

                if (count != charList.Length)
                    return false;

                uint pointer = /*client.Memory.ReadUInt32(*/Addresses.Client.LoginCharList/*)*/;

                for (int i = 0; i < count; i++)
                {
                    pointer += 60;
                    client.Memory.WriteUInt32(pointer, charList[i].WorldIP);
                    pointer += 4;
                    client.Memory.WriteString(pointer, BitConverter.GetBytes(charList[i].WorldIP).ToIPString());
                    pointer += 16;
                    client.Memory.WriteUInt16(pointer, charList[i].WorldPort);
                    pointer += 4; // 2 padding bytes..
                }

                return true;
            }

            public CharacterLoginInfo[] CharacterList
            {
                get
                {
                    CharacterLoginInfo[] charList = new CharacterLoginInfo[CharListCount];

                    uint pointer = client.Memory.ReadUInt32(Addresses.Client.LoginCharList);

                    for (int i = 0; i < charList.Length; i++)
                    {
                        charList[i].CharName = client.Memory.ReadString(pointer);
                        pointer += 30;
                        charList[i].WorldName = client.Memory.ReadString(pointer);
                        pointer += 30;
                        charList[i].WorldIP = client.Memory.ReadUInt32(pointer);
                        pointer += 4;
                        charList[i].WorldIPString = client.Memory.ReadString(pointer);
                        pointer += 16;
                        charList[i].WorldPort = client.Memory.ReadUInt16(pointer);
                        pointer += 4; // 2 padding bytes..
                    }

                    return charList;
                }
            }

            public byte CharListCount
            {
                get { return client.Memory.ReadByte(Addresses.Client.LoginCharListLength); }
            }
        }
    }
}
