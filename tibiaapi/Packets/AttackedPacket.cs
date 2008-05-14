using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class AttackedPacket : Packet
    {
        private int creatureId;
        public int CreatureId
        {
            get { return creatureId; }
        }
        public AttackedPacket()
        {
            type = PacketType.Attacked;
            destination = PacketDestination.Client;
        }
        public AttackedPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Attacked) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                creatureId = p.GetLong();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static AttackedPacket Create(Objects.Creature creature)
        {
            return Create(creature.Id);
        }

        public static AttackedPacket Create(int id)
        {
            PacketBuilder p = new PacketBuilder(PacketType.Attacked);
            p.AddLong(id);
            AttackedPacket atp = new AttackedPacket(p.GetPacket());
            return atp;
        }
    }
}
