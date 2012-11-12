using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerWalkCancelPacket : IncomingPacket
    {
        public byte Direction { get; set; }

        public PlayerWalkCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerWalkCancel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerWalkCancel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerWalkCancel;

            Direction = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Direction);
        }
    }
}