using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveAllTextPacket : PipePacket
    {

        public RemoveAllTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveAllText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveAllText)
                return false;

            Type = PipePacketType.RemoveAllText;

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
            RemoveAllTextPacket p = new RemoveAllTextPacket(client);
            return p.Send();
        }
    }
}




