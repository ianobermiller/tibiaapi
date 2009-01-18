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
        private ObjectType _type;

        private int _objectCount;
        private uint _address;
        private uint _squareNumber;

        private Location _location;
        private Location _memoryLocation;

        private Item _ground;
        private List<Item> _items;
        private Client _client;
        #endregion

        #region Constructors
        //memory tile contructors
        public Tile(Client client, uint address, uint squareNumber, Location location)
            : this(client, address, squareNumber)
        {
            _location = location;
        }

        public Tile(Client client, uint address, uint squareNumber)
        {
            _type = ObjectType.Memory;
            _client = client;
            _squareNumber = squareNumber;
            _address = address;
            _memoryLocation = squareNumber.ToMemoryLocation();

            _objectCount = _client.ReadInt32(address + Tibia.Addresses.Map.Distance_Square_ObjectCount) - 1;
            _ground = new Item(_client, _client.ReadUInt32(address + 
                Tibia.Addresses.Map.Distance_Square_Objects + Tibia.Addresses.Map.Distance_Object_Id));
        }

        //packet tile constructors
        public Tile(Client client, uint groundId, Location location)
        {
            _type = ObjectType.Packet;
            _client = client;
            _location = location;
            _items = new List<Item> { };

            if (groundId > 0)
                _ground = new Item(_client, groundId);
        }
        #endregion

        #region Proprietys
        public ObjectType Type
        {
            get { return _type; }
        }

        public List<TileObject> Objects
        {
            get
            {
                if (_type == ObjectType.Packet)
                    return null;

                List<TileObject> objects = new List<TileObject>();
                uint pointer = _address + Addresses.Map.Distance_Square_Objects;

                for (int i = 0; i < _objectCount; i++)
                {
                    // Go to the next object
                    pointer += Addresses.Map.Step_Square_Object;

                    objects.Add(new TileObject(
                        _client.ReadInt32(pointer + Addresses.Map.Distance_Object_Id),
                        _client.ReadInt32(pointer + Addresses.Map.Distance_Object_Data),
                        _client.ReadInt32(pointer + Addresses.Map.Distance_Object_Data_Ex),
                        i + 1));
                }

                return objects;
            }
        }

        public Location MemoryLocation
        {
            get
            {
                if (_type == ObjectType.Memory)
                    return _memoryLocation;
                else
                    return Location.Invalid;
            }
            set { _memoryLocation = value; }
        }

        public Location Location
        {
            get { return _location; }
        }

        public Client Client
        {
            get { return _client; }
        }

        public List<Item> Items
        {
            get
            {
                if (_type == ObjectType.Packet)
                    return _items;
                else
                {
                    List<Item> items = new List<Item>();

                    foreach (TileObject tileObject in Objects)
                        items.Add(new Item(_client, (uint)tileObject.Id, (byte)tileObject.Data, "",
                            new ItemLocation(_location, (byte)tileObject.StackOrder), true));

                    return items;
                }
            }
        }

        public Item Ground
        {
            get { return _ground; }
            set { _ground = value; }
        }

        public uint Address
        {
            get { return _address; }
        }

        public int ObjectCount
        {
            get
            {
                if (_type == ObjectType.Packet)
                    return _items.Count;
                else
                    return _objectCount;
            }
        }
        #endregion

        #region Public Methods
        public bool ReplaceGround(uint newId)
        {
            if (_type == ObjectType.Packet)
                return false;

            return _client.WriteUInt32(_address +
                Addresses.Map.Distance_Square_Objects +
                Addresses.Map.Distance_Object_Id, newId);
        }

        public bool ReplaceObject(TileObject oldObject, TileObject newObject)
        {
            if (_type == ObjectType.Packet)
                return false;

            uint pointer = (uint)(_address +
                (Addresses.Map.Distance_Square_Objects +
                Addresses.Map.Step_Square_Object * oldObject.StackOrder));

            return _client.WriteInt32(pointer + Addresses.Map.Distance_Object_Id, newObject.Id) &&
                _client.WriteInt32(pointer + Addresses.Map.Distance_Object_Data, newObject.Data) &&
                _client.WriteInt32(pointer + Addresses.Map.Distance_Object_Data_Ex, newObject.DataEx);
        }
        #endregion
    }

    /// <summary>
    /// Represents an object on a memory Tile
    /// </summary>
    public class TileObject
    {
        public int StackOrder { get; set; }
        public int Id { get; set; }
        public int Data { get; set; }
        public int DataEx { get; set; }

        public TileObject(int id, int data, int dataEx)
            : this(id, data, dataEx, 0) { }

        public TileObject(int id, int data, int dataEx, int stackOrder)
        {
            StackOrder = stackOrder;
            Id = id;
            Data = data;
            DataEx = dataEx;
        }
    }
}
