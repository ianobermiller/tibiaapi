using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class BottomRowPacket : MapPacket
    {

        public BottomRowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.BottomRow;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.BottomRow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.BottomRow;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Y++;

            return ParseMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y + 7, Client.playerLocation.Z, 18, 1, outMsg);
        }
    }
}