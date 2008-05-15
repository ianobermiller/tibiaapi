using System;
using System.Text;

namespace Tibia.Packets
{
    public class StatusMessagePacket : Packet
    {
        private StatusMessageType color;
        private string message;

        public StatusMessageType Color
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
                PacketBuilder p = new PacketBuilder(packet, 3);
                color = (StatusMessageType)p.GetByte();
                message = p.GetString();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static StatusMessagePacket Create(StatusMessageType color, string message)
        {
            PacketBuilder p = new PacketBuilder(PacketType.StatusMessage);
            p.AddByte((byte)color);
            p.AddString(message);
            return new StatusMessagePacket(p.GetPacket());
        }
    }
}
