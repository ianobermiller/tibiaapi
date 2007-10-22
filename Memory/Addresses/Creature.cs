namespace Tibia.Memory.Addresses
{
    /// <summary>
    /// Distances and enumerations for creatures.
    /// </summary>
    public static class Creature
    {
        public static uint Distance_Id = 0;
        public static uint Distance_Type = 3;
        public static uint Distance_Name = 4;

        public static uint Distance_X = 36;
        public static uint Distance_Y = 40;
        public static uint Distance_Z = 44;

        public static uint Distance_ScreenOffsetHoriz = 48;
        public static uint Distance_ScreenOffsetVert = 52;

        public static uint Distance_IsWalking = 76;
        public static uint Distance_WalkSpeed = 140;
        public static uint Distance_Direction = 80;
        public static uint Distance_IsVisible = 144;
        public static uint Distance_BlackSquare = 128; // Is attacking player

        public static uint Distance_Light = 120;
        public static uint Distance_LightColor = 124;
        public static uint Distance_HPBar = 136;

        public static uint Distance_Skull = 148;
        public static uint Distance_Party = 152;

        public static uint Distance_Outfit = 96;
        public static uint Distance_Color_Head = 100;
        public static uint Distance_Color_Body = 104;
        public static uint Distance_Color_Legs = 108;
        public static uint Distance_Color_Feet = 112;
        public static uint Distance_Addon = 116;
        
        public static class Outfit_t
        {
            public static class Color
            {
                public static uint Red = 94;
                public static uint Orange = 77;
                public static uint Yellow = 79;
                public static uint Green = 82;
                public static uint Blue = 88;
                public static uint Purple = 90;
                public static uint Brown = 116;
                public static uint Black = 114;
                public static uint White = 0;
                public static uint Pink = 91;
                public static uint Grey = 57;
                public static uint Peach = 1;
            }

            public enum Addon : byte
            {
                None = 0,
                Addon1 = 1,
                Addon2 = 2,
                Both = 3
            }

            public enum Type
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
        }

        public static class Light_t
        {
            public static uint None = 0;
            public static uint Torch = 7;
            public static uint Full = 27;

            public static class Color
            {
                public static uint None = 0;
                public static uint Orange = 206;  // default light color
                public static uint White = 215;
            }
        }

        public enum Direction_t : byte
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        public enum Skull_t
        {
            None = 0,
            Yellow = 1,
            Green = 2,
            White = 3,
            Red = 4
        }

        public enum Party_t
        {
            None = 0,
            Inviter = 1,
            Invitee = 2,
            Member = 3,
            Leader = 4
        }

        public enum CreatureType_t : byte
        {
            Player = 0,
            NPC = 64
        }
    }
}
