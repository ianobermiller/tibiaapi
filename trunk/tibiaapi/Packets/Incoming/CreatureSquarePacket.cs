using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureSquarePacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public SquareColor Color { get; set; }

        public CreatureSquarePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_SQUARE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_SQUARE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_SQUARE;

            CreatureId = msg.GetUInt32();
            Color = (SquareColor)msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddByte((byte)Color);

            return msg.Packet;
        }
    }
}