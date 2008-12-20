using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class TurnPacket : OutgoingPacket
    {
        public Constants.TurnDirection Direction { get; set; }

        public TurnPacket(Objects.Client c, Constants.TurnDirection direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Tibia.Constants.TurnDirection.Down:
                    Type = OutgoingPacketType.TurnDown;
                    break;
                case Tibia.Constants.TurnDirection.Up:
                    Type = OutgoingPacketType.TurnUp;
                    break;
                case Tibia.Constants.TurnDirection.Right:
                    Type = OutgoingPacketType.TurnRight;
                    break;
                case Tibia.Constants.TurnDirection.Left:
                    Type = OutgoingPacketType.TurnLeft;
                    break;
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Constants.TurnDirection direction)
        {
            TurnPacket p = new TurnPacket(client, direction);
            return p.Send();
        }


    }
}