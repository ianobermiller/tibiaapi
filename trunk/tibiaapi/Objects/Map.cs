using System;
using System.Collections.Generic;
using System.Linq;

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

        #region Get Tiles
        public Tile GetTileWithPlayer()
        {
            int playerId = client.Memory.ReadInt32(Addresses.Player.Id);
            return GetTiles(false, false).First(
                t => t.Objects.Any(
                    o => o.Id == 0x63 && o.Data == playerId));
        }

        public IEnumerable<Tile> GetTiles()
        {
            return GetTiles(false, true);
        }

        public IEnumerable<Tile> GetTilesOnSameFloor()
        {
            return GetTiles(true, true);
        }

        private IEnumerable<Tile> GetTiles(bool sameFloor, bool getWorldLocation)
        {
            Tile playerTile = null;
            uint startNumber = 0;
            uint endNumber = Addresses.Map.MaxSquares + 1;

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
                if (getWorldLocation)
                    yield return GetTile(i, playerTile);
                else
                    yield return GetTile(i);
            }

        }
        #endregion

        #region Conversions
        private Location OffsetMemoryLocation(Location loc, int offsetX, int offsetY)
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

        #region Get Tile
        /// <summary>
        /// Get the map square at the specified world location.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Tile GetTile(Location worldLocation)
        {
            return GetTile(worldLocation, GetTileWithPlayer());
        }

        private Tile GetTile(Location worldLocation, Tile playerTile)
        {
            Location memoryLocation = worldLocation.ToMemoryLocation(playerTile, client);
            uint tileNumber = memoryLocation.ToTileNumber();
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber, worldLocation);
        }

        private Tile GetTile(uint tileNumber)
        {
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber);
        }

        private Tile GetTile(uint tileNumber, Tile playerTile)
        {
            Location worldLocation = tileNumber.ToMemoryLocation().ToWorldLocation(playerTile, client);
            return new Tile(client, tileNumber.ToMapTileAddress(client), tileNumber, worldLocation);
        }
        #endregion

        #region Replace
        /// <summary>
        /// Replace all the trees on the map with small fir trees.
        /// Only needs to be called ONCE.
        /// </summary>
        /// <returns></returns>
        public void ReplaceTrees()
        {
            uint smallFirTreeId = 3682;
            byte[] smallFirTreeBytes = client.Memory.ReadBytes(
                client.Memory.ReadUInt32(
                    client.Memory.ReadUInt32(Addresses.Client.DatPointer) + 8)
                + Addresses.DatItem.Sprite
                + (uint)(0x4C * (smallFirTreeId - 100)), 3);
            foreach (int id in Tibia.Constants.Items.TreeArray)
            {
                uint address = client.Memory.ReadUInt32(client.Memory.ReadUInt32(Addresses.Client.DatPointer) + 8) 
                    + Addresses.DatItem.Sprite 
                    + (uint)(0x4C * (id - 100));
                client.Memory.WriteBytes(address, smallFirTreeBytes, 3); 
            }
        }
        #endregion

        #region Name Spy
        /// <summary>
        /// Enable name spying.
        /// </summary>
        /// <param name="enable"></param>
        public void NameSpyOn()
        {
            client.Memory.WriteBytes(Addresses.Map.NameSpy1, Addresses.Map.Nops, 2);
            client.Memory.WriteBytes(Addresses.Map.NameSpy2, Addresses.Map.Nops, 2);
        }
        /// <summary>
        /// Disable name spying.
        /// </summary>
        /// <param name="enable"></param>
        public void NameSpyOff()
        {
            client.Memory.WriteBytes(Addresses.Map.NameSpy1, BitConverter.GetBytes(Addresses.Map.NameSpy1Default), 2);
            client.Memory.WriteBytes(Addresses.Map.NameSpy2, BitConverter.GetBytes(Addresses.Map.NameSpy2Default), 2);
        }
        #endregion

        #region Level Spy
        /// <summary>
        /// Enable level spy for the given floor. The floor parameter
        /// is relative to the floor the player is currently on.
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        public bool LevelSpyOn(int floor)
        {
            int playerZ, tempPtr;

            client.Memory.WriteBytes(Addresses.Map.LevelSpy1, Addresses.Map.Nops, 6);
            client.Memory.WriteBytes(Addresses.Map.LevelSpy2, Addresses.Map.Nops, 6);
            client.Memory.WriteBytes(Addresses.Map.LevelSpy3, Addresses.Map.Nops, 6);

            tempPtr = client.Memory.ReadInt32(Addresses.Map.LevelSpyPtr);
            tempPtr += Addresses.Map.LevelSpyAdd1;
            tempPtr = client.Memory.ReadInt32(tempPtr);
            tempPtr += (int)Addresses.Map.LevelSpyAdd2;

            playerZ = client.Memory.ReadInt32(Addresses.Player.Z);

            if (playerZ <= 7)
            {
                if (playerZ - floor >= 0 && playerZ - floor <= 7)
                {
                    playerZ = 7 - playerZ;
                    client.Memory.WriteInt32(tempPtr, playerZ + floor);
                    return true;
                }
            }
            else
            {
                if (floor >= -2 && floor <= 2 && playerZ - floor < 16)
                {
                    client.Memory.WriteInt32(tempPtr, 2 + floor);
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Disable level spy.
        /// </summary>
        public void LevelSpyOff()
        {
            client.Memory.WriteBytes(Addresses.Map.LevelSpy1, Addresses.Map.LevelSpyDefault, 6);
            client.Memory.WriteBytes(Addresses.Map.LevelSpy2, Addresses.Map.LevelSpyDefault, 6);
            client.Memory.WriteBytes(Addresses.Map.LevelSpy3, Addresses.Map.LevelSpyDefault, 6);
        }
        #endregion

        #region Full Light
        /// <summary>
        /// Enable full light.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public void FullLightOn()
        {
            client.Memory.WriteBytes(Addresses.Map.FullLightNop, Addresses.Map.FullLightNopEdited, 2);
            client.Memory.WriteByte(Addresses.Map.FullLightAdr, Addresses.Map.FullLightAdrEdited);
        }

        /// <summary>
        /// Disable full light.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public void FullLightOff()
        {
            client.Memory.WriteBytes(Addresses.Map.FullLightNop, Addresses.Map.FullLightNopDefault, 2);
            client.Memory.WriteByte(Addresses.Map.FullLightAdr, Addresses.Map.FullLightAdrDefault);
        }
        #endregion
    }
}
