using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerGoodsPacket : IncomingPacket
    {
        public uint Cash { get; set; }
        public List<ShopInfo> ItemList { get; set; }

        public PlayerGoodsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerGoods;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerGoods)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerGoods;

            Cash = msg.GetUInt32();
            byte count = msg.GetByte();

            ItemList = new List<ShopInfo> { };

            for (int i = 0; i < count; i++)
            {
                ShopInfo item = new ShopInfo();

                item.ItemId = msg.GetUInt16();
                item.CountOrSubType = msg.GetByte();

                ItemList.Add(item);
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Cash);

            msg.AddByte((byte)ItemList.Count);

            foreach (ShopInfo i in ItemList)
            {
                msg.AddUInt16(i.ItemId);
                msg.AddByte(i.CountOrSubType);
            }
        }
    }
}