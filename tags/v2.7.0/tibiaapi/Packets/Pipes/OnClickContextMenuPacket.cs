using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class OnClickContextMenuPacket : PipePacket
    {
        public int EventId { get; set; }

        public OnClickContextMenuPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.OnClickContextMenu;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.OnClickContextMenu)
                return false;

            Type = PipePacketType.OnClickContextMenu;
            EventId = (int)msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 5);
            msg.AddByte((byte)Type);

            msg.AddUInt32((uint)EventId);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, int eventId)
        {
            AddContextMenuPacket p = new AddContextMenuPacket(client);
            p.EventId = eventId;
            return p.Send();
        }

    }
}




