using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ContainerClosePacket : Packet
    {
        byte number;

        public byte Number
        {
            get { return number; }
        }

        public ContainerClosePacket(Client c)
            : base(c)
        {
            type = PacketType.ContainerClose;
            destination = PacketDestination.Server;
        }

        public ContainerClosePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerClose) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                number = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ContainerClosePacket Create(Client c, byte number)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ContainerClose);
            p.AddByte(number);
            return new ContainerClosePacket(c, p.GetPacket());
        }
    }
}
