using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class NpcChannelClosePacket : OutgoingPacket
    {
        public NpcChannelClosePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.NpcChannelClose;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.NpcChannelClose)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.NpcChannelClose;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            NpcChannelClosePacket p = new NpcChannelClosePacket(client);
            return p.Send();
        }
    }
}