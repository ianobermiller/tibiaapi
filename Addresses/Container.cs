namespace Addresses
{
    /// <summary>
    /// Player container addresses and distances.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Start and end of the container list in memory.
        /// </summary>
        public static uint Start = 0x617000;
        public static uint End = 0x618EC0;

        /// <summary>
        /// Distances between containers and slots.
        /// </summary>
        public static uint Step_Container = 492;                    //8.0
        public static uint Step_Slot = 12;                   //8.0

        /// <summary>
        /// Maximums.
        /// </summary>
        public static uint Max_Containers = 16;
        public static uint Max_Stack = 100;

        /// <summary>
        /// Container properties.
        /// </summary>
        public static uint Distance_IsOpen = 0;   //8.0
        public static uint Distance_Id = 4;       //8.0
        public static uint Distance_Name = 16;    //8.0
        public static uint Distance_Volume = 48;  //8.0
        public static uint Distance_Amount = 56;  //8.0

        /// <summary>
        /// Item properties.
        /// </summary>
        public static uint Distance_Item_Id = 60;                  //8.0
        public static uint Distance_Item_Count = 64;               //8.0
    }
}
