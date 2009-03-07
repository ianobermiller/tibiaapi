using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveTextPacket : PipePacket
    {
        public string TextName { get; set; }

        public RemoveTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveText)
                return false;

            Type = PipePacketType.RemoveText;
            TextName = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddString(TextName);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, string textName)
        {
            RemoveTextPacket p = new RemoveTextPacket(client);
            p.TextName = textName;
            return p.Send();
        }

    }
}
