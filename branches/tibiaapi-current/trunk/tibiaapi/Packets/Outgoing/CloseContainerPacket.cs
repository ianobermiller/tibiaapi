using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CloseContainerPacket : OutgoingPacket
    {
        public byte Id { get; set; }

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
            CloseContainerPacket p = new CloseContainerPacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}