using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SnapBackPacket : IncomingPacket
    {
        public SnapBackPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SnapBack;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SnapBack)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SnapBack;

            msg.GetByte();

            return true;
        }
    }
}