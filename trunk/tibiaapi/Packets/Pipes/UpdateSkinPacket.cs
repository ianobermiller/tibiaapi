using System;
using Tibia.Objects;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Packets.Pipes
{
    public class UpdateSkinPacket : AddSkinPacket
    {
        public UpdateSkinPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.UpdateSkin;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.UpdateSkin)
                return false;

            Type = PipePacketType.UpdateSkin;
            PosX = msg.GetUInt16();
            PosY = msg.GetUInt16();
            Width = msg.GetUInt16();
            Height = msg.GetUInt16();
            SkinId = msg.GetUInt32();

            return true;
        }

        public static new bool Send(Objects.Client client, uint skinId, ushort posX, ushort posY, ushort width, ushort height)
        {
            UpdateSkinPacket p = new UpdateSkinPacket(client);

            p.PosX = posX;
            p.PosY = posY;
            p.Width = width;
            p.Height = height;
            p.SkinId = skinId;

            return p.Send();
        }

    }
}