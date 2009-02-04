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
        
        #region Constructor
        /// <summary>
        /// Create a map object.
        /// </summary>
        /// <param name="c"></param>
        public Map(Client c)
        {
            client = c;
        }
        #endregion

        #region Get Squares

        /// <summary>
        /// Get all the adjacent squares to a world location, including the square at that location
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public List<Tile> GetTilesAdjacentTo(Location worldLocation)
        {
            Tile playerTile = GetTileWithPlayer();
            List<Tile> tiles = new List<Tile>(9);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Location offset = new Location(worldLocation.X + x,
                        worldLocation.Y + y, worldLocation.Z);
                    tiles.Add(createMapTile(offset, playerTile));
                }
            }
            return tiles;
        }

        public Tile GetTileWithPlayer()
        {
            int playerId = client.ReadInt32(Addresses.Player.Id);
            return getSingleTile(GetTilesWithObject(new TileObject(
                0x63, playerId, 0), false, false));
        }

        public List<Tile> GetTilesWithGround(uint groundId, bool sameFloor)
        {
            return GetTiles(delegate(Tile tile)
            {
                if (tile.Ground.Id == groundId)
                    return true;

                return false;
            }, sameFloor);
        }

        public List<Tile> GetTilesWithObject(TileObject testObject, bool sameFloor)
        {
            return GetTilesWithObject(testObject, sameFloor, true);
        }

        public List<Tile> GetTilesWithObject(TileObject testObject, bool sameFloor, bool getWorldLocation)
        {
            return GetTiles(delegate(Tile tile)
            {
                foreach (var oldObject in tile.Objects)
                {
                    if ((testObject.Id == 0 || oldObject.Id == testObject.Id) &&
                        (testObject.Data == 0 || oldObject.Data == testObject.Data) &&
                        (testObject.DataEx == 0 || oldObject.DataEx == testObject.DataEx))
                        return true;
                }
                return false;
            }, sameFloor, getWorldLocation, Addresses.Map.Max_Squares);
        }

        public Tile GetTile(Predicate<Tile> match, bool sameFloor)
        {
            return getSingleTile(GetTiles(match, sameFloor, true, 1));
        }

        private Tile getSingleTile(List<Tile> tiles)
        {
            if (tiles.Count > 0)
                return tiles[0];
            else
                return null;
        }

        public List<Tile> GetTiles(Predicate<Tile> match, bool sameFloor)
        {
            return GetTiles(match, sameFloor, true, Addresses.Map.Max_Squares);
        }

        public List<Tile> GetTiles(Predicate<Tile> match, bool sameFloor, bool getWorldLocation, uint maxTiles)
        {
            List<Tile> tiles = new List<Tile>();
            Tile playerTile = null;
            uint startNumber = 0;
            uint endNumber = Addresses.Map.Max_Squares + 1;

            if (sameFloor)
            {
                playerTile = GetTileWithPlayer();

                if (playerTile != null)
                {
                    int playerFloor = playerTile.MemoryLocation.Z;
                    startNumber = new Location(0, 0, playerFloor).ToTileNumber();
                    endNumber = new Location(0, 0, playerFloor + 1).ToTileNumber();
                }
            }

            if (getWorldLocation && playerTile == null)
                playerTile = GetTileWithPlayer();

            for (uint i = startNumber; i < endNumber; i++)
            {
                Tile mapTile;

                if (getWorldLocation)
                    mapTile = createMapTile(i, playerTile);
                else
                    mapTile = createMapTile(i);

                 if (match(mapTile))
                    tiles.Add(mapTile);

                if (tiles.Count >= maxTiles) 
                    break;
            }

            return tiles;
        }
        #endregion

        #region Conversions
        public Location OffsetMemoryLocation(Location loc, int offsetX, int offsetY)
        {
            Location newLoc = new Location();

            newLoc.X = loc.X + offsetX;

            if (newLoc.X < 0) 
                newLoc.X += 18;
            if (newLoc.X > 17) 
                newLoc.X -= 18;

            newLoc.Y = loc.Y + offsetY;
            if (newLoc.Y < 0) 
                newLoc.Y += 14;
            if (newLoc.Y > 13) 
                newLoc.Y -= 14;

            newLoc.Z = loc.Z;

            return newLoc;
        }
        #endregion

        #region Create MapSquare
        /// <summary>
        /// Get the map square at the specified world location.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Tile CreateMapTile(Location worldLocation)
        {
            return createMapTile(worldLocation, GetTileWithPlayer());
        }

        private Tile createMapTile(Location worldLocation, Tile playerTile)
        {
            Location memoryLocation = worldLocation.ToMemoryLocation(playerTile, client);
            uint tileNumber = memoryLocation.ToTileNumber();
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber, worldLocation);
        }

        private Tile createMapTile(uint tileNumber)
        {
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber);
        }

        private Tile createMapTile(uint tileNumber, Tile playerTile)
        {
            Location worldLocation = tileNumber.ToMemoryLocation().ToWorldLocation(playerTile, client);
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber, worldLocation);
        }
        #endregion

        #region Replace

        /// <summary>
        /// Replace all tiles in a list with a new id.
        /// </summary>
        public int ReplaceGround(List<uint> idList, uint newTileId, bool sameFloor)
        {
            return GetTiles(delegate(Tile mapTile)
            {
                if (idList.Contains(mapTile.Ground.Id))
                {
                    mapTile.ReplaceGround(newTileId);
                    return true;
                }
                return false;
            }, sameFloor).Count;
        }

        /// <summary>
        /// Replace all tiles matching the old id with the new id.
        /// </summary>
        public int ReplaceGround(uint oldTileId, uint newTileId, bool sameFloor)
        {
            return ReplaceGround(new List<uint>() { oldTileId }, newTileId, sameFloor);
        }

        public int ReplaceObjects(List<TileObject> objectList, TileObject newObject, bool sameFloor)
        {
            return GetTiles(delegate(Tile mapTile)
            {
                foreach (TileObject oldObject in mapTile.Objects)
                {
                    foreach (TileObject testObject in objectList)
                    {
                        if ((testObject.Id == 0 || oldObject.Id == testObject.Id) &&
                            (testObject.Data == 0 || oldObject.Data == testObject.Data) &&
                            (testObject.DataEx == 0 || oldObject.DataEx == testObject.DataEx))
                        {
                            mapTile.ReplaceObject(oldObject, newObject);
                            return true;
                        }
                    }
                }
                return false;
            }, sameFloor).Count;
        }

        public int ReplaceObject(TileObject testObject, TileObject newObject, bool sameFloor)
        {
            return ReplaceObjects(new List<TileObject>() { testObject },
                newObject, sameFloor);
        }

        /// <summary>
        /// Replace all the trees on the map with small fir trees.
        /// </summary>
        /// <returns></returns>
        public void ReplaceTrees()
        {
            uint smallFirTreeId = 3682;
            byte[] smallFirTreeBytes = client.ReadBytes(
                client.ReadUInt32(
                    client.ReadUInt32(Addresses.Client.DatPointer) + 8)
                + Addresses.DatItem.Sprite
                + (uint)(0x4C * (smallFirTreeId - 100)), 3);
            foreach (int id in Tibia.Constants.Items.TreeArray)
            {
                uint address = client.ReadUInt32(client.ReadUInt32(Addresses.Client.DatPointer) + 8) 
                    + Addresses.DatItem.Sprite 
                    + (uint)(0x4C * (id - 100));
                client.WriteBytes(address, smallFirTreeBytes, 3); 
            }
        }
        #endregion

        #region Special Purpose
        public List<Tile> GetFishTiles()
        {
            Player player = client.GetPlayer();
            List<Tile> tiles = new List<Tile>();
            List<uint> fishIds = Constants.Tiles.Water.GetFishIds();
            GetTiles(delegate(Tile tile)
            {
                if (fishIds.Contains(tile.Ground.Id))
                    if (tile.Location.Z == player.Location.Z && tile.Location.X - player.Location.X < 7 && tile.Location.Y - player.Location.Y < 6)
                    {
                        tiles.Add(tile);
                        return true;
                    }
                return false;
            }, true);
            return tiles;
        }
        #endregion

        #region Name Spy
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
        #endregion

        #region Level Spy
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

                tempPtr = client.ReadInt32(Addresses.Map.LevelSpyPtr);
                tempPtr += Addresses.Map.LevelSpyAdd1;
                tempPtr = client.ReadInt32(tempPtr);
                tempPtr += (int)Addresses.Map.LevelSpyAdd2;

                playerZ = client.ReadInt32(Addresses.Player.Z);

                if (playerZ <= 7)
                {
                    if (playerZ - floor >= 0 && playerZ - floor <= 7)
                    {
                        playerZ = 7 - playerZ;
                        client.WriteInt32(tempPtr, playerZ + floor);
                        return true;
                    }
                }
                else
                {
                    if (floor >= -2 && floor <= 2 && playerZ - floor < 16)
                    {
                        client.WriteInt32(tempPtr, 2 + floor);
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
        #endregion

        #region Full Light
        /// <summary>
        /// Enable or disable full light.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public void FullLight(bool enable)
        {
            if (enable)
            {
                client.WriteBytes(Addresses.Map.FullLightNop, Addresses.Map.FullLightNopEdited, 2);
                client.WriteByte(Addresses.Map.FullLightAdr, Addresses.Map.FullLightAdrEdited);
            }
            else
            {
                client.WriteBytes(Addresses.Map.FullLightNop, Addresses.Map.FullLightNopDefault, 2);
                client.WriteByte(Addresses.Map.FullLightAdr, Addresses.Map.FullLightAdrDefault);
            }
        }
        #endregion
    }
}
