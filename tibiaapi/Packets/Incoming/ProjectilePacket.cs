using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ProjectilePacket : IncomingPacket
    {

        public Objects.Location FromPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public ProjectileType Effect { get; set; }

        public ProjectilePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Projectile;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position; 

            if (msg.GetByte() != (byte)IncomingPacketType.Projectile)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Projectile;

            try
            {
                FromPosition = msg.GetLocation();
                ToPosition = msg.GetLocation();
                Effect = (ProjectileType)msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte((byte)Effect);

            return msg.Packet;
        }
    }
}