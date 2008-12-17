using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class InventoryResetSlotPacket : IncomingPacket
    {
        public byte Slot { get; set; }

        public InventoryResetSlotPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.INVENTORY_RESET_SLOT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.INVENTORY_RESET_SLOT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.INVENTORY_RESET_SLOT;
            Slot = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte(Slot);

            return msg.Packet;
        }
    }
}