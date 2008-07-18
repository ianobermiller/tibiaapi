using System;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class TradeBoxOpenPacket : Packet
    {
        private byte itemCount;
        private List<TradeItem> items;
        public byte ItemCount
        {
            get { return itemCount; }
        }
        public List<TradeItem> Items
        {
            get { return items; }
        }
        public TradeBoxOpenPacket(Client c)
            : base(c)
        {
            type = PacketType.TradeBoxOpen;
            destination = PacketDestination.Client;
        }
        public TradeBoxOpenPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.TradeBoxOpen) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                itemCount = p.GetByte();
                items = new List<TradeItem>(itemCount);
                for (int i = 0; i < itemCount; i++)
                {
                    int tid = p.GetInt();
                    p.Skip(1);
                    string tname = p.GetString();
                    int tsprice = p.GetLong();
                    int tbprice = p.GetLong();
                    items.Add(new TradeItem(tid, tname, tsprice, tbprice));
                }
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static TradeBoxOpenPacket Create(Client c, List<TradeItem> items)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.TradeBoxOpen);
            p.AddByte((byte)items.Count);
            foreach(TradeItem item in items)
            {
                p.AddInt(item.Id);
                p.Skip(1);
                p.AddString(item.Name);
                p.AddLong(item.SellPrice);
                p.AddLong(item.BuyPrice);
            }
            return new TradeBoxOpenPacket(c, p.GetPacket());
        }
    }
    public class TradeItem
    {
        private int id;
        private string name;
        private int sellprice;
        private int buyprice;
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public int SellPrice
        {
            get { return sellprice; }
        }
        public int BuyPrice
        {
            get { return buyprice; }
        }
        public TradeItem(int Id, string Name, int SellPrice, int BuyPrice)
        {
            id = Id;
            name = Name;
            sellprice = SellPrice;
            buyprice = BuyPrice;
        }
    }
}