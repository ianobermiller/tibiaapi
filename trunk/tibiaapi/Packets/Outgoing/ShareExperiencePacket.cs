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

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(System.Convert.ToByte(Enabled));
        }

        public static bool Send(Objects.Client client, bool enabled)
        {
            ShareExperiencePacket p = new ShareExperiencePacket(client);
            p.Enabled = enabled;
            return p.Send();
        }
    }
}