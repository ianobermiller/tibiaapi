using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class UpContainerPacket : OutgoingPacket
    {
        public byte Id { get; set; }

        public UpContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.UpContainer;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.UpContainer)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.UpContainer;

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
            UpContainerPacket p = new UpContainerPacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}