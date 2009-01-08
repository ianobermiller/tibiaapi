using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ShopSellPacket : OutgoingPacket
    {
        public ushort ItemId { get; set; }
        public byte Count { get; set; }
        public byte Amount { get; set; }

        public ShopSellPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ShopSell;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ShopSell)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ShopSell;

            ItemId = msg.GetUInt16();
            Count = msg.GetByte();
            Amount = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddUInt16(ItemId);
            msg.AddByte(Count);
            msg.AddByte(Amount);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, ushort itemId, byte count, byte amount)
        {
            ShopSellPacket p = new ShopSellPacket(client);

            p.ItemId = itemId;
            p.Count = count;
            p.Amount = amount;

            return p.Send();
        }
    }
}