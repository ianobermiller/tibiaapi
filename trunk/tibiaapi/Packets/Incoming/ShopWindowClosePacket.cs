using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ShopWindowClosePacket : IncomingPacket
    {

        public ShopWindowClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ShopWindowClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ShopWindowClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ShopWindowClose;

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