using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tibia;
using Tibia.Packets;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class HookProxy : Tibia.Util.SocketBase
    {
        private enum Protocol { None, Login, World }

        private Client client;

        private Protocol protocol;

        private uint[] xteaKey;
        private byte lastRecvPacketType;

        private NetworkMessage serverRecvMsg;
        private NetworkMessage serverSendMsg;
        private NetworkMessage clientRecvMsg;
        private NetworkMessage clientSendMsg;

        public delegate void SplitPacket(byte type, byte[] data);

        public event SplitPacket SplitPacketFromServer;
        public event SplitPacket SplitPacketFromClient;


        public HookProxy(Client client)
        {
            this.client = client;
            serverRecvMsg = new NetworkMessage(client);
            serverSendMsg = new NetworkMessage(client);
            clientRecvMsg = new NetworkMessage(client);
            clientSendMsg = new NetworkMessage(client);

            client.Dll.Pipe.OnSocketRecv += new Pipe.PipeListener(Pipe_OnSocketRecv);
            client.Dll.Pipe.OnSocketSend += new Pipe.PipeListener(Pipe_OnSocketSend);
            if (client.LoggedIn)
            {
                protocol = Protocol.World;
                xteaKey = client.IO.XteaKey;
            }
            else
            {
                protocol = Protocol.None;
            }
        }

        private void Pipe_OnSocketRecv(Tibia.Packets.NetworkMessage msg)
        {
            int length = msg.GetUInt16();
            if (msg.GetByte() == (byte)Packets.PipePacketType.HookReceivedPacket)
            {
                byte[] buf=new byte[msg.Data.Length-3];
                Array.Copy(msg.Data,3,buf,0,buf.Length);
                ProcessFromServer(buf);
            }
        }

        private void Pipe_OnSocketSend(Tibia.Packets.NetworkMessage msg)
        {
            int length = msg.GetUInt16();
            if (msg.GetByte() == (byte)Packets.PipePacketType.HookSentPacket)
            {
                byte[] buf = new byte[msg.Data.Length - 3];
                Array.Copy(msg.Data, 3, buf, 0, buf.Length);
                ProcessFromClient(buf);
            }
        }

        public void ProcessFromServer(byte[] buffer)
        {
            int length = (int)BitConverter.ToUInt16(buffer, 0) + 2;

            Array.Copy(buffer, serverRecvMsg.GetBuffer(), length);
            serverRecvMsg.Length = length;

            OnReceivedDataFromServer(serverRecvMsg.Data);

            switch (protocol)
            {
                case Protocol.Login:
                    break;
                case Protocol.World:
                    bool adlerOkay = serverRecvMsg.CheckAdler32();
                    bool decryptOkay = serverRecvMsg.XteaDecrypt(client.IO.XteaKey);
                    if (adlerOkay && decryptOkay)
                    {
                        serverRecvMsg.Position = 6;
                        int msgSize = (int)serverRecvMsg.GetUInt16() + 8;
                        clientSendMsg.Reset();

                        while (serverRecvMsg.Position < msgSize)
                        {
                            int position = serverRecvMsg.Position;

                            if (!ParsePacketFromServer(client, serverRecvMsg, clientSendMsg))
                            {
                                byte[] unknown = serverRecvMsg.GetBytes(serverRecvMsg.Length - serverRecvMsg.Position);

                                if (SplitPacketFromServer != null)
                                    SplitPacketFromServer.Invoke(unknown[0], unknown);
                                break;
                            }

                            lastRecvPacketType = serverRecvMsg.GetBuffer()[position];

                            if (SplitPacketFromServer != null)
                            {
                                byte[] data = new byte[serverRecvMsg.Position - position];
                                Array.Copy(serverRecvMsg.GetBuffer(), position, data, 0, data.Length);

                                SplitPacketFromServer.Invoke(data[0], data);
                            }
                        }
                    }
                    break;
                case Protocol.None:
                    break;
            }
        }

        public void ProcessFromClient(byte[] buffer)
        {
            int length = (int)BitConverter.ToUInt16(buffer, 0) + 2;

            Array.Copy(buffer, clientRecvMsg.GetBuffer(), length);
            clientRecvMsg.Length = length;

            OnReceivedDataFromClient(clientRecvMsg.Data);

            switch (protocol)
            {
                case Protocol.None:
                case Protocol.Login:
                    ParseFirstClientMsg();
                    break;
                case Protocol.World:
                    bool adlerOkay = clientRecvMsg.CheckAdler32();
                    bool decryptOkay = clientRecvMsg.XteaDecrypt(client.IO.XteaKey);
                    if (adlerOkay && decryptOkay)
                    {

                        clientRecvMsg.Position = 6;
                        int msgLength = (int)clientRecvMsg.GetUInt16() + 8;


                        int position = clientRecvMsg.Position;

                        if (!ParsePacketFromClient(client, clientRecvMsg, serverSendMsg))
                        {
                            //unknown packet
                            byte[] unknown = clientRecvMsg.GetBytes(clientRecvMsg.Length - clientRecvMsg.Position);

                            if (SplitPacketFromClient != null)
                                SplitPacketFromClient.Invoke(unknown[0], unknown);
                        }

                        if (SplitPacketFromClient != null)
                        {
                            byte[] data = new byte[clientRecvMsg.Position - position];
                            Array.Copy(clientRecvMsg.GetBuffer(), position, data, 0, data.Length);
                            SplitPacketFromClient.Invoke(data[0], data);
                        }
                    }
                    break;
            }
        }

        private void ParseFirstClientMsg()
        {
            try
            {
                clientRecvMsg.Position = 6;
                byte protocolId = clientRecvMsg.GetByte();

                switch (protocolId)
                {
                    case 0x01:
                        protocol = Protocol.Login;
                        break;

                    case 0x0A:
                        protocol = Protocol.World;
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }
    }
}
