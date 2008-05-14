using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class CreatureHealthPacket : Packet
    {
        private int creatureId;
        private byte creatureHP;

        public int CreatureId
        {
            get { return creatureId; }
        }

        public byte CreatureHP
        {
            get { return creatureHP; }
        }

        public CreatureHealthPacket()
        {
            type = PacketType.CreatureHealth;
            destination = PacketDestination.Client;
        }

        public CreatureHealthPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureHealth) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                creatureId = p.GetLong();
                creatureHP = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureHealthPacket Create(Objects.Creature creature, byte hp)
        {
            return Create(creature.Id, hp);
        }

        public static CreatureHealthPacket Create(int id, byte hp)
        {
            PacketBuilder p = new PacketBuilder(PacketType.CreatureHealth);
            p.AddLong(id);
            p.AddByte(hp);
            CreatureHealthPacket chp = new CreatureHealthPacket(p.GetPacket());
            return chp;
        }
    }
}
