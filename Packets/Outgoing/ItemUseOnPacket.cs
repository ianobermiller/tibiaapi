using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ItemUseOnPacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort FromSpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public ushort ToSpriteId { get; set; }
        public byte ToStackPosition { get; set; }

        public ItemUseOnPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUseOn;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUseOn)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUseOn;

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
            NetworkMessage msg = new NetworkMessage(Client, 0);

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
            ItemUseOnPacket p = new ItemUseOnPacket(client);

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