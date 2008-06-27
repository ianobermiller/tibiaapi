using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Tibia.Objects;

namespace Tibia.Util
{

    //The proxy seems to only work on the first character. Probably has to
    //do with the SetCharacterListIp method.

    public class Proxy
    {
        #region Variables
        private Socket socketClient;

        private NetworkStream netStreamClient;
        private NetworkStream netStreamServer;
        private NetworkStream netStreamLogin;

        private TcpListener tcpClient;
        private TcpClient   tcpServer;
        private TcpClient   tcpLogin;

        private const string Localhost = "127.0.0.1";
        private byte[]       LocalhostBytes = new byte[] { 127, 0, 0, 1 };
        private const int    DefaultPort = 7171;
        private byte[]       DefaultPortBytes = BitConverter.GetBytes((short)7171);

        private Client        client;
        private CharListPacket        charList;
        private byte                  selectedChar;
        private byte[]                data = new byte[4096];
        private LoginServer[] loginServers = new LoginServer[] {
            new LoginServer("login01.tibia.com", 7171),
            new LoginServer("login02.tibia.com", 7171),
            new LoginServer("login03.tibia.com", 7171),
            new LoginServer("login04.tibia.com", 7171),
            new LoginServer("login05.tibia.com", 7171),
            new LoginServer("tibia01.cipsoft.com", 7171),
            new LoginServer("tibia02.cipsoft.com", 7171),
            new LoginServer("tibia03.cipsoft.com", 7171),
            new LoginServer("tibia04.cipsoft.com", 7171),
            new LoginServer("tibia05.cipsoft.com", 7171)
        };
        #endregion

        #region Events
        /// <summary>
        /// A generic function prototype for packet events.
        /// </summary>
        /// <param name="packet">The unencrypted packet that was recieved.</param>
        /// <returns>The unencrypted packet to be forwarded. If null, the packet will not be forwarded.</returns>
        public delegate byte[] PacketListener(byte[] packet);

        /// <summary>
        /// A function prototype for proxy notifications.
        /// </summary>
        /// <returns></returns>
        public delegate byte[] ProxyNotification();

        /// <summary>
        /// Called when the client has logged in.
        /// </summary>
        public ProxyNotification LoggedIn;

        /// <summary>
        /// Called when the client has logged out.
        /// </summary>
        public ProxyNotification LoggedOut;

        /// <summary>
        /// Called when a packet is recieved from the server.
        /// </summary>
        public PacketListener PacketFromServer;

        /// <summary>
        /// Called when a packet is recieved from the client.
        /// </summary>
        public PacketListener PacketFromClient;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new proxy and start listening for the client to connect.
        /// </summary>
        /// <param name="c"></param>
        public Proxy(Client c) : this(c, new LoginServer(string.Empty, 0)) { }

        /// <summary>
        /// Create a new proxy that connects to the specified server and the default port (7171).
        /// </summary>
        /// <param name="c"></param>
        /// <param name="serverIP"></param>
        public Proxy(Client c, string serverIP) : this(c, serverIP, DefaultPort) { }

        /// <summary>
        /// Create a new proxy that connects to the specified server and port.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        public Proxy(Client c, string serverIP, short serverPort) : this (c, new LoginServer(serverIP, serverPort)) { }

        /// <summary>
        /// Create a new proxy with the specified login server.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ls"></param>
        public Proxy(Client c, LoginServer ls)
        {
            client = c;
            if (!ls.Server.Equals(string.Empty))
            {
                loginServers = new LoginServer[] { ls };
            }
            client.SetServer(Localhost, DefaultPort);
            StartClientListener();
        }
        #endregion

        /// <summary>
        /// Restart the proxy.
        /// </summary>
        public void Restart()
        {
            StartClientListener();
        }

        private void StartClientListener()
        {
            tcpClient = new TcpListener(IPAddress.Any, DefaultPort);
            tcpClient.Start();
            tcpClient.BeginAcceptSocket((AsyncCallback)ClientConnected, null);
        }

        private void ClientConnected(IAsyncResult ar)
        {
            socketClient = tcpClient.EndAcceptSocket(ar);

            if (socketClient.Connected)
            {
                netStreamClient = new NetworkStream(socketClient);

                //Connect the proxy to the login server.
                tcpLogin = new TcpClient(loginServers[0].Server, loginServers[0].Port);
                netStreamLogin = tcpLogin.GetStream();

                //Listen for the client to request the character list
                netStreamClient.BeginRead(data, 0, data.Length, ClientLoginRecieved, null);
            }
        }

        private void ClientLoginRecieved(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);

            if (bytesRead > 0)
            {
                // Relay the login details to the Login Server
                netStreamLogin.BeginWrite(data, 0, bytesRead, null, null);

                // Begin read for the character list
                netStreamLogin.BeginRead(data, 0, data.Length, CharListRecieved, null);
            }
        }

        private void CharListRecieved(IAsyncResult ar)
        {
            int bytesRead = netStreamLogin.EndRead(ar);

            if (bytesRead > 0)
            {
                // Process the character list
                ProcessCharListPacket(data, bytesRead);

                // Send the modified char list to the client
                netStreamClient.BeginWrite(data, 0, bytesRead, null, null);

                // Refresh the client listener because the client reconnects on a
                // different port with the game server
                RefreshClientListener();
            }
        }

        private void RefreshClientListener()
        {
            // Refresh the client listener
            tcpClient.Stop();
            tcpClient.Start();
            tcpClient.BeginAcceptSocket((AsyncCallback)ClientReconnected, null);
        }

        private void ClientReconnected(IAsyncResult ar)
        {
            socketClient = tcpClient.EndAcceptSocket(ar);

            if (socketClient.Connected)
            {
                // The client has successfully reconnected
                netStreamClient = new NetworkStream(socketClient);

                // Begint to read the game login packet from the client
                netStreamClient.BeginRead(data, 0, data.Length, ClientGameLoginRecieved, null);
            }
        }

        private void ClientGameLoginRecieved(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);

            if (bytesRead > 0)
            {
                // Read the selection index from memory
                selectedChar = client.ReadByte(Addresses.Client.LoginSelectedChar);

                // Connect to the selected game world
                tcpServer = new TcpClient(charList.chars[selectedChar].worldIP, charList.chars[selectedChar].worldPort);
                netStreamServer = tcpServer.GetStream();

                // Begin to write the login data to the game server
                netStreamServer.BeginWrite(data, 0, bytesRead, null, null);

                // Start asynchronous reading
                netStreamServer.BeginRead(data, 0, data.Length, (AsyncCallback)ServerRecieve, null);
                netStreamClient.BeginRead(data, 0, data.Length, (AsyncCallback)ClientRecieve, null);

                // Notify that the client has logged in
                if (LoggedIn != null)
                    LoggedIn();
            }
        }

        private void ServerRecieve(IAsyncResult ar)
        {
            //try
            //{
                if (!netStreamServer.CanRead)
                    return;
                int bytesRead = netStreamServer.EndRead(ar);
                if (bytesRead > 0)
                {
                    // Get the packet length
                    long packetlength = BitConverter.ToInt16(data, 0);

                    // Only send it to the application if the length is correct
                    // if it isn't, that means it is one of the beginning map messages,
                    // which doesn't decrypt/encrypt correctly.
                    if (PacketFromServer != null && packetlength == bytesRead - 2)
                    {
                        byte[] packet = new byte[bytesRead];
                        Array.Copy(data, packet, bytesRead);
                        byte[] newPacket = EncryptPacket(PacketFromServer(DecryptPacket(packet)));
                        //byte[] newPacket = PacketFromServer(packet);
                        if (newPacket != null)
                        {
                            // Packet editing not supported yet, something goes wrong in 
                            // Encrypting or decrypting, usually get an error when saying "hi"
                            netStreamClient.BeginWrite(packet, 0, packet.Length, null, null);
                            //netStreamClient.BeginWrite(newPacket, 0, newPacket.Length, null, null);
                        }
                    }
                    else
                    {
                        netStreamClient.BeginWrite(data, 0, bytesRead, null, null);
                    }
                }
                netStreamServer.BeginRead(data, 0, data.Length, (AsyncCallback)ServerRecieve, null);
            //}
            //catch
            //{

            //}
        }

        private void ClientRecieve(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);

            if (GetPacketType(data) == 0x14)
            {
                Stop();
                Restart();
                // Notify that the client has logged out
                if (LoggedOut != null)
                    LoggedOut();
                return;
            }
            //try
            //{
                if (bytesRead > 0)
                {
                    if (PacketFromClient != null)
                    {
                        byte[] packet = new byte[bytesRead];
                        Array.Copy(data, packet, bytesRead);

                        byte[] newPacket = EncryptPacket(PacketFromClient(DecryptPacket(packet)));
                        //byte[] newPacket = PacketFromClient(packet);

                        if (newPacket != null)
                        {
                            netStreamServer.BeginWrite(packet, 0, packet.Length, null, null);
                            //netStreamServer.BeginWrite(newPacket, 0, newPacket.Length, null, null);
                        }
                    }
                    else
                    {
                        netStreamServer.BeginWrite(data, 0, bytesRead, null, null);
                    }
                }
                netStreamClient.BeginRead(data, 0, data.Length, (AsyncCallback)ClientRecieve, null);
            //}
            //catch
            //{

            //}
        }

        private void Stop()
        {
            netStreamClient.Close();
            netStreamServer.Close();
            netStreamLogin.Close();
            tcpClient.Stop();
            tcpServer.Close();
            tcpLogin.Close();
            socketClient.Close();
        }

        private void ProcessCharListPacket(byte[] data, int length)
        {
            byte[] packet = new byte[length];
            byte[] key = client.ReadBytes(Addresses.Client.XTeaKey, 16);

            Array.Copy(data, packet, length);
            packet = XTEA.Decrypt(packet, key);

            // Make sure this is a login packet, not invalid login
            if (packet[2] == 0x14)
            {
                // Initialize the character list
                charList = new CharListPacket();
                int index = 0;

                charList.type = packet[2];

                index = 3; // MOTD length
                charList.lenMotd = BitConverter.ToInt16(packet, index);
                index += 2; // MOTD text
                charList.motd = Encoding.ASCII.GetString(packet, index, charList.lenMotd);
                index += charList.lenMotd + 1; // Number of chars (add one for the mysterious 0x64 byte)
                charList.numChars = packet[index];
                charList.chars = new CharListChar[charList.numChars];
                index += 1; // Length of first character's name

                for (int i = 0; i < charList.numChars; i++)
                {
                    charList.chars[i].lenCharName = BitConverter.ToInt16(packet, index);
                    index += 2; // Character name text
                    charList.chars[i].charName = Encoding.ASCII.GetString(packet, index, charList.chars[i].lenCharName);
                    index += charList.chars[i].lenCharName; // Length of world name
                    charList.chars[i].lenWorldName = BitConverter.ToInt16(packet, index);
                    index += 2; // World name text
                    charList.chars[i].worldName = Encoding.ASCII.GetString(packet, index, charList.chars[i].lenWorldName);
                    index += charList.chars[i].lenWorldName; // World IP Address
                    charList.chars[i].worldIP = IPBytesToString(packet, index);
                    Array.Copy(LocalhostBytes, 0, packet, index, 4);
                    index += 4; // World Port
                    charList.chars[i].worldPort = BitConverter.ToInt16(packet, index);
                    Array.Copy(DefaultPortBytes, 0, packet, index, 2);
                    index += 2; // Premium days or next chars name length
                }

                charList.premiumDays = BitConverter.ToInt16(packet, index);
            }

            packet = XTEA.Encrypt(packet, key);
            Array.Copy(packet, data, length);
        }

        /// <summary>
        /// Sends a packet to the server
        /// </summary>
        /// <param name="packet"></param>
        public void SendToServer(byte[] packet)
        {
            byte[] encrypted = EncryptPacket(packet);
            netStreamServer.Write(encrypted, 0, encrypted.Length);
        }
        /// <summary>
        /// Sends a packet to the client
        /// </summary>
        /// <param name="packet"></param>
        public void SendToClient(byte[] packet)
        {
            byte[] encrypted = EncryptPacket(packet);
            netStreamClient.Write(encrypted, 0, encrypted.Length);
        }

        /// <summary>
        /// Wrapper for XTEA.Encrypt
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public byte[] EncryptPacket(byte[] packet)
        {
            return XTEA.Encrypt(packet, client.ReadBytes(Addresses.Client.XTeaKey, 16));
        }

        /// <summary>
        /// Wrapper for XTEA.Decrypt
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public byte[] DecryptPacket(byte[] packet)
        {
            return XTEA.Decrypt(packet, client.ReadBytes(Addresses.Client.XTeaKey, 16));
        }

        /// <summary>
        /// Get the type of an encrypted packet
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private byte GetPacketType(byte[] packet)
        {
            return XTEA.DecryptType(packet, client.ReadBytes(Addresses.Client.XTeaKey, 16));
        }

        /// <summary>
        /// Convert a 4 byte IP Address to a string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string IPBytesToString(byte[] data, int index)
        {
            return "" + data[index] + "." + data[index + 1] + "." + data[index + 2] + "." + data[index + 3];
        }
    }
    /// <summary>
    /// Stores the data from the server's character list packet.
    /// </summary>
    public struct CharListPacket
    {
        public byte type;
        public short lenMotd;
        public string motd;
        public byte numChars;
        public CharListChar[] chars;
        public short premiumDays;

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Type: " + type + "\n\r");
            s.Append("MOTD [" + lenMotd + "]: " + motd + "\n\r");
            s.Append("Characters: " + numChars + "\n\r");
            for(int i = 0; i < numChars; i++)
            {
                s.Append("Character " + i + "\n\r");
                s.Append("Name [" + chars[i].lenCharName + "]: " + chars[i].charName + "\n\r");
                s.Append("World Name [" + chars[i].lenWorldName + "]: " + chars[i].worldName + "\n\r");
                s.Append("World IP: " + chars[i].worldIP + "\n\r");
                s.Append("World Port: " + chars[i].worldPort + "\n\r");
            }
            return s.ToString();
        }
    }

    /// <summary>
    /// Represents one character in the server's character list packet.
    /// </summary>
    public struct CharListChar
    {
        public short lenCharName;
        public string charName;
        public short lenWorldName;
        public string worldName;
        public string worldIP;
        public short worldPort;
    }
}
