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
        public List<MapSquare> GetSquaresAdjacentTo(Location worldLocation)
        {
            MapSquare playerSquare = GetSquareWithPlayer();
            List<MapSquare> squares = new List<MapSquare>(9);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Location offset = new Location(
                        worldLocation.X + x, 
                        worldLocation.Y + y, 
                        worldLocation.Z);
                    squares.Add(CreateMapSquare(offset, playerSquare));
                }
            }
            return squares;
        }

        public MapSquare GetSquareWithPlayer()
        {
            int playerId = client.ReadInt32(Addresses.Player.Id);
            return GetSingleSquare(GetSquaresWithObject(new MapObject(
                0x63,
                playerId,
                0), false, false));
        }

        public List<MapSquare> GetSquaresWithTile(uint tileId, bool sameFloor)
        {
            return GetSquares(delegate(MapSquare square)
            {
                if (square.Tile.Id == tileId)
                    return true;
                return false;
            }, sameFloor);
        }

        public List<MapSquare> GetSquaresWithObject(MapObject testObject, bool sameFloor)
        {
            return GetSquaresWithObject(testObject, sameFloor, true);
        }

        public List<MapSquare> GetSquaresWithObject(MapObject testObject, bool sameFloor, bool getWorldLocation)
        {
            return GetSquares(delegate(MapSquare square)
            {
                foreach (MapObject oldObject in square.Objects)
                {
                    if ((testObject.Id == 0 ||
                            oldObject.Id == testObject.Id) &&
                        (testObject.Data == 0 ||
                            oldObject.Data == testObject.Data) &&
                        (testObject.DataEx == 0 ||
                            oldObject.DataEx == testObject.DataEx))
                        return true;
                }
                return false;
            }, sameFloor, getWorldLocation, Addresses.Map.Max_Squares);
        }

        public MapSquare GetSquare(Predicate<MapSquare> match, bool sameFloor)
        {
            return GetSingleSquare(GetSquares(match, sameFloor, true, 1));
        }

        private MapSquare GetSingleSquare(List<MapSquare> squares)
        {
            if (squares.Count > 0)
            {
                return squares[0];
            }
            else
            {
                return null;
            }
        }

        public List<MapSquare> GetSquares(Predicate<MapSquare> match, bool sameFloor)
        {
            return GetSquares(match, sameFloor, true, Addresses.Map.Max_Squares);
        }

        public List<MapSquare> GetSquares(Predicate<MapSquare> match, bool sameFloor, bool getWorldLocation, uint maxSquares)
        {
            List<MapSquare> squares = new List<MapSquare>();
            MapSquare playerSquare = null;
            uint startNumber = 0;
            uint endNumber = Addresses.Map.Max_Squares + 1;

            if (sameFloor)
            {
                playerSquare = GetSquareWithPlayer();

                if (playerSquare != null)
                {
                    int playerFloor = playerSquare.MemoryLocation.Z;
                    startNumber = Map.ConvertMemoryLocationToSquareNumber(
                        new Location(0, 0, playerFloor));
                    endNumber = Map.ConvertMemoryLocationToSquareNumber(
                        new Location(0, 0, playerFloor + 1));
                }
            }

            for (uint i = startNumber; i < endNumber; i++)
            {
                MapSquare mapSquare;
                if (getWorldLocation)
                {
                    if (playerSquare == null)
                        playerSquare = GetSquareWithPlayer();

                    mapSquare = CreateMapSquare(i, playerSquare);
                }
                else
                    mapSquare = CreateMapSquare(i);

                if (match(mapSquare))
                    squares.Add(mapSquare);

                if (squares.Count >= maxSquares) 
                    break;
            }

            return squares;
        }
        #endregion

        #region Conversions
        public static Location ConvertSquareNumberToMemoryLocation(uint squareNumber)
        {
            Location l = new Location();

            l.Z = Convert.ToInt32(squareNumber / (14 * 18));
            l.Y = Convert.ToInt32((squareNumber - l.Z * 14 * 18) / 18);
            l.X = Convert.ToInt32((squareNumber - l.Z * 14 * 18) - l.Y * 18);

            return l;
        }

        /// <summary>
        /// Convert a local location to a square number.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static uint ConvertMemoryLocationToSquareNumber(Location l)
        {
            return Convert.ToUInt32(l.X + l.Y * 18 + l.Z * 14 * 18);
        }

        public Location ConvertMemoryLocationToWorldLocation(Location loc, MapSquare playerSquare)
        {
            Location globalPlayerLoc = client.GetPlayer().Location;
            Location localPlayerLoc = playerSquare.MemoryLocation;
            int xAdjustment = globalPlayerLoc.X - localPlayerLoc.X;
            int yAdjustment = globalPlayerLoc.Y - localPlayerLoc.Y;
            int zAdjustment = globalPlayerLoc.Z - localPlayerLoc.Z;
            return new Location(
                loc.X + xAdjustment,
                loc.Y + yAdjustment,
                loc.Z + zAdjustment);
        }

        public Location ConvertWorldLocationToMemoryLocation(Location loc, MapSquare playerSquare)
        {
            Location globalPlayerLoc = client.GetPlayer().Location;
            Location localPlayerLoc = playerSquare.MemoryLocation;
            int xAdjustment = globalPlayerLoc.X - localPlayerLoc.X;
            int yAdjustment = globalPlayerLoc.Y - localPlayerLoc.Y;
            int zAdjustment = globalPlayerLoc.Z - localPlayerLoc.Z;
            return new Location(
                loc.X - xAdjustment,
                loc.Y - yAdjustment,
                loc.Z - zAdjustment);
        }

        public uint ConvertSquareNumberToMapSquareAddress(uint squareNumber)
        {
            uint mapBegin = client.ReadUInt32(Addresses.Map.MapPointer);
            uint address = mapBegin + (Addresses.Map.Step_Square * squareNumber);
            return address;
        }

        public Location OffsetMemoryLocation(Location loc, int offsetX, int offsetY)
        {
            Location newLoc = new Location();

            newLoc.X = loc.X + offsetX;
            if (newLoc.X < 0) newLoc.X += 18;
            if (newLoc.X > 17) newLoc.X -= 18;

            newLoc.Y = loc.Y + offsetY;
            if (newLoc.Y < 0) newLoc.Y += 14;
            if (newLoc.Y > 13) newLoc.Y -= 14;

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
        public MapSquare CreateMapSquare(Location worldLocation)
        {
            return CreateMapSquare(worldLocation, GetSquareWithPlayer());
        }

        public MapSquare CreateMapSquare(Location worldLocation, MapSquare playerSquare)
        {
            Location memoryLocation = ConvertWorldLocationToMemoryLocation(worldLocation, playerSquare);
            uint squareNumber = ConvertMemoryLocationToSquareNumber(memoryLocation);
            return new MapSquare(
                client, 
                ConvertSquareNumberToMapSquareAddress(squareNumber), 
                squareNumber, 
                worldLocation);
        }

        private MapSquare CreateMapSquare(uint squareNumber)
        {
            return new MapSquare(
                client,
                ConvertSquareNumberToMapSquareAddress(squareNumber),
                squareNumber);
        }

        private MapSquare CreateMapSquare(uint squareNumber, MapSquare playerSquare)
        {
            Location worldLocation = ConvertMemoryLocationToWorldLocation(
                ConvertSquareNumberToMemoryLocation(squareNumber), playerSquare);
            return new MapSquare(
                client,
                ConvertSquareNumberToMapSquareAddress(squareNumber),
                squareNumber,
                worldLocation);
        }
        #endregion

        #region Replace
        /// <summary>
        /// Replace all tiles in a list with a new id.
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="newTileId"></param>
        /// <param name="sameFloor"></param>
        /// <returns></returns>
        public int ReplaceTiles(List<uint> idList, uint newTileId, bool sameFloor)
        {
            return GetSquares(delegate(MapSquare mapSquare)
            {
                if (idList.Contains(mapSquare.Tile.Id))
                {
                    mapSquare.ReplaceTile(newTileId);
                    return true;
                }
                return false;
            }, sameFloor).Count;
        }

        /// <summary>
        /// Replace all tiles matching the old id with the new id.
        /// </summary>
        /// <param name="oldTileId"></param>
        /// <param name="newTileId"></param>
        /// <param name="sameFloor"></param>
        /// <returns></returns>
        public int ReplaceTile(uint oldTileId, uint newTileId, bool sameFloor)
        {
            return ReplaceTiles(new List<uint>() { oldTileId }, newTileId, sameFloor);
        }

        public int ReplaceObjects(List<MapObject> objectList, MapObject newObject, bool sameFloor)
        {
            return GetSquares(delegate(MapSquare mapSquare)
            {
                foreach (MapObject oldObject in mapSquare.Objects)
                {
                    foreach (MapObject testObject in objectList)
                    {
                        if ((testObject.Id == 0 || 
                                oldObject.Id == testObject.Id) &&
                            (testObject.Data == 0 || 
                                oldObject.Data == testObject.Data) &&
                            (testObject.DataEx == 0 || 
                                oldObject.DataEx == testObject.DataEx))
                        {
                            mapSquare.ReplaceObject(oldObject, newObject);
                            return true;
                        }
                    }
                }
                return false;
            }, sameFloor).Count;
        }

        public int ReplaceObject(MapObject testObject, MapObject newObject, bool sameFloor)
        {
            return ReplaceObjects(
                new List<MapObject>() { testObject }, 
                newObject, 
                sameFloor);
        }

        /// <summary>
        /// Replace all the trees on the map with small fir trees.
        /// </summary>
        /// <returns></returns>
        public int ReplaceTrees()
        {
            int[] treearray =
            {
                957,
                3608, 3609, 3613, 3614, 3615, 3616, 3617, 3618, 3619, 3620,
                3621, 3622, 3623, 3624, 3625, 3626, 3631, 3632, 3633, 3634,
                3635, 3636, 3637, 3638, 3639, 3640, 3641, 3647, 3649, 3687,
                3688, 3689, 3691, 3692, 3694, 3742, 3743, 3744, 3745, 3750,
                3751, 3752, 3753, 3754, 3755, 3756, 3757, 3758, 3759, 3760,
                3761, 3762, 3780, 3871, 3872, 3873, 3877, 3878, 3884, 3885,
                3899, 3901, 3902, 3903, 3905, 3908, 3909, 3910, 3911, 3920,
                3921, 5091, 5092, 5093, 5094, 5095, 5155, 5156, 5389, 5390,
                5391, 5392, 6094, 7020, 7021, 7022, 7023, 7024
            };
            List<MapObject> trees = new List<MapObject>(treearray.Length);
            foreach (int id in treearray)
            {
                trees.Add(new MapObject(id, 0, 0));
            }
            MapObject smallFirTree = new MapObject(3682, 0, 0);
            return ReplaceObjects(trees, smallFirTree, true);
        }
        #endregion

        #region Special Purpose
        public List<Tile> GetFishTiles()
        {
            Player player = client.GetPlayer();
            List<Tile> tiles = new List<Tile>();
            List<uint> fishIds = Constants.Tiles.Water.GetFishIds();
            GetSquares(delegate(MapSquare square)
            {
                if (fishIds.Contains(square.Tile.Id))
                    if (square.Tile.Location.Z == player.Location.Z && square.Tile.Location.X - player.Location.X < 7 && square.Tile.Location.Y - player.Location.Y < 6)
                    {
                        tiles.Add(square.Tile);
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
