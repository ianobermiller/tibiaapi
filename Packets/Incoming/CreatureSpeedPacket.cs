using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureSpeedPacket : Packet
    {
        private int creatureId;
        private int speed;

        public int CreatureId
        {
            get { return creatureId; }
        }
        public int Speed
        {
            get { return speed; }
        }
        public CreatureSpeedPacket(Client c)
            : base(c)
        {
            type = PacketType.CreatureSpeed;
            destination = PacketDestination.Client;
        }
        public CreatureSpeedPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureSpeed) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureId = p.GetLong();
                speed = p.GetInt();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureSpeedPacket Create(Client c, Creature creature, int speed)
        {
            return Create(c, creature.Id, speed);
        }
        public static CreatureSpeedPacket Create(Client c, int id, int speed)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureSpeed);
            p.AddLong(id);
            p.AddInt(speed);
            return new CreatureSpeedPacket(c, p.GetPacket());
        }
    }
}
