using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreateInContainerPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public Objects.Item Item { get; set; }

        public CreateInContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreateInContainer;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreateInContainer)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreateInContainer;

            Container = msg.GetByte();
            Item = msg.GetItem();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Container);
            msg.AddItem(Item);
        }

    }
}