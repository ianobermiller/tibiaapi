using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class UpArrowContainerPacket : OutgoingPacket
    {
        public byte Id { get; set; }

        public UpArrowContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.UP_ARROW_CONTAINER;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.UP_ARROW_CONTAINER)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.UP_ARROW_CONTAINER;

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

        public static bool Send(Objects.Client client, byte id)
        {
            UpArrowContainerPacket p = new UpArrowContainerPacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}