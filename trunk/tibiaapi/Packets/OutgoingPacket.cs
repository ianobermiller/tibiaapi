using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Util;
using System.Runtime.InteropServices;

namespace Tibia.Packets
{
    public class OutgoingPacket
    {
        public OutgoingPacket(Objects.Client c)
        {
            Client = c;
            Forward = true;
        }

        public bool Forward { get; set; }
        public PacketDestination Destination { get; set; }
        public OutgoingPacketType Type { get; set; }
        public Objects.Client Client { get; set; }

        public virtual byte[] ToByteArray() { return null; }

        public bool Send() 
        {
            if (Client.UsingProxy)
            {
                NetworkMessage msg = new NetworkMessage();
                msg.AddBytes(ToByteArray());
                msg.InsetLogicalPacketHeader();
                msg.PrepareToSend();

                if (Destination == PacketDestination.Client)
                    Client.Proxy.SendToClient(msg);
                else if (Destination == PacketDestination.Server)
                    Client.Proxy.SendToServer(msg);
                else
                    return false;

                return true;

            }
            else if (Destination == PacketDestination.Server)
            {
                // send with dll.

                byte[] packet = ToByteArray();
                byte[] sendPacket = new byte[packet.Length + 2];
                Array.Copy(packet, 0, sendPacket, 2, packet.Length);
                Array.Copy(BitConverter.GetBytes((ushort)packet.Length), sendPacket, 2);

                return SendPacketWithDLL(Client, sendPacket);
            }

            return false;
        }

        #region Sending Packets with packet.dll
        [DllImport("packet.dll")]
        private static extern bool SendPacket(uint processID, byte[] packet);

        /// <summary>
        /// Send a packet through the client using packet.dll.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketWithDLL(Objects.Client client, Byte[] packet)
        {
            try
            {
                return SendPacket((uint)client.Process.Id, packet);
            }
            catch (DllNotFoundException)
            {
                throw new Exceptions.PacketDllNotFoundException();
            }
            catch (AccessViolationException)
            {
                return true;
            }
        }
        #endregion

        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos) { return false; }
    }
}
