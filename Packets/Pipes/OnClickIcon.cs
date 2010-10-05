using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class OnClickIconPacket:PipePacket
    {
        public uint IconId { get; set; }


        public OnClickIconPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.OnClickIcon;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.OnClickIcon)
                return false;

            Type = PipePacketType.OnClickIcon;
            IconId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client,5);
            msg.AddByte((byte)Type);

            msg.AddUInt32(IconId);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint iconId)
        {
            OnClickIconPacket p = new OnClickIconPacket(client);

            p.IconId = iconId;

            return p.Send();
        }

    }
}
