using Tibia.Constants;

namespace Tibia.Packets.Pipes
{
    public class RemoveIconPacket : PipePacket
    {
        public uint IconId { get; set; }

        public RemoveIconPacket(Objects.Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveIcon;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveIcon)
                return false;

            Type = PipePacketType.RemoveIcon;
            IconId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 5);
            msg.AddByte((byte)Type);

            msg.AddUInt32(IconId);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint iconId)
        {
            RemoveIconPacket p = new RemoveIconPacket(client);

            p.IconId = iconId;

            return p.Send();
        }
    }
}