using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class QuitGamePacket : OutgoingPacket
    {
        public QuitGamePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.QuitGame;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.QuitGame)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.QuitGame;

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