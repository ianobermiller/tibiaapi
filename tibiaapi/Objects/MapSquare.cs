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
        public Tile Tile = new Tile();
        public List<MapObject> Objects;

        public MapSquare(Client c, uint addr, Location location)
        {
            client = c;
            address = addr;
            Tile.Location = location;
            GetData();
        }

        private void GetData()
        {
            uint pointer;

            int objectCount = client.ReadInt(address + Addresses.Map.Distance_Square_ObjectCount) - 1; // -1 for Tile

            // Get the tile data (first object)
            pointer = address + Addresses.Map.Distance_Square_Objects;
            Tile.Id = Convert.ToUInt32(client.ReadInt(pointer + Addresses.Map.Distance_Object_Id));

            Objects = new List<MapObject>(objectCount);

            for (uint i = 0; i < objectCount; i++)
            {
                // Go to the next object
                pointer += Addresses.Map.Step_Square_Object;

                Objects.Add(new MapObject(
                    client.ReadInt(pointer + Addresses.Map.Distance_Object_Id),
                    client.ReadInt(pointer + Addresses.Map.Distance_Object_Data),
                    client.ReadInt(pointer + Addresses.Map.Distance_Object_Data_Ex)));
            }
        }
    }

    /// <summary>
    /// Represents an object on a MapSquare
    /// </summary>
    public struct MapObject
    {
        public int Id;
        public int Data;
        public int DataEx;

        public MapObject(int id, int data, int dataex)
        {
            this.Id = id;
            this.Data = data;
            this.DataEx = dataex;
        }
    }
}
