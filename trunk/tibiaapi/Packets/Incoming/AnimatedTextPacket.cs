using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class AnimatedTextPacket : IncomingPacket
    {
        public Objects.Location Location { get; set; }
        public string Message { get; set; }
        public TextColor Color { get; set; }

        public AnimatedTextPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.AnimatedText;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.AnimatedText)
                return false;

            Destination = destination;
            Type = IncomingPacketType.AnimatedText;

            Location = msg.GetLocation();
            Color = (TextColor)msg.GetByte();
            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
            msg.AddByte((byte)Color);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, string message, Objects.Location position, TextColor color)
        {
            AnimatedTextPacket p = new AnimatedTextPacket(client);
            p.Message = message;
            p.Location = position;
            p.Color = color;
            return p.Send();
        }
    }
}
