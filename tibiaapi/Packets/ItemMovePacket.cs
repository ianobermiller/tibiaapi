using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ItemMovePacket : Packet
    {
        ItemLocation from;
        int fromItemId;
        byte fromStackPos;
        ItemLocation to;
        byte count;

        public ItemLocation From
        {
            get { return from; }
        }

        public int FromItemId
        {
            get { return fromItemId; }
        }

        public byte FromStackPos
        {
            get { return fromStackPos; }
        }

        public ItemLocation To
        {
            get { return to; }
        }

        public byte Count
        {
            get { return count; }
        }

        public ItemMovePacket()
        {
            type = PacketType.ItemMove;
            destination = PacketDestination.Server;
        }

        public ItemMovePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemMove) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);

                from = p.GetItemLocation();
                fromItemId = p.GetInt();
                fromStackPos = p.GetByte();
                to = p.GetItemLocation();
                count = p.GetByte();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ItemMovePacket Create(ItemLocation from, uint fromItemId, byte fromStackPos,
            ItemLocation to, byte count)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ItemMove);
            p.AddItemLocation(from);
            p.AddInt((int)fromItemId);
            p.AddByte(fromStackPos);
            p.AddItemLocation(to);
            p.AddByte(count);
            return new ItemMovePacket(p.GetPacket());
        }
    }
}
