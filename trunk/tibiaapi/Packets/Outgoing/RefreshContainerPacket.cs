using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RefreshContainerPacket : OutgoingPacket
    {
        public byte ContainerId { get; set; }

        public RefreshContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RefreshContainer;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RefreshContainer)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RefreshContainer;

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
            RefreshContainerPacket p = new RefreshContainerPacket(client);
            p.ContainerId = containerId;
            return p.Send();
        }
    }
}