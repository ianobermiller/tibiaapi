using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class OwnTradePacket : IncomingPacket
    {
        public string Name { get; set; }
        public byte Count { get; set; }
        public List<Objects.Item> Items { get; set; }

        public OwnTradePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OwnTrade;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OwnTrade)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OwnTrade;

            Name = msg.GetString();
            Count = msg.GetByte();

            Items = new List<Tibia.Objects.Item>(Count);

            for (int i = 0; i < Count; i++)
            {
                Objects.Item item = msg.GetItem();

                Items.Add(item);
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddString(Name);
            msg.AddByte((byte)Items.Count);

            foreach (Objects.Item i in Items)
            {
                msg.AddItem(i);
            }
        }
    }
}