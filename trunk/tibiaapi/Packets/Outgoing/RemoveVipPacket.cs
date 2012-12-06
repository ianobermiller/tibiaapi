using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RemoveVipPacket : OutgoingPacket
    {
        public uint Id { get; set; }

        public RemoveVipPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RemoveVip;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RemoveVip)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RemoveVip;

            Id = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(Id);
        }

        public static bool Send(Objects.Client client, uint id)
        {
            return new RemoveVipPacket(client) { Id = id }.Send();
        }
    }
}