using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ContainerClosePacket : OutgoingPacket
    {
        public byte Id { get; set; }

        public ContainerClosePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ContainerClose;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ContainerClose)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ContainerClose;

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

        public static bool Send(Objects.Client client, byte id)
        {
            ContainerClosePacket p = new ContainerClosePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}