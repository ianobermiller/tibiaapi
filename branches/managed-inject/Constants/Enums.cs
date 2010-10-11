using System.Collections.Generic;

namespace Tibia.Constants
{
    #region General

    public enum ObjectType
    {
        Memory,
        Packet
    }

    /// <summary>
    /// The direction to walk in or turn to.
    /// </summary>
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        UpRight = 5,
        DownRight = 6,
        DownLeft = 7,
        UpLeft = 8
    }

    /// <summary>
    /// The byte that is sent on RSA encrypted packets
    /// </summary>
    public enum OperatingSystem : byte
    {
        Linux = 1,
        Windows = 2
    }

    /// <summary>
    /// Different types of locations.
    /// </summary>
    public enum ItemLocationType
    {
        Ground,
        Slot,
        Container
    }

    /// <summary>
    /// The type of server.
    /// </summary>
    public enum ServerType
    {
        Real,
        OT
    }

    #endregion

    #region Client
    /// <summary>
    /// Formerly Cursor
    /// </summary>
    public enum ActionState : byte
    {
        None = 0,
        LeftClick = 1, // // left-click to walk or to use the client interface
        Left = LeftClick,    // walk etc
        RightClick = 2, // right-click to use an object such as a torch or an apple
        Right = RightClick,   // use
        InspectObject = 3, // left-click + right-click to see or inspect an object
        Inspect = InspectObject,
        MoveObject = 6, // dragging an object to move it to a new location
        Drag = MoveObject,
        UseObject = 7, // using an object such as a rope, a shovel, a fishing rod, or a rune
        Using = UseObject,   // in-use fishing shooting rune
        SelectHotkeyObject = 8, // selecting an object to bind to a hotkey from the "Hotkey Options" window
        TradeObject = 9, // using "Trade with..." on an object to select a player with whom to trade
        Trade = TradeObject,
        ClientHelp = 10, // // client mouse over tooltip help
        Help = ClientHelp,   // client help
        OpenDialogWindow = 11, // opening a dialog window such as the "Options" window, "Select Outfit" window, or "Move Objects" window
        PopupMenu = 12 // showing a popup menu with options such as "Invite to Party", "Set Outfit", "Copy Name", or "Set Mark"
    }

    public enum CurrentDialog
    {
        MoveObjects = 88
    }

    public enum LoginStatus : byte
    {
        LoggedOut = 0,
        NotLoggedIn = LoggedOut,
        LoggingIn = 6,
        LoggedIn = 8
    }

    public static class RSAKey
    {
        public static string OpenTibia = "109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413";
        public static string RealTibia = "132127743205872284062295099082293384952776326496165507967876361843343953435544496682053323833394351797728954155097012103928360786959821132214473291575712138800495033169914814069637740318278150290733684032524174782740134357629699062987023311132821016569775488792221429527047321331896351555606801473202394175817";
    }
    #endregion

    #region Player

    public enum Attack : byte
    {
        FullAttack = 1,
        Balance = 2,
        FullDefense = 3
    }

    public enum Follow : byte
    {
        DoNotFollow = 0,
        FollowClose = 1,
        FollowDistance = 2
    }

    public enum Flag
    {
        None = 0,
        Poisoned = 1,
        Burning = 2,
        Electrified = 4,
        Drunk = 8,
        ProtectedByMagicShield = 16,
        Paralysed = 32,
        Paralyzed = Paralysed,
        Hasted = 64,
        InBattle = 128,
        Drowning = 256,
        Freezing = 512,
        Dazzled = 1024,
        Cursed = 2048,
        Strengthened = 4096,
        CannotLogoutOrEnterProtectionZone = 8192,
        WithinProtectionZone = 16384
    }

    public enum SlotNumber
    {
        None = 0,
        Head = 1,
        Necklace = 2,
        Backpack = 3,
        Armor = 4,
        Right = 5,
        Left = 6,
        Legs = 7,
        Feet = 8,
        Ring = 9,
        Ammo = 10,
        First = Head,
        Last = Ammo
    }

    #endregion

    #region Outfits

    public enum OutfitAddon : byte
    {
        None = 0,
        Addon1 = 1,
        Addon2 = 2,
        Addon3 = 3,
        First = Addon1,
        Second = Addon2,
        Both = Addon3
    }

    // Not really an enum, because we want to allow any number for color.
    public static class OutfitColor
    {
        public static int Red = 94;
        public static int Orange = 77;
        public static int Yellow = 79;
        public static int Green = 82;
        public static int Blue = 88;
        public static int Purple = 90;
        public static int Brown = 116;
        public static int Black = 114;
        public static int White = 0;
        public static int Pink = 91;
        public static int Grey = 57;
        public static int Peach = 1;
    }

    public enum OutfitType
    {
        Invisible = 0,               // Stealth Ring Effect Also For Item As Outfit
        OrcWarlord = 2,
        WarWolf = 3,
        OrcRider = 4,
        Orc = 5,
        OrcShaman = 6,
        OrcWarrior = 7,
        OrcBerserker = 8,
        Necromancer = 9,
        ButterflyYellow = 10,
        WaterElemental = 11,
        DemonColor = 12,
        BlackSheep = 13,
        Sheep = 14,
        Troll = 15,
        Bear = 16,
        Beholder = 17,
        Ghoul = 18,
        Slime = 19,
        QuaraPredator = 20,
        Rat = 21,
        Cyclops = 22,
        MinotaurMage = 23,
        MinotaurArcher = 24,
        Minotaur = 25,
        Rotworm = 26,
        Wolf = 27,
        Snake = 28,
        MinotaurGuard = 29,
        Spider = 30,
        Deer = 31,
        Dog = 32,
        Skeleton = 33,
        Dragon = 34,
        Demon = 35,
        PoisonSpider = 36,
        DemonSkeleton = 37,
        GiantSpider = 38,
        DragonLord = 39,
        FireDevil = 40,
        Lion = 41,
        PolarBear = 42,
        Scorpion = 43,
        Wasp = 44,
        Bug = 45,
        QuaraConstrictor = 46,
        QuaraHydromancer = 47,
        Ghost = 48,
        FireElemental = 49,
        OrcSpearman = 50,
        GreenDjinn = 51,
        WinterWolf = 52,
        FrostTroll = 53,
        Witch = 54,
        Behemoth = 55,
        CaveRat = 56,
        Monk = 57,
        Priestess = 58,
        OrcLeader = 59,
        Pig = 60,
        Goblin = 61,
        Elf = 62,
        ElfArcanist = 63,
        ElfScout = 64,
        Mummy = 65,
        DwarfGeomancer = 66,
        StoneGolem = 67,
        Vampire = 68,
        Dwarf = 69,
        DwarfGuard = 70,
        DwarfSoldier = 71,
        QuaraMantassin = 72,
        Hero = 73,
        Rabbit = 74,
        GameMasterVoluntary = 75,
        SwampTroll = 76,
        QuaraPincher = 77,
        Banshee = 78,
        AncientScarab = 79,
        BlueDjinn = 80,
        Cobra = 81,
        Larva = 82,
        Scarab = 83,
        Pharaoh1 = 84,
        Pharaoh2 = 85,
        Pharaoh3 = 86,
        PharaohDressed1 = 87,
        PharaohDressed2 = 88,
        Pharaoh4 = 89,
        Pharaoh5 = 90,
        PharaohDressed3 = 91,
        Mimic = 92,
        PirateMarauder = 93,
        Hyaena = 94,
        Gargoyle = 95,
        PirateCutthroat = 96,
        PirateBuccaneer = 97,
        PirateCorsair = 98,
        Lich = 99,
        CryptShambler = 100,
        Bonebeast = 101,
        Deathslicer = 102,
        Efreet = 103,
        Marid = 104,
        Badger = 105,
        Skunk = 106,
        Demon2 = 107,
        ElderBeholder = 108,
        Gazer = 109,
        Yeti = 110,
        Chicken = 111,
        Crab = 112,
        LizardTemplar = 113,
        LizardSentinel = 114,
        LizardSnakecharmer = 115,
        Kongra = 116,
        Merlkin = 117,
        Sibang = 118,
        Crocodile = 119,
        Carniphila = 120,
        Hydra = 121,
        Bat = 122,
        Panda = 123,
        Centipede = 124,
        Tiger = 125,
        OldFemale = 126,
        OldMale = 127,
        CitizenMale = 128,
        HunterMale = 129,
        MageMale = 130,
        KnightMale = 131,
        NoblemanMale = 132,
        SummonerMale = 133,
        WarriorMale = 134,
        // Nothing = 135
        CitizenFemale = 136,
        HunterFemale = 137,
        SummonerFemale = 138,
        KnightFemale = 139,
        NoblemanFemale = 140,
        MageFemale = 141,
        WarriorFemale = 142,
        BarbarianMale = 143,
        DruidMale = 144,
        WizardMale = 145,
        OrientalMale = 146,
        BarbarianFemale = 147,
        DruidFemale = 148,
        WizardFemale = 149,
        OrientalFemale = 150,
        PirateMale = 151,
        AssassinMale = 152,
        BeggarMale = 153,
        ShamanMale = 154,
        PirateFemale = 155,
        AssassinFemale = 156,
        BeggarFemale = 157,
        ShamanFemale = 158,
        ElfColor = 159,
        DwarfColor = 160,
        // Nothing = 161-191
        CarrionWorm = 192,
        EnlightenedsOfTheCult = 193,
        AdeptsOfTheCult = 194,
        PirateSkeleton = 195,
        PirateGhost = 196,
        Tortoise = 197,
        ThornbackTortoise = 198,
        Mammoth = 199,
        BloodCrab = 200,
        Demon3 = 201,
        MinotaurGuard2 = 202,
        ElfArcanist2 = 203,
        DragonLord2 = 204,
        StoneGolem2 = 205,
        Monk2 = 206,
        MinotaurGuard3 = 207,
        GiantSpider2 = 208,
        Necromancer2 = 209,
        ElderBeholder2 = 210,
        Elephant = 211,
        Flamingo = 212,
        ButterflyPink = 213,
        DworcVoodoomaster = 214,
        DworcFleshhunter = 215,
        DworcVenomsniper = 216,
        Parrot = 217,
        TerrorBird = 218,
        Tarantula = 219,
        SerpentSpawn = 220,
        SpitNettle = 221,
        Toad = 222,
        Seagull = 223,
        AzureFrog = 224,
        DarkMonk = 225,
        FrogColor = 226,
        ButterflyBlue = 227,
        ButterflyRed = 228,
        Ferumbras = 229,
        HandOfCursedFate = 230,
        UndeadDragon = 231,
        LostSoul = 232,
        BetrayedWraith = 233,
        DarkTorturer = 234,
        Spectre = 235,
        Destroyer = 236,
        DiabloicImp = 237,
        Defiler = 238,
        Wyvern = 239,
        Hellhound = 240,
        Phantasm = 241,
        Hellfire = 242,
        HellfireFighter = 243,
        Juggernaut = 244,
        Nightmare = 245,
        Blightwalker = 246,
        Plaguesmith = 247,
        FrostDragon = 248,
        ChakoyaTribewarden = 249,
        Penguin = 250,
        NorsemanMale = 251,
        NorsemanFemale = 252,
        BarbarianHeadsplitter = 253,
        BarbarianSkullhunter = 254,
        BarbarianBloodwalker = 255,
        Braindeath = 256,
        FrostGiant = 257,
        Husky = 258,
        ChakoyaToolshaper = 259,
        ChakoyaWindcaller = 260,
        IceGolem = 261,
        SilverRabbit = 262,
        CrystalSpider = 263,
        BarbarianBrutetamer = 264,
        FrostGiantess = 265,
        GameMasterCustomerSupport = 266,
        Swimmer = 267,
        NightmareKnightMale = 268,
        NightmareKnightFemale = 269,
        JesterFemale = 270,
        DragonHatchling = 271,
        DragonLordHatchling = 272,
        JesterMale = 273,
        Squirrel = 274,
        SeaSerpent = 275,
        Cat = 276,
        CyclopsSmith = 277,
        BrotherhoodOfBonesMale = 278,
        BrotherhoodOfBonesFemale = 279,
        CyclopsDrone = 280,
        TrollChampion = 281,
        IslandTroll = 282,
        FrostDragonHatchling = 283,
        Cockroach = 284,
        EarthOverlord = 285,
        SlickWaterElemental = 286,
        TheCount = 287,
        DemonHunterFemale = 288,
        DemonHunterMale = 289,
        MassiveEnergyElemental = 290,
        Wyrm = 291,
        Pumpkin = 292,
        EnergyElemental = 293,
        Wisp = 294,
        RotwormQueen = 295,
        GoblinAssassin = 296,
        GoblinScavenger = 297,
        SkeletonWarrior = 298,
        BogRaider = 299,
        GrimReaper = 300,
        EarthElemental = 301,
        CommunityManager = 302,
        Unknown1 = 303,
        WorkerGolem = 304,
        MutatedRat = 305,
        UndeadGladiator = 306,
        MutatedBat = 307,
        Werewolf = 308,
        Azerus = 309,
        HauntedTreeling = 310,
        Zombie = 311,
        VampireBride = 312,
        Gozzler = 313,
        AcidBlob = 314,
        DeathBlob = 315,
        MercuryBlob = 316,
        YoungSeaSerpent = 317,
        MutatedTiger = 318,
        Unknown2 = 319,
        Nightstalker = 320,
        NightmareScion = 321,
        Hellspawn = 322,
        MutatedHuman = 323,
        YalaharianFemale = 324,
        YalaharianMale = 325,
        WarGolem = 326,
        WhiteFemale = 327,
        WeddingMale = 328,
        WeddingFemale = 329,
        Medusa = 330,
        Queen = 331,
        King = 332,
        SmallStoneGolem = 333
    }

    #endregion

    #region Creature

    public static class LightSize
    {
        public static int None = 0;
        public static int Torch = 7;
        public static int Full = 27;
    }

    public static class LightColor
    {
        public static int None = 0;
        public static int Default = 206; // default light color
        public static int Orange = Default;
        public static int White = 215;
    }

    public enum Skull : byte
    {
        None = 0,
        Yellow = 1,
        Green = 2,
        White = 3,
        Red = 4,
        Black = 5
    }

    public enum PartyShield
    {
        None = 0,
        Host = 1, // the host invites the guest to the party
        Inviter = Host,
        Guest = 2, // the guest joins the host at the party
        Invitee = Guest,
        Member = 3,
        Leader = 4,
        MemberSharedExp = 5,
        LeaderSharedExp = 6,
        MemberSharedExpInactive = 7,
        LeaderSharedExpInactive = 8

    }

    public enum CreatureType : byte
    {
        Player = 0x0,
        Target = 0x1,
        NPC = 0x40
    }

    public enum WarIcon
    {
        None = 0,
        Green = 1,
        Red = 2,
        Blue = 3
    }

    #endregion

    #region Spells

    public enum SpellCategory
    {
        Attack,
        Healing,
        Summon,
        Supply,
        Support
    }

    public enum SpellType
    {
        Instant,
        ItemTransformation,
        Creation
    }

    #endregion

    #region VipList

    public enum VipStatus
    {
        Offline = 0,
        Online = 1
    }
    public enum VipIcon
    {
        Blank = 0,
        Heart = 1,
        Skull = 2,
        Lightning = 3,
        Crosshair = 4,
        Star = 5,
        YinYang = 6,
        Triangle = 7,
        X = 8,
        Dollar = 9,
        Cross = 10
    }

    #endregion

    #region Hotkeys

    public enum HotkeyObjectUseType
    {
        WithCrosshairs = 0,
        UseOnTarget = 1,
        UseOnSelf = 2
    }

    #endregion

    #region Text Display
    public enum ClientFont : int
    {
        Normal = 1,
        NormalBorder = 2,
        Small = 3,
        Weird = 4
    }
    #endregion


    public enum SkinType : int
    {
        Pointer = 1,
        ResizeVertical = 2,
        Wait = 3,
        Crosshairs = 4,
        Crosshairs2 = 5,
        Help = 6,
        Background = 7,
        LightGrayVertical = 8,
        DarkGrayVertical = 9,
        LightGrayHorizontal = 10,
        DarkGrayHorizontal = 11,
        ButtonUp = 12,
        ButtonDown = 13
    }

    public enum ContextMenuType : byte
    {
        AllMenus = 0x00,
        SetOutfitContextMenu = 0x01,
        PartyActionContextMenu = 0x02,
        CopyNameContextMenu = 0x03,
        TradeWithContextMenu = 0x04,
        LookContextMenu = 0x05
    }

    public class SpeechTypeInfo
    {
        public readonly SpeechType SpeechType;
        public readonly byte Value;
        public readonly AdditionalSpeechData AdditionalSpeechData;

        public SpeechTypeInfo(SpeechType speechType, byte value, AdditionalSpeechData data)
        {
            this.SpeechType = speechType;
            this.Value = value;
            this.AdditionalSpeechData = data;
        }
    }

    public enum AdditionalSpeechData
    {
        None,
        Location,
        ChannelId,
        Time
    }

    public static class Enums
    {
        private static Dictionary<byte, SpeechTypeInfo> valueToSpeechTypeInfo861 = new Dictionary<byte, SpeechTypeInfo>();
        private static Dictionary<SpeechType, SpeechTypeInfo> enumToSpeechTypeInfo861 = new Dictionary<SpeechType, SpeechTypeInfo>()
        {
            { SpeechType.Say, new SpeechTypeInfo(SpeechType.Say, 0x01, AdditionalSpeechData.Location) },
            { SpeechType.Whisper, new SpeechTypeInfo(SpeechType.Whisper, 0x02, AdditionalSpeechData.Location) },
            { SpeechType.Yell, new SpeechTypeInfo(SpeechType.Yell, 0x03, AdditionalSpeechData.Location) },
            { SpeechType.PrivatePlayerToNPC, new SpeechTypeInfo(SpeechType.PrivatePlayerToNPC, 0x04, AdditionalSpeechData.None) },
            { SpeechType.PrivateNPCToPlayer, new SpeechTypeInfo(SpeechType.PrivateNPCToPlayer, 0x05, AdditionalSpeechData.Location) },
            { SpeechType.Private, new SpeechTypeInfo(SpeechType.Private, 0x06, AdditionalSpeechData.None) },
            { SpeechType.ChannelYellow, new SpeechTypeInfo(SpeechType.ChannelYellow, 0x07, AdditionalSpeechData.ChannelId) },
            { SpeechType.ChannelWhite, new SpeechTypeInfo(SpeechType.ChannelWhite, 0x08, AdditionalSpeechData.ChannelId) },
            { SpeechType.RuleViolationReport, new SpeechTypeInfo(SpeechType.RuleViolationReport, 0x09, AdditionalSpeechData.Time) },
            { SpeechType.ChannelRed, new SpeechTypeInfo(SpeechType.ChannelRed, 0x0A, AdditionalSpeechData.ChannelId) },
            { SpeechType.RuleViolationContinue, new SpeechTypeInfo(SpeechType.RuleViolationContinue, 0x0B, AdditionalSpeechData.None) },
            { SpeechType.ChannelOrange, new SpeechTypeInfo(SpeechType.ChannelOrange, 0x0C, AdditionalSpeechData.ChannelId) },
            { SpeechType.MonsterSay, new SpeechTypeInfo(SpeechType.MonsterSay, 0x0D, AdditionalSpeechData.Location) },
        };

        private static Dictionary<byte, SpeechTypeInfo> valueToSpeechTypeInfoPre861 = new Dictionary<byte, SpeechTypeInfo>();
        private static Dictionary<SpeechType, SpeechTypeInfo> enumToSpeechTypeInfoPre861 = new Dictionary<SpeechType, SpeechTypeInfo>()
        {
            { SpeechType.Say, new SpeechTypeInfo(SpeechType.Say, 0x01, AdditionalSpeechData.Location) },
            { SpeechType.Whisper, new SpeechTypeInfo(SpeechType.Whisper, 0x02, AdditionalSpeechData.Location) },
            { SpeechType.Yell, new SpeechTypeInfo(SpeechType.Yell, 0x03, AdditionalSpeechData.Location) },
            { SpeechType.PrivatePlayerToNPC, new SpeechTypeInfo(SpeechType.PrivatePlayerToNPC, 0x04, AdditionalSpeechData.None) },
            { SpeechType.PrivateNPCToPlayer, new SpeechTypeInfo(SpeechType.PrivateNPCToPlayer, 0x05, AdditionalSpeechData.Location) },
            { SpeechType.Private, new SpeechTypeInfo(SpeechType.Private, 0x06, AdditionalSpeechData.None) },
            { SpeechType.ChannelYellow, new SpeechTypeInfo(SpeechType.ChannelYellow, 0x07, AdditionalSpeechData.ChannelId) },
            { SpeechType.ChannelWhite, new SpeechTypeInfo(SpeechType.ChannelWhite, 0x08, AdditionalSpeechData.ChannelId) },
            { SpeechType.RuleViolationReport, new SpeechTypeInfo(SpeechType.RuleViolationReport, 0x09, AdditionalSpeechData.Time) },
            { SpeechType.RuleViolationAnswer, new SpeechTypeInfo(SpeechType.RuleViolationAnswer, 0x0A, AdditionalSpeechData.None) },
            { SpeechType.RuleViolationContinue, new SpeechTypeInfo( SpeechType.RuleViolationContinue, 0x0B, AdditionalSpeechData.None) },
            { SpeechType.Broadcast, new SpeechTypeInfo(SpeechType.Broadcast, 0x0C, AdditionalSpeechData.None) },
            { SpeechType.ChannelRed, new SpeechTypeInfo(SpeechType.ChannelRed, 0x0D, AdditionalSpeechData.ChannelId) },
            { SpeechType.PrivateRed, new SpeechTypeInfo(SpeechType.PrivateRed, 0x0E, AdditionalSpeechData.None) },
            { SpeechType.ChannelOrange, new SpeechTypeInfo(SpeechType.ChannelOrange, 0x0F, AdditionalSpeechData.ChannelId) },
            { SpeechType.ChannelRedAnonymous, new SpeechTypeInfo(SpeechType.ChannelRedAnonymous, 0x11, AdditionalSpeechData.ChannelId) },
            { SpeechType.MonsterSay, new SpeechTypeInfo(SpeechType.MonsterSay, 0x13, AdditionalSpeechData.Location) },
            { SpeechType.MonsterYell, new SpeechTypeInfo(SpeechType.MonsterYell, 0x14, AdditionalSpeechData.Location) },
        };

        static Enums()
        {
            enumToSpeechTypeInfo861.Values.Foreach(s => valueToSpeechTypeInfo861.Add(s.Value, s));
            enumToSpeechTypeInfoPre861.Values.Foreach(s => valueToSpeechTypeInfoPre861.Add(s.Value, s));
        }

        public static SpeechTypeInfo GetSpeechTypeInfo(ushort version, byte value)
        {
            if (version >= 861)
            {
                return valueToSpeechTypeInfo861[value];
            }
            else
            {
                return valueToSpeechTypeInfoPre861[value];
            }
        }

        public static SpeechTypeInfo GetSpeechTypeInfo(ushort version, SpeechType speechType)
        {
            if (version >= 861)
            {
                return enumToSpeechTypeInfo861[speechType];
            }
            else
            {
                return enumToSpeechTypeInfoPre861[speechType];
            }
        }
    }

    public enum SpeechType
    {
        Say,
        Whisper,
        Yell,
        PrivatePlayerToNPC,
        PrivateNPCToPlayer,
        Private,
        ChannelYellow,
        ChannelWhite,
        ChannelOrange,
        ChannelRed,
        ChannelRedAnonymous,
        RuleViolationReport,
        RuleViolationAnswer,
        RuleViolationContinue,
        MonsterSay,
        // Old
        Broadcast,
        MonsterYell,
        PrivateRed
    }

    public enum TextMessageColor : byte
    {
        YellowConsole = 1, // 2, 3
        PurpleConsole = 4, // 21
        TealConsole = 5, // 6
        RedServerLog = 9, // 11
        OrangeConsole = 13, // 14
        RedCenterGameWindowAndServerLog = 15,
        WhiteServerLogAndCenterGameWindow = 16,
        WhiteServerLogAndBottomGameWindow = 17,
        WhiteBottomGameWindow = 18, // 20
        GreenCenterGameWindow = 19,
        RedConsole = 22, // 23
        // None: 7, 8, 10, 12
    }

    public enum SquareColor : byte
    {
        Black = 0,
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
    }/// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        // Pipe
        PipePacket = 0xFF
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
        DefaultTemplate = 0x00,
        HooksEnableDisable = 0x01,
        SetConstant = 0x02,
        DisplayText = 0x03,
        RemoveText = 0x04,
        RemoveAllText = 0x05,
        DisplayCreatureText = 0x06,
        RemoveCreatureText = 0x07,
        UpdateCreatureText = 0x08,
        AddContextMenu = 0x09,
        RemoveContextMenu = 0x0A,
        RemoveAllContextMenus = 0x0B,
        OnClickContextMenu = 0x0C,
        UnloadDll = 0x0D,
        HookReceivedPacket = 0x0E,
        HookSentPacket = 0x0F,
        HookSendToServer = 0x10,
        EventTriggers = 0x11,
        AddIcon = 0x12,
        UpdateIcon = 0x13,
        RemoveIcon = 0x14,
        OnClickIcon = 0x15,
        RemoveAllIcons = 0x16,
        AddSkin = 0x17,
        RemoveSkin = 0x18,
        UpdateSkin = 0x19,
        RemoveAllSkins = 0x20,
        HookSendToClient = 0x21,
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

    public enum PacketCreatureType : byte
    {
        Known,
        Unknown,
        Turn
    }
}
