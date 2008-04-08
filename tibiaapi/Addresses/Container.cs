namespace Tibia.Addresses
{
    /// <summary>
    /// Player container addresses and distances.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Start of the container list in memory.
        /// </summary>
        public static uint Start = 0x0061C0D0; //8.1, 8.0 = 0x617000
        /// <summary>
        /// End of the container list in memory.
        /// </summary>
        public static uint End = 0x0061DF90; //8.1, 8.0 = 0x618EC0

        /// <summary>
        /// Distance between the containers.
        /// </summary>
        public static uint Step_Container = 492;                    //8.0
        /// <summary>
        /// Distance between the slots.
        /// </summary>
        public static uint Step_Slot = 12;                   //8.0

        /// <summary>
        /// Maximum amount of containers that can be opened.
        /// </summary>
        public static uint Max_Containers = 16;
        /// <summary>
        /// The maximum amount of items in one stack.
        /// </summary>
        public static uint Max_Stack = 100;

        /// <summary>
        /// The distance from the start of the container to the IsOpen variable.
        /// </summary>
        public static uint Distance_IsOpen = 0;   //8.0
        /// <summary>
        /// The distance from the start of the container to the Id variable.
        /// </summary>
        public static uint Distance_Id = 4;       //8.0
        /// <summary>
        /// The distance from the start of the container to the Name variable.
        /// </summary>
        public static uint Distance_Name = 16;    //8.0
        /// <summary>
        /// The distance from the start of the container to the Volume variable.
        /// Volume is the maximum items the container can hold.
        /// </summary>
        public static uint Distance_Volume = 48;  //8.0
        /// <summary>
        /// The distance from the start of the contianer to the Amount variable.
        /// Amount is the current amount of items in the container.
        /// </summary>
        public static uint Distance_Amount = 56;  //8.0

        /// <summary>
        /// The distance from the start of the item to the Id variable.
        /// </summary>
        public static uint Distance_Item_Id = 60;                  //8.0
        /// <summary>
        /// The distance from the start of the item to the ItemCount variable.
        /// </summary>
        public static uint Distance_Item_Count = 64;               //8.0
    }
}
