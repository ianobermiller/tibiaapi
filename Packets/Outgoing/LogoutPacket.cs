using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class LogoutPacket : OutgoingPacket
    {
        public LogoutPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Logout;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Logout)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Logout;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            LogoutPacket p = new LogoutPacket(client);
            return p.Send();
        }
    }
}