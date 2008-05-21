using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CancelAutoWalkPacket : Packet
    {
        public CancelAutoWalkPacket(Client c)
            : base(c)
        {
            type = PacketType.CancelAutoWalk;
            destination = PacketDestination.Client;
        }

        public CancelAutoWalkPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CancelAutoWalk) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CancelAutoWalkPacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CancelAutoWalk);
            return new CancelAutoWalkPacket(c, p.GetPacket());
        }
    }
}