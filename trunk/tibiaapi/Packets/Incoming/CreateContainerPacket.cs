using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreateContainerPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public Objects.Item Item { get; set; }

        public CreateContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreateContainer;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreateContainer)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreateContainer;

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