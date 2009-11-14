using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        // Pipe
        PipePacket          = 0xFF
    }

    public enum SpeechType : byte
    {
        Say = 0x01,	//normal talk
        Whisper = 0x02,	//whispering - #w text
        Yell = 0x03,	//yelling - #y text
        PrivatePlayerToNPC = 0x04, //Player-to-NPC speaking(NPCs channel)
        PrivateNPCToPlayer = 0x05, //NPC-to-Player speaking
        Private = 0x06, //Players speaking privately to players
        ChannelYellow = 0x07,	//Yellow message in chat
        ChannelWhite = 0x08, //White message in chat
        RuleViolationReport = 0x09, //Reporting rule violation - Ctrl+R
        RuleViolationAnswer = 0x0A, //Answering report
        RuleViolationContinue = 0x0B, //Answering the answer of the report
        Broadcast = 0x0C,	//Broadcast a message - #b
        ChannelRed = 0x0D,	//Talk red on chat - #c
        PrivateRed = 0x0E,	//Red private - @name@ text
        ChannelOrange = 0x0F,	//Talk orange on text
        //SPEAK_                = 0x10, //?
        ChannelRedAnonymous = 0x11,	//Talk red anonymously on chat - #d
        //SPEAK_MONSTER_SAY12 = 0x12, //?????
        MonsterSay = 0x13,	//Talk orange
        MonsterYell = 0x14,	//Yell orange
    }

    public enum StatusMessage : byte
    {
        ConsoleRed = 0x12, //Red message in the console
        ConsoleOrange = 0x13, //Orange message in the console
        ConsoleOrange2 = 0x14, //Orange message in the console
        Warning = 0x15, //Red message in game window and in the console
        EventAdvance = 0x16, //White message in game window and in the console
        EventDefault = 0x17, //White message at the bottom of the game window and in the console
        StatusDefault = 0x18, //White message at the bottom of the game window and in the console
        DescriptionGreen = 0x19, //Green message in game window and in the console
        StatusSmall = 0x1A, //White message at the bottom of the game window"
        ConsoleBlue = 0x1B, //Blue message in the console
    }

    public enum IncomingPacketType : byte
    {
        // GameServer
        SelfAppear = 0x0A,
        GMAction = 0x0B,
        ErrorMessage = 0x14,
        FyiMessage = 0x15,
        WaitingList = 0x16,
        Ping = 0x1E,
        Death = 0x28,
        CanReportBugs = 0x32,
        MapDescription = 0x64,
        MoveNorth = 0x65,
        MoveEast = 0x66,
        MoveSouth = 0x67,
        MoveWest = 0x68,
        TileUpdate = 0x69,
        TileAddThing = 0x6A,
        TileTransformThing = 0x6B,
        TileRemoveThing = 0x6C,
        CreatureMove = 0x6D,
        ContainerOpen = 0x6E,
        ContainerClose = 0x6F,
        ContainerAddItem = 0x70,
        ContainerUpdateItem = 0x71,
        ContainerRemoveItem = 0x72,
        InventorySetSlot = 0x78,
        InventoryResetSlot = 0x79,
        ShopWindowOpen = 0x7A,
        ShopSaleGoldCount = 0x7B,
        ShopWindowClose = 0x7C,
        SafeTradeRequestAck = 0x7D,
        SafeTradeRequestNoAck = 0x7E,
        SafeTradeClose = 0x7F,
        WorldLight = 0x82,
        MagicEffect = 0x83,
        AnimatedText = 0x84,
        Projectile = 0x85,
        CreatureSquare = 0x86,
        CreatureHealth = 0x8C,
        CreatureLight = 0x8D,
        CreatureOutfit = 0x8E,
        CreatureSpeed = 0x8F,
        CreatureSkull = 0x90,
        CreatureShield = 0x91,
        ItemTextWindow = 0x96,
        HouseTextWindow = 0x97,
        PlayerStatus = 0xA0,
        PlayerSkillsUpdate = 0xA1,
        PlayerFlags = 0xA2,
        CancelTarget = 0xA3,
        CreatureSpeech = 0xAA,
        ChannelList = 0xAB,
        ChannelOpen = 0xAC,
        ChannelOpenPrivate = 0xAD,
        RuleViolationOpen = 0xAE,
        RuleViolationRemove = 0xAF,
        RuleViolationCancel = 0xB0,
        RuleViolationLock = 0xB1,
        PrivateChannelCreate = 0xB2,
        ChannelClosePrivate = 0xB3,
        TextMessage = 0xB4,
        PlayerWalkCancel = 0xB5,
        FloorChangeUp = 0xBE,
        FloorChangeDown = 0xBF,
        OutfitWindow = 0xC8,
        VipState = 0xD2,
        VipLogin = 0xD3,
        VipLogout = 0xD4,
        QuestList = 0xF0,
        QuestPartList = 0xF1,
        ShowTutorial = 0xDC,
        AddMapMarker = 0xDD,
    }

    public enum OutgoingPacketType : byte
    {
        LoginServerRequest = 0x01,
        GameServerRequest = 0x0A,
        Logout = 0x14,
        ItemMove = 0x78,
        ShopBuy = 0x7A,
        ShopSell = 0x7B,
        ShopClose = 0x7C,
        ItemUse = 0x82,
        ItemUseOn = 0x83,
        ItemRotate = 0x85,
        LookAt = 0x8C,
        PlayerSpeech = 0x96,
        ChannelList = 0x97,
        ChannelOpen = 0x98,
        ChannelClose = 0x99,
        Attack = 0xA1,
        Follow = 0xA2,
        CancelMove = 0xBE,
        ItemUseBattlelist = 0x84,
        ContainerClose = 0x87,
        ContainerOpenParent = 0x88,
        TurnUp = 0x6F,
        TurnRight = 0x70,
        TurnDown = 0x71,
        TurnLeft = 0x72,
        AutoWalk = 0x64,
        AutoWalkCancel = 0x69,
        MoveUp = 0x65,
        MoveRight = 0x66,
        MoveDown = 0x67,
        MoveLeft = 0x68,
        MoveUpRight = 0x6A,
        MoveDownRight = 0x6B,
        MoveDownLeft = 0x6C,
        MoveUpLeft = 0x6D,
        VipAdd = 0xDC,
        VipRemove = 0xDD,
        SetOutfit = 0xD3,
        Ping = 0x1E,
        FightModes = 0xA0,
        ContainerUpdate = 0xCA,
        TileUpdate = 0xC9,
        PrivateChannelOpen = 0x9A,
        NpcChannelClose = 0x9E,
    }

    /// <summary>
    /// Identifies the PipePacket by its type (3rd byte)
    /// </summary>
    public enum PipePacketType : byte
    {
        DefaultTemplate      = 0x00,
        HooksEnableDisable   = 0x01,
        SetConstant          = 0x02,
        DisplayText          = 0x03,
        RemoveText           = 0x04,
        RemoveAllText        = 0x05,
        DisplayCreatureText  = 0x06,
        RemoveCreatureText   = 0x07,
        UpdateCreatureText   = 0x08,
        AddContextMenu       = 0x09,
        RemoveContextMenu    = 0x0A,
        RemoveAllContextMenus= 0x0B,
        OnClickContextMenu   = 0x0C,
        UnloadDll            = 0x0D,
        HookReceivedPacket   = 0x0E,
        HookSentPacket       = 0x0F,
        HookSendToServer     = 0x10,
        EventTriggers        = 0x11,
        AddIcon              = 0x12,
        UpdateIcon           = 0x13,
        RemoveIcon           = 0x14,
        OnClickIcon          = 0x15,
        RemoveAllIcons       = 0x16,
        AddSkin              = 0x17,
        RemoveSkin           = 0x18,
        UpdateSkin           = 0x19,
        RemoveAllSkins       = 0x20
    }

    public enum PipeConstantType : byte
    {
        PrintName = 0x01,
        PrintFPS = 0x02,
        ShowFPS = 0x03,
        PrintTextFunc = 0x04,
        NopFPS = 0x05,
        AddContextMenuFunc = 0x06,
        OnClickContextMenu = 0x07,
        SetOutfitContextMenu = 0x08,
        PartyActionContextMenu = 0x09,
        CopyNameContextMenu = 0x0A,
        OnClickContextMenuVf = 0x0B,
        TradeWithContextMenu = 0x0C,
        Recv                 =0x0D,
        Send                 =0x0E,
        EventTrigger         =0x0F,
        LookContextMenu = 0x10,
        DrawItemFunc = 0x11,
        DrawSkinFunc = 0x12
    }
    
    /// <summary>
    /// Describes the packets destination
    /// </summary>
    public enum PacketDestination : byte
    {
        Client,
        Server,
        Pipe,
        None
    }

    public enum SquareColor : byte
    {
        Black = 0
    }

    #region Speech

    public enum ChatChannel : ushort
    {
        Guild = 0x00,
        Party = 0x01,
        //?Gamemaster = 0x01,
        Tutor = 0x02,
        RuleReport = 0x03,
        Game = 0x05,
        Trade = 0x06,
        TradeRook = 0x07,
        RealLife = 0x08,
        Help = 0x09,
        OwnPrivate = 0x0E,
        Custom = 0xA0,
        Custom1 = 0xA1,
        Custom2 = 0xA2,
        Custom3 = 0xA3,
        Custom4 = 0xA4,
        Custom5 = 0xA5,
        Custom6 = 0xA6,
        Custom7 = 0xA7,
        Custom8 = 0xA8,
        Custom9 = 0xA9,
        Private = 0xFFFF,
        None = 0xAAAA

    }

    #endregion

    public enum TextColor : byte
    {
        Blue = 5,
        Green = 30,
        LightBlue = 35,
        Crystal = 65,
        Purple = 83,
        Platinum = 89,
        LightGrey = 129,
        DarkRed = 144,
        Red = 180,
        Orange = 198,
        Gold = 210,
        White = 215,
        None = 255
    }

    public enum ProjectileType : byte
    {
        Spear = 0x01,
        Bolt = 0x02,
        Arrow = 0x03,
        Fire = 0x04,
        Energy = 0x05,
        PoisonArrow = 0x06,
        BurstArrow = 0x07,
        ThrowingStar = 0x08,
        ThrowingKnife = 0x09,
        SmallStone = 0x0A,
        Skull = 0x0B,
        BigStone = 0x0C,
        SnowBall = 0x0D,
        PowerBolt = 0x0E,
        SmallPoison = 0x0F,
        InfernalBolt = 0x10,
        HuntingSpear = 0x11,
        EnchantedSpear = 0x12,
        AssassinStar = 0x13,
        ViperStar = 0x14,
        RoyalSpear = 0x15,
        SniperArrow = 0x16,
        OnyxArrow = 0x17,
        EarthArrow = 0x18,
        NormalSword = 0x19,
        NormalAxe = 0x1A,
        NormalClub = 0x1B,
        EtherealSpear = 0x1C,
        Ice = 0x1D,
        Earth = 0x1E,
        Holy = 0x1F,
        Death = 0x20,
        FlashArrow = 0x21,
        FlamingArrow = 0x22,
        ShiverArrow = 0x23,
        EnergySmall = 0x24,
        IceSmall = 0x25,
        HolySmall = 0x26,
        EarthSmall = 0x27,
        EarthArrow2 = 0x28,
        Explosion = 0x29,
        Cake = 0x2A
    }

    public enum TileAnimationType
    {
        DrawBlood = 0x00,
        LoseEnergy = 0x01,
        Puff = 0x02,
        BlockHit = 0x03,
        ExplosionArea = 0x04,
        ExplosionDamage = 0x05,
        FireArea = 0x06,
        YellowRings = 0x07,
        PoisonRings = 0x08,
        HitArea = 0x09,
        Teleport = 0x0a,
        EnergyDamage = 0x0b,
        MagicEnergy = 0x0c,
        MagicBlood = 0x0d,
        MagicPoison = 0x0e,
        HitByFire = 0x0f,
        Poison = 0x10,
        MortArea = 0x11,
        SoundGreen = 0x12,
        SoundRed = 0x13,
        PoisonArea = 0x14,
        SoundYellow = 0x15,
        SoundPurple = 0x16,
        SoundBlue = 0x17,
        SoundWhite = 0x18,
        Bubbles = 0x19,
        Craps = 0x1a,
        GiftWraps = 0x1b,
        FireworkYellow = 0x1c,
        FireworkRed = 0x1d,
        FireworkBlue = 0x1e,
        Stun = 0x1f,
        Sleep = 0x20,
        WaterCreature = 0x21,
        Groundshaker = 0x22,
        Hearts = 0x23,
        FireAttack = 0x24,
        EnergyArea = 0x25,
        SmallClouds = 0x26,
        HolyDamage = 0x27,
        BigClouds = 0x28,
        IceArea = 0x29,
        IceTornado = 0x2a,
        IceAttack = 0x2b,
        Stones = 0x2c,
        SmallPlants = 0x2d,
        Carniphila = 0x2e,
        PurpleEnergy = 0x2f,
        YellowEnergy = 0x30,
        HolyArea = 0x31,
        BigPlants = 0x32,
        Cake = 0x33,
        GiantIce = 0x34,
        WaterSplash = 0x35,
        PlantAttack = 0x36,
        BlueArrow = 0x38,
        FlashSquare = 0x39  
    }

    public enum PartyShield
    {
        None = 0,
        Inviter = 1,
        Invitee = 2,
        Member = 3,
        Leader = 4,
        MemberSharedExp = 5,
        LeaderSharedExp = 6,
        MemberSharedExpInactive = 7,
        LeaderSharedExpInactive = 8,
        MemberShareExpAlone = 9,
        LeaderSharedExpAlone = 10
    }

    public class AvalibleOutfit
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public byte Addons { get; set; }

        public AvalibleOutfit() { }
    }

    public class ShopInfo
    {
        public ushort ItemId { get; set; }
        public byte SubType { get; set; }
        public uint BuyPrice { get; set; }
        public uint SellPrice { get; set; }
        public string ItemName { get; set; }
        public uint Weight { get; set; }

        public ShopInfo() { }

        public ShopInfo(ushort itemId, byte subType, uint buyPrice, uint sellPrice)
        {
            ItemId = itemId;
            SubType = subType;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }
    }

    public enum PacketCreatureType : byte
    {
        Known,
        Unknown,
        Turn
    }

    public class PacketCreature
    {
        public PacketCreatureType Type { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public byte Health { get; set; }
        public byte Direction { get; set; }
        public Objects.Outfit Outfit { get; set; }
        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }
        public ushort Speed { get; set; }
        public Constants.Skull Skull { get; set; }
        public PartyShield PartyShield { get; set; }
        public Constants.WarIcon WarIcon { get; set; }
        public uint RemoveId { get; set; }
        public Objects.Location Location { get; set; }
        public Objects.Client Client { get; set; }

        public PacketCreature(Objects.Client client)
        {
            Client = client;
        }
    }

    // (http://www.tpforums.org/forum/showthread.php?t=2399)
    /// <summary>
    /// Credits to Vitor for the EventTypes
    /// </summary>
    public enum EventType : byte
    {
        RegularDialog = 0x01,
        RegularDialog2 = 0x02,
        CharacterListLoading = 0x03,
        ConnectionToGameWorld = 0x04,
        LoginQueue = 0x05,
        Logout = 0x06,
        Exit = 0x07,
        EnterGame = 0x08,
        CharacterListLoading2 = 0x09,
        CharacterList = 0x0A,
        YouAreDead = 0x0B,
        LinkcopyWarning = 0x0C,
        AccountDataWarning = 0x0D,
        Undefined1 = 0x0E,
        Undefined2 = 0x0F,
        EditList = 0x10,
        SetOutfit = 0x11,
        BugReport = 0x12,
        ChannelList = 0x13,
        InvitePlayerPrivate = 0x14,
        ExcludePlayerPrivate = 0x15,
        IgnoreList = 0x16,
        RuleViolationReport = 0x17,
        AddToVip = 0x18,
        EditVip = 0x19,
        Undefined3 = 0x1A,
        Undefined4 = 0x1B,
        QuestLog = 0x1C,
        QuestLine = 0x1D,
        Info = 0x1E,
        GMRuleViolationPanel = 0x1F,
        EditMinimapMark = 0x20,
        EditMinimapMark2 = 0x21,
        HelpMenu = 0x22,
        TutorialHintsMenu = 0x23,
        OptionsMenu = 0x24,
        GraphicsOptionMenu = 0x25,
        AdvancedGraphicsOptionMenu = 0x26,
        ConsoleOptions = 0x27,
        Hotkey = 0x28,
        GeneralOptionsMenu = 0x29,
        MessageOfTheDay = 0x2A,
        DownloadClientUpdate = 0x2B,
        Undefined5 = 0x2C,
        Undefined6 = 0x2D,
        LastUsedHotkeyCrosshair = 0x2E,
        LastTradedItem = 0x2F,
        ClientHelp = 0x30,
        OpenPrivateChannelWithPlayer = 0x31,
        OpenChatChannel = 0x32,
        OpenChatChannel2 = 0x33,
        Undefined7 = 0x34,
        RuleViolationReportChannel = 0x35,
        OpenNPCsChannel = 0x36,
        Undefined8 = 0x37,
        Undefined9 = 0x38,
        NPCTrade = 0x39,
        Undefined10 = 0x3A,
        Undefined11 = 0x3B,
        Undefined12 = 0x3C,
        Undefined13 = 0x3D,
        TutorialHint = 0x3E,
        LastLookedItemContextMenu = 0x3F,
        AttackCreatureContextMenu = 0x40,
        AddToVipContextMenu = 0x41,
        CurrentSelectedChannelMessagesContextMenu = 0x42,
        CurrentSelectedChannelContextMenu = 0x43,
        EmptyContextMenu = 0x44,
        PasteContextMenu = 0x45,
        MinimapMark = 0x46,
        SkillsContextMenu = 0x47,
        NPCTradingItemsContextMenu = 0x48,
        ConnectToCharacterList = 0x49,
        ConnectToGameWorldUsingLastChosenCharacter = 0x4A,
        Undefined14 = 0x4B,
        RestartClientAfterPatchExecution = 0x53,
        UpdateClient = 0x54
    }


}
