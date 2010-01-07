using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class InventorySetSlotPacket : IncomingPacket
    {
        public byte Slot { get; set; }
        public Objects.Item Item { get; set; }

        public InventorySetSlotPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.InventorySetSlot;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.InventorySetSlot)
                return false;

            Destination = destination;
            Type = IncomingPacketType.InventorySetSlot;

            Slot = msg.GetByte();

            Item = msg.GetItem();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Slot);

            msg.AddItem(Item);
        }

        public static bool Send(Objects.Client client, byte slot, Objects.Item item)
        {
            InventorySetSlotPacket p = new InventorySetSlotPacket(client);
            p.Slot = slot;
            p.Item = item;
            return p.Send();
        }
    }
}