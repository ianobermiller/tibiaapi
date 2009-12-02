using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ShopClosePacket : OutgoingPacket
    {
        public ShopClosePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ShopClose;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ShopClose)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ShopClose;

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            ShopClosePacket p = new ShopClosePacket(client);
            return p.Send();
        }
    }
}