namespace Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Id = 0x60EAD0;               //8.0
        public static uint Exp = 0x60EAC4;              //8.0
        public static uint Flags = 0x60EA58;            //8.0

        public static uint Level = 0x60EAC0;            //8.0
        public static uint Level_Percent = 0x60EAB8;       //8.0
        public static uint MagicLevel = 0x60EABC;           //8.0
        public static uint MagicLevel_Percent = 0x60EAB4;      //8.0

        public static uint Mana = 0x60EAB0;             //8.0
        public static uint Mana_Max = 0x60EAAC;         //8.0
        public static uint HP = 0x60EACC;               //8.0
        public static uint HP_Max = 0x60EAC8;           //8.0

        public static uint Soul = 0x60EAA8;             //8.0
        public static uint Cap = 0x60EAA0;              //8.0
        public static uint Stamina = 0x60EAA4;          //8.0

        public static uint Fist = 0x60EA78;             //8.0
        public static uint Fist_Percent = 0x60EA5C;        //8.0
        public static uint Club = 0x60EA7C;             //8.0
        public static uint Club_Percent = 0x60EA60;        //8.0
        public static uint Sword = 0x60EA80;            //8.0
        public static uint Sword_Percent = 0x60EA64;       //8.0
        public static uint Axe = 0x60EA84;              //8.0
        public static uint Axe_Percent = 0x60EA68;         //8.0
        public static uint Distance = 0x60EA84;         //8.0
        public static uint Distance_Percent = 0x60EA6C;    //8.0
        public static uint Shielding = 0x60EA8C;        //8.0
        public static uint Shielding_Percent = 0x60EA70;   //8.0
        public static uint Fishing = 0x60EA90;          //8.0
        public static uint Fishing_Percent = 0x60EA74;     //8.0

        public static uint Slot_Head = 0x616F88;        //8.0
        public static uint Slot_Neck = Slot_Head + 12;    //8.0
        public static uint Slot_Backpack = Slot_Head + 24;    //8.0
        public static uint Slot_Armor = Slot_Head + 36;       //8.0
        public static uint Slot_Right = Slot_Head + 48;       //8.0
        public static uint Slot_Left = Slot_Head + 60;        //8.0
        public static uint Slot_Legs = Slot_Head + 72;        //8.0
        public static uint Slot_Feet = Slot_Head + 84;        //8.0
        public static uint Slot_Ring = Slot_Head + 96;        //8.0
        public static uint Slot_Ammo = Slot_Head + 108;        //8.0

        public static uint Distance_Slot_Count = 4;

        public static uint Slot_Right_Count = 0x616FC8; //8.0
        public static uint Slot_Left_Count = 0x616FBC;  //8.0
        public static uint Slot_Ammo_Count = 0x616FF8;  //8.0

        public static uint GoTo_X = 0x60EB10;           //8.0
        public static uint GoTo_Y = 0x60EB14;           //8.0
        public static uint GoTo_Z = 0x60EB0C;           //8.0

        public static uint RedSquare = 0x60EA9C;        //8.0
        public static uint GreenSquare = 0x60EA98;      //8.0
        public static uint WhiteSquare = 0x60EA94;      //8.0

        public static uint AccessN = 0x766DF4;          //8.0
        public static uint AccessS = 0x766DC4;          //8.0

        public static uint Target_ID = 0x60EA9C;        //8.0
        public static uint Target_Type = 0x60EA9F;      //8.0
        public static uint Target_BList_ID = 0x60EA94;  //8.0
        public static uint Target_BList_Type = 0x60EA97;//8.0
    }
}
