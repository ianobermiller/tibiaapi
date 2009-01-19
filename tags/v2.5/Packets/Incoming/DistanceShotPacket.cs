using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class DistanceShotPacket : IncomingPacket
    {

        public Objects.Location FromPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public ProjectileType Effect { get; set; }

        public DistanceShotPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Projectile;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Projectile)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Projectile;

            FromPosition = msg.GetLocation();
            ToPosition = msg.GetLocation();
            Effect = (ProjectileType)msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte((byte)Effect);

            return msg.Packet;
        }
    }
}