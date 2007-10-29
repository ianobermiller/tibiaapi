namespace Tibia.Addresses
{
    public static class VipList
    {
        /// <summary>
        /// Start of the VipList
        /// </summary>
        public static uint Start = 0x60C7F0;
        
        /// <summary>
        /// End of VipList
        /// </summary>
        public static uint End = 0x60C840;
        
        /// <summary>
        /// Step between Players
        /// </summary>
        public static uint Step_Players = 0x2C;

        /// <summary>
        /// Max names in VipList
        /// </summary>
        public static uint Max_Players = 100;
    }
}
