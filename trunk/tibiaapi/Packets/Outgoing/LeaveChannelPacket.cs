using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class LeaveChannelPacket : OutgoingPacket
    {

        public ChatChannel ChannelId { get; set; }

        public LeaveChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.LeaveChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.LeaveChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.LeaveChannel;

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
            LeaveChannelPacket p = new LeaveChannelPacket(client);
            p.ChannelId = channel;
            return p.Send();
        }

    }
}