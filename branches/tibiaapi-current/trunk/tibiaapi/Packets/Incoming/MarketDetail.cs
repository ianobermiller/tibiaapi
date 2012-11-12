using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MarketDetailPacket : IncomingPacket
    {
        public MarketDetailPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MarketDetail;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MarketDetail)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MarketDetail;

            msg.GetUInt16();

            for (int i = 0; i <= 14; i++)
                msg.GetString();

            byte Count = msg.GetByte();
            Count -= 1;
            while (Count >= 0)
            {
                msg.GetUInt32();
                msg.GetUInt32();
                msg.GetUInt32();
                msg.GetUInt32();
                Count -= 1;
            }

            Count = msg.GetByte();
            Count -= 1;
            while (Count >= 0)
            {
                msg.GetUInt32();
                msg.GetUInt32();
                msg.GetUInt32();
                msg.GetUInt32();
                Count -= 1;
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}