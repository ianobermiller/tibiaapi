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
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client)
        {
            RemoveAllTextPacket p = new RemoveAllTextPacket(client);
            return p.Send();
        }
    }
}




