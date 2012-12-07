using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CloseContainerPacket : IncomingPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CloseContainer;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int postion = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CloseContainer)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CloseContainer;

            ContainerId = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(ContainerId);
        }
    }
}