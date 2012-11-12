using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class OwnOfferPacket : IncomingPacket
    {
        public string Name { get; set; }
        public byte Count { get; set; }
        public List<Objects.Item> Items { get; set; }

        public OwnOfferPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OwnOffer;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OwnOffer)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OwnOffer;

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