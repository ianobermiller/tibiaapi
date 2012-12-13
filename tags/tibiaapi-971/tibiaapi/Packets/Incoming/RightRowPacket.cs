using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class RightRowPacket : MapPacket
    {

        public RightRowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RightRow;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RightRow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.RightRow;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.X++;

            return ParseMapDescription(msg, Client.playerLocation.X + 9, Client.playerLocation.Y - 6, Client.playerLocation.Z, 1, 14, outMsg);
        }
    }
}