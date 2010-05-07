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
        public static uint StartTime = 0x7E0788; //8.57

        /// <summary>
        /// Address to the XTea encryption key.
        /// </summary>
        public static uint XTeaKey = 0x7998B4; //8.57 : RecvStream + 0x10

        /// <summary>
        /// Address of the socket struct
        /// </summary>
        public static uint SocketStruct = 0x799888; //8.57

        /// <summary>
        /// Pointer to the WS2_32.Recv function
        /// </summary>
        public static uint RecvPointer = 0x5B85E4; //8.57

        /// <summary>
        /// Pointer to the WS2_32.Send function
        /// </summary>
        public static uint SendPointer = 0x5B8610; //8.57


        /// <summary>
        /// FPS (Frames Per Second) Pointer
        /// </summary>
        public static uint FrameRatePointer = 0x79DA6C; //8.57

        /// <summary>
        /// FPS limit offset
        /// </summary>
        public static int FrameRateLimitOffset = 0x58;

        /// <summary>
        /// Current fps offset
        /// </summary>
        public static int FrameRateCurrentOffset = 0x60;

        /// <summary>
        /// Address to activate multiclient.
        /// </summary>
        public static uint MultiClient = 0x50BB24; //8.57

        /// <summary>
        /// Value to be written to the multiclient address(JMP).
        /// </summary>
        public static byte MultiClientJMP = 0xEB;

        ///<summary>
        /// Original value of the multiclient address(JNZ).
        /// </summary>
        public static byte MultiClientJNZ = 0x75; 

        /// <summary>
        /// 8 = Connected | 0 = Disconnected
        /// </summary>
        public static uint Status = 0x79CF20; //8.57

        /// <summary>
        /// Safe mode (don't attack other players)
        /// </summary>
        public static uint SafeMode = 0x799CDC; //8.57
        /// <summary>
        /// Follow mode while attacking (Follow, keep distance, stand still)
        /// </summary>
        public static uint FollowMode = SafeMode + 4;

        /// <summary>
        /// Attack type (Full attack, half and half, full defense)
        /// </summary>
        public static uint AttackMode = FollowMode + 4;

        /// <summary>
        /// Action state (formerly MouseCursor icon)
        /// </summary>
        public static uint ActionState = 0x79CF80; //8.57

        /// <summary>
        /// Action state freezer
        /// </summary>
        public static uint ActionStateFreezer = 0x51D074; //8.57
        public static byte[] ActionStateOriginal = new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC };
        public static byte[] ActionStateFreezed = new byte[] { 0xC7, 0x05, 0x00 , 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0xC3 };
        /// <summary>
        /// The text of the last message sent to the default channel(innacurate?).
        /// </summary>
        public static uint LastMSGText = 0x7E094F8; //8.57

        /// <summary>
        /// The last player to send a message to the default channel(innacurate?).
        /// </summary>
        public static uint LastMSGAuthor = LastMSGText - 0x28;

        /// <summary>
        /// The statusbar text to be displayed.
        /// </summary>
        public static uint StatusbarText = 0x7E07A8; //8.57
        /// <summary>
        /// The time that the text will be displayed for in the statusbar.
        /// </summary>
        public static uint StatusbarTime = StatusbarText - 4;

        /// <summary>
        /// The id of the last clicked item.
        /// </summary>
        public static uint ClickId = 0x79CFC0; // 8.57
        /// <summary>
        /// The amount of the last clicked item (eg. 52 fish)
        /// </summary>
        public static uint ClickCount = ClickId + 4;
        /// <summary>
        /// The floor that was clicked.
        /// </summary>
        public static uint ClickZ = ClickId - 0x68;

        /// <summary>
        /// Used for showing item id functions.
        /// </summary>
        public static uint ClickContextMenuItemId = 0x79CFCC; //8.57

        /// <summary>
        /// Used for showing item id functions
        /// Deprecated on 8.5x due to player stacking?
        /// </summary>
        public static uint ClickContextMenuItemGroundId = 0; //8.57

        /// <summary>
        /// Used for searching the last right-clicked creature
        /// </summary>
        public static uint ClickContextMenuCreatureId = 0x79CFD0; //8.57

        /// <summary>
        /// The id of the last item seen (looked at).
        /// </summary>
        public static uint SeeId = ClickId + 12;
        /// <summary>
        /// The amount of the last item seen (eg. 42 fish).
        /// </summary>
        public static uint SeeCount = SeeId + 4;
        /// <summary>
        /// The floor that the last seen item is on.
        /// </summary>
        public static uint SeeZ = SeeId - 0x68;
        
        /// <summary>
        /// The text that came with the last seen item (eg. You see a fish).
        /// Deprecated on 8.50+ due to Server Log channel.
        /// </summary>
        public static uint SeeText = 0; //8.50 
        
        // Login Server addresses
        public static uint LoginServerStart = 0x7947F0; //8.57
        public static uint StepLoginServer = 112;
        public static uint DistancePort = 100;
        public static uint MaxLoginServers = 10;

        /// <summary>
        /// RSA Key Adress
        /// </summary>
        public static uint RSA = 0x5B8980; //8.57

		  
        /// <summary>
        /// Login character list. This points to the character list.
        /// </summary>
        public static uint LoginCharList = 0x79CED4; //8.57

        /// <summary>
        /// Login character list length, specifies how many characters the upper address leads to
        /// </summary>
        public static uint LoginCharListLength = 0x79CED8; // 8.57

        /* Character List Format
        
        30 bytes - Character name, null terminated string
        30 bytes - Server name, null terminated string
        4 bytes - Binary IP, the IP address in hex
        16 bytes - IP string, null terminated string of the IP address
        2 bytes - Port number
        2 bytes - Padding
        
        */

        /// <summary>
        /// Login character list selected character. This address doesn't move.
        /// </summary>
        public static uint LoginSelectedChar = 0x79CED0; // 8.57

        //This format is for the character list that is stored at 0x76450D (8.40).
        //This format is also how it comes in the packet.
        //This list gets overwritten with different data when connected to the game world.
        
        /* Character List Format
        
        2 bytes - Length of name
        n bytes - Name
        
        2 bytes - Length of sever name
        n bytes - Server name
        
        4 bytes - IP address
        2 bytes - Port number
        
        */


        /// <summary>
        /// Pointer to an address. When that address has 0x4E added to
        /// it, it points to the game window rect 
        /// struct.
        /// </summary>
        public static uint GameWindowRectPointer = 0x64C254; //8.57
        public static uint GameWindowBar = 0x7E079C; //8.57
        /*
            Several notes are needed on this one.
            1) This address is in the stack so it is very volitile. However it appears
               that it does not change as long as the player stays logged in.
            2) This address is always in the same place as long as the player
               is logged in.
            3) This address points to another address. Once you obtain that address
               you must add 0x4E and that will point to the begining of the struct.
         
            Struct Layout (each 4 bytes):
            X, Y, Width, Height
        */

        public static uint DatPointer = 0x7998D4; //8.57

        public static uint EventTriggerPointer = 0x51F4D0; //8.57
        public static uint DialogPointer = 0x64F5BC; //8.57
        public static uint DialogLeft = 0x14;
        public static uint DialogTop = 0x18;
        public static uint DialogWidth = 0x1C;
        public static uint DialogHeight = 0x20;
        public static uint DialogCaption = 0x50;

        /// <summary>
        /// Last Received Packet
        /// </summary>
        public static uint LastRcvPacket = 0x795068; //8.57

        /// <summary>
        /// Call to decrypt packet
        /// </summary>
        public static uint DecryptCall = 0x45C345; //8.57 : Same as GetNextPacketCall

        /// <summary>
        /// Auto login stuff
        /// </summary>
        public static uint LoginPassword = 0x79CEDC; //8.57
        public static uint LoginAccount = LoginPassword + 32;
        public static uint LoginAccountNum = 0; // deprecated

        public static uint LoginPatch = 0x47935E; // 8.11
        public static uint LoginPatch2 = 0x47A2B3; // 8.11

        public static byte Nop = 0x90;
        public static byte[] LoginPatchOrig = new byte[] { 0xE8, 0x0D, 0x1D, 0x09, 0x00 };
        public static byte[] LoginPatchOrig2 = new byte[] { 0xE8, 0xC8, 0x15, 0x09, 0x00 };

        /// <summary>
        /// The function that tibia calls to parse packets
        /// </summary>
        public static uint ParserFunc = 0x45C310; //8.57

        /// <summary>
        /// The address of the call to get next packet command
        /// </summary>
        public static uint GetNextPacketCall = 0x45C345; //8.57 : Same as DecryptCall
        
        /// <summary>
        /// The address of the received "stream". It is laid as pointer to buffer, dwSize, dwSize
        /// </summary>
        public static uint RecvStream = 0x7998A4; //8.57


    }
}
