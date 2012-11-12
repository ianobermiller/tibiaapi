using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RejectTradePacket : OutgoingPacket
    {
        public RejectTradePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RejectTrade;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RejectTrade)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RejectTrade;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            RejectTradePacket p = new RejectTradePacket(client);
            return p.Send();
        }
    }
}