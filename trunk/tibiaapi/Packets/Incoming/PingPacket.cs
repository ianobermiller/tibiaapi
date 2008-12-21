using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class PingPacket : IncomingPacket
    {

        public PingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Ping;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Ping)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Ping;

            //no data


            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            return msg.Packet;
        }
    }
}