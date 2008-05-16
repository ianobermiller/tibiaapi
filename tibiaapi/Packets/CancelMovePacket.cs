using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class CancelMovePacket : Packet
    {
        public CancelMovePacket()
        {
            type = PacketType.CancelMove;
            destination = PacketDestination.Server;
        }

        public CancelMovePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CancelMove) return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CancelMovePacket Create()
        {
            PacketBuilder p = new PacketBuilder(PacketType.CancelMove);
            return new CancelMovePacket(p.GetPacket());
        }
    }
}
