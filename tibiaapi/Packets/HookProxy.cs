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
    public class HookProxy : ProxyBase
    {
        private Client client;

        private Protocol protocol;

        private NetworkMessage serverRecvMsg;
        private NetworkMessage serverSendMsg;
        private NetworkMessage clientRecvMsg;
        private NetworkMessage clientSendMsg;

        private int fullPacketSize;
        private int partialPacketSize;

        public HookProxy(Client client)
        {
            this.client = client;
            serverRecvMsg = new NetworkMessage(client);
            serverSendMsg = new NetworkMessage(client);
            clientRecvMsg = new NetworkMessage(client);
            clientSendMsg = new NetworkMessage(client);
            
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            client.Dll.Pipe.OnSocketRecv += new Pipe.PipeListener(Pipe_OnSocketRecv);
            client.Dll.Pipe.OnSocketSend += new Pipe.PipeListener(Pipe_OnSocketSend);
            client.Dll.Pipe.OnPacketRecv += new Pipe.PipeListener(Pipe_OnPacketRecv);

            if (client.LoggedIn)
            {
                protocol = Protocol.World;
            }
            else
            {
                protocol = Protocol.None;
            }
        }

        private void Pipe_OnPacketRecv(Tibia.Packets.NetworkMessage msg)
        {
            int length = msg.GetUInt16();
            if (msg.GetByte() == (byte)Packets.PipePacketType.OnGetNextPacket)
            {
                byte[] buf = new byte[msg.Data.Length - 3];
                Array.Copy(msg.Data, 3, buf, 0, buf.Length);
                try
                {
                    ParsePacketFromServer(client, new NetworkMessage(client, buf), clientSendMsg);
                }
                catch (Exception ex)
                {
                    WriteDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
                }
            }
        }

        private void Pipe_OnSocketRecv(Tibia.Packets.NetworkMessage msg)
        {
            int length = msg.GetUInt16();
            if (msg.GetByte() == (byte)Packets.PipePacketType.HookReceivedPacket)
            {
                byte[] buf = new byte[msg.Data.Length - 3];
                Array.Copy(msg.Data, 3, buf, 0, buf.Length);
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

            if (length > buffer.Length)
            {
                // Packet is split into multiple chunks
                if (partialPacketSize == 0)
                {
                    // This is the first chunk
                    fullPacketSize = length;
                    Array.Copy(buffer, serverRecvMsg.GetBuffer(), buffer.Length);
                    partialPacketSize = buffer.Length;
                    return;
                }
                else
                {
                    // This is a subsequent chunk
                    Array.Copy(buffer, 0, serverRecvMsg.GetBuffer(), partialPacketSize, buffer.Length);
                    partialPacketSize += buffer.Length;

                    if (partialPacketSize < fullPacketSize)
                    {
                        // Packet is still incomplete
                        return;
                    }
                    else
                    {
                        // Packet is complete
                        serverRecvMsg.Length = fullPacketSize;
                    }
                }
            }
            else
            {
                fullPacketSize = 0;
                partialPacketSize = 0;
                Array.Copy(buffer, serverRecvMsg.GetBuffer(), length);
                serverRecvMsg.Length = length;
            }

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
                            bool parsed = false;

                            try
                            {
                                parsed = ParsePacketFromServer(client, serverRecvMsg, clientSendMsg);
                            }
                            catch (Exception ex)
                            {
                                WriteDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
                            }

                            if (!parsed)
                            {
                                if (serverRecvMsg.Length - serverRecvMsg.Position > 0 &&
                                    serverRecvMsg.Length < serverRecvMsg.GetBuffer().Length)
                                {
                                    byte[] unknown = serverRecvMsg.GetBytes(serverRecvMsg.Length - serverRecvMsg.Position);
                                    OnSplitPacketFromServer(unknown[0], unknown);
                                }
                                break;
                            }

                            if (serverRecvMsg.Position - position > 0)
                            {
                                byte[] data = new byte[serverRecvMsg.Position - position];
                                Array.Copy(serverRecvMsg.GetBuffer(), position, data, 0, data.Length);

                                OnSplitPacketFromServer(data[0], data);
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
            int length = buffer.Length;

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

                            OnSplitPacketFromClient(unknown[0], unknown);
                        }

                        byte[] data = new byte[clientRecvMsg.Position - position];
                        Array.Copy(clientRecvMsg.GetBuffer(), position, data, 0, data.Length);
                        OnSplitPacketFromClient(data[0], data);
                    }
                    break;
            }
        }

        public void SendToServer(byte[] packet)
        {
            Pipes.HookSendToServerPacket.Send(client, packet);
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
            catch (Exception ex)
            {
                WriteDebug(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
        }
    }
}
