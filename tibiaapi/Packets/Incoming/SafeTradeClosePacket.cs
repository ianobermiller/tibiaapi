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
            Type = IncomingPacketType_t.SAFE_TRADE_CLOSE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.SAFE_TRADE_CLOSE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.SAFE_TRADE_CLOSE;

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