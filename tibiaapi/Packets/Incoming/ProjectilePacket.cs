using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
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

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte((byte)Effect);
        }

        public static bool Send(Objects.Client client, Objects.Location fromLocation, Objects.Location toLocation, ProjectileType effect)
        {
            ProjectilePacket p = new ProjectilePacket(client);
            p.FromPosition = fromLocation;
            p.ToPosition = toLocation;
            p.Effect = effect;
            return p.Send();
        }
    }
}