using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tibia;
using Tibia.Packets;
using Tibia.Objects;
using Tibia.Util;

namespace Tibia.Util
{

    //FIXME: Update to new packet system.

    /*
    public class RawSocket
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);

        #region variables
        private Client client;
        private int pid;

        private Socket mainSocket;
        private ushort localPort;
        private ushort remotePort;
        private ushort proxyPort=0;
        private SocketMode mode=SocketMode.UsingDefaultRemotePort;
        private byte[] receive_buf = new byte[4096];
        private bool Adler;
        private bool log = false;
        private bool moreToCome = false;
        private int bytesLeftToCome = 0;
        private byte[] toJoin;
        private Queue<byte[]> IncomingGamePacketQueue = new Queue<byte[]>();
        private Queue<byte[]> OutgoingGamePacketQueue = new Queue<byte[]>();
        private Queue<byte[]> packetServerToClientQueue=new Queue<byte[]>();
        private Queue<string> flagServerToClientQueue=new Queue<string>();
        private int partialRemaining=0;
        public byte[] xtea=new byte[16];
        #endregion
        #region Events
        public delegate void RawPacketListener(byte[] packet, string flags);
        public delegate void PacketListener(Packet p);
        public delegate void SocketNotification(string message);

        public SocketNotification OnError;
        public RawPacketListener ReceivedIPPacketFromClient;
        public RawPacketListener ReceivedIPPacketFromServer;
        public RawPacketListener ReceivedTCPPacketFromClient;
        public RawPacketListener ReceivedTCPPacketFromServer;
        public RawPacketListener ReceivedRawGamePacketFromServer;
        public PacketListener ReceivedGamePacketFromClient;
        public PacketListener ReceivedGamePacketFromServer;
        public PacketListener SplitPacketFromServer;

        public PacketListener ReceivedAnimatedTextPacket;
        public PacketListener ReceivedBookOpenPacket;
        public PacketListener ReceivedCancelAutoWalkPacket;
        public PacketListener ReceivedChannelListPacket;
        public PacketListener ReceivedChannelOpenPacket;
        public PacketListener ReceivedChatMessagePacket;
        public PacketListener ReceivedContainerClosedPacket;
        public PacketListener ReceivedContainerItemAddPacket;
        public PacketListener ReceivedContainerItemRemovePacket;
        public PacketListener ReceivedContainerItemUpdatePacket;
        public PacketListener ReceivedContainerOpenedPacket;
        public PacketListener ReceivedCreatureHealthPacket;
        public PacketListener ReceivedCreatureLightPacket;
        public PacketListener ReceivedCreatureMovePacket;
        public PacketListener ReceivedCreatureOutfitPacket;
        public PacketListener ReceivedCreatureSpeedPacket;
        public PacketListener ReceivedCreatureSkullPacket;
        public PacketListener ReceivedCreatureSquarePacket;
        public PacketListener ReceivedEqItemAddPacket;
        public PacketListener ReceivedEqItemRemovePacket;
        public PacketListener ReceivedFlagUpdatePacket;
        public PacketListener ReceivedInformationBoxPacket;
        public PacketListener ReceivedMapItemAddPacket;
        public PacketListener ReceivedMapItemRemovePacket;
        public PacketListener ReceivedMapItemUpdatePacket;
        public PacketListener ReceivedNpcTradeListPacket;
        public PacketListener ReceivedNpcTradeGoldCountPacket;
        public PacketListener ReceivedPartyInvitePacket;
        public PacketListener ReceivedPrivateChannelOpenPacket;
        public PacketListener ReceivedProjectilePacket;
        public PacketListener ReceivedSkillUpdatePacket;
        public PacketListener ReceivedStatusMessagePacket;
        public PacketListener ReceivedStatusUpdatePacket;
        public PacketListener ReceivedTileAnimationPacket;
        public PacketListener ReceivedVipAddPacket;
        public PacketListener ReceivedVipLoginPacket;
        public PacketListener ReceivedVipLogoutPacket;
        public PacketListener ReceivedWorldLightPacket;
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
        #region socket core
        public RawSocket(Client client, bool Adler)
        {
            this.Adler = Adler;
            this.client=client;
            pid = client.Process.Id;
            string strIP = null;

            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                strIP = HosyEntry.AddressList[0].ToString();
                //Console.WriteLine("IP: " + strIP);
            }
            else{}
                //Console.WriteLine("No ip assigned");
            mainSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Raw, ProtocolType.IP);

            //Bind the socket to the selected IP address
            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(strIP), 0));

            //Set the socket  options
            mainSocket.SetSocketOption(SocketOptionLevel.IP,            //Applies only to IP packets
                                       SocketOptionName.HeaderIncluded, //Set the include the header
                                       true);

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
                if (log && bytesRead > 0)
                {
                    PreParse(receive_buf, bytesRead);
                }
                mainSocket.BeginReceive(receive_buf, 0, receive_buf.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), null);
            }
            catch (ObjectDisposedException)
            {
                if (OnError != null)
                OnError("Objected Disposed Exception");
            }
            catch (Exception ex)
            {
                if (OnError != null)
                OnError(ex.Message);
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
                            if(ReceivedIPPacketFromClient!=null)
                                ReceivedIPPacketFromClient((byte[])ipPacket.Clone(), String.Copy(flags));
                            if(ReceivedTCPPacketFromClient!=null)
                                ReceivedTCPPacketFromClient((byte[])tcpPacket.Clone(), String.Copy(flags));
                            ParseServerToClient(gameRawPacket, flags);
                        }
                        //packet from client
                        else if (sourcePort == localPort && destinationPort == remotePort)
                        {
                            if(ReceivedIPPacketFromClient!=null)
                                ReceivedIPPacketFromClient((byte[])ipPacket.Clone(), String.Copy(flags));
                            if(ReceivedTCPPacketFromClient!=null)
                                ReceivedTCPPacketFromClient((byte[])tcpPacket.Clone(), String.Copy(flags));
                            RaiseOutgoingEvents(gameRawPacket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if(OnError!=null)
                OnError(ex.Message);
            }
        }

        private void RaiseOutgoingEvents(byte[] packet)
        {
            if (ReceivedGamePacketFromClient != null)
                ReceivedGamePacketFromClient(new Packet(client, DecryptPacket(packet)));
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
                        byte[] packet= new byte[packetlength];
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
            if (IncomingGamePacketQueue.Count > 0)
            {

                byte[] original = IncomingGamePacketQueue.Dequeue();
                byte[] decrypted = DecryptPacket(original);

                int remaining = 0; // the bytes worth of logical packets left

                // Always call the default (if attached to)
                if (ReceivedGamePacketFromServer != null)
                    ReceivedGamePacketFromServer(new Packet(client,decrypted));

                // Is this a part of a larger packet?
                if (partialRemaining > 0)
                {

                    // Subtract from the remaining needed
                    partialRemaining -= decrypted.Length;
                }
                else
                {
                    // No, create a new partial packet
                    //partial = new PacketBuilder(client, decrypted);
                    remaining = BitConverter.ToInt16(decrypted,0);
                    partialRemaining = remaining - (decrypted.Length - 2); // packet length - part we already have
                }

                // Do we have a complete packet now?
                if (partialRemaining == 0)
                {
                    int length = 0;

                    // Keep going until no more logical packets
                    while (remaining > 0)
                    {
                        length = RaiseIncomingEvents(decrypted);

                        // If packet not found in database, skip the rest
                        if (length == -1)                        
                            break;
                        

                        length++;
                        //not tested yet
                        if (SplitPacketFromServer != null)
                            SplitPacketFromServer(new Packet(client, Packet.Repackage(decrypted, 2, length)));


                        // Subtract the amount that was parsed
                        remaining -= length;

                        // Repackage decrypted without the first logical packet
                        if (remaining > 0)
                            decrypted = Packet.Repackage(decrypted, length + 2);
                    }

                }

                if (IncomingGamePacketQueue.Count > 0)
                    ProcessIncomingGamePacketQueue();
            }
        }

        private int RaiseIncomingEvents(byte[] packet)
        {
            int length = -1;
            if (packet.Length < 3) return length;
            Packet p;
            PacketType type = (PacketType)packet[2];
            switch (type)
            {
                case PacketType.AnimatedText:
                    p = new AnimatedTextPacket(client, packet);
                    length = p.Index;
                    if (ReceivedAnimatedTextPacket != null)
                         ReceivedAnimatedTextPacket(p);
                    break;
                case PacketType.BookOpen:
                    p = new BookOpenPacket(client, packet);
                    length = p.Index;
                    if (ReceivedBookOpenPacket != null)
                         ReceivedBookOpenPacket(p);
                    break;
                case PacketType.CancelAutoWalk:
                    p = new CancelAutoWalkPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCancelAutoWalkPacket != null)
                         ReceivedCancelAutoWalkPacket(p);
                    break;
                case PacketType.ChannelList:
                    p = new ChannelListPacket(client, packet);
                    length = p.Index;
                    if (ReceivedChannelListPacket != null)
                         ReceivedChannelListPacket(p);
                    break;
                case PacketType.ChannelOpen:
                    p = new ChannelOpenPacket(client, packet);
                    length = p.Index;
                    if (ReceivedChannelOpenPacket != null)
                         ReceivedChannelOpenPacket(p);
                    break;
                case PacketType.ChatMessage:
                    p = new ChatMessagePacket(client, packet);
                    length = p.Index;
                    if (ReceivedChatMessagePacket != null)
                         ReceivedChatMessagePacket(p);
                    break;
                case PacketType.ContainerClosed:
                    p = new ContainerClosedPacket(client, packet);
                    length = p.Index;
                    if (ReceivedContainerClosedPacket != null)
                         ReceivedContainerClosedPacket(p);
                    break;
                case PacketType.ContainerItemAdd:
                    p = new ContainerItemAddPacket(client, packet);
                    length = p.Index;
                    if (ReceivedContainerItemAddPacket != null)
                         ReceivedContainerItemAddPacket(p);
                    break;
                case PacketType.ContainerItemRemove:
                    p = new ContainerItemRemovePacket(client, packet);
                    length = p.Index;
                    if (ReceivedContainerItemRemovePacket != null)
                         ReceivedContainerItemRemovePacket(p);
                    break;
                case PacketType.ContainerItemUpdate:
                    p = new ContainerItemUpdatePacket(client, packet);
                    length = p.Index;
                    if (ReceivedContainerItemUpdatePacket != null)
                         ReceivedContainerItemUpdatePacket(p);
                    break;
                case PacketType.ContainerOpened:
                    p = new ContainerOpenedPacket(client, packet);
                    length = p.Index;
                    if (ReceivedContainerOpenedPacket != null)
                         ReceivedContainerOpenedPacket(p);
                    break;
                case PacketType.CreatureHealth:
                    p = new CreatureHealthPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureHealthPacket != null)
                         ReceivedCreatureHealthPacket(p);
                    break;
                case PacketType.CreatureLight:
                    p = new CreatureLightPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureLightPacket != null)
                         ReceivedCreatureLightPacket(p);
                    break;
                case PacketType.CreatureMove:
                    p = new CreatureMovePacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureMovePacket != null)
                         ReceivedCreatureMovePacket(p);
                    break;
                case PacketType.CreatureOutfit:
                    p = new CreatureOutfitPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureOutfitPacket != null)
                         ReceivedCreatureOutfitPacket(p);
                    break;
                case PacketType.CreatureSkull:
                    p = new CreatureSkullPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureSkullPacket != null)
                         ReceivedCreatureSkullPacket(p);
                    break;
                case PacketType.CreatureSpeed:
                    p = new CreatureSpeedPacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureSpeedPacket != null)
                         ReceivedCreatureSpeedPacket(p);
                    break;
                case PacketType.CreatureSquare:
                    p = new CreatureSquarePacket(client, packet);
                    length = p.Index;
                    if (ReceivedCreatureSquarePacket != null)
                         ReceivedCreatureSquarePacket(p);
                    break;
                case PacketType.EqItemAdd:
                    p = new EqItemAddPacket(client, packet);
                    length = p.Index;
                    if (ReceivedEqItemAddPacket != null)
                         ReceivedEqItemAddPacket(p);
                    break;
                case PacketType.EqItemRemove:
                    p = new EqItemRemovePacket(client, packet);
                    length = p.Index;
                    if (ReceivedEqItemRemovePacket != null)
                         ReceivedEqItemRemovePacket(p);
                    break;
                case PacketType.FlagUpdate:
                    p = new FlagUpdatePacket(client, packet);
                    length = p.Index;
                    if (ReceivedFlagUpdatePacket != null)
                         ReceivedFlagUpdatePacket(p);
                    break;
                case PacketType.InformationBox:
                    p = new InformationBoxPacket(client, packet);
                    length = p.Index;
                    if (ReceivedInformationBoxPacket != null)
                         ReceivedInformationBoxPacket(p);
                    break;
                case PacketType.MapItemAdd:
                    p = new MapItemAddPacket(client, packet);
                    length = p.Index;
                    if (ReceivedMapItemAddPacket != null)
                         ReceivedMapItemAddPacket(p);
                    break;
                case PacketType.MapItemRemove:
                    p = new MapItemRemovePacket(client, packet);
                    length = p.Index;
                    if (ReceivedMapItemRemovePacket != null)
                         ReceivedMapItemRemovePacket(p);
                    break;
                case PacketType.MapItemUpdate:
                    p = new MapItemUpdatePacket(client, packet);
                    length = p.Index;
                    if (ReceivedMapItemUpdatePacket != null)
                         ReceivedMapItemUpdatePacket(p);
                    break;
                case PacketType.NpcTradeList:
                    p = new NpcTradeListPacket(client, packet);
                    length = p.Index;
                    if (ReceivedNpcTradeListPacket != null)
                         ReceivedNpcTradeListPacket(p);
                    break;
                case PacketType.NpcTradeGoldCountSaleList:
                    p = new NpcTradeGoldCountSaleListPacket(client, packet);
                    length = p.Index;
                    if (ReceivedNpcTradeGoldCountPacket != null)
                         ReceivedNpcTradeGoldCountPacket(p);
                    break;
                case PacketType.PartyInvite:
                    p = new PartyInvitePacket(client, packet);
                    length = p.Index;
                    if (ReceivedPartyInvitePacket != null)
                         ReceivedPartyInvitePacket(p);
                    break;
                case PacketType.PrivateChannelOpen:
                    p = new PrivateChannelOpenPacket(client, packet);
                    length = p.Index;
                    if (ReceivedPrivateChannelOpenPacket != null)
                         ReceivedPrivateChannelOpenPacket(p);
                    break;
                case PacketType.Projectile:
                    p = new ProjectilePacket(client, packet);
                    length = p.Index;
                    if (ReceivedProjectilePacket != null)
                         ReceivedProjectilePacket(p);
                    break;
                case PacketType.SkillUpdate:
                    p = new SkillUpdatePacket(client, packet);
                    if (ReceivedSkillUpdatePacket != null)
                         ReceivedSkillUpdatePacket(p);
                    break;
                case PacketType.StatusMessage:
                    p = new StatusMessagePacket(client, packet);
                    length = p.Index;
                    if (ReceivedStatusMessagePacket != null)
                         ReceivedStatusMessagePacket(p);
                    break;
                case PacketType.StatusUpdate:
                    p = new StatusUpdatePacket(client, packet, Adler);
                    length = p.Index;
                    if (ReceivedStatusUpdatePacket != null)
                         ReceivedStatusUpdatePacket(p);
                    break;
                case PacketType.TileAnimation:
                    p = new TileAnimationPacket(client, packet);
                    length = p.Index;
                    if (ReceivedTileAnimationPacket != null)
                         ReceivedTileAnimationPacket(p);
                    break;
                case PacketType.VipAdd:
                    p = new VipAddPacket(client, packet);
                    length = p.Index;
                    if (ReceivedVipAddPacket != null)
                         ReceivedVipAddPacket(p);
                    break;
                case PacketType.VipLogin:
                    p = new VipLoginPacket(client, packet);
                    length = p.Index;
                    if (ReceivedVipLoginPacket != null)
                         ReceivedVipLoginPacket(p);
                    break;
                case PacketType.VipLogout:
                    p = new VipLogoutPacket(client, packet);
                    length = p.Index;
                    if (ReceivedVipLogoutPacket != null)
                         ReceivedVipLogoutPacket(p);
                    break;
                case PacketType.WorldLight:
                    p = new WorldLightPacket(client, packet);
                    length = p.Index;
                    if (ReceivedWorldLightPacket != null)
                         ReceivedWorldLightPacket(p);
                    break;
            }
            return length;
        }
        #endregion
        #region properties
        public bool Enabled
        {
            get { return log; }
            set { log = value; }
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
        #region ports, flags and encryption/decryption

        public void UpdatePorts()
        {
            MIB_TCPROW_OWNER_PID[] tcptable = GetAllTcpConnections();
            switch (mode)
            {
                case SocketMode.UsingDefaultRemotePort:                
                    for (int i = 0; i < tcptable.Length; i++)
                    {
                        ushort remote_ = BitConverter.ToUInt16(new byte[2] { tcptable[i].remotePort2,tcptable[i].remotePort1}, 0);
                        if (remote_ == 7171 && tcptable[i].owningPid == pid)
                        {
                            remotePort = 7171;
                            localPort = BitConverter.ToUInt16(new byte[2] { tcptable[i].localPort2, tcptable[i].localPort1}, 0);
                            return;
                        }
                    }
                    break;
                case SocketMode.UsingSpecialRemotePort:
                    for (int i = 0; i < tcptable.Length; i++)
                    {
                        if (tcptable[i].owningPid == pid)
                        {
                            remotePort = BitConverter.ToUInt16(new byte[2] { tcptable[i].remotePort2,tcptable[i].remotePort1}, 0);
                            localPort= BitConverter.ToUInt16(new byte[2] { tcptable[i].localPort2, tcptable[i].localPort1 }, 0);
                            return;
                        }
                    }
                    localPort=0;
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
                

        public byte[] DecryptPacket(byte[] packet)
        {
            xtea=client.ReadBytes(Tibia.Addresses.Client.XTeaKey, 16);
            return XTEA.Decrypt(packet,xtea , Adler);
        }

        public byte[] EncryptPacket(byte[] packet)
        {
            xtea = client.ReadBytes(Tibia.Addresses.Client.XTeaKey, 16);
            return XTEA.Encrypt(packet, xtea, Adler);
        }
        #endregion


    }
     * */
}



    
