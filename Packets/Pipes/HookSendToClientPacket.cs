using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class HookSendToClientPacket : PipePacket
    {
        public byte[] PacketToSend { get; set; }

        public HookSendToClientPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.HookSendToClient;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.HookSendToClient)
                return false;

            Type = PipePacketType.HookSendToClient;
            PacketToSend = msg.GetBytes(msg.Length - 1);

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 1 + PacketToSend.Length);
            msg.AddByte((byte)Type);
            msg.AddBytes(PacketToSend);
            return msg.Data;
        }

        public static bool Send(Objects.Client client, byte[] packet)
        {
            HookSendToClientPacket p = new HookSendToClientPacket(client);

            p.PacketToSend = packet;

            return p.Send();
        }

    }
}
