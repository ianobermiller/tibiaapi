using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class LeftRowPacket : MapPacket
    {

        public LeftRowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.LeftRow;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.LeftRow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.LeftRow;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.X--;

            return ParseMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 1, 14, outMsg);
        }
    }
}