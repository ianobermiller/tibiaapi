using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelClosePrivatePacket : IncomingPacket
    {

        public ushort ChannelId { get; set; }

        public ChannelClosePrivatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelClosePrivate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelClosePrivate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelClosePrivate;

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