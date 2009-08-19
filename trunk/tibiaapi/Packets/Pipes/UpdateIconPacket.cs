using System;
using Tibia.Objects;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Packets.Pipes
{
    public class UpdateIconPacket : AddIconPacket
    {
        public UpdateIconPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.UpdateIcon;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.UpdateIcon)
                return false;

            Type = PipePacketType.UpdateIcon;
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

        public static new bool Send(Objects.Client client, uint iconId, ushort posX, ushort posY, ushort size, ushort itemId, ushort itemCount, ClientFont font, Color color)
        {
            UpdateIconPacket p = new UpdateIconPacket(client);

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


