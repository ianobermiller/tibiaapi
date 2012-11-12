using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CancelMovePacket : OutgoingPacket
    {
        public CancelMovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.CancelMove;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.CancelMove)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.CancelMove;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            AttackPacket p = new AttackPacket(client);
            return p.Send();
        }
    }
}