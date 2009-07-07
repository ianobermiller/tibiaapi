using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveAllContextMenusPacket : PipePacket
    {

        public RemoveAllContextMenusPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveAllContextMenus;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveAllContextMenus)
                return false;

            Type = PipePacketType.RemoveAllContextMenus;

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
            RemoveAllContextMenusPacket p = new RemoveAllContextMenusPacket(client);
            return p.Send();
        }

    }
}




