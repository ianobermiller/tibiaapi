using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ChangeOutfitPacket : OutgoingPacket
    {
        public Objects.Outfit Outfit { get; set; }

        public ChangeOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChangeOutfit;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChangeOutfit)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChangeOutfit;

            Outfit = msg.GetOutfit();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddOutfit(Outfit);
        }

        public static bool Send(Objects.Client client, Objects.Outfit outfit)
        {
            return new ChangeOutfitPacket(client) { Outfit = outfit }.Send();
        }
    }
}