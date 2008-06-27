using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ContainerItemUpdatePacket : Packet
    {
        private byte container;
        private byte slot;
        private Item item;
        public byte Container
        {
            get { return container; }
        }
        public byte Slot
        {
            get { return slot; }
        }
        public Item Item
        {
            get { return item; }
        }
        public ContainerItemUpdatePacket(Client c)
            : base(c)
        {
            type = PacketType.ContainerItemUpdate;
            destination = PacketDestination.Client;
        }
        public ContainerItemUpdatePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerItemUpdate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                container = p.GetByte();
                slot = p.GetByte();
                item = p.GetItem();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ContainerItemUpdatePacket Create(Client c, byte container, byte slot, Item item)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ContainerItemUpdate);
            p.AddByte(container);
            p.AddByte(slot);
            p.AddItem(item);
            return new ContainerItemUpdatePacket(c, p.GetPacket());
        }
    }
}