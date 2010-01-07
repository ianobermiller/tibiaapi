using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ContainerClosePacket : IncomingPacket
    {
        public byte Id { get; set; }

        public ContainerClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int postion = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerClose;

            Id = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Id);
        }

        public static bool Send(Objects.Client client, byte id)
        {
            ContainerClosePacket p = new ContainerClosePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}