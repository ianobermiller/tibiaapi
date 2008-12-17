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
        public ShootType_t Effect { get; set; }

        public DistanceShotPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.DISTANCE_SHOT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.DISTANCE_SHOT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.DISTANCE_SHOT;

            FromPosition = msg.GetLocation();
            ToPosition = msg.GetLocation();
            Effect = (ShootType_t)msg.GetByte();

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