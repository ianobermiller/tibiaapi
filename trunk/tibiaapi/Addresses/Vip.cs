namespace Tibia.Addresses
{    
    public static class Vip
    {


        /// <summary>
        /// Points to the list's marker node.
        /// This node points to the first element and is pointed to by the last element in the VipList.
        /// </summary>
        public static uint MarkerNodePtr;

        /// <summary>
        /// Total number of players in the VipList.
        /// </summary>
        public static uint Count;

        /// <summary>
        /// Distances for Vips.
        /// </summary>
        public static uint DistancePreviousNode;
        public static uint DistanceNextNode;        
        public static uint DistanceId;
        public static uint DistanceIcon;
        public static uint DistanceNotify;
        public static uint DistanceNameField;
        public static uint DistanceDescriptionField;
        public static uint DistanceStatus; // 0 = offline, 1 = online
        
        /// <summary>
        /// Distances for TextFields.
        /// </summary>
        public static uint DistanceText;
        public static uint DistanceTextLength;
        public static uint DistanceFieldType;        
        

    }
}
