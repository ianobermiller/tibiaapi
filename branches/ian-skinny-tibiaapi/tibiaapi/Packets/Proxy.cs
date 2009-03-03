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
using System.Threading;

namespace Tibia.Packets
{
    public class Proxy : Util.SocketBase
    {
        #region Vars

        static byte[] localHostBytes = new byte[] { 127, 0, 0, 1 };
        static Random randon = new Random();

        private Client _client;

        private NetworkMessage _clientRecvMsg, _serverRecvMsg;
        private NetworkMessage _clientSendMsg, _serverSendMsg;

        private bool _isOtServer;

        private LoginServer[] _loginServers;
        private CharacterLoginInfo[] _charList;
        private ushort _serverPort;

        private uint[] _xteaKey;

        private Protocol _protocol = Protocol.NONE;

        private int _selectedLoginServer = 0;

        private TcpListener _clientTcp;
        private Socket _clientSocket;
        private NetworkStream _clientStream;
        private Queue<byte[]> _clientSendQueue;
        private object _clientSendQueueLock;
        private bool _clientWritting;
        private Thread _clientSendThread;
        private object _clientSendThreadLock;

        private bool _accepting = false;
        private object _restartLock;

        private TcpClient _serverTcp;
        private NetworkStream _serverStream;
        private Queue<byte[]> _serverSendQueue;
        private object _serverSendQueueLock;
        private bool _serverWritting;
        private Thread _serverSendThread;
        private object _serverSendThreadLock;

        private object _debugLock;
        private bool _connected;

        private DateTime _lastInteraction;

        #endregion

        #region Constructor

        public Proxy(Client client)
        {
            try
            {
                _client = client;
                _serverRecvMsg = new NetworkMessage(_client);
                _clientRecvMsg = new NetworkMessage(_client);
                _clientSendMsg = new NetworkMessage(_client);
                _serverSendMsg = new NetworkMessage(_client);

                _clientSendQueue = new Queue<byte[]> { };
                _serverSendQueue = new Queue<byte[]> { };

                _clientSendQueueLock = new object();
                _serverSendQueueLock = new object();
                _restartLock = new object();

                _clientSendThreadLock = new object();
                _serverSendThreadLock = new object();

                _debugLock = new object();

                _xteaKey = new uint[4];

                _loginServers = _client.Login.Servers;

                if (_loginServers[0].Server == "localhost")
                    _loginServers = _client.Login.DefaultServers;

                if (_serverPort == 0)
                    _serverPort = GetFreePort();

                _client.Login.SetServer("localhost", (short)_serverPort);

                if (_client.Login.RSA == Constants.RSAKey.OpenTibia)
                    _isOtServer = true;
                else
                {
                    _client.Login.RSA = Constants.RSAKey.OpenTibia;
                    _isOtServer = false;
                }

                if (_client.Login.CharListCount != 0)
                {
                    _charList = _client.Login.CharacterList;
                    _client.Login.SetCharListServer(localHostBytes, _serverPort);
                }

                //login event
                ReceivedSelfAppearIncomingPacket += new IncomingPacketListener(Proxy_ReceivedSelfAppearIncomingPacket);

                startListen();
                _client.IO.UsingProxy = true;
            }
            catch (Exception ex)
            {
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        #endregion

        #region Events Handle

        private bool Proxy_ReceivedSelfAppearIncomingPacket(IncomingPacket packet)
        {
            _connected = true;

            if (PlayerLogin != null)
                Util.Scheduler.AddTask(PlayerLogin, new object[] { this, new EventArgs() }, 1000);

            return true;
        }

        #endregion

        #region Events

        public event EventHandler PlayerLogin;
        public event EventHandler PlayerLogout;

        #endregion

        #region Propriedades

        public uint[] XteaKey
        {
            get { return _xteaKey; }
        }

        #endregion

        #region Public Functions

        public void CheckState()
        {
            if ((DateTime.Now - _lastInteraction).TotalSeconds >= 30)
            {
                restart();
            }
        }

        public void SendToServer(byte[] data)
        {
            lock (_serverSendQueueLock)
            {
                _serverSendQueue.Enqueue(data);
            }

            lock (_serverSendThreadLock)
            {
                if (!_serverWritting)
                {
                    _serverWritting = true;
                    _serverSendThread = new Thread(new ThreadStart(serverSend));
                    _serverSendThread.Start();
                }
            }
        }

        public void SendToClient(byte[] data)
        {
            lock (_clientSendQueueLock)
            {
                _clientSendQueue.Enqueue(data);
            }

            lock (_clientSendThreadLock)
            {
                if (!_clientWritting)
                {
                    _clientWritting = true;
                    _clientSendThread = new Thread(new ThreadStart(clientSend));
                    _clientSendThread.Start();
                }
            }
        }

        #endregion

        #region Private Functions

        private void startListen()
        {
            try
            {
                _accepting = true;
                _protocol = Protocol.NONE;
                _clientSendQueue.Clear();
                _serverSendQueue.Clear();

                _clientTcp = new TcpListener(IPAddress.Any, _serverPort);
                _clientTcp.Start();
                _clientTcp.BeginAcceptSocket(new AsyncCallback(listenCallBack), null);
            }
            catch (Exception ex)
            {
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void restart()
        {
            lock (_restartLock)
            {
                if (_accepting)
                    return;

                stop();
                startListen();
            }
        }

        private void stop()
        {
            try
            {
                if (_connected)
                {
                    _connected = false;

                    if (PlayerLogout != null)
                        PlayerLogout.Invoke(this, new EventArgs());
                }

                if (_clientTcp != null)
                    _clientTcp.Stop();

                if (_clientSocket != null)
                    _clientSocket.Close();

                if (_clientStream != null)
                    _clientStream.Close();

                if (_serverTcp != null)
                    _serverTcp.Close();
            }
            catch (Exception ex)
            {
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void serverSend()
        {
            try
            {
                byte[] packet = null;

                lock (_serverSendQueueLock)
                {
                    if (_serverSendQueue.Count > 0)
                        packet = _serverSendQueue.Dequeue();
                    else
                    {
                        _serverWritting = false;
                        return;
                    }
                }

                if (packet == null)
                {
                    _serverWritting = false;
                    throw new Exception("Null Packet.");
                }

                _serverStream.BeginWrite(packet, 0, packet.Length, new AsyncCallback(serverSendCallBack), null);
            }
            catch (Exception ex)
            {
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void clientSend()
        {
            try
            {
                byte[] packet = null;

                lock (_clientSendQueueLock)
                {
                    if (_clientSendQueue.Count > 0)
                        packet = _clientSendQueue.Dequeue();
                    else
                    {
                        _clientWritting = false;
                        return;
                    }
                }

                if (packet == null)
                {
                    _clientWritting = false;
                    throw new Exception("Null Packet.");
                }

                _clientStream.BeginWrite(packet, 0, packet.Length, new AsyncCallback(clientSendCallBack), null);
            }
            catch (System.IO.IOException) { }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void parseCharacterList()
        {
            try
            {
                if (_serverRecvMsg.CheckAdler32() && _serverRecvMsg.PrepareToRead())
                {
                    _serverRecvMsg.GetUInt16();

                    while (_serverRecvMsg.Position < _serverRecvMsg.Length)
                    {
                        byte cmd = _serverRecvMsg.GetByte();

                        switch (cmd)
                        {
                            case 0x0A: //Error message
                                _serverRecvMsg.GetString();
                                break;
                            case 0x0B: //For your information
                                _serverRecvMsg.GetString();
                                break;
                            case 0x14: //MOTD
                                _serverRecvMsg.GetString();
                                break;
                            case 0x1E: //Patching exe/dat/spr messages
                            case 0x1F:
                            case 0x20:
                                //DisconnectClient(0x0A, "A new client is avalible, please download it first!");
                                return;
                            case 0x28: //Select other login server
                                _selectedLoginServer = randon.Next(0, _loginServers.Length - 1);
                                break;
                            case 0x64: //character list
                                int nChar = (int)_serverRecvMsg.GetByte();
                                _charList = new CharacterLoginInfo[nChar];

                                for (int i = 0; i < nChar; i++)
                                {
                                    _charList[i].CharName = _serverRecvMsg.GetString();
                                    _charList[i].WorldName = _serverRecvMsg.GetString();
                                    _charList[i].WorldIP = _serverRecvMsg.PeekUInt32();
                                    _serverRecvMsg.AddBytes(localHostBytes);
                                    _charList[i].WorldPort = _serverRecvMsg.PeekUInt16();
                                    _serverRecvMsg.AddUInt16(_serverPort);
                                }

                                //send this data to client
                                _serverRecvMsg.PrepareToSend();
                                _clientStream.Write(_serverRecvMsg.GetBuffer(), 0, _serverRecvMsg.Length);
                                restart();
                                return;
                            default:
                                break;
                        }
                    }

                    _serverRecvMsg.PrepareToSend();
                    _clientStream.Write(_serverRecvMsg.GetBuffer(), 0, _serverRecvMsg.Length);
                    restart();
                    return;
                }
                else
                    restart();
            }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void parseFirstClientMsg()
        {
            try
            {
                _clientRecvMsg.Position = 6;
                byte protocolId = _clientRecvMsg.GetByte();
                int position;

                switch (protocolId)
                {
                    case 0x01:

                        _protocol = Protocol.LOGIN;
                        _clientRecvMsg.GetUInt16();
                        ushort clientVersion = _clientRecvMsg.GetUInt16();

                        _clientRecvMsg.GetUInt32();
                        _clientRecvMsg.GetUInt32();
                        _clientRecvMsg.GetUInt32();

                        position = _clientRecvMsg.Position;

                        _clientRecvMsg.RsaOTDecrypt();

                        if (_clientRecvMsg.GetByte() != 0)
                        {
                            restart();
                            return;
                        }

                        _xteaKey[0] = _clientRecvMsg.GetUInt32();
                        _xteaKey[1] = _clientRecvMsg.GetUInt32();
                        _xteaKey[2] = _clientRecvMsg.GetUInt32();
                        _xteaKey[3] = _clientRecvMsg.GetUInt32();

                        if (clientVersion != 840)
                        {
                        }

                        _serverTcp = new TcpClient(_loginServers[_selectedLoginServer].Server, _loginServers[_selectedLoginServer].Port);
                        _serverStream = _serverTcp.GetStream();

                        if (_isOtServer)
                            _clientRecvMsg.RsaOTEncrypt(position);
                        else
                            _clientRecvMsg.RsaCipEncrypt(position);

                        _clientRecvMsg.InsertAdler32();
                        _clientRecvMsg.InsertPacketHeader();

                        _serverStream.Write(_clientRecvMsg.GetBuffer(), 0, _clientRecvMsg.Length);
                        _serverStream.BeginRead(_serverRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(serverReadCallBack), null);
                        break;

                    case 0x0A:

                        _protocol = Protocol.WORLD;
                        _clientRecvMsg.GetUInt16();
                        _clientRecvMsg.GetUInt16();


                        position = _clientRecvMsg.Position;

                        _clientRecvMsg.RsaOTDecrypt();

                        if (_clientRecvMsg.GetByte() != 0)
                        {
                            restart();
                            return;
                        }

                        _xteaKey[0] = _clientRecvMsg.GetUInt32();
                        _xteaKey[1] = _clientRecvMsg.GetUInt32();
                        _xteaKey[2] = _clientRecvMsg.GetUInt32();
                        _xteaKey[3] = _clientRecvMsg.GetUInt32();


                        _clientRecvMsg.GetByte(); //unknow..
                        _clientRecvMsg.GetString();
                        string name = _clientRecvMsg.GetString();
                        int selectedChar = getSelectedChar(name);

                        if (selectedChar >= 0)
                        {
                            if (_isOtServer)
                                _clientRecvMsg.RsaOTEncrypt(position);
                            else
                                _clientRecvMsg.RsaCipEncrypt(position);

                            _clientRecvMsg.InsertAdler32();
                            _clientRecvMsg.InsertPacketHeader();

                            _serverTcp = new TcpClient(BitConverter.GetBytes(_charList[selectedChar].WorldIP).ToIPString(), _charList[selectedChar].WorldPort);
                            _serverStream = _serverTcp.GetStream();

                            _serverStream.Write(_clientRecvMsg.GetBuffer(), 0, _clientRecvMsg.Length);

                            _serverStream.BeginRead(_serverRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(serverReadCallBack), null);
                            _clientStream.BeginRead(_clientRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(clientReadCallBack), null);
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        #endregion

        #region Callbacks

        private void serverSendCallBack(IAsyncResult ar)
        {
            try
            {
                _serverStream.EndWrite(ar);

                bool runAgain = false;

                lock (_serverSendQueueLock)
                {
                    if (_serverSendQueue.Count > 0)
                        runAgain = true;
                }

                if (runAgain)
                    serverSend();
                else
                    _serverWritting = false;
            }
            catch (ObjectDisposedException) { restart(); }
            catch (System.IO.IOException) { restart(); }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void clientSendCallBack(IAsyncResult ar)
        {
            try
            {
                _clientStream.EndWrite(ar);

                bool runAgain = false;

                lock (_clientSendQueueLock)
                {
                    if (_clientSendQueue.Count > 0)
                        runAgain = true;
                }

                if (runAgain)
                    clientSend();
                else
                    _clientWritting = false;
            }
            catch (ObjectDisposedException) { restart(); }
            catch (System.IO.IOException) { restart(); }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void listenCallBack(IAsyncResult ar)
        {
            try
            {
                _accepting = false;
                _clientSocket = _clientTcp.EndAcceptSocket(ar);
                _clientTcp.Stop();

                _clientStream = new NetworkStream(_clientSocket);
                _clientStream.BeginRead(_clientRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(clientReadCallBack), null);
            }
            catch (ObjectDisposedException) { restart(); }
            catch (System.IO.IOException) { restart(); }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void serverReadCallBack(IAsyncResult ar)
        {
            try
            {
                int read = _serverStream.EndRead(ar);

                if (read < 2)
                {
                    restart();
                    return;
                }

                _lastInteraction = DateTime.Now;
                int pSize = (int)BitConverter.ToUInt16(_serverRecvMsg.GetBuffer(), 0) + 2;

                while (read < pSize)
                {
                    if (_serverStream.CanRead)
                        read += _serverStream.Read(_serverRecvMsg.GetBuffer(), read, pSize - read);
                    else
                    {
                        throw new Exception("Connection broken.");
                    }
                }

                _serverRecvMsg.Length = pSize;

                switch (_protocol)
                {
                    case Protocol.LOGIN:
                        parseCharacterList();
                        break;
                    case Protocol.WORLD:

                        if (_serverRecvMsg.CheckAdler32() && _serverRecvMsg.XteaDecrypt(_xteaKey))
                        {

                            _serverRecvMsg.Position = 6;
                            int msgSize = (int)_serverRecvMsg.GetUInt16() + 8;
                            _clientSendMsg.Reset();

                            while (_serverRecvMsg.Position < msgSize)
                            {
                                if (!ParseServerPacket(_client, _serverRecvMsg, _clientSendMsg))
                                {
                                    byte[] unknown = _serverRecvMsg.GetBytes(_serverRecvMsg.Length - _serverRecvMsg.Position);
                                    writeDebug("Unknown incoming packet: " + unknown.ToHexString());
                                    _clientSendMsg.AddBytes(unknown);
                                    break;
                                }
                            }

                            if (_clientSendMsg.Length > 8)
                            {
                                _clientSendMsg.InsetLogicalPacketHeader();
                                _clientSendMsg.XteaEncrypt(_xteaKey);
                                _clientSendMsg.InsertAdler32();
                                _clientSendMsg.InsertPacketHeader();

                                lock (_clientSendQueueLock)
                                {
                                    _clientSendQueue.Enqueue(_clientSendMsg.Data);
                                }

                                lock (_clientSendThreadLock)
                                {
                                    if (!_clientWritting)
                                    {
                                        _clientWritting = true;
                                        _clientSendThread = new Thread(new ThreadStart(clientSend));
                                        _clientSendThread.Start();
                                    }
                                }
                            }
                        }

                        _serverStream.BeginRead(_serverRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(serverReadCallBack), null);
                        break;
                    case Protocol.NONE:
                        break;
                }
            }
            catch (System.IO.IOException) { }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        private void clientReadCallBack(IAsyncResult ar)
        {
            try
            {
                int read = _clientStream.EndRead(ar);

                if (read < 2)
                {
                    restart();
                    return;
                }

                int pSize = (int)BitConverter.ToUInt16(_clientRecvMsg.GetBuffer(), 0) + 2;

                while (read < pSize)
                {
                    if (_clientStream.CanRead)
                        read += _clientStream.Read(_clientRecvMsg.GetBuffer(), read, pSize - read);
                    else
                    {
                        throw new Exception("Connection broken.");
                    }
                }

                _clientRecvMsg.Length = pSize;

                switch (_protocol)
                {
                    case Protocol.NONE:
                        parseFirstClientMsg();
                        break;
                    case Protocol.WORLD:

                        if (_clientRecvMsg.CheckAdler32() && _clientRecvMsg.XteaDecrypt(_xteaKey))
                        {

                            _clientRecvMsg.Position = 6;
                            int msgLength = (int)_clientRecvMsg.GetUInt16() + 8;
                            _serverSendMsg.Reset();

                            if (!ParseClientPacket(_client, _clientRecvMsg, _serverSendMsg))
                            {
                                //unknown packet
                                byte[] unknown = _clientRecvMsg.GetBytes(_clientRecvMsg.Length - _clientRecvMsg.Position);
                                writeDebug("Unknown outgoing packet: " + unknown.ToHexString());
                                _serverSendMsg.AddBytes(unknown);
                            }

                            if (_serverSendMsg.Length > 8)
                            {
                                _serverSendMsg.InsetLogicalPacketHeader();
                                _serverSendMsg.XteaEncrypt(_xteaKey);
                                _serverSendMsg.InsertAdler32();
                                _serverSendMsg.InsertPacketHeader();

                                lock (_serverSendQueueLock)
                                {
                                    _serverSendQueue.Enqueue(_serverSendMsg.Data);
                                }

                                lock (_serverSendThreadLock)
                                {
                                    if (!_serverWritting)
                                    {
                                        _serverWritting = true;
                                        _serverSendThread = new Thread(new ThreadStart(serverSend));
                                        _serverSendThread.Start();
                                    }
                                }
                            }
                        }
                        _clientStream.BeginRead(_clientRecvMsg.GetBuffer(), 0, 2, new AsyncCallback(clientReadCallBack), null);
                        break;
                    case Protocol.LOGIN:
                        break;
                }
            }
            catch (ObjectDisposedException) { }
            catch (System.IO.IOException) { }
            catch (Exception ex)
            {
                restart();
                writeDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }

        #endregion

        #region Private Enums

        private enum Protocol
        {
            NONE,
            LOGIN,
            WORLD
        }

        #endregion

        #region Other Functions

        private void writeDebug(string msg)
        {
            try
            {
                lock (_debugLock)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(Application.StartupPath, "proxy_log.txt"), true);
                    sw.WriteLine(System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + " >> " + msg);
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private int getSelectedChar(string name)
        {
            for (int i = 0; i < _charList.Length; i++)
            {
                if (_charList[i].CharName == name)
                    return i;
            }

            return -1;
        }

        #endregion
    }
}