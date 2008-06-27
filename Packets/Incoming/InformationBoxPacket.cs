using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class InformationBoxPacket : Packet
    {
        private string message;
        public string Message
        {
            get { return message; }
        }
        public InformationBoxPacket(Client c)
            : base(c)
        {
            type = PacketType.InformationBox;
            destination = PacketDestination.Client;
        }
        public InformationBoxPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.InformationBox) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                message = p.GetString();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static InformationBoxPacket Create(Client c,string message)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.InformationBox);
            p.AddString(message);
            return new InformationBoxPacket(c, p.GetPacket());
        }
    }
}