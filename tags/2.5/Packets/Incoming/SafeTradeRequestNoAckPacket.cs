using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class SafeTradeRequestNoAckPacket : IncomingPacket
    {
        public string Name { get; set; }
        public byte Count { get; set; }
        public List<Objects.Item> Items { get; set; }

        public SafeTradeRequestNoAckPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SafeTradeRequestNoAck;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.SafeTradeRequestNoAck)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SafeTradeRequestNoAck;

            Name = msg.GetString();
            Count = msg.GetByte();

            Items = new List<Tibia.Objects.Item>(Count);

            for (int i = 0; i < Count; i++)
            {
                Objects.Item item = new Tibia.Objects.Item(Client, msg.GetUInt16(), 0);

                if (item.HasExtraByte)
                    item.Count = msg.GetByte();

                Items.Add(item);
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddString(Name);
            msg.AddByte((byte)Items.Count);

            foreach (Objects.Item i in Items)
            {
                msg.AddUInt16((ushort)i.Id);

                if (i.HasExtraByte)
                    msg.AddByte(i.Count);
            }

            return msg.Packet;
        }
    }
}