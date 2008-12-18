using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class TileAddThingPacket : IncomingPacket
    {
        public Objects.Location Position { get; set; }
        public Objects.Item Item { get; set; }
        public PacketCreature Creature { get; set; }
        public ushort ThingId { get; set; }

        public TileAddThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.TILE_ADD_THING;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.TILE_ADD_THING)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.TILE_ADD_THING;

            Position = msg.GetLocation();
            ThingId = msg.GetUInt16();

            if (ThingId == 0x0061 || ThingId == 0x0062)
            {
                Creature = new PacketCreature(Client);

                if (ThingId == 0x0062)
                {
                    Creature.Type = PacketCreatureType_t.KNOW;
                    Creature.Id = msg.GetUInt32();
                }
                else if (ThingId == 0x0061)
                {
                    Creature.Type = PacketCreatureType_t.UNKNOW;
                    Creature.RemoveId = msg.GetUInt32();
                    Creature.Id = msg.GetUInt32();
                    Creature.Name = msg.GetString();
                }

                Creature.Health = msg.GetByte();
                Creature.Direction = msg.GetByte();

                Creature.Outfit = msg.GetOutfit();

                Creature.LightLevel = msg.GetByte();
                Creature.LightColor = msg.GetByte();

                Creature.Speed = msg.GetUInt16();
                Creature.Skull = (Constants.Skulls_t)msg.GetByte();
                Creature.PartyShield = (PartyShields_t)msg.GetByte();

            }
            else if (ThingId == 0x0063)
            {
                Creature = new PacketCreature(Client);
                Creature.Type = PacketCreatureType_t.TURN;
                Creature.Id = msg.GetUInt32();
                Creature.Direction = msg.GetByte();
            }
            else
            {
                Item = new Tibia.Objects.Item(Client, ThingId);
                Item.Loc = new Tibia.Objects.ItemLocation(Position);

                if (Item.HasExtraByte)
                    Item.Count = msg.GetByte();
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddUInt16(ThingId);

            if (ThingId == 0x0061 || ThingId == 0x0062)
            {
                if (ThingId == 0x0062)
                    msg.AddUInt32(Creature.Id);
                else if (ThingId == 0x0061)
                {
                    msg.AddUInt32(Creature.RemoveId);
                    msg.AddUInt32(Creature.Id);
                    msg.AddString(Creature.Name);
                }

                msg.AddByte(Creature.Health);
                msg.AddByte(Creature.Direction);
                msg.AddOutfit(Creature.Outfit);

                msg.AddByte(Creature.LightLevel);
                msg.AddByte(Creature.LightColor);

                msg.AddUInt16(Creature.Speed);
                msg.AddByte((byte)Creature.Skull);
                msg.AddByte((byte)Creature.PartyShield);
            }
            else if (ThingId == 0x0063)
            {
                msg.AddUInt32(Creature.Id);
                msg.AddByte(Creature.Direction);
            }
            else
            {
                if (Item.HasExtraByte)
                    msg.AddByte(Item.Count);
            }

            return msg.Packet;
        }
    }
}