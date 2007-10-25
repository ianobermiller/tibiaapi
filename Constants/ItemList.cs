using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Constants
{
    /// <summary>
    /// Contains lists of items.
    /// </summary>
    public static class ItemList
    {
        public class Ammunition : List<Objects.Ammunition>
        { 
            public Ammunition()
            {
                this.Add(new Objects.Ammunition(Items.Ammunition.Spear, "Spear", true));
                this.Add(new Objects.Ammunition(Items.Ammunition.SmallStone, "Small Stone", true));
                this.Add(new Objects.Ammunition(Items.Ammunition.ThrowingStar, "Throwing Star", true));
                this.Add(new Objects.Ammunition(Items.Ammunition.Bolts, "Bolts", false));
                this.Add(new Objects.Ammunition(Items.Ammunition.Arrow, "Arrow", false));
                this.Add(new Objects.Ammunition(Items.Ammunition.BurstArrow, "Burst Arrow", false));
                this.Add(new Objects.Ammunition(Items.Ammunition.PoisonedArrow, "Poisoned Arrow", false));
                this.Add(new Objects.Ammunition(Items.Ammunition.PowerBolt, "Power Bolt", false));
            }
        }

        public class Container : List<Objects.Item>
        {
            public Container()
            {
                this.Add(new Objects.Item(Items.Container.BackpackBlack, "Black Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackBlue, "Blue Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackBrown, "Brown Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackGold, "Gold Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackGrass, "Grass Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackGreen, "Green Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackPirate, "Pirate Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackPurple, "Purple Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackRed, "Red Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackStar, "Star Backpack"));
                this.Add(new Objects.Item(Items.Container.BackpackYellow, "Yellow Backpack"));
                this.Add(new Objects.Item(Items.Container.BagBrown, "Brown Bag"));
                this.Add(new Objects.Item(Items.Container.ParcelNew, "New Parcel"));
                this.Add(new Objects.Item(Items.Container.ParcelUsed, "Used Parcel"));
            }
        }

        public class Food : List<Objects.Food>
        {
            public Food()
            {
                this.Add(new Objects.Food(Items.Food.Apple, "Apple", 0));
                this.Add(new Objects.Food(Items.Food.Banana, "Banana", 0));
                this.Add(new Objects.Food(Items.Food.Blueberry, "Blueberry", 0));
                this.Add(new Objects.Food(Items.Food.Bread, "Bread", 0));
                this.Add(new Objects.Food(Items.Food.BrownBread, "Brown Bread", 0));
                this.Add(new Objects.Food(Items.Food.BrownMushroom, "Brown Mushroom", 0));
                this.Add(new Objects.Food(Items.Food.Carrot, "Carrot", 0));
                this.Add(new Objects.Food(Items.Food.Cheese, "Cheese", 0));
                this.Add(new Objects.Food(Items.Food.Cherry, "Cherry", 0));
                this.Add(new Objects.Food(Items.Food.Coconut, "Coconut", 0));
                this.Add(new Objects.Food(Items.Food.Cookie, "Cookie", 0));
                this.Add(new Objects.Food(Items.Food.Corncob, "Corncob", 0));
                this.Add(new Objects.Food(Items.Food.DragonHam, "Dragon Ham", 0));
                this.Add(new Objects.Food(Items.Food.Egg, "Egg", 0));
                this.Add(new Objects.Food(Items.Food.Fish, "Fish", 0));
                this.Add(new Objects.Food(Items.Food.Grapes, "Grapes", 0));
                this.Add(new Objects.Food(Items.Food.GreenMushroom, "Green Mushroom", 0));
                this.Add(new Objects.Food(Items.Food.Ham, "Ham", 0));
                this.Add(new Objects.Food(Items.Food.Meat, "Meat", 0));
                this.Add(new Objects.Food(Items.Food.Mellon, "Mellon", 0));
                this.Add(new Objects.Food(Items.Food.Orange, "Orange", 0));
                this.Add(new Objects.Food(Items.Food.Roll, "Roll", 0));
                this.Add(new Objects.Food(Items.Food.Salmon, "Salmon", 0));
                this.Add(new Objects.Food(Items.Food.WhiteMushroom, "White Mushroom", 0));
            }
        }

        public class Quest : List<Objects.Item>
        {
            public Quest()
            {
                this.Add(new Objects.Item(Items.Quest.ApeFur, "Ape Fur"));
                this.Add(new Objects.Item(Items.Quest.BatWing, "Bat Wing"));
                this.Add(new Objects.Item(Items.Quest.BearPaw, "Bear Paw"));
                this.Add(new Objects.Item(Items.Quest.BeholderEye, "Beholder Eye"));
                this.Add(new Objects.Item(Items.Quest.BluePiece, "Blue Piece"));
                this.Add(new Objects.Item(Items.Quest.BrownPiece, "Brown Piece"));
                this.Add(new Objects.Item(Items.Quest.ChickenFeather, "Chicken Feather"));
                this.Add(new Objects.Item(Items.Quest.DragonClaw, "Dragon Claw"));
                this.Add(new Objects.Item(Items.Quest.FishFin, "Fish Fin"));
                this.Add(new Objects.Item(Items.Quest.GreenDragonLeather, "Green Dragon Leather"));
                this.Add(new Objects.Item(Items.Quest.GreenDragonScale, "Green Dragon Scale"));
                this.Add(new Objects.Item(Items.Quest.GreenPiece, "Green Piece"));
                this.Add(new Objects.Item(Items.Quest.HeavenBlossom, "Heaven Blossom"));
                this.Add(new Objects.Item(Items.Quest.HoneyComb, "Honey Comb"));
                this.Add(new Objects.Item(Items.Quest.LizardLeather, "Lizard Leather"));
                this.Add(new Objects.Item(Items.Quest.LizardScale, "Lizard Scale"));
                this.Add(new Objects.Item(Items.Quest.MinotaurLeather, "Minotaur Leather"));
                this.Add(new Objects.Item(Items.Quest.RedDragonLeather, "Red Dragon Leather"));
                this.Add(new Objects.Item(Items.Quest.RedDragonScale, "Red Dragon Scale"));
                this.Add(new Objects.Item(Items.Quest.RedPiece, "Red Piece"));
                this.Add(new Objects.Item(Items.Quest.SniperGloves, "Sniper Gloves"));
                this.Add(new Objects.Item(Items.Quest.VampireDust, "Vampire Dust"));
                this.Add(new Objects.Item(Items.Quest.WhitePiece, "White Piece"));
                this.Add(new Objects.Item(Items.Quest.WolfPaw, "Wolf Paw"));
                this.Add(new Objects.Item(Items.Quest.YellowPiece, "Yellow Piece"));
            }
        }

        public class Rune : List<Objects.Rune>
        {
            public Rune()
            {
                this.Add(new Objects.Rune(Items.Rune.AnimateDead, "Animate Dead", "adana mort", 600, 0, Constants.SpellCategory.Summon));
                this.Add(new Objects.Rune(Items.Rune.Antidote, "Antidote", "adana pox", 200, 1, Constants.SpellCategory.Healing));
                this.Add(new Objects.Rune(Items.Rune.Chameleon, "Chameleon", "adevo ina", 600, 0, Constants.SpellCategory.Support));
                this.Add(new Objects.Rune(Items.Rune.ConvinceCreature, "Convince Creature", "adeta sio", 200, 0, Constants.SpellCategory.Summon));
                this.Add(new Objects.Rune(Items.Rune.Desintegrate, "Desintegrate", "adito tera", 200, 0, Constants.SpellCategory.Support));
                this.Add(new Objects.Rune(Items.Rune.DestroyField, "Destroy Field", "adito grav", 120, 2, Constants.SpellCategory.Support));
                this.Add(new Objects.Rune(Items.Rune.EnergyField, "Energy Field", "adevo grav vis", 320, 2, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.EnergyWall, "Energy Wall", "adevo mas grav vis", 1000, 5, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.EnergyBomb, "Energy Bomb", "adevo mas vis", 880, 5, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.Envenom, "Envenom", "adevo res pox", 400, 0, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.Explosion, "Explosion", "adevo mas hur", 720, 4, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.FireField, "Fire Field", "adevo grav flam", 240, 1, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.FireWall, "Fire Wall", "adevo mas grav flam", 780, 4, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.Fireball, "Fireball", "adori flam", 160, 2, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.FireBomb, "Fire Bomb", "adevo mas flam", 600, 4, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.GreatFireball, "Great Fireball", "adori gran flam", 480, 3, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.HeavyMagicMissile, "Heavy Magic Missile", "adori gran", 280, 2, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.IntenseHealing, "Intense Healing Rune", "adura gran", 240, 2, Constants.SpellCategory.Healing));
                this.Add(new Objects.Rune(Items.Rune.LightMagicMissile, "Light Magic Missile", "adori", 120, 1, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.MagicWall, "Magic Wall", "adevo grav tera", 750, 5, Constants.SpellCategory.Support));
                this.Add(new Objects.Rune(Items.Rune.Paralyze, "Paralyze", "adana ani", 1400, 0, Constants.SpellCategory.Support));
                this.Add(new Objects.Rune(Items.Rune.PoisonBomb, "Poison Bomb", "adevo mas pox", 520, 2, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.PoisonField, "Poison Field", "adevo grav pox", 200, 0, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.PoisonWall, "Poison Wall", "adevo mas grav pox", 640, 0, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.Soulfire, "Soulfire", "adevo res flam", 600, 0, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.SuddenDeath, "Sudden Death", "adori vita vis", 880, 5, Constants.SpellCategory.Attack));
                this.Add(new Objects.Rune(Items.Rune.UltimateHealing, "Ultimate Healing Rune", "adura vita", 400, 3, Constants.SpellCategory.Healing));
            }
        }

        public class Tool : List<Objects.Item>
        {
            public Tool()
            {
                this.Add(new Objects.Item(Items.Tool.Rope, "Rope"));
                this.Add(new Objects.Item(Items.Tool.FishingRod, "Fishing Rod"));
                this.Add(new Objects.Item(Items.Tool.Pick, "Pick"));
                this.Add(new Objects.Item(Items.Tool.Shovel, "Shovel"));
            }
        }

        public class Valuable : List<Objects.Item>
        {
            public Valuable()
            {
                this.Add(new Objects.Item(Items.Valuable.GoldCoin, "Gold Coin"));
                this.Add(new Objects.Item(Items.Valuable.PlatinumCoin, "Platinum Coin"));
                this.Add(new Objects.Item(Items.Valuable.CrystalCoin, "Crystal Coin"));
                this.Add(new Objects.Item(Items.Valuable.ScarabCoin, "Scarab Coin"));
                this.Add(new Objects.Item(Items.Valuable.SmallAmethyst, "Small Amethyst"));
                this.Add(new Objects.Item(Items.Valuable.SmallEmerald, "Small Emerald"));
                this.Add(new Objects.Item(Items.Valuable.BlackPearl, "Black Pearl"));
                this.Add(new Objects.Item(Items.Valuable.WhitePearl, "White Pearl"));
            }
        }

        public class Bottle : List<Objects.Item>
        {
            public Bottle()
            {
                this.Add(new Objects.Item(Items.Bottle.Vial, "Vial"));
            }
        }
    }
}
