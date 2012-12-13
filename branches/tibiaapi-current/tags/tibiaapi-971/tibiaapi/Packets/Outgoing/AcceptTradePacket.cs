using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class AcceptTradePacket : OutgoingPacket
    {
        public AcceptTradePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AcceptTrade;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AcceptTrade)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AcceptTrade;
            
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new AcceptTradePacket(client).Send();
        }
    }
}