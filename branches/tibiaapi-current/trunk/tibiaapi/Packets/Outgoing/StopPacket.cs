using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class StopPacket : OutgoingPacket
    {
        public StopPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Stop;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Stop)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Stop;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            QuitGamePacket p = new QuitGamePacket(client);
            return p.Send();
        }
    }
}