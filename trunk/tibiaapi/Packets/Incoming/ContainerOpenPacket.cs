using System;
using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ContainerOpenPacket : IncomingPacket
    {

        public ushort ItemId { get; set; }
        public string Name { get; set; }
        public byte Id { get; set; }
        public byte Capacity { get; set; }
        public byte HasParent { get; set; }
        public byte ItemCount { get; set; }
        public List<Objects.Item> Items { get; set; }

        public ContainerOpenPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerOpen;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerOpen)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerOpen;

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

        public override void ToNetworkMessage(ref NetworkMessage msg)
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

        public static bool Send(Objects.Client client, byte id, ushort itemId, string name, byte capacity, byte hasParent, List<Objects.Item> items)
        {
            ContainerOpenPacket p = new ContainerOpenPacket(client);

            p.Id = id;
            p.ItemId = itemId;
            p.Name = name;
            p.Capacity = capacity;
            p.HasParent = hasParent;
            p.Items = items;

            return p.Send();
        }
    }
}