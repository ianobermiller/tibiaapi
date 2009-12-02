using Tibia.Constants;

namespace Tibia.Packets
{
    public class OutgoingPacket : Packet
    {
        public OutgoingPacketType Type { get; set; }

        public OutgoingPacket(Objects.Client c)
            : base(c) {}
    }
}
