using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ContainerRemoveItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public byte Slot { get; set; }

        public ContainerRemoveItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CONTAINER_REMOVE_ITEM;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CONTAINER_REMOVE_ITEM)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CONTAINER_REMOVE_ITEM;

            Container = msg.GetByte();
            Slot = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte(Container);
            msg.AddByte(Slot);

            return msg.Packet;
        }
    }
}