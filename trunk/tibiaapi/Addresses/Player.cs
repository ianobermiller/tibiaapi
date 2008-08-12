namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Exp = 0x626C64; // 8.22

        public static uint GoTo_X = Exp + 80;
        public static uint GoTo_Y = Exp + 76;
        public static uint GoTo_Z = Exp + 72;

        public static uint Id = Exp + 12;
        public static uint HP = Exp + 8;
        public static uint HP_Max = Exp + 4;

        public static uint Level = Exp - 4;
        public static uint MagicLevel = Exp - 8;
        public static uint Level_Percent = Exp - 12;
        public static uint MagicLevel_Percent = Exp - 16;

        public static uint Mana = Exp - 20;
        public static uint Mana_Max = Exp - 24;

        public static uint Soul = Exp - 28;
        public static uint Stamina = Exp - 32;
        public static uint Cap = Exp - 36;

        public static uint Fishing = Exp - 52;
        public static uint Shielding = Exp - 56;
        public static uint Distance = Exp - 60;
        public static uint Axe = Exp - 64;
        public static uint Sword = Exp - 68;
        public static uint Club = Exp - 72;
        public static uint Fist = Exp - 76;

        public static uint Fishing_Percent = Exp - 80;
        public static uint Shielding_Percent = Exp - 84;
        public static uint Distance_Percent = Exp - 88;
        public static uint Axe_Percent = Exp - 92;
        public static uint Sword_Percent = Exp - 96;
        public static uint Club_Percent = Exp - 100;
        public static uint Fist_Percent = Exp - 104;
        public static uint Flags = Exp - 108;

        /// <summary>
        /// Total number of equipment slots (accessed 0-10)
        /// </summary>
        public static int Max_Slots = 11;
        public static uint Slot_Head = 0x62D190; // 8.21
        public static uint Slot_Neck = Slot_Head + 12;
        public static uint Slot_Backpack = Slot_Head + 24;
        public static uint Slot_Armor = Slot_Head + 36;
        public static uint Slot_Right = Slot_Head + 48;
        public static uint Slot_Left = Slot_Head + 60;
        public static uint Slot_Legs = Slot_Head + 72;
        public static uint Slot_Feet = Slot_Head + 84;
        public static uint Slot_Ring = Slot_Head + 96;
        public static uint Slot_Ammo = Slot_Head + 108;

        public static uint Distance_Slot_Count = 4;
        public static uint Slot_Right_Count = Slot_Right + Distance_Slot_Count;
        public static uint Slot_Left_Count = Slot_Left + Distance_Slot_Count;
        public static uint Slot_Ammo_Count = Slot_Ammo + Distance_Slot_Count;



        public static uint CurrentTileToGo = 0x624C78; // 8.21
        public static uint TilesToGo = 0x624C7C; // 8.21


        public static uint RedSquare = 0x624C3C; // 8.21
        public static uint GreenSquare = RedSquare - 4;
        public static uint WhiteSquare = GreenSquare - 8;

        public static uint AccessN = 0x766DF4; // 8.0
        public static uint AccessS = 0x766DC4; // 8.0

        public static uint Target_ID = RedSquare;    
        public static uint Target_BList_ID = Target_ID - 8; 
        public static uint Target_BList_Type = Target_ID - 5;
        public static uint Target_Type = Target_ID + 3;     

        /// <summary>
        /// Static address for player Z, used for level spy
        /// </summary>
        public static uint Z = 0x631AF8; // 8.22
    }
}
