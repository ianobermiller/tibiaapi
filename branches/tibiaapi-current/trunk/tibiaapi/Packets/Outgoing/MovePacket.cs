using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MovePacket : OutgoingPacket
    {
        public Constants.Direction Direction { get; set; }

        public MovePacket(Objects.Client c, Constants.Direction direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Tibia.Constants.Direction.Down:
                    Type = OutgoingPacketType.GoSouth;
                    break;
                case Tibia.Constants.Direction.Up:
                    Type = OutgoingPacketType.GoNorth;
                    break;
                case Tibia.Constants.Direction.Right:
                    Type = OutgoingPacketType.GoEast;
                    break;
                case Tibia.Constants.Direction.Left:
                    Type = OutgoingPacketType.GoWest;
                    break;
                case Tibia.Constants.Direction.DownLeft:
                    Type = OutgoingPacketType.GoSouthWest;
                    break;
                case Tibia.Constants.Direction.DownRight:
                    Type = OutgoingPacketType.GoSouthEast;
                    break;
                case Tibia.Constants.Direction.UpLeft:
                    Type = OutgoingPacketType.GoNorthWest;
                    break;
                case Tibia.Constants.Direction.UpRight:
                    Type = OutgoingPacketType.GoNorthEast;
                    break;
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
            MovePacket p = new MovePacket(client, direction);
            return p.Send();
        }
    }
}