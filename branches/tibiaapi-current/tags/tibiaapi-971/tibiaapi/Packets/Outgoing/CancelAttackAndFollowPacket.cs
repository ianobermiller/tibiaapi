using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CancelAttackAndFollowPacket : OutgoingPacket
    {
        public CancelAttackAndFollowPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.CancelAttackAndFollow;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.CancelAttackAndFollow)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.CancelAttackAndFollow;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new CancelAttackAndFollowPacket(client).Send();
        }
    }
}