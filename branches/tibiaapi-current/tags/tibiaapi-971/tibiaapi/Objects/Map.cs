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
        public Tile playerTile;

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
            if (playerTile == null || playerTile.Location != client.PlayerLocation)
            {
                uint playerId = client.Player.Id;
                playerTile = GetTiles(false, false)
                                .Where(t => t.ObjectCount > 1)
                                .FirstOrDefault(t => t.Objects
                                                .Any(o => o.Id == 0x63 &&
                                                    o.Data == playerId &&
                                                    o.StackOrder < o.FromTile.ObjectCount));
                if (playerTile == null)
                    throw new Exception("Player not found.");

                playerTile.Location = client.PlayerLocation;

            }
            return playerTile;
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
            uint endNumber = client.Addresses.Map.MaxTiles + 1;

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
            // All credits goes to blaster_89 who solved this.
            if (playerTile == null) { playerTile = GetTileWithPlayer(); }
             Location memLoc = worldLocation.ToMemoryLocation(playerTile,client);
             uint num = memLoc.ToTileNumber();
     
            int minFloor = 0, maxFloor = 0;
            for (int i = 0; i < 8; i++)
            { 
                if (playerTile.TileNumber   >= client.Addresses.Map.MaxTiles  * i &&
                    playerTile.TileNumber <= client.Addresses.Map.MaxTiles * (i + 1))
                {
             
                    minFloor =  Convert.ToInt32(client.Addresses.Map.MaxTiles * i);
                    maxFloor =  Convert.ToInt32(client.Addresses.Map.MaxTiles * (i + 1) - 1);
                    break;
                }
            }
            if (num > maxFloor) { num = Convert.ToUInt32(num - maxFloor + minFloor - 1); }
            else if (num < minFloor) { num = Convert.ToUInt32(maxFloor - minFloor + num + 1); }
            return GetTile(num, playerTile);
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
            
            Item smallFirTree = new Item(client, 3682);
            ushort smallFirTreeSpriteId = smallFirTree.SpriteIds[0];
            ushort[] newIds;
            foreach (int id in Tibia.Constants.Items.TreeArray)
            {
                Item tree = new Item(client,(uint) id);
                newIds = new ushort[tree.SpriteCount];
                newIds[0] = smallFirTreeSpriteId;
                tree.SpriteIds = newIds;
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
            client.Memory.WriteBytes(client.Addresses.Map.NameSpy1, client.Addresses.Map.Nops, 2);
            client.Memory.WriteBytes(client.Addresses.Map.NameSpy2, client.Addresses.Map.Nops, 2);
        }
        /// <summary>
        /// Disable name spying.
        /// </summary>
        /// <param name="enable"></param>
        public void NameSpyOff()
        {
            client.Memory.WriteBytes(client.Addresses.Map.NameSpy1, BitConverter.GetBytes(client.Addresses.Map.NameSpy1Default), 2);
            client.Memory.WriteBytes(client.Addresses.Map.NameSpy2, BitConverter.GetBytes(client.Addresses.Map.NameSpy2Default), 2);
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

            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy1, client.Addresses.Map.Nops, 6);
            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy2, client.Addresses.Map.Nops, 6);
            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy3, client.Addresses.Map.Nops, 6);

            tempPtr = client.Memory.ReadInt32(client.Addresses.Map.LevelSpyPtr);
            tempPtr += client.Addresses.Map.LevelSpyAdd1;
            tempPtr = client.Memory.ReadInt32(tempPtr);
            tempPtr += (int)client.Addresses.Map.LevelSpyAdd2;

            playerZ = (int)client.Player.Z;

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
            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy1, client.Addresses.Map.LevelSpyDefault, 6);
            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy2, client.Addresses.Map.LevelSpyDefault, 6);
            client.Memory.WriteBytes(client.Addresses.Map.LevelSpy3, client.Addresses.Map.LevelSpyDefault, 6);
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
            client.Memory.WriteBytes(client.Addresses.Map.FullLightNop, client.Addresses.Map.FullLightNopEdited, 2);
            client.Memory.WriteByte(client.Addresses.Map.FullLightAdr, client.Addresses.Map.FullLightAdrEdited);
        }

        /// <summary>
        /// Disable full light.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public void FullLightOff()
        {
            client.Memory.WriteBytes(client.Addresses.Map.FullLightNop, client.Addresses.Map.FullLightNopDefault, 2);
            client.Memory.WriteByte(client.Addresses.Map.FullLightAdr, client.Addresses.Map.FullLightAdrDefault);
        }
        #endregion
    }
}
