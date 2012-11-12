using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class GetOutfitPacket : OutgoingPacket
    {
        public GetOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.GetOutfit;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.GetOutfit)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.GetOutfit;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            GetOutfitPacket p = new GetOutfitPacket(client);
            return p.Send();
        }
    }
}