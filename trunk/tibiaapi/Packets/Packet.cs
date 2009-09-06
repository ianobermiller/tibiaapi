using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Tibia.Util;

namespace Tibia.Packets
{
    public class Packet
    {
        public enum SendMethod { Proxy, HookProxy, Memory }
        public uint PacketId { get; set; }
        public bool Forward { get; set; }
        public PacketDestination Destination { get; set; }
        public Objects.Client Client { get; set; }

        private static object msgLock = new object();
        private static NetworkMessage msg;

        public Packet(Objects.Client c)
        {
            Client = c;
            Forward = true;
        }

        public virtual void ToNetworkMessage(ref NetworkMessage msg)
        {
            throw new Exception("ToNetworkMessage not implemented.");
        }

        public bool Send()
        {
            
            if (Client.IO.UsingProxy)
            {
                return Send(SendMethod.Proxy);
            }
            else if (Client.Dll.Pipe != null && Client.Dll.Pipe.Connected && Destination!=PacketDestination.Client)
            {
                return Send(SendMethod.HookProxy);
            }
            else
            {
                return Send(SendMethod.Memory);
            }
        }

        public bool Send(SendMethod method) 
        {
            if (msg == null)
                msg = new NetworkMessage(Client, 4048);

            switch (method)
            {
                
                case SendMethod.Proxy:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(ref msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            if (Destination == PacketDestination.Client)
                                Client.IO.Proxy.SendToClient(msg.Data);
                            else if (Destination == PacketDestination.Server)
                                Client.IO.Proxy.SendToServer(msg.Data);

                            return true;
                        }
                    }
                    break;
                case SendMethod.HookProxy:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(ref msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            Pipes.HookSendToServerPacket.Send(Client, msg.Data);

                            return true;
                        }
                    }
                    break;
                case SendMethod.Memory:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(ref msg);
                        if (Destination == PacketDestination.Server)
                        {
                            if (msg.Length > 8)
                            {
                                msg.InsetLogicalPacketHeader();
                                msg.PrepareToSend();

                                return SendPacketToServerByMemory(Client, msg.Data);
                            }
                        }
                        else if (Destination == PacketDestination.Client)
                        {
                            byte[] data = new byte[msg.Data.Length - 8];
                            Array.Copy(msg.Data, 8, data, 0, data.Length);
                            SendPacketToClientByMemory(Client, data);
                        }
                    }
                    break;
            }

            return false;
        }

        #region Sending Packets with Stepler's Method
        // (http://www.tpforums.org/forum/showthread.php?t=2832)
        /// <summary>
        /// Send a packet through the client by writing some code in memory and running it.
        /// The packet must not contain any header(no length nor Adler checksum) and be unencrypted
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketToServerByMemory(Objects.Client client, byte[] packet)
        {
            if (client.LoggedIn)
            {
                if (!client.IO.IsSendToServerCodeWritten)
                    if (!client.IO.WriteSocketSendCode()) return false;

                uint bufferSize = (uint)(4 + msg.Length);
                byte[] readyPacket = new byte[bufferSize];
                Array.Copy(BitConverter.GetBytes(msg.Length), readyPacket, 4);
                Array.Copy(packet, 0, readyPacket, 4, packet.Length);

                IntPtr pRemote = Tibia.Util.WinApi.VirtualAllocEx(client.ProcessHandle, IntPtr.Zero, /*bufferSize*/
                                            bufferSize,
                                            Tibia.Util.WinApi.MEM_COMMIT | Tibia.Util.WinApi.MEM_RESERVE,
                                            Tibia.Util.WinApi.PAGE_EXECUTE_READWRITE);

                if (pRemote != IntPtr.Zero)
                {
                    if (client.Memory.WriteBytes(pRemote.ToInt64(), readyPacket, bufferSize))
                    {
                        IntPtr threadHandle = Tibia.Util.WinApi.CreateRemoteThread(client.ProcessHandle, IntPtr.Zero, 0,
                            client.IO.SendToServerAddress, pRemote, 0, IntPtr.Zero);
                        Tibia.Util.WinApi.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                        Tibia.Util.WinApi.CloseHandle(threadHandle);
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }
        #endregion

        public bool SendPacketToClientByMemory(Objects.Client client, byte[] packet)
        {
            bool ret = false;
            if (client.LoggedIn)
            {
                if (!client.IO.IsSendToClientCodeWritten)
                    if (!client.IO.WriteOnGetNextPacketCode()) return false;

                byte[] originalStream = client.Memory.ReadBytes(Tibia.Addresses.Client.RecvStream, 12);
                
                IntPtr myStreamAddress = WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)packet.Length,
                    Tibia.Util.WinApi.MEM_COMMIT | Tibia.Util.WinApi.MEM_RESERVE,
                    Tibia.Util.WinApi.PAGE_EXECUTE_READWRITE);
                if (myStreamAddress != IntPtr.Zero )
                {

                    if (client.Memory.WriteBytes(
                        myStreamAddress.ToInt64(),
                        packet,
                        (uint)packet.Length))
                    {
                        byte[] myStream = new byte[12];
                        Array.Copy(BitConverter.GetBytes(myStreamAddress.ToInt32()), myStream, 4);
                        Array.Copy(BitConverter.GetBytes(packet.Length), 0, myStream, 4, 4);

                        if (client.Memory.WriteBytes(Tibia.Addresses.Client.RecvStream,
                            myStream, 12))
                        {
                            if (client.Memory.WriteByte(client.IO.SendToClientAddress.ToInt64(), 0x1))
                            {

                                IntPtr threadHandle = WinApi.CreateRemoteThread(
                                                            client.ProcessHandle,
                                                            IntPtr.Zero,
                                                            0,
                                                            new IntPtr(Tibia.Addresses.Client.ParserFunc),
                                                            IntPtr.Zero,
                                                            0,
                                                            IntPtr.Zero);
                                WinApi.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                                WinApi.CloseHandle(threadHandle);

                                ret = true;
                                client.Memory.WriteByte(client.IO.SendToClientAddress.ToInt64(), 0x0);

                            }

                            client.Memory.WriteBytes(Tibia.Addresses.Client.RecvStream,
                            originalStream, 12);
                        }

                    }
                }
                if (myStreamAddress != IntPtr.Zero)
                    WinApi.VirtualFreeEx(client.ProcessHandle,
                                        myStreamAddress,
                                        12,
                                        WinApi.MEM_RELEASE);
            }
            return ret;
        }



        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        { 
            return false;
        }

        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            return false;
        }
    }
}
