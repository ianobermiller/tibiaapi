using System;
using Tibia.Objects;

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

        public CreatureHealthPacket(Client c) : base(c)
        {
            type = PacketType.CreatureHealth;
            destination = PacketDestination.Client;
        }

        public CreatureHealthPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureHealth) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
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

        public static CreatureHealthPacket Create(Client c, Objects.Creature creature, byte hp)
        {
            return Create(c, creature.Id, hp);
        }

        public static CreatureHealthPacket Create(Client c, int id, byte hp)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureHealth);
            p.AddLong(id);
            p.AddByte(hp);
            return new CreatureHealthPacket(c, p.GetPacket());
        }
    }
}
