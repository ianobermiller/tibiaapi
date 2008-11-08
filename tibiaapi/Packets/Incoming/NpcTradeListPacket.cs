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
                    byte subType = p.GetByte();
                    string name = p.GetString();
                    int weight = p.GetLong();
                    int sellPrice = p.GetLong();
                    int buyPrice = p.GetLong();
                    items.Add(new TradeItem(id, subType, name, weight, sellPrice, buyPrice));
                }
                index = p.Index;
                index = -1;
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
                p.AddByte(item.SubType);
                p.AddString(item.Name);
                p.AddLong(item.Weight);
                p.AddLong(item.SellPrice);
                p.AddLong(item.BuyPrice);
            }
            return new NpcTradeListPacket(c, p.GetPacket());
        }
    }
    public class TradeItem
    {
        private int id;
        private byte subType;
        private string name;
        private int weight;
        private int sellprice;
        private int buyprice;
        public int Id
        {
            get { return id; }
        }
        public byte SubType
        {
            get { return subType; }
        }
        public string Name
        {
            get { return name; }
        }
        public int Weight
        {
            get { return weight; }
        }
        public int SellPrice
        {
            get { return sellprice; }
        }
        public int BuyPrice
        {
            get { return buyprice; }
        }
        public TradeItem(int id, byte subtype, string name, int weight, int sellPrice, int buyPrice)
        {
            this.id = id;
            this.subType = subtype;
            this.name = name;
            this.weight = weight;
            this.sellprice = sellPrice;
            this.buyprice = buyPrice;
        }
    }
}