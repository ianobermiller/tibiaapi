using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ContainerUpdateItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public byte Slot { get; set; }
        public Objects.Item Item { get; set; }

        public ContainerUpdateItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerUpdateItem;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerUpdateItem)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerUpdateItem;

            Container = msg.GetByte();
            Slot = msg.GetByte();
            Item = msg.GetItem();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Container);
            msg.AddByte(Slot);
            msg.AddItem(Item);
        }
    }
}