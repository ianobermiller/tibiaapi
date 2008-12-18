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
            Type = IncomingPacketType.InventoryResetSlot;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.InventoryResetSlot)
                return false;

            Destination = destination;
            Type = IncomingPacketType.InventoryResetSlot;
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