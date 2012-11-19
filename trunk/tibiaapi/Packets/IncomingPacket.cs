using Tibia.Constants;

namespace Tibia.Packets
{
    public class IncomingPacket : Packet
    {
        public IncomingPacketType Type { get; set; }

        public IncomingPacket(Objects.Client c)
            : base(c) { }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            throw new System.NotImplementedException();
        }
    }
}