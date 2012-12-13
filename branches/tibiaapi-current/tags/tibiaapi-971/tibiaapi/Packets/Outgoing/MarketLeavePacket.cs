using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MarketLeavePacket : OutgoingPacket
    {
        public MarketLeavePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MarketLeave;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MarketLeave)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MarketLeave;
            
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            MarketLeavePacket p = new MarketLeavePacket(client);
            return p.Send();
        }
    }
}