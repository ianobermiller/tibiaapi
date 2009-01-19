using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class AutoWalkPacket : OutgoingPacket
    {
        public List<Constants.WalkDirection> Directions { get; set; }

        public AutoWalkPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AutoWalk;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AutoWalk)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AutoWalk;

            Directions = new List<Tibia.Constants.WalkDirection> { };
            byte count = msg.GetByte();

            for (int i = 0; i < count; i++)
            {
                Constants.WalkDirection direction;
                byte dir = msg.GetByte();

                switch (dir)
                {
                    case 1: direction = Tibia.Constants.WalkDirection.Right; break;
                    case 2: direction = Tibia.Constants.WalkDirection.UpRight; break;
                    case 3: direction = Tibia.Constants.WalkDirection.Up; break;
                    case 4: direction = Tibia.Constants.WalkDirection.UpLeft; break;
                    case 5: direction = Tibia.Constants.WalkDirection.Left; break;
                    case 6: direction = Tibia.Constants.WalkDirection.DownLeft; break;
                    case 7: direction = Tibia.Constants.WalkDirection.Down; break;
                    case 8: direction = Tibia.Constants.WalkDirection.DownRight; break;
                    default: continue;
                }

                Directions.Add(direction);
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddByte((byte)Directions.Count);

            foreach (var dir in Directions)
            {
                switch (dir)
                {
                    case Tibia.Constants.WalkDirection.Right: msg.AddByte(1); break;
                    case Tibia.Constants.WalkDirection.UpRight: msg.AddByte(2); break;
                    case Tibia.Constants.WalkDirection.Up: msg.AddByte(3);break;
                    case Tibia.Constants.WalkDirection.UpLeft: msg.AddByte(4); break;
                    case Tibia.Constants.WalkDirection.Left: msg.AddByte(5); break;
                    case Tibia.Constants.WalkDirection.DownLeft: msg.AddByte(6);break;
                    case Tibia.Constants.WalkDirection.Down: msg.AddByte(7); break;
                    case Tibia.Constants.WalkDirection.DownRight: msg.AddByte(8); break;
                    default: continue;
                }
            }

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, List<Constants.WalkDirection> directions)
        {
            AutoWalkPacket p = new AutoWalkPacket(client);
            p.Directions = directions;
            return p.Send();
        }
    }
}