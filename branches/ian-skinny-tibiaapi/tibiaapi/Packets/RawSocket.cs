using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tibia;
using Tibia.Objects;
using Tibia.Util;

namespace Tibia.Packets
{
    public class RawSocket : SocketBase
    {

        #region Variables
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);
        private Client client;
        private int pid;

        private Socket mainSocket;
        private ushort localPort;
        private ushort remotePort;
        private ushort proxyPort = 0;
        private SocketMode mode = SocketMode.UsingDefaultRemotePort;
        private byte[] receive_buf = new byte[4096];
        private bool Adler;
        private bool enabled = false;
        private bool moreToCome = false;
        private int bytesLeftToCome = 0;
        private byte[] toJoin;
        private Queue<byte[]> IncomingGamePacketQueue = new Queue<byte[]>();
        private Queue<byte[]> OutgoingGamePacketQueue = new Queue<byte[]>();
        private Queue<byte[]> packetServerToClientQueue = new Queue<byte[]>();
        private Queue<string> flagServerToClientQueue = new Queue<string>();
        #endregion

        #region Properties
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public ushort ProxyPort
        {
            get { return proxyPort; }
            set { proxyPort = value; }
        }

        public SocketMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        #endregion

        #region Events

        public delegate void Notification(string where, string messsage);
        public Notification OnError;

        public delegate void MessageListener(NetworkMessage message);
        public event MessageListener ReceivedMessageFromClient;
        public event MessageListener ReceivedMessageFromServer;

        public delegate void RawPacketListener(byte[] packet, string flags);
        public RawPacketListener ReceivedIPPacketFromClient;
        public RawPacketListener ReceivedIPPacketFromServer;
        public RawPacketListener ReceivedTCPPacketFromClient;
        public RawPacketListener ReceivedTCPPacketFromServer;
        public RawPacketListener ReceivedRawGamePacketFromServer;

        #endregion

        #region Structs and Enums

        public enum SocketMode
        {
            UsingDefaultRemotePort,
            UsingSpecialRemotePort,
            UsingProxy
        }

        public enum TCP_TABLE_CLASS
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public uint state;
            public uint localAddr;
            public byte localPort1;
            public byte localPort2;
            public byte localPort3;
            public byte localPort4;
            public uint remoteAddr;
            public byte remotePort1;
            public byte remotePort2;
            public byte remotePort3;
            public byte remotePort4;
            public int owningPid;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            MIB_TCPROW_OWNER_PID table;
        }
        #endregion

        #region TCP table

        //public TcpRow[] GetAllTcpConnections()
        public static MIB_TCPROW_OWNER_PID[] GetAllTcpConnections()
        {
            //  TcpRow is my own class to display returned rows in a nice manner.
            //    TcpRow[] tTable;
            MIB_TCPROW_OWNER_PID[] tTable;
            int AF_INET = 2;    // IP_v4
            int buffSize = 0;

            // how much memory do we need?
            uint ret = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                ret = GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
                if (ret != 0)
                {
                    return null;
                }

                // get the number of entries in the table
                //MibTcpTable tab = (MibTcpTable)Marshal.PtrToStructure(buffTable, typeof(MibTcpTable));
                MIB_TCPTABLE_OWNER_PID tab = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_TCPTABLE_OWNER_PID));
                //IntPtr rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tab.numberOfEntries) );
                IntPtr rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tab.dwNumEntries));
                // buffer we will be returning
                //tTable = new TcpRow[tab.numberOfEntries];
                tTable = new MIB_TCPROW_OWNER_PID[tab.dwNumEntries];

                //for (int i = 0; i < tab.numberOfEntries; i++)        
                for (int i = 0; i < tab.dwNumEntries; i++)
                {
                    //MibTcpRow_Owner_Pid tcpRow = (MibTcpRow_Owner_Pid)Marshal.PtrToStructure(rowPtr, typeof(MibTcpRow_Owner_Pid));
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    //tTable[i] = new TcpRow(tcpRow);
                    tTable[i] = tcpRow;
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));   // next entry
                }

            }
            finally
            {
                // Free the Memory
                Marshal.FreeHGlobal(buffTable);
            }

            return tTable;
        }
        #endregion

        #region Socket Core
        public RawSocket(Client client, bool adler)
            : this(client, adler, GetDefaultLocalIp()) { }
        public RawSocket(Client client, bool adler, string localIp)
        {
            this.Adler = adler;
            this.client = client;
            pid = client.Process.Id;

            mainSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Raw, ProtocolType.IP);

            //Bind the socket to the selected IP address
            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(localIp), 0));

            //Set the socket  options
            mainSocket.SetSocketOption(SocketOptionLevel.IP,            //Applies only to IP packets
                                       SocketOptionName.HeaderIncluded, //Set the include the header
                                       true);

            // FIX THIS PART!
            byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
            byte[] byOut = new byte[4] { 1, 0, 0, 0 }; //Capture outgoing packets

            //Socket.IOControl is analogous to the WSAIoctl method of Winsock 2
            mainSocket.IOControl(IOControlCode.ReceiveAll,              //Equivalent to SIO_RCVALL constant
                //of Winsock 2
                                 byTrue,
                                 byOut);

            //Start receiving the packets asynchronously
            mainSocket.BeginReceive(receive_buf, 0, receive_buf.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), null);
        }

        public void Close()
        {
            mainSocket.Close();
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int bytesRead = mainSocket.EndReceive(ar);
                if (enabled && bytesRead > 0)
                {
                    PreParse(receive_buf, bytesRead);
                }
                mainSocket.BeginReceive(receive_buf, 0, receive_buf.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), null);
            }
            catch (ObjectDisposedException)
            {
                if (OnError != null)
                    OnError("OnReceive", "Objected Disposed Exception");
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError("OnReceive", ex.Message);
            }
        }

        private void PreParse(byte[] ipPacket, int bytesRead)
        {
            try
            {
                if (ipPacket[9] == 0x06)
                {
                    ushort datagramLength = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(ipPacket, 2));
                    byte ipHeaderLength = ipPacket[0];
                    ipHeaderLength <<= 4;
                    ipHeaderLength >>= 4;
                    ipHeaderLength *= 4;
                    byte[] tcpPacket = new byte[datagramLength - ipHeaderLength];
                    Array.Copy(ipPacket, ipHeaderLength, tcpPacket, 0, tcpPacket.Length);
                    ushort sourcePort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(tcpPacket, 0));
                    ushort destinationPort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(tcpPacket, 2));
                    ushort dataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(tcpPacket, 12));
                    byte tcpHeaderLength = (byte)(dataOffsetAndFlags >> 12);
                    tcpHeaderLength *= 4;
                    if ((tcpPacket.Length - tcpHeaderLength) > 0)
                    {
                        byte[] gameRawPacket = new byte[tcpPacket.Length - tcpHeaderLength];
                        Array.Copy(tcpPacket, tcpHeaderLength, gameRawPacket, 0, gameRawPacket.Length);
                        string flags = GetFlags(tcpPacket);
                        UpdatePorts();
                        //Packet from server
                        if (sourcePort == remotePort && destinationPort == localPort)
                        {
                            if (ReceivedRawGamePacketFromServer != null)
                                ReceivedRawGamePacketFromServer((byte[])gameRawPacket.Clone(), String.Copy(flags));
                            if (ReceivedIPPacketFromClient != null)
                                ReceivedIPPacketFromClient((byte[])ipPacket.Clone(), String.Copy(flags));
                            if (ReceivedTCPPacketFromClient != null)
                                ReceivedTCPPacketFromClient((byte[])tcpPacket.Clone(), String.Copy(flags));
                            ParseServerToClient(gameRawPacket, flags);
                        }
                        //packet from client
                        else if (sourcePort == localPort && destinationPort == remotePort)
                        {
                            if (ReceivedIPPacketFromClient != null)
                                ReceivedIPPacketFromClient((byte[])ipPacket.Clone(), String.Copy(flags));
                            if (ReceivedTCPPacketFromClient != null)
                                ReceivedTCPPacketFromClient((byte[])tcpPacket.Clone(), String.Copy(flags));
                            RaiseOutgoingEvents(gameRawPacket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError("PreParse", ex.Message);
            }
        }

        private void RaiseOutgoingEvents(byte[] data)
        {
            if (ReceivedMessageFromClient != null)
                ReceivedMessageFromClient(new NetworkMessage(client, data));

            NetworkMessage msg = new NetworkMessage(client, data);

            msg.PrepareToRead();
            msg.GetUInt16();

            while (msg.Position < msg.Length)
            {
                OutgoingPacket packet = ParseServerPacket(client, msg);
                byte[] packetBytes;

                if (packet == null)
                {
                    packetBytes = msg.GetBytes(msg.Length - msg.Position);

                    if (packetBytes.Length > 0)
                    {
                        OnOutgoingSplitPacket(packetBytes[0], packetBytes);
                    }
                    break;
                }
                else
                {

                    packetBytes = packet.ToByteArray();

                    OnOutgoingSplitPacket((byte)packet.Type, packetBytes);
                }
            }
        }

        private void ParseServerToClient(byte[] packet, string flags)
        {
            if (BitConverter.ToInt16(packet, 0) == packet.Length - 2)
            {
                packetServerToClientQueue.Clear();
                flagServerToClientQueue.Clear();
                IncomingGamePacketQueue.Enqueue(packet);
                ProcessIncomingGamePacketQueue();
            }
            else
            {
                packetServerToClientQueue.Enqueue(packet);
                flagServerToClientQueue.Enqueue(flags);
            }
            if (packetServerToClientQueue.Count > 0) ProcessQueues();
        }

        private void ProcessQueues()
        {
            int N = packetServerToClientQueue.Count;
            byte[][] packets = new byte[N][];
            string[] flags = new string[N];
            byte[] lastpart = new byte[1];
            byte[] gamePacket;
            bool hasACK = false;
            int totallength = 0;
            int offset = 0;
            for (int i = 0; i < N; i++)
            {
                flags[i] = flagServerToClientQueue.Dequeue();
                packets[i] = packetServerToClientQueue.Dequeue();
                totallength += packets[i].Length;
            }
            gamePacket = new byte[totallength];
            for (int i = 0; i < N; i++)
            {
                if (flags[i] == "0x18 (PSH, ACK)")
                {
                    lastpart = new byte[packets[i].Length];
                    lastpart = (byte[])packets[i].Clone();
                }
                else
                {
                    hasACK = true;
                    packets[i].CopyTo(gamePacket, offset);
                    offset += packets[i].Length;
                }
            }
            if (lastpart.Length > 1)
            {
                if (hasACK)
                {
                    lastpart.CopyTo(gamePacket, offset);
                    ParseGamePacket(gamePacket);
                    return;
                }
                else
                {
                    if (BitConverter.ToInt16(lastpart, 0) <= lastpart.Length - 2)
                    {
                        offset = 0;
                        while (offset <= lastpart.Length)
                        {
                            if (offset > lastpart.Length - 2) break;
                            offset += BitConverter.ToUInt16(lastpart, offset) + 2;
                        }
                        if (offset == lastpart.Length)
                        {
                            ParseGamePacket(lastpart);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < N; i++)
            {
                flagServerToClientQueue.Enqueue(flags[i]);
                packetServerToClientQueue.Enqueue(packets[i]);
            }
        }

        private void ParseGamePacket(byte[] gamePacket)
        {
            int offset = 0;
            int bytesRead = gamePacket.Length;
            while (bytesRead - offset > 0)
            {
                // Parse the data into a single packet
                if (moreToCome)
                {
                    if (bytesRead >= bytesLeftToCome)
                    {
                        Array.Copy(gamePacket, offset, toJoin, toJoin.Length - bytesLeftToCome, bytesLeftToCome);
                        IncomingGamePacketQueue.Enqueue(toJoin);

                        offset += bytesLeftToCome;
                        moreToCome = false;
                    }
                    else
                    {
                        Array.Copy(gamePacket, offset, toJoin,
                            toJoin.Length - bytesLeftToCome + bytesRead, bytesRead);

                        bytesLeftToCome -= bytesRead;
                        offset += bytesRead;
                    }
                }
                else
                {
                    // Get the packet length
                    int packetlength = BitConverter.ToInt16(gamePacket, offset) + 2;
                    if (packetlength <= bytesRead)
                    {
                        byte[] packet = new byte[packetlength];
                        Array.Copy(gamePacket, offset, packet, 0, packetlength);

                        IncomingGamePacketQueue.Enqueue(packet);
                    }
                    else
                    {
                        toJoin = new byte[packetlength];
                        Array.Copy(gamePacket, offset, toJoin, 0, packetlength);
                        bytesLeftToCome = packetlength - bytesRead;
                        moreToCome = true;
                    }
                    offset += packetlength;
                }
            }
            if (!moreToCome)
                ProcessIncomingGamePacketQueue();
        }

        private void ProcessIncomingGamePacketQueue()
        {
            while (IncomingGamePacketQueue.Count > 0)
            {

                NetworkMessage msg = new NetworkMessage(client, IncomingGamePacketQueue.Dequeue());
                if (ReceivedMessageFromServer != null)
                    ReceivedMessageFromServer.Invoke(msg);

                msg.PrepareToRead();
                msg.GetUInt16(); //logical packet size

                while (msg.Position < msg.Length)
                {
                    IncomingPacket packet = ParseClientPacket(client, msg);
                    byte[] packetBytes;

                    if (packet == null)
                    {
                        packetBytes = msg.GetBytes(msg.Length - msg.Position);

                        if (packetBytes.Length > 0)
                        {
                            OnIncomingSplitPacket(packetBytes[0], packetBytes);
                        }
                        break;
                    }
                    else
                    {
                        packetBytes = packet.ToByteArray();
                        OnIncomingSplitPacket((byte)packet.Type, packetBytes);
                    }
                }
            }
        }

        #endregion

        #region Ports, Flags

        public void UpdatePorts()
        {
            MIB_TCPROW_OWNER_PID[] tcptable = GetAllTcpConnections();
            switch (mode)
            {
                case SocketMode.UsingDefaultRemotePort:
                    for (int i = 0; i < tcptable.Length; i++)
                    {
                        ushort remote_ = BitConverter.ToUInt16(new byte[2] { tcptable[i].remotePort2, tcptable[i].remotePort1 }, 0);
                        if (remote_ == 7171 && tcptable[i].owningPid == pid)
                        {
                            remotePort = 7171;
                            localPort = BitConverter.ToUInt16(new byte[2] { tcptable[i].localPort2, tcptable[i].localPort1 }, 0);
                            return;
                        }
                    }
                    break;
                case SocketMode.UsingSpecialRemotePort:
                    for (int i = 0; i < tcptable.Length; i++)
                    {
                        if (tcptable[i].owningPid == pid)
                        {
                            remotePort = BitConverter.ToUInt16(new byte[2] { tcptable[i].remotePort2, tcptable[i].remotePort1 }, 0);
                            localPort = BitConverter.ToUInt16(new byte[2] { tcptable[i].localPort2, tcptable[i].localPort1 }, 0);
                            return;
                        }
                    }
                    localPort = 0;
                    break;
                case SocketMode.UsingProxy:
                    for (int i = 0; i < tcptable.Length; i++)
                    {
                        ushort remote_ = BitConverter.ToUInt16(new byte[2] { tcptable[i].remotePort2, tcptable[i].remotePort1 }, 0);
                        if (remote_ == proxyPort && tcptable[i].owningPid == pid)
                        {
                            localPort = BitConverter.ToUInt16(new byte[2] { tcptable[i].localPort2, tcptable[i].localPort1 }, 0);
                            //:O?
                            localPort++;
                            remotePort = 7171;
                            return;
                        }
                    }
                    break;
            }
        }

        private string GetFlags(byte[] ipData)
        {
            ushort dataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(ipData, 12));
            int nFlags = dataOffsetAndFlags & 0x3F;
            string strFlags = string.Format("0x{0:x2} (", nFlags);

            if ((nFlags & 0x01) != 0)
            {
                strFlags += "FIN, ";
            }
            if ((nFlags & 0x02) != 0)
            {
                strFlags += "SYN, ";
            }
            if ((nFlags & 0x04) != 0)
            {
                strFlags += "RST, ";
            }
            if ((nFlags & 0x08) != 0)
            {
                strFlags += "PSH, ";
            }
            if ((nFlags & 0x10) != 0)
            {
                strFlags += "ACK, ";
            }
            if ((nFlags & 0x20) != 0)
            {
                strFlags += "URG";
            }
            strFlags += ")";
            if (strFlags.Contains("()"))
            {
                strFlags = strFlags.Remove(strFlags.Length - 3);
            }
            else if (strFlags.Contains(", )"))
            {
                strFlags = strFlags.Remove(strFlags.Length - 3, 2);
            }
            return strFlags;
        }

        private Objects.Location GetPlayerPosition()
        {
            Location pos = Location.Invalid;

            try
            {
                pos = client.GetPlayer().Location;
            }
            catch (Exception) { }

            return pos;
        }
        #endregion
    }
}