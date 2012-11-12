using System;
using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class TurnPacket : OutgoingPacket
    {
        public Constants.Direction Direction { get; set; }

        public TurnPacket(Objects.Client c, Constants.Direction direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Tibia.Constants.Direction.Down:
                    Type = OutgoingPacketType.RotateSouth;
                    break;
                case Tibia.Constants.Direction.Up:
                    Type = OutgoingPacketType.RotateNorth;
                    break;
                case Tibia.Constants.Direction.Right:
                    Type = OutgoingPacketType.RotateEast;
                    break;
                case Tibia.Constants.Direction.Left:
                    Type = OutgoingPacketType.RotateWest;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "direction",
                        "Valid directions for turning are Up, Right, Down, and Left.");
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
            TurnPacket p = new TurnPacket(client, direction);
            return p.Send();
        }
    }
}