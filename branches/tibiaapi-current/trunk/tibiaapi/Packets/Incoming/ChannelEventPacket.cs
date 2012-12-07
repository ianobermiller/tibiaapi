using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelEventPacket : IncomingPacket
    {
        public ushort ChannelId { get; set; }
        public string PlayerName { get; set; }
        public byte EventType { get; set; }

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

            ChannelId = msg.GetUInt16();
            PlayerName = msg.GetString();
            EventType = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(ChannelId);
            msg.AddString(PlayerName);
            msg.AddByte(EventType);
        }
    }
}