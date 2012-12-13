namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public MapAddresses Map = new MapAddresses();

        /// <summary>
        /// Map memory addresses.
        /// </summary>
        public class MapAddresses
        {
            /// <summary>
            /// Pointer to the start of the map memory addresses.
            /// </summary>
            public uint MapPointer;

            /// <summary>
            /// Step between tiles on the map.
            /// </summary>
            public uint StepTile;

            /// <summary>
            /// Step between objects on a tile.
            /// The first object is the gound, subsequent objects are any nonmoveable items (trees),
            /// creatures (players), or items in that tile.
            /// </summary>
            public uint StepTileObject;

            /// <summary>
            /// Distance from the tile to the number of objects on the square.
            /// </summary>
            public uint DistanceTileItemsCount;

            /// <summary>
            /// Indicates the position of the respective item on the stack.
            /// </summary>
            public uint DistanceTileItemOrder;

            /// <summary>
            /// Distance to the first object on a tile.
            /// </summary>
            public uint DistanceTileItems;

            /// <summary>
            /// Effect shown at the tile.
            /// </summary>
            public uint DistanceTileEffect;

            /// <summary>
            /// Distance to the id of the object that is on a tile.
            /// </summary>
            public uint DistanceItemId;
            /// <summary>
            /// Distance to the data of the object that is on a tile.
            /// </summary>
            public uint DistanceItemData;
            /// <summary>
            /// Distance to the ExData (extra data) of the object that is on a tile.
            /// </summary>
            public uint DistanceItemDataEx;

            /// <summary>
            /// Maximum number of objects per tile including ground.
            /// </summary>
            public uint MaxTileItems;

            /// <summary>
            /// Maximum number of tiles in the X direction
            /// </summary>
            public uint MaxX;

            /// <summary>
            /// Maximum number of tiles in the Y direction
            /// </summary>
            public uint MaxY;

            /// <summary>
            /// Maximum number of tiles in the Z direction
            /// </summary>
            public uint MaxZ;

            /// <summary>
            /// Maximum number of tiles.
            /// </summary>
            public uint MaxTiles;

            /// <summary>
            /// The default (starting) Z axis value
            /// </summary>
            public uint ZAxisDefault;

            /// <summary>
            /// Memory address for player tile
            /// </summary>
            public uint PlayerTile;

            /// <summary>
            /// Nop Value, to use with namespy and levelspy
            /// </summary>
            public byte[] Nops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

            /// <summary>
            /// NameSpy address 1.
            /// </summary>
            public uint NameSpy1;


            /// <summary>
            /// NameSpy address 2.
            /// </summary>
            public uint NameSpy2;


            /// <summary>
            /// Default value for Namespy1.
            /// </summary>
            public uint NameSpy1Default;
            /// <summary>
            /// Default value for Namespy2.
            /// </summary>
            public uint NameSpy2Default;

            /// <summary>
            /// Level spy address 1.
            /// </summary>
            public uint LevelSpy1;


            /// <summary>
            /// Level spy address 2.
            /// </summary>
            public uint LevelSpy2;


            /// <summary>
            /// Level spy address 3.
            /// </summary>
            public uint LevelSpy3;


            /// <summary>
            /// Level spy pointer.
            /// </summary>
            public uint LevelSpyPtr;

            /// <summary>
            /// Defaults for level spy.
            /// </summary>
            public byte[] LevelSpyDefault;
            /// <summary>
            /// Level spy add 1.
            /// </summary>
            public byte LevelSpyAdd1;
            /// <summary>
            /// Level spy add 2.
            /// </summary>
            public uint LevelSpyAdd2;

            /// <summary>
            /// Write to this byte to reveal invisible creatures.
            /// Thanks to Stiju @ http://www.tpforums.org/forum/showthread.php?t=1141
            /// </summary>
            [System.Obsolete]
            public uint RevealInvisible1 = 0x45F7A3;  // 8.52
            [System.Obsolete]
            public byte RevealInvisible1Default = 0x72;
            [System.Obsolete]
            public byte RevealInvisible1Edited = 0xEB;

            [System.Obsolete]
            public uint RevealInvisible2 = 0x4EC595;  // 8.52
            [System.Obsolete]
            public byte RevealInvisible2Default = 0x75;
            [System.Obsolete]
            public byte RevealInvisible2Edited = 0xEB;

            /// <summary>
            /// Global light, all floors, used for improving levelspy
            /// </summary>
            public uint FullLightNop;
            public byte[] FullLightNopDefault;
            public byte[] FullLightNopEdited;

            public uint FullLightAdr;
            public byte FullLightAdrDefault;
            public byte FullLightAdrEdited;
        }
    }
}