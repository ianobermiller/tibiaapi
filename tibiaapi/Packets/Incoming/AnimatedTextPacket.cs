using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class AnimatedTextPacket : Packet
    {
        private Objects.Location loc;
        private string message;
        private TextColor color;

        public string Message
        {
            get { return message; }
        }
        public TextColor Color
        {
            get { return color; }
        }
        public Objects.Location Loc
        {
            get { return loc; }
        }

        public AnimatedTextPacket(Client c) : base(c)
        {
            type = PacketType.AnimatedText;
            destination = PacketDestination.Client;
        }
        public AnimatedTextPacket(Client c, byte[] data) : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.AnimatedText) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                loc = p.GetLocation();
                color = (TextColor)p.GetByte();
                message = p.GetString();
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static AnimatedTextPacket Create(Client c, string message, TextColor Color, Objects.Location loc)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.AnimatedText);
            p.AddLocation(loc);
            p.AddByte((byte)Color);
            p.AddString(message);
            return new AnimatedTextPacket(c, p.GetPacket());
        }
    }
}
