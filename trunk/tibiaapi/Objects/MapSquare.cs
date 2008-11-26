using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a square on the map.
    /// </summary>
    public class MapSquare
    {
        private Client client;
        public uint Address;
        public uint SquareNumber;
        public Location MemoryLocation;
        public int ObjectCount;
        public Tile Tile = new Tile();

        public List<Item> Items
        {
            get
            {
                List<Item> items = new List<Item>();

                foreach (MapObject mo in Objects)
                {
                    Item item = new Item(new ItemLocation(Tile.Location, (byte)mo.StackOrder));
                    item.Client = client;
                    item.Id = (uint)mo.Id;
                    item.Count = (byte)mo.Data;
                    items.Add(item);
                }

                return items;
            }
        }

        public List<MapObject> Objects
        {
            get
            {
                List<MapObject> objects = new List<MapObject>();

                uint pointer = Address + Addresses.Map.Distance_Square_Objects;

                for (int i = 0; i < ObjectCount; i++)
                {
                    // Go to the next object
                    pointer += Addresses.Map.Step_Square_Object;

                    objects.Add(new MapObject(
                        client.ReadInt(pointer + 
                            Addresses.Map.Distance_Object_Id),
                        client.ReadInt(pointer + 
                            Addresses.Map.Distance_Object_Data),
                        client.ReadInt(pointer + 
                            Addresses.Map.Distance_Object_Data_Ex),
                        i + 1));
                }

                return objects;
            }
        }

        public MapSquare(Client client, uint address, uint squareNumber, Location location)
            : this(client, address, squareNumber)
        {
            Tile.Location = location;
        }

        public MapSquare(Client client, uint address, uint squareNumber)
        {
            this.client = client;
            this.SquareNumber = squareNumber;
            this.Address = address;
            this.MemoryLocation = Map.ConvertSquareNumberToMemoryLocation(squareNumber);

            ObjectCount = client.ReadInt(address + Addresses.Map.Distance_Square_ObjectCount) - 1; // -1 for Tile

            // Get the tile data (first object)
            Tile.Id = Convert.ToUInt32(client.ReadInt(address + 
                Addresses.Map.Distance_Square_Objects + 
                Addresses.Map.Distance_Object_Id));
        }

        public void ReplaceTile(uint newId)
        {
            client.WriteInt(Address + 
                Addresses.Map.Distance_Square_Objects + 
                Addresses.Map.Distance_Object_Id, (int)newId);
        }

        public void ReplaceObject(MapObject oldObject, MapObject newObject)
        {
            uint pointer = (uint)(Address +
                (Addresses.Map.Distance_Square_Objects +
                Addresses.Map.Step_Square_Object * oldObject.StackOrder));
            client.WriteInt(pointer + 
                Addresses.Map.Distance_Object_Id,
                newObject.Id);
            client.WriteInt(pointer +
                Addresses.Map.Distance_Object_Data,
                newObject.Data);
            client.WriteInt(pointer +
                Addresses.Map.Distance_Object_Data_Ex,
                newObject.DataEx);
        }
    }

    /// <summary>
    /// Represents an object on a MapSquare
    /// </summary>
    public struct MapObject
    {
        public int StackOrder;
        public int Id;
        public int Data;
        public int DataEx;
        public MapObject(int id, int data, int dataex)
            : this(id, data, dataex, 0)
        { }
        public MapObject(int id, int data, int dataex, int stackOrder)
        {
            this.StackOrder = stackOrder;
            this.Id = id;
            this.Data = data;
            this.DataEx = dataex;
        }
    }
}
