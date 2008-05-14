using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class AnimatedTextPacket : Packet
    {
        private Objects.Location loc;
        private ushort lenMessage;
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

        public AnimatedTextPacket()
        {
            type = PacketType.AnimatedText;
            destination = PacketDestination.Client;
        }
        public AnimatedTextPacket(byte[] data) : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.AnimatedText) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                loc = p.GetLocation();
                color = (TextColor)p.GetByte();
                lenMessage = p.GetInt();
                message = p.GetString(lenMessage);
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static AnimatedTextPacket Create(string message, TextColor Color, Objects.Location loc)
        {
            PacketBuilder p = new PacketBuilder(PacketType.AnimatedText);
            p.AddLocation(loc);
            p.AddByte((byte)Color);
            p.AddInt(message.Length);
            p.AddString(message);
            AnimatedTextPacket amp = new AnimatedTextPacket(p.GetPacket());
            return amp;
        }
    }
}
