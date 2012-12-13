using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeleteOnMapPacket : IncomingPacket
    {
        public DeleteOnMapPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.DeleteOnMap;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.DeleteOnMap)
                return false;

            Destination = destination;
            Type = IncomingPacketType.DeleteOnMap;

            msg.GetLocation();
            msg.GetByte();

            return true;
        }
    }
}