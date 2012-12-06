using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class CloseNPCChannelPacket : OutgoingPacket
    {
        public CloseNPCChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.CloseNPCChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.CloseNPCChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.CloseNPCChannel;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new CloseNPCChannelPacket(client).Send();
        }
    }
}