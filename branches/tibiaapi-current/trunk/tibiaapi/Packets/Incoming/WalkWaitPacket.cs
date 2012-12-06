using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class WalkWaitPacket : IncomingPacket
    {
        public WalkWaitPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.WalkWait;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.WalkWait)
                return false;

            Destination = destination;
            Type = IncomingPacketType.WalkWait;

            msg.GetUInt16(); //?

            return true;
        }
    }
}