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

        Form debugForm;
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

        public uint[] XteaKey
        {
            get { return xteaKey; }
        }

        public ushort Port
        {
            get { return portServer; }
            set { portServer = value; }
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

            Start();

            if (debug)
            {
                DebugOn = true;
                debugForm = new Form();
                RichTextBox myRichTextBox = new RichTextBox();
                myRichTextBox.Name = "richTextBox";
                myRichTextBox.Dock = DockStyle.Fill;
                debugForm.Controls.Add(myRichTextBox);
                debugForm.Disposed += new EventHandler(debugFrom_Disposed);
                PrintDebug += Proxy_PrintDebug;
                debugForm.Show();
            }
        }

        private bool Proxy_ReceivedSelfAppearIncomingPacket(IncomingPacket packet)
        {
            if (PlayerLogin != null)
                PlayerLogin.BeginInvoke(null, null);

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
                        DisconnectClient(0x14, "Unknow character, please relogin..");
                        return;
                    }

                default:
                    {
                        Restart();
                        return;
                    }
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
                            DisconnectClient(0x0A, "A new client are avalible, please download it first!");
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

                Objects.Location pos = /*GetPlayerPosition()*/ Location.GetInvalid();

                while (msg.Position < msg.Length)
                {
                    OutgoingPacket packet = ParseServerPacket(client, msg, pos);
                    byte[] packetBytes;

                    if (packet == null)
                    {
                        if (DebugOn)
                            WriteDebug("Unknown outgoing packet.. skipping the rest! type: " + msg.PeekByte());

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
                            WriteDebug("Unknow incoming packet.. skping the rest! type: " + msg.PeekByte());

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

        #region Debug
        void Proxy_PrintDebug(string message)
        {
            if (debugForm.Disposing)
                return;

            if (debugForm.InvokeRequired)
            {
                debugForm.Invoke(new Action<string>(Proxy_PrintDebug), new object[] { message });
                return;
            }

            RichTextBox myRichTextBox = (RichTextBox)debugForm.Controls["richTextBox"];
            myRichTextBox.AppendText(message + "\n");
            myRichTextBox.Select(myRichTextBox.TextLength - 1, 0);
            myRichTextBox.ScrollToCaret();
        }

        void debugFrom_Disposed(object sender, EventArgs e)
        {
            PrintDebug -= Proxy_PrintDebug;
            DebugOn = false;
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
            Location pos = Location.GetInvalid();

            try
            {
                pos = GetPlayer().Location;
            }
            catch (Exception) { }

            return pos;
        }

        #endregion

        #region Port Checking
        /// <summary>
        /// Check if a port is open on localhost
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool CheckPort(ushort port)
        {
            try
            {
                TcpListener tcpScan = new TcpListener(IPAddress.Any, port);
                tcpScan.Start();
                tcpScan.Stop();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the first free port on localhost starting at the default 7171
        /// </summary>
        /// <returns></returns>
        public static ushort GetFreePort()
        {
            return GetFreePort(7172);
        }

        /// <summary>
        /// Get the first free port on localhost beginning at start
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ushort GetFreePort(ushort start)
        {
            while (!CheckPort(start))
            {
                start++;
            }

            return start;
        }
        #endregion
    }

    public abstract class SocketBase
    {
        public event Action<string> PrintDebug;


        public delegate bool IncomingPacketListener(Packets.IncomingPacket packet);
        public delegate bool OutgoingPacketListener(Packets.OutgoingPacket packet);


        //incoming
        public event IncomingPacketListener ReceivedAnimatedTextIncomingPacket;
        public event IncomingPacketListener ReceivedCancelTargetIncomingPacket;
        public event IncomingPacketListener ReceivedCanReportBugsIncomingPacket;
        public event IncomingPacketListener ReceivedChannelClosePrivateIncomingPacket;
        public event IncomingPacketListener ReceivedChannelListIncomingPacket;
        public event IncomingPacketListener ReceivedChannelOpenIncomingPacket;
        public event IncomingPacketListener ReceivedChannelOpenPrivateIncomingPacket;
        public event IncomingPacketListener ReceivedContainerAddItemIncomingPacket;
        public event IncomingPacketListener ReceivedContainerCloseIncomingPacket;
        public event IncomingPacketListener ReceivedContainerOpenIncomingPacket;
        public event IncomingPacketListener ReceivedContainerRemoveItemIncomingPacket;
        public event IncomingPacketListener ReceivedContainerUpdateItemIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureHealthIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureLightIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureMoveIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSkullIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeakIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeedIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSquareIncomingPacket;
        public event IncomingPacketListener ReceivedDeathIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeDownIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeUpIncomingPacket;
        public event IncomingPacketListener ReceivedFyiMessageIncomingPacket;
        public event IncomingPacketListener ReceivedInventoryResetSlotIncomingPacket;
        public event IncomingPacketListener ReceivedInventorySetSlotIncomingPacket;
        public event IncomingPacketListener ReceivedItemTextWindowIncomingPacket;
        public event IncomingPacketListener ReceivedMagicEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMapDescriptionIncomingPacket;
        public event IncomingPacketListener ReceivedMoveEastIncomingPacket;
        public event IncomingPacketListener ReceivedMoveNorthIncomingPacket;
        public event IncomingPacketListener ReceivedMoveSouthIncomingPacket;
        public event IncomingPacketListener ReceivedMoveWestIncomingPacket;
        public event IncomingPacketListener ReceivedOutfitWindowIncomingPacket;
        public event IncomingPacketListener ReceivedPingIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerFlagsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerSkillsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerStatusIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerWalkCancelIncomingPacket;
        public event IncomingPacketListener ReceivedPrivateChannelCreateIncomingPacket;
        public event IncomingPacketListener ReceivedProjectileIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationCancelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationLockIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationOpenIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationRemoveIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeCloseIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeRequestAckIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeRequestNoAckIncomingPacket;
        public event IncomingPacketListener ReceivedSelfAppearIncomingPacket;
        public event IncomingPacketListener ReceivedShopSaleGoldCountIncomingPacket;
        public event IncomingPacketListener ReceivedShopWindowCloseIncomingPacket;
        public event IncomingPacketListener ReceivedShopWindowOpenIncomingPacket;
        public event IncomingPacketListener ReceivedTextMessageIncomingPacket;
        public event IncomingPacketListener ReceivedTileAddThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileRemoveThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileTransformThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileUpdateIncomingPacket;
        public event IncomingPacketListener ReceivedVipLoginIncomingPacket;
        public event IncomingPacketListener ReceivedVipLogoutIncomingPacket;
        public event IncomingPacketListener ReceivedVipStateIncomingPacket;
        public event IncomingPacketListener ReceivedWaitingListIncomingPacket;
        public event IncomingPacketListener ReceivedWorldLightIncomingPacket;

        //outgoing
        public event OutgoingPacketListener ReceivedChannelCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelOpenOutgoingPacket;
        public event OutgoingPacketListener ReceivedPlayerSpeechOutgoingPacket;
        public event OutgoingPacketListener ReceivedAttackOutgoingPacket;
        public event OutgoingPacketListener ReceivedFollowOutgoingPacket;
        public event OutgoingPacketListener ReceivedLookAtOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOnOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseBattlelistOutgoingPacket;
        public event OutgoingPacketListener ReceivedCancelMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedBattleWindowOutgoingPacket;
        public event OutgoingPacketListener ReceivedLogoutOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerOpenParentOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopBuyOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopSellOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnOutgoingPacket;
        public event OutgoingPacketListener ReceivedMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedAutoWalkOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipAddOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipRemoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemRotateOutgoingPacket;


        #region ClientPacket
        protected IncomingPacket ParseClientPacket(Client client, NetworkMessage msg, ref Objects.Location pos)
        {
            IncomingPacket packet;
            IncomingPacketType type = (IncomingPacketType)msg.PeekByte();

            switch (type)
            {
                case IncomingPacketType.AnimatedText:
                    if (DebugOn)
                        WriteDebug("AnimatedText");
                    packet = new Packets.Incoming.AnimatedTextPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedAnimatedTextIncomingPacket != null)
                            packet.Forward = ReceivedAnimatedTextIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerClose:
                    if (DebugOn)
                        WriteDebug("ContainerClose");
                    packet = new Packets.Incoming.ContainerClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerCloseIncomingPacket != null)
                            packet.Forward = ReceivedContainerCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSpeak:
                    if (DebugOn)
                        WriteDebug("CreatureSpeak");
                    packet = new Packets.Incoming.CreatureSpeakPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSpeakIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeakIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelOpen:
                    if (DebugOn)
                        WriteDebug("ChannelOpen");
                    packet = new Packets.Incoming.ChannelOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelOpenIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerWalkCancel:
                    if (DebugOn)
                        WriteDebug("PlayerWalkCancel");
                    packet = new Packets.Incoming.PlayerWalkCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerWalkCancelIncomingPacket != null)
                            packet.Forward = ReceivedPlayerWalkCancelIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelList:
                    if (DebugOn)
                        WriteDebug("ChannelList");
                    packet = new Packets.Incoming.ChannelListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelListIncomingPacket != null)
                            packet.Forward = ReceivedChannelListIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureMove:
                    if (DebugOn)
                        WriteDebug("CreatureMove");
                    packet = new Packets.Incoming.CreatureMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureMoveIncomingPacket != null)
                            packet.Forward = ReceivedCreatureMoveIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TextMessage:
                    if (DebugOn)
                        WriteDebug("TextMessage");
                    packet = new Packets.Incoming.TextMessagePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTextMessageIncomingPacket != null)
                            packet.Forward = ReceivedTextMessageIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileAddThing:
                    if (DebugOn)
                        WriteDebug("TileAddThing");
                    packet = new Packets.Incoming.TileAddThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileAddThingIncomingPacket != null)
                            packet.Forward = ReceivedTileAddThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureOutfit:
                    if (DebugOn)
                        WriteDebug("CreatureOutfit");
                    packet = new Packets.Incoming.CreatureOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureOutfitIncomingPacket != null)
                            packet.Forward = ReceivedCreatureOutfitIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureLight:
                    if (DebugOn)
                        WriteDebug("CreatureLight");
                    packet = new Packets.Incoming.CreatureLightPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureLightIncomingPacket != null)
                            packet.Forward = ReceivedCreatureLightIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureHealth:
                    if (DebugOn)
                        WriteDebug("CreatureHealth");
                    packet = new Packets.Incoming.CreatureHealthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureHealthIncomingPacket != null)
                            packet.Forward = ReceivedCreatureHealthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSpeed:
                    if (DebugOn)
                        WriteDebug("CreatureSpeed");
                    packet = new Packets.Incoming.CreatureSpeedPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSpeedIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeedIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSquare:
                    if (DebugOn)
                        WriteDebug("CreatureSquare");
                    packet = new Packets.Incoming.CreatureSquarePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSquareIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSquareIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileTransformThing:
                    if (DebugOn)
                        WriteDebug("TileTransformThing");
                    packet = new Packets.Incoming.TileTransformThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileTransformThingIncomingPacket != null)
                            packet.Forward = ReceivedTileTransformThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileRemoveThing:
                    if (DebugOn)
                        WriteDebug("TileRemoveThing");
                    packet = new Packets.Incoming.TileRemoveThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileRemoveThingIncomingPacket != null)
                            packet.Forward = ReceivedTileRemoveThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerAddItem:
                    if (DebugOn)
                        WriteDebug("ContainerAddItem");
                    packet = new Packets.Incoming.ContainerAddItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerAddItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerAddItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerRemoveItem:
                    if (DebugOn)
                        WriteDebug("ContainerRemoveItem");
                    packet = new Packets.Incoming.ContainerRemoveItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerRemoveItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerRemoveItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerUpdateItem:
                    if (DebugOn)
                        WriteDebug("ContainerUpdateItem");
                    packet = new Packets.Incoming.ContainerUpdateItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerUpdateItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerUpdateItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerOpen:
                    if (DebugOn)
                        WriteDebug("ContainerOpen");
                    packet = new Packets.Incoming.ContainerOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerOpenIncomingPacket != null)
                            packet.Forward = ReceivedContainerOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ItemTextWindow:
                    if (DebugOn)
                        WriteDebug("ItemTextWindow");
                    packet = new Packets.Incoming.ItemTextWindowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedItemTextWindowIncomingPacket != null)
                            packet.Forward = ReceivedItemTextWindowIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.WorldLight:
                    if (DebugOn)
                        WriteDebug("WorldLight");
                    packet = new Packets.Incoming.WorldLightPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedWorldLightIncomingPacket != null)
                            packet.Forward = ReceivedWorldLightIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Projectile:
                    if (DebugOn)
                        WriteDebug("Projectile");
                    packet = new Packets.Incoming.ProjectilePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedProjectileIncomingPacket != null)
                            packet.Forward = ReceivedProjectileIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MapDescription:
                    if (DebugOn)
                        WriteDebug("MapDescription");
                    packet = new Packets.Incoming.MapDescriptionPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMapDescriptionIncomingPacket != null)
                            packet.Forward = ReceivedMapDescriptionIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveNorth:
                    if (DebugOn)
                        WriteDebug("MoveNorth");
                    packet = new Packets.Incoming.MoveNorthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveNorthIncomingPacket != null)
                            packet.Forward = ReceivedMoveNorthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveSouth:
                    if (DebugOn)
                        WriteDebug("MoveSouth");
                    packet = new Packets.Incoming.MoveSouthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveSouthIncomingPacket != null)
                            packet.Forward = ReceivedMoveSouthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveEast:
                    if (DebugOn)
                        WriteDebug("MoveEast");
                    packet = new Packets.Incoming.MoveEastPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveEastIncomingPacket != null)
                            packet.Forward = ReceivedMoveEastIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveWest:
                    if (DebugOn)
                        WriteDebug("MoveWest");
                    packet = new Packets.Incoming.MoveWestPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveWestIncomingPacket != null)
                            packet.Forward = ReceivedMoveWestIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SelfAppear:
                    if (DebugOn)
                        WriteDebug("SelfAppear");
                    packet = new Packets.Incoming.SelfAppearPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSelfAppearIncomingPacket != null)
                            packet.Forward = ReceivedSelfAppearIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MagicEffect:
                    if (DebugOn)
                        WriteDebug("MagicEffect");
                    packet = new Packets.Incoming.MagicEffectPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMagicEffectIncomingPacket != null)
                            packet.Forward = ReceivedMagicEffectIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FloorChangeDown:
                    if (DebugOn)
                        WriteDebug("FloorChangeDown");
                    packet = new Packets.Incoming.FloorChangeDownPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFloorChangeDownIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeDownIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FloorChangeUp:
                    if (DebugOn)
                        WriteDebug("FloorChangeUp");
                    packet = new Packets.Incoming.FloorChangeUpPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFloorChangeUpIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeUpIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerStatus:
                    if (DebugOn)
                        WriteDebug("PlayerStatus");
                    packet = new Packets.Incoming.PlayerStatusPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerStatusIncomingPacket != null)
                            packet.Forward = ReceivedPlayerStatusIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSkull:
                    if (DebugOn)
                        WriteDebug("CreatureSkull");
                    packet = new Packets.Incoming.CreatureSkullPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSkullIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSkullIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.WaitingList:
                    if (DebugOn)
                        WriteDebug("WaitingList");
                    packet = new Packets.Incoming.WaitingListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedWaitingListIncomingPacket != null)
                            packet.Forward = ReceivedWaitingListIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Ping:
                    if (DebugOn)
                        WriteDebug("Ping");
                    packet = new Packets.Incoming.PingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPingIncomingPacket != null)
                            packet.Forward = ReceivedPingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Death:
                    if (DebugOn)
                        WriteDebug("Death");
                    packet = new Packets.Incoming.DeathPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedDeathIncomingPacket != null)
                            packet.Forward = ReceivedDeathIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CanReportBugs:
                    if (DebugOn)
                        WriteDebug("CanReportBugs");
                    packet = new Packets.Incoming.CanReportBugsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCanReportBugsIncomingPacket != null)
                            packet.Forward = ReceivedCanReportBugsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileUpdate:
                    if (DebugOn)
                        WriteDebug("TileUpdate");
                    packet = new Packets.Incoming.TileUpdatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileUpdateIncomingPacket != null)
                            packet.Forward = ReceivedTileUpdateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FyiMessage:
                    if (DebugOn)
                        WriteDebug("FyiMessage");
                    packet = new Packets.Incoming.FyiMessagePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFyiMessageIncomingPacket != null)
                            packet.Forward = ReceivedFyiMessageIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.InventorySetSlot:
                    if (DebugOn)
                        WriteDebug("InventorySetSlot");
                    packet = new Packets.Incoming.InventorySetSlotPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedInventorySetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventorySetSlotIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.InventoryResetSlot:
                    if (DebugOn)
                        WriteDebug("InventoryResetSlot");
                    packet = new Packets.Incoming.InventoryResetSlotPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedInventoryResetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventoryResetSlotIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestAck:
                    if (DebugOn)
                        WriteDebug("SafeTradeRequestAck");
                    packet = new Packets.Incoming.SafeTradeRequestAckPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeRequestAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestAckIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestNoAck:
                    if (DebugOn)
                        WriteDebug("SafeTradeRequestNoAck");
                    packet = new Packets.Incoming.SafeTradeRequestNoAckPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeRequestNoAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestNoAckIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeClose:
                    if (DebugOn)
                        WriteDebug("SafeTradeClose");
                    packet = new Packets.Incoming.SafeTradeClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeCloseIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerSkillsUpdate:
                    if (DebugOn)
                        WriteDebug("PlayerSkillsUpdate");
                    packet = new Packets.Incoming.PlayerSkillsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerSkillsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerSkillsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerFlags:
                    if (DebugOn)
                        WriteDebug("PlayerFlags");
                    packet = new Packets.Incoming.PlayerFlagsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerFlagsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerFlagsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelOpenPrivate:
                    if (DebugOn)
                        WriteDebug("ChannelOpenPrivate");
                    packet = new Packets.Incoming.ChannelOpenPrivatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelOpenPrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenPrivateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PrivateChannelCreate:
                    if (DebugOn)
                        WriteDebug("PrivateChannelCreate");
                    packet = new Packets.Incoming.PrivateChannelCreatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPrivateChannelCreateIncomingPacket != null)
                            packet.Forward = ReceivedPrivateChannelCreateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelClosePrivate:
                    if (DebugOn)
                        WriteDebug("ChannelClosePrivate");
                    packet = new Packets.Incoming.ChannelClosePrivatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelClosePrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelClosePrivateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipState:
                    if (DebugOn)
                        WriteDebug("VipState");
                    packet = new Packets.Incoming.VipStatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipStateIncomingPacket != null)
                            packet.Forward = ReceivedVipStateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipLogin:
                    if (DebugOn)
                        WriteDebug("VipLogin");
                    packet = new Packets.Incoming.VipLoginPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipLoginIncomingPacket != null)
                            packet.Forward = ReceivedVipLoginIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipLogout:
                    if (DebugOn)
                        WriteDebug("VipLogout");
                    packet = new Packets.Incoming.VipLogoutPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipLogoutIncomingPacket != null)
                            packet.Forward = ReceivedVipLogoutIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopSaleGoldCount:
                    if (DebugOn)
                        WriteDebug("ShopSaleGoldCount");
                    packet = new Packets.Incoming.ShopSaleGoldCountPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopSaleGoldCountIncomingPacket != null)
                            packet.Forward = ReceivedShopSaleGoldCountIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopWindowOpen:
                    if (DebugOn)
                        WriteDebug("ShopWindowOpen");
                    packet = new Packets.Incoming.ShopWindowOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopWindowOpenIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopWindowClose:
                    if (DebugOn)
                        WriteDebug("ShopWindowClose");
                    packet = new Packets.Incoming.ShopWindowClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopWindowCloseIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.OutfitWindow:
                    if (DebugOn)
                        WriteDebug("OutfitWindow");
                    packet = new Packets.Incoming.OutfitWindowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedOutfitWindowIncomingPacket != null)
                            packet.Forward = ReceivedOutfitWindowIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationOpen:
                    if (DebugOn)
                        WriteDebug("RuleViolationOpen");
                    packet = new Packets.Incoming.RuleViolationOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationOpenIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationRemove:
                    if (DebugOn)
                        WriteDebug("RuleViolationRemove");
                    packet = new Packets.Incoming.RuleViolationRemovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationRemoveIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationRemoveIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationCancel:
                    if (DebugOn)
                        WriteDebug("RuleViolationCancel");
                    packet = new Packets.Incoming.RuleViolationCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationCancelIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationCancelIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationLock:
                    if (DebugOn)
                        WriteDebug("RuleViolationLock");
                    packet = new Packets.Incoming.RuleViolationLockPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationLockIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationLockIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CancelTarget:
                    if (DebugOn)
                        WriteDebug("CancelTarget");

                    packet = new Packets.Incoming.CancelTargetPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCancelTargetIncomingPacket != null)
                            packet.Forward = ReceivedCancelTargetIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                default:
                    break;
            }

            return null;
        }
        #endregion

        #region ServerPacket
        protected OutgoingPacket ParseServerPacket(Client client, NetworkMessage msg, Location pos)
        {
            OutgoingPacket packet;
            OutgoingPacketType type = (OutgoingPacketType)msg.PeekByte();

            switch (type)
            {
                case OutgoingPacketType.ChannelClose:
                    packet = new Packets.Outgoing.ChannelClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedChannelCloseOutgoingPacket != null)
                            packet.Forward = ReceivedChannelCloseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ChannelOpen:
                    packet = new Packets.Outgoing.ChannelOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedChannelOpenOutgoingPacket != null)
                            packet.Forward = ReceivedChannelOpenOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.PlayerSpeech:
                    packet = new Packets.Outgoing.PlayerSpeechPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedPlayerSpeechOutgoingPacket != null)
                            packet.Forward = ReceivedPlayerSpeechOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Attack:
                    packet = new Packets.Outgoing.AttackPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedAttackOutgoingPacket != null)
                            packet.Forward = ReceivedAttackOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Follow:
                    packet = new Packets.Outgoing.FollowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedFollowOutgoingPacket != null)
                            packet.Forward = ReceivedFollowOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.LookAt:
                    packet = new Packets.Outgoing.LookAtPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedLookAtOutgoingPacket != null)
                            packet.Forward = ReceivedLookAtOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUse:
                    packet = new Packets.Outgoing.ItemUsePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUseOn:
                    packet = new Packets.Outgoing.ItemUseOnPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseOnOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOnOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemMove:
                    packet = new Packets.Outgoing.ItemMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseBattlelistOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseBattlelistOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.CancelMove:
                    packet = new Packets.Outgoing.CancelMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedCancelMoveOutgoingPacket != null)
                            packet.Forward = ReceivedCancelMoveOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUseBattlelist:
                    packet = new Packets.Outgoing.ItemUseBattlelistPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedBattleWindowOutgoingPacket != null)
                            packet.Forward = ReceivedBattleWindowOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Logout:
                    packet = new Packets.Outgoing.LogoutPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedLogoutOutgoingPacket != null)
                            packet.Forward = ReceivedLogoutOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ContainerClose:
                    packet = new Packets.Outgoing.ContainerClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedContainerCloseOutgoingPacket != null)
                            packet.Forward = ReceivedContainerCloseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ContainerOpenParent:
                    packet = new Packets.Outgoing.ContainerOpenParentPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedContainerOpenParentOutgoingPacket != null)
                            packet.Forward = ReceivedContainerOpenParentOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ShopBuy:
                    packet = new Packets.Outgoing.ShopBuyPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedShopBuyOutgoingPacket != null)
                            packet.Forward = ReceivedShopBuyOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ShopSell:
                    packet = new Packets.Outgoing.ShopSellPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedShopSellOutgoingPacket != null)
                            packet.Forward = ReceivedShopSellOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.TurnDown:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Down);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnUp:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Up);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Left);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Right);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDown:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Down);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDownLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.DownLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDownRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.DownRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Left);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Right);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUp:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Up);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUpLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.UpLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUpRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.UpRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.AutoWalk:
                    packet = new Packets.Outgoing.AutoWalkPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedAutoWalkOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.VipAdd:
                    packet = new Packets.Outgoing.VipAddPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedVipAddOutgoingPacket != null)
                            packet.Forward = ReceivedVipAddOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.VipRemove:
                    packet = new Packets.Outgoing.VipRemovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedVipRemoveOutgoingPacket != null)
                            packet.Forward = ReceivedVipRemoveOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemRotate:
                    packet = new Packets.Outgoing.ItemRotatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemRotateOutgoingPacket != null)
                            packet.Forward = ReceivedItemRotateOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                default:
                    break;
            }

            return null;
        }
        #endregion

        protected void WriteDebug(string message)
        {
            if (PrintDebug != null)
                PrintDebug.BeginInvoke(message, null, null);
        }

        protected bool DebugOn { get; set; }
    }
}