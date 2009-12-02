using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class SetOutfitPacket : OutgoingPacket
    {
        public Objects.Outfit Outfit { get; set; }

        public SetOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.SetOutfit;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.SetOutfit)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.SetOutfit;

            Outfit = msg.GetOutfit();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddOutfit(Outfit);
        }

        public static bool Send(Objects.Client client, Objects.Outfit outfit)
        {
            SetOutfitPacket p = new SetOutfitPacket(client);
            p.Outfit = outfit;
            return p.Send();
        }
    }
}