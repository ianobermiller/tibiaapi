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

        public MapItemRemovePacket()
        {
            type = PacketType.MapItemRemove;
            destination = PacketDestination.Client;
        }
        public MapItemRemovePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.MapItemRemove) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
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

        public static MapItemRemovePacket Create()
        {
            byte[] packet = new byte[1];

            MapItemRemovePacket p = new MapItemRemovePacket(packet);
            return p;
        }
    }
}
