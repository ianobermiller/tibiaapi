using System;
using System.Collections.Generic;

// Enumerations

namespace Tibia.Constants
{
    #region General

    public enum ObjectType
    {
        Memory,
        Packet,
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
    public enum OperationalSystem : byte
    {
        Linux = 1,
        Windows = 2
    };
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
        Left = 1,    // walk etc
        Right = 2,   // use
        Inspect = 3,
        Drag = 6,
        Using = 7,   // in-use fishing shooting rune
        Trade = 9,
        Help = 10,   // client help
        PopupMenu = 12
    }

    public enum Window
    {
        MoveObjects = 88
    }

    public enum LoginStatus : byte
    {
        NotLoggedIn = 0,
        LoggingIn = 6,
        LoggedIn = 8
    }

    public static class RSAKey
    {
        public static string OpenTibia = "109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413";
        public static string RealTibia = "124710459426827943004376449897985582167801707960697037164044904862948569380850421396904597686953877022394604239428185498284169068581802277612081027966724336319448537811441719076484340922854929273517308661370727105382899118999403808045846444647284499123164879035103627004668521005328367415259939915284902061793";
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
        Poisoned = 1,
        Burning = 2,
        Energized = 4,
        Drunk = 8,
        MagicShield = 16,
        Paralyzed = 32,
        Hasted = 64,
        Battle = 128,
        Drowning = 256,
        Frozen = 512,
        Dazzled = 1024,
        Cursed = 2048,
        None = 0
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
        Ammo = 10
    }

    #endregion

    #region Outfits

    public enum OutfitAddon : byte
    {
        None = 0,
        Addon1 = 1,
        Addon2 = 2,
        Both = 3
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
        WarGolem = 326
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
        public static int Orange = 206;  // default light color
        public static int White = 215;
    }

    public enum Skull : byte
    {
        None = 0,
        Yellow = 1,
        Green = 2,
        White = 3,
        Red = 4
    }

    public enum Party
    {
        None = 0,
        Inviter = 1,
        Invitee = 2,
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
        Online = 1,
        Offline = 0
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
        Cross = 10,

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
    }
    #endregion

    public enum ContextMenuType : byte
    {
        AllMenus = 0x00,
        SetOutfitContextMenu = 0x01,
        PartyActionContextMenu = 0x02,
        CopyNameContextMenu = 0x03,
        TradeWithContextMenu = 0x04,
    }
}
