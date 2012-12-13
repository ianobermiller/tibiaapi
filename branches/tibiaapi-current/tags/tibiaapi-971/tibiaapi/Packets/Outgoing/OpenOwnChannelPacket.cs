using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class OpenOwnChannelPacket : OutgoingPacket
    {
        public OpenOwnChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.OpenOwnChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.OpenOwnChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.OpenOwnChannel;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new OpenOwnChannelPacket(client).Send();
        }
    }
}