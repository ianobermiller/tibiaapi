using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ItemUseOnPacket : Packet
    {
        ItemLocation from;
        int fromItemId;
        byte fromStackPos;
        Location toLoc = Location.GetInvalid();
        ItemLocation toItemLoc = null;
        int toItemId;
        byte toStackPos;

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

        public Location ToLoc
        {
            get { return ToLoc; }
        }

        public ItemLocation ToItemLoc
        {
            get { return toItemLoc; }
        }

        public int ToItemId
        {
            get { return toItemId; }
        }

        public byte ToStackPos
        {
            get { return toStackPos; }
        }

        public ItemUseOnPacket()
        {
            type = PacketType.ItemUseOn;
            destination = PacketDestination.Server;
        }

        public ItemUseOnPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemUseOn) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                from = p.GetItemLocation();
                fromItemId = p.GetInt();
                fromStackPos = p.GetByte();

                if (p.PeekInt() == 0xFFFF)
                {
                    // An inventory item
                    toItemLoc = p.GetItemLocation();
                }
                else
                {
                    // A tile or item on the ground
                    toLoc = p.GetLocation();
                }

                toItemId = p.GetInt();
                toStackPos = p.GetByte();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ItemUseOnPacket Create(ItemLocation fromLoc, uint fromItemId, byte fromStackPos,
            ItemLocation toLoc, uint toItemId, byte toStackPos)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ItemUseOn);
            p.AddItemLocation(fromLoc);
            p.AddInt((int)fromItemId);
            p.AddByte(fromStackPos);
            p.AddItemLocation(toLoc);
            p.AddInt((int)toItemId);
            p.AddByte(toStackPos);
            return new ItemUseOnPacket(p.GetPacket());
        }

        public static ItemUseOnPacket Create(ItemLocation fromLoc, uint fromItemId, byte fromStackPos,
            Location toLoc, uint toItemId, byte toStackPos)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ItemUseOn);
            p.AddItemLocation(fromLoc);
            p.AddInt((int)fromItemId);
            p.AddByte(fromStackPos);
            p.AddLocation(toLoc);
            p.AddInt((int)toItemId);
            p.AddByte(toStackPos);
            return new ItemUseOnPacket(p.GetPacket());
        }
    }
}
