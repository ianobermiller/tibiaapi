using Tibia.Constants;

namespace Tibia.Packets.Pipes
{
    public class RemoveAllIconsPacket:PipePacket
    {
        public RemoveAllIconsPacket(Objects.Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveAllIcons;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveAllIcons)
                return false;

            Type = PipePacketType.RemoveAllIcons;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 1);
            msg.AddByte((byte)Type);
            return msg.Data;
        }

        public static bool Send(Objects.Client client)
        {
            RemoveAllIconsPacket p = new RemoveAllIconsPacket(client);
            return p.Send();
        }
    }
}
