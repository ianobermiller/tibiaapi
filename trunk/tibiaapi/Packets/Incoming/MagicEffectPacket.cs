using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MagicEffectPacket : IncomingPacket
    {
        public Objects.Location Position { get; set; }
        public byte Effect { get; set; }

        public MagicEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.MAGIC_EFFECT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.MAGIC_EFFECT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.MAGIC_EFFECT;

            Position = msg.GetLocation();
            Effect = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddByte(Effect);

            return msg.Packet;
        }
    }
}