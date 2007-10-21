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
        public ItemLocation Location;
        public bool Found;

        /** Many different constructors **/
        public Item() : this(0) { }
        public Item(bool found) : this(0, "", 0, null, found) { }
        public Item(uint id) : this(id, "") { }
        public Item(uint id, string name) : this(id, name, 0, null, false) { }
        public Item(ItemLocation loc) : this(0, "", 0, loc, false) { }
        public Item(uint id, byte count, ItemLocation loc, bool found) : this(id, "", count, loc, found) { }

        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name (only used when representing an item type)</param>
        /// <param name="count">number of items in the stack (also charges on a rune)</param>
        /// <param name="loc">location in game</param>
        /// <param name="found">used when searching</param>
        public Item(uint id, string name, byte count, ItemLocation loc, bool found)
        {
            Id = id;
            Name = name;
            Count = count;
            Location = loc;
            Found = found;
        }

        /// <summary>
        /// Check whether or not this item is in a list (checks by ID only)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>True if the item is in the list, false if not</returns>
        public bool isInList<T>(List<T> list) where T : Item
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
        public Spell.SpellCategory Category;

        /// <summary>
        /// Default rune constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name</param>
        /// <param name="words">spell words used to create the rune</param>
        /// <param name="manaPoints">amount of mana needed to make the rune</param>
        /// <param name="soulPoints">amount of soul points needed to make the rune</param>
        /// <param name="category">the runes category (attack, healing, etc.)</param>
        public Rune(uint id, string name, string words, uint manaPoints, uint soulPoints, Spell.SpellCategory category)
            : base(id, name)
        {
            Words = words;
            ManaPoints = manaPoints;
            SoulPoints = soulPoints;
            Category = category;
        }
    }

    /// <summary>
    /// Represents an item's location in game/memory. Can be either a slot, an inventory location, or on the ground.
    /// </summary>
    public class ItemLocation
    {
        public ItemLocationType type;
        public byte container;
        public byte position;
        public Location groundLocation;
        public byte stackOrder;
        public Slot.SlotNumber slot;

        /// <summary>
        /// Create a new item location at the specified slot (head, backpack, right, left, etc).
        /// </summary>
        /// <param name="s">slot</param>
        public ItemLocation(Slot.SlotNumber s)
        {
            type = ItemLocationType.SLOT;
            slot = s;
        }

        /// <summary>
        /// Create a new item loction at the specified inventory location.
        /// </summary>
        /// <param name="c">container</param>
        /// <param name="p">position in container</param>
        public ItemLocation(byte c, byte p)
        {
            type = ItemLocationType.CONTAINER;
            container = c;
            position = p;
        }

        /// <summary>
        /// Create a new item location from a general location (Objects.Location, in the Structures file).
        /// </summary>
        /// <param name="l"></param>
        public ItemLocation(Location l)
        {
            type = ItemLocationType.GROUND;
            groundLocation = l;
        }

        /// <summary>
        /// Different types of locations.
        /// </summary>
        public enum ItemLocationType
        {
            GROUND,
            SLOT,
            CONTAINER
        }
    }
}
