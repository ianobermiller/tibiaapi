using System;
using System.Collections.Generic;
using Tibia.Objects;
using System.Text;

namespace Tibia.Packets
{
    public class MapItemAddPacket : Packet
    {
        public MapItemAddPacket()
        {
            type = PacketType.MapItemAdd;
            destination = PacketDestination.Client;
        }
        public MapItemAddPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.MapItemAdd) return false;
                int index = 3;

                return true;
            }
            else 
            {
                return false;
            }
        }

        public static MapItemAddPacket Create(int icon, byte number, string name, int volume, List<Item> items)
        {
            byte[] packet = new byte[1];


            MapItemAddPacket p = new MapItemAddPacket(packet);
            return p;
        }
    }
}
