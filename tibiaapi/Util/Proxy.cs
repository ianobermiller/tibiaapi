using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using Tibia.Packets;
using Tibia.Objects;
using System.Windows.Forms;
using System.Net;

namespace Tibia.Util
{
    public class Proxy : SocketBase
    {
        #region Vars
        static byte[] localHostBytes = new byte[] { 127, 0, 0, 1 };
        static Random randon = new Random();

        private Objects.Client client;

        private LoginServer[] loginServers;
        private uint selectedLoginServer = 0;
        public bool IsOtServer { get; set; }

        private TcpListener tcpServer;
        private Socket socketServer;
        private NetworkStream networkStreamServer;
        private byte[] bufferServer = new byte[2];
        private int readBytesServer;
        private int packetSizeServer;
        private bool writingServer;
        private Queue<NetworkMessage> serverSendQueue = new Queue<NetworkMessage> { };
        private Queue<NetworkMessage> serverReceiveQueue = new Queue<NetworkMessage> { };
        private ushort portServer = 0;
        private bool isFirstMsg;

        private TcpClient tcpClient;
        private NetworkStream networkStreamClient;
        private byte[] bufferClient = new byte[2];
        private int readBytesClient;
        private int packetSizeClient;
        private bool writingClient;
        private DateTime lastClientWrite = DateTime.UtcNow;
        private Queue<NetworkMessage> clientSendQueue = new Queue<NetworkMessage> { };
        private Queue<NetworkMessage> clientReceiveQueue = new Queue<NetworkMessage> { };

        private bool acceptingConnection;
        private CharList[] charList;
        private uint[] xteaKey;

        private Objects.Player player;
        private bool isConnected;

        #endregion

        #region Properties
        public Objects.Client Client
        {
            get { return client; }
        }

        public bool Connected
        {
            get { return isConnected; }
        }

        public ushort Port
        {
            get { return portServer; }
            set { portServer = value; }
        }

        public uint[] XteaKey
        {
            get { return xteaKey; }
        }
        #endregion

        #region Events

        public event Action PlayerLogin;
        public event Action PlayerLogout;
        public event Action ClientConnect;

        public delegate void MessageListener(NetworkMessage message);
        public event MessageListener ReceivedMessageFromClient;
        public event MessageListener ReceivedMessageFromServer;

        public delegate void SplitPacket(byte type, byte[] packet);

        public event SplitPacket IncomingSplitPacket;
        public event SplitPacket OutgoingSplitPacket;

        #endregion

        #region Constructor/Deconstructor

        public Proxy(Client c) : this(c, false) { }

        public Proxy(Client c, bool debug)
        {
            client = c;

            loginServers = client.LoginServers;

            if (portServer == 0)
                portServer = GetFreePort();

            client.SetServer("localhost", (short)portServer);

            if (client.RSA == Constants.RSAKey.OpenTibia)
                IsOtServer = true;
            else
            {
                client.RSA = Constants.RSAKey.OpenTibia;
                IsOtServer = false;
            }

            if (client.CharListCount != 0)
            {
                charList = client.CharList;
                client.SetCharListServer(localHostBytes, portServer);
            }

            //events
            ReceivedSelfAppearIncomingPacket += new IncomingPacketListener(Proxy_ReceivedSelfAppearIncomingPacket);

            client.UsingProxy = true;
            DebugOn = debug;
            Start();
        }

        private bool Proxy_ReceivedSelfAppearIncomingPacket(IncomingPacket packet)
        {
            if (PlayerLogin != null)
                Scheduler.addTask(PlayerLogin, null, 500);

            isConnected = true;
            return true;
        }

        ~Proxy()
        {
            if (!client.Process.HasExited)
            {
                client.LoginServers = loginServers;

                if (!IsOtServer)
                    client.RSA = Constants.RSAKey.RealTibia;

                if (client.CharListCount != 0 && client.CharListCount == charList.Length)
                {
                    client.SetCharListServer(charList);
                }
            }

            client.UsingProxy = false;
        }

        #endregion

        #region Control
        public void Start()
        {
            if (DebugOn)
                WriteDebug("Start Function");

            if (acceptingConnection)
                return;

            acceptingConnection = true;

            serverReceiveQueue.Clear();
            serverSendQueue.Clear();
            clientReceiveQueue.Clear();
            clientSendQueue.Clear();

            tcpServer = new TcpListener(System.Net.IPAddress.Any, portServer);
            tcpServer.Start();
            tcpServer.BeginAcceptSocket((AsyncCallback)SocketAcepted, null);
        }

        private void Close()
        {

            if (DebugOn)
                WriteDebug("Close Function.");

            if (tcpClient != null)
                tcpClient.Close();

            if (tcpServer != null)
                tcpServer.Stop();

            if (socketServer != null)
                socketServer.Close();

            acceptingConnection = false;
        }

        private void Restart()
        {
            if (DebugOn)
                WriteDebug("Restart Function.");

            lock ("acceptingConnection")
            {
                if (acceptingConnection)
                    return;

                if (isConnected)
                {
                    player = null;

                    if (PlayerLogout != null)
                        PlayerLogout.BeginInvoke(null, null);
                }

                isConnected = false;

                Close();
                Start();
            }
        }

        public void SendToClient(NetworkMessage msg)
        {
            if (!isConnected)
                throw new Tibia.Exceptions.ProxyDisconnectedException();

            serverSendQueue.Enqueue(msg);
            ProcessServerSendQueue();
        }

        public void SendToServer(NetworkMessage msg)
        {
            if (!isConnected)
                throw new Tibia.Exceptions.ProxyDisconnectedException();

            clientSendQueue.Enqueue(msg);
            ProcessClientSendQueue();
        }
        #endregion

        #region Server
        private void SocketAcepted(IAsyncResult ar)
        {
            if (DebugOn)
                WriteDebug("OnSocketAcepted Function.");

            socketServer = tcpServer.EndAcceptSocket(ar);

            if (socketServer.Connected)
                networkStreamServer = new NetworkStream(socketServer);

            if (ClientConnect != null)
                ClientConnect.BeginInvoke(null, null);

            acceptingConnection = false;

            isFirstMsg = true;
            networkStreamServer.BeginRead(bufferServer, 0, 2, (AsyncCallback)ServerReadPacket, null);
        }

        private void ServerReadPacket(IAsyncResult ar)
        {
            if (acceptingConnection)
                return;

            try
            {
                readBytesServer = networkStreamServer.EndRead(ar);
            }
            catch (Exception)
            {
                return;
            }

            if (readBytesServer == 0)
            {
                Restart();
                return;
            }

            packetSizeServer = (int)BitConverter.ToUInt16(bufferServer, 0) + 2;
            NetworkMessage msg = new NetworkMessage(Client, packetSizeServer);
            Array.Copy(bufferServer, msg.GetBuffer(), 2);

            while (readBytesServer < packetSizeServer)
            {
                if (networkStreamServer.CanRead)
                    readBytesServer += networkStreamServer.Read(msg.GetBuffer(), readBytesServer, packetSizeServer - readBytesServer);
                else
                {
                    Restart();
                    return;
                }
            }

            if (ReceivedMessageFromServer != null)
                ReceivedMessageFromServer.BeginInvoke(new NetworkMessage(Client, msg.Packet), null, null);

            if (isFirstMsg)
            {
                isFirstMsg = false;
                ServerParseFirstMsg(msg);
            }
            else
            {
                serverReceiveQueue.Enqueue(msg);
                ProcessServerReceiveQueue();

                if (networkStreamServer.CanRead)
                    networkStreamServer.BeginRead(bufferServer, 0, 2, (AsyncCallback)ServerReadPacket, null);
                else
                    Restart();
            }
        }

        private void ServerParseFirstMsg(NetworkMessage msg)
        {
            if (DebugOn)
                WriteDebug("ServerParseFirstMsg Function.");

            msg.Position = 6;

            byte protocolId = msg.GetByte();
            uint[] key = new uint[4];

            int pos;

            switch (protocolId)
            {
                case 0x01: //login server

                    ushort osVersion = msg.GetUInt16();
                    ushort clientVersion = msg.GetUInt16();

                    msg.GetUInt32();
                    msg.GetUInt32();
                    msg.GetUInt32();

                    pos = msg.Position;

                    msg.RsaOTDecrypt();

                    if (msg.GetByte() != 0)
                    {
                        Restart();
                        return;
                    }

                    key[0] = msg.GetUInt32();
                    key[1] = msg.GetUInt32();
                    key[2] = msg.GetUInt32();
                    key[3] = msg.GetUInt32();

                    xteaKey = key;

                    if (clientVersion != Version.CurrentVersion)
                    {
                        DisconnectClient(0x0A, "This proxy requires client 8.40");
                        return;
                    }

                    try
                    {
                        tcpClient = new TcpClient(loginServers[selectedLoginServer].Server, loginServers[selectedLoginServer].Port);
                        networkStreamClient = tcpClient.GetStream();
                    }
                    catch (Exception)
                    {
                        DisconnectClient(0x0A, "Connection time out.");
                        return;
                    }


                    if (IsOtServer)
                        msg.RsaOTEncrypt(pos);
                    else
                        msg.RsaCipEncrypt(pos);

                    msg.InsertAdler32();
                    msg.InsertPacketHeader();

                    networkStreamClient.BeginWrite(msg.Packet, 0, msg.Length, null, null);
                    networkStreamClient.BeginRead(bufferClient, 0, 2, (AsyncCallback)CharListReceived, null);

                    break;

                case 0x0A: // world server

                    msg.GetUInt16(); //os
                    msg.GetUInt16(); //version

                    pos = msg.Position;

                    msg.RsaOTDecrypt();
                    msg.GetByte();

                    key[0] = msg.GetUInt32();
                    key[1] = msg.GetUInt32();
                    key[2] = msg.GetUInt32();
                    key[3] = msg.GetUInt32();

                    xteaKey = key;

                    msg.GetByte();
                    msg.GetString();
                    string name = msg.GetString();

                    int selectedChar = GetSelectedChar(name);

                    if (selectedChar >= 0)
                    {
                        try
                        {
                            tcpClient = new TcpClient(BitConverter.GetBytes(charList[selectedChar].WorldIP).ToIPString(), charList[selectedChar].WorldPort);
                            networkStreamClient = tcpClient.GetStream();
                        }
                        catch (Exception)
                        {
                            DisconnectClient(0x14, "Connection timeout.");
                            return;
                        }

                        if (IsOtServer)
                            msg.RsaOTEncrypt(pos);
                        else
                            msg.RsaCipEncrypt(pos);

                        msg.InsertAdler32();
                        msg.InsertPacketHeader();

                        networkStreamClient.Write(msg.Packet, 0, msg.Length);

                        networkStreamClient.BeginRead(bufferClient, 0, 2, (AsyncCallback)ClientReadPacket, null);
                        networkStreamServer.BeginRead(bufferServer, 0, 2, (AsyncCallback)ServerReadPacket, null);

                        return;

                    }
                    else
                    {
                        DisconnectClient(0x14, "Unknown character, please relogin.");
                        return;
                    }

                default:
                    Restart();
                    return;
            }
        }

        private void CharListReceived(IAsyncResult ar)
        {
            if (DebugOn)
                WriteDebug("OnCharListReceived Function.");

            readBytesClient = networkStreamClient.EndRead(ar);

            if (readBytesClient == 2)
            {
                packetSizeClient = (int)BitConverter.ToUInt16(bufferClient, 0) + 2;
                NetworkMessage msg = new NetworkMessage(Client, packetSizeClient);
                Array.Copy(bufferClient, msg.GetBuffer(), 2);

                while (readBytesClient < packetSizeClient)
                {
                    if (networkStreamClient.CanRead)
                        readBytesClient += networkStreamClient.Read(msg.GetBuffer(), readBytesClient, packetSizeClient - readBytesClient);
                    else
                        Restart();
                }

                if (ReceivedMessageFromClient != null)
                    ReceivedMessageFromClient.BeginInvoke(new NetworkMessage(Client, msg.Packet), null, null);

                msg.PrepareToRead();
                msg.GetUInt16(); //packet size..

                while (msg.Position < msg.Length)
                {
                    byte cmd = msg.GetByte();

                    switch (cmd)
                    {
                        case 0x0A: //Error message
                            msg.GetString();
                            break;
                        case 0x0B: //For your information
                            msg.GetString();
                            break;
                        case 0x14: //MOTD
                            msg.GetString();
                            break;
                        case 0x1E: //Patching exe/dat/spr messages
                        case 0x1F:
                        case 0x20:
                            DisconnectClient(0x0A, "A new client is avalible, please download it first!");
                            return;
                        case 0x28: //Select other login server
                            selectedLoginServer = (uint)randon.Next(0, loginServers.Length - 1);
                            break;
                        case 0x64: //character list
                            int nChar = (int)msg.GetByte();
                            charList = new CharList[nChar];

                            for (int i = 0; i < nChar; i++)
                            {
                                charList[i].CharName = msg.GetString();
                                charList[i].WorldName = msg.GetString();
                                charList[i].WorldIP = msg.PeekUInt32();
                                msg.AddBytes(localHostBytes);
                                charList[i].WorldPort = msg.PeekUInt16();
                                msg.AddUInt16(portServer);
                            }

                            //ushort premmy = msg.GetUInt16();
                            //send this data to client

                            msg.PrepareToSend();

                            if (networkStreamServer.CanWrite)
                                networkStreamServer.Write(msg.Packet, 0, msg.Length);

                            Restart();
                            return;
                        default:
                            break;
                    }
                }

                msg.PrepareToSend();
                networkStreamServer.Write(msg.Packet, 0, msg.Length);

                Restart();
                return;

            }
            else
                Restart();

        }

        private void DisconnectClient(byte cmd, string message)
        {
            if (DebugOn)
                WriteDebug("DisconnectClient Function.");

            NetworkMessage msg = new NetworkMessage(Client);
            msg.AddByte(cmd);
            msg.AddString(message);

            msg.InsetLogicalPacketHeader();
            msg.PrepareToSend();

            networkStreamServer.Write(msg.Packet, 0, msg.Length);

            Restart();
        }

        private void ProcessServerReceiveQueue()
        {
            while (serverReceiveQueue.Count > 0)
            {
                NetworkMessage msg = serverReceiveQueue.Dequeue();
                NetworkMessage output = new NetworkMessage(Client);
                bool haveContent = false;

                msg.PrepareToRead();
                msg.GetUInt16(); //logical packet size

                Objects.Location pos = /*GetPlayerPosition()*/ Location.Invalid;

                while (msg.Position < msg.Length)
                {
                    OutgoingPacket packet = ParseServerPacket(client, msg, pos);
                    byte[] packetBytes;

                    if (packet == null)
                    {
                        if (DebugOn)
                            WriteDebug("Unknown outgoing packet.. skipping the rest! type: " + msg.PeekByte().ToString("X"));

                        packetBytes = msg.GetBytes(msg.Length - msg.Position);

                        if (packetBytes.Length > 0)
                        {
                            if (OutgoingSplitPacket != null)
                                OutgoingSplitPacket.BeginInvoke(packetBytes[0], packetBytes, null, null);

                            //skip the rest...
                            haveContent = true;
                            output.AddBytes(packetBytes);
                        }

                        break;
                    }
                    else
                    {

                        packetBytes = packet.ToByteArray();

                        if (OutgoingSplitPacket != null)
                            OutgoingSplitPacket.BeginInvoke((byte)packet.Type, packetBytes, null, null);

                        if (packet.Forward)
                        {
                            haveContent = true;
                            output.AddBytes(packetBytes);
                        }
                    }

                }

                if (haveContent)
                {
                    output.InsetLogicalPacketHeader();
                    output.PrepareToSend();
                    clientSendQueue.Enqueue(output);
                    ProcessClientSendQueue();
                }
            }
        }

        private void ProcessServerSendQueue()
        {

            if (writingServer)
                return;

            if (serverSendQueue.Count > 0)
            {
                NetworkMessage msg = serverSendQueue.Dequeue();
                ServerWrite(msg.Packet);
            }
        }

        private void ServerWrite(byte[] buffer)
        {

            if (!writingServer)
            {
                writingServer = true;

                if (networkStreamServer.CanWrite)
                    networkStreamServer.BeginWrite(buffer, 0, buffer.Length, (AsyncCallback)ServerWriteDone, null);
                else
                {
                    //TODO: Handle the error.
                }
            }
        }

        private void ServerWriteDone(IAsyncResult ar)
        {
            networkStreamServer.EndWrite(ar);
            writingServer = false;

            if (serverSendQueue.Count > 0)
                ProcessServerSendQueue();
        }

        #endregion

        #region Client

        private void ClientReadPacket(IAsyncResult ar)
        {
            if (acceptingConnection)
                return;

            //sometimes when close the client without logout this may trigger an exception
            try
            {
                readBytesClient = networkStreamClient.EndRead(ar);
            }
            catch (Exception)
            {
                return;
            }

            if (readBytesClient == 0)
            {
                Restart();
                return;
            }

            packetSizeClient = (int)BitConverter.ToUInt16(bufferClient, 0) + 2;
            NetworkMessage msg = new NetworkMessage(client, packetSizeClient);
            Array.Copy(bufferClient, msg.GetBuffer(), 2);

            while (readBytesClient < packetSizeClient)
            {
                if (networkStreamClient.CanRead)
                    readBytesClient += networkStreamClient.Read(msg.GetBuffer(), readBytesClient, packetSizeClient - readBytesClient);
                else
                    Restart();
            }

            if (ReceivedMessageFromClient != null)
                ReceivedMessageFromClient.BeginInvoke(new NetworkMessage(client, msg.Packet), null, null);

            clientReceiveQueue.Enqueue(msg);
            ProcessClientReceiveQueue();

            if (networkStreamClient.CanRead)
                networkStreamClient.BeginRead(bufferClient, 0, 2, (AsyncCallback)ClientReadPacket, null);
            else
                Restart();

        }

        private void ProcessClientReceiveQueue()
        {
            while (clientReceiveQueue.Count > 0)
            {
                NetworkMessage msg = clientReceiveQueue.Dequeue();
                NetworkMessage output = new NetworkMessage(client);
                bool haveContent = false;

                msg.PrepareToRead();
                msg.GetUInt16(); //logical packet size

                Objects.Location pos = GetPlayerPosition();

                while (msg.Position < msg.Length)
                {
                    IncomingPacket packet = ParseClientPacket(client, msg, ref pos);
                    byte[] packetBytes;

                    if (packet == null)
                    {
                        if (DebugOn)
                            WriteDebug("Unknown incoming packet.. skiping the rest! type: " + msg.PeekByte().ToString("X"));

                        packetBytes = msg.GetBytes(msg.Length - msg.Position);

                        if (packetBytes.Length > 0)
                        {
                            if (IncomingSplitPacket != null)
                                IncomingSplitPacket.BeginInvoke(packetBytes[0], packetBytes, null, null);

                            //skip the rest...
                            haveContent = true;
                            output.AddBytes(packetBytes);
                        }

                        break;
                    }
                    else
                    {
                        packetBytes = packet.ToByteArray();

                        if (IncomingSplitPacket != null)
                            IncomingSplitPacket.BeginInvoke((byte)packet.Type, packetBytes, null, null);

                        if (packet.Forward)
                        {
                            haveContent = true;
                            output.AddBytes(packetBytes);
                        }
                    }

                }

                if (haveContent)
                {
                    output.InsetLogicalPacketHeader();
                    output.PrepareToSend();
                    serverSendQueue.Enqueue(output);
                    ProcessServerSendQueue();
                }
            }
        }

        private void ProcessClientSendQueue()
        {
            if (writingClient)
                return;

            if (clientSendQueue.Count > 0)
            {
                NetworkMessage msg = clientSendQueue.Dequeue();

                if (msg != null)
                    ClientWrite(msg.Packet);
            }
        }

        private void ClientWrite(byte[] buffer)
        {
            if (!writingClient)
            {
                writingClient = true;

                if (lastClientWrite.AddMilliseconds(125) > DateTime.UtcNow)
                    System.Threading.Thread.Sleep(125);

                if (networkStreamClient.CanWrite)
                    networkStreamClient.BeginWrite(buffer, 0, buffer.Length, (AsyncCallback)ClientWriteDone, null);
            }
        }

        private void ClientWriteDone(IAsyncResult ar)
        {
            networkStreamClient.EndWrite(ar);
            writingClient = false;

            if (clientSendQueue.Count > 0)
                ProcessClientSendQueue();
        }

        #endregion

        #region Other Functions
        private int GetSelectedChar(string name)
        {
            for (int i = 0; i < charList.Length; i++)
            {
                if (charList[i].CharName == name)
                    return i;
            }

            return -1;
        }

        public Objects.Player GetPlayer()
        {
            try
            {
                if (player == null)
                    player = client.GetPlayer();
            }
            catch (Exception) { }

            return player;
        }

        public Objects.Location GetPlayerPosition()
        {
            Location pos = Location.Invalid;

            try
            {
                pos = GetPlayer().Location;
            }
            catch (Exception) { }

            return pos;
        }

        #endregion
    }
}