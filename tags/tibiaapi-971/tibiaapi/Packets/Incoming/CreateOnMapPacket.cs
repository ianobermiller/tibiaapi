using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreateOnMapPacket : IncomingPacket
    {
        public Objects.Location Location { get; set; }
        public Objects.Item Item { get; set; }
        public PacketCreature Creature { get; set; }
        public ushort ThingId { get; set; }
        public byte Stack { get; set; }

        public CreateOnMapPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreateOnMap;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CreateOnMap)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreateOnMap;

            Location = msg.GetLocation();
            Stack = msg.GetByte();
            ThingId = msg.GetUInt16();

            if (ThingId == 97 || ThingId == 98)
            {
                Creature = new PacketCreature(Client);

                if (ThingId == 98)
                {
                    Creature.Type = PacketCreatureType.Known;
                    Creature.Id = msg.GetUInt32();
                }
                else if (ThingId == 97)
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
                    if (ThingId == 97)
                        Creature.WarIcon = (Constants.WarIcon)msg.GetByte();

                    Creature.IsBlocking = msg.GetByte().Equals(0x01);
                }
            }
            else if (ThingId == 99)
            {
                Creature = new PacketCreature(Client);
                Creature.Type = PacketCreatureType.Turn;
                Creature.Id = msg.GetUInt32();
                Creature.Direction = msg.GetByte();
                Creature.IsBlocking = msg.GetByte().Equals(0x01);
            }
            else
            {
                Item = new Tibia.Objects.Item(Client, ThingId);
                Item.Location = Tibia.Objects.ItemLocation.FromLocation(Location);

                if (Item.HasExtraByte)
                    Item.Count = msg.GetByte();
            }

            return true;
        }
    }
}