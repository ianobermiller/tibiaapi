using System;
using Tibia.Objects;

namespace Tibia.Packets
{

    public class WorldLightPacket : Packet
    {
        private byte strength;
        private byte color;
        public byte Strength
        {
            get { return strength; }
        }
        public byte Color
        {
            get { return color; }
        }
        public WorldLightPacket(Client c)
            : base(c)
        {
            type = PacketType.WorldLight;
            destination = PacketDestination.Client;
        }
        public WorldLightPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.WorldLight) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                strength = p.GetByte();
                color = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static WorldLightPacket Create(Client c,byte strength, byte color)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.WorldLight);
            p.AddByte(strength);
            p.AddByte(color);
            return new WorldLightPacket(c, p.GetPacket());
        }
    }
}