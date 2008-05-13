using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class MapItemRemovePacket : Packet
    {
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
                int index = 3;

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
