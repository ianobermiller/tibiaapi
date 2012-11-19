using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MarketLeavePacket : IncomingPacket
    {
        public MarketLeavePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MarketLeave;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MarketLeave)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MarketLeave;

            return true;
        }
    }
}