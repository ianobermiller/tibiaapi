using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ShareExperiencePacket : OutgoingPacket
    {
        public bool Enabled { get; set; }

        public ShareExperiencePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ShareExperience;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ShareExperience)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ShareExperience;

            Enabled = System.Convert.ToBoolean(msg.GetByte());

            if (Client.VersionNumber < 910)
                msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(System.Convert.ToByte(Enabled));

            if (Client.VersionNumber < 910)
                msg.AddByte(0);

        }

        public static bool Send(Objects.Client client, bool enabled)
        {
            return new ShareExperiencePacket(client) { Enabled = enabled }.Send();
        }
    }
}