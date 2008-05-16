using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class EqItemAddPacket: Packet
    {
        private Item item;
        private ItemLocation loc;
        public Item Item
        {
            get { return item; }
        }
        public ItemLocation Loc
        {
            get { return loc; }
        }
        public EqItemAddPacket()
        {
            type = PacketType.EqItemAdd;
            destination = PacketDestination.Client;
        }
        public EqItemAddPacket(byte[] data, Util.DatReader dat)
            : this()
        {
            ParseData(data,dat);
        }
        public new bool ParseData(byte[] packet, Util.DatReader dat)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.EqItemAdd) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                loc = new ItemLocation((Tibia.Constants.SlotNumber)p.GetByte());
                item = p.GetItem(dat);
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static EqItemAddPacket Create(int id, Tibia.Constants.SlotNumber slot,Client client)
        {
            Util.DatReader dat = new Tibia.Util.DatReader(client);
            DatItem i = dat.GetItem((uint)id);
            if (i.GetFlag(Tibia.Addresses.DatItem.Flag.IsStackable))
            {
                return Create(new Item((uint)id, 1, new ItemLocation(slot), client, true), slot, client);
            }
            else
            {

                return Create(new Item((uint)id, 0, new ItemLocation(slot), client, true), slot, client);
            }
        }
        public static EqItemAddPacket Create(Item i, Tibia.Constants.SlotNumber slot, Client client)
        {
            PacketBuilder p = new PacketBuilder(PacketType.EqItemAdd);
            p.AddByte((byte)slot);
            p.AddItem(i);
            return new EqItemAddPacket(p.GetPacket(),new Util.DatReader(client));
        }
    }
}
