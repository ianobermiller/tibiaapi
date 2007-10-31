using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a square on the map.
    /// </summary>
    public class MapSquare
    {
        private uint address;
        private Client client;
        public int objectCount;
        public Tile tile;
        public List<MapObject> objects;

        public MapSquare(Client c, uint a)
        {
            client = c;
            address = a;
            Refresh();
        }

        public void Refresh()
        {
            uint pointer;

            objectCount = client.readInt(address + Addresses.Map.Distance_Square_ObjectCount);

            // Get the tile data (first object)
            pointer = address + Addresses.Map.Distance_Square_Objects;
            tile = new Tile(Convert.ToUInt32(client.readInt(pointer + Addresses.Map.Distance_Object_Id)));

            // Get all the objects above the tile, first clear out the old objects
            objects.Clear();

            for (uint i = 0; i < objectCount; i++)
            {
                // Go to the next object
                pointer += Addresses.Map.Step_Square_Object;

                objects.Add(new MapObject(
                    client.readInt(pointer + Addresses.Map.Distance_Object_Id),
                    client.readInt(pointer + Addresses.Map.Distance_Object_Data),
                    client.readInt(pointer + Addresses.Map.Distance_Object_Data_Ex)));
            }
        }
    }

    /// <summary>
    /// Represents an object on a MapSquare
    /// </summary>
    public struct MapObject
    {
        public int id;
        public int data;
        public int dataex;

        public MapObject(int id, int data, int dataex)
        {
            this.id = id;
            this.data = data;
            this.dataex = dataex;
        }
    }
}
