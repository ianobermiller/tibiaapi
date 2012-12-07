using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class RuleViolationChannelPacket : IncomingPacket
    {
        public ushort ChannelId { get; set; }
        public RuleViolationChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.RuleViolationChannel;

            ChannelId = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(ChannelId);
        }
    }
}
