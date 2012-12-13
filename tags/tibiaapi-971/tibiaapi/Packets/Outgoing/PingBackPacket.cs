using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class PingBackPacket : OutgoingPacket
    {
        public PingBackPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.PingBack;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.PingBack)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.PingBack;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            PingBackPacket p = new PingBackPacket(client);
            return p.Send();
        }
    }
}