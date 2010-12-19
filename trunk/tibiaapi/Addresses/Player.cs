namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Experience = 0x63FD50; // 8.70
        public static uint Flags = Experience - 112;

        public static uint GoToX = Flags + 196;
        public static uint GoToY = Flags + 192;
        public static uint GoToZ = Flags + 188;

        public static uint Id = Flags + 128;
        public static uint Health = Flags + 124;
        public static uint HealthMax = Flags + 120;

        public static uint Level = Flags + 104;
        public static uint MagicLevel = Flags + 100;
        public static uint LevelPercent = Flags + 96;
        public static uint MagicLevelPercent = Flags + 92;

        public static uint Mana = Flags + 88;
        public static uint ManaMax = Flags + 84;

        public static uint Soul = Flags + 80;
        public static uint Stamina = Flags + 76;
        public static uint Capacity = Flags + 72;

        public static uint Fishing = Flags + 56;
        public static uint Shielding = Flags + 52;
        public static uint Distance = Flags + 48;
        public static uint Axe = Flags + 44;
        public static uint Sword = Flags + 40;
        public static uint Club = Flags + 36;
        public static uint Fist = Flags + 32;

        public static uint FishingPercent = Flags + 28;
        public static uint ShieldingPercent = Flags + 24;
        public static uint DistancePercent = Flags + 20;
        public static uint AxePercent = Flags + 16;
        public static uint SwordPercent = Flags + 12;
        public static uint ClubPercent = Flags + 8;
        public static uint FistPercent = Flags + 4;


        /// <summary>
        /// Total number of equipment slots (accessed 0-10)
        /// </summary>
        public static int MaxSlots = 11;
        public static uint SlotHead = 0x6790C8; // 8.70
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

        public static uint CurrentTileToGo = Flags + 132; // 8.70
        public static uint TilesToGo = Flags + 136; // 8.70

        /// <summary>
        /// The number of times the player has attacked
        /// </summary>
        public static uint AttackCount = 0x63D900; //8.70

        /// <summary>
        /// The number of times the player has followed
        /// </summary>
        public static uint FollowCount = AttackCount; // 8.70 

        public static uint RedSquare = Flags + 68; // 8.70
        public static uint GreenSquare = Flags + 64;
        public static uint WhiteSquare = Flags + 60;

        public static uint AccessN = 0; // 8.0
        public static uint AccessS = 0; // 8.0

        public static uint TargetId = RedSquare;
        public static uint TargetBattlelistId = TargetId - 8;
        public static uint TargetBattlelistType = TargetId - 5;
        public static uint TargetType = TargetId + 3;

        /// <summary>
        /// Static address for player Z, used for level spy
        /// </summary>
        public static uint Z = 0x67BA30; // 8.70

        public static uint Y = Z + 4; // 
        public static uint X = Z + 8; // 
    }
}
