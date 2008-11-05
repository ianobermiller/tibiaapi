using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class MapItemUpdatePacket : Packet
    {
        private Location loc;
        private byte stackpos;
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

        public byte StackPos
        {
            get { return stackpos; }
        }
        public int CreatureId
        {
            get { return creatureId; }
        }

        public MapItemAddType UpdateType
        {
            get { return addType; }
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
        public MapItemUpdatePacket(Client c)
            : base(c)
        {
            type = PacketType.MapItemUpdate;
            destination = PacketDestination.Client;
        }
        public MapItemUpdatePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                int typen;
                if (type != PacketType.MapItemUpdate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                loc = p.GetLocation();
                stackpos = p.GetByte();
                typen = p.PeekInt();
                if (typen == 0x63)
                {
                    addType = MapItemAddType.CreatureReturning;
                    p.Skip(2);
                    creatureId=p.GetLong();
                    creatureDir = (Constants.TurnDirection)p.GetByte();
                }
                else if (typen == 0x61 || typen == 0x62)
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
                    p.Skip(2);
                    creatureSpeed = p.GetInt();
                    creatureSkull = (Constants.Skull)p.GetByte();
                }
                else
                {
                    addType = MapItemAddType.Item;
                    item = p.GetItem();
                    item.Loc = new ItemLocation(loc, stackpos);
                }
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static MapItemUpdatePacket Create(Client c,Location loc, Item item)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.MapItemUpdate);
            p.AddLocation(loc);
            p.AddItem(item);
            return new MapItemUpdatePacket(c, p.GetPacket());
        }
    }
}