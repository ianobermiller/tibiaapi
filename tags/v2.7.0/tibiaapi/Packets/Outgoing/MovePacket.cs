using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    Type = OutgoingPacketType.MoveDown;
                    break;
                case Tibia.Constants.Direction.Up:
                    Type = OutgoingPacketType.MoveUp;
                    break;
                case Tibia.Constants.Direction.Right:
                    Type = OutgoingPacketType.MoveRight;
                    break;
                case Tibia.Constants.Direction.Left:
                    Type = OutgoingPacketType.MoveLeft;
                    break;
                case Tibia.Constants.Direction.DownLeft:
                    Type = OutgoingPacketType.MoveDownLeft;
                    break;
                case Tibia.Constants.Direction.DownRight:
                    Type = OutgoingPacketType.MoveDownRight;
                    break;
                case Tibia.Constants.Direction.UpLeft:
                    Type = OutgoingPacketType.MoveUpLeft;
                    break;
                case Tibia.Constants.Direction.UpRight:
                    Type = OutgoingPacketType.MoveUpRight;
                    break;
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
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