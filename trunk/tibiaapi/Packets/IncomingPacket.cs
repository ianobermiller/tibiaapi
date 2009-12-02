using Tibia.Constants;

namespace Tibia.Packets
{
    public class IncomingPacket : Packet
    {
        public IncomingPacketType Type { get; set; }

        public IncomingPacket(Objects.Client c)
            : base(c) {}
    }
}