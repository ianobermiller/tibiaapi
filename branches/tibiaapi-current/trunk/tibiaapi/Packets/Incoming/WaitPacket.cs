using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class WaitPacket : IncomingPacket
    {
        public WaitPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Wait;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Wait)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Wait;

            msg.GetUInt16(); //?

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}