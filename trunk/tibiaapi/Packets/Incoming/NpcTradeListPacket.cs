using System;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class NpcTradeListPacket : Packet
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
        public NpcTradeListPacket(Client c)
            : base(c)
        {
            type = PacketType.NpcTradeList;
            destination = PacketDestination.Client;
        }
        public NpcTradeListPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.NpcTradeList) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                itemCount = p.GetByte();
                items = new List<TradeItem>(itemCount);
                for (int i = 0; i < itemCount; i++)
                {
                    int id = p.GetInt();
                    p.Skip(1);
                    string name = p.GetString();
                    int sellPrice = p.GetLong();
                    int buyPrice = p.GetLong();
                    items.Add(new TradeItem(id, name, sellPrice, buyPrice));
                }
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static NpcTradeListPacket Create(Client c, List<TradeItem> items)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.NpcTradeList);
            p.AddByte((byte)items.Count);
            foreach(TradeItem item in items)
            {
                p.AddShort(item.Id);
                p.Skip(1);
                p.AddString(item.Name);
                p.AddLong(item.SellPrice);
                p.AddLong(item.BuyPrice);
            }
            return new NpcTradeListPacket(c, p.GetPacket());
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