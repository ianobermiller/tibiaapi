using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ItemUsePacket : OutgoingPacket
    {
        public Objects.Location Position { get; set; }
        public ushort SpriteId { get; set; }
        public byte StackPosition { get; set; }
        public byte Index { get; set; }

        public ItemUsePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUse;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUse)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUse;

            Position = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            StackPosition = msg.GetByte();
            Index = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddUInt16(SpriteId);
            msg.AddByte(StackPosition);
            msg.AddByte(Index);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location position, ushort spriteId, byte stackPostion, byte index)
        {
            ItemUsePacket p = new ItemUsePacket(client);
            p.Position = position;
            p.SpriteId = spriteId;
            p.StackPosition = stackPostion;
            p.Index = index;

            return p.Send();
        }
    }
}