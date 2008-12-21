using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class SafeTradeRequestAckPacket : IncomingPacket
    {
        public string Name { get; set; }
        public byte Count { get; set; }
        public List<Objects.Item> Items { get; set; }

        public SafeTradeRequestAckPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SafeTradeRequestAck;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SafeTradeRequestAck)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SafeTradeRequestAck;

            try
            {
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
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
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