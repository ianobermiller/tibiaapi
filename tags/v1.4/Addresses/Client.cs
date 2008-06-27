namespace Tibia.Addresses
{
    /// <summary>
    /// Client addresses not specific to a player.
    /// </summary>
    public static class Client
    {
        /// <summary>
        /// The system time in ms when the client was started.
        /// Used for Creatures.Distance_BlackSquare calculations.
        /// </summary>
        public static uint StartTime = 0x76D90C; //8.1

        /// <summary>
        /// Address to the encryption key.
        /// </summary>
        public static uint XTeaKey = 0x768C7C; //8.1, 8.0 = 7637AC

        /// <summary>
        /// FPS (Frames Per Second) Limit
        /// </summary>
        public static uint FrameRate = 0x76CE0C; //8.1, 8.0 = 0x7661F4

        /// <summary>
        /// Address to activate multiclient.
        /// </summary>
        public static uint MultiClient = 0xF8944; //8.1

        /// <summary>
        /// Value to be written to the multiclient address.
        /// </summary>
        public static uint MultiClientValue = 0xEB; //8.1

        /// <summary>
        /// 8 = Connected | 0 = Disconnected
        /// </summary>
        public static uint Status = 0x76C2C8;           //8.1, 8.0 = 0x766DF8

        /// <summary>
        /// Attack type (Full attack, half and half, full defense)
        /// </summary>
        public static uint AttackMode = 0x7690A4; //8.1, 8.0 = 0x763BD4

        /// <summary>
        /// Follow mode while attacking (Follow, keep distance, stand still)
        /// </summary>
        public static uint FollowMode = 0x7690A0; //8.1, 8.0 = 0x763BD0

        /// <summary>
        /// Safe mode (don't attack other players)
        /// </summary>
        public static uint SafeMode = 0x76909C; //8.1, 8.0 = 0x763BCC

        /// <summary>
        /// Cursor icon
        /// </summary>
        public static uint MouseCursor = 0x0076C328; //8.1, 8.0 = 0x751BD8

        /// <summary>
        /// The window that is foremost
        /// </summary>
        public static uint CurrentWindow = 0x61E984;    //8.1, 8.0 = 0x6198B4

        /// <summary>
        /// The last player to send a message to the default channel.
        /// </summary>
        public static uint LastMSGAuthor = LastMSGText - 0x28; //8.1, 8.0 = 0x768680

        /// <summary>
        /// The text of the last message sent to the default channel.
        /// </summary>
        public static uint LastMSGText = 0x76DB78; //8.1, 8.0 = 0x7686A8

        /// <summary>
        /// Statusbar
        /// </summary>
        public static uint Statusbar_Text = 0x0076D928; // 8.1, 8.0 = 0x00768458
        public static uint Statusbar_Time = Statusbar_Text - 4; // 8.1, 8.0 = 0x00768454

        /// <summary>
        /// Information on the last clicked item
        /// </summary>
        public static uint Click_Id = 0x76C364;   //8.1, 8.0 = 0x766E94
        public static uint Click_Count = Click_Id + 4;  //8.1, 8.0 = 0x766E98
        public static uint Click_Z = Click_Id - 0x68;      //8.1, 8.0 = 0x766E2C

        /// <summary>
        /// See (inspect)
        /// </summary>
        public static uint See_Id = Click_Id + 12;     //8.1, 8.0 = 0x766EA0
        public static uint See_Count = See_Id + 4;  //8.1, 8.0 = 0x766EA4
        public static uint See_Z = See_Id - 0x68;      //8.1, 8.0 = 0x766E00

        // Login Server addresses
        public static uint LoginServerStart = 0x763BB8; //8.1, 8.0 = 0x75EAE8
        public static uint Step_LoginServer = 112;
        public static uint Distance_Port = 100;
        public static uint Max_LoginServers = 10;

        /// <summary>
        /// RSA Key Adress
        /// </summary>
        public static uint RSA = 0x597610;                  //8.1, 8.0 = 0x593610
		  
        /// <summary>
        /// Login character list.
        /// </summary>
        public static uint LoginCharList = 0x76C28C;        //8.1, 8.0 = 0x766DBC

        /// <summary>
        /// Login character list selected character.
        /// </summary>
        public static uint LoginSelectedChar = 0x76C288;    //8.1, 8.0 = 0x766DB8
    }
}
