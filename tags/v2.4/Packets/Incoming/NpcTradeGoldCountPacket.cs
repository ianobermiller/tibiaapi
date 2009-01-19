using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class NpcTradeGoldCountPacket : Packet
    {
        private int goldCount;
        public int GoldCount
        {
            get { return goldCount; }
        }

        public NpcTradeGoldCountPacket(Client c)
            : base(c)
        {
            type = PacketType.NpcTradeGoldCount;
            destination = PacketDestination.Client;
        }

        public NpcTradeGoldCountPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.NpcTradeGoldCount) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                goldCount = p.GetInt();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static NpcTradeGoldCountPacket Create(Client c, int goldCount)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.NpcTradeGoldCount);
            p.AddInt(goldCount);
            return new NpcTradeGoldCountPacket(c, p.GetPacket());
        }
    }
}