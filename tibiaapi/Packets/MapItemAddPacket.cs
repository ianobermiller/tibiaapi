using System;
using System.Collections.Generic;
using Tibia.Objects;
using System.Text;

namespace Tibia.Packets
{
    public class MapItemAddPacket : Packet
    {
        private Location loc;
        private Item item;

        public Location Loc
        {
            get { return loc; }
        }

        public Item Item
        {
            get { return item; }
        }

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
                int typen;
                PacketBuilder p = new PacketBuilder(packet);
                loc.X = p.GetInt();
                loc.Y = p.GetInt();
                loc.Z = p.GetByte();
                typen = p.GetInt();
                if (typen == 0x61)
                {
                    //new creature, all info
                }
                else if (typen == 0x62)
                {
                    //Creature, Known ID
                }
                else if (typen == 0x63)
                {
                    //Creature, known ID
                }
                else
                {
                    item = new Item((uint)typen);
                    try
                    {
                        item.Count = p.GetByte();
                    }
                    catch (Exception e) { }
                }
                // Incomplete
                //index = p.Index;
                index = -1;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static MapItemAddPacket Create(Location loc, Item item)
        {
            PacketBuilder pkt = new PacketBuilder();
            pkt.AddInt(9);
            pkt.AddByte((byte)PacketType.MapItemAdd);
            pkt.AddInt(loc.X);
            pkt.AddInt(loc.Y);
            pkt.AddByte((byte)loc.Z);
            pkt.AddInt((int)item.Id);
            if (item.Count >0)
            {
                pkt.AddByte(item.Count);
            }
            MapItemAddPacket p = new MapItemAddPacket(pkt.Data);
            return p;
        }
    }
}
