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
            int playerId = client.ReadInt(Addresses.Player.Id);
            return GetCreatureSquare(playerId);
        }
        public Tile GetCreatureSquare(int Id)
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
                            
                            if (objectId == Id)
                            {
                                playerLocation.Number = i;
                                playerLocation.Location = SquareNumberToLocation(playerLocation.Number);
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
        /// Convert a location to a square number
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static uint LocationToSquareNumber(Location l)
        {
            return Convert.ToUInt32(l.X + l.Y * 18 + l.Z * 14 * 18);
        }

        /// <summary>
        /// Get a squares absoulute coordinates by comparing its relative coordinates with that of the players known coordinates.
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <returns></returns>
        public Location GetAbsoluteLocation(uint squareNumber)
        {
            return GetAbsoluteLocation(squareNumber, GetPlayerSquare());
        }

        /// <summary>
        /// Get a squares absoulute coordinates by comparing its relative coordinates with that of the players known coordinates (using an existing player tile location).
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <param name="playerTile"></param>
        /// <returns></returns>
        public Location GetAbsoluteLocation(uint squareNumber, Tile playerTile)
        {
            Location playerRelative = SquareNumberToLocation(playerTile.Number);

            int xAdjustment = playerTile.Location.X - playerRelative.X;
            int yAdjustment = playerTile.Location.Y - playerRelative.Y;
            int zAdjustment = playerTile.Location.Z - playerRelative.Z;

            Location squareRelative = SquareNumberToLocation(squareNumber);
            return new Location(
                squareRelative.X + xAdjustment,
                squareRelative.Y + yAdjustment,
                squareRelative.Z + zAdjustment);
        }

        public Location GetRelativeLocation(Creature creature, int relX, int relY)
        {
            Tile loc = GetCreatureSquare(creature.Id);
            Location newLoc = new Location();
            newLoc.X = loc.Location.X + relX;
            if (newLoc.X < 0) { newLoc.X += 18; }
            if (newLoc.X > 17) { newLoc.X -= 18; }
            newLoc.Y = loc.Location.Y + relY;
            if (newLoc.Y < 0) { newLoc.Y += 14; }
            if (newLoc.Y > 13) { newLoc.Y -= 14; }
            newLoc.Z = loc.Location.Z;
            return newLoc;
        }

        public Tile GetTileInfo(Location loc)
        {
            Tile temp = new Tile();
            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));
            temp.Number = LocationToSquareNumber(loc);
            temp.Id = (uint)client.ReadInt((mapBegin + (Addresses.Map.Step_Square * temp.Number) + Addresses.Map.Distance_Square_Objects + Addresses.Map.Distance_Object_Id));
            return temp;
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
        /// Enable or disable level spy for the given floor. The floor parameter
        /// is relative to the floor the player is currently on.
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
        /// Get tiles on the same floor as the player. Automatically gets the absolute location of each tile.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(uint id)
        {
            return GetTiles(id, true);
        }

        /// <summary>
        /// Get tiles with specified id. Automatically gets the absolute location of each tile.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sameFloor"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(uint id, bool sameFloor)
        {
            return GetTiles(delegate(uint i)
            {
                return i == id;
            }, sameFloor);
        }

        /// <summary>
        /// Get tiles whose id is in the specified list.
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(List<uint> idList)
        {
            return GetTiles(idList, true);
        }

        /// <summary>
        /// Get tiles whose id is in the specified list.
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="sameFloor"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(List<uint> idList, bool sameFloor)
        {
            return GetTiles(delegate(uint i)
            {
                return idList.Contains(i);
            }, sameFloor);
        }

        /// <summary>
        /// Return a list of tiles that match the specified predicate.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="sameFloor"></param>
        /// <returns></returns>
        public List<Tile> GetTiles(Predicate<uint> match, bool sameFloor)
        {
            List<Tile> tiles = new List<Tile>();
            Location loc = new Location();
            Tile playerTile = new Tile();
            uint squareNumber;

            uint mapBegin = Convert.ToUInt32(client.ReadInt(Addresses.Map.MapPointer));

            uint squarePointer = mapBegin
                + Addresses.Map.Distance_Square_Objects
                + Addresses.Map.Distance_Object_Id;

            playerTile = GetPlayerSquare();

            // Fast, only check the player z
            if (sameFloor)
            {
                loc.Z = Map.SquareNumberToLocation(playerTile.Number).Z;

                for (int y = 0; y < 14; y++)
                {
                    loc.Y = y;
                    for (int x = 0; x < 18; x++)
                    {
                        loc.X = x;
                        squareNumber = LocationToSquareNumber(loc);
                        uint id = Convert.ToUInt32(client.ReadInt(squarePointer +
                            squareNumber * Addresses.Map.Step_Square));
                        if (match(id))
                        {
                            Tile temp = new Tile(squareNumber);
                            temp.Id = id;
                            temp.Location = GetAbsoluteLocation(squareNumber, playerTile);
                            tiles.Add(temp);
                        }
                    }
                }
            }
            else // Slow way
            {
                for (uint i = 0; i < Addresses.Map.Max_Squares; i++)
                {
                    uint id = Convert.ToUInt32(client.ReadInt(squarePointer));
                    if (match(id))
                    {
                        Tile temp = new Tile(i);
                        temp.Id = id;
                        temp.Location = GetAbsoluteLocation(i, playerTile);
                        tiles.Add(temp);
                    }
                    squarePointer += Addresses.Map.Step_Square;
                }
            }

            return tiles;
        }

        /// <summary>
        /// Returns all of the tiles in a list of tiles that are within
        /// viewing range of the centerTile.
        /// </summary>
        /// <param name="tiles">List to look through.</param>
        /// <param name="centerTile">The center tile.</param>
        /// <returns>The list that contains all of the tiles within viewing range.</returns>
        public List<Tile> GetTilesWithinView(List<Tile> tiles, Tile centerTile)
        {
            List<Tile> newtiles = new List<Tile>();

            for (int i = 0; i < tiles.Count; ++i)
            {
                //Not counting the center square we can see 7 squares to the
                //right and left and 5 squares to the top and bottom.

                int x = Math.Abs(tiles[i].Location.X - centerTile.Location.X);
                int y = Math.Abs(tiles[i].Location.Y - centerTile.Location.Y);

                if (x <= 7 && y <= 5)
                    newtiles.Add(tiles[i]);
            }

            return newtiles;
        }

        /// <summary>
        /// Get all the water tiles with fish.
        /// </summary>
        /// <returns></returns>
        public List<Tile> GetFishTiles()
        {
            return GetTiles(Constants.Tiles.Water.GetFishIds(), true);
        }
    }
}
