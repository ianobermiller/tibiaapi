namespace Tibia.Addresses
{
    /// <summary>
    /// Client addresses not specific to a player.
    /// </summary>
    public static class Client
    {
        /// <summary>
        /// Address to the encryption key.
        /// </summary>
        public static uint XTeaKey = 0x7637AC; //8.0

        /// <summary>
        /// FPS (Frames Per Second) Limi
        /// </summary>
        public static uint FrameRate = 0x7661F4; //8.0

        /// <summary>
        /// Address to activate multiclient.
        /// </summary>
        public static uint MultiClient = 0x4EFB71; //?.?

        /// <summary>
        /// Value to be written to the multiclient address.
        /// </summary>
        public static uint MultiClientValue = 0xEB; //8.0

        /// <summary>
        /// 8 = Connected | 0 = Disconnected
        /// </summary>
        public static uint Status = 0x766DF8;           //8.0

        /// <summary>
        /// Follow mode while attacking (Follow, keep distance, stand still)
        /// </summary>
        public static uint FollowMode = 0x763BD0; //8.0

        /// <summary>
        /// Attack type (Full attack, half and half, full defense)
        /// </summary>
        public static uint AttackMode = 0x763BD4; //8.0

        /// <summary>
        /// Safe mode (don't attack other players)
        /// </summary>
        public static uint SafeMode = 0x763BCC; //8.0

        /// <summary>
        /// Cursor icon
        /// </summary>
        public const long MouseCursor = 0x751BD8;

        /// <summary>
        /// The window that is foremost
        /// </summary>
        public static uint CurrentWindow = 0x6198B4;    //Window

        /// <summary>
        /// The last player to send a message to the default channel.
        /// </summary>
        public static uint LastMSGAuthor = 0x768680; //8.0

        /// <summary>
        /// The text of the last message sent to the default channel.
        /// </summary>
        public static uint LastMSGText = 0x7686A8; //8.0

        /// <summary>
        /// Statusbar
        /// </summary>
        public static uint Statusbar_Text = 0x00768458;
        public static uint Statusbar_Time = 0x00768454;

        /// <summary>
        /// Information on the last clicked item
        /// </summary>
        public static uint Click_Id = 0x766E94;
        public static uint Click_Count = 0x766E98;
        public static uint Click_Z = 0x766E2C;

        /// <summary>
        /// See (inspect)
        /// </summary>
        public static uint See_Id = 0x766EA0; // also 0x00766EAC
        public static uint See_Count = 0x766EA4; // also 0x00766EB0
        public static uint See_Z = 0x766E00; // also 0x00766E10

        // Login Server addresses
        public static uint LoginServerStart = 0x75EAE8; //8.0
        public static uint Step_LoginServer = 112;
        public static uint Distance_Port = 100;
        public static uint Max_LoginServers = 10;

        /// <summary>
        /// RSA Key Adress
        /// </summary>
        public static uint RSA = 0x593610;
		  
        /// <summary>
        /// Login character list.
        /// </summary>
        public static uint LoginCharList = 0x766DBC;

        /// <summary>
        /// Login character list selected character.
        /// </summary>
        public static uint LoginSelectedChar = 0x766DB8;
    }
}
