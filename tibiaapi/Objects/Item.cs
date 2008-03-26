using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents one stack of items. Can also represent a type of item (with no location in memory).
    /// </summary>
    public class Item
    {
        public uint Id;
        public string Name;
        public byte Count;
        public bool Stackable;
        public ItemLocation Loc;
        public bool Found;
        public Client client;

        #region Constructors
        /** Many different constructors **/
        public Item() : this(0) { }
        public Item(Client c, bool found) : this(0, "", 0, null, c, found) { }
        public Item(uint id) : this(id, "") { }
        public Item(uint id, string name) : this(id, name, 0, null, null, false) { }
        public Item(ItemLocation loc) : this(0, "", 0, loc, null, false) { }
        public Item(uint id, byte count, ItemLocation loc, Client c, bool found) : this(id, "", count, loc, c, found) { }

        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name (only used when representing an item type)</param>
        /// <param name="count">number of items in the stack (also charges on a rune)</param>
        /// <param name="loc">location in game</param>
        /// <param name="c">client (used for sending packets)</param>
        /// <param name="found">used when searching</param>
        public Item(uint id, string name, byte count, ItemLocation loc, Client c, bool found)
        {
            Id = id;
            Name = name;
            Count = count;
            Loc = loc;
            Found = found;
            client = c;
        }
        #endregion

        #region Packet Functions

        /// <summary>
        /// Use the item (eg. eat food).
        /// </summary>
        /// <returns></returns>
        public bool Use()
        {
            if (client == null) return false;
            
            byte[] packet = new byte[12];
            packet[00] = 0x0A;
            packet[01] = 0x00;
            packet[02] = 0x82;

            Array.Copy(ItemLocationToBytes(Loc), 0, packet, 3, 5);

            packet[08] = Packet.Lo(Id);
            packet[09] = Packet.Hi(Id);
            packet[10] = Loc.stackOrder;
            packet[11] = 0x0F;

            return client.Send(packet);
        }

        /// <summary>
        /// Use the item on a tile (eg. fish, rope, pick, shovel, etc).
        /// </summary>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool Use(Objects.Tile onTile)
        {
            if (client == null) return false;
            
            byte[] packet = new byte[19];

            packet[00] = 0x11;
            packet[01] = 0x00;
            packet[02] = 0x83;

            Array.Copy(ItemLocationToBytes(Loc), 0, packet, 3, 5);

            packet[08] = Packet.Lo(Id);
            packet[09] = Packet.Hi(Id);
            packet[10] = 0x00;
            packet[11] = Packet.Lo(onTile.Location.X);
            packet[12] = Packet.Hi(onTile.Location.X);
            packet[13] = Packet.Lo(onTile.Location.Y);
            packet[14] = Packet.Hi(onTile.Location.Y);
            packet[15] = Packet.Lo(onTile.Location.Z);
            packet[16] = Packet.Lo(onTile.Id);
            packet[17] = Packet.Hi(onTile.Id);
            packet[18] = 0x00;

            return client.Send(packet);
        }

        /// <summary>
        /// Use an item on another item.
        /// TODO
        /// </summary>
        /// <param name="onItem"></param>
        /// <returns></returns>
        public bool Use(Objects.Item onItem)
        {
            if (client == null) return false;
            byte[] packet = null;

            return client.Send(packet);
        }

        /// <summary>
        /// Use an item on a creature (eg. use a rune on someone, drink a manafluid).
        /// If it is a player shoot on xyz coors, if it is a creature shoot through
        /// the battlelist (more accurate).
        /// </summary>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public bool Use(Objects.Creature onCreature)
        {
            if (client == null) return false;

            byte[] packet;

            switch (onCreature.Type)
            {

                case Constants.CreatureType.Player:

                    packet = new byte[19];
                    packet[00] = 0x11;
                    packet[01] = 0x00;
                    packet[02] = 0x83;

                    Array.Copy(ItemLocationToBytes(Loc), 0, packet, 3, 5);

                    packet[08] = Packet.Lo(Id);
                    packet[09] = Packet.Hi(Id);
                    packet[10] = packet[07];

                    int x = onCreature.Location.X;
                    packet[11] = Packet.Lo(x);
                    packet[12] = Packet.Hi(x);

                    int y = onCreature.Location.Y;
                    packet[13] = Packet.Lo(y);
                    packet[14] = Packet.Hi(y);
                    packet[15] = Convert.ToByte(onCreature.Location.Z);

                    packet[16] = 0x63;
                    packet[17] = 0x00;

                    if (Id == Constants.Items.Bottle.Vial)
                        packet[18] = Count;
                    else
                        packet[18] = 0x01;

                    return client.Send(packet);

                case Constants.CreatureType.Target:
                case Constants.CreatureType.NPC:

                    packet = new byte[15];
                    packet[00] = 0x0D;
                    packet[01] = 0x00;
                    packet[02] = 0x84;

                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = 0x00;
                    packet[06] = 0x00;
                    packet[07] = 0x00;

                    packet[08] = Packet.Lo(Id);
                    packet[09] = Packet.Hi(Id);

                    if (Id == Constants.Items.Bottle.Vial)
                        packet[10] = Count;
                    else
                        packet[10] = 0x00;

                    // 11 - 14
                    Array.Copy(BitConverter.GetBytes(onCreature.Id), 0, packet, 11, 4);

                    return client.Send(packet);

                default:
                    return false;
            }
        }

        /// <summary>
        /// Use the item on yourself
        /// </summary>
        /// <returns></returns>
        public bool UseOnSelf()
        {
            byte[] packet = new byte[15];
            packet[00] = 0x0D;
            packet[01] = 0x00;
            packet[02] = 0x84;

            packet[03] = 0xFF;
            packet[04] = 0xFF;
            packet[05] = 0x00;
            packet[06] = 0x00;
            packet[07] = 0x00;

            packet[08] = Packet.Lo(Id);
            packet[09] = Packet.Hi(Id);

            if (Id == Constants.Items.Bottle.Vial)
                packet[10] = Count;
            else
                packet[10] = 0x00;

            // 11 - 14
            int playerID = client.ReadInt(Addresses.Player.Id);
            Array.Copy(BitConverter.GetBytes(playerID), 0, packet, 11, 4);

            return client.Send(packet);
        }

        /// <summary>
        /// Move an item to a location (eg. move a blank rune to the right hand).
        /// </summary>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public bool Move(Objects.ItemLocation toLocation)
        {
            if (client == null) return false;
            
            return Move(new Objects.Item(toLocation));
        }

        /// <summary>
        /// Move an item into another item (eg. put an item into a backpack).
        /// </summary>
        /// <param name="toItem"></param>
        /// <returns></returns>
        public bool Move(Objects.Item toItem)
        {
            if (client == null) return false;

            byte[] packet = new byte[17];
            packet[00] = 0x0F;
            packet[01] = 0x00;
            packet[02] = 0x78;

            Array.Copy(ItemLocationToBytes(Loc), 0, packet, 3, 5);

            packet[08] = Packet.Lo(Id);
            packet[09] = Packet.Hi(Id);

            packet[10] = packet[07];

            Array.Copy(ItemLocationToBytes(toItem.Loc), 0, packet, 11, 5);

            packet[16] = Count;

            return client.Send(packet);
        }

        /// <summary>
        /// Get the packet bytes for an item location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static byte[] ItemLocationToBytes(Objects.ItemLocation location)
        {            
            byte[] bytes = new byte[5];

            switch (location.type)
            {
                case Constants.ItemLocationType.CONTAINER:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = Convert.ToByte(0x40 + location.container);
                    bytes[03] = 0x00;
                    bytes[04] = location.position;
                    break;
                case Constants.ItemLocationType.SLOT:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = Convert.ToByte(location.slot);
                    bytes[03] = 0x00;
                    bytes[04] = 0x00;
                    break;
                case Constants.ItemLocationType.GROUND:
                    bytes[00] = Packet.Lo(location.groundLocation.X);
                    bytes[01] = Packet.Hi(location.groundLocation.X);
                    bytes[02] = Packet.Lo(location.groundLocation.Y);
                    bytes[03] = Packet.Hi(location.groundLocation.Y);
                    bytes[04] = Convert.ToByte(location.groundLocation.Z);
                    break;
            }

            return bytes;
        }

        #endregion

        /// <summary>
        /// Check whether or not this item is in a list (checks by ID only)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>True if the item is in the list, false if not</returns>
        public bool IsInList<T>(List<T> list) where T : Item
        {
            if (Id != 0)
                return (list.Find(delegate(T i) { return Id == i.Id; }) != null);
            else
                return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    #region Special Item Types

    /// <summary>
    /// Represents an ammo item. Same as a regular item, but with the PickUp field.
    /// </summary>
    public class Ammunition : Item
    {
        public bool PickUp;

        public Ammunition(uint id, string name, bool pickUp) : base(id, name)
        {
            PickUp = pickUp;
        }
    }

    /// <summary>
    /// Represents a food item. Same as regular item but also stores regeneration time.
    /// </summary>
    public class Food : Item
    {
        public uint RegenerationTime;

        public Food(uint id, string name, uint regenerationTime) : base(id, name)
        {
            RegenerationTime = regenerationTime;
        }
    }

    /// <summary>
    /// Represents a rune item. Contains metadata relating specifically to runes.
    /// </summary>
    public class Rune : Item
    {
        public string Words;
        public uint ManaPoints;
        public uint SoulPoints;
        public Constants.SpellCategory Category;

        /// <summary>
        /// Default rune constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name</param>
        /// <param name="words">spell words used to create the rune</param>
        /// <param name="manaPoints">amount of mana needed to make the rune</param>
        /// <param name="soulPoints">amount of soul points needed to make the rune</param>
        /// <param name="category">the runes category (attack, healing, etc.)</param>
        public Rune(uint id, string name, string words, uint manaPoints, uint soulPoints, Constants.SpellCategory category)
            : base(id, name)
        {
            Words = words;
            ManaPoints = manaPoints;
            SoulPoints = soulPoints;
            Category = category;
        }
    }

    #endregion

    /// <summary>
    /// Represents an item's location in game/memory. Can be either a slot, an inventory location, or on the ground.
    /// </summary>
    public class ItemLocation
    {
        public Constants.ItemLocationType type;
        public byte container;
        public byte position;
        public Location groundLocation;
        public byte stackOrder;
        public Constants.SlotNumber slot;

        /// <summary>
        /// Create a new item location at the specified slot (head, backpack, right, left, etc).
        /// </summary>
        /// <param name="s">slot</param>
        public ItemLocation(Constants.SlotNumber s)
        {
            type = Constants.ItemLocationType.SLOT;
            slot = s;
        }

        /// <summary>
        /// Create a new item loction at the specified inventory location.
        /// </summary>
        /// <param name="c">container</param>
        /// <param name="p">position in container</param>
        public ItemLocation(byte c, byte p)
        {
            type = Constants.ItemLocationType.CONTAINER;
            container = c;
            position = p;
            stackOrder = p;
        }

        /// <summary>
        /// Create a new item location from a general location and stack order (Objects.Location, in the Structures file).
        /// </summary>
        /// <param name="l"></param>
        public ItemLocation(Location l, byte stack)
        {
            type = Constants.ItemLocationType.GROUND;
            groundLocation = l;
            stackOrder = stack;
        }
    }
}
