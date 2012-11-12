using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class OpenChannelPacket : OutgoingPacket
    {
        public OpenChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.OpenChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.OpenChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.OpenChannel;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            OpenChannelPacket p = new OpenChannelPacket(client);
            return p.Send();
        }
    }
}