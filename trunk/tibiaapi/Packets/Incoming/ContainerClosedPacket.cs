using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ContainerClosedPacket : Packet
    {
        private byte id;
        public byte Id
        {
            get { return id; }
        }

        public ContainerClosedPacket(Client c)
            : base(c)
        {
            type = PacketType.ContainerClosed;
            destination = PacketDestination.Client;
        }
        public ContainerClosedPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerClosed) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                id = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ContainerClosedPacket Create(Client c,byte Id)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ContainerClosed);
            p.AddByte(Id);
            return new ContainerClosedPacket(c, p.GetPacket());
        }
    }
}