using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SafeTradeClosePacket : IncomingPacket
    {

        public SafeTradeClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SafeTradeClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.SafeTradeClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SafeTradeClose;

            //no data


            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}