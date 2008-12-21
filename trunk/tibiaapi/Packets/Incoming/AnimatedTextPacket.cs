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
            Type = IncomingPacketType.AnimatedText;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.AnimatedText)
                return false;

            Destination = destination;
            Type = IncomingPacketType.AnimatedText;

            try
            {
                Position = msg.GetLocation();
                Color = (TextColor)msg.GetByte();
                Message = msg.GetString();
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
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddLocation(Position);
            msg.AddByte((byte)Color);
            msg.AddString(Message);

            return msg.Packet;
        }
    }
}
