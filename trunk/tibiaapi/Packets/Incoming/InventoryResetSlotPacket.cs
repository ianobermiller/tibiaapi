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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.InventoryResetSlot)
                return false;

            Destination = destination;
            Type = IncomingPacketType.InventoryResetSlot;

            try
            {
                Slot = msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Slot);
        }
    }
}