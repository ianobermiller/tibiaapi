using System;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class NpcTradeGoldCountSaleListPacket : Packet
    {
        private int goldCount;
        public int GoldCount
        {
            get { return goldCount; }
        }

        private List<TradeItem> saleItems;
        public List<TradeItem> SaleItems
        {
            get { return saleItems; }
        }

        public NpcTradeGoldCountSaleListPacket(Client c)
            : base(c)
        {
            type = PacketType.NpcTradeGoldCountSaleList;
            destination = PacketDestination.Client;
        }

        public NpcTradeGoldCountSaleListPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.NpcTradeGoldCountSaleList) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                goldCount = p.GetLong();

                int saleItemCount = p.GetByte();
                saleItems = new List<TradeItem>(saleItemCount);

                for (int i = 0; i < saleItemCount; i++)
                {
                    int id = p.GetInt();
                    byte subtype = p.GetByte();
                    saleItems.Add(new TradeItem(id, subtype));
                }

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static NpcTradeGoldCountSaleListPacket Create(Client c, int goldCount, List<TradeItem> saleItems)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.NpcTradeGoldCountSaleList);
            p.AddLong(goldCount);
            p.AddByte((byte)saleItems.Count);

            foreach (TradeItem t in saleItems)
            {
                p.AddInt(t.Id);
                p.AddByte(t.SubType);
            }

            return new NpcTradeGoldCountSaleListPacket(c, p.GetPacket());
        }
    }
}