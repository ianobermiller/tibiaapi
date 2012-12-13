using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MarketBrowsePacket : IncomingPacket
    {
        public MarketBrowsePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MarketBrowse;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MarketBrowse)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MarketBrowse;

            byte Unknown1 = msg.GetByte();

            byte Count = msg.GetByte();
            Count -= 1;
            while (Count >= 0)
            {
                //read market buy offer
                return false;
            }

            Count = msg.GetByte();
            Count -= 1;
            while (Count >= 0)
            {
                //read market sell offer
                return false;
            }

            return true;
        }
    }
}