using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ContainerClosePacket : Packet
    {
        byte number;

        public byte Number
        {
            get { return number; }
        }

        public ContainerClosePacket()
        {
            type = PacketType.ContainerClose;
            destination = PacketDestination.Server;
        }

        public ContainerClosePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerClose) return false;
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

        public static ContainerClosePacket Create(byte number)
        {
            PacketBuilder p = new PacketBuilder(PacketType.ContainerClose);
            p.AddByte(number);
            return new ContainerClosePacket(p.GetPacket());
        }
    }
}
