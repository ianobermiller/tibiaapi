using System;
using Tibia.Constants;
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

        private object msgLock = new object();
        private NetworkMessage msg;

        public Packet(Objects.Client c)
        {
            Client = c;
            Forward = true;
        }

        public virtual void ToNetworkMessage(NetworkMessage msg)
        {
            throw new System.NotImplementedException();
        }

        public bool Send()
        {

            if (Client.IO.UsingProxy)
            {
                return Send(SendMethod.Proxy);
            }
            else if (Client.Dll.Pipe != null && Client.Dll.Pipe.Connected && Destination != PacketDestination.Client)
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
                        ToNetworkMessage(msg);

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
                        ToNetworkMessage(msg);

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
                        ToNetworkMessage(msg);
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

                uint bufferSize = (uint)(4 + packet.Length);
                byte[] readyPacket = new byte[bufferSize];
                Array.Copy(BitConverter.GetBytes(packet.Length), readyPacket, 4);
                Array.Copy(packet, 0, readyPacket, 4, packet.Length);

                IntPtr pRemote = WinApi.VirtualAllocEx(client.ProcessHandle, IntPtr.Zero, /*bufferSize*/
                    bufferSize,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);

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

        public static bool SendPacketToClientByMemory(Objects.Client client, byte[] packet)
        {
            if (client.LoggedIn)
            {
                bool result = true;

                if (!client.IO.IsSendToClientCodeWritten)
                    if (!client.IO.WriteOnGetNextPacketCode()) return false;

                var myPacketAddress = WinApi.VirtualAllocEx(client.ProcessHandle,
                                                            IntPtr.Zero,
                                                            (uint)packet.Length,
                                                            WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                                                            WinApi.MemoryProtection.ExecuteReadWrite);
                if (myPacketAddress == IntPtr.Zero)
                    return false;

                result &= client.Memory.WriteBytes(myPacketAddress.ToInt64(), packet, (uint)packet.Length);
                result &= client.Memory.WriteUInt32(client.IO.MyStreamAddress.ToInt64(), (uint)myPacketAddress);
                result &= client.Memory.WriteUInt32(client.IO.MyStreamAddress.ToInt64() + 4, (uint)packet.Length);
                result &= client.Memory.WriteUInt32(client.IO.MyStreamAddress.ToInt64() + 8, 0);

                if (result)
                {
                    IntPtr threadHandle = Tibia.Util.WinApi.CreateRemoteThread(client.ProcessHandle, IntPtr.Zero, 0,
                        client.IO.SendToClientAddress, IntPtr.Zero, 0, IntPtr.Zero);
                    Tibia.Util.WinApi.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                    Tibia.Util.WinApi.CloseHandle(threadHandle);
                    result = true;
                }


                WinApi.VirtualFreeEx(client.ProcessHandle, myPacketAddress, (uint)packet.Length, WinApi.AllocationType.Release);
                return result;
            }
            else return false;
        }
        #endregion
        
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
