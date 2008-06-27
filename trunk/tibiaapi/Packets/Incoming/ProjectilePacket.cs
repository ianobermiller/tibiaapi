using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ProjectilePacket: Packet
    {
        private Objects.Location from;
        private Objects.Location to;
        private ProjectileType projectile;
        
        public Objects.Location From
        {
            get { return from; }
        }

        public Objects.Location To
        {
            get { return to; }
        }
        public ProjectileType Projectile
        {
            get { return projectile; }
        }

        public ProjectilePacket(Client c) : base(c)
        {
            type = PacketType.Projectile;
            destination = PacketDestination.Client;
        }

        public ProjectilePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Projectile) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                from = p.GetLocation();
                to = p.GetLocation();
                projectile = (ProjectileType)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ProjectilePacket Create(Client c, Objects.Location from, Objects.Location to, ProjectileType projectile)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.Projectile);
            p.AddLocation(from);
            p.AddLocation(to);
            p.AddByte((byte)projectile);
            return new ProjectilePacket(c, p.GetPacket());
        }
    }
}
