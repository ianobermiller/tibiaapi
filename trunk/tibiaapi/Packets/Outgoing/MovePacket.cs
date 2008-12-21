using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class MovePacket : OutgoingPacket
    {
        public Constants.WalkDirection Direction { get; set; }

        public MovePacket(Objects.Client c, Constants.WalkDirection direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Tibia.Constants.WalkDirection.Down:
                    Type = OutgoingPacketType.MoveDown;
                    break;
                case Tibia.Constants.WalkDirection.Up:
                    Type = OutgoingPacketType.MoveUp;
                    break;
                case Tibia.Constants.WalkDirection.Right:
                    Type = OutgoingPacketType.MoveRight;
                    break;
                case Tibia.Constants.WalkDirection.Left:
                    Type = OutgoingPacketType.MoveLeft;
                    break;
                case Tibia.Constants.WalkDirection.DownLeft:
                    Type = OutgoingPacketType.MoveDownLeft;
                    break;
                case Tibia.Constants.WalkDirection.DownRight:
                    Type = OutgoingPacketType.MoveDownRight;
                    break;
                case Tibia.Constants.WalkDirection.UpLeft:
                    Type = OutgoingPacketType.MoveUpLeft;
                    break;
                case Tibia.Constants.WalkDirection.UpRight:
                    Type = OutgoingPacketType.MoveUpRight;
                    break;
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client ,0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Constants.WalkDirection direction)
        {
            MovePacket p = new MovePacket(client, direction);
            return p.Send();
        }


    }
}