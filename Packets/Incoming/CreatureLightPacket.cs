using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureLightPacket : Packet 
    {
        private int creatureId;
        private byte light;
        private byte color;

        public int CreatureId
        {
            get { return creatureId; }
        }
        public byte Light
        {
            get { return light; }
        }
        public byte Color
        {
            get { return color; }
        }
        public CreatureLightPacket(Client c) : base(c)
        {
            type = PacketType.CreatureLight;
            destination = PacketDestination.Client;
        }
        public CreatureLightPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureLight) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureId=p.GetLong();
                light = p.GetByte();
                color = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureLightPacket Create(Client c, Creature creature, byte light, byte color)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureLight);
            p.AddLong(creature.Id);
            p.AddByte(light);
            p.AddByte(color);
            return new CreatureLightPacket(c, p.GetPacket());
        }
    }
}
