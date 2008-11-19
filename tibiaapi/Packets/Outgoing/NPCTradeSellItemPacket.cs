using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Packet sent to the server to indicate which item to sell to the current NPC.
    /// </summary>
    public class NPCTradeSellItemPacket : Packet
    {
        int itemId;
        byte count;

        public int ItemId
        {
            get { return itemId; }
        }

        public byte Count
        {
            get { return count; }
        }

        public NPCTradeSellItemPacket(Client c)
            : base(c)
        {
            type = PacketType.ItemSell;
            destination = PacketDestination.Server;
        }

        public NPCTradeSellItemPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ItemSell) return false;
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

        public static NPCTradeSellItemPacket Create(Client c, int itemId, byte count)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ItemSell);
            p.AddInt((int)itemId);
            p.AddByte(0x00);
            p.AddByte(count);
            p.AddByte(0x01);
            return new NPCTradeSellItemPacket(c, p.GetPacket());
        }
    }
}
