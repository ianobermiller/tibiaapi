using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreatureMovePacket : IncomingPacket
    {
        public byte FromStackPosition { get; set; }
        public Objects.Location FromLocation { get; set; }
        public Objects.Location ToLocation { get; set; }

        public CreatureMovePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureMove;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureMove)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureMove;

            FromLocation = msg.GetLocation();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromLocation);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToLocation);
        }
    }
}