using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CloseChannelPacket : IncomingPacket
    {

        public ushort ChannelId { get; set; }

        public CloseChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CloseChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CloseChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CloseChannel;

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