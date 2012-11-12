using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelEventPacket : IncomingPacket
    {
        public ChannelEventPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelEvent;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelEvent)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelEvent;

            msg.GetUInt16();
            msg.GetString();
            msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}