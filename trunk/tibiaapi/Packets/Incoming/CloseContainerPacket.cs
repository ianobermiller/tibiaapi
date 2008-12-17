using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CloseContainerPacket : IncomingPacket
    {
        public byte Id { get; set; }

        public CloseContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CLOSE_CONTAINER;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CLOSE_CONTAINER)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CLOSE_CONTAINER;
            Id = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte(Id);

            return msg.Packet;
        }
    }
}