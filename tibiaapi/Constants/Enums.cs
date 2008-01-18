using System;
using System.Collections.Generic;

// Enumerations

namespace Tibia.Constants
{
    #region General

    public enum WalkDirection
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

    public enum TurnDirection : byte
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    /// <summary>
    /// Different types of locations.
    /// </summary>
    public enum ItemLocationType
    {
        GROUND,
        SLOT,
        CONTAINER
    }

    #endregion

    #region Client
    public enum Cursor : byte
    {
        None = 0,
        Left = 1,    // walk etc
        Right = 2,   // use
        Inspect = 3,
        Drag = 6,
        Using = 7,   // in-use fishing shooting rune
        Help = 10   // client help
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
        Holy = 1024,
        Thunder = 2048,
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

        // Orcs
        OrcWarlord = 2,
        WarWolf = 3,
        OrcRider = 4,
        Orc = 5,
        OrcShaman = 6,
        OrcWarrior = 7,
        OrcBerserker = 8,
        OrcSpearman = 50,
        OrcLeader = 59,

        Necromancer = 9,

        // Butterflies
        ButterflyYellow = 10,
        ButterflyPink = 213,
        ButterflyBlue = 227,
        ButterflyRed = 228,

        // Elementals
        WaterElemental = 11,
        FireElemental = 49,

        // Demons
        DemonColor = 12,
        Demon = 35, // Standard Demon
        Demon2 = 107,
        Demon3 = 201,

        BlackSheep = 13,
        Sheep = 14,
        Troll = 15,
        Bear = 16,
        Beholder = 17,
        Ghoul = 18,
        Slime = 19,

        // Quara
        QuaraPredator = 20,
        QuaraConstrictor = 46,
        QuaraHydromancer = 47,
        QuaraMantassin = 72,
        QuaraPincher = 77,

        Rat = 21,
        Cyclops = 22,

        // Minotaur
        MinotaurMage = 23,
        MinotaurArcher = 24,
        Minotaur = 25,
        MinotaurGuard = 29,
        MinotaurGuard2 = 202,
        MinotaurGuard3 = 207,

        Rotworm = 26,
        Wolf = 27,
        Snake = 28,
        // Spiders
        Spider = 30,
        PoisonSpider = 36,
        GiantSpider = 38,
        GiantSpider2 = 208,
        Tarantula = 219,
        CrystalSpider = 263,

        Deer = 31,
        Dog = 32,
        Skeleton = 33,

        // Dragons
        Dragon = 34,
        DragonLord = 39,
        DragonLord2 = 204,
        UndeadDragon = 231,
        FrostDragon = 248,

        DemonSkeleton = 37,
        FireDevil = 40,
        Lion = 41,
        PolarBear = 42,
        Scorpion = 43,
        Wasp = 44,
        Bug = 45,
        Ghost = 48,

        // Djinn
        GreenDjinn = 51,
        BlueDjinn = 80,
        Efreet = 103,
        Marid = 104,

        WinterWolf = 52,
        FrostTroll = 53,
        Witch = 54,
        Behemoth = 55,
        CaveRat = 56,
        Monk = 57,
        Priestess = 58,
        Pig = 60,
        Goblin = 61,

        // Elves
        Elf = 62,
        ElfArcanist = 63,
        ElfScout = 64,
        ElfColor = 159,
        ElfArcanist2 = 203,

        Mummy = 65,
        StoneGolem = 67,
        Vampire = 68,

        // Dwarves
        Dwarf = 69,
        DwarfGuard = 70,
        DwarfSoldier = 71,
        DwarfGeomancer = 66,
        DwarfColor = 160,

        Hero = 73,
        Rabbit = 74,
        GameMaster = 75,
        SwampTroll = 76,
        Banshee = 78,
        AncientScarab = 79,

        Cobra = 81,
        Larva = 82,
        Scarab = 83,

        // Pharaohs
        Pharaoh1 = 84,
        Pharaoh2 = 85,
        Pharaoh3 = 86,
        PharaohDressed1 = 87,
        PharaohDressed2 = 88,
        Pharaoh4 = 89,
        Pharaoh5 = 90,
        PharaohDressed3 = 91,

        Mimic = 92,
        Hyaena = 94,
        Gargoyle = 95,

        // Pirates
        PirateCutthroat = 96,
        PirateBuccaneer = 97,
        PirateCorsair = 98,
        PirateMarauder = 93,
        PirateSkeleton = 195,
        PirateGhost = 196,

        Lich = 99,
        CryptShambler = 100,
        Bonebeast = 101,
        Deathslicer = 102,
        Badger = 105,
        Skunk = 106,
        ElderBeholder = 108,
        Gazer = 109,
        Yeti = 110,
        Chicken = 111,
        Crab = 112,

        // Lizards
        LizardTemplar = 113,
        LizardSentinel = 114,
        LizardSnakecharmer = 115,

        // Apes
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

        // Human Outfits
        OldFemale = 126,
        OldMale = 127,

        CitizenMale = 128,
        HunterMale = 129,
        MageMale = 130,
        KnightMale = 131,
        NoblemanMale = 132,
        SummonerMale = 133,
        WarriorMale = 134,

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

        BrotherhoodMale = 278,
        BrotherhoodFemale = 279,

        NightmareKnightMale = 268,
        NightmareKnightFemale = 269,

        JesterMale = 273,
        JesterFemale=274,

        CarrionWorm = 192,

        // Cult
        EnlightenedsOfTheCult = 193,
        AdeptsOfTheCult = 194,

        Tortoise = 197,
        ThornbackTortoise = 198,
        Mammoth = 199,
        BloodCrab = 200,
        StoneGolem2 = 205,
        Monk2 = 206,
        Necromancer2 = 209,
        ElderBeholder2 = 210,
        Elephant = 211,
        Flamingo = 212,

        // Dworcs
        DworcVoodoomaster = 214,
        DworcFleshhunter = 215,
        DworcVenomsniper = 216,

        Parrot = 217,
        TerrorBird = 218,
        SerpentSpawn = 220,
        SpitNettle = 221,
        Toad = 222,
        Seagull = 223,
        AzureFrog = 224,
        FrogColor = 226,
        Ferumbras = 229,
        HandOfCursedFate = 230,
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
        Penguin = 250,
        NorsemanMale = 251,
        NorsemanFemale = 252,

        // Barbarians
        BarbarianHeadsplitter = 253,
        BarbarianSkullhunter = 254,
        BarbarianBloodwalker = 255,
        BarbarianBrutetamer = 264,

        Braindeath = 256,
        FrostGiant = 257,
        Husky = 258,

        // Chakoya
        ChakoyaTribewarden = 249,
        ChakoyaToolshaper = 259,
        ChakoyaWindcaller = 260,

        IceGolem = 261,
        SilverRabbit = 262,
        FrostGiantess = 265,
        CommunityManager = 266

        
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

    public enum Skull
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
        Leader = 4
    }

    public enum CreatureType : byte
    {
        Player = 0,
        Target = 1,
        NPC = 64
    }

    #endregion

    #region Speech

    public enum SpeechChannel
    {
        None = -1,
        Guild = 0,
        Game = 4,
        Trade = 5,
        RealLife = 6,
        Help = 7,
        OwnPrivate = 14,
        Private1 = 17
    }

    public enum SpeechType
    {
        Normal = 1,
        Whisper = 2,
        Yell = 3,
        PrivateMessage = 4,
        Channel = 5
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
        Rune
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
}
