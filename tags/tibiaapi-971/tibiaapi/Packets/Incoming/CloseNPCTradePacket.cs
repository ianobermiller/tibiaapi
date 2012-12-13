using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CloseNPCTradePacket : IncomingPacket
    {

        public CloseNPCTradePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CloseNPCTrade;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CloseNPCTrade)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CloseNPCTrade;

            //no data

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}