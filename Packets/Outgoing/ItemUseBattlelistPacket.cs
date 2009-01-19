using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class ItemUseBattlelistPacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort SpriteId { get; set; }
        public byte StackPosition { get; set; }
        public uint CreatureId { get; set; }

        public ItemUseBattlelistPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUseBattlelist;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUseBattlelist)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUseBattlelist;

            FromPosition = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            StackPosition = msg.GetByte();
            CreatureId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(SpriteId);
            msg.AddByte(StackPosition);
            msg.AddUInt32(CreatureId);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte stackPostion,uint creatureId)
        {
            ItemUseBattlelistPacket p = new ItemUseBattlelistPacket(client);

            p.FromPosition = fromPosition;
            p.SpriteId = spriteId;
            p.StackPosition = stackPostion;
            p.CreatureId = creatureId;

            return p.Send();
        }
    }
}