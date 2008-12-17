using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ContainerAddItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public Objects.Item Item { get; set; }

        public ContainerAddItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CONTAINER_ADD_ITEM;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CONTAINER_ADD_ITEM)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CONTAINER_ADD_ITEM;

            Container = msg.GetByte();
            Item = new Tibia.Objects.Item(Client, msg.GetUInt16(), 0);

            if (Item.HasExtraByte)
                Item.Count = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte(Container);
            msg.AddUInt16((ushort)Item.Id);

            if (Item.HasExtraByte)
                msg.AddByte(Item.Count);

            return msg.Packet;
        }
    }
}