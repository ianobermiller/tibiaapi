using System.Collections.Generic;

namespace Tibia.Memory.Addresses
{
    /// <summary>
    /// Battle list adddresses and distances.
    /// </summary>
    public static class BattleList
    {
        /// <summary>
        /// Start of the battle list.
        /// </summary>
        public static uint Start = 0x60EB30; //8.0

        /// <summary>
        /// End of the battle list.
        /// </summary>
        public static uint End = 0x6148F0; //8.0

        /// <summary>
        /// Distance between creatures.
        /// </summary>
        public static uint Step_Creatures = 0xA0; //8.0

        /// <summary>
        /// Maximum number of creatures.
        /// </summary>
        public static uint Max_Creatures = 100;
    }
}
