namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Experience;
        public static uint Flags;

        public static uint GoToX;
        public static uint GoToY;
        public static uint GoToZ;

        public static uint Id;
        public static uint Health;
        public static uint HealthMax;

        public static uint Level;
        public static uint MagicLevel;
        public static uint LevelPercent;
        public static uint MagicLevelPercent;

        public static uint Mana;
        public static uint ManaMax;

        public static uint Soul;
        public static uint Stamina;
        public static uint Capacity;

        public static uint Fishing;
        public static uint Shielding;
        public static uint Distance;
        public static uint Axe;
        public static uint Sword;
        public static uint Club;
        public static uint Fist;

        public static uint FishingPercent;
        public static uint ShieldingPercent;
        public static uint DistancePercent;
        public static uint AxePercent;
        public static uint SwordPercent;
        public static uint ClubPercent;
        public static uint FistPercent;


        /// <summary>
        /// Total number of equipment slots (accessed 0-9)
        /// </summary>
        public static int MaxSlots;
        public static uint SlotHead;
        public static uint SlotNeck;
        public static uint SlotBackpack;
        public static uint SlotArmor;
        public static uint SlotRight;
        public static uint SlotLeft;
        public static uint SlotLegs;
        public static uint SlotFeet;
        public static uint SlotRing;
        public static uint SlotAmmo;

        public static uint DistanceSlotCount;

        public static uint CurrentTileToGo;
        public static uint TilesToGo;

        /// <summary>
        /// The number of times the player has attacked
        /// </summary>
        public static uint AttackCount;

        /// <summary>
        /// The number of times the player has followed
        /// </summary>
        public static uint FollowCount;

        public static uint RedSquare = Flags + 68; // 8.72
        public static uint GreenSquare = Flags + 64;
        public static uint WhiteSquare = Flags + 60;

        public static uint AccessN;
        public static uint AccessS;

        public static uint TargetId;
        public static uint TargetBattlelistId;
        public static uint TargetBattlelistType;
        public static uint TargetType;

        /// <summary>
        /// Static address for player Z, used for level spy
        /// </summary>
        public static uint Z;

        public static uint Y;
        public static uint X;
    }
}
