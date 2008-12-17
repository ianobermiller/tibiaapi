using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class InventorySetSlotPacket : IncomingPacket
    {
        public byte Slot { get; set; }
        public Objects.Item Item { get; set; }

        public InventorySetSlotPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.INVENTORY_SET_SLOT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.INVENTORY_SET_SLOT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.INVENTORY_SET_SLOT;
            Slot = msg.GetByte();

            Item = new Tibia.Objects.Item(Client, msg.GetUInt16(), 0);

            if (Item.HasExtraByte)
                Item.Count = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte(Slot);

            msg.AddUInt16((ushort)Item.Id);

            if (Item.HasExtraByte)
                msg.AddByte(Item.Count);

            return msg.Packet;
        }
    }
}