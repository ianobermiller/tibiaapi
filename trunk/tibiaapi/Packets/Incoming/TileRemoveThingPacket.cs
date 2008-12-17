using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class TileRemoveThingPacket : IncomingPacket
    {
        public byte StackPosition { get; set; }
        public Objects.Location Position { get; set; }

        public TileRemoveThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.TILE_REMOVE_THING;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.TILE_REMOVE_THING)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.TILE_REMOVE_THING;

            Position = msg.GetLocation();
            StackPosition = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddByte(StackPosition);

            return msg.Packet;
        }
    }
}