using System;
using System.Collections.Generic;
using Tibia.Objects;
using System.Text;

namespace Tibia.Packets
{
    public class MapItemAddPacket : Packet
    {
        private Location loc;
        private Item item;
        private int creatureId;
        private MapItemAddType addType;
        private int knownRemoved;
        private string creatureName;
        private byte creatureHpBar;
        private Constants.TurnDirection creatureDir;
        private bool creatureInvis;
        private int creatureSpeed;
        private Constants.Skull creatureSkull;

        public Location Loc
        {
            get { return loc; }
        }

        public Item Item
        {
            get { return item; }
        }

        public int CreatureId
        {
            get { return creatureId; }
        }

        public MapItemAddType AddType
        {
            get { return addType; }
        }

        /// <summary>
        /// Has a value when the maximum amount of known ID's (150)
        /// has been reached. This is the ID that should be removed.
        /// </summary>
        public int KnownRemoved
        {
            get { return knownRemoved; }
        }

        public string CreatureName
        {
            get { return creatureName; }
        }

        public byte CreatureHpBar
        {
            get { return CreatureHpBar; }
        }

        public Constants.TurnDirection CreatureDir
        {
            get { return creatureDir; }
        }

        public bool CreatureInvis
        {
            get { return creatureInvis; }
        }

        public int CreatureSpeed
        {
            get { return creatureSpeed; }
        }

        public Constants.Skull CreatureSkull
        {
            get { return creatureSkull; }
        }

        public MapItemAddPacket(Client c) : base(c)
        {
            type = PacketType.MapItemAdd;
            destination = PacketDestination.Client;
        }
        public MapItemAddPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.MapItemAdd) return false;
                int typen;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                loc = p.GetLocation();
                typen = p.PeekInt();
                if (typen == 0x63)
                {
                    // returning creature
                    addType = MapItemAddType.CreatureReturning;
                    p.Skip(2);
                    creatureId = p.GetLong();
                    creatureDir = (Constants.TurnDirection)p.GetByte();
                }
                else if (typen == 0x62 || typen == 0x61) // creature with description
                {
                    p.Skip(2);
                    if (typen == 0x62)
                    {
                        // Known creature
                        addType = MapItemAddType.CreatureKnown;
                        creatureId = p.GetLong();
                    }
                    else if (typen == 0x61)
                    {
                        // New creature
                        addType = MapItemAddType.CreatureNew;
                        knownRemoved = p.GetLong();
                        creatureId = p.GetLong();
                        creatureName = p.GetString();
                    }
                    creatureHpBar = p.GetByte();
                    creatureDir = (Constants.TurnDirection)p.GetByte();

                    if (p.GetInt() == 0) // is invis
                    {
                        if (p.GetInt() == 0)
                            creatureInvis = true; // no outfit
                        //else monster outfit
                    }
                    else // player outfit
                    {
                        creatureInvis = false;
                        p.Skip(6); // outfit colors
                    }

                    // light
                    p.Skip(2);

                    // speed
                    creatureSpeed = p.GetInt();

                    // skull
                    creatureSkull = (Constants.Skull)p.GetByte();

                }
                else // just an item
                {
                    addType = MapItemAddType.Item;
                    item = p.GetItem();
                }
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static MapItemAddPacket Create(Client c, Location loc, Item item, Client client)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.MapItemAdd);
            p.AddLocation(loc);
            p.AddItem(item);
            return new MapItemAddPacket(c, p.GetPacket());
        }
    }
}
