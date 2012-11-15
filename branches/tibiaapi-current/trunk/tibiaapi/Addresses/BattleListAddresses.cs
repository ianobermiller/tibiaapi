namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public BattleListAddresses BattleList = new BattleListAddresses();
        /// <summary>
        /// Battle list adddresses and distances.
        /// </summary>
        public class BattleListAddresses
        {
            /// <summary>
            /// Distance between creatures.
            /// </summary>
            public uint StepCreatures; // A8 before 8.70

            /// <summary>
            /// Maximum number of creatures.
            /// </summary>
            public uint MaxCreatures; // it was 250 before 8.62

            /// <summary>
            /// Start of the battle list.
            /// </summary>
            public uint Start;

            /// <summary>
            /// End of the battle list.
            /// </summary>
            public uint End;
        }
    }
}
