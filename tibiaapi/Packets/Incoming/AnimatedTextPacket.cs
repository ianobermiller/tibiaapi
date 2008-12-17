using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class AnimatedTextPacket : IncomingPacket
    {
        public Objects.Location Position { get; set; }
        public string Message { get; set; }
        public TextColor Color { get; set; }

        public AnimatedTextPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.ANIMATED_TEXT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.ANIMATED_TEXT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.ANIMATED_TEXT;
            Position = msg.GetLocation();
            Color = (TextColor)msg.GetByte();
            Message = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddLocation(Position);
            msg.AddByte((byte)Color);
            msg.AddString(Message);

            return msg.Packet;
        }
    }
}
