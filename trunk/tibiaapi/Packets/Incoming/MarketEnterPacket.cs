using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MarketEnterPacket : IncomingPacket
    {
        public MarketEnterPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MarketEnter;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MarketEnter)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MarketEnter;

            uint AccountBalance = msg.GetUInt32();
            byte ActiveOffers = msg.GetByte();
            ushort Count = msg.GetUInt16();
            Count -= 1;

            while (Count >= 0)
            {
                msg.GetUInt16();
                msg.GetUInt16();
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}