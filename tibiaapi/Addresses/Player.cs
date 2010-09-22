namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Experience = 0x637C4C; // 8.62

        public static uint GoToX = Experience + 80;
        public static uint GoToY = Experience + 76;
        public static uint GoToZ = Experience + 72;

        public static uint Id = Experience + 12;
        public static uint Health = Experience + 8;
        public static uint HealthMax = Experience + 4;

        public static uint Level = Experience - 4;
        public static uint MagicLevel = Experience - 8;
        public static uint LevelPercent = Experience - 12;
        public static uint MagicLevelPercent = Experience - 16;

        public static uint Mana = Experience - 20;
        public static uint ManaMax = Experience - 24;

        public static uint Soul = Experience - 28;
        public static uint Stamina = Experience - 32;
        public static uint Capacity = Experience - 36;

        public static uint Fishing = Experience - 52;
        public static uint Shielding = Experience - 56;
        public static uint Distance = Experience - 60;
        public static uint Axe = Experience - 64;
        public static uint Sword = Experience - 68;
        public static uint Club = Experience - 72;
        public static uint Fist = Experience - 76;

        public static uint FishingPercent = Experience - 80;
        public static uint ShieldingPercent = Experience - 84;
        public static uint DistancePercent = Experience - 88;
        public static uint AxePercent = Experience - 92;
        public static uint SwordPercent = Experience - 96;
        public static uint ClubPercent = Experience - 100;
        public static uint FistPercent = Experience - 104;
        public static uint Flags = Experience - 108;

        /// <summary>
        /// Total number of equipment slots (accessed 0-10)
        /// </summary>
        public static int MaxSlots = 11;
        public static uint SlotHead = 0x66FAC0; // 8.62
        public static uint SlotNeck = SlotHead + 12;
        public static uint SlotBackpack = SlotHead + 24;
        public static uint SlotArmor = SlotHead + 36;
        public static uint SlotRight = SlotHead + 48;
        public static uint SlotLeft = SlotHead + 60;
        public static uint SlotLegs = SlotHead + 72;
        public static uint SlotFeet = SlotHead + 84;
        public static uint SlotRing = SlotHead + 96;
        public static uint SlotAmmo = SlotHead + 108;

        public static uint DistanceSlotCount = 4;

        public static uint CurrentTileToGo = 0x637C60; // 8.62
        public static uint TilesToGo = 0x637C64; // 8.62

        /// <summary>
        /// The number of times the player has attacked
        /// </summary>
        public static uint AttackCount = 0x635800; //8.62

        /// <summary>
        /// The number of times the player has followed
        /// </summary>
        public static uint FollowCount = AttackCount + 0x20; //8.62

        public static uint RedSquare = 0x637C24; // 8.62
        public static uint GreenSquare = RedSquare - 4;
        public static uint WhiteSquare = GreenSquare - 8;

        public static uint AccessN = 0; // 8.0
        public static uint AccessS = 0; // 8.0

        public static uint TargetId = RedSquare;
        public static uint TargetBattlelistId = TargetId - 8; 
        public static uint TargetBattlelistType = TargetId - 5;
        public static uint TargetType = TargetId + 3;

        /// <summary>
        /// Static address for player Z, used for level spy
        /// </summary>
        public static uint Z = 0x672428; // 8.62

        public static uint Y = Z + 4; // 8.62
        public static uint X = Z + 8; // 8.62
    }
}
