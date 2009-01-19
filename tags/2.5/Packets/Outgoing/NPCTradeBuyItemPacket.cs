using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Packet sent to the server to indicate which item to buy from the current NPC trade list.
    /// </summary>
    public class NPCTradeBuyItemPacket : Packet
    {
        int itemId;
        byte count;

        public int ItemIdId
        {
            get { return itemId; }
        }

        public byte Count
        {
            get { return count; }
        }

        public NPCTradeBuyItemPacket(Client c)
            : base(c)
        {
            type = PacketType.ItemBuy;
            destination = PacketDestination.Server;
        }

        public NPCTradeBuyItemPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemBuy) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                itemId = p.GetInt();
                count = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static NPCTradeBuyItemPacket Create(Client c, int itemId, byte count)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ItemBuy);
            p.AddInt((int)itemId);
            p.AddByte(0x00);
            p.AddByte(count);
            p.AddByte(0x00);
            p.AddByte(0x00);
            return new NPCTradeBuyItemPacket(c, p.GetPacket());
        }
    }
}
