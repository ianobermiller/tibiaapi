using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureMovePacket : IncomingPacket
    {
        public byte FromStackPosition { get; set; }
        public Objects.Location FromPosition { get; set; }
        public Objects.Location ToPosition { get; set; }

        public CreatureMovePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_MOVE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_MOVE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_MOVE;
            FromPosition = msg.GetLocation();
            FromStackPosition = msg.GetByte();
            ToPosition = msg.GetLocation();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddLocation(FromPosition);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToPosition);

            return msg.Packet;
        }
    }
}