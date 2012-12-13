using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CloseContainerPacket : OutgoingPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.CloseContainer;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.CloseContainer)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.CloseContainer;

            ContainerId = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(ContainerId);
        }

        public static bool Send(Objects.Client client, byte containerId)
        {
            return new CloseContainerPacket(client) { ContainerId = containerId }.Send();
        }
    }
}