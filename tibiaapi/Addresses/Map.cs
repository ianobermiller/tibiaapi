namespace Tibia.Addresses
{
    /// <summary>
    /// Map memory addresses.
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Pointer to the start of the map memory addresses.
        /// </summary>
        public static uint MapPointer;

        /// <summary>
        /// Step between tiles on the map.
        /// </summary>
        public static uint StepTile;

        /// <summary>
        /// Step between objects on a tile.
        /// The first object is the gound, subsequent objects are any nonmoveable items (trees),
        /// creatures (players), or items in that tile.
        /// </summary>
        public static uint StepTileObject;

        /// <summary>
        /// Distance from the tile to the number of objects on the square.
        /// </summary>
        public static uint DistanceTileObjectCount;

        /// <summary>
        /// Distance to the first object on a tile.
        /// </summary>
        public static uint DistanceTileObjects;

        /// <summary>
        /// Distance to the id of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectId;
        /// <summary>
        /// Distance to the data of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectData;
        /// <summary>
        /// Distance to the ExData (extra data) of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectDataEx;

        /// <summary>
        /// Maximum number of objects per tile.
        /// </summary>
        public static uint MaxTileObjects;

        /// <summary>
        /// Maximum number of tiles in the X direction
        /// </summary>
        public static uint MaxX;

        /// <summary>
        /// Maximum number of tiles in the Y direction
        /// </summary>
        public static uint MaxY;

        /// <summary>
        /// Maximum number of tiles in the Z direction
        /// </summary>
        public static uint MaxZ;

        /// <summary>
        /// Maximum number of tiles.
        /// </summary>
        public static uint MaxTiles;

        /// <summary>
        /// The default (starting) Z axis value
        /// </summary>
        public static uint ZAxisDefault;

        /// <summary>
        /// Memory address for player tile
        /// </summary>
        public static uint PlayerTile;

        /// <summary>
        /// Nop Value, to use with namespy and levelspy
        /// </summary>
        public static byte[] Nops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        /// <summary>
        /// NameSpy address 1.
        /// </summary>
        public static uint NameSpy1;


        /// <summary>
        /// NameSpy address 2.
        /// </summary>
        public static uint NameSpy2;


        /// <summary>
        /// Default value for Namespy1.
        /// </summary>
        public static uint NameSpy1Default;
        /// <summary>
        /// Default value for Namespy2.
        /// </summary>
        public static uint NameSpy2Default;

        /// <summary>
        /// Level spy address 1.
        /// </summary>
        public static uint LevelSpy1;


        /// <summary>
        /// Level spy address 2.
        /// </summary>
        public static uint LevelSpy2;


        /// <summary>
        /// Level spy address 3.
        /// </summary>
        public static uint LevelSpy3;


        /// <summary>
        /// Level spy pointer.
        /// </summary>
        public static uint LevelSpyPtr;

        /// <summary>
        /// Defaults for level spy.
        /// </summary>
        public static byte[] LevelSpyDefault = { 0x89, 0x86, 0xC0, 0x5B, 0x00, 0x00 };
        /// <summary>
        /// Level spy add 1.
        /// </summary>
        public static byte LevelSpyAdd1;
        /// <summary>
        /// Level spy add 2.
        /// </summary>
        public static uint LevelSpyAdd2;

        /// <summary>
        /// Write to this byte to reveal invisible creatures.
        /// Thanks to Stiju @ http://www.tpforums.org/forum/showthread.php?t=1141
        /// </summary>
        public static uint RevealInvisible1 = 0x45F7A3;  // 8.52
        public static byte RevealInvisible1Default = 0x72;
        public static byte RevealInvisible1Edited = 0xEB;

        public static uint RevealInvisible2 = 0x4EC595;  // 8.52
        public static byte RevealInvisible2Default = 0x75;
        public static byte RevealInvisible2Edited = 0xEB;

        /// <summary>
        /// Global light, all floors, used for improving levelspy
        /// </summary>
        public static uint FullLightNop;
        public static byte[] FullLightNopDefault;
        public static byte[] FullLightNopEdited;

        public static uint FullLightAdr;
        public static byte FullLightAdrDefault;
        public static byte FullLightAdrEdited;
    }
}
