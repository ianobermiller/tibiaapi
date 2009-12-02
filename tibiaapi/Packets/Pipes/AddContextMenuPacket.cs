using System;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class AddContextMenuPacket : PipePacket
    {
        public int EventId { get; set; }
        public string Text { get; set; }
        public Constants.ContextMenuType ContextMenuType { get; set; }
        public byte HasSeparator { get; set; }

        public AddContextMenuPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.AddContextMenu;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.AddContextMenu)
                return false;

            Type = PipePacketType.AddContextMenu;
            EventId = (int)msg.GetUInt32();
            Text = msg.GetString();
            ContextMenuType = (Constants.ContextMenuType)msg.GetByte();
            HasSeparator = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 9 + Text.Length);
            msg.AddByte((byte)Type);

            msg.AddUInt32((uint)EventId);
            msg.AddString(Text);
            msg.AddByte((byte)ContextMenuType);
            msg.AddByte(HasSeparator);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, int eventId, string text, Constants.ContextMenuType contextMenuType, bool hasSeparator)
        {
            AddContextMenuPacket p = new AddContextMenuPacket(client);

            p.EventId = eventId;
            p.Text = text;
            p.ContextMenuType = contextMenuType;
            p.HasSeparator = Convert.ToByte(hasSeparator);

            return p.Send();
        }

    }
}



