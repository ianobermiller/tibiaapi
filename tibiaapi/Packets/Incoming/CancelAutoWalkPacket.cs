using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CancelAutoWalkPacket : Packet
    {
        private byte dir;
        public byte Dir
        {
            get { return dir; }
        }

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
                dir = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CancelAutoWalkPacket Create(Client c,byte dir)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CancelAutoWalk);
            p.AddByte(dir);
            return new CancelAutoWalkPacket(c, p.GetPacket());
        }
    }
}