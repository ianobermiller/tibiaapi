using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RequestOutfitPacket : OutgoingPacket
    {
        public RequestOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RequestOutfit;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RequestOutfit)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RequestOutfit;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new RequestOutfitPacket(client).Send();
        }
    }
}