using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class SafeTradeClosePacket : IncomingPacket
    {

        public SafeTradeClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SafeTradeClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.SafeTradeClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SafeTradeClose;

            //no data


            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            return msg.Packet;
        }
    }
}