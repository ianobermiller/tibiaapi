using System;
using System.Collections.Generic;
using Tibia.Packets;
using System.Drawing;
using Tibia.Util;

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
        protected ItemLocation location;

        #region Constructors

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
            : this(client, id, count, name, null) { }

        public Item(Client client, uint id, byte count, string name, ItemLocation location)
        {
            this.client = client;
            this.id = id;
            this.count = count;
            this.name = name;
            this.location = location;
        }

        #endregion

        #region Packet Functions

        /// <summary>
        /// Opens a container in the same window. Only works if the item is a container.
        /// </summary>
        /// <param name="container">Which container window to open in.</param>
        /// <returns></returns>
        public bool OpenAsContainer(byte container)
        {
            return Packets.Outgoing.ItemUsePacket.Send(client, location.ToLocation(), (ushort)id, location.StackOrder, container);
        }

        /// <summary>
        /// Use the item (eg. eat food).
        /// </summary>
        /// <returns></returns>
        public bool Use()
        {
            return Packets.Outgoing.ItemUsePacket.Send(client, location.ToLocation(), (ushort)id, location.StackOrder, 0x0F);
        }

        /// <summary>
        /// Use the item on a tile (eg. fish, rope, pick, shovel, etc).
        /// </summary>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool Use(Objects.Tile onTile)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, location.ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Ground.Id, 0);
        }

        /// <summary>
        /// Use an item on another item.
        /// Not tested.
        /// </summary>
        /// <param name="onItem"></param>
        /// <returns></returns>
        public bool Use(Objects.Item onItem)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, location.ToLocation(), (ushort)id, 0, onItem.Location.ToLocation(), (ushort)onItem.Id, 0); 
        }

        /// <summary>
        /// Use an item on a creature.
        /// </summary>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public bool Use(Objects.Creature onCreature)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, location.ToLocation(), (ushort)id, location.ToBytes()[4], onCreature.Location, 0x63, 0);
        }

        /// <summary>
        /// Use the item on yourself
        /// </summary>
        /// <returns></returns>
        public bool UseOnSelf()
        {
            uint playerId = client.Memory.ReadUInt32(Addresses.Player.Id);
            
            byte stack = 0;

            if (id == Constants.Items.Bottle.Vial.Id) 
                stack = count;

            return Packets.Outgoing.ItemUseBattlelistPacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, stack, playerId);
        }

        /// <summary>
        /// Move an item to a location (eg. move a blank rune to the right hand).
        /// </summary>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public bool Move(Objects.ItemLocation toLocation)
        {
            return Move(toLocation, this.count);
        }

        /// <summary>
        /// Move an item to a location (eg. move a blank rune to the right hand).
        /// </summary>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public bool Move(Objects.ItemLocation toLocation, byte count)
        {
            return Packets.Outgoing.ItemMovePacket.Send(client, location.ToLocation(), (ushort)id, location.ToBytes()[4], toLocation.ToLocation(), count);
        }

        /// <summary>
        /// Look at an item.
        /// </summary>
        /// <returns></returns>
        public bool Look()
        {
            return Packets.Outgoing.LookAtPacket.Send(client, location.ToLocation(), (ushort)id, location.StackOrder);
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
                uint baseAddr = client.Memory.ReadUInt32(Addresses.Client.DatPointer);
                return client.Memory.ReadUInt32(baseAddr + 4);
            }
        }

        /// <summary>
        /// Gets or sets the location of this item.
        /// </summary>
        public ItemLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Gets the DatAddress of the item in the dat structure.
        /// </summary>
        public uint DatAddress
        {
            get
            {
                uint baseAddr = client.Memory.ReadUInt32(Addresses.Client.DatPointer);                
                return client.Memory.ReadUInt32(baseAddr + 8) + (0x4C + Addresses.DatItem.Unknown2/10) * (id - 100);
            }
        }

        /// <summary>
        /// Gets or sets the visible width of the item.
        /// </summary>
        public int Width
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Width); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Width, value); }
        }

        /// <summary>
        /// Gets or sets the visible height of the item.
        /// </summary>
        public int Height
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Height); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Height, value); }
        }

        public int Unknown1
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Unknown1); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Unknown1, value); }
        }

        public int Layers
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Layers); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Layers, value); }
        }

        public int PatternX
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.PatternX); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.PatternX, value); }
        }

        public int PatternY
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.PatternY); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.PatternY, value); }
        }

        public int PatternDepth
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.PatternDepth); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.PatternDepth, value); }
        }

        public int Phase
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Phase); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Phase, value); }
        }

        public int SpriteCount
        {
            get
            {
                return Width * Height * Layers * PatternX * PatternY * PatternDepth * Phase;
            }
        }

        public ushort[] SpriteIds
        {
            get
            {
                int count = SpriteCount;
                uint address = client.Memory.ReadUInt32(DatAddress + Addresses.DatItem.Sprite);
                ushort[] spriteIds = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    spriteIds[i] = client.Memory.ReadUInt16(address + i * 2);
                }
                return spriteIds;
            }
            set
            {
                int count = SpriteCount;
                if (value.Length != count)
                    throw new ArgumentException("value.Length!=SpriteCount");
                uint address = client.Memory.ReadUInt32(DatAddress + Addresses.DatItem.Sprite);
                for (int i = 0; i < count; i++)
                {
                    client.Memory.WriteUInt16(address + i * 2,value[i]);
                }

            }

        }

        public Image[] Sprites
        {
            get
            {
                ushort[] spriteIds = SpriteIds;
                Image[] sprites = new Image[spriteIds.Length];
                for (int i = 0; i < spriteIds.Length; i++)
                {
                    try
                    {
                        sprites[i] = SpriteReader.GetSpriteImage(client, spriteIds[i]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        sprites[i] = null;
                    }
                }
                return sprites;
            }
        }

        /// <summary>
        /// Gets or sets the flags of the item.
        /// </summary>
        public uint Flags
        {
            get { return client.Memory.ReadUInt32(DatAddress + Addresses.DatItem.Flags); }
            set { client.Memory.WriteUInt32(DatAddress + Addresses.DatItem.Flags, value); }
        }

        /// <summary>
        /// Gets or sets the walking speed of the item. (Walkable tiles)
        /// </summary>
        public int WalkSpeed
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.WalkSpeed); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.WalkSpeed, value); }
        }

        /// <summary>
        /// Gets or sets the text limit of the item. (Writable items)
        /// </summary>
        public int TextLimit
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.TextLimit); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.TextLimit, value); }
        }

        /// <summary>
        /// Gets or sets the light radius of the item. (Light items)
        /// </summary>
        public int LightRadius
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.LightRadius); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.LightRadius, value); }
        }

        /// <summary>
        /// Gets or sets the light color of the item. (Light items)
        /// </summary>
        public int LightColor
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.LightColor); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.LightColor, value); }
        }

        /// <summary>
        /// Gets or sets how many pixels the item is shifted horizontally from the lower
        /// right corner of the tile.
        /// </summary>
        public int ShiftX
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.ShiftX); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.ShiftX, value); }
        }

        /// <summary>
        /// Gets or sets how many pixels the item is shifted vertically from the lower
        /// right corner of the tile.
        /// </summary>
        public int ShiftY
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.ShiftY); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.ShiftY, value); }
        }

        /// <summary>
        /// Gets or sets the height created by the item when a character walks over.
        /// </summary>
        public int WalkHeight
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.WalkHeight); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.WalkHeight, value); }
        }

        /// <summary>
        /// Gets or sets the color shown by the item in the automap.
        /// </summary>
        public int AutomapColor
        {
            get { return client.Memory.ReadInt32(DatAddress + Addresses.DatItem.Automap); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.Automap, value); }
        }

        /// <summary>
        /// Gets or sets the help id for the item in Help mode.
        /// </summary>
        public Addresses.DatItem.Help LensHelp
        {
            get { return (Addresses.DatItem.Help)client.Memory.ReadInt32(DatAddress + Addresses.DatItem.LensHelp); }
            set { client.Memory.WriteInt32(DatAddress + Addresses.DatItem.LensHelp, (int)value); }
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

        #region Flags

        public bool GetFlag(Addresses.DatItem.Flag flag)
        {
            return (Flags & (uint)flag) == (uint)flag;
        }

        public void SetFlag(Addresses.DatItem.Flag flag, bool enable)
        {
            if (enable)
                Flags |= (uint)flag;
            else
                Flags &= ~(uint)flag;
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
                    if (Id == i.Id)
                        return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return Name;
        }
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
        public Constants.ItemLocationType Type;
        public byte ContainerId;
        public byte ContainerSlot;
        public Location GroundLocation;
        public byte StackOrder;
        public Constants.SlotNumber Slot;

        public ItemLocation() { }

        public static ItemLocation FromSlot(Constants.SlotNumber slot)
        {
            ItemLocation loc = new ItemLocation();
            loc.Type = Constants.ItemLocationType.Slot;
            loc.Slot = slot;
            return loc;
        }

        public static ItemLocation FromContainer(byte container, byte position)
        {
            ItemLocation loc = new ItemLocation();
            loc.Type = Constants.ItemLocationType.Container;
            loc.ContainerId = container;
            loc.ContainerSlot = position;
            loc.StackOrder = position;
            return loc;
        }

        public static ItemLocation FromLocation(Location location, byte stack)
        {
            ItemLocation loc = new ItemLocation();
            loc.Type = Constants.ItemLocationType.Ground;
            loc.GroundLocation = location;
            loc.StackOrder = stack;
            return loc;
        }

        public static ItemLocation FromLocation(Location location)
        {
            return FromLocation(location, 1);
        }

        public static ItemLocation FromItemLocation(ItemLocation location)
        {
            switch (location.Type)
            {
                case Tibia.Constants.ItemLocationType.Container:
                    return ItemLocation.FromContainer(location.ContainerId, location.ContainerSlot);
                case Tibia.Constants.ItemLocationType.Ground:
                    return ItemLocation.FromLocation(location.GroundLocation, location.StackOrder);
                case Tibia.Constants.ItemLocationType.Slot:
                    return ItemLocation.FromSlot(location.Slot);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Return a blank item location for packets (FF FF 00 00 00)
        /// </summary>
        /// <returns></returns>
        public static ItemLocation FromHotkey()
        {
            return FromSlot(Constants.SlotNumber.None);
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[5];

            switch (Type)
            {
                case Constants.ItemLocationType.Container:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = (byte)(0x40 + ContainerId);
                    bytes[03] = 0x00;
                    bytes[04] = ContainerSlot;
                    break;
                case Constants.ItemLocationType.Slot:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = (byte)Slot;
                    bytes[03] = 0x00;
                    bytes[04] = 0x00;
                    break;
                case Constants.ItemLocationType.Ground:
                    bytes[00] = GroundLocation.X.Low();
                    bytes[01] = GroundLocation.X.High();
                    bytes[02] = GroundLocation.Y.Low();
                    bytes[03] = GroundLocation.Y.High(); 
                    bytes[04] = (byte)GroundLocation.Z;
                    break;
            }

            return bytes;
        }

        public Location ToLocation()
        {
            Location newPos = new Location();

            switch (Type)
            {
                case Constants.ItemLocationType.Container:
                    newPos.X = 0xFFFF;
                    newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)(0x40 + ContainerId), 0x00 }, 0);
                    newPos.Z = (int)ContainerSlot;
                    break;
                case Constants.ItemLocationType.Slot:
                    newPos.X = 0xFFFF;
                    newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)Slot, 0x00 }, 0);
                    newPos.Z = 0;
                    break;
                case Constants.ItemLocationType.Ground:
                    newPos = GroundLocation;
                    break;
            }

            return newPos;
        }
    }
}
