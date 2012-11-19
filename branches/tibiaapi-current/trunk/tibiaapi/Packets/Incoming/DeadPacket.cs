using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeadPacket : IncomingPacket
    {

        public DeadPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Dead;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Dead)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Dead;

            msg.GetByte(); //?

            return true;
        }
    }
}