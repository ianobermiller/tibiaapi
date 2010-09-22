namespace Tibia.Addresses
{
    public static class Vip
    {
        /// <summary>
        /// Step between Players.
        /// </summary>
        public static uint StepPlayers = 0x2C;

        /// <summary>
        /// Max names in VipList.
        /// </summary>
        public static uint MaxPlayers = 200;

        /// <summary>
        /// Start of the VipList.
        /// </summary>
        public static uint Start = 0x635978; // 8.62

        /// <summary>
        /// End of the VipList.
        /// </summary>
        public static uint End = Start + (StepPlayers * MaxPlayers);

        /// <summary>
        /// Distances for Vips.
        /// </summary>
        public static uint DistanceId = 0;
        public static uint DistanceName = 4;
        public static uint DistanceStatus = 34; // 0 = offline, 1 = online
        public static uint DistanceIcon = 40;
    }
}
