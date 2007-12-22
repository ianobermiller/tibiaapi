namespace Tibia.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Flags = 0x00613AF8; //8.1, 8.0 = 0x60EA58

        public static uint Exp = 0x00613B64; //8.1, 8.0 = 0x60EAC4

        public static uint Id = Exp + 12; //8.1, 8.0 = 0x60EAD0
        public static uint HP = Exp + 8; //8.1, 8.0 = 0x60EACC
        public static uint HP_Max = Exp + 4; //8.1, 8.0 = 0x60EAC8

        public static uint Level = Exp - 4; //8.1, 8.0 = 0x60EAC0
        public static uint MagicLevel = Exp - 8; //8.1, 8.0 = 0x60EABC
        public static uint Level_Percent = Exp - 12; //8.1, 8.0 = 0x60EAB8
        public static uint MagicLevel_Percent = Exp - 16; //8.1, 8.0 = 0x60EAB4

        public static uint Mana = Exp - 20; //8.1, 8.0 = 0x60EAB0
        public static uint Mana_Max = Exp - 24; //8.1, 8.0 = 0x60EAAC

        public static uint Soul = Exp - 28; //8.1, 8.0 = 0x60EAA8
        public static uint Stamina = Exp - 32; //8.1, 8.0 = 0x60EAA4
        public static uint Cap = Exp - 36; //8.1, 8.0 = 0x60EAA0

        public static uint Fist_Percent = 0x00613AFC;               //8.1, 8.0 = 0x60EA5C
        public static uint Club_Percent = Fist_Percent + 4;         //8.1, 8.0 = 0x60EA60
        public static uint Sword_Percent = Fist_Percent + 8;        //8.1, 8.0 = 0x60EA64
        public static uint Axe_Percent = Fist_Percent + 12;         //8.1, 8.0 = 0x60EA68
        public static uint Distance_Percent = Fist_Percent + 16;    //8.1, 8.0 = 0x60EA6C
        public static uint Shielding_Percent = Fist_Percent + 20;   //8.1, 8.0 = 0x60EA70
        public static uint Fishing_Percent = Fist_Percent + 24;     //8.1, 8.0 = 0x60EA74
        public static uint Fist = Fist_Percent + 28;                //8.1, 8.0 = 0x60EA78
        public static uint Club = Fist_Percent + 32;                //8.1, 8.0 = 0x60EA7C
        public static uint Sword = Fist_Percent + 36;               //8.1, 8.0 = 0x60EA80
        public static uint Axe = Fist_Percent + 40;                 //8.1, 8.0 = 0x60EA84
        public static uint Distance = Fist_Percent + 44;            //8.1, 8.0 = 0x60EA88
        public static uint Shielding = Fist_Percent + 48;           //8.1, 8.0 = 0x60EA8C
        public static uint Fishing = Fist_Percent + 52;             //8.1, 8.0 = 0x60EA90

        public static uint Slot_Head = 0x61C058;            //8.1, 8.0 = 0x616F88
        public static uint Slot_Neck = Slot_Head + 12;      //8.1
        public static uint Slot_Backpack = Slot_Head + 24;  //8.1
        public static uint Slot_Armor = Slot_Head + 36;     //8.1
        public static uint Slot_Right = Slot_Head + 48;     //8.1
        public static uint Slot_Left = Slot_Head + 60;      //8.1
        public static uint Slot_Legs = Slot_Head + 72;      //8.1
        public static uint Slot_Feet = Slot_Head + 84;      //8.1
        public static uint Slot_Ring = Slot_Head + 96;      //8.1
        public static uint Slot_Ammo = Slot_Head + 108;     //8.1

        public static uint Distance_Slot_Count = 4;

        public static uint Slot_Right_Count = Slot_Right + 4; //8.1, 8.0 = 0x616FC8
        public static uint Slot_Left_Count = Slot_Left + 4; //8.1, 8.0 = 0x616FBC
        public static uint Slot_Ammo_Count = Slot_Ammo + 4; //8.1, 8.0 = 0x616FF8

        public static uint CurrentTileToGo = 0x613B78; //8.1
        public static uint TilesToGo = 0x613B7C; //8.1

        public static uint GoTo_X = 0x613BB4;           //8.1, 8.0 = 0x60EB10
        public static uint GoTo_Y = GoTo_X - 4;           //8.1, 8.0 = 0x60EB14
        public static uint GoTo_Z = GoTo_X - 8;           //8.1, 8.0 = 0x60EB0C

        public static uint RedSquare = 0x613B3C;        //8.1, 8.0 = 0x60EA9C
        public static uint GreenSquare = RedSquare - 4;      //8.1
        public static uint WhiteSquare = GreenSquare - 8;      //8.1

        public static uint AccessN = 0x766DF4;          //8.0
        public static uint AccessS = 0x766DC4;          //8.0

        public static uint Target_ID = RedSquare;        //8.1
        public static uint Target_BList_ID = Target_ID - 8;  //8.1
        public static uint Target_BList_Type = Target_ID - 5;//8.1
        public static uint Target_Type = Target_ID + 3;      //8.1
    }
}
