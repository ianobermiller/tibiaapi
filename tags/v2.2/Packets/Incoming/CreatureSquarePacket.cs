using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureSquarePacket : Packet
    {
        private int creatureId;
        private SquareColor color;

        public int CreatureId
        {
            get { return creatureId; }
        }

        public SquareColor Color
        {
            get { return color; }
        }

        public CreatureSquarePacket(Client c) : base(c)
        {
            type = PacketType.CreatureSquare;
            destination = PacketDestination.Client;
        }

        public CreatureSquarePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureSquare) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureId = p.GetLong();
                color = (SquareColor)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureSquarePacket Create(Client c, Objects.Creature creature, SquareColor color)
        {
            return Create(c, creature.Id, color);
        }

        public static CreatureSquarePacket Create(Client c, int id, SquareColor color)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureSquare);
            p.AddLong(id);
            p.AddByte((byte)color);
            return new CreatureSquarePacket(c, p.GetPacket());
        }
    }
}
