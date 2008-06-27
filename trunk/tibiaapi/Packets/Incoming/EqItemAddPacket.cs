using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class EqItemAddPacket: Packet
    {
        private Item item;
        private Constants.SlotNumber slot;
        public Item Item
        {
            get { return item; }
        }
        public Constants.SlotNumber Slot
        {
            get { return slot; }
        }
        public EqItemAddPacket(Client c)
            : base(c)
        {
            type = PacketType.EqItemAdd;
            destination = PacketDestination.Client;
        }
        public EqItemAddPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.EqItemAdd) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                slot = (Tibia.Constants.SlotNumber)p.GetByte();
                item = p.GetItem();
                item.Loc = new ItemLocation(slot);
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static EqItemAddPacket Create(Client c, int id, Tibia.Constants.SlotNumber slot)
        {
            Util.DatReader dat = c.DatReader;
            DatItem i = dat.GetItem((uint)id);
            if (i.GetFlag(Tibia.Addresses.DatItem.Flag.IsStackable))
            {
                return Create(c, new Item((uint)id, 1, new ItemLocation(slot), c, true), slot);
            }
            else
            {

                return Create(c, new Item((uint)id, 0, new ItemLocation(slot), c, true), slot);
            }
        }
        public static EqItemAddPacket Create(Client c, Item i, Tibia.Constants.SlotNumber slot)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.EqItemAdd);
            p.AddByte((byte)slot);
            p.AddItem(i);
            return new EqItemAddPacket(c, p.GetPacket());
        }
    }
}
