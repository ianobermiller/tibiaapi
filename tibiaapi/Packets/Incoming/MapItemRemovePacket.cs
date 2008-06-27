using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class MapItemRemovePacket : Packet
    {
        private Location from;
        private int stackPos;

        public Location From
        {
            get { return from; }
        }

        public int StackPos
        {
            get { return StackPos; }
        }

        public MapItemRemovePacket(Client c) : base(c)
        {
            type = PacketType.MapItemRemove;
            destination = PacketDestination.Client;
        }
        public MapItemRemovePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.MapItemRemove) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                from = p.GetLocation();
                stackPos = p.GetByte();
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static MapItemRemovePacket Create(Client c, Location from, byte fromStackPosition)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.MapItemRemove);
            p.AddLocation(from);
            p.AddByte(fromStackPosition);
            return new MapItemRemovePacket(c, p.GetPacket());
        }
    }
}
