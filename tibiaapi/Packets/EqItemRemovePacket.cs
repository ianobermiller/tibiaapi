using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class EqItemRemovePacket:Packet
    {
        private ItemLocation loc;
        public ItemLocation Loc
        {
            get { return loc; }
        }
        public EqItemRemovePacket()
        {
            type = PacketType.EqItemRemove;
            destination = PacketDestination.Client;
        }
        public EqItemRemovePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.EqItemRemove) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                loc = new ItemLocation((Tibia.Constants.SlotNumber)p.GetByte());
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static EqItemRemovePacket Create(Tibia.Constants.SlotNumber slot)
        {
            PacketBuilder p = new PacketBuilder(PacketType.EqItemRemove);
            p.AddByte((byte)slot);
            return new EqItemRemovePacket(p.GetPacket());
        }
    }
}
