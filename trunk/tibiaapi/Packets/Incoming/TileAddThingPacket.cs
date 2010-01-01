using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TileAddThingPacket : IncomingPacket
    {
        public Objects.Location Position { get; set; }
        public Objects.Item Item { get; set; }
        public PacketCreature Creature { get; set; }
        public ushort ThingId { get; set; }
        public byte Stack { get; set; }

        public TileAddThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TileAddThing;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileAddThing)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileAddThing;

            try
            {
                Position = msg.GetLocation();
                Stack = msg.GetByte();
                ThingId = msg.GetUInt16();
                
                if (ThingId == 0x0061 || ThingId == 0x0062)
                {
                    Creature = new PacketCreature(Client);

                    if (ThingId == 0x0062)
                    {
                        Creature.Type = PacketCreatureType.Known;
                        Creature.Id = msg.GetUInt32();
                    }
                    else if (ThingId == 0x0061)
                    {
                        Creature.Type = PacketCreatureType.Unknown;
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
                    Creature.Skull = (Constants.Skull)msg.GetByte();
                    Creature.PartyShield = (PartyShield)msg.GetByte();

                    
                    if (Client.VersionNumber >= 853)
                    {
                        if (ThingId == 0x0061)
                            Creature.WarIcon = (Constants.WarIcon)msg.GetByte();

                        Creature.IsBlocking = msg.GetByte().Equals(0x01);
                    }
                }
                else if (ThingId == 0x0063)
                {
                    Creature = new PacketCreature(Client);
                    Creature.Type = PacketCreatureType.Turn;
                    Creature.Id = msg.GetUInt32();
                    Creature.Direction = msg.GetByte();
                }
                else
                {
                    Item = new Tibia.Objects.Item(Client, ThingId);
                    Item.Location = Tibia.Objects.ItemLocation.FromLocation(Position);

                    if (Item.HasExtraByte)
                        Item.Count = msg.GetByte();
                }
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddByte(Stack);
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
                if (Client.VersionNumber >= 853)
                {
                    if (ThingId == 0x0061)
                        msg.AddByte((byte)Creature.WarIcon);
                    msg.AddByte(Convert.ToByte(Creature.IsBlocking));
                }
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
        }
    }
}