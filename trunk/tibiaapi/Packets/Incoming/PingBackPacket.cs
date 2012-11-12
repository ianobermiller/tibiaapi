using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PingBackPacket : IncomingPacket
    {
        public PingBackPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PingBack;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PingBack)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PingBack;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}