using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ContainerPacket : IncomingPacket
    {
        public ushort ItemId { get; set; }
        public string Name { get; set; }
        public byte Id { get; set; }
        public byte Capacity { get; set; }
        public byte HasParent { get; set; }
        public byte ItemCount { get; set; }
        public List<Objects.Item> Items { get; set; }

        public ContainerPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Container;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Container)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Container;

            Id = msg.GetByte();
            ItemId = msg.GetUInt16();
            Name = msg.GetString();
            Capacity = msg.GetByte();
            HasParent = msg.GetByte();
            ItemCount = msg.GetByte();

            Items = new List<Tibia.Objects.Item>(ItemCount);

            for (int i = 0; i < ItemCount; i++)
            {
                Objects.Item item = msg.GetItem();
                item.Location = Tibia.Objects.ItemLocation.FromContainer(Id, (byte)i);
                Items.Add(item);
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddByte(Id);
            msg.AddUInt16(ItemId);
            msg.AddString(Name);
            msg.AddByte(Capacity);
            msg.AddByte(HasParent);
            msg.AddByte((byte)Items.Count);


            foreach (Objects.Item i in Items)
            {
                msg.AddItem(i);
            }
        }
    }
}