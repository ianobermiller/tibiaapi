using Tibia.Constants;

namespace Tibia.Packets
{
    public class OutgoingPacket : Packet
    {
        public OutgoingPacketType Type { get; set; }

        public OutgoingPacket(Objects.Client c)
            : base(c) { }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            throw new System.NotImplementedException();
        }
    }
}
