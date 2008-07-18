using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class LookAtPacket : Packet
    {
        Location loc;
        int itemId;
        byte stackPos;

        public Location Location
        {
            get { return loc; }
        }

        public int ItemId
        {
            get { return itemId; }
        }

        public byte StackPos
        {
            get { return stackPos; }
        }

        public LookAtPacket(Client c) : base(c)
        {
            type = PacketType.LookAt;
            destination = PacketDestination.Server;
        }

        public LookAtPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.LookAt) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                loc = p.GetLocation();
                itemId = p.GetInt();
                stackPos = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LookAtPacket Create(Client c, Location loc, int itemId, byte stackPos)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.LookAt);
            p.AddLocation(loc);
            p.AddInt(itemId);
            p.AddByte(stackPos);
            return new LookAtPacket(c, p.GetPacket());
        }
    }
}
