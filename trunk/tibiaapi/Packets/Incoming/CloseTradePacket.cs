using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CloseTradePacket : IncomingPacket
    {

        public CloseTradePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CloseTrade;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CloseTrade)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CloseTrade;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}