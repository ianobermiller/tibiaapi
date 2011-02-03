namespace Tibia.Addresses
{
    /// <summary>
    /// Battle list adddresses and distances.
    /// </summary>
    public static class BattleList
    {
        /// <summary>
        /// Distance between creatures.
        /// </summary>
        public static uint StepCreatures = 0xAC; // A8 before 8.70

        /// <summary>
        /// Maximum number of creatures.
        /// </summary>
        public static uint MaxCreatures = 1300; // it was 250 before 8.62

        /// <summary>
        /// Start of the battle list.
        /// </summary>
        public static uint Start = 0x63FDE8; //8.71

        /// <summary>
        /// End of the battle list.
        /// </summary>
        public static uint End = Start + (StepCreatures * MaxCreatures);
    }
}
