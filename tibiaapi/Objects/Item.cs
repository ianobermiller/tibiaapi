using System;
using System.Collections.Generic;
using Tibia.Packets;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents one stack of items. Can also represent a type of item (with no location in memory).
    /// </summary>
    public class Item
    {
        protected Client client;
        protected uint id;
        protected string name;
        protected byte count;
        protected ItemLocation loc;
        protected bool found;

        #region Constructors

        //only for ItemsList
        /// <summary>
        /// Please use this constructor only for items list.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Item(uint id, string name) 
            : this(null, id, 0, name) { }

        public Item(Client client, uint id)
            : this(client, id, 0) { }

        public Item(Client client, uint id, byte count)
            : this(client, id, count, "") { }

        public Item(Client client, uint id, byte count, string name)
            : this(client, id, count, "", null, false) { }

        public Item(Client client, uint id, byte count, string name, ItemLocation location, bool found)
        {
            this.client = client;
            this.id = id;
            this.count = count;
            this.name = name;
            this.loc = location;
            this.found = found;
        }

        #endregion

        #region Packet Functions

        /// <summary>
        /// Opens a container in the same window. Only works if the item is a container.
        /// </summary>
        /// <param name="container">Which container window to open in.</param>
        /// <returns></returns>
        public bool OpenContainer(byte container)
        {
            return Packets.Outgoing.ItemUsePacket.Send(client, loc.ToLocation(), (ushort)id, loc.stackOrder, container);
        }

        /// <summary>
        /// Use the item (eg. eat food).
        /// </summary>
        /// <returns></returns>
        public bool Use()
        {
            return Packets.Outgoing.ItemUsePacket.Send(client, loc.ToLocation(), (ushort)id, loc.stackOrder, 0x0F);
        }

        /// <summary>
        /// Use the item on a tile (eg. fish, rope, pick, shovel, etc).
        /// </summary>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool Use(Objects.Tile onTile)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, loc.ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Id, 0);
        }



        /// <summary>
        /// Use the item on a tile location.
        /// Gets the tile id automatically.
        /// </summary>
        /// <param name="onLocation"></param>
        /// <returns></returns>
        public bool Use(Location onLocation)
        {
            MapSquare square = client.Map.CreateMapSquare(onLocation);
            return Use(square.Tile);
        }

        /// <summary>
        /// Use an item on another item.
        /// Not tested.
        /// </summary>
        /// <param name="onItem"></param>
        /// <returns></returns>
        public bool Use(Objects.Item onItem)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, loc.ToLocation(), (ushort)id, 0, onItem.Loc.ToLocation(), (ushort)onItem.Id, 0); 
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
            return Packets.Outgoing.ItemUseOnPacket.Send(client, loc.ToLocation(), (ushort)id, loc.ToBytes()[4], onCreature.Location, 0x63, 0);
        }

        /// <summary>
        /// Use the item on yourself
        /// </summary>
        /// <returns></returns>
        public bool UseOnSelf()
        {
            uint playerId = client.ReadUInt32(Addresses.Player.Id);
            
            byte stack = 0;

            if (id == Constants.Items.Bottle.Vial.Id) 
                stack = count;

            return Packets.Outgoing.ItemUseBattlelistPacket.Send(client, ItemLocation.Hotkey().ToLocation(), (ushort)id, stack, playerId);
        }

        /// <summary>
        /// Move an item to a location (eg. move a blank rune to the right hand).
        /// </summary>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public bool Move(Objects.ItemLocation toLocation)
        {
            if (client == null) 
                return false;
            
            return Move(new Objects.Item(client, 0, 0, "", toLocation, false));
        }

        /// <summary>
        /// Move an item into another item (eg. put an item into a backpack).
        /// </summary>
        /// <param name="toItem"></param>
        /// <returns></returns>
        public bool Move(Objects.Item toItem)
        {
            return Packets.Outgoing.ItemMovePacket.Send(client, loc.ToLocation(), (ushort)id, loc.ToBytes()[4], toItem.Loc.ToLocation(), count);
        }

        /// <summary>
        /// Look at an item.
        /// </summary>
        /// <returns></returns>
        public bool Look()
        {
            return Packets.Outgoing.LookAtPacket.Send(client, loc.ToLocation(), (ushort)id, loc.stackOrder);
        }

        /// <summary>
        /// Gets the DatAddress of the item in the dat structure.
        /// </summary>
        public uint DatAddress
        {
            get
            {
                uint baseAddr = (uint)client.ReadInt32(Addresses.Client.DatPointer);
                return (uint)client.ReadInt32(baseAddr + 8) + 0x4C * (id - 100);
            }
        }

        /// <summary>
        /// Gets or sets the visible width of the item.
        /// </summary>
        public int Width
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Width); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Width, value); }
        }

        /// <summary>
        /// Gets or sets the visible height of the item.
        /// </summary>
        public int Height
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Height); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Height, value); }
        }

        public int Unknown1
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Unknown1); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Unknown1, value); }
        }

        public int Layers
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Layers); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Layers, value); }
        }

        public int PatternX
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.PatternX); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.PatternX, value); }
        }

        public int PatternY
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.PatternY); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.PatternY, value); }
        }

        public int PatternDepth
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.PatternDepth); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.PatternDepth, value); }
        }

        public int Phase
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Phase); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Phase, value); }
        }

        /// <summary>
        /// Gets the DatAddress of the sprite structure of the item.
        /// </summary>
        public int Sprites
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Sprites); }
            //set { client.WriteInt(DatAddress + Addresses.DatItem.Sprites, value); }
        }

        /// <summary>
        /// Gets or sets the flags of the item.
        /// </summary>
        public int Flags
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Flags); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Flags, value); }
        }

        /// <summary>
        /// Gets or sets the walking speed of the item. (Walkable tiles)
        /// </summary>
        public int WalkSpeed
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.WalkSpeed); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.WalkSpeed, value); }
        }

        /// <summary>
        /// Gets or sets the text limit of the item. (Writable items)
        /// </summary>
        public int TextLimit
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.TextLimit); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.TextLimit, value); }
        }

        /// <summary>
        /// Gets or sets the light radius of the item. (Light items)
        /// </summary>
        public int LightRadius
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.LightRadius); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.LightRadius, value); }
        }

        /// <summary>
        /// Gets or sets the light color of the item. (Light items)
        /// </summary>
        public int LightColor
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.LightColor); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.LightColor, value); }
        }

        /// <summary>
        /// Gets or sets how many pixels the item is shifted horizontally from the lower
        /// right corner of the tile.
        /// </summary>
        public int ShiftX
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.ShiftX); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.ShiftX, value); }
        }

        /// <summary>
        /// Gets or sets how many pixels the item is shifted vertically from the lower
        /// right corner of the tile.
        /// </summary>
        public int ShiftY
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.ShiftY); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.ShiftY, value); }
        }

        /// <summary>
        /// Gets or sets the height created by the item when a character walks over.
        /// </summary>
        public int WalkHeight
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.WalkHeight); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.WalkHeight, value); }
        }

        /// <summary>
        /// Gets or sets the color shown by the item in the automap.
        /// </summary>
        public int AutomapColor
        {
            get { return client.ReadInt32(DatAddress + Addresses.DatItem.Automap); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.Automap, value); }
        }

        /// <summary>
        /// Gets or sets the help id for the item in Help mode.
        /// </summary>
        public Addresses.DatItem.Help LensHelp
        {
            get { return (Addresses.DatItem.Help)client.ReadInt32(DatAddress + Addresses.DatItem.LensHelp); }
            set { client.WriteInt32(DatAddress + Addresses.DatItem.LensHelp, (int)value); }
        }
        #endregion

        #region Get/Set Properties
        /// <summary>
        /// Gets the client associated with this item;
        /// </summary>
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }
        /// <summary>
        /// Gets or sets the id of the item.
        /// </summary>
        public uint Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the amount stacked of this item.
        /// </summary>
        public byte Count
        {
            get { return count; }
            set { count = value; }
        }

        /// <summary>
        /// Gets the total number of items/objects in Tibia.
        /// </summary>
        public uint TotalItemCount
        {
            get
            {
                uint baseAddr = (uint)client.ReadInt32(Addresses.Client.DatPointer);
                return (uint)client.ReadInt32(baseAddr + 4);
            }
        }

        /// <summary>
        /// Gets or sets the location of this item.
        /// </summary>
        public ItemLocation Loc
        {
            get { return loc; }
            set { loc = value; }
        }
        /// <summary>
        /// Gets or sets whether this item is found.
        /// </summary>
        public bool Found
        {
            get { return found; }
            set { found = value; }
        }
        #endregion

        #region Composite Properties

        /// <summary>
        /// Returns whether the item has an extra byte (count, fluid type, charges, etc).
        /// </summary>
        public bool HasExtraByte
        {
            get
            {
                return GetFlag(Tibia.Addresses.DatItem.Flag.IsStackable) ||
                       GetFlag(Tibia.Addresses.DatItem.Flag.IsRune) ||
                       GetFlag(Tibia.Addresses.DatItem.Flag.IsSplash) ||
                       GetFlag(Tibia.Addresses.DatItem.Flag.IsFluidContainer);
            }
        }

        #endregion

        /// <summary>
        /// Check whether or not this item is in a list (checks by ID only)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>True if the item is in the list, false if not</returns>
        public bool IsInList<T>(IEnumerable<T> list) where T : Item
        {
            if (Id != 0)
            {
                foreach (T i in list)
                {
                    if (Id == i.Id) return true;
                }
                return false;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return Name;
        }

        #region Flags

        public bool GetFlag(Addresses.DatItem.Flag flag)
        {
            return (Flags & (int)flag) == (int)flag;
        }

        public void SetFlag(Addresses.DatItem.Flag flag, bool enable)
        {
            if (enable)
                Flags |= (int)flag;
            else
                Flags &= ~(int)flag;
        }

        #endregion
    }

    #region Special Item Types

    /// <summary>
    /// Represents a food item. Same as regular item but also stores regeneration time.
    /// </summary>
    public class Food : Item
    {
        public uint RegenerationTime;

        public Food(uint id, string name, uint regenerationTime)
            : base(null, id, 0, name)
        {
            RegenerationTime = regenerationTime;
        }

        public Food(Client client, uint id, string name, uint regenerationTime)
            : base(client, id, 0, name)
        {
            RegenerationTime = regenerationTime;
        }
    }

    /// <summary>
    /// Represents a rune item. Contains metadata relating specifically to runes.
    /// </summary>
    public class TransformingItem : Item
    {
        public Spell Spell;
        public uint SoulPoints;
        public Item OriginalItem;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name</param>
        /// <param name="spell">spell used to create the item</param>
        /// <param name="soulPoints">amount of soul points needed to make the item</param>
        /// <param name="originalItem">the item that the spell words are used on to create this item</param>
        public TransformingItem(Client client, uint id, string name, Spell spell, uint soulPoints, Item originalItem)
            : base(client, id, 0, name)
        {
            Spell = spell;
            SoulPoints = soulPoints;
            OriginalItem = originalItem;
        }

        public TransformingItem(uint id, string name, Spell spell, uint soulPoints, Item originalItem)
            : base(null, id, 0, name)
        {
            Spell = spell;
            SoulPoints = soulPoints;
            OriginalItem = originalItem;
        }

    }

    /// <summary>
    /// Represents a rune item. Contains metadata relating specifically to runes.
    /// </summary>
    public class Rune : TransformingItem
    {
        /// <summary>
        /// Default rune constructor.
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name</param>
        /// <param name="spell">spell used to create the rune</param>
        /// <param name="soulPoints">amount of soul points needed to make the rune</param>
        public Rune(Client client, uint id, string name, Spell spell, uint soulPoints)
            : base(client, id, name, spell, soulPoints, Constants.Items.Rune.Blank)
        {
        }

        public Rune(uint id, string name, Spell spell, uint soulPoints)
            : base(null, id, name, spell, soulPoints, Constants.Items.Rune.Blank)
        {
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
            type = Constants.ItemLocationType.Slot;
            slot = s;
        }

        /// <summary>
        /// Create a new item loction at the specified inventory location.
        /// </summary>
        /// <param name="c">container</param>
        /// <param name="p">position in container</param>
        public ItemLocation(byte c, byte p)
        {
            type = Constants.ItemLocationType.Container;
            container = c;
            position = p;
            stackOrder = p;
        }

        /// <summary>
        /// Create a new item location from a general location and stack order.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="stack"></param>
        public ItemLocation(Location l, byte stack)
        {
            type = Constants.ItemLocationType.Ground;
            groundLocation = l;
            stackOrder = stack;
        }

        /// <summary>
        /// Create a new item location at the specified location.
        /// </summary>
        /// <param name="l"></param>
        public ItemLocation(Location l)
        {
            type = Constants.ItemLocationType.Ground;
            groundLocation = l;
            stackOrder = 1;
        }

        /// <summary>
        /// Return a blank item location for packets (FF FF 00 00 00)
        /// </summary>
        /// <returns></returns>
        public static ItemLocation Hotkey()
        {
            return new ItemLocation(Constants.SlotNumber.None);
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[5];

            switch (type)
            {
                case Constants.ItemLocationType.Container:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = (byte)(0x40 + container);
                    bytes[03] = 0x00;
                    bytes[04] = position;
                    break;
                case Constants.ItemLocationType.Slot:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = (byte)slot;
                    bytes[03] = 0x00;
                    bytes[04] = 0x00;
                    break;
                case Constants.ItemLocationType.Ground:
                    bytes[00] = groundLocation.X.Low();
                    bytes[01] = groundLocation.X.High();
                    bytes[02] = groundLocation.Y.Low();
                    bytes[03] = groundLocation.Y.High(); 
                    bytes[04] = (byte)groundLocation.Z;
                    break;
            }

            return bytes;
        }

        public Location ToLocation()
        {
            Location newPos = new Location();

            switch (type)
            {
                case Constants.ItemLocationType.Container:
                    {
                        newPos.X = (int)BitConverter.ToUInt16(new byte[] { 0xFF, 0xFF }, 0);
                        newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)(0x40 + container), 0x00 }, 0);
                        newPos.Z = (int)position;
                        break;
                    }
                case Constants.ItemLocationType.Slot:
                    {
                        newPos.X = (int)BitConverter.ToUInt16(new byte[] { 0xFF, 0xFF }, 0);
                        newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)slot, 0x00 }, 0);
                        newPos.Z = 0;
                        break;
                    }
                case Constants.ItemLocationType.Ground:
                    {
                        newPos = groundLocation;
                        break;
                    }
            }

            return newPos;
        }

    }
}
