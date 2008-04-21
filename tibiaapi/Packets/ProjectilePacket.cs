using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ProjectilePacket: Packet
    {
        private Objects.Location from;
        private Objects.Location to;
        private byte projectile;
        
        public Objects.Location From
        {
            get { return from; }
        }

        public Objects.Location To
        {
            get { return to; }
        }
        public byte Projectile
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
                int index = 3;
                from.X = BitConverter.ToInt16(packet, index);
                index += 2;
                from.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                from.Z = packet[index];
                index += 1;
                to.X = BitConverter.ToInt16(packet, index);
                index += 2;
                to.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                to.Z = packet[index];
                index += 1;
                projectile = packet[index];
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ProjectilePacket Create(Objects.Location from, Objects.Location to, byte projectile)
        {
            byte[] packet = new byte[14];

            packet[0] = 0x0C;
            packet[2] = (byte)PacketType.Projectile;
            Array.Copy(from.ToBytes(), 0, packet, 3, 5);
            Array.Copy(to.ToBytes(), 0, packet, 8, 5);
            packet[13] = projectile;

            ProjectilePacket pp = new ProjectilePacket(packet);
            return pp;
        }
    }
}
