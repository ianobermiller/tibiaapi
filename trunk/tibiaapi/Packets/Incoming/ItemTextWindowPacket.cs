using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ItemTextWindowPacket : IncomingPacket
    {
        public uint WindowId { get; set; }
        public ushort ItemId { get; set; }
        public ushort MaxLength { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }

        public ItemTextWindowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ItemTextWindow;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ItemTextWindow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ItemTextWindow;

            WindowId = msg.GetUInt32();
            ItemId = msg.GetUInt16();
            MaxLength = msg.GetUInt16();
            Text = msg.GetString();
            Author = msg.GetString();
            Date = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(WindowId);
            msg.AddUInt16(ItemId);
            msg.AddUInt16(MaxLength);
            msg.AddString(Text);
            msg.AddString(Author);
            msg.AddString(Date);
        }
    }
}