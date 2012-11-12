using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class GetChannelsPacket : OutgoingPacket
    {
        public GetChannelsPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.GetChannels;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.GetChannels)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.GetChannels;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            GetChannelsPacket p = new GetChannelsPacket(client);
            return p.Send();
        }
    }
}