using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents the Tibia map in memory.
    /// </summary>
    public class Map
    {
        private Client client;

        /// <summary>
        /// Create a map object.
        /// </summary>
        /// <param name="c"></param>
        public Map(Client c)
        {
            client = c;
        }

        /// <summary>
        /// Replace all the tile matching a certain criteria with a new id.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="newTileId"></param>
        /// <returns></returns>
        public int ReplaceTile(Predicate<int> match, int newTileId)
        {
            uint mapBegin = Convert.ToUInt32(client.ReadInt(Memory.Addresses.Map.MapPointer));
            int replacedTileCount = 0;

            // search through map data
            for (uint i = mapBegin; i < mapBegin + (Memory.Addresses.Map.Step_Square * Memory.Addresses.Map.Max_Squares); i += Memory.Addresses.Map.Step_Square)
            {
                // get tile id from current tile data
                uint j = i + Memory.Addresses.Map.Distance_Square_Objects + Memory.Addresses.Map.Distance_Object_Id;

                // read tile id
                int tileId = client.ReadInt(j);

                // skip blank ids
                if (tileId == 0)
                    continue;

                // tile id matches old tile id
                if (match(tileId))
                {
                    // replace current tile id with new tile id
                    client.WriteInt(j, newTileId);
                    replacedTileCount++;
                }
            }

            return replacedTileCount;
        }

        /// <summary>
        /// Replace all tiles matching the old id with the new id.
        /// </summary>
        /// <param name="oldTileId"></param>
        /// <param name="newTileId"></param>
        /// <returns></returns>
        public int ReplaceTile(int oldTileId, int newTileId)
        {
            return ReplaceTile(delegate(int i)
            {
                return i == oldTileId;
            }, newTileId);
        }

        /// <summary>
        /// Replace all tiles in a list with a new id.
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="newTileId"></param>
        /// <returns></returns>
        public int ReplaceTile(List<int> idList, int newTileId)
        {
            return ReplaceTile(delegate(int i)
            {
                return idList.Contains(i);
            }, newTileId);
        }

        /// <summary>
        /// Find player on local map
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public Tile GetPlayerTile()
        {
            Tile playerLocation = new Tile();

            uint mapBegin = Convert.ToUInt32(client.ReadInt(Memory.Addresses.Map.MapPointer));

            uint squarePointer = mapBegin;

            for (uint i = 0; i < Memory.Addresses.Map.Max_Squares; i++)
            {
                if (client.ReadInt(squarePointer + Memory.Addresses.Map.Distance_Square_ObjectCount) > 1)
                {
                    uint objectPointer = squarePointer + Memory.Addresses.Map.Distance_Square_Objects;
                    for (uint j = 0; j < Memory.Addresses.Map.Max_Square_Objects; j++)
                    {
                        if (client.ReadInt(objectPointer + Memory.Addresses.Map.Distance_Object_Id) == 99)
                        {
                            int objectId = client.ReadInt(objectPointer + Memory.Addresses.Map.Distance_Object_Data);
                            int playerId = client.ReadInt(Memory.Addresses.Player.Id);
                            if (objectId == playerId)
                            {
                                playerLocation.Id = i;
                                return playerLocation;
                            }
                        }
                        objectPointer += Memory.Addresses.Map.Step_Square_Object;
                    }
                }
                squarePointer += Memory.Addresses.Map.Step_Square;
            }

            return playerLocation;
        }

        /// <summary>
        /// Convert a tiles number to xyz coordinates.
        /// </summary>
        /// <param name="tileNumber"></param>
        /// <returns></returns>
        public static Location GetLocation(uint tileNumber)
        {
            Location l = new Location();

            l.Z = Convert.ToInt32(tileNumber / (14 * 18));
            l.Y = Convert.ToInt32((tileNumber - l.Z * 14 * 18) / 18);
            l.X = Convert.ToInt32((tileNumber - l.Z * 14 * 18) - l.Y * 18);

            return l;
        }
    }
}
