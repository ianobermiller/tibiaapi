using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeathPacket : IncomingPacket
    {

        public DeathPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Death;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Death)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Death;

            msg.GetByte(); //?

            return true;
        }
    }
}