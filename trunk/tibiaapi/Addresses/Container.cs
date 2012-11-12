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
        public static uint Start;

        /// <summary>
        /// Distance between the containers.
        /// </summary>
        public static uint StepContainer;
        /// <summary>
        /// Distance between the slots.
        /// </summary>
        public static uint StepSlot;

        /// <summary>
        /// Maximum amount of containers that can be opened.
        /// </summary>
        public static uint MaxContainers;

        /// <summary>
        /// The maximum amount of items in one stack.
        /// </summary>
        public static uint MaxStack;

        /// <summary>
        /// The distance from the start of the container to the IsOpen variable.
        /// </summary>
        public static uint DistanceIsOpen;

        /// <summary>
        /// The distance from the start of the container to the Id variable.
        /// </summary>
        public static uint DistanceId;

        /// <summary>
        /// The distance from the start of the container to the Name variable.
        /// </summary>
        public static uint DistanceName;

        /// <summary>
        /// The distance from the start of the container to the Volume variable.
        /// Volume is the maximum items the container can hold.
        /// </summary>
        public static uint DistanceVolume;

        /// <summary>
        /// The distance from the start of the container to the HasParent variable.
        /// Indicates if a container is not the root of a container tree.
        /// </summary>
        public static uint DistanceHasParent;

        /// <summary>
        /// The distance from the start of the contianer to the Amount variable.
        /// Amount is the current amount of items in the container.
        /// </summary>
        public static uint DistanceAmount;

        /// <summary>
        /// The distance from the start of the item to the Id variable.
        /// </summary>
        public static uint DistanceItemId;
        /// <summary>
        /// The distance from the start of the item to the ItemCount variable.
        /// </summary>
        public static uint DistanceItemCount;

        /// <summary>
        /// End of the container list in memory.
        /// </summary>
        public static uint End;
    }
}
