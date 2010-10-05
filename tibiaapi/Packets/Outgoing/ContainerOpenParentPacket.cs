using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ContainerOpenParentPacket : OutgoingPacket
    {
        public byte Id { get; set; }

        public ContainerOpenParentPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ContainerOpenParent;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ContainerOpenParent)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ContainerOpenParent;

            Id = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Id);
        }

        public static bool Send(Objects.Client client, byte id)
        {
            ContainerOpenParentPacket p = new ContainerOpenParentPacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}