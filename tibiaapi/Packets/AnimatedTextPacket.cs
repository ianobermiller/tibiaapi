using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class AnimatedTextPacket : Packet
    {
        private Objects.Location loc;
        private short lenMessage;
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
                int index = 3;
                loc.X = BitConverter.ToInt16(packet, index);
                index += 2;
                loc.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                loc.Z = packet[index];
                index += 1;
                color = (TextColor)packet[index];
                index += 1;
                lenMessage = BitConverter.ToInt16(packet, index);
                index += 2;
                message = Encoding.ASCII.GetString(packet, index, lenMessage);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static AnimatedTextPacket Create(string message, TextColor Color, Objects.Location loc)
        {
            byte[] packet = new byte[message.Length + 11];
            Array.Copy(BitConverter.GetBytes((short)(message.Length + 9)), packet, 2);
            packet[2] = (byte)PacketType.AnimatedText;
            Array.Copy(BitConverter.GetBytes((short)(loc.X)), 0, packet,3, 2);
            Array.Copy(BitConverter.GetBytes((short)(loc.Y)), 0, packet,5, 2);
            packet[7] = (byte)loc.Z;
            packet[8] = (byte)Color;
            Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9, 2);
            Array.Copy(Encoding.ASCII.GetBytes(message), 0, packet, 11, message.Length);
            AnimatedTextPacket amp = new AnimatedTextPacket(packet);
            return amp;
        }
    }
}
