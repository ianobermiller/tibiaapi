namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public ContainerAddresses Container = new ContainerAddresses();

        /// <summary>
        /// Player container addresses and distances.
        /// </summary>
        public class ContainerAddresses
        {
            /// <summary>
            /// Start of the container list in memory.
            /// </summary>
            public uint Start;

            /// <summary>
            /// Distance between the containers.
            /// </summary>
            public uint StepContainer;
            /// <summary>
            /// Distance between the slots.
            /// </summary>
            public uint StepSlot;

            /// <summary>
            /// Maximum amount of slots a container may contain.
            /// </summary>
            public uint MaxSlots;

            /// <summary>
            /// Maximum amount of containers that can be opened.
            /// </summary>
            public uint MaxContainers;

            /// <summary>
            /// The maximum amount of items in one stack.
            /// </summary>
            public uint MaxStack;

            /// <summary>
            /// The distance from the start of the container to the IsOpen variable.
            /// </summary>
            public uint DistanceIsOpen;

            /// <summary>
            /// The distance from the start of the container to the Id variable.
            /// </summary>
            public uint DistanceId;

            /// <summary>
            /// The distance from the start of the container to the Name variable.
            /// </summary>
            public uint DistanceName;

            /// <summary>
            /// The distance from the start of the container to the Volume variable.
            /// Volume is the maximum items the container can hold.
            /// </summary>
            public uint DistanceVolume;

            /// <summary>
            /// The distance from the start of the container to the HasParent variable.
            /// Indicates if a container is not the root of a container tree.
            /// </summary>
            public uint DistanceHasParent;

            /// <summary>
            /// The distance from the start of the container to the Amount variable.
            /// Amount is the current amount of items in the container.
            /// </summary>
            public uint DistanceAmount;

            /// <summary>
            /// The distance from the start of the contianer to the beginning of an array of slot data.
            /// </summary>
            public uint DistanceContainerSlotsBegin;

            /// <summary>
            /// The distance from the start of the item to the Id variable.
            /// </summary>
            public uint DistanceContainerSlotItemId;
            /// <summary>
            /// The distance from the start of the item to the ItemCount variable.
            /// </summary>
            public uint DistanceContainerSlotItemCount;

            /// <summary>
            /// End of the container list in memory.
            /// </summary>
            public uint End;
        }
    }
}