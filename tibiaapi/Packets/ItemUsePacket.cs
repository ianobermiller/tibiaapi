using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ItemUsePacket : Packet
    {
        ItemLocation loc;
        int itemId;
        byte stackPos;
        byte containerIndex;

        public ItemLocation Location
        {
            get { return loc; }
        }

        public int ItemId
        {
            get { return itemId; }
        }

        public byte StackPos
        {
            get { return stackPos; }
        }

        public byte ContainerIndex
        {
            get { return containerIndex; }
        }

        public ItemUsePacket()
        {
            type = PacketType.ItemUse;
            destination = PacketDestination.Server;
        }

        public ItemUsePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemUse) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                loc = p.GetItemLocation();
                itemId = p.GetInt();
                stackPos = p.GetByte();
                containerIndex = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ItemUsePacket Create(ItemLocation loc, uint itemId, byte stackPos, byte containerIndex)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ItemUse);
            p.AddItemLocation(loc);
            p.AddInt((int)itemId);
            p.AddByte(stackPos);
            p.AddByte(containerIndex);
            return new ItemUsePacket(p.GetPacket());
        }
    }
}
