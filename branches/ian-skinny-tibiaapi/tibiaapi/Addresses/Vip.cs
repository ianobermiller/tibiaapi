namespace Tibia.Addresses
{
    public static class Vip
    {
        /// <summary>
        /// Step between Players.
        /// </summary>
        public static uint Step_Players = 0x2C;

        /// <summary>
        /// Max names in VipList.
        /// </summary>
        public static uint Max_Players = 100;

        /// <summary>
        /// Start of the VipList.
        /// </summary>
        public static uint Start = 0x629A50; // 8.40

        /// <summary>
        /// End of the VipList.
        /// </summary>
        public static uint End = Start + (Step_Players * Max_Players); // 8.40

        /// <summary>
        /// Distances for Vips.
        /// </summary>
        public static uint Distance_Id = 0;
        public static uint Distance_Name = 4;
        public static uint Distance_Status = 34; // 0 = offline, 1 = online
        public static uint Distance_Icon = 40;
    }
}
