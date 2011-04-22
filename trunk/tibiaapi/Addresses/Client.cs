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
        public static uint StartTime;

        /// <summary>
        /// Address to the XTea encryption key.
        /// </summary>
        public static uint XTeaKey; //RecvStream + 0x10

        /// <summary>
        /// Address of the socket struct
        /// </summary>
        public static uint SocketStruct;

        /// <summary>
        /// Pointer to the WS2_32.Recv function
        /// </summary>
        public static uint RecvPointer;

        /// <summary>
        /// Pointer to the WS2_32.Send function
        /// </summary>
        public static uint SendPointer;


        /// <summary>
        /// FPS (Frames Per Second) Pointer
        /// </summary>
        public static uint FrameRatePointer;

        /// <summary>
        /// FPS limit offset
        /// </summary>
        public static int FrameRateLimitOffset;

        /// <summary>
        /// Current fps offset
        /// </summary>
        public static int FrameRateCurrentOffset;

        /// <summary>
        /// Address to activate multiclient.
        /// </summary>
        public static uint MultiClient;

        /// <summary>
        /// Value to be written to the multiclient address(JMP).
        /// </summary>
        public static byte MultiClientJMP;

        ///<summary>
        /// Original value of the multiclient address(JNZ).
        /// </summary>
        public static byte MultiClientJNZ;

        /// <summary>
        /// 8 = Connected | 0 = Disconnected
        /// </summary>
        public static uint Status;

        /// <summary>
        /// Safe mode (don't attack other players)
        /// </summary>
        public static uint SafeMode;
        /// <summary>
        /// Follow mode while attacking (Follow, keep distance, stand still)
        /// </summary>
        public static uint FollowMode;

        /// <summary>
        /// Attack type (Full attack, half and half, full defense)
        /// </summary>
        public static uint AttackMode;

        /// <summary>
        /// Action state (formerly MouseCursor icon)
        /// </summary>
        public static uint ActionState;

        /// <summary>
        /// Action state freezer
        /// </summary>
        public static uint ActionStateFreezer;
        public static byte[] ActionStateOriginal;
        public static byte[] ActionStateFreezed;
        /// <summary>
        /// The text of the last message sent to the default channel(innacurate?).
        /// </summary>
        public static uint LastMSGText;

        /// <summary>
        /// The last player to send a message to the default channel(innacurate?).
        /// </summary>
        public static uint LastMSGAuthor;

        /// <summary>
        /// The statusbar text to be displayed.
        /// </summary>
        public static uint StatusbarText;
        /// <summary>
        /// The time that the text will be displayed for in the statusbar.
        /// </summary>
        public static uint StatusbarTime;

        /// <summary>
        /// The id of the last clicked item.
        /// </summary>
        public static uint ClickId;
        /// <summary>
        /// The amount of the last clicked item (eg. 52 fish)
        /// </summary>
        public static uint ClickCount;
        /// <summary>
        /// The floor that was clicked.
        /// </summary>
        public static uint ClickZ;

        /// <summary>
        /// Used for showing item id functions.
        /// </summary>
        public static uint ClickContextMenuItemId; //This is also the "SeeID"

        /// <summary>
        /// Used for showing item id functions
        /// Deprecated on 8.5x due to player stacking?
        /// </summary>
        public static uint ClickContextMenuItemGroundId;

        /// <summary>
        /// Used for searching the last right-clicked creature
        /// </summary>
        public static uint ClickContextMenuCreatureId;

        /// <summary>
        /// The id of the last item seen (looked at).
        /// </summary>
        public static uint SeeId;
        /// <summary>
        /// The amount of the last item seen (eg. 42 fish).
        /// </summary>
        public static uint SeeCount;
        /// <summary>
        /// The floor that the last seen item is on.
        /// </summary>
        public static uint SeeZ;
        
        /// <summary>
        /// The text that came with the last seen item (eg. You see a fish).
        /// Deprecated on 8.50+ due to Server Log channel.
        /// </summary>
        public static uint SeeText;
        
        // Login Server addresses
        public static uint LoginServerStart;
        public static uint StepLoginServer;
        public static uint DistancePort;
        public static uint MaxLoginServers;

        /// <summary>
        /// RSA Key Adress
        /// </summary>
        public static uint RSA;

		  
        /// <summary>
        /// Login character list. This points to the character list.
        /// </summary>
        public static uint LoginCharList;

        /// <summary>
        /// Login character list length, specifies how many characters the upper address leads to
        /// </summary>
        public static uint LoginCharListLength;

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
        public static uint LoginSelectedChar;

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
        public static uint GameWindowRectPointer;
        public static uint GameWindowBar;
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

        public static uint DatPointer;

        public static uint EventTriggerPointer;
        public static uint DialogPointer;
        public static uint DialogLeft;
        public static uint DialogTop;
        public static uint DialogWidth;
        public static uint DialogHeight;
        public static uint DialogCaption;

        /// <summary>
        /// Last Received Packet
        /// </summary>
        public static uint LastRcvPacket;

        /// <summary>
        /// Call to decrypt packet
        /// </summary>
        public static uint DecryptCall; //Same as GetNextPacketCall ALSO = ParserFunc + 0X35
        


        /// <summary>
        /// Auto login stuff
        /// </summary>
        public static uint LoginPassword;
        public static uint LoginAccount;
        public static uint LoginAccountNum; // deprecated

        public static uint LoginPatch;
        public static uint LoginPatch2;

        public static byte Nop = 0x90;
        public static byte[] LoginPatchOrig;
        public static byte[] LoginPatchOrig2;

        /// <summary>
        /// The function that tibia calls to parse packets
        /// </summary>

        public static uint ParserFunc;

        /// <summary>
        /// The address of the call to get next packet command
        /// </summary>
        public static uint GetNextPacketCall; //Same as DecryptCall
        
        /// <summary>
        /// The address of the received "stream". It is laid as pointer to buffer, dwSize, dwSize
        /// </summary>
        public static uint RecvStream;
    }
}
