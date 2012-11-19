using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class FullMapPacket : MapPacket
    {

        public FullMapPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FullMap;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.FullMap)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FullMap;

            Client.playerLocation = msg.GetLocation();
            outMsg.AddLocation(Client.playerLocation);

            return ParseMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 18, 14, outMsg);
        }
    }
}