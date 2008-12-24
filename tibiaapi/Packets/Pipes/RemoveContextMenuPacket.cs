using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveContextMenuPacket : PipePacket
    {
        public int EventId { get; set; }
        public string Text { get; set; }
        public ContextMenu.Type ContextMenuType { get; set; }
        public byte HasSeparator { get; set; }

        public RemoveContextMenuPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveContextMenu;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveContextMenu)
                return false;

            Type = PipePacketType.RemoveContextMenu;
            EventId = (int)msg.GetUInt32();
            Text = msg.GetString();
            ContextMenuType = (ContextMenu.Type)msg.GetByte();
            HasSeparator = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddUInt32((uint)EventId);
            msg.AddString(Text);
            msg.AddByte((byte)ContextMenuType);
            msg.AddByte(HasSeparator);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, int eventId, string text, ContextMenu.Type contextMenuType, bool hasSeparator)
        {
            RemoveContextMenuPacket p = new RemoveContextMenuPacket(client);

            p.EventId = eventId;
            p.Text = text;
            p.ContextMenuType = contextMenuType;
            p.HasSeparator = Convert.ToByte(hasSeparator);

            return p.Send();
        }

    }
}



