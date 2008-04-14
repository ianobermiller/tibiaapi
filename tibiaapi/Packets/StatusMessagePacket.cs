using System;
using System.Text;

namespace Tibia.Packets
{
    public class StatusMessagePacket : Packet
    {
        private StatusMessageColor color;
        private short lenMessage;
        private string message;

        public StatusMessageColor Color
        {
            get { return color; }
        }

        public string Message
        {
            get { return message; }
        }

        public StatusMessagePacket()
        {
            type = PacketType.StatusMessage;
            destination = PacketDestination.Client;
        }

        public StatusMessagePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.StatusMessage) return false;
                int index = 3; // Color
                color = (StatusMessageColor)packet[index];
                index += 1; // Message length
                lenMessage = BitConverter.ToInt16(packet, index);
                index += 2; // Begin message
                message = Encoding.ASCII.GetString(packet, index, lenMessage);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static StatusMessagePacket Create(StatusMessageColor color, string message)
        {
            byte[] packet = new byte[message.Length + 6];
            Array.Copy(BitConverter.GetBytes((short)(message.Length + 4)), packet, 2);
            packet[2] = (byte)PacketType.StatusMessage;
            packet[3] = (byte)color;
            Array.Copy(Encoding.ASCII.GetBytes(message), 0, packet, 4, message.Length);
            StatusMessagePacket smp = new StatusMessagePacket(packet);
            return smp;
        }
    }
}
