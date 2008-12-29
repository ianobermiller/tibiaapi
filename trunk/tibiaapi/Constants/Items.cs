using System;
using Tibia.Objects;

namespace Tibia.Constants
{
    /// <summary>
    /// Contains item ids.
    /// </summary>
    public static class Items
    {
        public static class Ammunition
        {
            public static Item Arrow = new Item(3447, "Arrow");
            public static Item BurstArrow = new Item(3449, "Burst Arrow");
            public static Item PoisonedArrow = new Item(3448, "Poisoned Arrow");
            public static Item EarthArrow = new Item(7774, "Earth Arrow");
            public static Item FlashArrow = new Item(761, "Flash Arrow");
            public static Item ShiverArrow = new Item(762, "Shiver Arrow");
            public static Item FlamingArrow = new Item(763, "Flaming Arrow");
            public static Item SniperArrow = new Item(7364, "Sniper Arrow");

            public static Item PowerBolt = new Item(3450, "Power Bolt");
            public static Item Bolts = new Item(3446, "Bolt");
            public static Item PiercingBolt = new Item(7363, "Piercing Bolt");

            public static Item SmallStone = new Item(1781, "Small Stone");
            public static Item ThrowingStar = new Item(3287, "Throwing Star");
            public static Item ThrowingKnife = new Item(3298, "Throwing Knife");

            public static Item Spear = new Item(3277, "Spear");
            public static Item HuntingSpear = new Item(3347, "Hunting Spear");
            public static TransformingItem EnchantedSpear = new TransformingItem(7367, "Enchanted Spear", Spells.EnchantSpear, 3, Ammunition.Spear);
            public static Item RoyalSpear = new Item(7378, "Royal Spear");
        }

        public static class Boots
        {
            public static Item BootsofHaste = new Item(3079, "Boots of Haste");
            public static Item LeatherBoots = new Item(3552, "Leather Boots");
            public static Item SteelBoots = new Item(3554, "Steel Boots");
            public static Item CrocodileBoots = new Item(3556, "Crocodile Boots");
            public static Item LightningBoots = new Item(820, "Lightning Boots");
            public static Item TerraBoots = new Item(813, "Terra Boots");
            public static Item FurBoots = new Item(7457, "Fur Boots");
            public static Item SoftBoots = new Item(6529, "Soft Boots");
        }

        public static class Bottle
        {
            public static Item Vial = new Item(2874, "Vial");
        }

        public static class Container
        {
            public static Item BackpackBlack = new Item(2870, "Backpack Black");
            public static Item BackpackBlue = new Item(2869, "Backpack Blue");
            public static Item BackpackBrown = new Item(2854, "Backpack Brown");
            public static Item BackpackGold = new Item(2871, "Backpack Gold");
            public static Item BackpackGrass = new Item(2872, "Backpack Grass");
            public static Item BackpackGreen = new Item(2865, "Backpack Green");
            public static Item BackpackPirate = new Item(5926, "Backpack Pirate");
            public static Item BackpackPurple = new Item(2868, "Backpack Purple");
            public static Item BackpackRed = new Item(2867, "Backpack Red");
            public static Item BackpackStar = new Item(5949, "Backpack Star");
            public static Item BackpackYellow = new Item(2866, "Backpack Yellow");
            public static Item ParcelUsed = new Item(3504, "Parcel Used");
            public static Item BagBrown = new Item(2853, "Bag Brown");
            public static Item BagGreen = new Item(2857, "Bag Green");
            public static Item BagYellow = new Item(2858, "Bag Yellow");
            public static Item BagRed = new Item(2859, "Bag Red");
            public static Item BagPurple = new Item(2860, "Bag Purple");
            public static Item BagBlue = new Item(2861, "Bag Blue");
            public static Item BagGrey = new Item(2862, "Bag Grey");
            public static Item ParcelNew = new Item(3503, "Parcel New");
            public static Item BackpackOfHolding = new Item(3253, "Backpack of Holding");
        }

        public static class Fluid
        {
            public static Item Empty = Bottle.Vial;
            public static Item Life = new Item(Bottle.Vial.Id, "Lifefluid") { Count = 11 };
            public static Item Mana = new Item(Bottle.Vial.Id, "Manafluid") { Count = 10 };
        }

        public static class Food
        {
            public static Objects.Food Apple = new Objects.Food(3585, "Apple", 0);
            public static Objects.Food Banana = new Objects.Food(3587, "Banana", 0);
            public static Objects.Food Blueberry = new Objects.Food(0, "Blueberry", 0);
            public static Objects.Food Bread = new Objects.Food(3600, "Bread", 0);
            public static Objects.Food BrownBread = new Objects.Food(3602, "Brown Bread", 0);
            public static Objects.Food BrownMushroom = new Objects.Food(3725, "Brown Mushroom", 0);
            public static Objects.Food Carrot = new Objects.Food(3595, "Carrot", 0);
            public static Objects.Food Cheese = new Objects.Food(3607, "Cheese", 0);
            public static Objects.Food Cherry = new Objects.Food(3590, "Cherry", 0);
            public static Objects.Food Coconut = new Objects.Food(3589, "Coconut", 0);
            public static Objects.Food Cookie = new Objects.Food(3598, "Cookie", 0);
            public static Objects.Food Corncob = new Objects.Food(3597, "Corncob", 0);
            public static Objects.Food DragonHam = new Objects.Food(3583, "Dragon Ham", 0);
            public static Objects.Food Egg = new Objects.Food(3606, "Egg", 0);
            public static Objects.Food Fish = new Objects.Food(3578, "Fish", 0);
            public static Objects.Food Grapes = new Objects.Food(3592, "Grapes", 0);
            public static Objects.Food GreenMushroom = new Objects.Food(3732, "Green Mushroom", 0);
            public static Objects.Food Ham = new Objects.Food(3582, "Ham", 0);
            public static Objects.Food Meat = new Objects.Food(3577, "Meat", 0);
            public static Objects.Food Mellon = new Objects.Food(3593, "Mellon", 0);
            public static Objects.Food Orange = new Objects.Food(3586, "Orange", 0);
            public static Objects.Food Roll = new Objects.Food(3601, "Roll", 0);
            public static Objects.Food Salmon = new Objects.Food(3579, "Salmon", 0);
            public static Objects.Food WhiteMushroom = new Objects.Food(3723, "White Mushroom", 0);
        }

        public static class Neck
        {
            public static Item AmuletOfLoss = new Item(3057, "Amulet of Loss");
            public static Item BronzeAmulet = new Item(3056, "Bronze Amulet");
            public static Item BronzeNecklace = new Item(3009, "Bronze Necklace");
            public static Item CrystalNecklace = new Item(3008, "Crystal Necklace");
            public static Item DragonNecklace = new Item(3085, "Dragon Necklace");
            public static Item ElvenAmulet = new Item(3082, "Elven Amulet");
            public static Item GarlicNecklace = new Item(3083, "Garlic Necklace");
            public static Item GoldenAmulet = new Item(3013, "Golden Amulet");
            public static Item PlatinumAmulet = new Item(3055, "Platinum Amulet");
            public static Item ProtectionAmulet = new Item(3084, "Protection Amulet");
            public static Item Scarf = new Item(3572, "Scarf");
            public static Item SilverAmulet = new Item(3054, "Silver Amulet");
            public static Item StarAmulet = new Item(3014, "Star Amulet");
            public static Item StoneSkinAmulet = new Item(3081, "Stone Skin Amulet");
            public static Item StrangeTalisman = new Item(3045, "Strange Talisman");
            public static Item WolfToothChain = new Item(3012, "Wolf ToothChain");
        }

        public static class Potion
        {
            public static Item Health = new Item(266, "Health");
            public static Item Mana = new Item(268, "Mana");
            public static Item StrongHealth = new Item(236, "Strong Health");
            public static Item StrongMana = new Item(237, "Strong Mana");
            public static Item GreatMana = new Item(238, "Great Mana");
            public static Item GreatHealth = new Item(239, "Great Health");
            public static Item UltimateHealth = new Item(7643, "Ultimate Health");
            public static Item GreatSpirit = new Item(7642, "Great Spirit");
        }

        public static class Quest
        {
            public static Item ApeFur = new Item(5883, "Ape Fur");
            public static Item BatWing = new Item(5894, "Bat Wing");
            public static Item BearPaw = new Item(5896, "Bear Paw");
            public static Item BeholderEye = new Item(5898, "Beholder Eye");
            public static Item BluePiece = new Item(5912, "Blue Piece");
            public static Item BrownPiece = new Item(5913, "Brown Piece");
            public static Item ChickenFeather = new Item(5890, "Chicken Feather");
            public static Item DragonClaw = new Item(5919, "Dragon Claw");
            public static Item FishFin = new Item(5895, "Fish Fin");
            public static Item GreenDragonLeather = new Item(5877, "Green Dragon Leather");
            public static Item GreenDragonScale = new Item(5920, "Green Dragon Scale");
            public static Item GreenPiece = new Item(5910, "Green Piece");
            public static Item HeavenBlossom = new Item(5921, "Heaven Blossom");
            public static Item HoneyComb = new Item(5902, "Honey Comb");
            public static Item LizardLeather = new Item(5876, "Lizard Leather");
            public static Item LizardScale = new Item(5881, "Lizard Scale");
            public static Item MinotaurLeather = new Item(5878, "Minotaur Leather");
            public static Item RedDragonLeather = new Item(5948, "Red Dragon Leather");
            public static Item RedDragonScale = new Item(5882, "Red Dragon Scale");
            public static Item RedPiece = new Item(5911, "Red Piece");
            public static Item SniperGloves = new Item(5875, "Sniper Gloves");
            public static Item VampireDust = new Item(5905, "Vampire Dust");
            public static Item WhitePiece = new Item(5909, "White Piece");
            public static Item WolfPaw = new Item(5897, "Wolf Paw");
            public static Item YellowPiece = new Item(5914, "Yellow Piece");
            public static Item IronOre = new Item(5880, "Iron Ore");
            public static Item Hook = new Item(6097, "Hook");
            public static Item EyePatch = new Item(6098, "Eye Patch");
            public static Item PegLeg = new Item(6126, "Peg Leg");
        }

        public static class Ring
        {
            public static Item AxeRing = new Item(3092, "Axe Ring");
            public static Item ClubRing = new Item(3093, "Club Ring");
            public static Item CrystalRing = new Item(3007, "Crystal Ring");
            public static Item DwarvenRing = new Item(3097, "Dwarven Ring");
            public static Item EnergyRing = new Item(3051, "Energy Ring");
            public static Item GoldenRing = new Item(3063, "Golden Ring");
            public static Item LifeRing = new Item(3052, "Life Ring");
            public static Item MightRing = new Item(3048, "Might Ring");
            public static Item PowerRing = new Item(3050, "Power Ring");
            public static Item RingOfHealing = new Item(3098, "Ring Of Healing");
            public static Item RingOfTheSkies = new Item(3006, "Ring of the Skies");
            public static Item StealthRing = new Item(3049, "Stealth Ring");
            public static Item SwordRing = new Item(3091, "Sword Ring");
            public static Item TimeRing = new Item(3053, "Time Ring");
            public static Item WeddingRing = new Item(3004, "Wedding Ring");
        }

        public static class Rune
        {
            public static Objects.Rune Blank = new Objects.Rune(3147, "Blank Rune", null, 0);
            public static Objects.Rune AnimateDead = new Objects.Rune(3203, "Animate Dead Rune", Spells.AnimateDead, 5);
            public static Objects.Rune Antidote = new Objects.Rune(3153, "Antidote Rune", Spells.AntidoteRune, 1);
            public static Objects.Rune Avalanche = new Objects.Rune(3161, "Avalanche Rune", Spells.AvalancheRune, 3);
            public static Objects.Rune Chameleon = new Objects.Rune(3178, "Chameleon Rune", Spells.Chameleon, 2);
            public static Objects.Rune ConvinceCreature = new Objects.Rune(3177, "Convince Creature Rune", Spells.ConvinceCreature, 3);
            public static Objects.Rune Desintegrate = new Objects.Rune(3197, "Desintegrate Rune", Spells.Desintegrate, 3);
            public static Objects.Rune DestroyField = new Objects.Rune(3148, "Destroy Field Rune", Spells.DestroyField, 2);
            public static Objects.Rune EnergyBomb = new Objects.Rune(3149, "Energy Bomb Rune", Spells.EnergyBomb, 5);
            public static Objects.Rune EnergyField = new Objects.Rune(3164, "Energy Field Rune", Spells.EnergyField, 2);
            public static Objects.Rune EnergyWall = new Objects.Rune(3166, "Energy Wall Rune", Spells.EnergyWall, 5);
            public static Objects.Rune Explosion = new Objects.Rune(3200, "Explosion Rune", Spells.Explosion, 4);
            public static Objects.Rune FireBomb = new Objects.Rune(3192, "Fire Bomb Rune", Spells.FireBomb, 4);
            public static Objects.Rune FireField = new Objects.Rune(3188, "Fire Field Rune", Spells.FireField, 1);
            public static Objects.Rune FireWall = new Objects.Rune(3190, "Fire Wall Rune", Spells.FireWall, 4);
            public static Objects.Rune Fireball = new Objects.Rune(3189, "Fireball Rune", Spells.Fireball, 3);
            public static Objects.Rune GreatFireball = new Objects.Rune(3191, "Great Fireball Rune", Spells.GreatFireball, 3);
            public static Objects.Rune HeavyMagicMissile = new Objects.Rune(3198, "Heavy Magic Missile Rune", Spells.HeavyMagicMissile, 2);
            public static Objects.Rune Icicle = new Objects.Rune(3158, "Icicle Rune", Spells.Icicle, 3);
            public static Objects.Rune IntenseHealing = new Objects.Rune(3152, "Intense Healing Rune", Spells.IntenseHealingRune, 2);
            public static Objects.Rune LightMagicMissile = new Objects.Rune(3174, "Light Magic Missile Rune", Spells.LightMagicMissile, 1);
            public static Objects.Rune MagicWall = new Objects.Rune(3180, "Magic Wall Rune", Spells.MagicWall, 5);
            public static Objects.Rune Paralyze = new Objects.Rune(3165, "Paralyze Rune", Spells.Paralyze, 3);
            public static Objects.Rune PoisonBomb = new Objects.Rune(3173, "Poison Bomb Rune", Spells.PoisonBomb, 2);
            public static Objects.Rune PoisonField = new Objects.Rune(3172, "Poison Field Rune", Spells.PoisonField, 1);
            public static Objects.Rune PoisonWall = new Objects.Rune(3176, "Poison Wall Rune", Spells.PoisonWall, 3);
            public static Objects.Rune Soulfire = new Objects.Rune(3195, "Soulfire Rune", Spells.Soulfire, 3);
            public static Objects.Rune Stalagmite = new Objects.Rune(3179, "Stalagmite Rune", Spells.Stalagmite, 2);
            public static Objects.Rune StoneShower = new Objects.Rune(3175, "Stone Shower Rune", Spells.StoneShower, 3);
            public static Objects.Rune SuddenDeath = new Objects.Rune(3155, "Sudden Death Rune", Spells.SuddenDeath, 5);
            public static Objects.Rune Thunderstorm = new Objects.Rune(3202, "Thunderstorm Rune", Spells.Thunderstorm, 3);
            public static Objects.Rune UltimateHealing = new Objects.Rune(3160, "Ultimate Healing Rune", Spells.UltimateHealingRune, 3);
        }

        public static class Tool
        {
            public static Item Rope = new Item(3003, "Rope");
            public static Item FishingRod = new Item(3483, "Fishing Rod");
            public static Item Pick = new Item(3456, "Pick");
            public static Item Shovel = new Item(3457, "Shovel");
            public static Item Scythe = new Item(3453, "Scythe");
            public static Item LightShovel = new Item(5710, "Light Shovel");
            public static Item ElvenhairRope = new Item(646, "Elvenhair Rope");
        }

        public static class Valuable
        {
            public static Item GoldCoin = new Item(3031, "Gold Coin");
            public static Item PlatinumCoin = new Item(3035, "Platinum Coin");
            public static Item CrystalCoin = new Item(3043, "Crystal Coin");
            public static Item ScarabCoin = new Item(3042, "Scarab Coin");
            public static Item SmallAmethyst = new Item(3033, "Small Amethyst");
            public static Item SmallEmerald = new Item(3032, "Small Emerald");
            public static Item BlackPearl = new Item(3027, "Black Pearl");
            public static Item WhitePearl = new Item(3026, "White Pearl");
            public static Item SmallDiamond = new Item(3028, "Small Diamond");
            public static Item SmallSapphire = new Item(3029, "Small Sapphire");
            public static Item SmallRuby = new Item(3030, "Small Ruby");
            public static Item Talon = new Item(3034, "Talon");
        }
    }
}
