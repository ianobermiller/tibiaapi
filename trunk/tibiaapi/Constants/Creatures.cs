using System;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Constants {
  public static class Creatures {
      public static CreatureData Achad = new CreatureData("Achad", 185, 70, 0, 0, 80, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10)},
          new List<DamageModifier>() {  },
          new List<string>() { "You won't pass me.",  "I have travelled far to fight here.",  },
          new List<Loot>() { }
      );
      public static CreatureData AcidBlob = new CreatureData("Acid Blob", 250, 250, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Kzzchhhh",  },
          new List<Loot>() { new Loot("Glob of Acid",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData AcolyteoftheCult = new CreatureData("Acolyte of the Cult", 390, 300, 0, 0, 220, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Praise Voodoo!",  "Power to the cult!",  "Feel the power of the cult!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Orange Book",  0,  LootPossibility.Rare,  0), new Loot("Dragon Necklace",  3085,  LootPossibility.Rare,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("rare)",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.Rare,  0), new Loot("Small Emerald",  3032,  LootPossibility.Rare,  0), new Loot("Terra Rod",  0,  LootPossibility.Rare,  0), new Loot("Green Tunic",  0,  LootPossibility.Rare,  0), new Loot("Pirate Voodoo Doll",  0,  LootPossibility.Rare,  0), new Loot("Key Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData AdeptoftheCult = new CreatureData("Adept of the Cult", 430, 400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Ice,  40), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Feel the power of the cult!",  "Praise the voodoo!",  "Power to the cult!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  57), new Loot("Clerical Mace",  0,  LootPossibility.SemiRare,  0), new Loot("Pirate Voodoo Doll",  0,  LootPossibility.SemiRare,  0), new Loot("Orange Book",  0,  LootPossibility.Rare,  0), new Loot("Time Ring",  0,  LootPossibility.Rare,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Rare,  0), new Loot("Small Ruby",  0,  LootPossibility.Rare,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("rare)",  0,  LootPossibility.Normal,  0), new Loot("Hailstorm Rod",  0,  LootPossibility.Rare,  0), new Loot("Amber Staff",  0,  LootPossibility.Rare,  0), new Loot("Red Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Key Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Lunar Staff",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Amazon = new CreatureData("Amazon", 110, 60, 0, 0, 45, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Your head shall be mine!",  "Yeeee ha!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Skull",  0,  LootPossibility.Normal,  2), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Brown Bread",  3602,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Studded Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.Rare,  0), new Loot("Small Ruby",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData AncientScarab = new CreatureData("Ancient Scarab", 1000, 720, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  230), new Loot("Scarab Coin",  3042,  LootPossibility.Normal,  4), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.SemiRare,  4), new Loot("Ancient Amulet",  0,  LootPossibility.SemiRare,  0), new Loot("Scarab Amulet",  0,  LootPossibility.Rare,  0), new Loot("Small Emerald",  3032,  LootPossibility.Rare,  3), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Daramanian Waraxe",  0,  LootPossibility.VeryRare,  0), new Loot("Scarab Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Terra Hood",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Annihilon = new CreatureData("Annihilon", 40000, 15000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  95), new DamageModifier(DamageType.Energy,  95), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5) },
          new List<string>() { "Flee as long as you can!",  "Annihilon's might will crush you all!",  "I am coming for you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  159), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  24), new Loot("Gold Ingots",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Potions",  0,  LootPossibility.Normal,  2), new Loot("Viper Star",  0,  LootPossibility.Normal,  29), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  20), new Loot("Berserk Potions",  0,  LootPossibility.Normal,  2), new Loot("Infernal Bolts",  0,  LootPossibility.Normal,  49), new Loot("Power Bolts",  0,  LootPossibility.Normal,  82), new Loot("Flaming Arrows",  0,  LootPossibility.Normal,  99), new Loot("Demon Horns",  0,  LootPossibility.Normal,  2), new Loot("Giant Shimmering Pearl",  0,  LootPossibility.Normal,  4), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Guardian Shield",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Guardian Halberd",  0,  LootPossibility.Normal,  0), new Loot("Violet Gem",  0,  LootPossibility.Normal,  0), new Loot("Green Gem",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Emerald Bangle",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Diamond Sceptre",  0,  LootPossibility.Normal,  0), new Loot("Red Gem",  0,  LootPossibility.Normal,  0), new Loot("Yellow Gem",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Tower Shield",  0,  LootPossibility.Normal,  0), new Loot("Paladin Armor",  0,  LootPossibility.Normal,  0), new Loot("Heavy Mace",  0,  LootPossibility.Normal,  0), new Loot("Skullcracker Armor",  0,  LootPossibility.Rare,  0), new Loot("Demonbone",  0,  LootPossibility.VeryRare,  0), new Loot("Onyx Flail",  0,  LootPossibility.VeryRare,  0), new Loot("Obsidian Truncheon",  0,  LootPossibility.Rare,  0), new Loot("The Stomper",  0,  LootPossibility.VeryRare,  0), new Loot("Lavos Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Apocalypse = new CreatureData("Apocalypse", 160000, 80000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "DESTRUCTION!",  "BOW TO THE POWER OF THE RUTHLESS SEVEN!",  "CHAOS!",  "DEATH TO ALL!",  },
          new List<Loot>() { new Loot("Expect very rare items",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ApprenticeSheng = new CreatureData("Apprentice Sheng", 95, 150, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I will protect the secrets of my master!",  "Kaplar!",  "This isle will become ours alone",  "You already know too much.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  2), new Loot("Dead Snakes",  0,  LootPossibility.Normal,  2), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Shovel",  3457,  LootPossibility.Normal,  0), new Loot("Carrot",  3595,  LootPossibility.Normal,  5), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Minotaur Leather",  5878,  LootPossibility.Always,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData ArachirTheAncientOne = new CreatureData("Arachir The Ancient One", 0, 1800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "I was the shadow that haunted the cradle of humanity!",  "Your worthles existence will nourish something greater!",  "I exist since eons ,  you want to defy me",  "Can you feel the passage of time,  mortal",  },
          new List<Loot>() { new Loot("Gold Coins",  0,  LootPossibility.Normal,  98), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Black Pearls",  0,  LootPossibility.Normal,  1), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.Normal,  0), new Loot("Bloody Edge",  0,  LootPossibility.Rare,  0), new Loot("Vampire Lord Token",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Arkhothep = new CreatureData("Arkhothep", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Armenius = new CreatureData("Armenius", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Arthei = new CreatureData("Arthei", 0, 4000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Marziel! Lersatio! Boreth! Come join me in this fight!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.Normal,  0), new Loot("Dreaded Cleaver",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Ashmunrah = new CreatureData("Ashmunrah", 5000, 3100, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "No mortal or undead will steal my secrets!",  "Ahhhh all those long years.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  140), new Loot("Silver Brooch",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Might Ring",  0,  LootPossibility.Rare,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Holy Scarab",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Assassin = new CreatureData("Assassin", 175, 105, 0, 0, 160, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "You are on my deathlist!",  "Die!",  "Feel the h,  of death!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Throwing Star",  3287,  LootPossibility.Normal,  14), new Loot("Torch",  0,  LootPossibility.Normal,  2), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Viper Star",  0,  LootPossibility.SemiRare,  8), new Loot("Leopard Armor",  0,  LootPossibility.Rare,  0), new Loot("Small Diamond",  0,  LootPossibility.VeryRare,  2), new Loot("Horseman Helmet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Avalanche = new CreatureData("Avalanche", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData AxeitusHeadbanger = new CreatureData("Axeitus Headbanger", 365, 140, 0, 0, 130, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Hicks",  "St,  still! Both of you! lt",  "hicksgt",  "",  "This victory will earn me a casket of beer.",  },
          new List<Loot>() { }
      );
      public static CreatureData Azerus = new CreatureData("Azerus", 0, 6000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1) },
          new List<string>() { "The ultimate will finally consume this unworthy existence!",  "My masters ,  I will tear down barriers ,  join the ultimate in its realm!",  "The power of the Yalahari will all be mine!",  "We will open the rift for a new time to come!",  "He who has returned from beyond has taught me secrets you can't even grasp!",  "The end of times has come!",  "The great machinator will make his entrance soon!",  "You might scratch my shields but they will never break!",  "You can't hope to penetrate my shields!",  "Do you really think you could beat me",  },
          new List<Loot>() { new Loot("It will turn into a teleport after killing",  0,  LootPossibility.Normal,  0), new Loot("so it gives no loot",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData AzureFrog = new CreatureData("Azure Frog", 60, 20, 0, 0, 24, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Badger = new CreatureData("Badger", 23, 5, 0, 0, 12, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Bandit = new CreatureData("Bandit", 245, 65, 0, 0, 43, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "H,  me your purse!",  "Your money or your life!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  28), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Iron Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("War Hammer",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Banshee = new CreatureData("Banshee", 1000, 900, 0, 0, 652, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Dance for me your dance of death!",  "Let the music play!",  "I will mourn your death!",  "Are you ready to rock",  "Feel my gentle kiss of death.",  "That's what I call easy listening!",  "IIIIEEEeeeeeehhhHHHH!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("C",  0,  LootPossibility.Normal,  0), new Loot("lestick",  0,  LootPossibility.Normal,  0), new Loot("Simple Dress",  0,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Mirror",  0,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Poison Dagger",  0,  LootPossibility.Rare,  0), new Loot("Silver Brooch",  0,  LootPossibility.Rare,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Rare,  0), new Loot("Lyre",  0,  LootPossibility.Rare,  0), new Loot("Spellbook",  0,  LootPossibility.Rare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Blue Robe",  0,  LootPossibility.Rare,  0), new Loot("White Pearl",  3026,  LootPossibility.VeryRare,  0), new Loot("Terra Mantle",  0,  LootPossibility.VeryRare,  0), new Loot("Wedding Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0), new Loot("Red Robe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Barbaria = new CreatureData("Barbaria", 600, 355, 0, 0, 133, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "To me,  creatures of the wild!",  "My instincts tell me about your cowardice.",  },
          new List<Loot>() { new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Gold Coin",  3031,  LootPossibility.Normal,  33), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Fur Bag",  0,  LootPossibility.Normal,  0), new Loot("Grey Small Book",  0,  LootPossibility.Normal,  0), new Loot("Hunting Spear",  3347,  LootPossibility.Normal,  0), new Loot("Mammoth Fur Cape",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData BarbarianBloodwalker = new CreatureData("Barbarian Bloodwalker", 305, 195, 0, 0, 200, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  5) },
          new List<string>() { "YAAAHEEE!",  "SLAUGHTER!",  "CARNAGE!",  "You can run but you can't hide",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  1), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Battle Axe",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Beastslayer Axe",  0,  LootPossibility.Rare,  0), new Loot("Fur Boots",  0,  LootPossibility.Rare,  0), new Loot("Red Piece of Cloth",  0,  LootPossibility.VeryRare,  0), new Loot("Shard",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BarbarianBrutetamer = new CreatureData("Barbarian Brutetamer", 145, 90, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "To me,  creatures of the wild!",  "My instincts tell me about your cowardice.",  "Feel the power of the beast!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Corncob",  3597,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  3), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Grey Small Book",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Hunting Spear",  3347,  LootPossibility.Normal,  0), new Loot("Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Brutetamer's Staff",  0,  LootPossibility.Rare,  0), new Loot("Fur Boots",  0,  LootPossibility.Rare,  0), new Loot("Mammoth Fur Shorts",  0,  LootPossibility.VeryRare,  0), new Loot("Mammoth Fur Cape",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BarbarianHeadsplitter = new CreatureData("Barbarian Headsplitter", 100, 85, 0, 0, 110, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "I will regain my honor with your blood!",  "Surrender is not option!",  "Its you or me!",  "Die! Die! Die!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Brown Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Scale Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Krimhorn Helmet",  0,  LootPossibility.Rare,  0), new Loot("Crystal Sword",  0,  LootPossibility.Rare,  0), new Loot("Fur boots",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData BarbarianSkullhunter = new CreatureData("Barbarian Skullhunter", 135, 85, 0, 0, 65, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "You will become my trophy.",  "Fight harder,  coward.",  "Show that you are a worthy opponent.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Brown Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Scale Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Life Ring",  0,  LootPossibility.Rare,  0), new Loot("Ragnir Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Crystal Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Fur Boots",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Bat = new CreatureData("Bat", 30, 10, 0, 0, 8, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20) },
          new List<string>() { "Flap! Flap!",  },
          new List<Loot>() { new Loot("Bat Wing",  5894,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Bazir = new CreatureData("Bazir", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "COME HERE! FREE ITEMS FOR EVERYONE!",  "BOW TO THE POWER OF THE RUTHLESS SEVEN!",  "Slay your friends ,  I will spare you!",  "DON'T BE AFRAID!  I AM COMING IN PEACE!",  },
          new List<Loot>() { new Loot("Expect very rare items",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Bear = new CreatureData("Bear", 80, 23, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Grrrr",  "Groar",  },
          new List<Loot>() { new Loot("Ham",  3582,  LootPossibility.Normal,  3), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Worms",  0,  LootPossibility.SemiRare,  3), new Loot("Bear Paw",  5896,  LootPossibility.Rare,  0), new Loot("Honeycomb",  0,  LootPossibility.Rare,  0), new Loot("Bag",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Behemoth = new CreatureData("Behemoth", 4000, 2500, 0, 0, 630, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Crush the intruders!",  "You're so little!",  "Human flesh -  delicious!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  140), new Loot("Meat",  3577,  LootPossibility.Normal,  6), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Crowbar",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Amphora",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Assassin Stars",  0,  LootPossibility.Normal,  2), new Loot("Dark Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.SemiRare,  4), new Loot("Giant Sword",  0,  LootPossibility.Rare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Perfect Behemoth Fang",  0,  LootPossibility.Rare,  0), new Loot("Strange Symbol",  0,  LootPossibility.Rare,  0), new Loot("War Axe",  0,  LootPossibility.Rare,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.VeryRare,  0), new Loot("Behemoth Claw",  0,  LootPossibility.VeryRare,  0), new Loot("Behemoth Trophy",  0,  LootPossibility.VeryRare,  0), new Loot("Steel Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Titan Axe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Beholder = new CreatureData("Beholder", 260, 170, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "You've got the look!",  "Let me take a look at you.",  "Eye for eye!",  "I've got to look!",  "Here's looking at you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  60), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Beholder Eye",  5898,  LootPossibility.Rare,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Terra Rod",  0,  LootPossibility.Rare,  0), new Loot("Beholder Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BerserkerChicken = new CreatureData("Berserker Chicken", 465, 220, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20) },
          new List<string>() { "Gokgoooook",  "Cluck Cluck",  "I will fill MY cushion with YOUR hair! CLUCK!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100)}
      );
      public static CreatureData BetrayedWraith = new CreatureData("Betrayed Wraith", 4200, 3500, 0, 0, 450, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20) },
          new List<string>() { "Rrrah!",  "Gnarr!",  "Tcharrr!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  302), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  1), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  4), new Loot("Sniper Arrows",  0,  LootPossibility.Normal,  5), new Loot("Orichalcum Pearls",  0,  LootPossibility.Normal,  2), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Spike Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0), new Loot("Skull Helmet",  0,  LootPossibility.Rare,  0), new Loot("Bloody Edge",  0,  LootPossibility.VeryRare,  0), new Loot("Amulet of Loss",  3057,  LootPossibility.Normal,  0), new Loot("not obtainable at PvP-Enforced Worlds)",  0,  LootPossibility.Normal,  0), new Loot("Golden Figurine",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BigBossTrolliver = new CreatureData("Big Boss Trolliver", 150, 105, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("Studded Club",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData BlackKnight = new CreatureData("Black Knight", 1800, 1600, 0, 0, 500, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Fire,  95), new DamageModifier(DamageType.Energy,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  8) },
          new List<string>() { "No prisoners!",  "By bolg's blood",  "You're no match for me!",  "NO MERCY!",  "MINE!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  124), new Loot("Spears",  0,  LootPossibility.Normal,  3), new Loot("Dark Armor",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Dark Helmet",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Brown Bread",  3602,  LootPossibility.Normal,  2), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Knight Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Legs",  0,  LootPossibility.Rare,  0), new Loot("Ruby Necklace",  0,  LootPossibility.Rare,  0), new Loot("Knight Armor",  0,  LootPossibility.Rare,  0), new Loot("Dragon Lance",  0,  LootPossibility.VeryRare,  0), new Loot("Lightning Legs",  0,  LootPossibility.VeryRare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BlackSheep = new CreatureData("Black Sheep", 20, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Maeh",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4)}
      );
      public static CreatureData BlazingFireElemental = new CreatureData("Blazing Fire Elemental", 650, 450, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  30), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Flaming Arrow",  763,  LootPossibility.Normal,  4), new Loot("Glimmering Soil",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Blightwalker = new CreatureData("Blightwalker", 8900, 5850, 0, 0, 900, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  50), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  30) },
          new List<string>() { "I can see you decaying!",  "Let me taste your mortality!",  "Your lifeforce is waning!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Bunches of Wheat",  0,  LootPossibility.Normal,  3), new Loot("Poison Arrows",  0,  LootPossibility.Normal,  9), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Seeds",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Scythe",  3453,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Hailstorm Rod",  0,  LootPossibility.SemiRare,  0), new Loot("Gold Ring",  0,  LootPossibility.Rare,  0), new Loot("Terra Legs",  0,  LootPossibility.Rare,  0), new Loot("Terra Mantle",  0,  LootPossibility.Rare,  0), new Loot("Garlic Necklace",  3083,  LootPossibility.VeryRare,  0), new Loot("Amulet of Loss",  3057,  LootPossibility.VeryRare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Gold Ingot",  0,  LootPossibility.VeryRare,  0), new Loot("Skull Staff",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BlisteringFireElemental = new CreatureData("Blistering Fire Elemental", 0, 1300, 0, 0, 975, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { "FCHHHRRR",  },
          new List<Loot>() { new Loot("Gp",  0,  LootPossibility.Normal,  125), new Loot("Small Rubies",  0,  LootPossibility.Normal,  3), new Loot("Glimmering Soil",  0,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Draconia",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData BloodCrab = new CreatureData("Blood Crab", 290, 160, 0, 0, 110, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("White Pearl",  3026,  LootPossibility.Rare,  0)}
      );
      public static CreatureData BloodCrabUnderwater = new CreatureData("Blood Crab Underwater", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Bloodpaw = new CreatureData("Bloodpaw", 100, 50, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData BlueDjinn = new CreatureData("Blue Djinn", 330, 215, 0, 0, 225, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  80), new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  12), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Simsalabim",  "Wishes can come true",  "Feel the power of my magic,  tiny mortal!",  "Be careful what you wish for.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  120), new Loot("Carrot",  3595,  LootPossibility.Normal,  0), new Loot("Royal Spear",  7378,  LootPossibility.Normal,  2), new Loot("Small Oil Lamp",  0,  LootPossibility.Normal,  0), new Loot("Blue Book",  0,  LootPossibility.Normal,  0), new Loot("Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Blue Rose",  0,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.Rare,  5), new Loot("Blue Piece of Cloth",  0,  LootPossibility.Rare,  0), new Loot("Mystic Turban",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BogRaider = new CreatureData("Bog Raider", 1300, 800, 0, 0, 600, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Fire,  85), new DamageModifier(DamageType.Earth,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Tchhh!",  "Slurp!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  129), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  2), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Belted Cape",  0,  LootPossibility.Normal,  0), new Loot("Springsprout Rod",  0,  LootPossibility.Normal,  0), new Loot("Hibiscus Dress",  0,  LootPossibility.SemiRare,  0), new Loot("Paladin Armor",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Bonebeast = new CreatureData("Bonebeast", 515, 580, 0, 0, 340, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Knooorrrrr!",  "Cccchhhhhhhhh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Bone Shield",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Rare,  0), new Loot("Hardened Bone",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Bones = new CreatureData("Bones", 9500, 3750, 0, 0, 1200, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Your new name is breakfast.",  "Keep that dog away!",  "Out Fluffy! Out! Bad dog!",  },
          new List<Loot>() { new Loot("Platinum Coins",  0,  LootPossibility.Normal,  4), new Loot("Gold Coins",  0,  LootPossibility.Normal,  200), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  2), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  3), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Skull Helmet",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Magic Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Boogey = new CreatureData("Boogey", 930, 475, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  25), new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Since you didn't ate your spinach Bogey comes to get you!",  "Too bad you did not eat your lunch,  now I have to punish you!",  "Even if you beat me,  I'll hide in your closet until you one day drop your guard!",  "You better had believe in me!",  "I'll take you into the darkness ... forever!",  "",  },
          new List<Loot>() { new Loot("Heavy Metal T-Shirt",  0,  LootPossibility.Normal,  0), new Loot("Club of the Fury",  0,  LootPossibility.Normal,  0), new Loot("Scythe of the Reaper or Musician's Bow",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Boreth = new CreatureData("Boreth", 1400, 1800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  5), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Ring of Healing",  0,  LootPossibility.SemiRare,  0), new Loot("Vampire Shield",  0,  LootPossibility.Rare,  0), new Loot("Hibiscus Dress",  0,  LootPossibility.Rare,  0), new Loot("Dreaded Cleaver",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Bovinus = new CreatureData("Bovinus", 150, 60, 0, 0, 50, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Braindeath = new CreatureData("Braindeath", 1225, 985, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  15) },
          new List<string>() { "You have disturbed my thoughts!",  "Let me turn you into something more useful!",  "Let me taste your brain!",  "You will be punished!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Sniper Arrows",  0,  LootPossibility.Normal,  4), new Loot("Spellbook",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Bone Sword",  0,  LootPossibility.Normal,  0), new Loot("Clerical Mace",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Beholder Eye",  5898,  LootPossibility.SemiRare,  0), new Loot("Haunted Blade",  0,  LootPossibility.Rare,  0), new Loot("Beholder Shield",  0,  LootPossibility.Normal,  0), new Loot("Beholder Helmet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData BrideofNight = new CreatureData("Bride of Night", 275, 450, 0, 0, 100, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20) },
          new List<string>() { "The darkness is coming!",  "The light will be extinguished!",  "Embrace the night!",  },
          new List<Loot>() { new Loot("Midnight Shard",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData BrutusBloodbeard = new CreatureData("Brutus Bloodbeard", 1200, 795, 0, 0, 350, false, false, FrontAttack.Wave, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  200), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Pirate Backpack",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Brutus Bloodbeard's Hat",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Bug = new CreatureData("Bug", 29, 18, 0, 0, 23, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  6), new Loot("Cherries",  0,  LootPossibility.Rare,  3)}
      );
      public static CreatureData ButterflyBlue = new CreatureData("Butterfly Blue", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ButterflyPurple = new CreatureData("Butterfly Purple", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ButterflyRed = new CreatureData("Butterfly Red", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData CaptainJones = new CreatureData("Captain Jones", 800, 825, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Crown Legs",  0,  LootPossibility.Normal,  0), new Loot("Focus Cape",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Spike Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Carniphila = new CreatureData("Carniphila", 255, 150, 0, 0, 330, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  35)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  37), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Sling Herb",  0,  LootPossibility.Normal,  0), new Loot("Shadow Herb",  0,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Dark Mushroom",  0,  LootPossibility.Normal,  0), new Loot("Seeds",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData CarrionWorm = new CreatureData("Carrion Worm", 145, 70, 0, 0, 45, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  49), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Worms",  0,  LootPossibility.Normal,  5), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Katana",  0,  LootPossibility.Normal,  0), new Loot("Copper Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Cat = new CreatureData("Cat", 20, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Mew!",  "Meow!",  "Meow meow!",  },
          new List<Loot>() { }
      );
      public static CreatureData CaveRat = new CreatureData("Cave Rat", 30, 10, 0, 0, 10, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Meep!",  "Meeeeep!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  2), new Loot("Worms",  0,  LootPossibility.Normal,  3), new Loot("Cheese",  3607,  LootPossibility.Normal,  0), new Loot("Cookie",  3598,  LootPossibility.VeryRare,  0), new Loot("Jacket",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Centipede = new CreatureData("Centipede", 70, 34, 0, 0, 46, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  17), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ChakoyaToolshaper = new CreatureData("Chakoya Toolshaper", 80, 40, 0, 0, 80, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Energy,  15) },
          new List<string>() { "Chikuva!",  "Jinuma jamjam!",  "Suvituka siq chuqua!",  "Kiyosa sipaju!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  29), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  2), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Bone Shield",  0,  LootPossibility.Normal,  0), new Loot("Mammoth Whopper",  0,  LootPossibility.SemiRare,  0), new Loot("Ice Cube",  0,  LootPossibility.Rare,  0), new Loot("Green Perch",  0,  LootPossibility.Rare,  0), new Loot("Northern Pike",  0,  LootPossibility.Rare,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData ChakoyaTribewarden = new CreatureData("Chakoya Tribewarden", 68, 40, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  25)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Energy,  15) },
          new List<string>() { "Quisavu tukavi!",  "Si siyoqua jamjam!",  "Achuq! jinuma!",  "Si ji jusipa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("meat",  3577,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  3), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Bone Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Mammoth Whopper",  0,  LootPossibility.SemiRare,  0), new Loot("Ice Cube",  0,  LootPossibility.Rare,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Rare,  0), new Loot("Green Perch",  0,  LootPossibility.Rare,  0), new Loot("Northern Pike",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData ChakoyaWindcaller = new CreatureData("Chakoya Windcaller", 84, 48, 0, 0, 82, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Energy,  15) },
          new List<string>() { "Siqsiq ji jusipa!",  "Jagura taluka taqua!",  "Mupi! Si siyoqua jinuma!",  "Quatu nguraka!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  36), new Loot("Fish",  3578,  LootPossibility.Normal,  4), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Bone Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Ice Cube",  0,  LootPossibility.Rare,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Rare,  0), new Loot("Northern Pike",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData ChargedEnergyElemental = new CreatureData("Charged Energy Elemental", 500, 450, 0, 0, 375, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  124), new Loot("Flash Arrow",  761,  LootPossibility.Rare,  3), new Loot("Energy Soil",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Chicken = new CreatureData("Chicken", 15, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Cluck Cluck",  "Gokgoooook",  },
          new List<Loot>() { new Loot("Worms",  0,  LootPossibility.Normal,  3), new Loot("Chicken Feather",  5890,  LootPossibility.Normal,  0), new Loot("Egg",  3606,  LootPossibility.SemiRare,  0), new Loot("Meat",  3577,  LootPossibility.Rare,  2), new Loot("Blue Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Green Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Purple Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Red Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Yellow Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("only during the Easter holiday)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Cobra = new CreatureData("Cobra", 65, 30, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Fsssss",  "Zzzzzz",  },
          new List<Loot>() { }
      );
      public static CreatureData Cockroach = new CreatureData("Cockroach", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Cockroach Leg",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData ColeriantheBarbarian = new CreatureData("Colerian the Barbarian", 265, 90, 0, 0, 100, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20) },
          new List<string>() { "Flee,  coward!",  "You will lose!",  "Yeehaawh",  },
          new List<Loot>() { }
      );
      public static CreatureData CoralFrog = new CreatureData("Coral Frog", 60, 20, 0, 0, 24, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData CountessSorrow = new CreatureData("Countess Sorrow", 0, 13000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "I'm so sorry ... for youuu!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Worn Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.Normal,  0), new Loot("Silver Mace",  0,  LootPossibility.VeryRare,  0), new Loot("Countess Sorrow's Frozen Tear",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Crab = new CreatureData("Crab", 55, 30, 0, 0, 20, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Fish",  3578,  LootPossibility.Normal,  0)}
      );
      public static CreatureData CrazedBeggar = new CreatureData("Crazed Beggar", 100, 35, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Hehehe!",  "Raahhh!",  "You are one of THEM! Die!",  "Wanna buy roses",  "Make it stop!",  "They're coming! They're coming!",  "Gimme money!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  9), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Roll",  3601,  LootPossibility.Normal,  0), new Loot("Wooden Spoon",  0,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Rolling Pin",  0,  LootPossibility.Normal,  0), new Loot("Wooden Hammer",  0,  LootPossibility.Normal,  0), new Loot("Lute",  0,  LootPossibility.Normal,  0), new Loot("Red Rose",  0,  LootPossibility.Normal,  0), new Loot("Small Blue Pillow",  0,  LootPossibility.Rare,  0), new Loot("Rum Flask",  0,  LootPossibility.Rare,  0), new Loot("Sling Herb",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Very Noble-Looking Watch",  0,  LootPossibility.VeryRare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData CrimsonFrog = new CreatureData("Crimson Frog", 60, 20, 0, 0, 24, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Crocodile = new CreatureData("Crocodile", 105, 40, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Crocodile Boots",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData CryptShambler = new CreatureData("Crypt Shambler", 330, 195, 0, 0, 195, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Aaaaahhhh!",  "Hoooohhh!",  "Uhhhhhhh!",  "Chhhhhhh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  55), new Loot("Worms",  0,  LootPossibility.Normal,  10), new Loot("Rotten Meat",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Iron Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Bone Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Bone Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Throwing Stars",  0,  LootPossibility.SemiRare,  3), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Small Diamonds",  0,  LootPossibility.Rare,  2)}
      );
      public static CreatureData CrystalSpider = new CreatureData("Crystal Spider", 1250, 900, 0, 0, 358, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20) },
          new List<string>() { "Screeech!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  95), new Loot("Sniper Arrow",  7364,  LootPossibility.Normal,  7), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Ice Cube",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Glacier Mask",  0,  LootPossibility.SemiRare,  0), new Loot("Shard",  0,  LootPossibility.SemiRare,  0), new Loot("Time Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Spider Silk",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Armor",  0,  LootPossibility.Rare,  0), new Loot("Knight Legs",  0,  LootPossibility.Rare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Rare,  0), new Loot("Sapphire Hammer",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData CursedGladiator = new CreatureData("Cursed Gladiator", 435, 215, 0, 0, 200, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Death where are you",  },
          new List<Loot>() { }
      );
      public static CreatureData Cyclops = new CreatureData("Cyclops", 260, 150, 0, 0, 105, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Energy,  25)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Human,  uh will dyh!",  "Youh ah trak!",  "Let da mashing begin!",  "Toks utat",  "Il lorstok human!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  52), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Ham",  3582,  LootPossibility.Normal,  4), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.SemiRare,  0), new Loot("Halberd",  0,  LootPossibility.SemiRare,  0), new Loot("Dark Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Club Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Cyclops Trophy",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData CyclopsDrone = new CreatureData("Cyclops Drone", 325, 200, 0, 0, 180, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Fee! Fie! Foe! Fum!",  "Luttl pest!",  "Me makking you pulp!",  "Humy tasy! Hum hum!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  24), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Halberd",  0,  LootPossibility.Rare,  0), new Loot("Cyclops Trophy",  0,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData CyclopsSmith = new CreatureData("Cyclops Smith", 435, 255, 0, 0, 220, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Outis emoi g' onoma.",  "Whack da humy!",  "Ai humy phary ty kaynon",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  69), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Heavy Machete",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Battle Axe",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Dark Helmet",  0,  LootPossibility.Rare,  0), new Loot("Cyclops Trophy",  0,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Club Ring",  0,  LootPossibility.Rare,  0), new Loot("Spiked Squelcher",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Daemon = new CreatureData("Daemon", 3000, 6000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  80), new DamageModifier(DamageType.Earth,  60)},
          new List<DamageModifier>() {  },
          new List<string>() { "Your soul will be mine!",  "CHAMEK ATH UTHUL ARAK!",  "I SMELL FEEEEAAAAAR!",  "Your resistance is futile!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  350), new Loot("Golden Helmet",  0,  LootPossibility.Normal,  0), new Loot("Enchanted Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Fire Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DamagedWorkerGolem = new CreatureData("Damaged Worker Golem", 260, 95, 0, 0, 90, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Klonk klonk klonk",  "Failure! Failure!",  "Good morning citizen. How may I serve you",  "Target identified: Rat! Termination initiated!",  "Rrrtttarrrttarrrtta",  "Danger will...chrrr! Danger!",  "Self-diagnosis failed.",  "Aw... chhhrrr orders.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  86), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.Rare,  0), new Loot("Sword Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Nails",  0,  LootPossibility.VeryRare,  3)}
      );
      public static CreatureData DarakantheExecutioner = new CreatureData("Darakan the Executioner", 3500, 1600, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "FIGHT LIKE A BARBARIAN!",  "VICTORY IS MINE!",  "I AM your father!",  "To be the man you have to beat the man!",  },
          new List<Loot>() { }
      );
      public static CreatureData DarkApprentice = new CreatureData("Dark Apprentice", 225, 100, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "Outch!",  "I must dispose of my masters enemies!",  "Oops,  I did it again.",  "From the spirits that I called Sir,  deliver me!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Blank Runes",  0,  LootPossibility.Normal,  3), new Loot("Dead Crimson Frog",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Dragonbreath",  0,  LootPossibility.Normal,  0), new Loot("Mana Potions",  0,  LootPossibility.Rare,  2), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Decay",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData DarkMagician = new CreatureData("Dark Magician", 325, 185, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Feel the power of my runes!",  "Killing you gets expensive!",  "My secrets are mine alone!",  "St,  still!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  80), new Loot("Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.VeryRare,  0), new Loot("Small Enchanted Amethyst",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DarkMonk = new CreatureData("Dark Monk", 190, 145, 0, 0, 150, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { "You are no match to us!",  "Your end has come!",  "This is where your path will end!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Bags",  0,  LootPossibility.Normal,  2), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("Brown Flask",  0,  LootPossibility.Normal,  0), new Loot("Lamp",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Ankh",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData DarkTorturer = new CreatureData("Dark Torturer", 7350, 4650, 0, 0, 1300, false, true, FrontAttack.Wave, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  1), new DamageModifier(DamageType.Earth,  90)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "You like it,  don't you",  "IahaEhheAie!",  "It's party time!",  "Harrr,  Harrr!",  "The torturer is in!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  0), new Loot("White Pearl",  3026,  LootPossibility.Normal,  1), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  3), new Loot("Ham",  3582,  LootPossibility.Normal,  8), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Assassin Star",  0,  LootPossibility.SemiRare,  2), new Loot("Saw",  0,  LootPossibility.Rare,  0), new Loot("Steel Boots",  0,  LootPossibility.SemiRare,  0), new Loot("Gold Ingot",  0,  LootPossibility.Rare,  0), new Loot("Demonic Essence",  0,  LootPossibility.Rare,  0), new Loot("Orichalcum Pearl",  0,  LootPossibility.Rare,  2), new Loot("Key Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Cat's Paw",  0,  LootPossibility.VeryRare,  0), new Loot("Vile Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Butcher's Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Legs",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DeadeyeDevious = new CreatureData("Deadeye Devious", 1450, 500, 0, 0, 250, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Let's kill 'em",  "Arrrgh!",  "You'll never take me alive!",  "§%§amp",  "§! #*$§$!!",  "You won't get me alive!",  },
          new List<Loot>() { new Loot("Gold",  0,  LootPossibility.Normal,  140), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  1), new Loot("Skull",  0,  LootPossibility.Normal,  2), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Pirate Backpack",  0,  LootPossibility.Normal,  0), new Loot("Deadeye Devious' Eye Patch",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData DeathBlob = new CreatureData("Death Blob", 320, 300, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Glob of Tar",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Deathbringer = new CreatureData("Deathbringer", 10000, 5100, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "YOU FOOLS WILL PAY!",  "YOU ALL WILL DIE!",  "DEATH,  DESTRUCTION!",  "I will eat your soul!",  },
          new List<Loot>() { }
      );
      public static CreatureData Deathslicer = new CreatureData("Deathslicer", 0, 0, 0, 0, 1000, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Deathspawn = new CreatureData("Deathspawn", 225, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Energy,  15), new DamageModifier(DamageType.Ice,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Deer = new CreatureData("Deer", 25, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  2)}
      );
      public static CreatureData Defiler = new CreatureData("Defiler", 3650, 3700, 0, 0, 712, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Blubb",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  280), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  4), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Talon",  0,  LootPossibility.Normal,  0), new Loot("Small Emeralds",  0,  LootPossibility.SemiRare,  3), new Loot("Small Rubies",  0,  LootPossibility.SemiRare,  2), new Loot("Small Diamond",  0,  LootPossibility.SemiRare,  1), new Loot("Plate Armor",  0,  LootPossibility.Rare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Demodras = new CreatureData("Demodras", 4500, 6000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I WILL SET THE WORLD ON FIRE!",  "I WILL PROTECT MY BROOD!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  290), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  10), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  4), new Loot("Power Bolt",  3450,  LootPossibility.Normal,  5), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  6), new Loot("Burst Arrows",  0,  LootPossibility.Normal,  10), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  10), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  0), new Loot("Fire Sword",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Dragon Hammer",  0,  LootPossibility.Normal,  0), new Loot("Dragon Shield",  0,  LootPossibility.Normal,  0), new Loot("Serpent Sword",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Broadsword",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Stuffed Dragon",  0,  LootPossibility.Normal,  0), new Loot("Tower Shield",  0,  LootPossibility.Normal,  0), new Loot("Gemmed Book",  0,  LootPossibility.Normal,  0), new Loot("Red Dragon Leather",  5948,  LootPossibility.Normal,  0), new Loot("Red Dragon Scale",  5882,  LootPossibility.Normal,  0), new Loot("Strange Helmet",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Dragonbreath",  0,  LootPossibility.Normal,  0), new Loot("Royal Helmet",  0,  LootPossibility.Rare,  0), new Loot("Dragon Scale Mail",  0,  LootPossibility.Rare,  0), new Loot("Dragon Claw",  5919,  LootPossibility.Always,  0)}
      );
      public static CreatureData Demon = new CreatureData("Demon", 8200, 6000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Energy,  50), new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  12), new DamageModifier(DamageType.Ice,  12) },
          new List<string>() { "Your soul will be mine!",  "CHAMEK ATH UTHUL ARAK!",  "I SMELL FEEEEAAAAAR!",  "Your resistance is futile!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  310), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  1), new Loot("Fire Mushrooms",  0,  LootPossibility.Normal,  6), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Golden Sickle",  0,  LootPossibility.Normal,  0), new Loot("Small Emerald",  3032,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Giant Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Great Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.SemiRare,  3), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  3), new Loot("Golden Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Ice Rapier",  0,  LootPossibility.SemiRare,  0), new Loot("Orb",  0,  LootPossibility.SemiRare,  0), new Loot("Purple Tome",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Talon",  0,  LootPossibility.SemiRare,  0), new Loot("Assassin Stars",  0,  LootPossibility.Rare,  5), new Loot("Demon Horn",  0,  LootPossibility.Rare,  0), new Loot("Demon Shield",  0,  LootPossibility.Rare,  0), new Loot("Might Ring",  0,  LootPossibility.Rare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.SemiRare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Golden Legs",  0,  LootPossibility.Rare,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Rare,  0), new Loot("Demon Trophy",  0,  LootPossibility.VeryRare,  0), new Loot("Demonrage Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Magic Plate Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DemonParrot = new CreatureData("Demon Parrot", 360, 225, 0, 0, 190, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "ISHH THAT THE BESHHT YOU HAVE TO OFFERRR,  TIBIANSHH",  "YOU ARRRRRE DOOMED!",  "I SHHMELL FEEAARRR!",  "MY SHHEED IS FEARRR AND MY HARRRVEST ISHH YOURRR SHHOUL!",  "Your shhoooul will be mineee!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  92)}
      );
      public static CreatureData DemonSkeleton = new CreatureData("Demon Skeleton", 400, 240, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Throwing Star",  3287,  LootPossibility.Normal,  3), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Iron Helmet",  0,  LootPossibility.Normal,  0), new Loot("Mysterious Fetish",  0,  LootPossibility.Rare,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Mind Stone",  0,  LootPossibility.Rare,  0), new Loot("Guardian Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Demongoblin = new CreatureData("Demongoblin", 50, 25, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  9), new Loot("Small Stone",  1781,  LootPossibility.Normal,  4), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Small Axe",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Destroyer = new CreatureData("Destroyer", 3700, 2500, 0, 0, 615, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { "COME HERE AND DIE!",  "Destructiooooon!",  "It's a good day to destroy!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  330), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  2), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Meat",  3577,  LootPossibility.Normal,  6), new Loot("Burst Arrows",  0,  LootPossibility.Normal,  12), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Crowbar",  0,  LootPossibility.Normal,  0), new Loot("Dark Armor",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Rare,  0), new Loot("Giant Sword",  0,  LootPossibility.Rare,  0), new Loot("Steel Boots",  0,  LootPossibility.Rare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Chaos Mace",  0,  LootPossibility.Rare,  0), new Loot("Skull Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.VeryRare,  0), new Loot("Dreaded Cleaver",  0,  LootPossibility.VeryRare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Dharalion = new CreatureData("Dharalion", 380, 380, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Feel my wrath!",  "Noone will stop my ascension!",  "My powers are divine!",  "You desecrated this temple!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Arrows",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Apples",  0,  LootPossibility.Normal,  0), new Loot("Long Swords",  0,  LootPossibility.Normal,  0), new Loot("Waterskin",  0,  LootPossibility.Normal,  0), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Bow",  0,  LootPossibility.Normal,  0), new Loot("Poison Arrows",  0,  LootPossibility.Normal,  0), new Loot("Grapes",  3592,  LootPossibility.Normal,  0), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("C",  0,  LootPossibility.Normal,  0), new Loot("le",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Melon",  0,  LootPossibility.Normal,  0), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("Green Tunic",  0,  LootPossibility.Normal,  0), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Inkwell",  0,  LootPossibility.Normal,  0), new Loot("Parchment",  0,  LootPossibility.Normal,  0), new Loot("Sling Herb",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("C",  0,  LootPossibility.Normal,  0), new Loot("le Sticks",  0,  LootPossibility.Normal,  0), new Loot("Elven Amulet",  3082,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0), new Loot("Yellow Gem",  0,  LootPossibility.Normal,  0), new Loot("Holy Orchid",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Golden Goblet",  0,  LootPossibility.Rare,  0), new Loot("Cornucopia",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData DiabolicImp = new CreatureData("Diabolic Imp", 1950, 2900, 0, 0, 870, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Muahaha!",  "He he he.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  197), new Loot("Scimitar",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  2), new Loot("Cleaver",  0,  LootPossibility.Normal,  0), new Loot("Pitchfork",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  3), new Loot("Concentrated Demonic Bloods",  0,  LootPossibility.Normal,  2), new Loot("Guardian Shield",  0,  LootPossibility.Normal,  0), new Loot("Platinum Coins",  0,  LootPossibility.SemiRare,  7), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Soul Orb",  0,  LootPossibility.SemiRare,  0), new Loot("Double Axe",  0,  LootPossibility.Rare,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Rare,  0), new Loot("Magma Monocle",  0,  LootPossibility.Rare,  0), new Loot("Magma Coat",  0,  LootPossibility.Rare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DiblisTheFair = new CreatureData("Diblis The Fair", 0, 1800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Not in my face you barbarian!",  "I envy you to be slain by someone as beautiful as me.",  "I will drain your ugly corpses of the last drop of blood.",  "My brides will feast on your souls!",  },
          new List<Loot>() { new Loot("gold",  0,  LootPossibility.Normal,  99), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  5), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Ring of Healing",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Vampire Shield",  0,  LootPossibility.Rare,  0), new Loot("Spellbook of Lost Souls",  0,  LootPossibility.VeryRare,  0), new Loot("Vampire Lord Token",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Dipthrah = new CreatureData("Dipthrah", 4200, 2900, 0, 0, 1400, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() {  },
          new List<string>() { "You can't escape death forever",  "Come closer to learn the final lesson",  "Undeath will shatter my shackles.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  158), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  3), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Rare,  0), new Loot("Blue Gem",  0,  LootPossibility.Rare,  0), new Loot("Ankh",  0,  LootPossibility.VeryRare,  0), new Loot("Skull Staff",  0,  LootPossibility.VeryRare,  0), new Loot("Pharaoh Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Ornamented Ankh",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData DirePenguin = new CreatureData("Dire Penguin", 173, 119, 0, 0, 115, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  50), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Grrrrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Fish",  3578,  LootPossibility.Normal,  4), new Loot("Green Perch",  0,  LootPossibility.Rare,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Rare,  0), new Loot("Dragon Hammer",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Dirtbeard = new CreatureData("Dirtbeard", 630, 375, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "You are no match for the scourge of the seas!",  "You move like a seasick whale!",  "Yarr,  death to all l, lubbers!",  },
          new List<Loot>() { new Loot("Odd Hat",  0,  LootPossibility.Normal,  0), new Loot("The Shield Nevermourn",  0,  LootPossibility.Normal,  0), new Loot("Pointed Rabbitslayer or Helmet of Nature",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DiseasedBill = new CreatureData("Diseased Bill", 0, 300, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  25), new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Ice,  25)},
          new List<DamageModifier>() {  },
          new List<string>() { "People like you are the plague ,  I'll be the cure!",  "You all will pay for not helping me!",  "Cough! Cough!",  "Desolate! Everything is so desolate!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  25)}
      );
      public static CreatureData DiseasedDan = new CreatureData("Diseased Dan", 0, 300, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  85), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Where... Where am I",  "Is that you,  Tom",  "Phew,  what an awful smell ... oh,  that's me.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DiseasedFred = new CreatureData("Diseased Fred", 0, 300, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  15), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  15)},
          new List<DamageModifier>() {  },
          new List<string>() { "You will suffer the same fate as I do!",  "The pain! The pain!",  "Stay away! I am contagious!",  "The plague will get you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  17)}
      );
      public static CreatureData DoctorPerhaps = new CreatureData("Doctor Perhaps", 475, 325, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5) },
          new List<string>() { "I might use some parts of you in my next creation!",  "You're only a testsubject to me!",  "My creations will kill you!",  "You can't beat what you can't comprehend!",  },
          new List<Loot>() { new Loot("Mighty Helm of Green Sparks",  0,  LootPossibility.Normal,  0), new Loot("Trousers of the ancients",  0,  LootPossibility.Normal,  0), new Loot("Meat shield",  0,  LootPossibility.Normal,  0), new Loot("Glutton's Mace",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Dog = new CreatureData("Dog", 20, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Wuff wuff",  },
          new List<Loot>() { }
      );
      public static CreatureData DoomDeer = new CreatureData("Doom Deer", 405, 200, 0, 0, 155, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I bet it was you who killed my mom!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100)}
      );
      public static CreatureData Dracola = new CreatureData("Dracola", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "DEATH CAN'T STOP MY HUNGER!",  "Your new name is breakfast",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  210), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  3), new Loot("Dark Armor",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Hardened Bone",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Skull Helmet",  0,  LootPossibility.Normal,  0), new Loot("Reaper's Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Dracola's Eye",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Dragon = new CreatureData("Dragon", 1000, 700, 0, 0, 400, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "FCHHHH",  "GROOOOAAAAAAAAR",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  110), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  3), new Loot("Burst Arrow",  3449,  LootPossibility.Normal,  12), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.SemiRare,  0), new Loot("Steel Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Broadsword",  0,  LootPossibility.SemiRare,  0), new Loot("Plate Legs",  0,  LootPossibility.SemiRare,  0), new Loot("Double Axe",  0,  LootPossibility.Rare,  0), new Loot("Dragon Hammer",  0,  LootPossibility.Rare,  0), new Loot("Green Dragon Leather",  5877,  LootPossibility.Rare,  0), new Loot("Green Dragon Scale",  5920,  LootPossibility.Rare,  0), new Loot("Serpent Sword",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Dragon Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Dragonbone Staff",  0,  LootPossibility.VeryRare,  0), new Loot("Small Diamond",  0,  LootPossibility.VeryRare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DragonHatchling = new CreatureData("Dragon Hatchling", 380, 185, 0, 0, 200, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  75)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Fchu",  "Rooawwrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DragonLord = new CreatureData("Dragon Lord", 1900, 2100, 0, 0, 760, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "YOU WILL BURN!",  "ZCHHHHHHH",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  250), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  5), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Gemmed Book",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Power Bolt",  3450,  LootPossibility.Normal,  7), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Royal spear",  7378,  LootPossibility.Normal,  3), new Loot("Strange Helmet",  0,  LootPossibility.Rare,  0), new Loot("Red Dragon Scale",  5882,  LootPossibility.Normal,  0), new Loot("Red Dragon Leather",  5948,  LootPossibility.Rare,  0), new Loot("Fire Sword",  0,  LootPossibility.Rare,  0), new Loot("Tower Shield",  0,  LootPossibility.Rare,  0), new Loot("Dragon Slayer",  0,  LootPossibility.VeryRare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Royal Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Dragon Scale Mail",  0,  LootPossibility.VeryRare,  0), new Loot("Dragon Lord Trophy",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DragonLordHatchling = new CreatureData("Dragon Lord Hatchling", 750, 645, 0, 0, 335, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Fchu",  "Rooawwrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  165), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  0), new Loot("Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Rare,  1), new Loot("Magma Boots",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Drasilla = new CreatureData("Drasilla", 1320, 700, 0, 0, 390, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "FCHHHHHHHH",  "GROOOOAAAAAAAAR",  "DIRTY LITTLE HUMANS",  "YOU CAN'T KEEP ME HERE FOREVER",  },
          new List<Loot>() { }
      );
      public static CreatureData Dreadbeast = new CreatureData("Dreadbeast", 800, 250, 0, 0, 140, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Fire,  55), new DamageModifier(DamageType.Energy,  15), new DamageModifier(DamageType.Ice,  40), new DamageModifier(DamageType.Drown,  75)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  50) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coins",  0,  LootPossibility.Normal,  80), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Rare,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Rare,  0), new Loot("Green Mushroom",  3732,  LootPossibility.VeryRare,  0), new Loot("Hardened Bone",  0,  LootPossibility.VeryRare,  0), new Loot("Bone Club",  0,  LootPossibility.SemiRare,  0), new Loot("Bone Shield",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Dryad = new CreatureData("Dryad", 310, 190, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Fire,  20) },
          new List<string>() { "Feel the wrath of mother Tibia!",  "Defiler of nature!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("White Mushroom",  3723,  LootPossibility.Normal,  2), new Loot("Seeds",  0,  LootPossibility.SemiRare,  3), new Loot("Flower Dress",  0,  LootPossibility.SemiRare,  0), new Loot("Flower Wreath",  0,  LootPossibility.SemiRare,  0), new Loot("Leaf Legs",  0,  LootPossibility.SemiRare,  0), new Loot("Coconut Shoes",  0,  LootPossibility.Normal,  0), new Loot("Orange Mushrooms",  0,  LootPossibility.Rare,  2)}
      );
      public static CreatureData Dwarf = new CreatureData("Dwarf", 90, 45, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Hail Durin!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  8), new Loot("White Mushrooms",  0,  LootPossibility.Normal,  2), new Loot("Hatchet",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Letter",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Copper Shield",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.VeryRare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DwarfDispenser = new CreatureData("Dwarf Dispenser", 0, 0, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill it",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DwarfGeomancer = new CreatureData("Dwarf Geomancer", 380, 265, 0, 0, 210, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  60), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Earth is the strongest element.",  "Dust to dust.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  35), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Pears",  0,  LootPossibility.Normal,  7), new Loot("Studded Legs",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("White Mushroom",  3723,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.SemiRare,  0), new Loot("Clerical Mace",  0,  LootPossibility.Rare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Rare,  0), new Loot("Small Sapphire",  0,  LootPossibility.VeryRare,  0), new Loot("Iron Ore",  0,  LootPossibility.VeryRare,  0), new Loot("Terra Boots",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DwarfGuard = new CreatureData("Dwarf Guard", 245, 165, 0, 0, 140, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Hail Durin!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("White Mushrooms",  0,  LootPossibility.Normal,  2), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Double Axe",  0,  LootPossibility.Rare,  0), new Loot("Iron Ore",  0,  LootPossibility.Rare,  0), new Loot("Axe Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DwarfHenchman = new CreatureData("Dwarf Henchman", 350, 15, 0, 0, 94, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  15), new DamageModifier(DamageType.Ice,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "This place is for our eyes only!",  "We will live ,  let you die!",  "I will die another day!",  "We have license to kill!",  },
          new List<Loot>() { new Loot("None",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData DwarfMiner = new CreatureData("Dwarf Miner", 120, 60, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Work,  work!",  "Intruders in the mines!",  "Mine,  all mine!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DwarfSoldier = new CreatureData("Dwarf Soldier", 135, 70, 0, 0, 130, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Hail Durin!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Bolts",  0,  LootPossibility.Normal,  7), new Loot("White Mushrooms",  0,  LootPossibility.Normal,  3), new Loot("Soldier Helmet",  0,  LootPossibility.Normal,  0), new Loot("Shovel",  3457,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Dwarven Shield",  0,  LootPossibility.Normal,  0), new Loot("Piercing Bolts",  0,  LootPossibility.SemiRare,  3), new Loot("Battle Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Iron Ore",  0,  LootPossibility.Rare,  0), new Loot("Axe Ring",  0,  LootPossibility.Rare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData DworcFleshhunter = new CreatureData("Dworc Fleshhunter", 85, 40, 0, 0, 41, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  15), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Grow truk grrrrr.",  "Brak brrretz!",  "Prek tars,  dekklep zurk.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  14), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Skulls",  0,  LootPossibility.Normal,  3), new Loot("Cleaver",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Tribal Mask",  0,  LootPossibility.SemiRare,  0), new Loot("Hunting Spear",  3347,  LootPossibility.SemiRare,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.SemiRare,  0), new Loot("Poison Dagger",  0,  LootPossibility.SemiRare,  0), new Loot("Bone Shield",  0,  LootPossibility.Rare,  0), new Loot("Ripper Lance",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData DworcVenomsniper = new CreatureData("Dworc Venomsniper", 80, 35, 0, 0, 17, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Ice,  13) },
          new List<string>() { "Grow truk grrrrr.",  "Brak brrretz!",  "Prek tars,  dekklep zurk.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  14), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Skulls",  0,  LootPossibility.Normal,  3), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Poison Arrows",  0,  LootPossibility.Normal,  3), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Throwing Knives",  0,  LootPossibility.Normal,  2), new Loot("Poison Dagger",  0,  LootPossibility.SemiRare,  0), new Loot("Tribal Mask",  0,  LootPossibility.SemiRare,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.Rare,  0), new Loot("Bast Skirt",  0,  LootPossibility.VeryRare,  0), new Loot("Seeds",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData DworcVoodoomaster = new CreatureData("Dworc Voodoomaster", 80, 55, 0, 0, 90, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Grow truk grrrrr.",  "Brak brrretz!",  "Prek tars,  dekklep zurk.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Skull",  0,  LootPossibility.Normal,  3), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.SemiRare,  0), new Loot("Poison Dagger",  0,  LootPossibility.SemiRare,  0), new Loot("Tribal Mask",  0,  LootPossibility.SemiRare,  0), new Loot("Strange Symbol",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Voodoo Doll",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData EarthElemental = new CreatureData("Earth Elemental", 650, 450, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  50), new DamageModifier(DamageType.Ice,  85)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  25) },
          new List<string>() { "Stomp.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Earth Arrows",  0,  LootPossibility.Normal,  30), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Small Stones",  0,  LootPossibility.SemiRare,  10), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Small Topaz",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData EarthOverlord = new CreatureData("Earth Overlord", 4000, 2800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  168), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  2), new Loot("Small Stones",  0,  LootPossibility.Normal,  15), new Loot("Mother Soil",  0,  LootPossibility.Normal,  0), new Loot("Terra Mantle",  0,  LootPossibility.Rare,  0), new Loot("Swamplair Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData EclipseKnight = new CreatureData("Eclipse Knight", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Create loot statistics)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Efreet = new CreatureData("Efreet", 550, 410, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  90), new DamageModifier(DamageType.Energy,  60), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  8), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "I grant you a deathwish!",  "Good wishes are for fairytales",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  125), new Loot("Pear",  0,  LootPossibility.Normal,  12), new Loot("Royal Spear",  7378,  LootPossibility.Normal,  3), new Loot("Heavy Machete",  0,  LootPossibility.Normal,  0), new Loot("Small Oil Lamp",  0,  LootPossibility.Normal,  0), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  4), new Loot("Seeds",  0,  LootPossibility.SemiRare,  2), new Loot("Green Tapestry",  0,  LootPossibility.Rare,  0), new Loot("Green Piece of Cloth",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Rare,  0), new Loot("Mystic Turban",  0,  LootPossibility.Rare,  0), new Loot("Magma Monocle",  0,  LootPossibility.Rare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Green Gem",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData ElderBeholder = new CreatureData("Elder Beholder", 500, 280, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  30), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Inferior creatures,  bow before my power!",  "Let me take a look at you!",  "659978 54764!",  "653768764!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  105), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Sniper Arrow",  7364,  LootPossibility.SemiRare,  5), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Beholder Eye",  5898,  LootPossibility.Rare,  0), new Loot("Terra Rod",  0,  LootPossibility.Rare,  0), new Loot("Beholder Shield",  0,  LootPossibility.Rare,  0), new Loot("Small Emerald",  3032,  LootPossibility.VeryRare,  0), new Loot("Beholder Helmet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Elephant = new CreatureData("Elephant", 320, 160, 0, 0, 100, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Hooooot-Toooooot!",  "Tooooot!",  "Trooooot!",  },
          new List<Loot>() { new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  3), new Loot("Tusk",  0,  LootPossibility.Rare,  2), new Loot("Tusk Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Elf = new CreatureData("Elf", 100, 42, 0, 0, 40, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Death to the Defilers!",  "You are not welcome here.",  "Flee as long as you can.",  "Bahaha aka!",  "Ulathil beia Thratha!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Arrows",  0,  LootPossibility.Normal,  6), new Loot("Red Apples",  0,  LootPossibility.Normal,  2), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Heaven Blossom",  5921,  LootPossibility.Rare,  0)}
      );
      public static CreatureData ElfArcanist = new CreatureData("Elf Arcanist", 220, 175, 0, 0, 220, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Fire,  50), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { "I'll bring balance upon you!",  "Vihil Ealuel",  "For the Daughter of the Stars!",  "Tha'shi Cenath",  "Feel my wrath!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  49), new Loot("Arrows",  0,  LootPossibility.Normal,  3), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Melon",  0,  LootPossibility.Normal,  0), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("Green Tunic",  0,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Inkwell",  0,  LootPossibility.Normal,  0), new Loot("Sling Herb",  0,  LootPossibility.Normal,  0), new Loot("C",  0,  LootPossibility.Normal,  0), new Loot("lestick",  0,  LootPossibility.Normal,  0), new Loot("Elven Amulet",  3082,  LootPossibility.SemiRare,  0), new Loot("Holy Orchids",  0,  LootPossibility.Rare,  2), new Loot("Strong Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0), new Loot("Yellow Gem",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData ElfScout = new CreatureData("Elf Scout", 160, 75, 0, 0, 110, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Tha'shi Ab'Dendriel!",  "Evicor guide my arrow!",  "Your existence will end here!",  "Feel the sting of my arrows!",  "Thy blood will quench the soil's thirst!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Arrow",  3447,  LootPossibility.Normal,  15), new Loot("Poison Arrow",  0,  LootPossibility.Normal,  6), new Loot("Grape",  0,  LootPossibility.Normal,  0), new Loot("Waterskin",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Bow",  0,  LootPossibility.SemiRare,  0), new Loot("Heaven Blossom",  5921,  LootPossibility.Rare,  0), new Loot("Elvish Bow",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData EnergyElemental = new CreatureData("Energy Elemental", 500, 550, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  55), new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Death,  5)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  15) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  170), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Throwing Stars",  0,  LootPossibility.Normal,  5), new Loot("Flash Arrow",  761,  LootPossibility.Normal,  10), new Loot("Small Amethysts",  0,  LootPossibility.SemiRare,  3), new Loot("Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Obsidian Lance",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Rare,  0), new Loot("Crystal Ring",  0,  LootPossibility.Rare,  0), new Loot("Energy Ring",  0,  LootPossibility.Rare,  0), new Loot("Guardian Shield",  0,  LootPossibility.VeryRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData EnergyOverlord = new CreatureData("Energy Overlord", 4000, 2800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Pure Energy",  0,  LootPossibility.Normal,  0), new Loot("Voltage Armor",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData EnlightenedoftheCult = new CreatureData("Enlightened of the Cult", 700, 500, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "Praise to my master Urgith!",  "You will rise as my servant!",  "Praise to my masters! Long live the triangle!",  "You will die in the name of the triangle!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  80), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  0), new Loot("Pirate Voodoo Doll",  0,  LootPossibility.SemiRare,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("semi-rare)",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Rare,  0), new Loot("Skull Staff",  0,  LootPossibility.Rare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Rare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0), new Loot("Blue Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Key Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Mysterious Voodoo Skull",  0,  LootPossibility.VeryRare,  0), new Loot("Amber Staff",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData EnragedBookworm = new CreatureData("Enraged Bookworm", 145, 83, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("none turns into a dead worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Esmeralda = new CreatureData("Esmeralda", 800, 600, 0, 0, 273, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  145), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  4), new Loot("Skull",  0,  LootPossibility.Always,  0), new Loot("Ring of Healing",  0,  LootPossibility.Always,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Epee",  0,  LootPossibility.Normal,  0), new Loot("Tower Shield",  0,  LootPossibility.Normal,  0), new Loot("Terra Mantle",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData EssenceofDarkness = new CreatureData("Essence of Darkness", 0, 30, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death, DamageType.Energy, DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  80), new DamageModifier(DamageType.Fire,  90)},
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData EvilMastermind = new CreatureData("Evil Mastermind", 1295, 675, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5) },
          new List<string>() { "You won't stop my masterplan to flood the world market with fake beholder language dictionaries!",  "My calculations tell me you'll die!",  "You can't stop me!",  "Beware! My evil monolog is coming!",  },
          new List<Loot>() { new Loot("Fan Club Membership Card",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData EvilSheep = new CreatureData("Evil Sheep", 350, 240, 0, 0, 140, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Grrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50)}
      );
      public static CreatureData EvilSheepLord = new CreatureData("Evil Sheep Lord", 400, 340, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "You can COUNT on us!",  "Maeh!",  "I feel you're getting sleepy! Maeh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("expect more loot",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData EyeoftheSeven = new CreatureData("Eye of the Seven", 0, 0, 0, 0, 500, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData FahimTheWise = new CreatureData("Fahim The Wise", 2000, 1500, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "You should know better than to be an enemy of the Marid",  "",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  62), new Loot("Blueberry",  0,  LootPossibility.Normal,  22), new Loot("Small Oil Lamp",  0,  LootPossibility.Normal,  0), new Loot("Blue Pieces of Cloth",  0,  LootPossibility.Normal,  4), new Loot("Strong Mana Potions",  0,  LootPossibility.Normal,  3), new Loot("Royal Spears",  0,  LootPossibility.Normal,  2), new Loot("Wooden Flute",  0,  LootPossibility.Normal,  0), new Loot("Heavy Machete",  0,  LootPossibility.Normal,  0), new Loot("Mystic Turban",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Rare,  0), new Loot("Magma Monocle",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData FallenMooh = new CreatureData("Fallen Mooh", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Fernfang = new CreatureData("Fernfang", 400, 600, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  70), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() {  },
          new List<string>() { "You desacrated this place!",  "Yoooohuuuu!",  "I will cleanse this isle!",  "Grrrrrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  26), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Brown Flask",  0,  LootPossibility.Normal,  0), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("Sling Herb",  0,  LootPossibility.Normal,  0), new Loot("Star Herb",  0,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Plate",  0,  LootPossibility.Normal,  0), new Loot("Lamp",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  2), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Ankh",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.Normal,  0), new Loot("Dirty Fur",  0,  LootPossibility.Normal,  0), new Loot("Green Tunic",  0,  LootPossibility.Normal,  0), new Loot("Wooden Whistle",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Ferumbras = new CreatureData("Ferumbras", 35000, 12000, 0, 0, 2400, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  90)},
          new List<DamageModifier>() {  },
          new List<string>() { "NO ONE WILL STOP ME THIS TIME!",  "THE POWER IS MINE!",  "I returned from death ,  you dream about defeating me",  "Witness the first seconds of my eternal world domination!",  "Even in my weakened state I will crush you all!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  270), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  75), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  92), new Loot("Small Rubies",  0,  LootPossibility.Normal,  49), new Loot("White Pearls",  0,  LootPossibility.Normal,  8), new Loot("Small Diamonds",  0,  LootPossibility.Normal,  90), new Loot("Small Topaz",  0,  LootPossibility.Normal,  86), new Loot("Gold Ingots",  0,  LootPossibility.Normal,  2), new Loot("Talon",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Normal,  0), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Emerald Bangle",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Golden Armor",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Jade Hammer",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Teddy Bear",  0,  LootPossibility.Normal,  0), new Loot("Thunder Hammer",  0,  LootPossibility.Normal,  0), new Loot("Magic Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Lost Souls",  0,  LootPossibility.Normal,  0), new Loot("Spellscroll of Prophecies",  0,  LootPossibility.Normal,  0), new Loot("Glacier Kilt",  0,  LootPossibility.Normal,  0), new Loot("Lightning Legs",  0,  LootPossibility.Normal,  0), new Loot("Terra Legs",  0,  LootPossibility.Normal,  0), new Loot("Abyss Hammer",  0,  LootPossibility.Normal,  0), new Loot("Berserker",  0,  LootPossibility.Normal,  0), new Loot("Chaos Mace",  0,  LootPossibility.Normal,  0), new Loot("Queen's Sceptre",  0,  LootPossibility.Normal,  0), new Loot("Ornamented Axe",  0,  LootPossibility.Normal,  0), new Loot("Vile Axe",  0,  LootPossibility.Normal,  0), new Loot("Greenwood Coat",  0,  LootPossibility.Normal,  0), new Loot("Divine Plate",  0,  LootPossibility.Normal,  0), new Loot("Emerald Sword",  0,  LootPossibility.Normal,  0), new Loot("Obsidian Truncheon",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Dark Mysteries",  0,  LootPossibility.Normal,  0), new Loot("Skullcrusher",  0,  LootPossibility.Normal,  0), new Loot("Great Shield",  0,  LootPossibility.Normal,  0), new Loot("Red Tome",  0,  LootPossibility.Normal,  0), new Loot("Phoenix Shield",  0,  LootPossibility.Normal,  0), new Loot("Hellforged Axe",  0,  LootPossibility.Normal,  0), new Loot("Impaler",  0,  LootPossibility.Normal,  0), new Loot("Tempest Shield",  0,  LootPossibility.Normal,  0), new Loot("Velvet Mantle",  0,  LootPossibility.Normal,  0), new Loot("Demonrage Sword",  0,  LootPossibility.Normal,  0), new Loot("Bloody Edge",  0,  LootPossibility.Normal,  0), new Loot("Demonwing Axe",  0,  LootPossibility.Normal,  0), new Loot("Havoc Blade",  0,  LootPossibility.Normal,  0), new Loot("Nightmare Blade",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Runed Sword",  0,  LootPossibility.Normal,  0), new Loot("Ferumbras' Hat",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData FireDevil = new CreatureData("Fire Devil", 200, 145, 0, 0, 160, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Ice,  20) },
          new List<string>() { "Hot,  eh",  "Hell,  oh,  hell!",  },
          new List<Loot>() { new Loot("Torches",  0,  LootPossibility.Normal,  2), new Loot("Pitchfork",  0,  LootPossibility.Normal,  0), new Loot("Cleaver",  0,  LootPossibility.Normal,  0), new Loot("Scimitar",  0,  LootPossibility.SemiRare,  0), new Loot("Blank Rune",  0,  LootPossibility.SemiRare,  0), new Loot("Double Axe",  0,  LootPossibility.Rare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.VeryRare,  0), new Loot("Guardian Shield",  0,  LootPossibility.VeryRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Dragonbreath",  0,  LootPossibility.VeryRare,  0), new Loot("Necrotic Rod",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData FireElemental = new CreatureData("Fire Elemental", 280, 220, 0, 0, 300, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  25) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData FireOverlord = new CreatureData("Fire Overlord", 4000, 2800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  125), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Eternal Flames",  0,  LootPossibility.Normal,  0), new Loot("Lavos Armor",  0,  LootPossibility.Rare,  0), new Loot("Magma Coat",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Flamethrower = new CreatureData("Flamethrower", 0, 0, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Flamingo = new CreatureData("Flamingo", 25, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Fluffy = new CreatureData("Fluffy", 4500, 3550, 0, 0, 750, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Wooof!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  109), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  8), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  4), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  0), new Loot("Knight Axe",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Spike Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ForemanKneebiter = new CreatureData("Foreman Kneebiter", 570, 445, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Freegoiz = new CreatureData("Freegoiz", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Yooodelaaahooohooo",  },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData FrostDragon = new CreatureData("Frost Dragon", 1800, 2100, 0, 0, 600, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  10)},
          new List<DamageModifier>() {  },
          new List<string>() { "YOU WILL FREEZE!",  "ZCHHHHH!",  "I am so cool",  "Chill out!.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  350), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  5), new Loot("Gemmed Book",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  0), new Loot("Ice Cube",  0,  LootPossibility.Normal,  0), new Loot("Power Bolt",  3450,  LootPossibility.Normal,  6), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Strange Helmet",  0,  LootPossibility.Rare,  0), new Loot("Ice Rapier",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.SemiRare,  0), new Loot("Shard",  0,  LootPossibility.Rare,  0), new Loot("Tower Shield",  0,  LootPossibility.Rare,  0), new Loot("Dragon Slayer",  0,  LootPossibility.VeryRare,  0), new Loot("Royal Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Dragon Scale Mail",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData FrostDragonHatchling = new CreatureData("Frost Dragon Hatchling", 800, 745, 0, 0, 380, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Rooawwrr",  "Fchu",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  55), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  0), new Loot("Spellbook of Enlightenment",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData FrostGiant = new CreatureData("Frost Giant", 270, 150, 0, 0, 200, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Hmm Humansoup!",  "St,  still ya tasy snack!",  "Joh Thun!",  "Hörre Sjan Flan!",  "Bröre Smöde!",  "Forle Bramma",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Ice Cube",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Battle Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Halberd",  0,  LootPossibility.Rare,  0), new Loot("Norse Shield",  0,  LootPossibility.Rare,  0), new Loot("Dark Helmet",  0,  LootPossibility.Rare,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Shard",  0,  LootPossibility.VeryRare,  0), new Loot("Club Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData FrostGiantess = new CreatureData("Frost Giantess", 275, 150, 0, 0, 150, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Ymirs Mjalle!",  "No run so much,  must stay fat!",  "Hörre Sjan Flan!",  "Damned fast food.",  "Come kiss the cook!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Ice Cube",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Norse Shield",  0,  LootPossibility.Normal,  0), new Loot("Small Stones",  0,  LootPossibility.SemiRare,  10), new Loot("Wolf Tooth Chain",  0,  LootPossibility.SemiRare,  0), new Loot("Club Ring",  0,  LootPossibility.Rare,  0), new Loot("Halberd",  0,  LootPossibility.Rare,  0), new Loot("Dark Helmet",  0,  LootPossibility.Rare,  0), new Loot("Shard",  0,  LootPossibility.Rare,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Glacier Shoes",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData FrostTroll = new CreatureData("Frost Troll", 55, 23, 0, 0, 20, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  15), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Brrr",  "Broar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  12), new Loot("Fish",  3578,  LootPossibility.Normal,  1), new Loot("Club",  0,  LootPossibility.Normal,  0), new Loot("Rapier",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Coat",  0,  LootPossibility.Normal,  0), new Loot("Twigs",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Frostfur = new CreatureData("Frostfur", 65, 35, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData FuriousTroll = new CreatureData("Furious Troll", 245, 185, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Slice! Slice!",  "DIE!!!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  179), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  1), new Loot("War Hammer",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Fury = new CreatureData("Fury", 4100, 4500, 0, 0, 0, false, true, FrontAttack.Wave, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Ahhhhrrrr!",  "Waaaaah!",  "Carnage!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("platinum coins",  0,  LootPossibility.Normal,  2), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  3), new Loot("Orichalcum Pearls",  0,  LootPossibility.Normal,  3), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  3), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Terra Rod",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Noble Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Red Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Great Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Legs",  0,  LootPossibility.Normal,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Assassin Star",  0,  LootPossibility.Rare,  1), new Loot("Steel Boots",  0,  LootPossibility.Rare,  0), new Loot("Crystal Ring",  0,  LootPossibility.Rare,  0), new Loot("Red Gem",  0,  LootPossibility.Rare,  0), new Loot("Assassin Dagger",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Legs",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Gamemaster = new CreatureData("Gamemaster", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GangMember = new CreatureData("Gang Member", 295, 70, 0, 0, 95, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "This is our territory!",  "Help me guys!",  "I don't like the way you look!",  "You're wearing the wrong colours!",  "Don't mess with us!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  23), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Brown Bread",  3602,  LootPossibility.Normal,  0), new Loot("Torches",  0,  LootPossibility.Normal,  2), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Studded Legs",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Club Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Gargoyle = new CreatureData("Gargoyle", 250, 150, 0, 0, 65, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  40), new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Feel my claws,  softskin!",  "There is a stone in your shoe",  "Stone sweet stone",  "Harrr harrr!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Small Stone",  1781,  LootPossibility.Normal,  10), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Studded Club",  0,  LootPossibility.Normal,  0), new Loot("Dark Armor",  0,  LootPossibility.Rare,  0), new Loot("Morning Star",  0,  LootPossibility.Rare,  0), new Loot("Steel Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.VeryRare,  0), new Loot("Club Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Gazer = new CreatureData("Gazer", 120, 90, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  11), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Mommy!",  "Buuuuhaaaahhaaaaa!",  "Me need mana!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GeneralMurius = new CreatureData("General Murius", 550, 450, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "You will get what you deserve!",  "Feel the power of the Mooh'Tah!",  "For the king!",  "Guards!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  38), new Loot("Power Bolts",  0,  LootPossibility.Normal,  8), new Loot("Piercing Bolts",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Shovel",  3457,  LootPossibility.Normal,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Bolts",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Legs",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Soldier Helmet",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Hatchet",  0,  LootPossibility.Normal,  0), new Loot("Fishing Rod",  3483,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Dead Snake",  0,  LootPossibility.Normal,  0), new Loot("Carrots",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0), new Loot("Dwarven Helmet",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Ghazbaran = new CreatureData("Ghazbaran", 60000, 15000, 0, 0, 5775, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "COME AND GIVE ME SOME AMUSEMENT",  "IS THAT THE BEST YOU HAVE TO OFFER,  TIBIANS",  "I AM GHAZBARAN OF THE TRIANGLE... AND I AM HERE TO CHALLENGE YOU ALL.",  "FLAWLESS VICTORY!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  200), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  73), new Loot("Crystal Coins",  0,  LootPossibility.Normal,  2), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  15), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  6), new Loot("Small Diamonds",  0,  LootPossibility.Normal,  5), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  4), new Loot("Talons",  0,  LootPossibility.Normal,  4), new Loot("White Pearls",  0,  LootPossibility.Normal,  15), new Loot("Black Pearls",  0,  LootPossibility.Normal,  15), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  5), new Loot("Demon Horns",  0,  LootPossibility.Normal,  2), new Loot("Onyx Arrows",  0,  LootPossibility.Normal,  55), new Loot("Assassin Stars",  0,  LootPossibility.Normal,  47), new Loot("Green Gem",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Normal,  0), new Loot("Orb",  0,  LootPossibility.Normal,  0), new Loot("Oceanborn Leviathan Armor",  0,  LootPossibility.Normal,  0), new Loot("Robe of the Ice Queen",  0,  LootPossibility.Normal,  0), new Loot("Frozen Plate",  0,  LootPossibility.Normal,  0), new Loot("Crystalline Armor",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Spellscroll of Prophecies",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Warding",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Dark Mysteries",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Lost Souls",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Twin Axe",  0,  LootPossibility.Normal,  0), new Loot("Glorious Axe",  0,  LootPossibility.Normal,  0), new Loot("Havoc Blade",  0,  LootPossibility.Normal,  0), new Loot("Bonebreaker",  0,  LootPossibility.Normal,  0), new Loot("Demonbone",  0,  LootPossibility.Normal,  0), new Loot("Thunder Hammer",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Golden Boots",  0,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Strange Symbol",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Ancient Amulet",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Berserk Potion",  0,  LootPossibility.Normal,  0), new Loot("Morgaroth's Heart",  0,  LootPossibility.Normal,  0), new Loot("Teddy Bear",  0,  LootPossibility.Normal,  0), new Loot("Blue Tome",  0,  LootPossibility.Normal,  0), new Loot("Ruthless Axe",  0,  LootPossibility.Normal,  0), new Loot("Ravenwing",  0,  LootPossibility.Normal,  0), new Loot("Mythril Axe",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Ghost = new CreatureData("Ghost", 150, 120, 0, 0, 125, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Huh!",  "Shhhhhh",  "Buuuuuh",  },
          new List<Loot>() { new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Shadow Herb",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Orange Book",  0,  LootPossibility.SemiRare,  0), new Loot("White Piece of Cloth",  0,  LootPossibility.Rare,  0), new Loot("Ancient Shield",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GhostlyApparition = new CreatureData("Ghostly Apparition", 0, 0, 0, 0, 6, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Fresh life spirit.... aaahhh.... let me feeeeed...",  "Theeeese waaalls.... hold unholy secretsss....",  "Saaaaave ussss.....",  "Aaahhhh.....",  "",  },
          new List<Loot>() { }
      );
      public static CreatureData Ghoul = new CreatureData("Ghoul", 100, 85, 0, 0, 97, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Worms",  0,  LootPossibility.Normal,  6), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Skulls",  0,  LootPossibility.SemiRare,  2), new Loot("Scale Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Brown Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Life Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GiantSpider = new CreatureData("Giant Spider", 1300, 900, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  115), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Poison Arrows",  0,  LootPossibility.Normal,  12), new Loot("Spider Silk",  0,  LootPossibility.Rare,  0), new Loot("Time Ring",  0,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Knight Armor",  0,  LootPossibility.Rare,  0), new Loot("Knight Legs",  0,  LootPossibility.Rare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.VeryRare,  0), new Loot("Lightning Headb",  0,  LootPossibility.Normal,  0), new Loot("very rare)",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Gladiator = new CreatureData("Gladiator", 185, 90, 0, 0, 90, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  15), new DamageModifier(DamageType.Holy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "You are no match for me!",  "Feel my prowess",  "Fight!",  "Take this!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Steel Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Belted Cape",  0,  LootPossibility.Rare,  0), new Loot("Iron Helmet",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Gloombringer = new CreatureData("Gloombringer", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GnorreChyllson = new CreatureData("Gnorre Chyllson", 7100, 4000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "I am like the merciless northwind.",  "Snow will be your death shroud.",  "Feel the wrath of father chyll!",  },
          new List<Loot>() { }
      );
      public static CreatureData Goblin = new CreatureData("Goblin", 50, 25, 0, 0, 35, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Zig Zag! Gobo attack!",  "Me green,  me mean!",  "Bugga! Bugga!",  "Help! Goblinkiller!",  "Me have him!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  9), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Small Axe",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Small Stone",  1781,  LootPossibility.Rare,  0)}
      );
      public static CreatureData GoblinAssassin = new CreatureData("Goblin Assassin", 75, 52, 0, 0, 50, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Goblin Powahhh!",  "Me kill you!",  "Me green menace!",  "Gobabunga!",  "Gooobliiiins!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  9), new Loot("Small Stones",  0,  LootPossibility.Normal,  4), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Small Axe",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GoblinLeader = new CreatureData("Goblin Leader", 50, 75, 0, 0, 95, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Go go,  Gobo attack!!",  "Me the greenest ,  the meanest!",  "Me have power to crush you!",  "Goblinkiller! Catch him!!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Small Stones",  0,  LootPossibility.Normal,  4), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Small Axe",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GoblinScavenger = new CreatureData("Goblin Scavenger", 60, 37, 0, 0, 75, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Shiny,  shiny!",  "You mean!",  "All mine!",  "Uhh!",  "Gimme gimme!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  9), new Loot("Small Stone",  1781,  LootPossibility.Normal,  4), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Small Axe",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Golgordan = new CreatureData("Golgordan", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Holy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Latrivan,  you fool!",  "We are the right h,  ,  the left h,  of the seven!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  234), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  8), new Loot("White Pearl",  3026,  LootPossibility.Normal,  11), new Loot("Small Emerald",  3032,  LootPossibility.Normal,  8), new Loot("Small Diamond  0-6 Onyx Arrow",  0,  LootPossibility.Normal,  4), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Silver Dagger",  0,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Giant Sword",  0,  LootPossibility.Normal,  0), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Gozzler = new CreatureData("Gozzler", 240, 180, 0, 0, 245, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5) },
          new List<string>() { "Huhuhuhuuu!",  "Let the fun begin!",  "Yihahaha!",  "I'll bite you! Nyehehehe!",  "Nyarnyarnyarnyar.",  "",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Brown Flask",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Battle Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Serpent Sword",  0,  LootPossibility.Rare,  0), new Loot("Clerical Mace",  0,  LootPossibility.Rare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GrandfatherTridian = new CreatureData("Grandfather Tridian", 1800, 1400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  35)},
          new List<DamageModifier>() {  },
          new List<string>() { "I will bring peace to your misguided soul!",  "Your intrusion can't be tolerated!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  80), new Loot("Piggy Bank",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Brown Mushroom",  3725,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Voodoo Doll",  0,  LootPossibility.Normal,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Voodoo",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData GravelordOshuran = new CreatureData("Gravelord Oshuran", 3100, 2400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  80), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Your mortality is disgusting!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  97), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("White Pearl",  3026,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.Normal,  0), new Loot("Lightning Boots",  0,  LootPossibility.Normal,  0), new Loot("Spellscroll of Prophecies",  0,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData GreenDjinn = new CreatureData("Green Djinn", 330, 215, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Fire,  80), new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  13), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Good wishes are for fairytales",  "I wish you a merry trip to hell!",  "Muahahahahaha",  "I grant you a deathwish!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  113), new Loot("Cheese",  3607,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Small Oil Lamp",  0,  LootPossibility.SemiRare,  0), new Loot("Royal Spear",  7378,  LootPossibility.SemiRare,  2), new Loot("Green Book",  0,  LootPossibility.SemiRare,  0), new Loot("Small Emerald",  3032,  LootPossibility.Rare,  4), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Green Piece of Cloth",  0,  LootPossibility.Rare,  0), new Loot("Mystic Turban",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GreenFrog = new CreatureData("Green Frog", 25, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  },
          new List<Loot>() { }
      );
      public static CreatureData GrimReaper = new CreatureData("Grim Reaper", 3900, 5500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  90), new DamageModifier(DamageType.Ice,  80), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Death!",  "Come a little closer!",  "The end is near!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  236), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  4), new Loot("Orichalcum Pearls",  0,  LootPossibility.Normal,  4), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  0), new Loot("Scythe",  3453,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Dark Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Glacier Kilt",  0,  LootPossibility.VeryRare,  0), new Loot("Underworld Rod",  0,  LootPossibility.VeryRare,  0), new Loot("Skullcracker Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Nightmare Blade",  0,  LootPossibility.VeryRare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GrimgorGuteater = new CreatureData("Grimgor Guteater", 1155, 670, 0, 0, 330, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Don't run,  You're burning off precious fat.",  },
          new List<Loot>() { }
      );
      public static CreatureData Grorlam = new CreatureData("Grorlam", 3000, 2400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  14), new Loot("Small Stone",  1781,  LootPossibility.Normal,  20), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Pick",  3456,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.Normal,  0), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Red Gem",  0,  LootPossibility.Normal,  0), new Loot("Carlin Sword",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Small Amethysts",  0,  LootPossibility.Rare,  2), new Loot("Steel Boots",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData GrynchClanGoblin = new CreatureData("Grynch Clan Goblin", 80, 4, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "T'was not me h,  in your pocket!",  "Look! Cool stuff in house. Let's get it!",  "Uhh! Nobody home! lt",  "chucklegt",  "",  "Me just borrowed it!",  "Me no steal! Me found it!",  "Me had it for five minutes. It's family heirloom now!",  "Nice human won't hurt little,  good goblin",  "Gimmegimme!",  "Invite me in you lovely house plx!",  "Other Goblin stole it!",  "All presents mine!",  "Me got ugly ones purse",  "Free itans plz!",  "Not me! Not me!",  "Guys,  help me here! Guys Guys",  "That only much dust in me pocket! Honest!",  "Can me have your stuff",  "Halp,  Big dumb one is after me!",  "Uh,  So many shiny things!",  "Utani hur hur hur!",  "Mee Stealing Never!!!",  "Oh what fun it is to steal a one-horse open sleigh!",  "Must have it! Must have it!",  "Where me put me lockpick",  "Catch me if you can!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  22), new Loot("Present Bag",  0,  LootPossibility.Normal,  0), new Loot("Snowballs",  0,  LootPossibility.Normal,  5), new Loot("Lump of Cake Dough",  0,  LootPossibility.Normal,  3), new Loot("Oranges",  0,  LootPossibility.Normal,  3), new Loot("Cookies",  0,  LootPossibility.Normal,  5), new Loot("Cherries",  0,  LootPossibility.Normal,  4), new Loot("Apples",  0,  LootPossibility.Normal,  3), new Loot("Eggs",  0,  LootPossibility.SemiRare,  2), new Loot("C",  0,  LootPossibility.Normal,  3), new Loot("y Canes",  0,  LootPossibility.SemiRare,  0), new Loot("Cream Cake",  0,  LootPossibility.SemiRare,  0), new Loot("Lute",  0,  LootPossibility.SemiRare,  0), new Loot("Blank Rune",  0,  LootPossibility.SemiRare,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("semi-rare)",  0,  LootPossibility.SemiRare,  0), new Loot("L",  0,  LootPossibility.Normal,  0), new Loot("scape Picture",  0,  LootPossibility.SemiRare,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.SemiRare,  0), new Loot("Scroll",  0,  LootPossibility.SemiRare,  0), new Loot("Gingerbreadman",  0,  LootPossibility.SemiRare,  2), new Loot("Scarf",  3572,  LootPossibility.SemiRare,  0), new Loot("Chicken Feathers",  0,  LootPossibility.SemiRare,  5), new Loot("Bat Wings",  0,  LootPossibility.SemiRare,  3), new Loot("Honeycomb",  0,  LootPossibility.SemiRare,  0), new Loot("Explorer Brooch",  0,  LootPossibility.SemiRare,  0), new Loot("Walnuts",  0,  LootPossibility.SemiRare,  5), new Loot("Peanuts",  0,  LootPossibility.Rare,  5), new Loot("Valentine's Cake",  0,  LootPossibility.Rare,  0), new Loot("Watch",  0,  LootPossibility.Rare,  0), new Loot("Broom",  0,  LootPossibility.Rare,  0), new Loot("Piggy Bank",  0,  LootPossibility.Rare,  0), new Loot("Dice",  0,  LootPossibility.Rare,  0), new Loot("Mirror",  0,  LootPossibility.Rare,  0), new Loot("various Pillows",  0,  LootPossibility.Rare,  0), new Loot("Flower Bowl",  0,  LootPossibility.Rare,  0), new Loot("Bottle",  0,  LootPossibility.Rare,  0), new Loot("Scarab Coin",  3042,  LootPossibility.Rare,  0), new Loot("Orichalcum Pearl",  0,  LootPossibility.Rare,  2), new Loot("Crystal Coin",  3043,  LootPossibility.VeryRare,  0), new Loot("Elvenhair Rope",  0,  LootPossibility.VeryRare,  0), new Loot("Vampire Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Hacker = new CreatureData("Hacker", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HairmanTheHuge = new CreatureData("Hairman The Huge", 600, 335, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Energy,  1), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coins",  0,  LootPossibility.Normal,  60), new Loot("Bananas",  0,  LootPossibility.Normal,  2), new Loot("Ape Fur",  5883,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HandofCursedFate = new CreatureData("Hand of Cursed Fate", 7500, 5000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Ice,  20) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  4), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Emerald Bangle",  0,  LootPossibility.Normal,  0), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  4), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Skull Staff",  0,  LootPossibility.SemiRare,  0), new Loot("Gold Ingot",  0,  LootPossibility.Rare,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Violet Gem",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Figurine",  0,  LootPossibility.VeryRare,  0), new Loot("Abyss Hammer",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData HarbringerofDarkness = new CreatureData("Harbringer of Darkness", 0, 0, 0, 0, 4066, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Create loot statistics)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HauntedTreeling = new CreatureData("Haunted Treeling", 450, 310, 0, 0, 300, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Knarrrz",  "Huuhuuhuuuhuuaarrr",  "Knorrrrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  96), new Loot("White Mushroom",  3723,  LootPossibility.Normal,  2), new Loot("Red Mushroom",  0,  LootPossibility.Normal,  0), new Loot("Wooden Trash",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Orange Mushroom",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Trunk Chair Kit",  0,  LootPossibility.SemiRare,  0), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Rare,  0), new Loot("Bullseye Potion",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData HellHole = new CreatureData("Hell Hole", 0, 0, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HellfireFighter = new CreatureData("Hellfire Fighter", 3800, 3900, 0, 0, 2749, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  165), new Loot("Burnt Scroll",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Small Ruby",  0,  LootPossibility.Normal,  4), new Loot("Orichalcum Pearl",  0,  LootPossibility.Normal,  2), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.SemiRare,  0), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("Emerald Bangle",  0,  LootPossibility.SemiRare,  0), new Loot("Fire Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Fire Axe",  0,  LootPossibility.Rare,  0), new Loot("Magma Legs",  0,  LootPossibility.Rare,  0), new Loot("Magma Coat",  0,  LootPossibility.VeryRare,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Hellgorak = new CreatureData("Hellgorak", 30000, 10000, 0, 0, 1800, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  98), new DamageModifier(DamageType.Holy,  98), new DamageModifier(DamageType.Death,  98), new DamageModifier(DamageType.Fire,  98), new DamageModifier(DamageType.Energy,  98), new DamageModifier(DamageType.Ice,  98), new DamageModifier(DamageType.Earth,  98)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Drown,  205) },
          new List<string>() { "I'll sacrifice yours souls to seven!",  "I'm bad news for you mortals!",  "No man can defeat me!",  "Your puny skills are no match for me.",  "I smell your fear.",  "Delicious!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  283), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  29), new Loot("Demonic Essence",  0,  LootPossibility.Always,  0), new Loot("Small Rubies",  0,  LootPossibility.Normal,  25), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  22), new Loot("Small Diamonds",  0,  LootPossibility.Normal,  21), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  25), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  21), new Loot("Small Topazs",  0,  LootPossibility.Normal,  25), new Loot("Black Pearls",  0,  LootPossibility.Normal,  22), new Loot("White Pearls",  0,  LootPossibility.Normal,  25), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  2), new Loot("Demon Horn",  0,  LootPossibility.Normal,  2), new Loot("Crystal Necklace",  3008,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Golden Amulet",  3013,  LootPossibility.Normal,  0), new Loot("Ruby Necklace",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Rusty Legs",  0,  LootPossibility.Rare,  0), new Loot("Beastslayer Axe",  0,  LootPossibility.Normal,  0), new Loot("Noble Axe",  0,  LootPossibility.Normal,  0), new Loot("Butcher's Axe",  0,  LootPossibility.Normal,  0), new Loot("Spirit Cloak",  0,  LootPossibility.Normal,  0), new Loot("Focus Cape",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.Normal,  0), new Loot("Crown Armor",  0,  LootPossibility.Normal,  0), new Loot("Knight Legs",  0,  LootPossibility.Normal,  0), new Loot("Crown Legs",  0,  LootPossibility.Normal,  0), new Loot("Magma Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Boots",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Warding",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Normal,  0), new Loot("Golden Armor",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Lost Souls",  0,  LootPossibility.SemiRare,  0), new Loot("Vile Axe",  0,  LootPossibility.Normal,  0), new Loot("Spellscroll of Prophecies",  0,  LootPossibility.Rare,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.Rare,  0), new Loot("Voltage Armor",  0,  LootPossibility.Rare,  0), new Loot("Spellbook of Dark Mysteries",  0,  LootPossibility.VeryRare,  0), new Loot("Demonwing Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Great Axe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Hellhound = new CreatureData("Hellhound", 7500, 6800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25), new DamageModifier(DamageType.Ice,  20) },
          new List<string>() { "GROOOOWL!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  375), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Ham",  3582,  LootPossibility.Normal,  6), new Loot("Throwing Knives",  0,  LootPossibility.Normal,  11), new Loot("Black Pearls",  0,  LootPossibility.Normal,  4), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Giant Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Knight Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Spike Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Ruthless Axe",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Hellspawn = new CreatureData("Hellspawn", 3500, 2550, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Fire,  40), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Your fragile bones are like toothpicks to me.",  "You little weasel will not live to see another day.",  "I'm just a messenger of what's yet to come.",  "HRAAAAAAAAAAAAAAAARRRR",  "I'm taking you down with me!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  207), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  6), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Assassin Stars",  0,  LootPossibility.Normal,  2), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Small Topazes",  0,  LootPossibility.Normal,  4), new Loot("Knight Legs",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.SemiRare,  2), new Loot("Warrior Helmet",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Spiked Squelcher",  0,  LootPossibility.Normal,  0), new Loot("Onyx Flail",  0,  LootPossibility.Rare,  0), new Loot("Dracoyle Statue",  0,  LootPossibility.VeryRare,  0), new Loot("Black Skull",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Hero = new CreatureData("Hero", 1400, 1200, 0, 0, 360, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Energy,  40), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  20) },
          new List<string>() { "Let's have a fight!",  "I will sing a tune at your grave.",  "Have you seen princess Lumelia",  "Welcome to my battleground!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Arrows",  0,  LootPossibility.Normal,  15), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("Lyre",  0,  LootPossibility.Normal,  0), new Loot("Green Tunic",  0,  LootPossibility.Normal,  0), new Loot("Bow",  0,  LootPossibility.Normal,  0), new Loot("Scarf",  3572,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Grapes",  3592,  LootPossibility.Normal,  0), new Loot("Red Rose",  0,  LootPossibility.Normal,  0), new Loot("Wedding Ring",  0,  LootPossibility.Normal,  0), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Sniper Arrow",  7364,  LootPossibility.Normal,  6), new Loot("War Hammer",  0,  LootPossibility.SemiRare,  0), new Loot("Red Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Crown Shield",  0,  LootPossibility.Rare,  0), new Loot("Crown Helmet",  0,  LootPossibility.Rare,  0), new Loot("Fire Sword",  0,  LootPossibility.Rare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0), new Loot("Might Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Crown Legs",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Hide = new CreatureData("Hide", 500, 240, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { },
          new List<Loot>() { new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  5), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Terra Hood",  0,  LootPossibility.Normal,  0), new Loot("Spider Silk",  0,  LootPossibility.Normal,  0), new Loot("Knight Legs",  0,  LootPossibility.Normal,  0), new Loot("Time Ring",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HighTemplarCobrass = new CreatureData("High Templar Cobrass", 410, 515, 0, 0, 80, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Salam",  0,  LootPossibility.Normal,  0), new Loot("er Shield",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Vase",  0,  LootPossibility.Rare,  0), new Loot("Lizard Scale",  5881,  LootPossibility.Always,  0), new Loot("Lizard Leather",  5876,  LootPossibility.Normal,  0)}
      );
      public static CreatureData HotDog = new CreatureData("Hot Dog", 505, 190, 0, 0, 125, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Holy,  5) },
          new List<string>() { "Wuff Wuff",  "Grrr Wuff",  "Show me how good you are without some rolled newspaper!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Hunter = new CreatureData("Hunter", 150, 150, 0, 0, 120, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10) },
          new List<string>() { "Guess who we are hunting!",  "Guess who we're hunting,  hahaha!",  "Bullseye!",  "You'll make a nice trophy!",  },
          new List<Loot>() { new Loot("Poison Arrow",  0,  LootPossibility.Normal,  2), new Loot("Arrows",  0,  LootPossibility.Normal,  24), new Loot("Oranges",  0,  LootPossibility.Normal,  2), new Loot("Rolls",  0,  LootPossibility.Normal,  2), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Bow",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Dragon Necklace",  3085,  LootPossibility.Rare,  0), new Loot("Burst Arrows",  0,  LootPossibility.Rare,  3), new Loot("Wolf Trophy",  0,  LootPossibility.VeryRare,  0), new Loot("Lion Trophy",  0,  LootPossibility.VeryRare,  0), new Loot("Sniper Gloves",  5875,  LootPossibility.VeryRare,  0), new Loot("Small Ruby",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Husky = new CreatureData("Husky", 140, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Yoooohuuuu!",  "Grrrrrrr",  "Ruff,  ruff!",  },
          new List<Loot>() { }
      );
      public static CreatureData Hyaena = new CreatureData("Hyaena", 60, 20, 0, 0, 20, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Grrrrrr",  "Hou hou hou!",  },
          new List<Loot>() { new Loot("Worms",  0,  LootPossibility.Normal,  4), new Loot("Meat",  3577,  LootPossibility.Normal,  2)}
      );
      public static CreatureData Hydra = new CreatureData("Hydra", 2350, 2100, 0, 0, 750, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "FCHHHHH",  "HISSSS",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  250), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  4), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Armor",  0,  LootPossibility.Rare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.Rare,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Hydra Egg",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0), new Loot("Royal Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Medusa Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData IceGolem = new CreatureData("Ice Golem", 385, 295, 0, 0, 305, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Ice, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20) },
          new List<string>() { "Chrrr.",  "Crrrrk.",  "Gnarr.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  53), new Loot("Ice Cube",  0,  LootPossibility.Rare,  0), new Loot("Ice Rapier",  0,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.Rare,  0), new Loot("Small Diamond",  0,  LootPossibility.Rare,  0), new Loot("Crystal Sword",  0,  LootPossibility.Rare,  0), new Loot("Strange Helmet",  0,  LootPossibility.Rare,  0), new Loot("Shard",  0,  LootPossibility.Rare,  0), new Loot("Spike Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Glacier Mask",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData IceOverlord = new CreatureData("Ice Overlord", 4000, 2800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  3), new Loot("Crystalline Armor",  0,  LootPossibility.Rare,  0), new Loot("Flawless Ice Crystal",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData IceWitch = new CreatureData("Ice Witch", 650, 580, 0, 0, 394, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Fire,  50), new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "So you think you are cool",  "I hope it is not too cold for you! HeHeHe.",  "Freeze!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Ice Cube",  0,  LootPossibility.Normal,  0), new Loot("Clerical Mace",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Shard",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Glacier Shoes",  0,  LootPossibility.Rare,  0), new Loot("Diamond Sceptre",  0,  LootPossibility.Rare,  0), new Loot("Mystic Turban",  0,  LootPossibility.Rare,  0), new Loot("Glacier Kilt",  0,  LootPossibility.VeryRare,  0), new Loot("Earmuffs",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData InfernalFrog = new CreatureData("Infernal Frog", 655, 190, 0, 0, 42, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  "No Kisses for you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  53)}
      );
      public static CreatureData Infernalist = new CreatureData("Infernalist", 3650, 4000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  95)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Nothing will remain but your scorched bones!",  "Some like it hot!",  "It's cooking time!",  "Feel the heat of battle!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  115), new Loot("Raspberry",  0,  LootPossibility.Normal,  5), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.SemiRare,  0), new Loot("Red Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Small Enchanted Ruby",  0,  LootPossibility.SemiRare,  0), new Loot("Energy Ring",  0,  LootPossibility.Rare,  0), new Loot("Magic Sulphur",  0,  LootPossibility.Rare,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Rare,  0), new Loot("Royal Tapestry",  0,  LootPossibility.Rare,  0), new Loot("Red Tome",  0,  LootPossibility.Rare,  0), new Loot("Gold Ingot",  0,  LootPossibility.VeryRare,  0), new Loot("Black Skull",  0,  LootPossibility.VeryRare,  0), new Loot("Magma Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Queen's Sceptre",  0,  LootPossibility.VeryRare,  0), new Loot("Crystal of Power",  0,  LootPossibility.VeryRare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Infernatil = new CreatureData("Infernatil", 160000, 85000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Worship Zathroth pathetic mortal!, Your soul will be mine!, ASHES TO ASHES!, YOU WILL ALL BURN!, THE DAY OF RECKONING IS AT HAND!, BOW TO THE POWER OF THE RUTHLESS SEVEN!",  },
          new List<Loot>() { new Loot("Expect very rare items",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Inky = new CreatureData("Inky", 600, 250, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  90)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Tssss!",  "Gaaahhh!",  "Gluh! Gluh!",  "Boohaa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  13)}
      );
      public static CreatureData IslandTroll = new CreatureData("Island Troll", 50, 20, 0, 0, 10, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Hmmm,  turtles",  "Hmmm,  dogs",  "Hmmm,  worms",  "Groar",  "Gruntz!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Wood",  0,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("H",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Studded Club",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Rare,  0), new Loot("Marlin",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData IsleofEvil = new CreatureData("Isle of Evil", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData JaggedEarthElemental = new CreatureData("Jagged Earth Elemental", 0, 1300, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  45), new DamageModifier(DamageType.Energy,  85), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15) },
          new List<string>() { "*STOMP STOMP*",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  180), new Loot("Some Leaves",  0,  LootPossibility.Normal,  0), new Loot("Twigs",  0,  LootPossibility.Normal,  0), new Loot("Small Stones Seeds",  0,  LootPossibility.Rare,  10), new Loot("Iron Ore",  0,  LootPossibility.Rare,  0), new Loot("Small Emeralds",  0,  LootPossibility.Rare,  2), new Loot("Natural Soil",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Juggernaut = new CreatureData("Juggernaut", 20000, 8700, 0, 0, 2260, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50), new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "RAAARRR!",  "GRRRRRR!",  "WAHHHH!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  400), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  8), new Loot("Ham",  3582,  LootPossibility.Normal,  8), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  3), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  4), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Dirty Fur",  0,  LootPossibility.Normal,  0), new Loot("Broken Pottery",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Dragon Hammer",  0,  LootPossibility.SemiRare,  0), new Loot("Spiked Squelcher",  0,  LootPossibility.SemiRare,  0), new Loot("Gold Ingot",  0,  LootPossibility.Rare,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.Rare,  0), new Loot("Golden Legs",  0,  LootPossibility.Rare,  0), new Loot("Golden Armor",  0,  LootPossibility.Rare,  0), new Loot("Mastermind Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Skullcracker Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Heavy Mace",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData KillerRabbit = new CreatureData("Killer Rabbit", 205, 160, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Who is lunch NOW",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90)}
      );
      public static CreatureData Kitty = new CreatureData("Kitty", 0, 0, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Kongra = new CreatureData("Kongra", 340, 115, 0, 0, 60, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { "Ungh! Ungh!",  "Hugah!",  "Huaauaauaauaa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Banana",  3587,  LootPossibility.Normal,  11), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Club Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Ape Fur",  5883,  LootPossibility.Rare,  0)}
      );
      public static CreatureData KongraAntiBotter = new CreatureData("Kongra AntiBotter", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData KosheitheDeathless = new CreatureData("Koshei the Deathless", 0, 0, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Energy,  90), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  15), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Your pain will be beyond imagination!",  "You can't defeat me! I will resurrect ,  take your soul!",  "Death is my ally!",  "Welcome to my domain visitor!",  "You will be my toy on the other side!",  "What a disgusting smell of life!",  "You will endure agony beyond thy death!",  },
          new List<Loot>() { new Loot("None",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData KreeboshtheExile = new CreatureData("Kreebosh the Exile", 805, 350, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  55)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "I bet you wish you weren't here.",  },
          new List<Loot>() { }
      );
      public static CreatureData Larva = new CreatureData("Larva", 70, 44, 0, 0, 36, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Meat",  3577,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Latrivan = new CreatureData("Latrivan", 25000, 10000, 0, 0, 1800, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "I might reward you for killing my brother ~ with a swift death!",  "Colateral damage is so fun!",  "Golgordan you fool!",  "We are the brothers of fear!",  },
          new List<Loot>() { new Loot("Gold Coins",  0,  LootPossibility.Normal,  150), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  6), new Loot("Talons",  0,  LootPossibility.Normal,  13), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  9), new Loot("Black Pearls",  0,  LootPossibility.Normal,  28), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Potion",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Silver Dagger",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Lavahole = new CreatureData("Lavahole", 0, 0, 0, 0, 111, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Lersatio = new CreatureData("Lersatio", 0, 2500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "FINE DIE PRETTY!",  "One day I will see my pretty face in a mirror again.",  },
          new List<Loot>() { new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  5), new Loot("Gold",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Ring of healing",  0,  LootPossibility.SemiRare,  0), new Loot("Vampire Shield",  0,  LootPossibility.Rare,  0), new Loot("Dreaded Cleaver",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData LethalLissy = new CreatureData("Lethal Lissy", 1450, 500, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  102), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Skull",  0,  LootPossibility.Normal,  2), new Loot("Lethal Lissy's Shirt",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Leviathan = new CreatureData("Leviathan", 6000, 5000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  15), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "CHHHRRRR",  "HISSSS",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  234), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  6), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  3), new Loot("Glacier Amulet",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Focus Cape",  0,  LootPossibility.Normal,  0), new Loot("Northwind Rod",  0,  LootPossibility.Normal,  0), new Loot("Glacier Kilt",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Crystalline Armor",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Legs",  0,  LootPossibility.Rare,  0), new Loot("Bonebreaker",  0,  LootPossibility.Normal,  0), new Loot("Moon Backpack",  0,  LootPossibility.Rare,  0), new Loot("Oceanborn Leviathan Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Sea Serpent Trophy",  0,  LootPossibility.Always,  0), new Loot("Leviathan's Amulet",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Lich = new CreatureData("Lich", 880, 900, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { "Doomed be the living!",  "Death awaits all!",  "Thy living flesh offends me!",  "Death ,  Decay!",  "You will endure agony beyond thy death!",  "Pain sweet pain!",  "Come to me my children!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  120), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  0), new Loot("White Pearl",  3026,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Strange Helmet",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.VeryRare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Lightning Boots",  0,  LootPossibility.Rare,  0), new Loot("Blue Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Castle Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Lion = new CreatureData("Lion", 80, 30, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Groarrr!",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  2)}
      );
      public static CreatureData ListofCreaturesbyConvinceCost = new CreatureData("List of Creatures by Convince Cost", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData LizardSentinel = new CreatureData("Lizard Sentinel", 265, 110, 0, 0, 115, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Tssss!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  60), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  3), new Loot("Hunting Spear",  3347,  LootPossibility.SemiRare,  0), new Loot("Obsidian Lance",  0,  LootPossibility.SemiRare,  0), new Loot("Halberd",  0,  LootPossibility.SemiRare,  0), new Loot("Sentinel Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Lizard Leather",  5876,  LootPossibility.SemiRare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Lizard Scale",  5881,  LootPossibility.Rare,  0), new Loot("Small Diamond",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData LizardSnakecharmer = new CreatureData("Lizard Snakecharmer", 325, 210, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "I smeeeel warm blood!",  "Shhhhhhh",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  70), new Loot("Dead Snake",  0,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Lizard Leather",  5876,  LootPossibility.SemiRare,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Rare,  0), new Loot("Lizard Scale",  5881,  LootPossibility.Rare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  0), new Loot("Terra Rod",  0,  LootPossibility.VeryRare,  0), new Loot("Mana Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Yellow Gem",  0,  LootPossibility.VeryRare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0), new Loot("Life Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Charmer's Tiara",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData LizardTemplar = new CreatureData("Lizard Templar", 410, 155, 0, 0, 70, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Hissss!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  60), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Lizard Leather",  5876,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.Rare,  0), new Loot("Templar Scytheblade",  0,  LootPossibility.Rare,  0), new Loot("Small Emerald",  3032,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Lizard Scale",  5881,  LootPossibility.Rare,  0), new Loot("Salam",  0,  LootPossibility.Normal,  0), new Loot("er Shield",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData LordoftheElements = new CreatureData("Lord of the Elements", 8000, 8000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Holy, DamageType.Death, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Ice,  30), new DamageModifier(DamageType.Earth,  45)},
          new List<DamageModifier>() {  },
          new List<string>() { "WHO DARES CALLING ME",  },
          new List<Loot>() { new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  6), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  2), new Loot("Small Emerald",  3032,  LootPossibility.Normal,  2), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Fireborn Giant Armor",  0,  LootPossibility.Rare,  0), new Loot("Earthborn Titan Armor",  0,  LootPossibility.Rare,  0), new Loot("Oceanborn Leviathan Armor",  0,  LootPossibility.Rare,  0), new Loot("Neutral Matter",  0,  LootPossibility.Normal,  0), new Loot("skinning only)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData LostSoul = new CreatureData("Lost Soul", 5800, 4000, 0, 0, 600, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20) },
          new List<string>() { "Mouuuurn meeee!",  "Forgive Meee!",  "Help meee!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  320), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  3), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Rotten Meat",  0,  LootPossibility.Normal,  2), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Legion Helmet",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Silver Goblet",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  2), new Loot("Titan Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Ruby Necklace",  0,  LootPossibility.Rare,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0), new Loot("Skeleton Decoration",  0,  LootPossibility.Rare,  0), new Loot("Key Ring",  0,  LootPossibility.Rare,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Rare,  0), new Loot("Skull Staff",  0,  LootPossibility.Rare,  0), new Loot("Amulet of Loss",  3057,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MadScientist = new CreatureData("Mad Scientist", 325, 205, 0, 0, 127, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "Die in the name of Science!",  "You will regret interrupting my studies!",  "Let me test this!",  "I will study your corpse!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  120), new Loot("Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Powder Herb",  0,  LootPossibility.SemiRare,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("Small Enchanted Amethyst",  0,  LootPossibility.Rare,  0), new Loot("White Mushroom",  3723,  LootPossibility.Rare,  3), new Loot("Cream Cake",  0,  LootPossibility.Rare,  0), new Loot("Cookie",  3598,  LootPossibility.Rare,  5), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("rare)",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("Of Vortex",  0,  LootPossibility.VeryRare,  0), new Loot("Mastermind Potion",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MadSheep = new CreatureData("Mad Sheep", 22, 0, 0, 0, 1, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Maeh",  "Groar!",  "Fchhhh",  "Meow!",  "Woof!",  },
          new List<Loot>() { }
      );
      public static CreatureData MadTechnomancer = new CreatureData("Mad Technomancer", 0, 55, 0, 0, 350, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "I'm going to make them an offer they can't refuse.",  "My masterplan cannot fail!",  "Gentlemen,  you can't fight here! This is the War Room!",  },
          new List<Loot>() { new Loot("Technomancer Beard",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Madareth = new CreatureData("Madareth", 0, 10000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  95), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "I am going to play with yourself!",  "Feel my wrath!",  "No one matches my battle prowess!",  "You will all die!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  30), new Loot("Demon Horns",  0,  LootPossibility.Normal,  2), new Loot("Small Diamonds",  0,  LootPossibility.Normal,  2), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Rusty Legs",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Potion",  0,  LootPossibility.Normal,  0), new Loot("Berserk Potion",  0,  LootPossibility.Normal,  0), new Loot("Bullseye Potion",  0,  LootPossibility.Normal,  0), new Loot("Axe Ring",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Sword Ring",  0,  LootPossibility.Normal,  0), new Loot("Club Ring",  0,  LootPossibility.Normal,  0), new Loot("Ring Of Healing",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Time Ring",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Starstorm",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Voodoo",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Underworld Rod",  0,  LootPossibility.Normal,  0), new Loot("Hailstorm Rod",  0,  LootPossibility.Normal,  0), new Loot("Springsprout Rod",  0,  LootPossibility.Normal,  0), new Loot("Lyre",  0,  LootPossibility.Normal,  0), new Loot("Wooden Flute",  0,  LootPossibility.Normal,  0), new Loot("Lute",  0,  LootPossibility.Normal,  0), new Loot("War Drum",  0,  LootPossibility.Normal,  0), new Loot("War Horn",  0,  LootPossibility.Normal,  0), new Loot("Didgeridoo",  0,  LootPossibility.Normal,  0), new Loot("Bloody Edge",  0,  LootPossibility.Normal,  0), new Loot("Crystal Sword",  0,  LootPossibility.Normal,  0), new Loot("Mercenary Sword",  0,  LootPossibility.Normal,  0), new Loot("Relic Sword",  0,  LootPossibility.Normal,  0), new Loot("Assassin Dagger",  0,  LootPossibility.Normal,  0), new Loot("Nightmare Blade",  0,  LootPossibility.Normal,  0), new Loot("Haunted Blade",  0,  LootPossibility.Normal,  0), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Demon Helmet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MagicPillar = new CreatureData("Magic Pillar", 0, 0, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Magicthrower = new CreatureData("Magicthrower", 0, 0, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Mahrdis = new CreatureData("Mahrdis", 3900, 3050, 0, 0, 2400, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Ashes to ashes!",  "Fire,  Fire!",  "The eternal flame dem, s its due!",  "Burnnnnnnnnn!",  "May my flames engulf you!",  "This is why I'm hot.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  214), new Loot("Small Rubies",  0,  LootPossibility.Normal,  3), new Loot("Life Ring",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Red Gem",  0,  LootPossibility.Rare,  0), new Loot("Fire Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Holy Falcon",  0,  LootPossibility.VeryRare,  0), new Loot("Phoenix Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Burning Heart",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Mammoth = new CreatureData("Mammoth", 320, 160, 0, 0, 110, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  15), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Troooooot!",  "Hooooot-Toooooot!",  "Tooooot.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  3), new Loot("Tusk",  0,  LootPossibility.Rare,  2), new Loot("Furry Club",  0,  LootPossibility.Rare,  0), new Loot("Tusk Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData ManintheCave = new CreatureData("Man in the Cave", 485, 770, 0, 0, 157, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "THE MONKS ARE MINE!",  "I will rope you up! All of you!",  "You have been roped up!",  "A MIC to rule them all!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  27), new Loot("Ropes",  0,  LootPossibility.Normal,  3), new Loot("Brown Bread",  3602,  LootPossibility.Normal,  0), new Loot("Brown Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Mammoth Fur Cape",  0,  LootPossibility.Normal,  0), new Loot("Mercenary Sword",  0,  LootPossibility.Normal,  0), new Loot("Shard",  0,  LootPossibility.Normal,  0), new Loot("Fur Cap",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Marid = new CreatureData("Marid", 550, 410, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  90), new DamageModifier(DamageType.Energy,  60), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Wishes can come true",  "Feel the power of my magic,  tiny mortal!",  "Simsalabim",  "Djinns will soon again be the greatest!",  "Be careful what you wish.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Blueberry",  0,  LootPossibility.Normal,  25), new Loot("Royal Spear",  7378,  LootPossibility.Normal,  3), new Loot("Heavy Machete",  0,  LootPossibility.Normal,  0), new Loot("Small Oil Lamp",  0,  LootPossibility.Normal,  0), new Loot("Blue Tapestry",  0,  LootPossibility.SemiRare,  0), new Loot("Blue Rose",  0,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  4), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Blue Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Wooden Flute",  0,  LootPossibility.Rare,  0), new Loot("Seeds",  0,  LootPossibility.Rare,  0), new Loot("Hailstorm Rod",  0,  LootPossibility.Rare,  0), new Loot("Mystic Turban",  0,  LootPossibility.Rare,  0), new Loot("Magma Monocle",  0,  LootPossibility.Rare,  0), new Loot("Blue Gem",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Marziel = new CreatureData("Marziel", 0, 3000, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  43), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "My brides will show you how to suffer beautifully",  },
          new List<Loot>() { new Loot("Platinum Coins",  0,  LootPossibility.Normal,  7), new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Massacre = new CreatureData("Massacre", 32000, 20000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10) },
          new List<string>() { "HATE! HATE! KILL! KILL!",  "GRRAAARRRHH!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  207), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  6), new Loot("Meat",  3577,  LootPossibility.Normal,  9), new Loot("Orichalcum Pearl",  0,  LootPossibility.Normal,  6), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Old Twig",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Golden Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Heavy Mace",  0,  LootPossibility.VeryRare,  0), new Loot("Berserker",  0,  LootPossibility.VeryRare,  0), new Loot("Great Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Piece of Massacre's Shell",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData MassiveEarthElemental = new CreatureData("Massive Earth Elemental", 1330, 950, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  45), new DamageModifier(DamageType.Energy,  90)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  221), new Loot("Small Stones",  0,  LootPossibility.Normal,  10), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Rare,  2), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Rare,  0), new Loot("Small Topaz",  0,  LootPossibility.SemiRare,  2), new Loot("Stone Skin Amulet",  3081,  LootPossibility.VeryRare,  0), new Loot("Terra Amulet",  0,  LootPossibility.VeryRare,  0), new Loot("Diamond Sceptre",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MassiveEnergyElemental = new CreatureData("Massive Energy Elemental", 1100, 950, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  70), new DamageModifier(DamageType.Holy,  25), new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  125), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  3), new Loot("Flash Arrow",  761,  LootPossibility.Normal,  14), new Loot("Rusty Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Lightning Pendant",  0,  LootPossibility.Rare,  0), new Loot("Spellbook of Warding",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Starstorm",  0,  LootPossibility.Rare,  0), new Loot("Energy spike sword",  0,  LootPossibility.Rare,  0), new Loot("Shockwave Amulet",  0,  LootPossibility.VeryRare,  0), new Loot("Lightning Legs",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MassiveFireElemental = new CreatureData("Massive Fire Elemental", 1200, 950, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData MassiveWaterElemental = new CreatureData("Massive Water Elemental", 1250, 1100, 0, 0, 431, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Holy,  30), new DamageModifier(DamageType.Death,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Worn Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.Normal,  0), new Loot("Giant Shimmering Pearl",  0,  LootPossibility.Normal,  0), new Loot("Rusty Legs",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MechanicalFighter = new CreatureData("Mechanical Fighter", 420, 255, 0, 0, 200, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Holy, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("None",  0,  LootPossibility.Normal,  0), new Loot("it turns into Wooden Trash",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Medusa = new CreatureData("Medusa", 4500, 4050, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "You will make sssuch a fine ssstatue!",  "There isss no chhhanccce of essscape",  "Are you tired or why are you moving thhat ssslow lt",  "chucklegt",  "",  "Jussst look at me!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  2), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  3), new Loot("Ultimate Health Potions",  0,  LootPossibility.Normal,  2), new Loot("Great Mana Potions",  0,  LootPossibility.Normal,  2), new Loot("White Pearl",  3026,  LootPossibility.Normal,  0), new Loot("Terra Amulet",  0,  LootPossibility.SemiRare,  0), new Loot("Medusa Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Sacred Tree Amulet",  0,  LootPossibility.Rare,  0), new Loot("Titan Axe",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Terra Mantle",  0,  LootPossibility.VeryRare,  0), new Loot("Terra Legs",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Mephiles = new CreatureData("Mephiles", 415, 415, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "I have a contract here which you should sign!",  "I sence so much potential in you. It's almost a shame I have to kill you.",  "Yes,  slay me for the loot I might have. Give in to your greed.",  "Wealth,  Power,  it is all at your fingertips. All you have to do is a bit blackmailing ,  bullying.",  "Come on. being a bit evil won't hurt you.",  },
          new List<Loot>() { new Loot("Stale Bread of Ancientness",  0,  LootPossibility.Normal,  0), new Loot("Poet's Fencing Quill",  0,  LootPossibility.Normal,  0), new Loot("The Rain Coat or Shield of the White Knight",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MercuryBlob = new CreatureData("Mercury Blob", 150, 180, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  65), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  15), new DamageModifier(DamageType.Earth,  65)},
          new List<DamageModifier>() {  },
          new List<string>() { "Crackle",  },
          new List<Loot>() { new Loot("Glob of Mercury",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MerikhtheSlaughterer = new CreatureData("Merikh the Slaughterer", 2000, 1500, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  87), new Loot("Green Piece of Cloth",  0,  LootPossibility.Normal,  4), new Loot("Royal Spear",  7378,  LootPossibility.Normal,  3), new Loot("Seeds",  0,  LootPossibility.Normal,  0), new Loot("Heavy Machete",  0,  LootPossibility.Normal,  0), new Loot("Small Oil Lamp",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  2), new Loot("Pears",  0,  LootPossibility.Normal,  8), new Loot("Mystic Turban",  0,  LootPossibility.Normal,  0), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  2), new Loot("Magma Monocle",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Merlkin = new CreatureData("Merlkin", 235, 145, 0, 0, 170, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { "Ugh! Ugh! Ugh!",  "Holy banana!",  "Chakka! Chakka!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  62), new Loot("Bananas",  0,  LootPossibility.Normal,  12), new Loot("Oranges",  0,  LootPossibility.Normal,  5), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Decay",  0,  LootPossibility.Rare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  0), new Loot("Ape Fur",  5883,  LootPossibility.Rare,  0), new Loot("Banana Staff",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MerlkinAntiBotter = new CreatureData("Merlkin AntiBotter", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Mimic = new CreatureData("Mimic", 30, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Minishabaal = new CreatureData("Minishabaal", 6000, 4000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I had Princess Lumelia as breakfast",  "Naaa-Nana-Naaa-Naaa!",  "My brother will come ,  get you for this!",  "Get them Fluffy!",  "He He He",  "Pftt,  Ferumbras such an upstart.",  "My dragon is not that old,  it's just second h, .",  "My other dragon is a red one.",  "When I am big I want to become the ruthless eighth.",  "Muahaha!",  "WHERE'S FLUFFY",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Pitchfork",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  0), new Loot("Surprise Bags",  0,  LootPossibility.Normal,  5), new Loot("Crown Legs",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Guardian Shield",  0,  LootPossibility.Normal,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.Rare,  0), new Loot("Golden Legs",  0,  LootPossibility.Rare,  0), new Loot("Tempest Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Demon Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Minotaur = new CreatureData("Minotaur", 100, 50, 0, 0, 45, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Kaplar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  28), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Shovel",  3457,  LootPossibility.Normal,  0), new Loot("Minotaur Leather",  5878,  LootPossibility.Rare,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MinotaurArcher = new CreatureData("Minotaur Archer", 100, 65, 0, 0, 100, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Ruan Wihmpy!",  "Kaplar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  28), new Loot("Bolts",  0,  LootPossibility.Normal,  23), new Loot("Piercing Bolts",  0,  LootPossibility.Normal,  4), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Soldier Helmet",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Chain Legs",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Minotaur Leather",  5878,  LootPossibility.Rare,  0), new Loot("Scale Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MinotaurGuard = new CreatureData("Minotaur Guard", 185, 160, 0, 0, 100, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Kirrl Karrrl!",  "Kaplar",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Fishing Rod",  3483,  LootPossibility.Normal,  0), new Loot("Hatchet",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Chain Legs",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.SemiRare,  0), new Loot("Minotaur Leather",  5878,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MinotaurMage = new CreatureData("Minotaur Mage", 155, 150, 0, 0, 205, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Learrn tha secrret uf deathhh!",  "Kaplar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Carrots",  0,  LootPossibility.Normal,  8), new Loot("Torches",  0,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Dead Snakes",  0,  LootPossibility.Normal,  2), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Legs",  0,  LootPossibility.Normal,  0), new Loot("Minotaur Leather",  5878,  LootPossibility.SemiRare,  0), new Loot("Taurus Mace",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.VeryRare,  0), new Loot("Mana Potion",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Monk = new CreatureData("Monk", 240, 200, 0, 0, 140, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10) },
          new List<string>() { "Repent Heretic!",  "A prayer to the almighty one!",  "I will punish the sinners!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("Scroll",  0,  LootPossibility.Normal,  0), new Loot("Lamp",  0,  LootPossibility.Normal,  0), new Loot("Brown Flask",  0,  LootPossibility.Normal,  0), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Leather Armor",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.SemiRare,  0), new Loot("Ankh",  0,  LootPossibility.SemiRare,  0), new Loot("Power Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Monstor = new CreatureData("Monstor", 800, 575, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Fire,  50), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  3), new DamageModifier(DamageType.Energy,  7) },
          new List<string>() { "NO ARMY ME STOPPING! GRARR!",  "ME DESTROY CITY! GROAR!",  "WHARR! MUST ... KIDNAP WOMEN!",  },
          new List<Loot>() { new Loot("Helmet of Ultimate Terror",  0,  LootPossibility.Normal,  0), new Loot("Farmer's Avenger",  0,  LootPossibility.Normal,  0), new Loot("Shield of Care or Incredible Mumpiz Slayer",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Morgaroth = new CreatureData("Morgaroth", 55000, 15000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "I AM MORGAROTH,  LORD OF THE TRIANGLE... AND YOU ARE LOST!",  "MY SEED IS FEAR AND MY HARVEST ARE YOUR SOULS!",  "ZATHROTH! LOOK AT THE DESTRUCTION I AM CAUSING IN YOUR NAME!",  "THE TRIANGLE OF TERROR WILL RISE!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  74), new Loot("Demonic Essences",  0,  LootPossibility.Normal,  5), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  7), new Loot("Small Diamonds",  0,  LootPossibility.Normal,  5), new Loot("White Pearls",  0,  LootPossibility.Normal,  11), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  9), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  13), new Loot("Demon Horns",  0,  LootPossibility.Normal,  2), new Loot("Infernal Bolts",  0,  LootPossibility.Normal,  100), new Loot("Ancient Amulet",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ball",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Demonbone",  0,  LootPossibility.Normal,  0), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Giant Sword",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Green Gem",  0,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Magic Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Magma Coat",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Moonlight Rod",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Normal,  0), new Loot("Onyx Flail",  0,  LootPossibility.Normal,  0), new Loot("Orb",  0,  LootPossibility.Normal,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Purple Tome",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Ring of the Sky",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Silver Dagger",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Strange Symbol",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Chain Bolter",  0,  LootPossibility.Rare,  0), new Loot("Dark Lord's Cape",  0,  LootPossibility.Rare,  0), new Loot("Fireborn Giant Armor",  0,  LootPossibility.Rare,  0), new Loot("Great Shield",  0,  LootPossibility.Rare,  0), new Loot("Molten Plate",  0,  LootPossibility.Rare,  0), new Loot("Morgaroth's Heart",  0,  LootPossibility.Rare,  0), new Loot("Obsidian Truncheon",  0,  LootPossibility.Rare,  0), new Loot("Royal Crossbow",  0,  LootPossibility.Rare,  0), new Loot("Teddy Bear",  0,  LootPossibility.Rare,  0), new Loot("The Devileye",  0,  LootPossibility.Rare,  0), new Loot("The Ironworker",  0,  LootPossibility.Rare,  0), new Loot("The Stomper",  0,  LootPossibility.Rare,  0), new Loot("Thunder Hammer",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Morguthis = new CreatureData("Morguthis", 4800, 3000, 0, 0, 1900, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Energy,  1), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "I am the supreme warrior!",  "Come ,  fight me,  cowards!",  "Vengeance!",  "You will make a fine trophy.",  "Let me hear the music of battle.",  "Another one to bite the dust!",  },
          new List<Loot>() { new Loot("Gold",  0,  LootPossibility.Normal,  153), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Knight Axe",  0,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.VeryRare,  0), new Loot("Ravager's Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Assassin Star",  0,  LootPossibility.VeryRare,  3), new Loot("Steel Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Sword Hilt",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData MoriktheGladiator = new CreatureData("Morik the Gladiator", 1235, 160, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "To be the one you'll have to beat the one!",  "Where did I put my ultimate health potion again",  "I am the best!",  "I'll take your ears as a trophy!",  },
          new List<Loot>() { new Loot("Morik's Helmet",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData MrPunish = new CreatureData("Mr Punish", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData MuddyEarthElemental = new CreatureData("Muddy Earth Elemental", 650, 450, 0, 0, 450, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  40), new DamageModifier(DamageType.Energy,  85), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  127), new Loot("Small Stones",  0,  LootPossibility.Normal,  3), new Loot("Some Leaves",  0,  LootPossibility.Normal,  0), new Loot("Natural Soil",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Mummy = new CreatureData("Mummy", 240, 150, 0, 0, 129, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "I will ssswallow your sssoul!",  "Mort ulhegh dakh visss.",  "Flesssh to dussst!",  "I will tassste life again!",  "Ahkahra exura belil mort!",  "Yohag Sssetham!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  91), new Loot("worms",  0,  LootPossibility.Normal,  3), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Strange Talisman",  3045,  LootPossibility.Normal,  0), new Loot("Silver Brooch",  0,  LootPossibility.Normal,  0), new Loot("Poison Dagger",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Rare,  0), new Loot("Yellow Piece of Cloth",  0,  LootPossibility.Rare,  0), new Loot("Black Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Munster = new CreatureData("Munster", 58, 35, 0, 0, 15, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Meep!",  "Meeeeep!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  19), new Loot("Worms",  0,  LootPossibility.Normal,  4), new Loot("Cookie",  3598,  LootPossibility.Normal,  0), new Loot("Club",  0,  LootPossibility.Normal,  0), new Loot("Jacket",  0,  LootPossibility.Normal,  0), new Loot("Cheese",  3607,  LootPossibility.Normal,  4), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Always,  0), new Loot("Dice",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MutatedBat = new CreatureData("Mutated Bat", 900, 615, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Shriiiiiek",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  111), new Loot("Star Herb",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Obsidian Lance",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  2), new Loot("Bat Wing",  5894,  LootPossibility.SemiRare,  2), new Loot("Black Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.Rare,  3), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  2), new Loot("Energy Ring",  0,  LootPossibility.Rare,  0), new Loot("Mercenary Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Batwing Hat",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MutatedHuman = new CreatureData("Mutated Human", 240, 150, 0, 0, 164, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Take that creature off my back!! I can fell it!",  "HEEEEEEEEEELP!",  "You will be the next infected one... GRAAAAAAAAARRR!",  "Science... is a curse.",  "Run as fast as you can.",  "Oh by the gods! What is this... aaaaaargh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Worm",  0,  LootPossibility.Normal,  3), new Loot("Cheese",  3607,  LootPossibility.Normal,  0), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Fern",  0,  LootPossibility.SemiRare,  0), new Loot("Strange Talisman",  3045,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Rare,  0), new Loot("Peanut",  0,  LootPossibility.Rare,  0), new Loot("Silver Amulet",  3054,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MutatedRat = new CreatureData("Mutated Rat", 550, 450, 0, 0, 305, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Grrrrrrrrrrrrrr!",  "Fcccccchhhhhh",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Stone Herb",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0), new Loot("Spellbook of Enlightenment",  0,  LootPossibility.VeryRare,  0), new Loot("Tower Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData MutatedTiger = new CreatureData("Mutated Tiger", 1100, 750, 0, 0, 0, false, true, FrontAttack.Wave, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "GRAAARRRRRR",  "CHHHHHHHHHHH",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  96), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Silky Tapestry",  0,  LootPossibility.Rare,  0), new Loot("Glorious Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Guardian Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Angelic Axe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Necromancer = new CreatureData("Necromancer", 580, 580, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  50), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Fire,  5) },
          new List<string>() { "Taste the sweetness of death!",  "Your corpse will be mine.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Poison Arrow",  0,  LootPossibility.Normal,  5), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.SemiRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Clerical Mace",  0,  LootPossibility.SemiRare,  0), new Loot("Mystic Turban",  0,  LootPossibility.Rare,  0), new Loot("Skull Staff",  0,  LootPossibility.VeryRare,  0), new Loot("Noble Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Spellbook of Warding",  0,  LootPossibility.VeryRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Necropharus = new CreatureData("Necropharus", 750, 1050, 0, 0, 300, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "You will rise as my servant!",  "Praise to my master Urgith!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  79), new Loot("Bone Club",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Clerical Mace",  0,  LootPossibility.Normal,  0), new Loot("Mystic Turban",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Moonlight Rod",  0,  LootPossibility.Normal,  0), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.SemiRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.Rare,  0), new Loot("Soul Stone",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Nightmare = new CreatureData("Nightmare", 2700, 2150, 0, 0, 720, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Close your eyes... I want to show you something.",  "I will haunt you forever!",  "Pffffrrrrrrrrrrrr.",  "I will make you scream.",  "Take a ride with me.",  "Weeeheeheeeheee!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  120), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Power Bolt",  3450,  LootPossibility.Normal,  4), new Loot("Concentrated Demonic Blood",  0,  LootPossibility.Normal,  2), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Platinum Coin",  3035,  LootPossibility.SemiRare,  4), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("Death Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Ancient Shield",  0,  LootPossibility.Rare,  0), new Loot("Knight Legs",  0,  LootPossibility.Rare,  0), new Loot("Steel Helmet",  0,  LootPossibility.Rare,  0), new Loot("War Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Skeleton Decoration",  0,  LootPossibility.Rare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0), new Loot("Mysterious Voodoo Skull",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData NightmareScion = new CreatureData("Nightmare Scion", 1400, 1350, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Weeeheeheee!",  "Pffffrrrrrrrrrrrr.",  "Peak a boo,  I killed you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  147), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  3), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0), new Loot("Crown Helmet",  0,  LootPossibility.Rare,  0), new Loot("Focus Cape",  0,  LootPossibility.Rare,  0), new Loot("Crystal of Focus",  0,  LootPossibility.VeryRare,  0), new Loot("Diamond Sceptre",  0,  LootPossibility.Rare,  0), new Loot("Bar of Chocolate",  0,  LootPossibility.VeryRare,  0), new Loot("Shadow Sceptre",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Nightstalker = new CreatureData("Nightstalker", 700, 500, 0, 0, 260, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "The sunlight is so depressing.",  "Come with me,  my child.",  "I've been in the shadow under your bed last night.",  "You never know what hides in the night.",  "I remember your face - ,  I know where you sleep.",  "Only the sweetest ,  cruelest dreams for you,  my love.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  107), new Loot("Shadow Herb",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Protection Amulet",  3084,  LootPossibility.SemiRare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Rare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Spirit Cloak",  0,  LootPossibility.Rare,  0), new Loot("Crystal of Balance",  0,  LootPossibility.VeryRare,  0), new Loot("Haunted Blade",  0,  LootPossibility.VeryRare,  0), new Loot("Chaos Mace",  0,  LootPossibility.VeryRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Nomad = new CreatureData("Nomad", 160, 60, 0, 0, 80, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Fire,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "I will leave your remains to the vultures!",  "We are the true sons of the desert!",  "We are swift as the wind of the desert!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  51), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Steel Shield",  0,  LootPossibility.Rare,  0), new Loot("Iron Helmet",  0,  LootPossibility.Rare,  0), new Loot("Parchment",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData NorgleGlacierbeard = new CreatureData("Norgle Glacierbeard", 4300, 2100, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "I'll extinguish you warmbloods.",  "REVENGE!",  "Far too hot.",  "DISGUSTING WARMBLOODS!",  "Revenge is sweetest when served cold.",  },
          new List<Loot>() { }
      );
      public static CreatureData NoviceoftheCult = new CreatureData("Novice of the Cult", 285, 100, 0, 0, 146, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  8), new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Energy,  8) },
          new List<string>() { "Fear us!",  "You will not tell anyone what you have seen!",  "Your curiosity will be punished!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Scarf",  3572,  LootPossibility.SemiRare,  0), new Loot("Orange Book",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Vortex",  0,  LootPossibility.Rare,  0), new Loot("Pirate Voodoo Doll",  0,  LootPossibility.VeryRare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Music Sheet",  0,  LootPossibility.Normal,  0), new Loot("very rare)",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.VeryRare,  0), new Loot("Garlic Necklace",  3083,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Omruc = new CreatureData("Omruc", 4300, 2950, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Now chhhou shhhee me ... Now chhhou don't.",  "Chhhhou are marked ashhh my prey.",  "Psssst,  I am over chhhere.",  "Catchhhh me if chhhou can.",  "Die!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  160), new Loot("Apple",  3585,  LootPossibility.Normal,  2), new Loot("Arrow",  3447,  LootPossibility.Normal,  21), new Loot("Poison Arrow",  0,  LootPossibility.Normal,  20), new Loot("Burst Arrow",  3449,  LootPossibility.Normal,  15), new Loot("Onyx Arrow 0-3 Power Bolt",  0,  LootPossibility.Normal,  2), new Loot("Small Diamond",  0,  LootPossibility.Normal,  3), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Yellow Gem",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Rare,  0), new Loot("Hammer of Wrath",  0,  LootPossibility.VeryRare,  0), new Loot("Crystal Arrow",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Orc = new CreatureData("Orc", 70, 25, 0, 0, 35, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Grow truk grrrrr.",  "Prek tars,  dekklep zurk.",  "Grak brrretz!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Studded Shield",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Old",  0,  LootPossibility.Normal,  0), new Loot("Used Backpack",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData OrcBerserker = new CreatureData("Orc Berserker", 210, 195, 0, 0, 200, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "KRAK ORRRRRRK!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Lamp",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Battle Axe",  0,  LootPossibility.Normal,  0), new Loot("Hunting Spears",  0,  LootPossibility.SemiRare,  2), new Loot("Halberd",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData OrcLeader = new CreatureData("Orc Leader", 450, 270, 0, 0, 255, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Ulderek futgyr human!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  35), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Throwing Knife",  3298,  LootPossibility.Normal,  4), new Loot("Scimitar",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Backpack",  0,  LootPossibility.Normal,  0), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.Rare,  0), new Loot("Royal Spear",  7378,  LootPossibility.Rare,  0), new Loot("Sword Ring",  0,  LootPossibility.Rare,  0), new Loot("Plate Legs",  0,  LootPossibility.Rare,  0), new Loot("Broadsword",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData OrcRider = new CreatureData("Orc Rider", 180, 110, 0, 0, 120, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Orc arga Huummmak!",  "Grrrrrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  87), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Backpack",  0,  LootPossibility.Normal,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.Normal,  0), new Loot("Orcish Axe",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Obsidian Lance",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData OrcShaman = new CreatureData("Orc Shaman", 115, 110, 0, 0, 81, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Huumans stinkk!",  "Grak brrretz gulu.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  5), new Loot("Corncobs",  0,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("Grey Small Book",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Decay",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData OrcSpearman = new CreatureData("Orc Spearman", 105, 38, 0, 0, 55, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Ugaar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Studded Legs",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("Machete",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Dirty Fur",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData OrcWarlord = new CreatureData("Orc Warlord", 950, 670, 0, 0, 450, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  80), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Ikem rambo zambo!",  "Orc buta bana!",  "Ranat Ulderek!",  "Fetchi Maruk Buta",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Fish",  3578,  LootPossibility.Normal,  2), new Loot("Scimitar",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Hunting Spear",  3347,  LootPossibility.Normal,  0), new Loot("Throwing Stars",  0,  LootPossibility.Normal,  22), new Loot("Orcish Axe",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Dark Helmet",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Dragon Hammer",  0,  LootPossibility.Rare,  0), new Loot("Crusader Helmet",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Orc Trophy",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Magma Boots",  0,  LootPossibility.VeryRare,  0), new Loot("on Thais)",  0,  LootPossibility.Normal,  0), new Loot("Amazon Armor",  0,  LootPossibility.Normal,  0), new Loot("on Thais)",  0,  LootPossibility.Normal,  0), new Loot("Amazon Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData OrcWarrior = new CreatureData("Orc Warrior", 125, 50, 0, 0, 60, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Alk!",  "Trak grrrr brik.",  "Grow truk grrrr.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Bottle",  0,  LootPossibility.Normal,  0), new Loot("Copper Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Studded Club",  0,  LootPossibility.Rare,  0), new Loot("Poison Dagger",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData OrchidFrog = new CreatureData("Orchid Frog", 60, 20, 0, 0, 24, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Ribbit!",  "Ribbit! Ribbit!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Worm",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData OrcustheCruel = new CreatureData("Orcus the Cruel", 480, 280, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData Orshabaal = new CreatureData("Orshabaal", 0, 0, 0, 0, 5000, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "PRAISED BE MY MASTERS,  THE RUTHLESS SEVEN!",  "YOU ARE DOOMED!",  "ORSHABAAL IS BACK!",  "Be prepared for the day my masters will come for you!",  "SOULS FOR ORSHABAAL!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  280), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  67), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  19), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  13), new Loot("Small Emerald",  3032,  LootPossibility.Normal,  9), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  9), new Loot("Talon",  0,  LootPossibility.Normal,  5), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  5), new Loot("Demon Horn",  0,  LootPossibility.Normal,  2), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  1), new Loot("Small Diamond",  0,  LootPossibility.Normal,  2), new Loot("Coconut",  3589,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Ring of the Sky",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.Normal,  0), new Loot("Teddy Bear",  0,  LootPossibility.Normal,  0), new Loot("Green Gem",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Purple Tome",  0,  LootPossibility.Normal,  0), new Loot("Orb",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.Normal,  0), new Loot("Strange Symbol",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Orshabaal's Brain",  0,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Giant Sword",  0,  LootPossibility.Normal,  0), new Loot("Silver Dagger",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Golden Sickle",  0,  LootPossibility.Normal,  0), new Loot("Thunder Hammer",  0,  LootPossibility.Normal,  0), new Loot("Dragon Hammer",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Decay",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Magic Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Golden Legs",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Shield",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ball",  0,  LootPossibility.Normal,  0), new Loot("Robe of the Underworld",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData OverchargedEnergyElement = new CreatureData("Overcharged Energy Element", 1750, 1300, 0, 0, 895, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Ice},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20) },
          new List<string>() { "BZZZZZZZZZZ",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Energy Soil",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  2), new Loot("Berserk Potion",  0,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Starstorm",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Panda = new CreatureData("Panda", 80, 23, 0, 0, 16, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Groar",  "Grrrr",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  2)}
      );
      public static CreatureData Parrot = new CreatureData("Parrot", 25, 0, 0, 0, 5, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "You advanshed,  you advanshed!",  "Neeewbiiieee!",  "Screeech!",  "Hunterrr ish PK!",  "BR PL SWE",  "Hope you die ,  loooosh it!",  "You powerrrrrrabuserrrrr!",  "You are corrrrupt! Corrrrupt!",  "Tarrrrp",  "Blesshhh my stake! Screeech!",  "Leeave orrr hunted!!",  "Shhtop whining! Rrah!",  "I'm heeerrre! Screeeech!",  },
          new List<Loot>() { }
      );
      public static CreatureData Penguin = new CreatureData("Penguin", 33, 0, 0, 0, 3, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Fish",  3578,  LootPossibility.SemiRare,  2), new Loot("Green Perch",  0,  LootPossibility.Rare,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Rare,  0), new Loot("Northern Pike",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Phantasm = new CreatureData("Phantasm", 3950, 4400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Oh my,  you forgot to put your pants on!",  "Weeheeheeheehee!",  "Its nothing but a dream.",  "Dream a little dream with me!",  "Give in.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  2), new Loot("Shadow Herb",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Small Emerald",  3032,  LootPossibility.Normal,  2), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Abyss Hammer",  0,  LootPossibility.Rare,  0), new Loot("Platinum Coin",  3035,  LootPossibility.Rare,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Death Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Pig = new CreatureData("Pig", 25, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Oink oink",  "Oink",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4)}
      );
      public static CreatureData Pillar = new CreatureData("Pillar", 0, 0, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData PirateBuccaneer = new CreatureData("Pirate Buccaneer", 425, 250, 0, 0, 260, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Give up!",  "Hiyaa",  "Plundeeeeer!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  59), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Worn Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Throwing Knives",  0,  LootPossibility.Normal,  6), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Battle Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Treasure Map",  0,  LootPossibility.Normal,  0), new Loot("Pirate Shirt",  0,  LootPossibility.Rare,  0), new Loot("Plate Armor",  0,  LootPossibility.Rare,  0), new Loot("Peg Leg",  0,  LootPossibility.Rare,  0), new Loot("Eye Patch",  0,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Pirate Backpack",  0,  LootPossibility.VeryRare,  0), new Loot("Hook",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData PirateCorsair = new CreatureData("Pirate Corsair", 675, 350, 0, 0, 320, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Hiyaa!",  "Give up!",  "Plundeeeeer!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Dark Shield",  0,  LootPossibility.Normal,  0), new Loot("Dark Armor",  0,  LootPossibility.Normal,  0), new Loot("Throwing Stars",  0,  LootPossibility.Normal,  12), new Loot("Treasure Map",  0,  LootPossibility.Normal,  0), new Loot("Hook",  0,  LootPossibility.SemiRare,  0), new Loot("Peg Leg",  0,  LootPossibility.SemiRare,  0), new Loot("Eye Patch",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Skull C",  0,  LootPossibility.Normal,  0), new Loot("le",  0,  LootPossibility.Rare,  0), new Loot("Pirate Backpack",  0,  LootPossibility.Rare,  0), new Loot("Pirate Hat",  0,  LootPossibility.Rare,  0), new Loot("Pirate Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData PirateCutthroat = new CreatureData("Pirate Cutthroat", 325, 175, 0, 0, 271, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "Give up!",  "Plundeeeeer!",  "Hiyaa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  48), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Peg Leg",  0,  LootPossibility.SemiRare,  0), new Loot("Hook",  0,  LootPossibility.SemiRare,  0), new Loot("Eye Patch",  0,  LootPossibility.SemiRare,  0), new Loot("Dice",  0,  LootPossibility.SemiRare,  0), new Loot("Pirate Knee Breeches",  0,  LootPossibility.Rare,  0), new Loot("Rum Flask",  0,  LootPossibility.Rare,  0), new Loot("Light Shovel",  0,  LootPossibility.Rare,  0), new Loot("Pirate Bag",  0,  LootPossibility.Rare,  0), new Loot("Treasure Map",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData PirateGhost = new CreatureData("Pirate Ghost", 275, 250, 0, 0, 240, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "Yooh Ho Hooh Ho!",  "Hell is waiting for You!",  "It's alive!",  "The curse! Aww the curse!",  "You will not get my treasure!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  69), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Torn Book",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0), new Loot("Parchment",  0,  LootPossibility.Rare,  0), new Loot("Spike Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Red Robe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData PirateMarauder = new CreatureData("Pirate Marauder", 210, 125, 0, 0, 180, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20), new DamageModifier(DamageType.Earth,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  3) },
          new List<string>() { "Plundeeeeer!",  "Hiyaa!",  "Give up!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  60), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Spears",  0,  LootPossibility.Normal,  2), new Loot("B",  0,  LootPossibility.Normal,  0), new Loot("ana",  0,  LootPossibility.SemiRare,  0), new Loot("Peg Leg",  0,  LootPossibility.SemiRare,  0), new Loot("Hook",  0,  LootPossibility.SemiRare,  0), new Loot("Eye Patch",  0,  LootPossibility.SemiRare,  0), new Loot("Treasure Map",  0,  LootPossibility.SemiRare,  0), new Loot("Rum Flask",  0,  LootPossibility.SemiRare,  0), new Loot("Pirate Bag",  0,  LootPossibility.SemiRare,  0), new Loot("Empty Goldfish Bowl",  0,  LootPossibility.Rare,  0), new Loot("Dice",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData PirateSkeleton = new CreatureData("Pirate Skeleton", 190, 85, 0, 0, 50, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  25), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Bone Club",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Plaguesmith = new CreatureData("Plaguesmith", 8250, 4500, 0, 0, 800, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "You are looking a bit feverish!",  "You don't look that good!",  "Hachoo!",  "Cough Cough",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  231), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  3), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  3), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  3), new Loot("Moldy Cheese",  0,  LootPossibility.Normal,  0), new Loot("Piece of Iron",  0,  LootPossibility.Normal,  0), new Loot("Silver Brooch",  0,  LootPossibility.Normal,  0), new Loot("Dirty Cape",  0,  LootPossibility.Normal,  0), new Loot("Crowbar",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("War Hammer",  0,  LootPossibility.Normal,  0), new Loot("Club Ring",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Axe Ring",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Knight Legs",  0,  LootPossibility.SemiRare,  0), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("Piece of Hell Steel",  0,  LootPossibility.Rare,  0), new Loot("Piece of Royal Steel",  0,  LootPossibility.Rare,  0), new Loot("Piece of Draconian Steel",  0,  LootPossibility.Rare,  0), new Loot("Hammer of Wrath",  0,  LootPossibility.Rare,  0), new Loot("Steel Boots",  0,  LootPossibility.Rare,  0), new Loot("War Horn",  0,  LootPossibility.VeryRare,  0), new Loot("Emerald Bangle",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Plaguethrower = new CreatureData("Plaguethrower", 0, 0, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Poacher = new CreatureData("Poacher", 90, 70, 0, 0, 70, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "You will not live to tell anyone!",  "You are my game today!",  "Look what has stepped into my trap!",  },
          new List<Loot>() { new Loot("Roll",  3601,  LootPossibility.Normal,  2), new Loot("Arrows",  0,  LootPossibility.Normal,  17), new Loot("Poison Arrows",  0,  LootPossibility.Normal,  3), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Bow",  0,  LootPossibility.Normal,  0), new Loot("Closed Trap",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData PoisonSpider = new CreatureData("Poison Spider", 26, 22, 0, 0, 22, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  4)}
      );
      public static CreatureData PolarBear = new CreatureData("Polar Bear", 85, 28, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "GROARRR!",  },
          new List<Loot>() { new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Bear Paw",  5896,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Priestess = new CreatureData("Priestess", 390, 420, 0, 0, 195, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  40), new DamageModifier(DamageType.Earth,  70)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { "Your energy is mine.",  "Now your life is come to the end,  hahahaha!",  "Throw the soul on the altar!",  },
          new List<Loot>() { new Loot("Apple",  3585,  LootPossibility.Normal,  2), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Sling Herb",  0,  LootPossibility.Normal,  0), new Loot("Goat Grass",  0,  LootPossibility.Normal,  0), new Loot("Orange Book",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ball",  0,  LootPossibility.Normal,  0), new Loot("Wood Mushroom",  0,  LootPossibility.Normal,  0), new Loot("Powder Herb",  0,  LootPossibility.Normal,  0), new Loot("Clerical Mace",  0,  LootPossibility.Normal,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.Normal,  0), new Loot("Wooden Flute",  0,  LootPossibility.Rare,  0), new Loot("Talon",  0,  LootPossibility.Rare,  0), new Loot("Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Hailstorm Rod",  0,  LootPossibility.Rare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Primitive = new CreatureData("Primitive", 200, 45, 0, 0, 90, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "We don't need a future!",  "I'll rook you all!",  "They thought they'd beaten us!",  "You are history!",  "There can only be one world!",  "Guido who",  "Die noob!",  "There are no dragons!",  "I'll quit forever! Again ...",  "You all are noobs!",  "Beware of the cyclops!",  "Just had a disconnect.",  "Magic is only good for girls!",  "We'll be back!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Studded Shield",  0,  LootPossibility.Normal,  0), new Loot("Studded Helmet",  0,  LootPossibility.Normal,  0), new Loot("Studded Armor",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData PythiustheRotten = new CreatureData("Pythius the Rotten", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData QuaraConstrictor = new CreatureData("Quara Constrictor", 450, 250, 0, 0, 256, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Boohaa!",  "Tssss!",  "Gluh! Gluh!",  "Gaaahhh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  93), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Shrimp",  0,  LootPossibility.Normal,  5), new Loot("Brass Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Fish Fin",  5895,  LootPossibility.SemiRare,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  0)}
      );
      public static CreatureData QuaraConstrictorScout = new CreatureData("Quara Constrictor Scout", 450, 200, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Tssss!",  "Gaaahhh!",  "Gluh! Gluh!",  "Boohaa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0)}
      );
      public static CreatureData QuaraHydromancer = new CreatureData("Quara Hydromancer", 1100, 800, 0, 0, 825, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Qua hah tsh!",  "Teech tsha tshul!",  "Quara tsha Fach!",  "Tssssha Quara!",  "Blubber.",  "Blup.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Remains of a Fish",  0,  LootPossibility.Normal,  0), new Loot("Shrimp",  0,  LootPossibility.SemiRare,  5), new Loot("White Pearl",  3026,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("Of Cosmic Energy",  0,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Knight Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraHydromancerScout = new CreatureData("Quara Hydromancer Scout", 1100, 800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Qua hah tsh!",  "Teech tsha tshul!",  "Quara tsha Fach!",  "Tssssha Quara!",  "Blubber.",  "Blup.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  80), new Loot("Remains of a Fish",  0,  LootPossibility.Normal,  0), new Loot("Staff",  0,  LootPossibility.Normal,  0), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("White Pearl",  3026,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Rare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Knight Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraMantassin = new CreatureData("Quara Mantassin", 800, 400, 0, 0, 140, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Shrrrr",  "Zuerk Pachak!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Shrimp",  0,  LootPossibility.Normal,  5), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Small Sapphire",  0,  LootPossibility.Rare,  0), new Loot("Strange Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Blue Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Glacier Shoes",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraMantassinScout = new CreatureData("Quara Mantassin Scout", 220, 100, 0, 0, 110, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Shrrrr",  "Zuerk Pachak!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  29), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraPincher = new CreatureData("Quara Pincher", 1800, 1200, 0, 0, 340, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Clank! Clank!",  "Clap!",  "Crrrk! Crrrk!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  120), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Small Ruby",  0,  LootPossibility.SemiRare,  0), new Loot("Shrimp",  0,  LootPossibility.SemiRare,  5), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.Rare,  0), new Loot("Glacier Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Great Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Crown Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraPincherScout = new CreatureData("Quara Pincher Scout", 775, 600, 0, 0, 240, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Clank! Clank!",  "Clap!",  "Crrrk! Crrrk!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  49), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Small Ruby",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0)}
      );
      public static CreatureData QuaraPredator = new CreatureData("Quara Predator", 2200, 1600, 0, 0, 470, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Gnarrr!",  "Tcharrr!",  "Rrrah!",  "Rraaar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Royal Spear",  7378,  LootPossibility.Normal,  7), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Shrimp",  0,  LootPossibility.SemiRare,  5), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0), new Loot("Skull Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Assassin Star",  0,  LootPossibility.VeryRare,  2), new Loot("Relic Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Glacier Robe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData QuaraPredatorScout = new CreatureData("Quara Predator Scout", 890, 400, 0, 0, 190, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Gnarrr!",  "Tcharrr!",  "Rrrah!",  "Rraaar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  37), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("Small Diamond",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Rabbit = new CreatureData("Rabbit", 15, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Carrot",  3595,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Blue Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Green Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Purple Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Red Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Yellow Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("only during the Easter holiday)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Rahemos = new CreatureData("Rahemos", 3700, 3100, 0, 0, 1850, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Ice, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  95), new DamageModifier(DamageType.Energy,  95)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  40), new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "It's not a trick,  it's Rahemos.",  "Abrah Kadabrah!",  "It's a kind of magic.",  "Meet my friend from hell!",  "I will make you believe in magic.",  "Nothing hidden in my wrappings.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  230), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  3), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Magician Hat",  0,  LootPossibility.Rare,  0), new Loot("Orb",  0,  LootPossibility.VeryRare,  0), new Loot("Necrotic Rod",  0,  LootPossibility.VeryRare,  0), new Loot("Violet Gem",  0,  LootPossibility.VeryRare,  0), new Loot("Crystal W",  0,  LootPossibility.Normal,  0), new Loot("very rare)",  0,  LootPossibility.VeryRare,  0), new Loot("Twin Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Ancient Rune",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Rat = new CreatureData("Rat", 20, 5, 0, 0, 8, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Meep!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  5), new Loot("Worm",  0,  LootPossibility.Normal,  3), new Loot("Cheese",  3607,  LootPossibility.Normal,  0), new Loot("Cookies",  0,  LootPossibility.Normal,  2)}
      );
      public static CreatureData RiftBrood = new CreatureData("Rift Brood", 3000, 1600, 0, 0, 270, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Holy,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  20) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData RiftLord = new CreatureData("Rift Lord", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData RiftPhantom = new CreatureData("Rift Phantom", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData RiftScythe = new CreatureData("Rift Scythe", 3600, 2000, 0, 0, 1000, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  65), new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData RiftWorm = new CreatureData("Rift Worm", 2800, 1195, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Ice,  5), new DamageModifier(DamageType.Earth,  25) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData RoaringWaterElemental = new CreatureData("Roaring Water Elemental", 1750, 1300, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  45), new DamageModifier(DamageType.Holy,  40), new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() {  },
          new List<string>() { "BLUB BLUB",  "SWASHHH",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  179), new Loot("Iced Soil",  0,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.Rare,  2), new Loot("Giant Shimmering Pearl",  0,  LootPossibility.Rare,  2), new Loot("Northwind Rod",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Rocky = new CreatureData("Rocky", 390, 190, 0, 0, 80, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Another little gladiator!",  "Come into my embrace.",  },
          new List<Loot>() { }
      );
      public static CreatureData RontheRipper = new CreatureData("Ron the Ripper", 1500, 500, 0, 0, 410, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Muahaha!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Skulls",  0,  LootPossibility.Normal,  2), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Small Diamond",  0,  LootPossibility.Normal,  0), new Loot("Ron the Ripper's Sabre",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData RottietheRotworm = new CreatureData("Rottie the Rotworm", 65, 40, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  27), new Loot("Worms",  0,  LootPossibility.Normal,  5), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Legion Helmet",  0,  LootPossibility.Normal,  0), new Loot("Copper Shield",  0,  LootPossibility.Normal,  0), new Loot("Katana",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Rotworm = new CreatureData("Rotworm", 65, 40, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  27), new Loot("Worms",  0,  LootPossibility.Normal,  5), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Rare,  0), new Loot("Sword",  0,  LootPossibility.Rare,  0), new Loot("Legion Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Copper Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Katana",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData RotwormQueen = new CreatureData("Rotworm Queen", 105, 75, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  86), new Loot("Worm",  0,  LootPossibility.Normal,  47), new Loot("Gl",  0,  LootPossibility.Normal,  0), new Loot("rare)",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData RukorZad = new CreatureData("Rukor Zad", 380, 380, 0, 0, 370, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  5)},
          new List<DamageModifier>() {  },
          new List<string>() { "I can kill a man in a thous,  ways. And that`s only with a spoon!",  "You shouldn`t have come here!",  "Haiiii!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  50), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Scarab = new CreatureData("Scarab", 320, 120, 0, 0, 115, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  15), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  18) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  55), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Arrows",  0,  LootPossibility.Normal,  3), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Heavy Machete",  0,  LootPossibility.Rare,  0), new Loot("Scarab Coins",  0,  LootPossibility.Rare,  2), new Loot("Small Emerald",  3032,  LootPossibility.Rare,  0), new Loot("Small Amethysts",  0,  LootPossibility.VeryRare,  2), new Loot("Daramanian Mace",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Scorpion = new CreatureData("Scorpion", 45, 45, 0, 0, 67, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData SeaSerpent = new CreatureData("Sea Serpent", 1950, 2300, 0, 0, 800, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Fire,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  15), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "CHHHRRRR",  "HISSSS",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  234), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  3), new Loot("Viking Helmet",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  3), new Loot("Strong Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Serpent Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Spirit Cloak",  0,  LootPossibility.SemiRare,  0), new Loot("Ring of Healing",  0,  LootPossibility.Rare,  0), new Loot("Northwind Rod",  0,  LootPossibility.Rare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Glacier Amulet",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Glacier Kilt",  0,  LootPossibility.VeryRare,  0), new Loot("Focus Cape",  0,  LootPossibility.VeryRare,  0), new Loot("Leviathan's Amulet",  0,  LootPossibility.VeryRare,  0), new Loot("Crystalline Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Seagull = new CreatureData("Seagull", 25, 0, 0, 0, 3, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData SerpentSpawn = new CreatureData("Serpent Spawn", 3000, 3050, 0, 0, 1400, false, true, FrontAttack.Beam, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "HISSSS",  "I bring your deathhh,  mortalssss",  "Sssssouls for the one",  "Tsssse one will risssse again",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  219), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  3), new Loot("Charmer's Tiara",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Gemmed Book",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Green Mushroom",  3732,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.Normal,  0), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Power Bolt",  3450,  LootPossibility.Normal,  0), new Loot("Crown Armor",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("Mercenary Sword",  0,  LootPossibility.Rare,  0), new Loot("Noble Axe",  0,  LootPossibility.Rare,  0), new Loot("Old Parchment",  0,  LootPossibility.Rare,  0), new Loot("Strange Helmet",  0,  LootPossibility.Rare,  0), new Loot("Tower Shield",  0,  LootPossibility.Rare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.Rare,  0), new Loot("Royal Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.VeryRare,  0), new Loot("Swamplair Armor",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData ServantGolem = new CreatureData("Servant Golem", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Do you think I might become a real boy one day",  "How may I serve you Sir or Ma'am",  "Washing procedure initiated!",  "Can I help you",  "Scan result: dusty human! Cleansing initiated!",  "I am listening!",  "Where are we going Sir or Ma'am",  "Praise the Yalahari!",  "Is that love or do you have a magnet in your pocket",  "Move on! There's nothing to see!",  "Do you want to taste a sample of the newest dish",  "I hope I am not annoying Sir or Ma'am",  "WARNING: BAD HAIRCUT DETECTED! Initializing haircut procedure!",  "Mommy",  "Everything is working as intended!",  "Rrrtttarrrttarrrtta",  },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ShadowofBoreth = new CreatureData("Shadow of Boreth", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ShadowofLersatio = new CreatureData("Shadow of Lersatio", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ShadowofMarziel = new CreatureData("Shadow of Marziel", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Shardhead = new CreatureData("Shardhead", 800, 650, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Holy},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  195), new Loot("Ice Cubes",  0,  LootPossibility.Normal,  2), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Shard",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Sharptooth = new CreatureData("Sharptooth", 2500, 1600, 0, 0, 500, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "Gnarrr!",  "Tcharrr!",  "Rrrah!",  "Rraaar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  13), new Loot("Fishbone",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Sheep = new CreatureData("Sheep", 20, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Maeh",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4)}
      );
      public static CreatureData Shredderthrower = new CreatureData("Shredderthrower", 0, 0, 0, 0, 110, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Sibang = new CreatureData("Sibang", 225, 105, 0, 0, 95, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  25)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  15) },
          new List<string>() { "Eeeeek! Eeeeek!",  "Huh! Huh! Huh!",  "Ahhuuaaa!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Coconuts",  0,  LootPossibility.Normal,  5), new Loot("Bananas",  0,  LootPossibility.Normal,  12), new Loot("Oranges",  0,  LootPossibility.Normal,  5), new Loot("Melon",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Small Stones",  0,  LootPossibility.Normal,  3), new Loot("Ape Fur",  5883,  LootPossibility.Rare,  0)}
      );
      public static CreatureData SilverRabbit = new CreatureData("Silver Rabbit", 15, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Carrot",  3595,  LootPossibility.Normal,  1), new Loot("Blue Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Green Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Purple Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Red Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("Yellow Coloured Egg",  0,  LootPossibility.Normal,  0), new Loot("only during the Easter holiday)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData SirValorcrest = new CreatureData("Sir Valorcrest", 0, 1800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "I challenge you!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  93), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Vampire Lord Token",  0,  LootPossibility.Always,  0), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Sword Ring",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.Rare,  0), new Loot("Chaos Mace",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Skeleton = new CreatureData("Skeleton", 50, 35, 0, 0, 30, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Torches",  0,  LootPossibility.Normal,  2), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("Hatchet",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Viking Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Sword",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData SkeletonWarrior = new CreatureData("Skeleton Warrior", 65, 45, 0, 0, 43, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Bone",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("White Mushroom",  3723,  LootPossibility.Normal,  3), new Loot("Brown Mushroom",  3725,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Skunk = new CreatureData("Skunk", 20, 3, 0, 0, 8, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Worms",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  2)}
      );
      public static CreatureData SlickWaterElemental = new CreatureData("Slick Water Elemental", 550, 450, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  45), new DamageModifier(DamageType.Holy,  40), new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() {  },
          new List<string>() { "BLUUUUB",  "SPLISH SPLASH",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Shiver Arrow",  762,  LootPossibility.SemiRare,  3), new Loot("Iced Soil",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Slim = new CreatureData("Slim", 1025, 580, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Zhroozzzzs.",  },
          new List<Loot>() { }
      );
      public static CreatureData Slime = new CreatureData("Slime", 150, 160, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Blubb",  },
          new List<Loot>() { }
      );
      public static CreatureData Smuggler = new CreatureData("Smuggler", 130, 48, 0, 0, 60, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "You saw something you shouldn't!",  "I will silence you forever!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Torch",  0,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  0), new Loot("Knife",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.SemiRare,  0), new Loot("Deer Trophy",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData SmugglerBaronSilvertoe = new CreatureData("Smuggler Baron Silvertoe", 280, 170, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I will make your death look like an accident!",  "you should not have interferred with my bussiness!",  "Bribes are expensive,  murder is cheap!",  "I see some profit in your death!",  "I expect you to die!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Short Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Snake = new CreatureData("Snake", 15, 10, 0, 0, 9, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Zzzzzzt",  },
          new List<Loot>() { }
      );
      public static CreatureData SonofVerminor = new CreatureData("Son of Verminor", 8500, 5900, 0, 0, 1000, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { }
      );
      public static CreatureData SpawnofDespair = new CreatureData("Spawn of Despair", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  40)},
          new List<DamageModifier>() {  },
          new List<string>() { "Tibia will suffer ,  writhe today!,  imes of darkness are at h, ,  The light weakens! The darkness grows stronger!,  YOU CALLED US! HERE WE ARE!,  The world will end today,  HRAAAAAAAAAAH,  OUR DAY HAS COME!,  HIDE YOU WEAKLINGS!,  Give it up. You fragile beings cannot have hope to defeat us demons.",  },
          new List<Loot>() { new Loot("Create loot statistics)",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Spectre = new CreatureData("Spectre", 1350, 2100, 0, 0, 820, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  97), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  8), new DamageModifier(DamageType.Energy,  8) },
          new List<string>() { "Revenge ... is so ... sweet.",  "Life...force! Feed me your... lifeforce",  "Mor... tals!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  300), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  7), new Loot("Blank Rune",  0,  LootPossibility.Normal,  2), new Loot("S",  0,  LootPossibility.Normal,  0), new Loot("als",  0,  LootPossibility.Normal,  0), new Loot("Lyre",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.SemiRare,  0), new Loot("White Piece of Cloth",  0,  LootPossibility.SemiRare,  0), new Loot("Silver Brooch",  0,  LootPossibility.Rare,  0), new Loot("Death Ring",  0,  LootPossibility.Rare,  0), new Loot("Relic Sword",  0,  LootPossibility.Rare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Shadow Sceptre",  0,  LootPossibility.VeryRare,  0), new Loot("Demonbone Amulet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Spider = new CreatureData("Spider", 20, 12, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  5)}
      );
      public static CreatureData SpiritofEarth = new CreatureData("Spirit of Earth", 1294, 800, 0, 0, 640, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Show your strengh ... or perish.",  },
          new List<Loot>() { }
      );
      public static CreatureData SpiritofFire = new CreatureData("Spirit of Fire", 2210, 950, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Feel the heat.",  },
          new List<Loot>() { }
      );
      public static CreatureData SpiritofLight = new CreatureData("Spirit of Light", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { }
      );
      public static CreatureData SpiritofWater = new CreatureData("Spirit of Water", 1517, 850, 0, 0, 995, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "Blubb",  },
          new List<Loot>() { }
      );
      public static CreatureData SpitNettle = new CreatureData("Spit Nettle", 150, 20, 0, 0, 45, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  5), new Loot("Shadow Herb",  0,  LootPossibility.Normal,  0), new Loot("Sling Herb",  0,  LootPossibility.Normal,  2), new Loot("Grave Flower",  0,  LootPossibility.SemiRare,  0), new Loot("Seeds",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Splasher = new CreatureData("Splasher", 0, 500, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Drown},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25), new DamageModifier(DamageType.Earth,  15) },
          new List<string>() { "Qua hah tsh!",  "Teech tsha tshul!",  "Quara tsha Fach!",  "Tssssha Quara!",  "Blubber.",  "Blup.",  },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Squirrel = new CreatureData("Squirrel", 20, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Chchch",  },
          new List<Loot>() { new Loot("Walnut",  0,  LootPossibility.SemiRare,  0), new Loot("Peanut",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData Stalker = new CreatureData("Stalker", 120, 90, 0, 0, 100, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Holy,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  8), new Loot("Throwing Knife",  3298,  LootPossibility.Normal,  2), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Backpack",  0,  LootPossibility.Normal,  0), new Loot("Katana",  0,  LootPossibility.Normal,  0), new Loot("Obsidian Lance",  0,  LootPossibility.Rare,  0), new Loot("Stealth Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData StoneGolem = new CreatureData("Stone Golem", 270, 160, 0, 0, 110, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  15), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Small Stone",  1781,  LootPossibility.Normal,  4), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Carlin Sword",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Power Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Iron Ore",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Red Gem",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Stonecracker = new CreatureData("Stonecracker", 0, 3500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Earth,  75)},
          new List<DamageModifier>() {  },
          new List<string>() { "HUAHAHA!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  78), new Loot("meat",  3577,  LootPossibility.Normal,  3), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Behemoth Claw",  0,  LootPossibility.Normal,  0), new Loot("Crowbar",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Perfect Behemoth Fang",  0,  LootPossibility.Normal,  0), new Loot("Assassin Star",  0,  LootPossibility.Normal,  0), new Loot("War Axe",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  0), new Loot("Strange Symbol",  0,  LootPossibility.Normal,  0), new Loot("Dark Armor",  0,  LootPossibility.Normal,  0), new Loot("Steel Boots",  0,  LootPossibility.Normal,  0), new Loot("Giant Sword",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData SvorenTheMad = new CreatureData("Svoren The Mad", 6300, 3000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "NO mommy NO. Leave me alone!",  "Not that tower again!",  },
          new List<Loot>() { }
      );
      public static CreatureData SwampTroll = new CreatureData("Swamp Troll", 55, 25, 0, 0, 14, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "Me strong! Me ate spinach!",  "Groar!",  "Grrrr",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  5), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Fishing Rod",  3483,  LootPossibility.Rare,  0), new Loot("Troll Green",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Tarantula = new CreatureData("Tarantula", 225, 120, 0, 0, 90, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Time Ring",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData TargetDummy = new CreatureData("Target Dummy", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  70), new DamageModifier(DamageType.Holy,  95), new DamageModifier(DamageType.Death,  95), new DamageModifier(DamageType.Fire,  95), new DamageModifier(DamageType.Energy,  95), new DamageModifier(DamageType.Ice,  95), new DamageModifier(DamageType.Earth,  95), new DamageModifier(DamageType.Drown,  95)},
          new List<DamageModifier>() {  },
          new List<string>() { "I hope you are enjoying your sparring Sir or Ma'am!",  "Threat level rising!",  "Engaging in hostile interaction!",  "Rrrtttarrrttarrrtta",  "Please feel free to hit me Sir or Ma'am!",  "klonk klonk klonk",  "Self-diagnosis running.",  "Battle simulation proceeding.",  "Repairs initiated!",  },
          new List<Loot>() { }
      );
      public static CreatureData TerrorBird = new CreatureData("Terror Bird", 300, 150, 0, 0, 90, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Carrah! Carrah!",  "Gruuuh Gruuuh.",  "CRAAAHHH!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Worms",  0,  LootPossibility.Normal,  3), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Seeds",  0,  LootPossibility.SemiRare,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Feather Headdress",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Thalas = new CreatureData("Thalas", 4100, 2950, 0, 0, 1400, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "You will become a feast for my maggots!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  159), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  3), new Loot("Poison Dagger",  0,  LootPossibility.SemiRare,  0), new Loot("Time Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Serpent Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Green Gem",  0,  LootPossibility.VeryRare,  0), new Loot("Djinn Blade",  0,  LootPossibility.VeryRare,  0), new Loot("Cobrafang Dagger",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheAbomination = new CreatureData("The Abomination", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheBigBadOne = new CreatureData("The Big Bad One", 300, 170, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Wolf Paws",  0,  LootPossibility.Normal,  2), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Wolf Trophy",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheBloodtusk = new CreatureData("The Bloodtusk", 600, 300, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  4), new Loot("Tusks",  0,  LootPossibility.Normal,  2), new Loot("Mammoth Fur Cape",  0,  LootPossibility.Normal,  0), new Loot("Tusk Shield",  0,  LootPossibility.Normal,  0), new Loot("Furry Club",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheCount = new CreatureData("The Count", 4600, 1750, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("The Ring of the Count",  0,  LootPossibility.Always,  0), new Loot("War Hammer",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheDarkDancer = new CreatureData("The Dark Dancer", 855, 435, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Fire,  40)},
          new List<DamageModifier>() {  },
          new List<string>() { "I hope you like my voice!",  },
          new List<Loot>() { }
      );
      public static CreatureData TheEvilEye = new CreatureData("The Evil Eye", 1200, 750, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Inferior creatures,  bow before my power!",  "653768764!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  56), new Loot("Longsword",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Spellbook",  0,  LootPossibility.Normal,  0), new Loot("Two-H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Beholder Shield",  0,  LootPossibility.Normal,  0), new Loot("Terra Rod",  0,  LootPossibility.Normal,  0), new Loot("Beholder Helmet",  0,  LootPossibility.Normal,  0), new Loot("Terra Mantle",  0,  LootPossibility.Normal,  0), new Loot("Beholder Eye",  5898,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheFrogPrince = new CreatureData("The Frog Prince", 55, 0, 0, 0, 1, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  85), new DamageModifier(DamageType.Ice,  85)},
          new List<DamageModifier>() {  },
          new List<string>() { "Don't Kill me!!",  },
          new List<Loot>() { new Loot("None",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheHag = new CreatureData("The Hag", 935, 510, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() {  },
          new List<string>() { "If you think I am too old to fight then you're wrong.",  "I've forgotten more things than you have ever learned!",  "Let me teach you a few things youngster!",  "I'll teach you respect for the old!",  },
          new List<Loot>() { }
      );
      public static CreatureData TheHairyOne = new CreatureData("The Hairy One", 325, 115, 0, 0, 70, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20) },
          new List<string>() { "Hugah!",  "Ungh! Ungh!",  },
          new List<Loot>() { }
      );
      public static CreatureData TheHalloweenHare = new CreatureData("The Halloween Hare", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheHandmaiden = new CreatureData("The Handmaiden", 0, 7500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  184), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Power Ring",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Piece of Iron",  0,  LootPossibility.Normal,  0), new Loot("Big Bone",  0,  LootPossibility.Normal,  0), new Loot("Steel Boots",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("The H",  0,  LootPossibility.Normal,  0), new Loot("maiden's Protector",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheHornedFox = new CreatureData("The Horned Fox", 265, 300, 0, 0, 120, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "I'll be back!",  "Catch me,  if you can!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  74), new Loot("Piercing Bolts",  0,  LootPossibility.SemiRare,  14), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Shovel",  3457,  LootPossibility.Normal,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Bolts",  0,  LootPossibility.Normal,  0), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Scale Armor",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Chain Legs",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmets",  0,  LootPossibility.Normal,  0), new Loot("Soldier Helmet",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Hatchet",  0,  LootPossibility.Normal,  0), new Loot("Fishing Rod",  3483,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Combat Knife",  0,  LootPossibility.Normal,  0), new Loot("Dead Snake",  0,  LootPossibility.Normal,  0), new Loot("Carrots",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0), new Loot("Dwarven Helmet",  0,  LootPossibility.Rare,  0), new Loot("Nose Ring",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheImperor = new CreatureData("The Imperor", 15000, 8000, 0, 0, 2000, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Poke! Poke! lt",  "chucklegt",  "",  "Let me tickle you with my trident!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  3), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Crown Legs",  0,  LootPossibility.Normal,  0), new Loot("Pitchfork",  0,  LootPossibility.Normal,  0), new Loot("Guardian Shield",  0,  LootPossibility.Normal,  0), new Loot("Imperor's Trident",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheMany = new CreatureData("The Many", 0, 4000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Strong Mana Potions",  0,  LootPossibility.Normal,  5), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  5), new Loot("Gold Ingots",  0,  LootPossibility.Normal,  3), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Egg of The Many",  0,  LootPossibility.Always,  0), new Loot("Sacred Tree Amulet",  0,  LootPossibility.SemiRare,  0), new Loot("Medusa Shield",  0,  LootPossibility.SemiRare,  0), new Loot("Warrior Helmet",  0,  LootPossibility.Normal,  0), new Loot("Royal Helmet",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheMaskedMarauder = new CreatureData("The Masked Marauder", 6800, 3500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Didn't you leave your house door open",  "Oops,  your shoelaces are open!",  "Look! It's Ferumbras behind you!",  },
          new List<Loot>() { }
      );
      public static CreatureData TheMutatedPumpkin = new CreatureData("The Mutated Pumpkin", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I had the Halloween Hare for breakfast!",  "Your soul will be mine...wait,  wrong line",  "Trick or treat I saw death!",  "No wait! Don't kill me! It's me,  your friend!",  "Bunnies,  bah! I'm the real thing!",  "Muahahahaha!",  "I've come to avenge all those mutilated pumpkins!",  "Wait until I get you!",  "Fear the spirit of Halloween!",  },
          new List<Loot>() { new Loot("Yummy Gummy Worm",  0,  LootPossibility.Normal,  40), new Loot("Toy Spider",  0,  LootPossibility.Normal,  0), new Loot("Spiderwebs",  0,  LootPossibility.Normal,  0), new Loot("Bat Decoration",  0,  LootPossibility.Normal,  0), new Loot("Skeleton Decoration",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheNoxiousSpawn = new CreatureData("The Noxious Spawn", 9500, 6000, 0, 0, 0, false, true, FrontAttack.Wave, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I bring you deathhhh,  mortalssss",  },
          new List<Loot>() { new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  4), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Assassin Stars",  0,  LootPossibility.Normal,  100), new Loot("Power Bolts",  0,  LootPossibility.Normal,  29), new Loot("Green Mushrooms",  0,  LootPossibility.Normal,  0), new Loot("Medusa Shield",  0,  LootPossibility.Normal,  0), new Loot("Noble Axe",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.Normal,  0), new Loot("Mercenary Sword",  0,  LootPossibility.Normal,  0), new Loot("Claw of 'The Noxious Spawn'",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheObliverator = new CreatureData("The Obliverator", 9500, 6000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "NO ONE WILL BEAT ME",  },
          new List<Loot>() { }
      );
      public static CreatureData TheOldWhopper = new CreatureData("The Old Whopper", 785, 750, 0, 0, 175, false, true, FrontAttack.Wave, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Han oydar hot auden oydar",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  106), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheOldWidow = new CreatureData("The Old Widow", 3200, 2800, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  231), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  2), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Time Ring",  0,  LootPossibility.Normal,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  3), new Loot("Knight Armor",  0,  LootPossibility.Normal,  0), new Loot("Knight Legs",  0,  LootPossibility.Normal,  0), new Loot("Spider Silk",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ThePitLord = new CreatureData("The Pit Lord", 4500, 2500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "I won't let you escape!",  "I'LL GET YOU ALL!",  "I'll crush you beneath my feet!",  },
          new List<Loot>() { }
      );
      public static CreatureData ThePlasmother = new CreatureData("The Plasmother", 0, 8300, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10) },
          new List<string>() { "Blubb",  "Blubb Blubb",  "Blubberdiblubb",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  13), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Black Pearls",  0,  LootPossibility.Normal,  3), new Loot("Small Sapphires",  0,  LootPossibility.Normal,  3), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("The Plasmother's Remains",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheRuthlessHerald = new CreatureData("The Ruthless Herald", 0, 0, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Holy, DamageType.Death, DamageType.Fire, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "I am so proud of my son Orshabaal.",  "Have you heard,  the imperor is marrying an imp named April What a fool.",  "The Ruthless Seven are going to make the easter bunny an honorary member!",  "They are coming ... perhaps for YOU!",  "Beware! The Ruthless Seven are coming!",  "Killing me is imp-ossible,  because I am imp-roved! I am immune to any imp-act!",  "The one who kills me gets an imp-outfit.",  "Hey you! I've heard that! You're first to die when the masters come.",  "Nice to meet you. I am Harold,  the ruthless herald.",  "Have you seen my friend Harvey I could swear he's somewhere around.",  "My masters are on their way!",  },
          new List<Loot>() { new Loot("Won't give any loot because you can't kill them",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData TheSnapper = new CreatureData("The Snapper", 300, 150, 0, 0, 55, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  15)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  194), new Loot("Health Potions",  0,  LootPossibility.Normal,  5), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Crocodile Boots",  0,  LootPossibility.Normal,  0), new Loot("Knight Armor",  0,  LootPossibility.Rare,  0), new Loot("Life Ring",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData TheWeakenedCount = new CreatureData("The Weakened Count", 740, 450, 0, 0, 283, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { "1... 2... 2... Uh,  can't concentrate.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  92), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.VeryRare,  0), new Loot("The Ring of the Count",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Thief = new CreatureData("Thief", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ThornbackTortoise = new CreatureData("Thornback Tortoise", 300, 150, 0, 0, 112, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  45), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Tortoise Egg",  0,  LootPossibility.Normal,  3), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Brown Mushroom",  3725,  LootPossibility.Rare,  0), new Loot("White Mushroom",  3723,  LootPossibility.Rare,  0), new Loot("War Hammer",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Thul = new CreatureData("Thul", 3000, 1800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "Gaaahhh!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Shrimp",  0,  LootPossibility.Normal,  0), new Loot("Small Amethyst",  3033,  LootPossibility.Normal,  0), new Loot("White Pearl",  3026,  LootPossibility.Normal,  0), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Skull Helmet",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Cosmic Energy",  0,  LootPossibility.Normal,  0), new Loot("Relic Sword",  0,  LootPossibility.Rare,  0), new Loot("Fish Fin",  5895,  LootPossibility.Always,  0)}
      );
      public static CreatureData TibiaBug = new CreatureData("Tibia Bug", 270, 50, 0, 0, 70, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "My father was a year 2k bug.",  "Psst,  I'll make you rich.",  "You are bugged ... by me!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  11), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Tiger = new CreatureData("Tiger", 75, 40, 0, 0, 40, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  4)}
      );
      public static CreatureData TiquandasRevenge = new CreatureData("Tiquandas Revenge", 2400, 2635, 0, 0, 910, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  110), new Loot("Meat",  3577,  LootPossibility.Normal,  50), new Loot("Ham",  3582,  LootPossibility.Normal,  8), new Loot("Dark Mushroom",  0,  LootPossibility.Normal,  6), new Loot("Seeds",  0,  LootPossibility.Always,  3), new Loot("M",  0,  LootPossibility.Normal,  0), new Loot("rake",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Toad = new CreatureData("Toad", 135, 60, 0, 0, 48, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Ribbit! Ribbit!",  "Ribbit!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  12), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Club",  0,  LootPossibility.Normal,  0), new Loot("War Hammer",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData TormentedGhost = new CreatureData("Tormented Ghost", 210, 5, 0, 0, 170, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Physical, DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Haaahhh",  "Grrglll",  },
          new List<Loot>() { new Loot("None",  0,  LootPossibility.Normal,  0), new Loot("you can't open Ghost Residue",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Tortoise = new CreatureData("Tortoise", 185, 90, 0, 0, 50, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  35), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Hams",  0,  LootPossibility.Normal,  2), new Loot("Tortoise Eggs",  0,  LootPossibility.SemiRare,  3), new Loot("Tortoise Shield",  0,  LootPossibility.Rare,  0), new Loot("Turtle Shell",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData TortoiseAntiBotter = new CreatureData("Tortoise AntiBotter", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Troll = new CreatureData("Troll", 50, 20, 0, 0, 25, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  1) },
          new List<string>() { "Hmmm,  bugs",  "Hmmm,  dogs",  "Grrr",  "Groar",  "Gruntz!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  12), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("H",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Studded Club",  0,  LootPossibility.SemiRare,  0), new Loot("Silver Amulet",  3054,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData TrollChampion = new CreatureData("Troll Champion", 75, 40, 0, 0, 35, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  1) },
          new List<string>() { "Meee maity!",  "Grrrr",  "Whaaaz up!",  "Gruntz!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  12), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Spear",  3277,  LootPossibility.Normal,  0), new Loot("H",  0,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Leather Helmet",  0,  LootPossibility.Normal,  0), new Loot("Wooden Shield",  0,  LootPossibility.Normal,  0), new Loot("Rope",  3003,  LootPossibility.Normal,  0), new Loot("Studded Club",  0,  LootPossibility.Normal,  0), new Loot("Arrows",  0,  LootPossibility.SemiRare,  5), new Loot("Silver Amulet",  3054,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData TrollLegionnaire = new CreatureData("Troll Legionnaire", 210, 140, 0, 0, 270, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "Attack!",  "Graaaaar!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  191), new Loot("Throwing Stars",  0,  LootPossibility.Normal,  10), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData UndeadDragon = new CreatureData("Undead Dragon", 8350, 7200, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Fire, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Ice,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25) },
          new List<string>() { "FEEEED MY ETERNAL HUNGER!",  "I SENSE LIFE",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Power Bolts",  0,  LootPossibility.Normal,  6), new Loot("Onyx Arrows",  0,  LootPossibility.Normal,  10), new Loot("Platinum Coins",  0,  LootPossibility.Normal,  5), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Broadsword",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Golden Mug",  0,  LootPossibility.Normal,  0), new Loot("Soul Orb",  0,  LootPossibility.Normal,  0), new Loot("Torn Book",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Dragonbone Staff",  0,  LootPossibility.SemiRare,  0), new Loot("Knight Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Life Crystal",  0,  LootPossibility.SemiRare,  0), new Loot("Hardened Bone",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Underworld Rod",  0,  LootPossibility.Rare,  0), new Loot("Gold Ingot",  0,  LootPossibility.Rare,  0), new Loot("War Axe",  0,  LootPossibility.Rare,  0), new Loot("Dragon Scale Mail",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Royal Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Skull Helmet",  0,  LootPossibility.VeryRare,  0), new Loot("Divine Plate",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData UndeadGladiator = new CreatureData("Undead Gladiator", 1000, 800, 0, 0, 385, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  80), new DamageModifier(DamageType.Energy,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { "Let's battle it out in a duel!",  "Bring it!",  "I'll fight here in eternity ,  beyond.",  "I will not give up!",  "Another foolish adventurer who tries to beat me.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Throwing Star",  3287,  LootPossibility.Normal,  18), new Loot("Hunting Spear",  3347,  LootPossibility.Normal,  0), new Loot("Scimitar",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Brass Armor",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Belted Cape",  0,  LootPossibility.SemiRare,  0), new Loot("Dark Helmet",  0,  LootPossibility.SemiRare,  0), new Loot("Crusader Helmet",  0,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Knight Axe",  0,  LootPossibility.Rare,  0), new Loot("Beastslayer Axe",  0,  LootPossibility.VeryRare,  0), new Loot("Warrior's Sweat",  0,  LootPossibility.VeryRare,  0), new Loot("Mercenary Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Glorious Axe",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData UndeadJester = new CreatureData("Undead Jester", 355, 5, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  20) },
          new List<string>() { "Who's bad",  "I have a cunning plan!",  "Resistance is futile! You will be amused!",  "Pull my finger!",  "Why did the chicken cross the road TO KILL YOU!!!",  "I will teach you all to mock me!",  "He who laughs last,  Laughs best!",  "Th-Th-Th-That's all,  folks!",  "A zathroth priest,  a druid ,  a paladin walk into a bar ...",  "Ha Ha!",  "Doh!",  "Zathroth made me do it!",  "And now for something completely different!",  "You think this is funny now",  "Are we having fun yet",  "Did i do that",  },
          new List<Loot>() { new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Bar of Chocolate",  0,  LootPossibility.Normal,  0), new Loot("Spellw",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Supersoft Pillow",  0,  LootPossibility.Normal,  0), new Loot("Suspicious Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Red Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Yellow Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("White Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Green Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Blue Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Brown Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("The Head of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("Part of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("Part of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("Part of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("Part of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("The Torso of a Jester Doll",  0,  LootPossibility.Normal,  0), new Loot("Piggy Bank",  0,  LootPossibility.Normal,  0), new Loot("Toy Mouse",  0,  LootPossibility.Normal,  0), new Loot("Green Perch",  0,  LootPossibility.Normal,  0), new Loot("Rainbow Trout",  0,  LootPossibility.Normal,  0), new Loot("Marlin",  0,  LootPossibility.Normal,  0), new Loot("Ice Cream Cone",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData UndeadMinion = new CreatureData("Undead Minion", 850, 550, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Blank Rune",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0), new Loot("Guardian Shield",  0,  LootPossibility.Rare,  0), new Loot("Surprise Bag",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Ungreez = new CreatureData("Ungreez", 8200, 500, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  30), new DamageModifier(DamageType.Death,  20), new DamageModifier(DamageType.Energy,  55)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "I teach you that even heroes can die!",  "You will die begging like the others did!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  90), new Loot("Fire Mushroom",  0,  LootPossibility.Normal,  6), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Updates85 = new CreatureData("Updates85", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Ushuriel = new CreatureData("Ushuriel", 0, 12015, 0, 0, 2600, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  40), new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Ice,  30), new DamageModifier(DamageType.Earth,  60)},
          new List<DamageModifier>() {  },
          new List<string>() { "You can't run or hide forever!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coins 0-3 Small Diamonds",  0,  LootPossibility.Normal,  30), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  8), new Loot("Black Pearls",  0,  LootPossibility.Normal,  14), new Loot("Small Emeralds",  0,  LootPossibility.Normal,  6), new Loot("White Pearls",  0,  LootPossibility.Normal,  14), new Loot("Small Amethysts",  0,  LootPossibility.Normal,  17), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  2), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  8), new Loot("Brown Mushroom",  3725,  LootPossibility.Normal,  30), new Loot("Demon Horn",  0,  LootPossibility.Normal,  2), new Loot("Crimson Sword",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.Normal,  7), new Loot("Hardened Bone",  0,  LootPossibility.Normal,  20), new Loot("Orb",  0,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Normal,  0), new Loot("Snakebite Rod",  0,  LootPossibility.Normal,  0), new Loot("Double Axe",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Magic Light W",  0,  LootPossibility.Normal,  0), new Loot("",  0,  LootPossibility.Normal,  0), new Loot("Death Ring",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Stealth Ring",  0,  LootPossibility.Normal,  0), new Loot("Gold Ring",  0,  LootPossibility.Normal,  0), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Silver Dagger",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0), new Loot("Silver Amulet",  3054,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Devil Helmet",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Ice Rapier",  0,  LootPossibility.Normal,  0), new Loot("Ring Of Healing",  0,  LootPossibility.Normal,  0), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("Of Decay",  0,  LootPossibility.Normal,  0), new Loot("Moonlight Rod",  0,  LootPossibility.Normal,  0), new Loot("Fire Axe",  0,  LootPossibility.Normal,  0), new Loot("Demon Shield",  0,  LootPossibility.Normal,  0), new Loot("Giant Sword",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Enchanted Chicken Wing",  0,  LootPossibility.Normal,  0), new Loot("Mysterious Voodoo Skull",  0,  LootPossibility.Normal,  0), new Loot("Green Gem",  0,  LootPossibility.Normal,  0), new Loot("Blue Gem",  0,  LootPossibility.Normal,  0), new Loot("Royal Helmet",  0,  LootPossibility.Normal,  0), new Loot("Skull Helmet",  0,  LootPossibility.Normal,  0), new Loot("Crown Helmet",  0,  LootPossibility.Normal,  0), new Loot("Warrior Helmet",  0,  LootPossibility.Normal,  0), new Loot("Thaian Sword",  0,  LootPossibility.Normal,  0), new Loot("Huge Chunk of Crude Iron",  0,  LootPossibility.Normal,  0), new Loot("Spirit Container",  0,  LootPossibility.Normal,  0), new Loot("Warrior's Sweat",  0,  LootPossibility.Normal,  0), new Loot("Spike Sword",  0,  LootPossibility.Normal,  0), new Loot("Fire Sword",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Dragon Slayer",  0,  LootPossibility.Normal,  0), new Loot("Unholy Book",  0,  LootPossibility.Rare,  0), new Loot("Runed Sword",  0,  LootPossibility.Rare,  0)}
      );
      public static CreatureData Valkyrie = new CreatureData("Valkyrie", 190, 85, 0, 0, 120, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  1), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  1) },
          new List<string>() { "Another head for me!",  "Head off!",  "Your head will be mine!",  "St,  still!",  "One more head for me!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  12), new Loot("Meat",  3577,  LootPossibility.Normal,  3), new Loot("Apples",  0,  LootPossibility.Normal,  4), new Loot("Skulls",  0,  LootPossibility.Normal,  2), new Loot("Dagger",  0,  LootPossibility.Normal,  0), new Loot("Spears",  0,  LootPossibility.Normal,  3), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Chain Armor",  0,  LootPossibility.Normal,  0), new Loot("Hunting Spear",  3347,  LootPossibility.SemiRare,  0), new Loot("Plate Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Necklace",  3008,  LootPossibility.Rare,  0), new Loot("Double Axe",  0,  LootPossibility.Rare,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Rare,  0), new Loot("Health Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Small Ruby",  0,  LootPossibility.VeryRare,  0), new Loot("Small Diamond",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Vampire = new CreatureData("Vampire", 475, 305, 0, 0, 300, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  35)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  25), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "BLOOD!",  "Let me kiss your neck",  "I smell warm blood!",  "I call you my bats!,  come!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  20), new Loot("Bag",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Bowl",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Normal,  0), new Loot("Grave Flower",  0,  LootPossibility.Normal,  0), new Loot("Katana",  0,  LootPossibility.Normal,  0), new Loot("Bronze Amulet",  3056,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Black Pearl",  3027,  LootPossibility.SemiRare,  0), new Loot("Strange Helmet",  0,  LootPossibility.Rare,  0), new Loot("Spike Sword",  0,  LootPossibility.Rare,  0), new Loot("Ice Rapier",  0,  LootPossibility.Rare,  0), new Loot("Emerald Bangle",  0,  LootPossibility.Rare,  0), new Loot("Vampire Shield",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData VampireBride = new CreatureData("Vampire Bride", 1200, 1050, 0, 0, 670, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  10), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20), new DamageModifier(DamageType.Drown,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Kneel before your Mistress!",  "Dead is the new alive.",  "Come,  let me kiss you,  darling. Oh wait,  I meant kill.",  "Enjoy the pain - I know you love it.",  "Are you suffering nicely enough",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  130), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Moonlight Rod",  0,  LootPossibility.Rare,  0), new Loot("Small Diamond",  0,  LootPossibility.Rare,  2), new Loot("Emerald Bangle",  0,  LootPossibility.Rare,  0), new Loot("Hibiscus Dress",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Velvet Tapestry",  0,  LootPossibility.Rare,  0), new Loot("Flower Bouquet",  0,  LootPossibility.Rare,  0), new Loot("Mysterious Voodoo Skull",  0,  LootPossibility.VeryRare,  0), new Loot("Boots of Haste",  0,  LootPossibility.VeryRare,  0), new Loot("Blood Goblet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData VampirePig = new CreatureData("Vampire Pig", 305, 165, 0, 0, 180, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10)},
          new List<DamageModifier>() {  },
          new List<string>() { "Oink",  "Oink oink",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  40)}
      );
      public static CreatureData Vashresamun = new CreatureData("Vashresamun", 4000, 2950, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  20), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() {  },
          new List<string>() { "If music is the food of death,  drop dead.",  "Are you enjoying my music",  "Come my maidens,  we have visitors!",  "Chakka Chakka!",  "Heheheheee!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  235), new Loot("White Pearl",  3026,  LootPossibility.Normal,  0), new Loot("Lute",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.SemiRare,  0), new Loot("Crystal Ring",  0,  LootPossibility.Rare,  0), new Loot("Panpipes",  0,  LootPossibility.Rare,  0), new Loot("Crystal Mace",  0,  LootPossibility.VeryRare,  0), new Loot("Ancient Tiara",  0,  LootPossibility.VeryRare,  0), new Loot("Blue Note",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Verminor = new CreatureData("Verminor", 160000, 80000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Expect very rare items",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData WarGolem = new CreatureData("War Golem", 4300, 2750, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  25), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  75), new DamageModifier(DamageType.Fire,  15), new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Ice,  30), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() {  },
          new List<string>() { "Azerus barada nikto!",  "Klonk klonk klonk",  "Engaging Enemy!",  "Threat level processed.",  "Charging weapon systems!",  "Auto repair in progress.",  "The battle is joined!",  "Termination initialized!",  "Rrrtttarrrttarrrtta",  "Eliminated",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  170), new Loot("Nails",  0,  LootPossibility.Normal,  5), new Loot("Two H",  0,  LootPossibility.Normal,  0), new Loot("ed Sword",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Battle Shield",  0,  LootPossibility.Normal,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.SemiRare,  0), new Loot("Berserk Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Epee",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Club Ring",  0,  LootPossibility.Rare,  0), new Loot("Dwarven Ring",  0,  LootPossibility.Rare,  0), new Loot("Crystal Pedestal",  0,  LootPossibility.Rare,  0), new Loot("Crystal of Power",  0,  LootPossibility.VeryRare,  0), new Loot("Lightning Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Steel Boots",  0,  LootPossibility.VeryRare,  0), new Loot("Berserker",  0,  LootPossibility.VeryRare,  0), new Loot("Jade Hammer",  0,  LootPossibility.VeryRare,  0), new Loot("Bonebreaker",  0,  LootPossibility.VeryRare,  0), new Loot("Life Crystal",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData WarWolf = new CreatureData("War Wolf", 140, 55, 0, 0, 50, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  10) },
          new List<string>() { "Yoooohhuuuu!",  "Grrrrrrr",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  2), new Loot("Wolf Paw",  5897,  LootPossibility.Rare,  0), new Loot("Wolf Trophy",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Warlock = new CreatureData("Warlock", 3500, 4000, 0, 0, 0, false, true, FrontAttack.Beam, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  90)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Even a rat is a better mage than you!",  "Learn the secret of our magic! YOUR death!",  "We don't like intruders!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Cherries",  0,  LootPossibility.Normal,  5), new Loot("Bread",  3600,  LootPossibility.Normal,  0), new Loot("C",  0,  LootPossibility.Normal,  0), new Loot("lestick",  0,  LootPossibility.Normal,  0), new Loot("Crystal Ring",  0,  LootPossibility.Normal,  0), new Loot("Dark Mushroom",  0,  LootPossibility.Normal,  0), new Loot("Energy Ring",  0,  LootPossibility.Normal,  0), new Loot("Inkwell",  0,  LootPossibility.Normal,  0), new Loot("Mind Stone",  0,  LootPossibility.Normal,  0), new Loot("Poison Dagger",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Talon",  0,  LootPossibility.Normal,  0), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.SemiRare,  0), new Loot("Assassin Star",  0,  LootPossibility.SemiRare,  4), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  0), new Loot("Red Tome",  0,  LootPossibility.Rare,  0), new Loot("Ring of the Sky",  0,  LootPossibility.Rare,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Rare,  0), new Loot("Lightning Robe",  0,  LootPossibility.VeryRare,  0), new Loot("Lightning Legs",  0,  LootPossibility.VeryRare,  0), new Loot("Golden Armor",  0,  LootPossibility.VeryRare,  0), new Loot("Piggy Bank",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData WarlordRuzad = new CreatureData("Warlord Ruzad", 2500, 1700, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10), new DamageModifier(DamageType.Fire,  80), new DamageModifier(DamageType.Energy,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Earth,  10) },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  59), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Fish",  3578,  LootPossibility.Normal,  0), new Loot("Scimitar",  0,  LootPossibility.Normal,  0), new Loot("Brass Legs",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Orcish Axe",  0,  LootPossibility.Normal,  0), new Loot("Protection Amulet",  3084,  LootPossibility.Normal,  0), new Loot("Plate Armor",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Wasp = new CreatureData("Wasp", 35, 24, 0, 0, 20, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  10) },
          new List<string>() { "Bssssss",  },
          new List<Loot>() { new Loot("Honeycomb",  0,  LootPossibility.SemiRare,  0)}
      );
      public static CreatureData WaterElemental = new CreatureData("Water Elemental", 550, 650, 0, 0, 560, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  70), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  25) },
          new List<string>() { "Splish splash",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  0), new Loot("Sword",  0,  LootPossibility.Normal,  0), new Loot("Steel Shield",  0,  LootPossibility.Normal,  0), new Loot("Worn Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Fishbone",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Rusty Legs",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Life Ring",  0,  LootPossibility.SemiRare,  0), new Loot("Small Emerald",  3032,  LootPossibility.SemiRare,  0), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  0), new Loot("Energy Ring",  0,  LootPossibility.Rare,  0), new Loot("Giant Shimmering Pearl",  0,  LootPossibility.Rare,  0), new Loot("Goldfish Bowl",  0,  LootPossibility.VeryRare,  0), new Loot("Leviathan's Amulet",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Webster = new CreatureData("Webster", 1750, 1200, 0, 0, 270, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice},
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { "You are lost.",  "Come my little morsel.",  },
          new List<Loot>() { }
      );
      public static CreatureData Werewolf = new CreatureData("Werewolf", 1955, 1900, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  5), new DamageModifier(DamageType.Earth,  5)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  5), new DamageModifier(DamageType.Fire,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "BLOOD!",  "HRAAAAAAAAAARRRRRR!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  225), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Plate Shield",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.SemiRare,  0), new Loot("Troll Green",  0,  LootPossibility.SemiRare,  0), new Loot("Wolf Paw",  5897,  LootPossibility.SemiRare,  0), new Loot("Great Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Plate Legs",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Rare,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Time Ring",  0,  LootPossibility.Rare,  0), new Loot("Ultimate Health Potion",  0,  LootPossibility.Rare,  0), new Loot("Berserk Potion",  0,  LootPossibility.VeryRare,  0), new Loot("Epee",  0,  LootPossibility.VeryRare,  0), new Loot("Dreaded Cleaver",  0,  LootPossibility.VeryRare,  0), new Loot("Relic Sword",  0,  LootPossibility.VeryRare,  0), new Loot("Platinum Amulet",  3055,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData WildWarrior = new CreatureData("Wild Warrior", 135, 60, 0, 0, 70, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  10)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  5), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "Gimme your money!",  "An enemy!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  30), new Loot("Meat",  3577,  LootPossibility.Normal,  0), new Loot("Axe",  0,  LootPossibility.Normal,  0), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Brass Shield",  0,  LootPossibility.Normal,  0), new Loot("Leather Legs",  0,  LootPossibility.Normal,  0), new Loot("Chain Helmet",  0,  LootPossibility.Normal,  0), new Loot("Iron Helmet",  0,  LootPossibility.Rare,  0), new Loot("Brass Armor",  0,  LootPossibility.Rare,  0), new Loot("Steel Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Doll",  0,  LootPossibility.VeryRare,  0), new Loot("War Hammer",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData WinterWolf = new CreatureData("Winter Wolf", 30, 20, 0, 0, 20, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Ice,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Energy,  1) },
          new List<string>() { },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  2)}
      );
      public static CreatureData Wisp = new CreatureData("Wisp", 115, 0, 0, 0, 7, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  65), new DamageModifier(DamageType.Fire,  10), new DamageModifier(DamageType.Energy,  30), new DamageModifier(DamageType.Earth,  90)},
          new List<DamageModifier>() {  },
          new List<string>() { "Crackle!",  "Tsshh",  },
          new List<Loot>() { new Loot("Moon Backpack",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Witch = new CreatureData("Witch", 300, 120, 0, 0, 115, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  20)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "Herba buridia ex!",  "Horax Pokti! Hihihi!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  10), new Loot("Cookies",  0,  LootPossibility.Normal,  8), new Loot("Wolf Tooth Chain",  0,  LootPossibility.Normal,  0), new Loot("Sickle",  0,  LootPossibility.Normal,  0), new Loot("Broom",  0,  LootPossibility.Normal,  0), new Loot("Leather Boots",  0,  LootPossibility.Normal,  0), new Loot("Coat",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Cheese",  3607,  LootPossibility.Normal,  0), new Loot("Star Herb",  0,  LootPossibility.Normal,  0), new Loot("Garlic Necklace",  3083,  LootPossibility.Rare,  0), new Loot("Silver Dagger",  0,  LootPossibility.Rare,  0), new Loot("Necrotic Rod",  0,  LootPossibility.Rare,  0), new Loot("Fern",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Wolf = new CreatureData("Wolf", 25, 18, 0, 0, 19, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  1), new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  1), new DamageModifier(DamageType.Ice,  1) },
          new List<string>() { "Yoooohhuuuu!",  "Grrrrrrr",  },
          new List<Loot>() { new Loot("Meat",  3577,  LootPossibility.Normal,  2), new Loot("Worm",  0,  LootPossibility.Normal,  0), new Loot("Wolf Paw",  5897,  LootPossibility.Rare,  0)}
      );
      public static CreatureData WorkerGolem = new CreatureData("Worker Golem", 1470, 1250, 0, 0, 361, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  10), new DamageModifier(DamageType.Holy,  50), new DamageModifier(DamageType.Death,  10), new DamageModifier(DamageType.Ice,  10), new DamageModifier(DamageType.Earth,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "INTRUDER ALARM!",  "klonk klonk klonk",  "Rrrtttarrrttarrrtta",  "Awaiting orders.",  "Secret objective complete.",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  131), new Loot("Nails",  0,  LootPossibility.Normal,  5), new Loot("Small Diamond",  0,  LootPossibility.SemiRare,  3), new Loot("Great Health Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Great Mana Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Berserk Potion",  0,  LootPossibility.SemiRare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Rusty Legs",  0,  LootPossibility.Normal,  0), new Loot("Iron Ore",  0,  LootPossibility.Rare,  0), new Loot("Life Crystal",  0,  LootPossibility.Rare,  0), new Loot("War Hammer",  0,  LootPossibility.Rare,  0), new Loot("Crystal Pedestal",  0,  LootPossibility.Rare,  0), new Loot("Spiked Squelcher",  0,  LootPossibility.Rare,  0), new Loot("Gear Wheel",  0,  LootPossibility.VeryRare,  2), new Loot("Might Ring",  0,  LootPossibility.VeryRare,  0), new Loot("Bonebreaker",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Wyrm = new CreatureData("Wyrm", 1825, 1550, 0, 0, 500, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  20), new DamageModifier(DamageType.Earth,  75)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Death,  5), new DamageModifier(DamageType.Ice,  5) },
          new List<string>() { "GRROARR",  "GRRR",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  0), new Loot("Burst Arrows",  0,  LootPossibility.Normal,  10), new Loot("Dragon Hams",  0,  LootPossibility.Normal,  3), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Crossbow",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Plate Legs",  0,  LootPossibility.Normal,  0), new Loot("Small Diamonds",  0,  LootPossibility.SemiRare,  3), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Draconia",  0,  LootPossibility.SemiRare,  0), new Loot("Lightning Pendant",  0,  LootPossibility.Rare,  0), new Loot("Focus Cape",  0,  LootPossibility.Rare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Starstorm",  0,  LootPossibility.Rare,  0), new Loot("Hibiscus Dress",  0,  LootPossibility.Rare,  0), new Loot("Shockwave Amulet",  0,  LootPossibility.VeryRare,  0), new Loot("Dragonbone Staff",  0,  LootPossibility.VeryRare,  0), new Loot("Composite Hornbow",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Wyvern = new CreatureData("Wyvern", 795, 515, 0, 0, 140, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Earth},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  10)},
          new List<DamageModifier>() {  },
          new List<string>() { "Shriiiek",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  75), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  3), new Loot("Gemmed Book",  0,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Bag",  0,  LootPossibility.SemiRare,  0), new Loot("Power Bolt",  3450,  LootPossibility.SemiRare,  2), new Loot("Emerald Bangle",  0,  LootPossibility.SemiRare,  0), new Loot("W",  0,  LootPossibility.Normal,  0), new Loot("of Inferno",  0,  LootPossibility.Rare,  0), new Loot("Small Sapphire",  0,  LootPossibility.Rare,  0), new Loot("Wyvern Fang",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData Xenia = new CreatureData("Xenia", 200, 255, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  44), new Loot("Sabre",  0,  LootPossibility.Normal,  0), new Loot("Skull",  0,  LootPossibility.Normal,  2), new Loot("Studded Shield",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData YagatheCrone = new CreatureData("Yaga the Crone", 620, 375, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Energy},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  1)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Death,  5) },
          new List<string>() { "Where did I park my hut",  "You will taste so sweet!",  "Hexipooh,  bewitched are you!",  },
          new List<Loot>() { new Loot("Cookies",  0,  LootPossibility.Normal,  8), new Loot("Broom",  0,  LootPossibility.Normal,  0), new Loot("Coat",  0,  LootPossibility.Normal,  0), new Loot("Cape",  0,  LootPossibility.Normal,  0), new Loot("Star Herb",  0,  LootPossibility.Normal,  0), new Loot("Wolf Tooth Chain",  0,  LootPossibility.Normal,  0), new Loot("Garlic Necklace",  3083,  LootPossibility.Normal,  0), new Loot("Necrotic Rod",  0,  LootPossibility.SemiRare,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Yakchal = new CreatureData("Yakchal", 0, 4400, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Holy,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Energy,  5) },
          new List<string>() { "YOU BETTER DIE TO MY MINIONS BECAUSE YOU'LL WISH YOU DID IF I COME FOR YOU!",  "You are mine!",  "I will make you all pay!",  "No one will stop my plans!",  "You are responsible for this!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  88), new Loot("Blue Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("Crystal Sword",  0,  LootPossibility.Normal,  0), new Loot("Life Ring",  0,  LootPossibility.Normal,  0), new Loot("Mastermind Potion",  0,  LootPossibility.Normal,  0), new Loot("Berserk Potion",  0,  LootPossibility.Normal,  0), new Loot("Bullseye Potion",  0,  LootPossibility.Normal,  0), new Loot("Earmuffs",  0,  LootPossibility.Normal,  0), new Loot("Dragon Necklace",  3085,  LootPossibility.Normal,  0), new Loot("Glacier Kilt",  0,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Rare,  0), new Loot("Glacier Robe",  0,  LootPossibility.Rare,  0), new Loot("Queen's Sceptre",  0,  LootPossibility.Rare,  0), new Loot("Skull Staff",  0,  LootPossibility.Rare,  0), new Loot("Gold Ingot",  0,  LootPossibility.Rare,  0), new Loot("Robe of the Ice Queen",  0,  LootPossibility.VeryRare,  0), new Loot("Shard",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Yalahari = new CreatureData("Yalahari", 0, 0, -1, -1, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() {  },
          new List<string>() { "",  },
          new List<Loot>() { new Loot("",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Yeti = new CreatureData("Yeti", 950, 460, 0, 0, 555, false, false, FrontAttack.None, 
          new List<DamageType>() { },
          new List<DamageModifier>() { },
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  1) },
          new List<string>() { "Yooodelaaahooohooo",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  100), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Ham",  3582,  LootPossibility.Normal,  5), new Loot("Snowballs",  0,  LootPossibility.Normal,  22), new Loot("Bunnyslippers",  0,  LootPossibility.VeryRare,  0)}
      );
      public static CreatureData YoungSeaSerpent = new CreatureData("Young Sea Serpent", 1050, 1000, 0, 0, 600, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Ice, DamageType.Earth, DamageType.Drown},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  30)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  20), new DamageModifier(DamageType.Death,  15), new DamageModifier(DamageType.Energy,  10) },
          new List<string>() { "HISSSS",  "CHHHRRRR",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  162), new Loot("Dragon Ham",  3583,  LootPossibility.Normal,  2), new Loot("Meat",  3577,  LootPossibility.Normal,  4), new Loot("Small Sapphire",  0,  LootPossibility.SemiRare,  0), new Loot("Dark Armor",  0,  LootPossibility.SemiRare,  0), new Loot("Stealth Ring",  0,  LootPossibility.Rare,  0), new Loot("Strong Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  2), new Loot("Battle Axe",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Morning Star",  0,  LootPossibility.Normal,  0), new Loot("Life Crystal",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Zarabustor = new CreatureData("Zarabustor", 5100, 8000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Energy, DamageType.Ice},
          new List<DamageModifier>() { new DamageModifier(DamageType.Earth,  90)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  1), new DamageModifier(DamageType.Holy,  1) },
          new List<string>() { "Killing is such a splendid diversion from my studies.",  "Time to test my newest spells!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  150), new Loot("Small Sapphire",  0,  LootPossibility.Normal,  2), new Loot("Assassin Star",  0,  LootPossibility.Normal,  1), new Loot("Poison Dagger",  0,  LootPossibility.Normal,  0), new Loot("Blue Robe",  0,  LootPossibility.Normal,  0), new Loot("Might Ring",  0,  LootPossibility.Normal,  0), new Loot("Skull Staff",  0,  LootPossibility.Normal,  0), new Loot("Golden Armor",  0,  LootPossibility.Normal,  0), new Loot("Lightning Legs",  0,  LootPossibility.Normal,  0), new Loot("Spellbook of Mind Control",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData ZevelonDuskbringer = new CreatureData("Zevelon Duskbringer", 1400, 1800, 0, 0, 0, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  50)},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  30) },
          new List<string>() { "I want Your Blood",  "Come Here!",  "I will be still around when my 'noble' race is gone",  "Human blood is not suitable for drinking!",  "Your short live is coming to an end.",  "Ashari Mortals. Come ,  stay forever!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  75), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  5), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  0), new Loot("Strong Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Ring of Healing",  0,  LootPossibility.Normal,  0), new Loot("Vampire Shield",  0,  LootPossibility.VeryRare,  0), new Loot("Vampire Lord Token",  0,  LootPossibility.Always,  0)}
      );
      public static CreatureData Zombie = new CreatureData("Zombie", 500, 280, 0, 0, 203, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Energy, DamageType.Ice, DamageType.Earth, DamageType.Drown, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  50)},
          new List<DamageModifier>() {  },
          new List<string>() { "Mst.... klll....",  "Whrrrr... ssss.... mmm.... grrrrl",  "Dnnnt... cmmm... clsrrr....",  "Httt.... hmnnsss...",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  64), new Loot("Mace",  0,  LootPossibility.Normal,  0), new Loot("Torch",  0,  LootPossibility.Normal,  0), new Loot("Brass Helmet",  0,  LootPossibility.Normal,  0), new Loot("Halberd",  0,  LootPossibility.Normal,  0), new Loot("Rotten Meat",  0,  LootPossibility.Normal,  0), new Loot("Steel Helmet",  0,  LootPossibility.Normal,  0), new Loot("Battle Hammer",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Normal,  0), new Loot("Mana Potion",  0,  LootPossibility.Rare,  0), new Loot("Simple Dress",  0,  LootPossibility.Rare,  0), new Loot("Life Ring",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Zoralurk = new CreatureData("Zoralurk", 0, 0, 0, 0, 2587, false, false, FrontAttack.None, 
          new List<DamageType>() { DamageType.Fire, DamageType.Fire},
          new List<DamageModifier>() { new DamageModifier(DamageType.Physical,  60), new DamageModifier(DamageType.Holy,  40), new DamageModifier(DamageType.Death,  90), new DamageModifier(DamageType.Energy,  60), new DamageModifier(DamageType.Ice,  50), new DamageModifier(DamageType.Earth,  80)},
          new List<DamageModifier>() {  },
          new List<string>() { "I AM ZORALURK,  THE DEMON WITH A THOUSAND FACES",  "BRING IT,  COCKROACHES!",  },
          new List<Loot>() { new Loot("Unknown",  0,  LootPossibility.Normal,  0)}
      );
      public static CreatureData Zugurosh = new CreatureData("Zugurosh", 95000, 10000, 0, 0, 0, false, true, FrontAttack.None, 
          new List<DamageType>() { DamageType.Death, DamageType.Paralysis},
          new List<DamageModifier>() { new DamageModifier(DamageType.Fire,  30), new DamageModifier(DamageType.Energy,  20), new DamageModifier(DamageType.Ice,  15), new DamageModifier(DamageType.Earth,  40)},
          new List<DamageModifier>() {  },
          new List<string>() { "You will run out of resources soon enough!",  "One little mistake  ,  your all are mine!",  "I sense your strength fading!",  "I know you will show a weakness!",  "Your fear will make you prone to mistakes!",  },
          new List<Loot>() { new Loot("Gold Coin",  3031,  LootPossibility.Normal,  134), new Loot("Black Pearl",  3027,  LootPossibility.Normal,  11), new Loot("Demonic Essence",  0,  LootPossibility.Normal,  0), new Loot("Small Saphire",  0,  LootPossibility.Normal,  7), new Loot("Onyx Arrow",  0,  LootPossibility.Normal,  8), new Loot("Platinum Coin",  3035,  LootPossibility.Normal,  28), new Loot("Red Piece of Cloth 0-10",  0,  LootPossibility.Normal,  10), new Loot("Blue Piece of Cloth",  0,  LootPossibility.Normal,  0), new Loot("White Piece of Cloth",  0,  LootPossibility.Normal,  10), new Loot("Green Piece of Cloth",  0,  LootPossibility.Normal,  8), new Loot("Brown Piece of Cloth",  0,  LootPossibility.Normal,  9), new Loot("Yellow Piece of Cloth",  0,  LootPossibility.Normal,  9), new Loot("Talons",  0,  LootPossibility.Normal,  27), new Loot("Soul Orb",  0,  LootPossibility.Normal,  10), new Loot("Great Health Potion",  0,  LootPossibility.Normal,  2), new Loot("Ultimate Health Potion",  0,  LootPossibility.Normal,  0), new Loot("Amulet of Loss",  3057,  LootPossibility.Normal,  0), new Loot("Boots of Haste",  0,  LootPossibility.Normal,  0), new Loot("Gold Ingot",  0,  LootPossibility.Normal,  0), new Loot("Great Spirit Potion",  0,  LootPossibility.Normal,  0), new Loot("Great Mana Potion",  0,  LootPossibility.Normal,  0), new Loot("Jewel Case",  0,  LootPossibility.Normal,  0), new Loot("Rusty Armor",  0,  LootPossibility.Rare,  0), new Loot("Rusty Legs",  0,  LootPossibility.Rare,  0), new Loot("Steel Boots",  0,  LootPossibility.Normal,  0), new Loot("Stone Skin Amulet",  3081,  LootPossibility.Normal,  0), new Loot("Demon Horn",  0,  LootPossibility.Normal,  0), new Loot("Golden Boots",  0,  LootPossibility.VeryRare,  0)}
      );
  }
}

