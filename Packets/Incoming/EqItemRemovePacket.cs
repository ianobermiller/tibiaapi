using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class EqItemRemovePacket:Packet
    {
        private Constants.SlotNumber slot;
        public Constants.SlotNumber Slot
        {
            get { return slot; }
        }
        public EqItemRemovePacket(Client c) : base(c)
        {
            type = PacketType.EqItemRemove;
            destination = PacketDestination.Client;
        }
        public EqItemRemovePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.EqItemRemove) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                slot = (Tibia.Constants.SlotNumber)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static EqItemRemovePacket Create(Client c, Tibia.Constants.SlotNumber slot)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.EqItemRemove);
            p.AddByte((byte)slot);
            return new EqItemRemovePacket(c, p.GetPacket());
        }
    }
}
