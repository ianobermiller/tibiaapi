using System;
using Tibia.Objects;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Packets.Pipes
{
    public class AddSkinPacket : PipePacket
    {
        public uint SkinId { get; set; }
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public ushort GUIId { get; set; }

        public AddSkinPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.AddSkin;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.AddSkin)
                return false;

            Type = PipePacketType.AddSkin;
            SkinId = msg.GetUInt32();
            PosX = msg.GetUInt16();
            PosY = msg.GetUInt16();
            Width = msg.GetUInt16();
            Height = msg.GetUInt16();
            GUIId = msg.GetUInt16();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 15);
            msg.AddByte((byte)Type);

            msg.AddUInt32(SkinId);
            msg.AddUInt16(PosX);
            msg.AddUInt16(PosY);
            msg.AddUInt16(Width);
            msg.AddUInt16(Height);
            msg.AddUInt16(GUIId);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint skinId, ushort posX, ushort posY, ushort width, ushort height, ushort guiId)
        {
            AddSkinPacket p = new AddSkinPacket(client);

            p.SkinId = skinId;
            p.PosX = posX;
            p.PosY = posY;
            p.Width = width;
            p.Height = height;
            p.GUIId = guiId;

            return p.Send();
        }
    }
}
