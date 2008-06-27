using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ItemUseBattlelistPacket : Packet
    {
        ItemLocation from;
        int fromItemId;
        byte fromStackPos;
        int creatureId;

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

        public int CreatureId
        {
            get { return creatureId; }
        }

        public ItemUseBattlelistPacket(Client c) : base(c)
        {
            type = PacketType.ItemUseBattlelist;
            destination = PacketDestination.Server;
        }

        public ItemUseBattlelistPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemUseBattlelist) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);

                from = p.GetItemLocation();
                fromItemId = p.GetInt();
                fromStackPos = p.GetByte();
                creatureId = p.GetLong();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ItemUseBattlelistPacket Create(Client c, ItemLocation from, uint fromItemId, byte fromStackPos,
            int creatureId)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ItemUseBattlelist);
            p.AddItemLocation(from);
            p.AddInt((int)fromItemId);
            p.AddByte(fromStackPos);
            p.AddLong(creatureId);
            return new ItemUseBattlelistPacket(c, p.GetPacket());
        }
    }
}
