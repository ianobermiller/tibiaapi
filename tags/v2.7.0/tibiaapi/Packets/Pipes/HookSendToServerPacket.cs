using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class HookSendToServerPacket : PipePacket
    {
        public byte[] PacketToSend { get; set; }

        public HookSendToServerPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.HookSendToServer;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.HookSendToServer)
                return false;

            Type = PipePacketType.HookSendToServer;
            ushort InnerLength = msg.GetUInt16();
            msg.Position -= 2;
            PacketToSend = msg.GetBytes(InnerLength+2);

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
            HookSendToServerPacket p = new HookSendToServerPacket(client);

            p.PacketToSend = packet;

            return p.Send();
        }

    }
}
