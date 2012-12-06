using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RequestChannelsPacket : OutgoingPacket
    {
        public RequestChannelsPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RequestChannels;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RequestChannels)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RequestChannels;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new RequestChannelsPacket(client).Send();
        }
    }
}