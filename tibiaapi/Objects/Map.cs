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
        public int ReplaceObject(Predicate<int> match, int newObjectId)
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
        public int ReplaceObject(int oldObjectId, int newObjectId)
        {
            return ReplaceObject(delegate(int i)
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
        public int ReplaceObject(List<int> idList, int newObjectId)
        {
            return ReplaceObject(delegate(int i)
            {
                return idList.Contains(i);
            }, newObjectId);
        }

        /// <summary>
        /// Replace all the trees on the map with small fir trees.
        /// </summary>
        /// <returns></returns>
        public int ReplaceTrees()
        {
            int[] treearray = { 3608, 3614, 3615, 3616, 3617,
                3618, 3619, 3620, 3621, 3622,
                3623, 3624, 3625, 3609, 3613,
                3626, 3632, 3633, 3634, 3636,
                3637, 3638, 3639, 3640, 3641,
                3647, 3649 };
            List<int> trees = new List<int>(treearray);
            return ReplaceObject(trees, 3682);
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
                                playerLocation.Number = i;
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
        
	public static uint LocationToSquareNumber(Location loc)
	{
	    uint i = 0;
	    i = Convert.ToUInt32(loc.X + loc.Y * 18 + loc.Z * 14 * 18);
	    return i;
        }

        /// <summary>
        /// Get a squares absoulute coordinates by comparing its relative coordinates with that of the players known coordinates.
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <returns></returns>
        public Location GetAbsoluteLocation(uint squareNumber)
        {
            Tile player = GetPlayerSquare();
            Location playerRelative = SquareNumberToLocation(player.Number);

            int xAdjustment = player.Location.X - playerRelative.X;
            int yAdjustment = player.Location.Y - playerRelative.Y;
            int zAdjustment = player.Location.Z - playerRelative.Z;

            Location squareRelative = SquareNumberToLocation(squareNumber);
            return new Location(
                squareRelative.X + xAdjustment,
                squareRelative.Y + yAdjustment,
                squareRelative.Z + zAdjustment);
        }

        /// <summary>
        /// Enable or disable name spying
        /// </summary>
        /// <param name="enable"></param>
        public void ShowNames(bool enable)
        {
            if (enable)
            {
                client.WriteBytes(Addresses.Map.NameSpy1, Addresses.Map.Nops, 2);
                client.WriteBytes(Addresses.Map.NameSpy2, Addresses.Map.Nops, 2);
            }
            else
            {
                client.WriteBytes(Addresses.Map.NameSpy1, BitConverter.GetBytes(Addresses.Map.NameSpy1Default), 2);
                client.WriteBytes(Addresses.Map.NameSpy2, BitConverter.GetBytes(Addresses.Map.NameSpy2Default), 2);
            }
        }

        /// <summary>
        /// Enable or disable level spy for the given floor
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public bool ShowFloor(int floor, bool enable)
        {
            if (enable)
            {
                int playerZ, tempPtr;

                client.WriteBytes(Addresses.Map.LevelSpy1, Addresses.Map.Nops, 6);
                client.WriteBytes(Addresses.Map.LevelSpy2, Addresses.Map.Nops, 6);
                client.WriteBytes(Addresses.Map.LevelSpy3, Addresses.Map.Nops, 6);

                tempPtr = client.ReadInt(Addresses.Map.LevelSpyPtr);
                tempPtr += Addresses.Map.LevelSpyAdd1;
                tempPtr = client.ReadInt(tempPtr);
                tempPtr += (int)Addresses.Map.LevelSpyAdd2;

                playerZ = client.ReadInt(Addresses.Player.Z);

                if (playerZ <= 7)
                {
                    if (playerZ - floor >= 0 && playerZ - floor <= 7)
                    {
                        playerZ = 7 - playerZ;
                        client.WriteInt(tempPtr, playerZ + floor);
                        return true;
                    }
                }
                else
                {
                    if (floor >= -2 && floor <= 2 && playerZ - floor < 14)
                    {
                        client.WriteInt(tempPtr, 2 + floor);
                        return true;
                    }
                }
            }
            else
            {
                client.WriteBytes(Addresses.Map.LevelSpy1, Addresses.Map.LevelSpyDefault, 6);
                client.WriteBytes(Addresses.Map.LevelSpy2, Addresses.Map.LevelSpyDefault, 6);
                client.WriteBytes(Addresses.Map.LevelSpy3, Addresses.Map.LevelSpyDefault, 6);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Get all tiles with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(uint id)
        {
            List<Tile> tiles = new List<Tile>();

            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));

            uint squarePointer = mapBegin
                + Addresses.Map.Distance_Square_Objects
                + Addresses.Map.Distance_Object_Id;

            for (uint i = 0; i < Addresses.Map.Max_Squares; i++)
            {
                if (client.ReadInt(squarePointer) == id)
                {
                    Tile temp = new Tile(i);
                    temp.Id = id;
                    tiles.Add(temp);
                }
                squarePointer += Addresses.Map.Step_Square;
            }
            return tiles;
        }
    }
}
