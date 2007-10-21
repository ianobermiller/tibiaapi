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

            public static class Addon
            {
                public static uint None = 0;
                public static uint Addon1 = 1;
                public static uint Addon2 = 2;
                public static uint Both = 3;
            }

            public static class Type
            {
                public static uint Invisible = 0;               // Stealth Ring Effect Also For Item As Outfit

                // Orcs
                public static uint OrcWarlord = 2;
                public static uint WarWolf = 3;
                public static uint OrcRider = 4;
                public static uint Orc = 5;
                public static uint OrcShaman = 6;
                public static uint OrcWarrior = 7;
                public static uint OrcBerserker = 8;
                public static uint OrcSpearman = 50;
                public static uint OrcLeader = 59;

                public static uint Necromancer = 9;

                // Butterflies
                public static uint ButterflyYellow = 10;
                public static uint ButterflyPink = 213;
                public static uint ButterflyBlue = 227;
                public static uint ButterflyRed = 228;

                // Elementals
                public static uint WaterElemental = 11;
                public static uint FireElemental = 49;

                // Demons
                public static uint DemonColor = 12;
                public static uint Demon = 35; // Standard Demon
                public static uint Demon2 = 107;
                public static uint Demon3 = 201;

                public static uint BlackSheep = 13;
                public static uint Sheep = 14;
                public static uint Troll = 15;
                public static uint Bear = 16;
                public static uint Beholder = 17;
                public static uint Ghoul = 18;
                public static uint Slime = 19;

                // Quara
                public static uint QuaraPredator = 20;
                public static uint QuaraConstrictor = 46;
                public static uint QuaraHydromancer = 47;
                public static uint QuaraMantassin = 72;
                public static uint QuaraPincher = 77;

                public static uint Rat = 21;
                public static uint Cyclops = 22;

                // Minotaur
                public static uint MinotaurMage = 23;
                public static uint MinotaurArcher = 24;
                public static uint Minotaur = 25;
                public static uint MinotaurGuard = 29;
                public static uint MinotaurGuard2 = 202;
                public static uint MinotaurGuard3 = 207;

                public static uint Rotworm = 26;
                public static uint Wolf = 27;
                public static uint Snake = 28;
                // Spiders
                public static uint Spider = 30;
                public static uint PoisonSpider = 36;
                public static uint GiantSpider = 38;
                public static uint GiantSpider2 = 208;
                public static uint Tarantula = 219;
                public static uint CrystalSpider = 263;

                public static uint Deer = 31;
                public static uint Dog = 32;
                public static uint Skeleton = 33;

                // Dragons
                public static uint Dragon = 34;
                public static uint DragonLord = 39;
                public static uint DragonLord2 = 204;
                public static uint UndeadDragon = 231;
                public static uint FrostDragon = 248;

                public static uint DemonSkeleton = 37;
                public static uint FireDevil = 40;
                public static uint Lion = 41;
                public static uint PolarBear = 42;
                public static uint Scorpion = 43;
                public static uint Wasp = 44;
                public static uint Bug = 45;
                public static uint Ghost = 48;

                // Djinn
                public static uint GreenDjinn = 51;
                public static uint BlueDjinn = 80;
                public static uint Efreet = 103;
                public static uint Marid = 104;

                public static uint WinterWolf = 52;
                public static uint FrostTroll = 53;
                public static uint Witch = 54;
                public static uint Behemoth = 55;
                public static uint CaveRat = 56;
                public static uint Monk = 57;
                public static uint Priestess = 58;
                public static uint Pig = 60;
                public static uint Goblin = 61;

                // Elves
                public static uint Elf = 62;
                public static uint ElfArcanist = 63;
                public static uint ElfScout = 64;
                public static uint ElfColor = 159;
                public static uint ElfArcanist2 = 203;

                public static uint Mummy = 65;
                public static uint StoneGolem = 67;
                public static uint Vampire = 68;

                // Dwarves
                public static uint Dwarf = 69;
                public static uint DwarfGuard = 70;
                public static uint DwarfSoldier = 71;
                public static uint DwarfGeomancer = 66;
                public static uint DwarfColor = 160;

                public static uint Hero = 73;
                public static uint Rabbit = 74;
                public static uint GameMaster = 75;
                public static uint SwampTroll = 76;
                public static uint Banshee = 78;
                public static uint AncientScarab = 79;

                public static uint Cobra = 81;
                public static uint Larva = 82;
                public static uint Scarab = 83;

                // Pharaohs
                public static uint Pharaoh1 = 84;
                public static uint Pharaoh2 = 85;
                public static uint Pharaoh3 = 86;
                public static uint PharaohDressed1 = 87;
                public static uint PharaohDressed2 = 88;
                public static uint Pharaoh4 = 89;
                public static uint Pharaoh5 = 90;
                public static uint PharaohDressed3 = 91;

                public static uint Mimic = 92;
                public static uint Hyaena = 94;
                public static uint Gargoyle = 95;

                // Pirates
                public static uint PirateCutthroat = 96;
                public static uint PirateBuccaneer = 97;
                public static uint PirateCorsair = 98;
                public static uint PirateMarauder = 93;
                public static uint PirateSkeleton = 195;
                public static uint PirateGhost = 196;

                public static uint Lich = 99;
                public static uint CryptShambler = 100;
                public static uint Bonebeast = 101;
                public static uint Deathslicer = 102;
                public static uint Badger = 105;
                public static uint Skunk = 106;
                public static uint ElderBeholder = 108;
                public static uint Gazer = 109;
                public static uint Yeti = 110;
                public static uint Chicken = 111;
                public static uint Crab = 112;

                // Lizards
                public static uint LizardTemplar = 113;
                public static uint LizardSentinel = 114;
                public static uint LizardSnakecharmer = 115;

                // Apes
                public static uint Kongra = 116;
                public static uint Merlkin = 117;
                public static uint Sibang = 118;

                public static uint Crocodile = 119;
                public static uint Carniphila = 120;
                public static uint Hydra = 121;
                public static uint Bat = 122;
                public static uint Panda = 123;
                public static uint Centipede = 124;
                public static uint Tiger = 125;

                // Human Outfits
                public static uint OldFemale = 126;
                public static uint OldMale = 127;

                public static uint CitizenMale = 128;
                public static uint HunterMale = 129;
                public static uint MageMale = 130;
                public static uint KnightMale = 131;
                public static uint NoblemanMale = 132;
                public static uint SummonerMale = 133;
                public static uint WarriorMale = 134;

                public static uint CitizenFemale = 136;
                public static uint HunterFemale = 137;
                public static uint SummonerFemale = 138;
                public static uint KnightFemale = 139;
                public static uint NoblemanFemale = 140;
                public static uint MageFemale = 141;
                public static uint WarriorFemale = 142;

                public static uint BarbarianMale = 143;
                public static uint DruidMale = 144;
                public static uint WizardMale = 145;
                public static uint OrientalMale = 146;

                public static uint BarbarianFemale = 147;
                public static uint DruidFemale = 148;
                public static uint WizardFemale = 149;
                public static uint OrientalFemale = 150;

                public static uint PirateMale = 151;
                public static uint AssassinMale = 152;
                public static uint BeggarMale = 153;
                public static uint ShamanMale = 154;

                public static uint PirateFemale = 155;
                public static uint AssassinFemale = 156;
                public static uint BeggarFemale = 157;
                public static uint ShamanFemale = 158;

                public static uint CarrionWorm = 192;

                // Cult
                public static uint EnlightenedsOfTheCult = 193;
                public static uint AdeptsOfTheCult = 194;

                public static uint Tortoise = 197;
                public static uint ThornbackTortoise = 198;
                public static uint Mammoth = 199;
                public static uint BloodCrab = 200;
                public static uint StoneGolem2 = 205;
                public static uint Monk2 = 206;
                public static uint Necromancer2 = 209;
                public static uint ElderBeholder2 = 210;
                public static uint Elephant = 211;
                public static uint Flamingo = 212;

                // Dworcs
                public static uint DworcVoodoomaster = 214;
                public static uint DworcFleshhunter = 215;
                public static uint DworcVenomsniper = 216;

                public static uint Parrot = 217;
                public static uint TerrorBird = 218;
                public static uint SerpentSpawn = 220;
                public static uint SpitNettle = 221;
                public static uint Toad = 222;
                public static uint Seagull = 223;
                public static uint AzureFrog = 224;
                public static uint FrogColor = 226;
                public static uint Ferumbras = 229;
                public static uint HandOfCursedFate = 230;
                public static uint LostSoul = 232;
                public static uint BetrayedWraith = 233;
                public static uint DarkTorturer = 234;
                public static uint Spectre = 235;
                public static uint Destroyer = 236;
                public static uint DiabloicImp = 237;
                public static uint Defiler = 238;
                public static uint Wyvern = 239;
                public static uint Hellhound = 240;
                public static uint Phantasm = 241;
                public static uint Hellfire = 242;
                public static uint HellfireFighter = 243;
                public static uint Juggernaut = 244;
                public static uint Nightmare = 245;
                public static uint Blightwalker = 246;
                public static uint Plaguesmith = 247;
                public static uint Penguin = 250;
                public static uint NorsemanMale = 251;
                public static uint NorsemanFemale = 252;

                // Barbarians
                public static uint BarbarianHeadsplitter = 253;
                public static uint BarbarianSkullhunter = 254;
                public static uint BarbarianBloodwalker = 255;
                public static uint BarbarianBrutetamer = 264;

                public static uint Braindeath = 256;
                public static uint FrostGiant = 257;
                public static uint Husky = 258;

                // Chakoya
                public static uint ChakoyaTribewarden = 249;
                public static uint ChakoyaToolshaper = 259;
                public static uint ChakoyaWindcaller = 260;

                public static uint IceGolem = 261;
                public static uint SilverRabbit = 262;
                public static uint FrostGiantess = 265;
                public static uint CommunityManager = 266;
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
