using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Pipes
{
    public class RemoveSkinPacket : PipePacket
    {
        public uint SkinId { get; set; }

        public RemoveSkinPacket(Objects.Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveSkin;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveSkin)
                return false;

            Type = PipePacketType.RemoveSkin;
            SkinId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 5);
            msg.AddByte((byte)Type);

            msg.AddUInt32(SkinId);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint skinId)
        {
            RemoveSkinPacket p = new RemoveSkinPacket(client);

            p.SkinId = skinId;

            return p.Send();
        }
    }
}