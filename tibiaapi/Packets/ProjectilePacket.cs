using System;
using System.Collections.Generic;
using System.Text;

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

        public ProjectilePacket()
        {
            type = PacketType.Projectile;
            destination = PacketDestination.Client;
        }

        public ProjectilePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Projectile) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
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

        public static ProjectilePacket Create(Objects.Location from, Objects.Location to, ProjectileType projectile)
        {
            PacketBuilder p = new PacketBuilder(PacketType.Projectile);
            p.AddLocation(from);
            p.AddLocation(to);
            p.AddByte((byte)projectile);
            return new ProjectilePacket(p.GetPacket());
        }
    }
}
