namespace Tibia.Addresses
{
    public static class Vip
    {
        /// <summary>
        /// Step between Players.
        /// </summary>
        public static uint StepPlayers;

        /// <summary>
        /// Max names in VipList.
        /// </summary>
        public static uint MaxPlayers;

        /// <summary>
        /// Start of the VipList.
        /// </summary>
        public static uint Start;

        /// <summary>
        /// End of the VipList.
        /// </summary>
        public static uint End;

        /// <summary>
        /// Distances for Vips.
        /// </summary>
        public static uint DistanceId;
        public static uint DistanceName;
        public static uint DistanceStatus; // 0 = offline, 1 = online
        public static uint DistanceIcon;
    }
}
