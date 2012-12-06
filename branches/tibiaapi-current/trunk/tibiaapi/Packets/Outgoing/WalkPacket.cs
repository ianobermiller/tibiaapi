using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class WalkPacket : OutgoingPacket
    {
        public Constants.Direction Direction { get; set; }

        public WalkPacket(Objects.Client c, Constants.Direction direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Tibia.Constants.Direction.Down:
                    Type = OutgoingPacketType.WalkSouth;
                    break;
                case Tibia.Constants.Direction.Up:
                    Type = OutgoingPacketType.WalkNorth;
                    break;
                case Tibia.Constants.Direction.Right:
                    Type = OutgoingPacketType.WalkEast;
                    break;
                case Tibia.Constants.Direction.Left:
                    Type = OutgoingPacketType.WalkWest;
                    break;
                case Tibia.Constants.Direction.DownLeft:
                    Type = OutgoingPacketType.WalkSouthWest;
                    break;
                case Tibia.Constants.Direction.DownRight:
                    Type = OutgoingPacketType.WalkSouthEast;
                    break;
                case Tibia.Constants.Direction.UpLeft:
                    Type = OutgoingPacketType.WalkNorthWest;
                    break;
                case Tibia.Constants.Direction.UpRight:
                    Type = OutgoingPacketType.WalkNorthEast;
                    break;
                default:
                    throw new System.Exception("Unknown Constants.Direction");
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client, Constants.Direction direction)
        {
            return new WalkPacket(client, direction).Send();
        }
    }
}