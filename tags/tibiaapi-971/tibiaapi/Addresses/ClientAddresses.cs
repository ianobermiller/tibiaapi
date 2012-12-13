namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public ClientAddresses Client = new ClientAddresses();

        /// <summary>
        /// Client addresses not specific to a player.
        /// </summary>
        public class ClientAddresses
        {
            /// <summary>
            /// The system time in ms when the client was started.
            /// Used for Creatures.Distance_BlackSquare calculations.
            /// </summary>
            public uint StartTime;

            /// <summary>
            /// Address to the XTea encryption key.
            /// </summary>
            public uint XTeaKey; //RecvStream + 0x10

            /// <summary>
            /// Address of the socket struct
            /// </summary>
            public uint SocketStruct;

            /// <summary>
            /// Pointer to the WS2_32.Recv function
            /// </summary>
            public uint RecvPointer;

            /// <summary>
            /// Pointer to the WS2_32.Send function
            /// </summary>
            public uint SendPointer;


            /// <summary>
            /// FPS (Frames Per Second) Pointer
            /// </summary>
            public uint FrameRatePointer;

            /// <summary>
            /// FPS limit offset
            /// </summary>
            public int FrameRateLimitOffset;

            /// <summary>
            /// Current fps offset
            /// </summary>
            public int FrameRateCurrentOffset;

            /// <summary>
            /// Address to activate multiclient.
            /// </summary>
            public uint MultiClient;

            /// <summary>
            /// Value to be written to the multiclient address(JMP).
            /// </summary>
            public byte MultiClientJMP;

            ///<summary>
            /// Original value of the multiclient address(JNZ).
            /// </summary>
            public byte MultiClientJNZ;

            /// <summary>
            /// 8 = Connected | 0 = Disconnected
            /// </summary>
            public uint Status;

            /// <summary>
            /// Safe mode (don't attack other players)
            /// </summary>
            public uint SafeMode;
            /// <summary>
            /// Follow mode while attacking (Follow, keep distance, stand still)
            /// </summary>
            public uint FollowMode;

            /// <summary>
            /// Attack type (Full attack, half and half, full defense)
            /// </summary>
            public uint AttackMode;

            /// <summary>
            /// Action state (formerly MouseCursor icon)
            /// </summary>
            public uint ActionState;

            /// <summary>
            /// Action state freezer
            /// </summary>
            public uint[] ActionStateFreezer;
            public byte[] ActionStateOriginal;
            public byte[] ActionStateFreezed;
            /// <summary>
            /// The text of the last message sent to the default channel(innacurate?).
            /// </summary>
            [System.Obsolete]
            public uint LastMSGText;

            /// <summary>
            /// The last player to send a message to the default channel(innacurate?).
            /// </summary>
            [System.Obsolete]
            public uint LastMSGAuthor;

            /// <summary>
            /// The statusbar text to be displayed.
            /// </summary>
            public uint StatusbarText;
            /// <summary>
            /// The time that the text will be displayed for in the statusbar.
            /// </summary>
            public uint StatusbarTime;

            /// <summary>
            /// The id of the last clicked item.
            /// </summary>
            public uint ClickId;
            /// <summary>
            /// The amount of the last clicked item (eg. 52 fish)
            /// </summary>
            public uint ClickCount;
            /// <summary>
            /// The floor that was clicked.
            /// </summary>
            [System.Obsolete]
            public uint ClickZ;

            /// <summary>
            /// Used for showing item id functions.
            /// </summary>
            public uint ClickContextMenuItemId; //This is also the "SeeID"

            /// <summary>
            /// Used for showing item id functions
            /// </summary>
            [System.Obsolete("Deprecated on 8.5x due to player stacking?")]
            public uint ClickContextMenuItemGroundId;

            /// <summary>
            /// Used for searching the last right-clicked creature
            /// </summary>
            public uint ClickContextMenuCreatureId;

            /// <summary>
            /// The id of the last item seen (looked at).
            /// </summary>
            public uint SeeId;
            /// <summary>
            /// The amount of the last item seen (eg. 42 fish).
            /// </summary>
            public uint SeeCount;
            /// <summary>
            /// The floor that the last seen item is on.
            /// </summary>
            [System.Obsolete]
            public uint SeeZ;

            /// <summary>
            /// The text that came with the last seen item (eg. You see a fish).
            /// </summary>
            [System.Obsolete("Deprecated on 8.50+ due to Server Log channel.")]
            public uint SeeText;

            // Login Server addresses
            public uint LoginServerStart;
            public uint StepLoginServer;
            public uint DistancePort;
            public uint MaxLoginServers;

            /// <summary>
            /// RSA Key Adress
            /// </summary>
            public uint RSA;


            /// <summary>
            /// Login character list. This points to the character list.
            /// </summary>
            [System.Obsolete]
            public uint LoginCharList;

            /// <summary>
            /// Login character list length, specifies how many characters the upper address leads to
            /// </summary>
            [System.Obsolete]
            public uint LoginCharListLength;


            public uint LoginStruct;
            public uint LoginCharListBegin;
            public uint LoginCharListEnd;
            public uint LoginCharListDistanceCharName;
            public uint LoginCharListDistanceWorldName;
            public uint LoginCharListDistanceIsPreview;
            public uint LoginCharListDistanceWorldIP;
            public uint LoginCharListDistanceWorldPort;
            public uint LoginCharListStepCharacter;

            /// <summary>
            /// Login character list selected character. This address doesn't move.
            /// </summary>
            public uint LoginSelectedChar;


            /// <summary>
            /// Pointer to an address. When that address has 0x4E added to
            /// it, it points to the game window rect 
            /// struct.
            /// </summary>
            public uint GameWindowRectPointer;
            public uint GameWindowBar;
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

            public uint DatPointer;

            public uint EventTriggerPointer;
            public uint DialogPointer;
            public uint DialogLeft;
            public uint DialogTop;
            public uint DialogWidth;
            public uint DialogHeight;
            public uint DialogCaption;

            /// <summary>
            /// Last Received Packet
            /// </summary>
            public uint LastRcvPacket;

            /// <summary>
            /// Call to decrypt packet
            /// </summary>
            public uint DecryptCall; //Same as GetNextPacketCall ALSO = ParserFunc + 0X35



            /// <summary>
            /// Auto login stuff
            /// </summary>
            public uint LoginPassword;
            public uint LoginAccount;
            [System.Obsolete("Deprecated due to account name being strings.")]
            public uint LoginAccountNum;
            [System.Obsolete]
            public uint LoginPatch;
            [System.Obsolete]
            public uint LoginPatch2;
            [System.Obsolete]
            public byte[] LoginPatchOrig;
            [System.Obsolete]
            public byte[] LoginPatchOrig2;

            public byte Nop = 0x90;

            /// <summary>
            /// The function that tibia calls to parse packets
            /// </summary>

            public uint ParserFunc;

            /// <summary>
            /// The address of the call to get next packet command
            /// </summary>
            public uint GetNextPacketCall; //Same as DecryptCall

            /// <summary>
            /// The address of the received "stream". It is laid as pointer to buffer, dwSize, dwSize
            /// </summary>
            public uint RecvStream;

        }
    }
}