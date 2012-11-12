using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MoveCreaturePacket : IncomingPacket
    {
        public byte FromStackPosition { get; set; }
        public Objects.Location FromLocation { get; set; }
        public Objects.Location ToLocation { get; set; }

        public MoveCreaturePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveCreature;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveCreature)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveCreature;

            FromLocation = msg.GetLocation();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}