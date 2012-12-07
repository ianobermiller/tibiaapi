using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CancelWalkPacket : IncomingPacket
    {
        public Direction Direction { get; set; }
        public CancelWalkPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CancelWalk;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CancelWalk)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CancelWalk;

            Direction= (Direction)msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)Direction);
        }
    }
}