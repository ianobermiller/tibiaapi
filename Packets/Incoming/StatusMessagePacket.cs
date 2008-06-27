using System;
using Tibia.Objects;

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

        public StatusMessagePacket(Client c)
            : base(c)
        {
            type = PacketType.StatusMessage;
            destination = PacketDestination.Client;
        }

        public StatusMessagePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.StatusMessage) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
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

        public static StatusMessagePacket Create(Client c, StatusMessageType color, string message)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.StatusMessage);
            p.AddByte((byte)color);
            p.AddString(message);
            return new StatusMessagePacket(c, p.GetPacket());
        }
    }
}
