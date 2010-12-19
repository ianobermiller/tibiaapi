namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Experience = 0x63FD50; // 8.70
        public static uint Flags = Experience - 112;

        public static uint GoToX = Player_Flags + 196;
        public static uint GoToY = Player_Flags + 192;
        public static uint GoToZ = Player_Flags + 188;

        public static uint Id = Player_Flags + 128;
        public static uint Health = Player_Flags + 124;
        public static uint HealthMax = Player_Flags + 120;

        public static uint Level = Player_Flags + 104;
        public static uint MagicLevel = Player_Flags + 100;
        public static uint LevelPercent = Player_Flags + 96;
        public static uint MagicLevelPercent = Player_Flags + 92;

        public static uint Mana = Player_Flags + 88;
        public static uint ManaMax = Player_Flags + 84;

        public static uint Soul = Player_Flags + 80;
        public static uint Stamina = Player_Flags + 76;
        public static uint Capacity = Player_Flags + 72;

        public static uint Fishing = Player_Flags + 56;
        public static uint Shielding = Player_Flags + 52;
        public static uint Distance = Player_Flags + 48;
        public static uint Axe = Player_Flags + 44;
        public static uint Sword = Player_Flags + 40;
        public static uint Club = Player_Flags + 36;
        public static uint Fist = Player_Flags + 32;

        public static uint FishingPercent = Player_Flags + 28;
        public static uint ShieldingPercent = Player_Flags + 24;
        public static uint DistancePercent = Player_Flags + 20;
        public static uint AxePercent = Player_Flags + 16;
        public static uint SwordPercent = Player_Flags + 12;
        public static uint ClubPercent = Player_Flags + 8;
        public static uint FistPercent = Player_Flags + 4;
        

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

        public static uint CurrentTileToGo = Player_Flags + 132; // 8.70
        public static uint TilesToGo = Player_Flags + 136; // 8.70

        /// <summary>
        /// The number of times the player has attacked
        /// </summary>
        public static uint AttackCount = 0x63D900; //8.70

        /// <summary>
        /// The number of times the player has followed
        /// </summary>
        public static uint FollowCount = AttackCount; // 8.70 

        public static uint RedSquare = Player_Flags + 68; // 8.70
        public static uint GreenSquare = Player_Flags + 64;
        public static uint WhiteSquare = Player_Flags + 60;

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
