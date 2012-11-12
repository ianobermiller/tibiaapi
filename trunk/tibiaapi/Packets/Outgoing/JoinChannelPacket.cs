using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class JoinChannelPacket : OutgoingPacket
    {

        public ChatChannel ChannelId { get; set; }

        public JoinChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.JoinChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.JoinChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.JoinChannel;

            ChannelId = (ChatChannel)msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)ChannelId);
        }

        public static bool Send(Objects.Client client, ChatChannel channel)
        {
            JoinChannelPacket p = new JoinChannelPacket(client);
            p.ChannelId = channel;
            return p.Send();
        }
    }
}