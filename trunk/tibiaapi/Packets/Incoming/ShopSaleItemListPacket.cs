using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ShopSaleItemListPacket : IncomingPacket
    {

        public uint Cash { get; set; }
        public List<ShopInfo> ItemList { get; set; }

        public ShopSaleItemListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.SHOP_SALE_ITEM_LIST;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.SHOP_SALE_ITEM_LIST)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.SHOP_SALE_ITEM_LIST;

            Cash = msg.GetUInt32();
            byte count = msg.GetByte();

            ItemList = new List<ShopInfo> { };

            for (int i = 0; i < count; i++)
            {
                ShopInfo item = new ShopInfo();

                item.ItemId = msg.GetUInt16();
                item.SubType = msg.GetByte();
            }


            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddUInt32(Cash);

            msg.AddByte((byte)ItemList.Count);

            foreach (ShopInfo i in ItemList)
            {
                msg.AddUInt16(i.ItemId);
                msg.AddByte(i.SubType);
            }

            return msg.Packet;
        }
    }
}