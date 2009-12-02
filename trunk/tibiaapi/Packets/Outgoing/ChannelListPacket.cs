using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ChannelListPacket : OutgoingPacket
    {
        public ChannelListPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChannelList;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChannelList)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChannelList;

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            ChannelListPacket p = new ChannelListPacket(client);
            return p.Send();
        }
    }
}