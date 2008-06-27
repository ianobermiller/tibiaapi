using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ContainerOpenParentPacket : Packet
    {
        byte number;

        public byte Number
        {
            get { return number; }
        }

        public ContainerOpenParentPacket(Client c) : base(c)
        {
            type = PacketType.ContainerOpenParent;
            destination = PacketDestination.Server;
        }

        public ContainerOpenParentPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerOpenParent) return false;
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

        public static ContainerOpenParentPacket Create(Client c, byte number)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ContainerOpenParent);
            p.AddByte(number);
            return new ContainerOpenParentPacket(c, p.GetPacket());
        }
    }
}
