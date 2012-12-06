using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CloseNPCTradePacket : OutgoingPacket
    {
        public CloseNPCTradePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.CloseNPCTrade;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.CloseNPCTrade)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.CloseNPCTrade;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new CloseNPCTradePacket(client).Send();
        }
    }
}