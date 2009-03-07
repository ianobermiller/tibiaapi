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
        public static uint MapPointer = 0x63F568; // 8.40

        /// <summary>
        /// Step between tiles on the map.
        /// </summary>
        public static uint StepTile = 172;

        /// <summary>
        /// Step between objects on a tile.
        /// The first object is the gound, subsequent objects are any nonmoveable items (trees),
        /// creatures (players), or items in that tile.
        /// </summary>
        public static uint StepTileObject = 12;

        /// <summary>
        /// Distance from the tile to the number of objects on the square.
        /// </summary>
        public static uint DistanceTileObjectCount = 0;

        /// <summary>
        /// Distance to the first object on a tile.
        /// </summary>
        public static uint DistanceTileObjects = 4;

        /// <summary>
        /// Distance to the id of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectId = 0;
        /// <summary>
        /// Distance to the data of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectData = 4;
        /// <summary>
        /// Distance to the ExData (extra data) of the object that is on a tile.
        /// </summary>
        public static uint DistanceObjectDataEx = 8;

        /// <summary>
        /// Maximum number of objects per tile.
        /// </summary>
        public static uint MaxSquareObjects = 13;

        /// <summary>
        /// Maximum number of tiles in the X direction
        /// </summary>
        public static uint MaxX = 18;

        /// <summary>
        /// Maximum number of tiles in the Y direction
        /// </summary>
        public static uint MaxY = 14;

        /// <summary>
        /// Maximum number of tiles in the Z direction
        /// </summary>
        public static uint MaxZ = 8;

        /// <summary>
        /// Maximum number of tiles.
        /// </summary>
        public static uint MaxSquares = 2016; // MaxX * MaxY * MaxZ

        /// <summary>
        /// The default (starting) Z axis value
        /// </summary>
        public static uint ZAxisDefault = 7; // default ground level

        /// <summary>
        /// Memory address for player tile
        /// </summary>
        public static uint PlayerTile = 0x3E3A08; // 8.1, Doesn't appear to exist in 8.21

        /// <summary>
        /// Nop Value, to use with namespy and levelspy
        /// </summary>
        public static byte[] Nops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        /// <summary>
        /// NameSpy address 1.
        /// </summary>
        public static uint NameSpy1 = 0x4EC179; // 8.40


        /// <summary>
        /// NameSpy address 2.
        /// </summary>
        public static uint NameSpy2 = 0x4EC183;  // 8.40


        /// <summary>
        /// Default value for Namespy1.
        /// </summary>
        public static uint NameSpy1Default = 19061;
        /// <summary>
        /// Default value for Namespy2.
        /// </summary>
        public static uint NameSpy2Default = 16501;

        /// <summary>
        /// Level spy address 1.
        /// </summary>
        public static uint LevelSpy1 = 0x4EE02A;  // 8.40


        /// <summary>
        /// Level spy address 2.
        /// </summary>
        public static uint LevelSpy2 = 0x4EE12F;  // 8.40


        /// <summary>
        /// Level spy address 3.
        /// </summary>
        public static uint LevelSpy3 = 0x4EE1B0;  // 8.40


        /// <summary>
        /// Level spy pointer.
        /// </summary>
        public static uint LevelSpyPtr = 0x6376A8;  // 8.40

        /// <summary>
        /// Defaults for level spy.
        /// </summary>
        public static byte[] LevelSpyDefault = { 0x89, 0x86, 0x88, 0x2A, 0x00, 0x00 };
        /// <summary>
        /// Level spy add 1.
        /// </summary>
        public static byte LevelSpyAdd1 = 28;
        /// <summary>
        /// Level spy add 2.
        /// </summary>
        public static uint LevelSpyAdd2 = 0x2A88;

        /// <summary>
        /// Write to this byte to reveal invisible creatures.
        /// Thanks to Stiju @ http://www.tpforums.org/forum/showthread.php?t=1141
        /// </summary>
        public static uint RevealInvisible1 = 0x45E2F3;  // 8.40
        public static byte RevealInvisible1Default = 0x72;
        public static byte RevealInvisible1Edited = 0xEB;

        public static uint RevealInvisible2 = 0x4EB445;  // 8.40
        public static byte RevealInvisible2Default = 0x75;
        public static byte RevealInvisible2Edited = 0xEB;

        /// <summary>
        /// Global light, all floors, used for improving levelspy
        /// </summary>
        public static uint FullLightNop = 0x4E4929;  // 8.40
        public static byte[] FullLightNopDefault = { 0x7E, 0x05 };
        public static byte[] FullLightNopEdited = { 0x90, 0x90 };

        public static uint FullLightAdr = 0x4E492C;  // 8.40
        public static byte FullLightAdrDefault = 0x80;
        public static byte FullLightAdrEdited = 0xFF;
    }
}
