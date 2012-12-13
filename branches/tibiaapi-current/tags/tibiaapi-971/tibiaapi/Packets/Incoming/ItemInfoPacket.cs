using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ItemInfoPacket : IncomingPacket
    {
        public class ItemInfo
        {
            public ushort Id { get; set; }
            public byte CountOrSubType { get; set; }
            public string Description { get; set; }
        }

        public System.Collections.Generic.List<ItemInfo> Items { get; set; }

        public ItemInfoPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ItemInfo;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ItemInfo)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ItemInfo;

            byte Count = msg.GetByte();
            Items = new System.Collections.Generic.List<ItemInfo>(Count);

            for (int i = 0; i < Count; i++)
            {
                Items.Add(new ItemInfo
                {
                    Id = msg.GetUInt16(),
                    CountOrSubType = msg.GetByte(),
                    Description = msg.GetString()
                });
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)Items.Count);
            foreach (ItemInfo ii in Items)
            {
                msg.AddUInt16(ii.Id);
                msg.AddByte(ii.CountOrSubType);
                msg.AddString(ii.Description);
            }
        }
    }
}