using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CancelPacket : OutgoingPacket
    {
        public CancelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Cancel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Cancel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Cancel;

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