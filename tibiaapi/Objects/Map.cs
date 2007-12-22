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

        #region Replace Tiles

        /// <summary>
        /// Replace all the tile matching a certain criteria with a new id.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="newTileId"></param>
        /// <returns></returns>
        public int ReplaceTile(Predicate<int> match, int newTileId)
        {
            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));
            int replacedTileCount = 0;

            // search through map data
            for (uint i = mapBegin; i < mapBegin + (Addresses.Map.Step_Square * Addresses.Map.Max_Squares); i += Addresses.Map.Step_Square)
            {
                // get tile id from current tile data
                uint j = i + Addresses.Map.Distance_Square_Objects + Addresses.Map.Distance_Object_Id;

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

        #endregion

        #region Replace Objects

        /// <summary>
        /// Replace all the object matching a certain criteria with a new id.
        /// Checks the 2nd and 3rd objects (skips 1st because that is the tile).
        /// </summary>
        /// <param name="match"></param>
        /// <param name="newObjectId"></param>
        /// <returns></returns>
        public int replaceObject(Predicate<int> match, int newObjectId)
        {
            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));
            int replacedObjectCount = 0;

            // search through map data
            for (uint i = mapBegin; i < mapBegin + (Addresses.Map.Step_Square * Addresses.Map.Max_Squares); i += Addresses.Map.Step_Square)
            {
                uint addressId = i + Addresses.Map.Distance_Square_Objects + Addresses.Map.Distance_Object_Id;
                for (int j = 2; j <= 3; j++)
                {
                    // get object id from current object data
                    addressId += Addresses.Map.Step_Square_Object;

                    // read object id
                    int objectId = client.ReadInt(addressId);

                    // skip blank ids
                    if (objectId > 0)
                    {
                        // object id matches old object id
                        if (match(objectId))
                        {
                            // replace current object id with new object id
                            client.WriteInt(addressId, newObjectId);
                            replacedObjectCount++;
                        }
                    }
                }
            }

            return replacedObjectCount;
        }

        /// <summary>
        /// Replace all objects matching the old id with the new id.
        /// </summary>
        /// <param name="oldObjectId"></param>
        /// <param name="newObjectId"></param>
        /// <returns></returns>
        public int replaceObject(int oldObjectId, int newObjectId)
        {
            return replaceObject(delegate(int i)
            {
                return i == oldObjectId;
            }, newObjectId);
        }

        /// <summary>
        /// Replace all objects in a list with a new id.
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="newObjectId"></param>
        /// <returns></returns>
        public int replaceObject(List<int> idList, int newObjectId)
        {
            return replaceObject(delegate(int i)
            {
                return idList.Contains(i);
            }, newObjectId);
        }

        /// <summary>
        /// Replace all the trees on the map with small fir trees.
        /// </summary>
        /// <returns></returns>
        public int replaceTrees()
        {
            int[] treearray = { 3608, 3614, 3615, 3616, 3617,
                3618, 3619, 3620, 3621, 3622,
                3623, 3624, 3625, 3609, 3613,
                3626, 3632, 3633, 3634, 3636,
                3637, 3638, 3639, 3640, 3641,
                3647, 3649 };
            List<int> trees = new List<int>(treearray);
            return replaceObject(trees, 3682);
        }

        #endregion

        /// <summary>
        /// Find player on local map
        /// </summary>
        /// <returns></returns>
        public Tile GetPlayerSquare()
        {
            Tile playerLocation = new Tile();

            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));

            uint squarePointer = mapBegin;

            for (uint i = 0; i < Addresses.Map.Max_Squares; i++)
            {
                if (client.ReadInt(squarePointer + Addresses.Map.Distance_Square_ObjectCount) > 1)
                {
                    uint objectPointer = squarePointer + Addresses.Map.Distance_Square_Objects;
                    for (uint j = 0; j < Addresses.Map.Max_Square_Objects; j++)
                    {
                        if (client.ReadInt(objectPointer + Addresses.Map.Distance_Object_Id) == 99)
                        {
                            int objectId = client.ReadInt(objectPointer + Addresses.Map.Distance_Object_Data);
                            int playerId = client.ReadInt(Addresses.Player.Id);
                            if (objectId == playerId)
                            {
                                playerLocation.Id = i;
                                Player player = client.GetPlayer();
                                playerLocation.Location = new Location(player.X, player.Y, player.Z);
                                return playerLocation;
                            }
                        }
                        objectPointer += Addresses.Map.Step_Square_Object;
                    }
                }
                squarePointer += Addresses.Map.Step_Square;
            }

            return playerLocation;
        }

        /// <summary>
        /// Convert a tiles number to xyz coordinates.
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <returns></returns>
        public static Location SquareNumberToLocation(uint squareNumber)
        {
            Location l = new Location();

            l.Z = Convert.ToInt32(squareNumber / (14 * 18));
            l.Y = Convert.ToInt32((squareNumber - l.Z * 14 * 18) / 18);
            l.X = Convert.ToInt32((squareNumber - l.Z * 14 * 18) - l.Y * 18);

            return l;
        }

        /// <summary>
        /// Get a squares absoulute coordinates by comparing its relative coordinates with that of the players known coordinates.
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <returns></returns>
        public Location GetAbsoluteLocation(uint squareNumber)
        {
            Tile player = GetPlayerSquare();
            Location playerRelative = SquareNumberToLocation(player.Id);

            int xAdjustment = player.Location.X - playerRelative.X;
            int yAdjustment = player.Location.Y - playerRelative.Y;
            int zAdjustment = player.Location.Z - playerRelative.Z;

            Location squareRelative = SquareNumberToLocation(squareNumber);
            return new Location(
                squareRelative.X + xAdjustment,
                squareRelative.Y + yAdjustment,
                squareRelative.Z + zAdjustment);
        }
    }
}
