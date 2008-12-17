using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class UseItemExPacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort FromSpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public ushort ToSpriteId { get; set; }
        public byte ToStackPosition { get; set; }

        public UseItemExPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.USE_ITEM_EX;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.USE_ITEM_EX)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.USE_ITEM_EX;

            FromPosition = msg.GetLocation();
            FromSpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToPosition = msg.GetLocation();
            ToSpriteId = msg.GetUInt16();
            ToStackPosition = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(FromSpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToPosition);
            msg.AddUInt16(ToSpriteId);
            msg.AddByte(ToStackPosition);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort fromSpriteId, byte fromStackPostion, Objects.Location toPosition, ushort toSpriteId, byte toStackPosition)
        {
            UseItemExPacket p = new UseItemExPacket(client);

            p.FromPosition = fromPosition;
            p.FromSpriteId = fromSpriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToPosition = toPosition;
            p.ToSpriteId = toSpriteId;
            p.ToStackPosition = toStackPosition;

            return p.Send();
        }
    }
}