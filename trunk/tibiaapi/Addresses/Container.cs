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
        public static uint Start = 0x642C40; //8.54

        /// <summary>
        /// Distance between the containers.
        /// </summary>
        public static uint StepContainer = 492;
        /// <summary>
        /// Distance between the slots.
        /// </summary>
        public static uint StepSlot = 12;

        /// <summary>
        /// Maximum amount of containers that can be opened.
        /// </summary>
        public static uint MaxContainers = 16;

        /// <summary>
        /// The maximum amount of items in one stack.
        /// </summary>
        public static uint MaxStack = 100;

        /// <summary>
        /// The distance from the start of the container to the IsOpen variable.
        /// </summary>
        public static uint DistanceIsOpen = 0;

        /// <summary>
        /// The distance from the start of the container to the Id variable.
        /// </summary>
        public static uint DistanceId = 4;

        /// <summary>
        /// The distance from the start of the container to the Name variable.
        /// </summary>
        public static uint DistanceName = 16;

        /// <summary>
        /// The distance from the start of the container to the Volume variable.
        /// Volume is the maximum items the container can hold.
        /// </summary>
        public static uint DistanceVolume = 48;

        public static uint DistanceHasParent = 52; // maybe.. only for test.. 

        /// <summary>
        /// The distance from the start of the contianer to the Amount variable.
        /// Amount is the current amount of items in the container.
        /// </summary>
        public static uint DistanceAmount = 56;

        /// <summary>
        /// The distance from the start of the item to the Id variable.
        /// </summary>
        public static uint DistanceItemId = 60;
        /// <summary>
        /// The distance from the start of the item to the ItemCount variable.
        /// </summary>
        public static uint DistanceItemCount = 64;

        /// <summary>
        /// End of the container list in memory.
        /// </summary>
        public static uint End = Start + (MaxContainers * StepContainer);
    }
}
