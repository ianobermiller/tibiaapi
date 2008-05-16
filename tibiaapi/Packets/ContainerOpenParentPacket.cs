using System;

namespace Tibia.Packets
{
    public class ContainerOpenParentPacket : Packet
    {
        byte number;

        public byte Number
        {
            get { return number; }
        }

        public ContainerOpenParentPacket()
        {
            type = PacketType.ContainerOpenParent;
            destination = PacketDestination.Server;
        }

        public ContainerOpenParentPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerOpenParent) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                number = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ContainerOpenParentPacket Create(byte number)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ContainerOpenParent);
            p.AddByte(number);
            return new ContainerOpenParentPacket(p.GetPacket());
        }
    }
}
