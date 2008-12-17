using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class BattleWindowPacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort SpriteId { get; set; }
        public byte StackPosition { get; set; }
        public uint CreatureId { get; set; }

        public BattleWindowPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.BATTLE_WINDOW;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.BATTLE_WINDOW)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.BATTLE_WINDOW;

            FromPosition = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            StackPosition = msg.GetByte();
            CreatureId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(SpriteId);
            msg.AddByte(StackPosition);
            msg.AddUInt32(CreatureId);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte stackPostion,uint creatureId)
        {
            BattleWindowPacket p = new BattleWindowPacket(client);

            p.FromPosition = fromPosition;
            p.SpriteId = spriteId;
            p.StackPosition = stackPostion;
            p.CreatureId = creatureId;

            return p.Send();
        }
    }
}