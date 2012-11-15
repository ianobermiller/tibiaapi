using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents an memory or packet tile.
    /// </summary>
    public class Tile
    {
        #region Vars
        private ObjectType type;

        private int objectCount;
        private uint address;
        private uint squareNumber;

        private Location location;
        private Location memoryLocation;

        private Item ground;
        private List<Item> items;
        private Client client;
        #endregion

        #region Constructors
        //memory tile contructors
        internal Tile(Client client, uint address, uint squareNumber, Location location)
            : this(client, address, squareNumber)
        {
            this.location = location;
        }

        internal Tile(Client client, uint address, uint squareNumber)
        {
            this.type = ObjectType.Memory;
            this.client = client;
            this.squareNumber = squareNumber;
            this.address = address;
            this.memoryLocation = squareNumber.ToMemoryLocation();

            this.ground = new Item(client, client.Memory.ReadUInt32(address +
                client.Addresses.Map.DistanceTileItems + client.Addresses.Map.DistanceItemId));
        }

        //packet tile constructors
        internal Tile(Client client, uint groundId, Location location)
        {
            this.type = ObjectType.Packet;
            this.client = client;
            this.location = location;
            this.items = new List<Item>();

            if (groundId > 0)
                this.ground = new Item(client, groundId);
        }
        #endregion

        #region Properties
        public ObjectType Type
        {
            get { return type; }
        }

        public Location Location
        {
            get { return location; }
            internal set { location = value; }
        }

        public Client Client
        {
            get { return client; }
        }

        public Item Ground
        {
            get { return ground; }
            set { ground = value; }
        }
        public uint TileNumber
        {
            get { return squareNumber; }
        }

        public uint Address
        {
            get { return address; }
        }

        public int ObjectCount
        {
            get
            {
                if (type == ObjectType.Packet)
                    return items.Count;
                else
                {
                    objectCount = client.Memory.ReadInt32(address + client.Addresses.Map.DistanceTileItemsCount);
                    return objectCount;
                }
            }
        }

        public List<TileObject> Objects
        {
            get
            {
                if (type == ObjectType.Packet)
                    return null;

                List<TileObject> objects = new List<TileObject>();
                uint pointerItems = address + client.Addresses.Map.DistanceTileItems;
                uint pointerOrder = address + client.Addresses.Map.DistanceTileItemOrder;
                for (uint i = 0; i < objectCount; i++)
                {

                    objects.Add(new TileObject(
                        client.Memory.ReadUInt32(pointerItems + client.Addresses.Map.DistanceItemId),
                        client.Memory.ReadUInt32(pointerItems + client.Addresses.Map.DistanceItemData),
                        client.Memory.ReadUInt32(pointerItems + client.Addresses.Map.DistanceItemDataEx),
                        client.Memory.ReadUInt32(pointerOrder),
                        this));

                    pointerItems += client.Addresses.Map.StepTileObject;
                    pointerOrder += 4;
                }

                return objects;
            }
        }

        public Location MemoryLocation
        {
            get
            {
                if (type == ObjectType.Memory)
                    return memoryLocation;
                else
                    return Location.Invalid;
            }
            set { memoryLocation = value; }
        }

        public List<Item> Items
        {
            get
            {
                if (items != null)
                    return items;
                else
                {
                    items = new List<Item>();

                    foreach (TileObject tileObject in Objects)
                        items.Add(new Item(client, (uint)tileObject.Id, (byte)tileObject.Data, "",
                            ItemLocation.FromLocation(location, (byte)tileObject.StackOrder)));

                    return items;
                }
            }
        }
        #endregion

        #region Public Methods
        public bool ReplaceGround(uint newId)
        {
            if (type == ObjectType.Packet)
                return false;

            return client.Memory.WriteUInt32(address +
               client.Addresses.Map.DistanceTileItems +
               client.Addresses.Map.DistanceItemId, newId);
        }

        public bool ReplaceObject(TileObject oldObject, TileObject newObject)
        {
            if (type == ObjectType.Packet)
                return false;

            uint pointer = (uint)(address +
                (client.Addresses.Map.DistanceTileItems +
               client.Addresses.Map.StepTileObject * oldObject.StackOrder));

            return client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemId, newObject.Id) &&
                client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemData, newObject.Data) &&
                client.Memory.WriteUInt32(pointer + client.Addresses.Map.DistanceItemDataEx, newObject.DataEx);
        }
        #endregion
    }

    #region TileObject
    /// <summary>
    /// Represents an object on a memory Tile
    /// </summary>
    public class TileObject
    {
        public uint StackOrder { get; set; }
        public uint Id { get; set; }
        public uint Data { get; set; }
        public uint DataEx { get; set; }
        public Tile FromTile { get; set; }

        public TileObject(uint id, uint data, uint dataEx)
            : this(id, data, dataEx, 0, null) { }

        public TileObject(uint id, uint data, uint dataEx, uint stackOrder, Tile fromTile)
        {
            StackOrder = stackOrder;
            Id = id;
            Data = data;
            DataEx = dataEx;
            FromTile = fromTile;
        }
    }
    #endregion
}
