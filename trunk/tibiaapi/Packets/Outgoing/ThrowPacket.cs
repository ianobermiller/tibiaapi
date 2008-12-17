using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ThrowPacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public byte Count { get; set; }

        public ThrowPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.THROW;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.THROW)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.THROW;

            FromPosition = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToPosition = msg.GetLocation();
            Count = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte(Count);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte fromStackPostion, Objects.Location toPosition, byte count)
        {
            ThrowPacket p = new ThrowPacket(client);

            p.FromPosition = fromPosition;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToPosition = toPosition;
            p.Count = count;

            return p.Send();
        }
    }
}