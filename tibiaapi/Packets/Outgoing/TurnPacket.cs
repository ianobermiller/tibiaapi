using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    Type = OutgoingPacketType.TurnDown;
                    break;
                case Tibia.Constants.Direction.Up:
                    Type = OutgoingPacketType.TurnUp;
                    break;
                case Tibia.Constants.Direction.Right:
                    Type = OutgoingPacketType.TurnRight;
                    break;
                case Tibia.Constants.Direction.Left:
                    Type = OutgoingPacketType.TurnLeft;
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

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Constants.Direction direction)
        {
            TurnPacket p = new TurnPacket(client, direction);
            return p.Send();
        }
    }
}