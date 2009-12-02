using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ShopWindowClosePacket : IncomingPacket
    {

        public ShopWindowClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ShopWindowClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ShopWindowClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ShopWindowClose;

            //no data

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}