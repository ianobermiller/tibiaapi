using System;
using System.Collections.Generic;
using System.Text;

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

        public CreatureSquarePacket()
        {
            type = PacketType.CreatureSquare;
            destination = PacketDestination.Client;
        }

        public CreatureSquarePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureSquare) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
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

        public static CreatureSquarePacket Create(Objects.Creature creature, SquareColor color)
        {
            return Create(creature.Id, color);
        }

        public static CreatureSquarePacket Create(int id, SquareColor color)
        {
            PacketBuilder p = new PacketBuilder(PacketType.CreatureSquare);
            p.AddLong(id);
            p.AddByte((byte)color);
            return new CreatureSquarePacket(p.GetPacket());
        }
    }
}
