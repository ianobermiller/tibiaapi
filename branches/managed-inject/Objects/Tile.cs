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

            this.objectCount = client.Memory.ReadInt32(address + Tibia.Addresses.Map.DistanceTileObjectCount) - 1;
            this.ground = new Item(client, client.Memory.ReadUInt32(address + 
                Tibia.Addresses.Map.DistanceTileObjects + Tibia.Addresses.Map.DistanceObjectId));
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
                    return objectCount;
            }
        }

        public List<TileObject> Objects
        {
            get
            {
                if (type == ObjectType.Packet)
                    return null;

                List<TileObject> objects = new List<TileObject>();
                uint pointer = address + Addresses.Map.DistanceTileObjects;

                if (objectCount <= 11)
                {
                    for (uint i = 0; i < objectCount; i++)
                    {
                        // Go to the next object
                        pointer += Addresses.Map.StepTileObject;

                        objects.Add(new TileObject(
                            client.Memory.ReadUInt32(pointer + Addresses.Map.DistanceObjectId),
                            client.Memory.ReadUInt32(pointer + Addresses.Map.DistanceObjectData),
                            client.Memory.ReadUInt32(pointer + Addresses.Map.DistanceObjectDataEx),
                            i + 1));
                    }
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
                Addresses.Map.DistanceTileObjects +
                Addresses.Map.DistanceObjectId, newId);
        }

        public bool ReplaceObject(TileObject oldObject, TileObject newObject)
        {
            if (type == ObjectType.Packet)
                return false;

            uint pointer = (uint)(address +
                (Addresses.Map.DistanceTileObjects +
                Addresses.Map.StepTileObject * oldObject.StackOrder));

            return client.Memory.WriteUInt32(pointer + Addresses.Map.DistanceObjectId, newObject.Id) &&
                client.Memory.WriteUInt32(pointer + Addresses.Map.DistanceObjectData, newObject.Data) &&
                client.Memory.WriteUInt32(pointer + Addresses.Map.DistanceObjectDataEx, newObject.DataEx);
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

        public TileObject(uint id, uint data, uint dataEx)
            : this(id, data, dataEx, 0) { }

        public TileObject(uint id, uint data, uint dataEx, uint stackOrder)
        {
            StackOrder = stackOrder;
            Id = id;
            Data = data;
            DataEx = dataEx;
        }
    }
    #endregion
}
