using System;
using Tibia.Objects;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Packets.Pipes
{
    public class AddIconPacket : PipePacket
    {
        public uint IconId { get; set; }
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public ushort Size { get; set; }
        public ushort ItemId { get; set; }
        public ushort ItemCount { get; set; }
        public ClientFont Font { get; set; }
        public Color Color { get; set; }

        public AddIconPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.AddIcon;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.AddIcon)
                return false;

            Type = PipePacketType.AddIcon;
            IconId = msg.GetUInt32();
            PosX = msg.GetUInt16();
            PosY = msg.GetUInt16();
            Size = msg.GetUInt16();
            ItemId = msg.GetUInt16();
            ItemCount = msg.GetUInt16();
            Font = (ClientFont)msg.GetByte();
            Color = Color.FromArgb(msg.GetByte(), msg.GetByte(), msg.GetByte());

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client,21);
            msg.AddByte((byte)Type);

            msg.AddUInt32(IconId);
            msg.AddUInt16(PosX);
            msg.AddUInt16(PosY);
            msg.AddUInt16(Size);
            msg.AddUInt16(ItemId);
            msg.AddUInt16(ItemCount);
            msg.AddUInt16(ItemId);
            msg.AddByte(Convert.ToByte(Font));
            msg.AddByte(Color.R);
            msg.AddByte(Color.G);
            msg.AddByte(Color.B);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint iconId, ushort posX, ushort posY, ushort size, ushort itemId, ushort itemCount, ClientFont font, Color color)
        {
            AddIconPacket p = new AddIconPacket(client);

            p.IconId = iconId;
            p.PosX = posX;
            p.PosY = posY;
            p.Size = size;
            p.ItemId = itemId;
            p.ItemCount = itemCount;
            p.Font = font;
            p.Color = color;

            return p.Send();
        }

    }
}



