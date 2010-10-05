using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class VipLoginPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public VipLoginPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.VipLogin;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.VipLogin)
                return false;

            Destination = destination;
            Type = IncomingPacketType.VipLogin;

            PlayerId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(PlayerId);
        }
    }
}