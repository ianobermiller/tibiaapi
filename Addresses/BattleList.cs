using System.Collections.Generic;

namespace Tibia.Addresses
{
    /// <summary>
    /// Battle list adddresses and distances.
    /// </summary>
    public static class BattleList
    {
        /// <summary>
        /// Start of the battle list.
        /// </summary>
        public static uint Start = 0x613BD0; // 8.1, 8.0 = 0x60EB30

        /// <summary>
        /// End of the battle list.
        /// </summary>
        public static uint End = 0x619990; //8.1, 8.0 = 0x6148F0

        /// <summary>
        /// Distance between creatures.
        /// </summary>
        public static uint Step_Creatures = 0xA0; //8.0

        /// <summary>
        /// Maximum number of creatures.
        /// </summary>
        public static uint Max_Creatures = 150;
    }
}
