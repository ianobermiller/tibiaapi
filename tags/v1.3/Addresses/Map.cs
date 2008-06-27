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
        public static uint MapPointer = 0x6234D8;     //8.1, 8.0 = 0x61E408

        /// <summary>
        /// Step between squares on the map.
        /// </summary>
        public static uint Step_Square = 172;          //8.0

        /// <summary>
        /// Step between objects on a square.
        /// The first object is the tile, subsequent objects are any nonmoveable items (trees),
        /// creatures (players), or items in that tile.
        /// </summary>
        public static uint Step_Square_Object = 12;

        /// <summary>
        /// Distance from the square to the number of objects on the square.
        /// </summary>
        public static uint Distance_Square_ObjectCount = 0;

        /// <summary>
        /// Distance to the first object on a square.
        /// </summary>
        public static uint Distance_Square_Objects = 4;

        public static uint Distance_Object_Id = 0;        //8.0
        public static uint Distance_Object_Data = 4;      //8.0
        public static uint Distance_Object_Data_Ex = 8;   //8.0

        /// <summary>
        /// Maximum number of tiles.
        /// </summary>
        public static uint Max_Squares = 2016;

        /// <summary>
        /// Maximum number of objects per tile.
        /// </summary>
        public static uint Max_Square_Objects = 13;

        /// <summary>
        /// The default (starting) Z axis value
        /// </summary>
        public static uint Z_Axis_Default = 7; // default ground level
        
        /// <summary>
        /// Class of addresses and distances for level spy.
        /// </summary>
        public class LevelSpy
        {
            public static uint NOP = 0x004C4FC0;        //8.1, 8.0 = 0x004C4320
            public static uint Above = 0x004C4FBC;      //8.1, 8.0 = 0x004C431C
            public static uint Below = 0x004C4FC4;      //8.1, 8.0 = 0x004C4324

            public static uint NOP_Default = 49451;     //8.0
            public static uint Above_Default = 7;       //8.0
            public static uint Below_Default = 2;       //8.0

            public static uint MIN = 0; //8.0
            public static uint MAX = 7; //8.0

        }

        /// <summary>
        /// Class of addresses and distances for name spy.
        /// </summary>
        public class NameSpy
        {
            public static uint NOP = 0x004DF469;     //8.1, 8.0 = 0x004DD2D7
            public static uint NOP2 = 0x004DF473;    //8.1, 8.0 = 0x004DD2E1

            public static uint NOP_Default = 19061;  //8.1, 8.0 = 19573
            public static uint NOP2_Default = 16501; //8.1, 8.0 = 17013
        }
    }
}
