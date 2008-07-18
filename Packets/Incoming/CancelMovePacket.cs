using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CancelMovePacket : Packet
    {
        public CancelMovePacket(Client c) : base(c)
        {
            type = PacketType.CancelMove;
            destination = PacketDestination.Server;
        }

        public CancelMovePacket(Client c, byte[] data)
            : this(c)
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

        public static CancelMovePacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CancelMove);
            return new CancelMovePacket(c, p.GetPacket());
        }
    }
}
