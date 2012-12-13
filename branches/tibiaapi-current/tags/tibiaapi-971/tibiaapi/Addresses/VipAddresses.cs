namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public VipAddresses Vip = new VipAddresses();

        public class VipAddresses
        {
            /// <summary>
            /// Points to the list's marker node.
            /// This node points to the first element and is pointed to by the last element in the VipList.
            /// </summary>
            public uint MarkerNodePtr;

            /// <summary>
            /// Total number of players in the VipList.
            /// </summary>
            public uint Count;

            /// <summary>
            /// Distances for Vips.
            /// </summary>
            public uint DistancePreviousNode;
            public uint DistanceNextNode;
            public uint DistanceId;
            public uint DistanceIcon;
            public uint DistanceNotify;
            public uint DistanceNameField;
            public uint DistanceDescriptionField;
            public uint DistanceStatus; // 0 = offline, 1 = online
        }
    }
}