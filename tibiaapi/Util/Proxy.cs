using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Tibia.Objects;
using Tibia.Packets;

namespace Tibia.Util
{
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

        private Client         client;
        private CharListPacket charList;
        private byte           selectedChar;
        private bool           connected;
        private byte[]         dataServer = new byte[8192];
        private byte[]         dataClient = new byte[8192];

        private LoginServer[]  loginServers = new LoginServer[] {
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
        /// <param name="packet">The unencrypted packet that was received.</param>
        /// <returns>The unencrypted packet to be forwarded. If null, the packet will not be forwarded.</returns>
        public delegate Packet PacketListener(Packet packet);

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
        /// Called when a packet is received from the server.
        /// </summary>
        public PacketListener ReceivedPacketFromServer;

        /// <summary>
        /// Called when a packet is received from the client.
        /// </summary>
        public PacketListener ReceivedPacketFromClient;

        public PacketListener ReceivedAnimatedTextPacket;
        public PacketListener ReceivedChatMessagePacket;
        public PacketListener ReceivedStatusMessagePacket;
        public PacketListener ReceivedProjectilePacket;
        public PacketListener RecievedCreatureHealthPacket;
        public PacketListener RecievedVipLoginPacket;

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
            c.UsingProxy = true;
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
                netStreamClient.BeginRead(dataClient, 0, dataClient.Length, ClientLoginReceived, null);
            }
        }

        private void ClientLoginReceived(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);

            if (bytesRead > 0)
            {
                // Relay the login details to the Login Server
                netStreamLogin.BeginWrite(dataClient, 0, bytesRead, null, null);

                // Begin read for the character list
                netStreamLogin.BeginRead(dataServer, 0, dataServer.Length, CharListReceived, null);
            }
        }

        private void CharListReceived(IAsyncResult ar)
        {
            int bytesRead = netStreamLogin.EndRead(ar);

            if (bytesRead > 0)
            {
                // Process the character list
                ProcessCharListPacket(dataServer, bytesRead);

                // Send the modified char list to the client
                netStreamClient.BeginWrite(dataServer, 0, bytesRead, null, null);

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
                netStreamClient.BeginRead(dataClient, 0, dataClient.Length, ClientGameLoginReceived, null);
            }
        }

        private void ClientGameLoginReceived(IAsyncResult ar)
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
                netStreamServer.BeginWrite(dataClient, 0, bytesRead, null, null);

                // Start asynchronous reading
                netStreamServer.BeginRead(dataServer, 0, dataServer.Length, (AsyncCallback)ReceiveFromServer, null);
                netStreamClient.BeginRead(dataClient, 0, dataClient.Length, (AsyncCallback)ReceiveFromClient, null);

                // The proxy is now connected
                connected = true;

                // Notify that the client has logged in
                if (LoggedIn != null)
                    LoggedIn();
            }
        }

        private void ReceiveFromServer(IAsyncResult ar)
        {
            if (!netStreamServer.CanRead) return;
                
            int bytesRead = netStreamServer.EndRead(ar);
            int offset = 0;

            while (bytesRead - offset > 0)
            {
                // Get the packet length
                int packetlength = BitConverter.ToInt16(dataServer, offset) + 2;

                // Parse the data into a single packet
                byte[] packet = new byte[packetlength];
                Array.Copy(dataServer, offset, packet, 0, packetlength);
                packet = DecryptPacket(packet);

                // Always call the default (if attached to)
                if (ReceivedPacketFromServer != null)
                    ReceivedPacketFromServer(new Packet(packet));


                Packet packetobj = RaiseEvents(packet);
                if (packetobj != null)
                {
                    // Packet editing not supported yet, something goes wrong in 
                    // Encrypting or decrypting, usually get an error when saying "hi"
                    netStreamClient.BeginWrite(dataServer, offset, packetlength, null, null);
                    //SendToClient(packetobj.Data);
                }

                offset += packetlength;
            }

            netStreamServer.BeginRead(dataServer, 0, dataServer.Length, (AsyncCallback)ReceiveFromServer, null);
        }

        private void ReceiveFromClient(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);

            if (GetPacketType(dataClient) == (byte)PacketType.Logout)
            {
                Stop();
                Restart();

                // Notify that the client has logged out
                if (LoggedOut != null)
                {
                    LoggedOut();
                }
                    
                return;
            }

            if (bytesRead > 0)
            {
                // Parse the data into a single packet
                byte[] packet = new byte[bytesRead];
                Array.Copy(dataClient, packet, bytesRead);
                packet = DecryptPacket(packet);

                // Always call the default (if attached to)
                if (ReceivedPacketFromClient != null)
                    ReceivedPacketFromClient(new Packet(packet));

                Packet packetobj = RaiseEvents(packet);

                if (packetobj != null)
                {
                    netStreamServer.BeginWrite(dataClient, 0, bytesRead, null, null);
                    //SendToServer(packetobj.Data);
                }
                else
                {
                    netStreamServer.BeginWrite(dataClient, 0, bytesRead, null, null);
                }
            }
            netStreamClient.BeginRead(dataClient, 0, dataClient.Length, (AsyncCallback)ReceiveFromClient, null);
        }

        private void Stop()
        {
            connected = false;
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

            charList = new CharListPacket();
            charList.ParseData(packet, LocalhostBytes, DefaultPortBytes);

            packet = XTEA.Encrypt(charList.Data, key);
            Array.Copy(packet, data, length);
        }

        private Packet RaiseEvents(byte[] packet)
        {
            if (packet.Length < 3) return new Packet(packet);
            switch ((PacketType)packet[2])
            {
                case PacketType.AnimatedText:
                    if (ReceivedAnimatedTextPacket != null)
                        return ReceivedAnimatedTextPacket(new AnimatedTextPacket(packet));
                    break;
                case PacketType.ChatMessage:
                    if (ReceivedChatMessagePacket != null)
                        return ReceivedChatMessagePacket(new ChatMessagePacket(packet));
                    break;
                case PacketType.StatusMessage:
                    if (ReceivedStatusMessagePacket != null)
                        return ReceivedStatusMessagePacket(new StatusMessagePacket(packet));
                    break;
                case PacketType.Projectile:
                    if (ReceivedProjectilePacket != null)
                        return ReceivedProjectilePacket(new ProjectilePacket(packet));
                    break;
                case PacketType.CreatureHealth:
                    if (RecievedCreatureHealthPacket != null)
                        return RecievedCreatureHealthPacket(new CreatureHealthPacket(packet));
                    break;
                case PacketType.VipLogin:
                    if (RecievedVipLoginPacket != null)
                        return RecievedVipLoginPacket(new VipLoginPacket(packet));
                    break;
            }
            return new Packet(packet);
        }

        /// <summary>
        /// Encrypts and sends a packet to the server
        /// </summary>
        /// <param name="packet"></param>
        public void SendToServer(byte[] packet)
        {
            byte[] encrypted = EncryptPacket(packet);
            netStreamServer.BeginWrite(encrypted, 0, encrypted.Length, null, null);
        }
        /// <summary>
        /// Encrypts and sends a packet to the client
        /// </summary>
        /// <param name="packet"></param>
        public void SendToClient(byte[] packet)
        {
            byte[] encrypted = EncryptPacket(packet);
            netStreamClient.BeginWrite(encrypted, 0, encrypted.Length, null, null);
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
        /// Returns true if the proxy is connected
        /// </summary>
        public bool Connected
        {
            get { return connected; }
        }
    }
}
