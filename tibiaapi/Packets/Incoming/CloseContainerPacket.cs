using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CloseContainerPacket : IncomingPacket
    {
        public byte Id { get; set; }

        public CloseContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ContainerClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerClose;
            Id = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte(Id);

            return msg.Packet;
        }
    }
}