using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeleteInContainerPacket : IncomingPacket
    {
        public byte ContainerId { get; set; }
        public byte Slot { get; set; }

        public DeleteInContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.DeleteInContainer;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.DeleteInContainer)
                return false;

            Destination = destination;
            Type = IncomingPacketType.DeleteInContainer;

            ContainerId = msg.GetByte();
            Slot = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(ContainerId);
            msg.AddByte(Slot);
        }

    }
}