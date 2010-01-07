using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ContainerRemoveItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public byte Slot { get; set; }

        public ContainerRemoveItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerRemoveItem;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerRemoveItem)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerRemoveItem;

            Container = msg.GetByte();
            Slot = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Container);
            msg.AddByte(Slot);
        }

    }
}