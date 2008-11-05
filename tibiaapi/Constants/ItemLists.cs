using System;
using System.Collections.Generic;
using System.Text;
using Tibia.Objects;

namespace Tibia.Constants
{
    /// <summary>
    /// Contains lists of items.
    /// </summary>
    public static class ItemLists
    {
        #region All Items
        public static Dictionary<uint, Item> AllItems = new Dictionary<uint, Item>
        {
			{ Items.Ammunition.Arrow.Id, Items.Ammunition.Arrow },
			{ Items.Ammunition.Bolts.Id, Items.Ammunition.Bolts },
			{ Items.Ammunition.BurstArrow.Id, Items.Ammunition.BurstArrow },
			{ Items.Ammunition.EarthArrow.Id, Items.Ammunition.EarthArrow },
			{ Items.Ammunition.EnchantedSpear.Id, Items.Ammunition.EnchantedSpear },
			{ Items.Ammunition.FlamingArrow.Id, Items.Ammunition.FlamingArrow },
			{ Items.Ammunition.FlashArrow.Id, Items.Ammunition.FlashArrow },
			{ Items.Ammunition.HuntingSpear.Id, Items.Ammunition.HuntingSpear },
			{ Items.Ammunition.PiercingBolt.Id, Items.Ammunition.PiercingBolt },
			{ Items.Ammunition.PoisonedArrow.Id, Items.Ammunition.PoisonedArrow },
			{ Items.Ammunition.PowerBolt.Id, Items.Ammunition.PowerBolt },
			{ Items.Ammunition.RoyalSpear.Id, Items.Ammunition.RoyalSpear },
			{ Items.Ammunition.ShiverArrow.Id, Items.Ammunition.ShiverArrow },
			{ Items.Ammunition.SmallStone.Id, Items.Ammunition.SmallStone },
			{ Items.Ammunition.SniperArrow.Id, Items.Ammunition.SniperArrow },
			{ Items.Ammunition.Spear.Id, Items.Ammunition.Spear },
			{ Items.Ammunition.ThrowingKnife.Id, Items.Ammunition.ThrowingKnife },
			{ Items.Ammunition.ThrowingStar.Id, Items.Ammunition.ThrowingStar },

			{ Items.Neck.AmuletOfLoss.Id, Items.Neck.AmuletOfLoss },
			{ Items.Neck.BronzeAmulet.Id, Items.Neck.BronzeAmulet },
			{ Items.Neck.BronzeNecklace.Id, Items.Neck.BronzeNecklace },
			{ Items.Neck.CrystalNecklace.Id, Items.Neck.CrystalNecklace },
			{ Items.Neck.DragonNecklace.Id, Items.Neck.DragonNecklace },
			{ Items.Neck.ElvenAmulet.Id, Items.Neck.ElvenAmulet },
			{ Items.Neck.GarlicNecklace.Id, Items.Neck.GarlicNecklace },
			{ Items.Neck.GoldenAmulet.Id, Items.Neck.GoldenAmulet },
			{ Items.Neck.PlatinumAmulet.Id, Items.Neck.PlatinumAmulet },
			{ Items.Neck.ProtectionAmulet.Id, Items.Neck.ProtectionAmulet },
			{ Items.Neck.Scarf.Id, Items.Neck.Scarf },
			{ Items.Neck.SilverAmulet.Id, Items.Neck.SilverAmulet },
			{ Items.Neck.StarAmulet.Id, Items.Neck.StarAmulet },
			{ Items.Neck.StoneSkinAmulet.Id, Items.Neck.StoneSkinAmulet },
			{ Items.Neck.StrangeTalisman.Id, Items.Neck.StrangeTalisman },
			{ Items.Neck.WolfToothChain.Id, Items.Neck.WolfToothChain },

			{ Items.Container.BackpackBlack.Id, Items.Container.BackpackBlack },
			{ Items.Container.BackpackBlue.Id, Items.Container.BackpackBlue },
			{ Items.Container.BackpackBrown.Id, Items.Container.BackpackBrown },
			{ Items.Container.BackpackGold.Id, Items.Container.BackpackGold },
			{ Items.Container.BackpackGrass.Id, Items.Container.BackpackGrass },
			{ Items.Container.BackpackGreen.Id, Items.Container.BackpackGreen },
			{ Items.Container.BackpackPirate.Id, Items.Container.BackpackPirate },
			{ Items.Container.BackpackPurple.Id, Items.Container.BackpackPurple },
			{ Items.Container.BackpackRed.Id, Items.Container.BackpackRed },
			{ Items.Container.BackpackStar.Id, Items.Container.BackpackStar },
			{ Items.Container.BackpackYellow.Id, Items.Container.BackpackYellow },
			{ Items.Container.BagBrown.Id, Items.Container.BagBrown },
			{ Items.Container.ParcelNew.Id, Items.Container.ParcelNew },
			{ Items.Container.ParcelUsed.Id, Items.Container.ParcelUsed },
			
			{ Items.Food.Apple.Id, Items.Food.Apple }, 
			{ Items.Food.Banana.Id, Items.Food.Banana }, 
			{ Items.Food.Blueberry.Id, Items.Food.Blueberry }, 
			{ Items.Food.Bread.Id, Items.Food.Bread }, 
			{ Items.Food.BrownBread.Id, Items.Food.BrownBread }, 
			{ Items.Food.BrownMushroom.Id, Items.Food.BrownMushroom }, 
			{ Items.Food.Carrot.Id, Items.Food.Carrot }, 
			{ Items.Food.Cheese.Id, Items.Food.Cheese }, 
			{ Items.Food.Cherry.Id, Items.Food.Cherry }, 
			{ Items.Food.Coconut.Id, Items.Food.Coconut }, 
			{ Items.Food.Cookie.Id, Items.Food.Cookie }, 
			{ Items.Food.Corncob.Id, Items.Food.Corncob }, 
			{ Items.Food.DragonHam.Id, Items.Food.DragonHam }, 
			{ Items.Food.Egg.Id, Items.Food.Egg }, 
			{ Items.Food.Fish.Id, Items.Food.Fish }, 
			{ Items.Food.Grapes.Id, Items.Food.Grapes }, 
			{ Items.Food.GreenMushroom.Id, Items.Food.GreenMushroom }, 
			{ Items.Food.Ham.Id, Items.Food.Ham }, 
			{ Items.Food.Meat.Id, Items.Food.Meat }, 
			{ Items.Food.Mellon.Id, Items.Food.Mellon }, 
			{ Items.Food.Orange.Id, Items.Food.Orange }, 
			{ Items.Food.Roll.Id, Items.Food.Roll }, 
			{ Items.Food.Salmon.Id, Items.Food.Salmon }, 
			{ Items.Food.WhiteMushroom.Id, Items.Food.WhiteMushroom },
			
			{ Items.Quest.ApeFur.Id, Items.Quest.ApeFur }, 
			{ Items.Quest.BatWing.Id, Items.Quest.BatWing }, 
			{ Items.Quest.BearPaw.Id, Items.Quest.BearPaw }, 
			{ Items.Quest.BeholderEye.Id, Items.Quest.BeholderEye }, 
			{ Items.Quest.BluePiece.Id, Items.Quest.BluePiece }, 
			{ Items.Quest.BrownPiece.Id, Items.Quest.BrownPiece }, 
			{ Items.Quest.ChickenFeather.Id, Items.Quest.ChickenFeather }, 
			{ Items.Quest.DragonClaw.Id, Items.Quest.DragonClaw }, 
			{ Items.Quest.FishFin.Id, Items.Quest.FishFin }, 
			{ Items.Quest.GreenDragonLeather.Id, Items.Quest.GreenDragonLeather }, 
			{ Items.Quest.GreenDragonScale.Id, Items.Quest.GreenDragonScale }, 
			{ Items.Quest.GreenPiece.Id, Items.Quest.GreenPiece }, 
			{ Items.Quest.HeavenBlossom.Id, Items.Quest.HeavenBlossom }, 
			{ Items.Quest.HoneyComb.Id, Items.Quest.HoneyComb }, 
			{ Items.Quest.LizardLeather.Id, Items.Quest.LizardLeather }, 
			{ Items.Quest.LizardScale.Id, Items.Quest.LizardScale }, 
			{ Items.Quest.MinotaurLeather.Id, Items.Quest.MinotaurLeather }, 
			{ Items.Quest.RedDragonLeather.Id, Items.Quest.RedDragonLeather }, 
			{ Items.Quest.RedDragonScale.Id, Items.Quest.RedDragonScale }, 
			{ Items.Quest.RedPiece.Id, Items.Quest.RedPiece }, 
			{ Items.Quest.SniperGloves.Id, Items.Quest.SniperGloves }, 
			{ Items.Quest.VampireDust.Id, Items.Quest.VampireDust }, 
			{ Items.Quest.WhitePiece.Id, Items.Quest.WhitePiece }, 
			{ Items.Quest.WolfPaw.Id, Items.Quest.WolfPaw }, 
			{ Items.Quest.YellowPiece.Id, Items.Quest.YellowPiece },
			
			{ Items.Tool.Rope.Id, Items.Tool.Rope }, 
			{ Items.Tool.FishingRod.Id, Items.Tool.FishingRod }, 
			{ Items.Tool.Pick.Id, Items.Tool.Pick }, 
			{ Items.Tool.Shovel.Id, Items.Tool.Shovel },
			
			{ Items.Valuable.GoldCoin.Id, Items.Valuable.GoldCoin }, 
			{ Items.Valuable.PlatinumCoin.Id, Items.Valuable.PlatinumCoin }, 
			{ Items.Valuable.CrystalCoin.Id, Items.Valuable.CrystalCoin }, 
			{ Items.Valuable.ScarabCoin.Id, Items.Valuable.ScarabCoin }, 
			{ Items.Valuable.SmallAmethyst.Id, Items.Valuable.SmallAmethyst }, 
			{ Items.Valuable.SmallEmerald.Id, Items.Valuable.SmallEmerald }, 
			{ Items.Valuable.BlackPearl.Id, Items.Valuable.BlackPearl }, 
			{ Items.Valuable.WhitePearl.Id, Items.Valuable.WhitePearl },
			
			{ Items.Bottle.Vial.Id, Items.Bottle.Vial },
			
			{ Items.Potion.Health.Id, Items.Potion.Health }, 
			{ Items.Potion.StrongHealth.Id, Items.Potion.StrongHealth }, 
			{ Items.Potion.GreatHealth.Id, Items.Potion.GreatHealth }, 
			{ Items.Potion.UltimateHealth.Id, Items.Potion.UltimateHealth },
			{ Items.Potion.Mana.Id, Items.Potion.Mana }, 
			{ Items.Potion.StrongMana.Id, Items.Potion.StrongMana }, 
			{ Items.Potion.GreatMana.Id, Items.Potion.GreatMana },
			{ Items.Potion.GreatSpirit.Id, Items.Potion.GreatSpirit },
			
			{ Items.Rune.AnimateDead.Id, Items.Rune.AnimateDead },
			{ Items.Rune.Antidote.Id, Items.Rune.Antidote },
			{ Items.Rune.Avalanche.Id, Items.Rune.Avalanche },
			{ Items.Rune.Chameleon.Id, Items.Rune.Chameleon },
			{ Items.Rune.ConvinceCreature.Id, Items.Rune.ConvinceCreature },
			{ Items.Rune.Desintegrate.Id, Items.Rune.Desintegrate },
			{ Items.Rune.DestroyField.Id, Items.Rune.DestroyField },
			{ Items.Rune.EnergyBomb.Id, Items.Rune.EnergyBomb },
			{ Items.Rune.EnergyField.Id, Items.Rune.EnergyField },
			{ Items.Rune.EnergyWall.Id, Items.Rune.EnergyWall },
			{ Items.Rune.Explosion.Id, Items.Rune.Explosion },
			{ Items.Rune.FireBomb.Id, Items.Rune.FireBomb },
			{ Items.Rune.FireField.Id, Items.Rune.FireField },
			{ Items.Rune.FireWall.Id, Items.Rune.FireWall },
			{ Items.Rune.Fireball.Id, Items.Rune.Fireball },
			{ Items.Rune.GreatFireball.Id, Items.Rune.GreatFireball },
			{ Items.Rune.HeavyMagicMissile.Id, Items.Rune.HeavyMagicMissile },
			{ Items.Rune.Icicle.Id, Items.Rune.Icicle },
			{ Items.Rune.IntenseHealing.Id, Items.Rune.IntenseHealing },
			{ Items.Rune.LightMagicMissile.Id, Items.Rune.LightMagicMissile },
			{ Items.Rune.MagicWall.Id, Items.Rune.MagicWall },
			{ Items.Rune.Paralyze.Id, Items.Rune.Paralyze },
			{ Items.Rune.PoisonBomb.Id, Items.Rune.PoisonBomb },
			{ Items.Rune.PoisonField.Id, Items.Rune.PoisonField },
			{ Items.Rune.PoisonWall.Id, Items.Rune.PoisonWall },
			{ Items.Rune.Soulfire.Id, Items.Rune.Soulfire },
			{ Items.Rune.Stalagmite.Id, Items.Rune.Stalagmite },
			{ Items.Rune.StoneShower.Id, Items.Rune.StoneShower },
			{ Items.Rune.SuddenDeath.Id, Items.Rune.SuddenDeath },
			{ Items.Rune.Thunderstorm.Id, Items.Rune.Thunderstorm },
			{ Items.Rune.UltimateHealing.Id, Items.Rune.UltimateHealing },
        };
        #endregion

        #region Ammunitions
        public static Dictionary<uint, Item> Ammunitions = new Dictionary<uint, Item>
        {
			{ Items.Ammunition.Arrow.Id, Items.Ammunition.Arrow },
			{ Items.Ammunition.Bolts.Id, Items.Ammunition.Bolts },
			{ Items.Ammunition.BurstArrow.Id, Items.Ammunition.BurstArrow },
			{ Items.Ammunition.EarthArrow.Id, Items.Ammunition.EarthArrow },
			{ Items.Ammunition.EnchantedSpear.Id, Items.Ammunition.EnchantedSpear },
			{ Items.Ammunition.FlamingArrow.Id, Items.Ammunition.FlamingArrow },
			{ Items.Ammunition.FlashArrow.Id, Items.Ammunition.FlashArrow },
			{ Items.Ammunition.HuntingSpear.Id, Items.Ammunition.HuntingSpear },
			{ Items.Ammunition.PiercingBolt.Id, Items.Ammunition.PiercingBolt },
			{ Items.Ammunition.PoisonedArrow.Id, Items.Ammunition.PoisonedArrow },
			{ Items.Ammunition.PowerBolt.Id, Items.Ammunition.PowerBolt },
			{ Items.Ammunition.RoyalSpear.Id, Items.Ammunition.RoyalSpear },
			{ Items.Ammunition.ShiverArrow.Id, Items.Ammunition.ShiverArrow },
			{ Items.Ammunition.SmallStone.Id, Items.Ammunition.SmallStone },
			{ Items.Ammunition.SniperArrow.Id, Items.Ammunition.SniperArrow },
			{ Items.Ammunition.Spear.Id, Items.Ammunition.Spear },
			{ Items.Ammunition.ThrowingKnife.Id, Items.Ammunition.ThrowingKnife },
			{ Items.Ammunition.ThrowingStar.Id, Items.Ammunition.ThrowingStar },
        };
        #endregion

        #region Necks
        public static Dictionary<uint, Item> Necks = new Dictionary<uint, Item>
        {
			{ Items.Neck.AmuletOfLoss.Id, Items.Neck.AmuletOfLoss },
			{ Items.Neck.BronzeAmulet.Id, Items.Neck.BronzeAmulet },
			{ Items.Neck.BronzeNecklace.Id, Items.Neck.BronzeNecklace },
			{ Items.Neck.CrystalNecklace.Id, Items.Neck.CrystalNecklace },
			{ Items.Neck.DragonNecklace.Id, Items.Neck.DragonNecklace },
			{ Items.Neck.ElvenAmulet.Id, Items.Neck.ElvenAmulet },
			{ Items.Neck.GarlicNecklace.Id, Items.Neck.GarlicNecklace },
			{ Items.Neck.GoldenAmulet.Id, Items.Neck.GoldenAmulet },
			{ Items.Neck.PlatinumAmulet.Id, Items.Neck.PlatinumAmulet },
			{ Items.Neck.ProtectionAmulet.Id, Items.Neck.ProtectionAmulet },
			{ Items.Neck.Scarf.Id, Items.Neck.Scarf },
			{ Items.Neck.SilverAmulet.Id, Items.Neck.SilverAmulet },
			{ Items.Neck.StarAmulet.Id, Items.Neck.StarAmulet },
			{ Items.Neck.StoneSkinAmulet.Id, Items.Neck.StoneSkinAmulet },
			{ Items.Neck.StrangeTalisman.Id, Items.Neck.StrangeTalisman },
			{ Items.Neck.WolfToothChain.Id, Items.Neck.WolfToothChain },
        };
        #endregion

        #region Containers
        public static Dictionary<uint, Item> Containers = new Dictionary<uint, Item>
        {
			{ Items.Container.BackpackBlack.Id, Items.Container.BackpackBlack },
			{ Items.Container.BackpackBlue.Id, Items.Container.BackpackBlue },
			{ Items.Container.BackpackBrown.Id, Items.Container.BackpackBrown },
			{ Items.Container.BackpackGold.Id, Items.Container.BackpackGold },
			{ Items.Container.BackpackGrass.Id, Items.Container.BackpackGrass },
			{ Items.Container.BackpackGreen.Id, Items.Container.BackpackGreen },
			{ Items.Container.BackpackPirate.Id, Items.Container.BackpackPirate },
			{ Items.Container.BackpackPurple.Id, Items.Container.BackpackPurple },
			{ Items.Container.BackpackRed.Id, Items.Container.BackpackRed },
			{ Items.Container.BackpackStar.Id, Items.Container.BackpackStar },
			{ Items.Container.BackpackYellow.Id, Items.Container.BackpackYellow },
			{ Items.Container.BagBrown.Id, Items.Container.BagBrown },
			{ Items.Container.ParcelNew.Id, Items.Container.ParcelNew },
			{ Items.Container.ParcelUsed.Id, Items.Container.ParcelUsed },
        };
        #endregion

        #region Foods
        public static Dictionary<uint, Objects.Food> Foods = new Dictionary<uint, Objects.Food>
		{
			{ Items.Food.Apple.Id, Items.Food.Apple }, 
			{ Items.Food.Banana.Id, Items.Food.Banana }, 
			{ Items.Food.Blueberry.Id, Items.Food.Blueberry }, 
			{ Items.Food.Bread.Id, Items.Food.Bread }, 
			{ Items.Food.BrownBread.Id, Items.Food.BrownBread }, 
			{ Items.Food.BrownMushroom.Id, Items.Food.BrownMushroom }, 
			{ Items.Food.Carrot.Id, Items.Food.Carrot }, 
			{ Items.Food.Cheese.Id, Items.Food.Cheese }, 
			{ Items.Food.Cherry.Id, Items.Food.Cherry }, 
			{ Items.Food.Coconut.Id, Items.Food.Coconut }, 
			{ Items.Food.Cookie.Id, Items.Food.Cookie }, 
			{ Items.Food.Corncob.Id, Items.Food.Corncob }, 
			{ Items.Food.DragonHam.Id, Items.Food.DragonHam }, 
			{ Items.Food.Egg.Id, Items.Food.Egg }, 
			{ Items.Food.Fish.Id, Items.Food.Fish }, 
			{ Items.Food.Grapes.Id, Items.Food.Grapes }, 
			{ Items.Food.GreenMushroom.Id, Items.Food.GreenMushroom }, 
			{ Items.Food.Ham.Id, Items.Food.Ham }, 
			{ Items.Food.Meat.Id, Items.Food.Meat }, 
			{ Items.Food.Mellon.Id, Items.Food.Mellon }, 
			{ Items.Food.Orange.Id, Items.Food.Orange }, 
			{ Items.Food.Roll.Id, Items.Food.Roll }, 
			{ Items.Food.Salmon.Id, Items.Food.Salmon }, 
			{ Items.Food.WhiteMushroom.Id, Items.Food.WhiteMushroom },
        };
        #endregion

        #region Quest Pieces
        public static Dictionary<uint, Item> QuestPieces = new Dictionary<uint, Item>
		{
			{ Items.Quest.ApeFur.Id, Items.Quest.ApeFur }, 
			{ Items.Quest.BatWing.Id, Items.Quest.BatWing }, 
			{ Items.Quest.BearPaw.Id, Items.Quest.BearPaw }, 
			{ Items.Quest.BeholderEye.Id, Items.Quest.BeholderEye }, 
			{ Items.Quest.BluePiece.Id, Items.Quest.BluePiece }, 
			{ Items.Quest.BrownPiece.Id, Items.Quest.BrownPiece }, 
			{ Items.Quest.ChickenFeather.Id, Items.Quest.ChickenFeather }, 
			{ Items.Quest.DragonClaw.Id, Items.Quest.DragonClaw }, 
			{ Items.Quest.EyePatch.Id, Items.Quest.EyePatch },
			{ Items.Quest.FishFin.Id, Items.Quest.FishFin }, 
			{ Items.Quest.GreenDragonLeather.Id, Items.Quest.GreenDragonLeather }, 
			{ Items.Quest.GreenDragonScale.Id, Items.Quest.GreenDragonScale }, 
			{ Items.Quest.GreenPiece.Id, Items.Quest.GreenPiece }, 
			{ Items.Quest.HeavenBlossom.Id, Items.Quest.HeavenBlossom }, 
			{ Items.Quest.Hook.Id, Items.Quest.Hook },
			{ Items.Quest.HoneyComb.Id, Items.Quest.HoneyComb }, 
			{ Items.Quest.LizardLeather.Id, Items.Quest.LizardLeather }, 
			{ Items.Quest.LizardScale.Id, Items.Quest.LizardScale }, 
			{ Items.Quest.MinotaurLeather.Id, Items.Quest.MinotaurLeather }, 
			{ Items.Quest.PegLeg.Id, Items.Quest.PegLeg },
			{ Items.Quest.RedDragonLeather.Id, Items.Quest.RedDragonLeather }, 
			{ Items.Quest.RedDragonScale.Id, Items.Quest.RedDragonScale }, 
			{ Items.Quest.RedPiece.Id, Items.Quest.RedPiece }, 
			{ Items.Quest.SniperGloves.Id, Items.Quest.SniperGloves }, 
			{ Items.Quest.VampireDust.Id, Items.Quest.VampireDust }, 
			{ Items.Quest.WhitePiece.Id, Items.Quest.WhitePiece }, 
			{ Items.Quest.WolfPaw.Id, Items.Quest.WolfPaw }, 
			{ Items.Quest.YellowPiece.Id, Items.Quest.YellowPiece },
        };
        #endregion

        #region Tools
        public static Dictionary<uint, Item> Tools = new Dictionary<uint, Item>
		{
			{ Items.Tool.Rope.Id, Items.Tool.Rope }, 
			{ Items.Tool.FishingRod.Id, Items.Tool.FishingRod }, 
			{ Items.Tool.Pick.Id, Items.Tool.Pick }, 
			{ Items.Tool.Shovel.Id, Items.Tool.Shovel },
        };
        #endregion

        #region Valuables
        public static Dictionary<uint, Item> Valuables = new Dictionary<uint, Item>
		{
			{ Items.Valuable.GoldCoin.Id, Items.Valuable.GoldCoin }, 
			{ Items.Valuable.PlatinumCoin.Id, Items.Valuable.PlatinumCoin }, 
			{ Items.Valuable.CrystalCoin.Id, Items.Valuable.CrystalCoin }, 
			{ Items.Valuable.ScarabCoin.Id, Items.Valuable.ScarabCoin }, 
			{ Items.Valuable.SmallAmethyst.Id, Items.Valuable.SmallAmethyst }, 
			{ Items.Valuable.SmallEmerald.Id, Items.Valuable.SmallEmerald }, 
			{ Items.Valuable.BlackPearl.Id, Items.Valuable.BlackPearl }, 
			{ Items.Valuable.WhitePearl.Id, Items.Valuable.WhitePearl },
        };
        #endregion

        #region Bottles
        public static Dictionary<uint, Item> Bottles = new Dictionary<uint, Item>
		{			
			{ Items.Bottle.Vial.Id, Items.Bottle.Vial },
        };
        #endregion

        #region Health Potions
        public static Dictionary<uint, Item> HealthPotions = new Dictionary<uint, Item>
		{
			{ Items.Potion.Health.Id, Items.Potion.Health }, 
			{ Items.Potion.StrongHealth.Id, Items.Potion.StrongHealth }, 
			{ Items.Potion.GreatHealth.Id, Items.Potion.GreatHealth }, 
			{ Items.Potion.UltimateHealth.Id, Items.Potion.UltimateHealth },
        };
        #endregion

        #region Mana Potions
        public static Dictionary<uint, Item> ManaPotions = new Dictionary<uint, Item>
		{
			{ Items.Potion.Mana.Id, Items.Potion.Mana }, 
			{ Items.Potion.StrongMana.Id, Items.Potion.StrongMana }, 
			{ Items.Potion.GreatMana.Id, Items.Potion.GreatMana },
        };
        #endregion

        #region Spirit Potions
        public static Dictionary<uint, Item> SpiritPotions = new Dictionary<uint, Item>
		{
			{ Items.Potion.GreatSpirit.Id, Items.Potion.GreatSpirit },
        };
        #endregion

        #region Rings
        public static Dictionary<uint, Item> Rings = new Dictionary<uint, Item>
		{
			{ Items.Ring.AxeRing.Id, Items.Ring.AxeRing },
			{ Items.Ring.ClubRing.Id, Items.Ring.ClubRing },
			{ Items.Ring.CrystalRing.Id, Items.Ring.CrystalRing },
			{ Items.Ring.DwarvenRing.Id, Items.Ring.DwarvenRing },
			{ Items.Ring.EnergyRing.Id, Items.Ring.EnergyRing },
			{ Items.Ring.GoldenRing.Id, Items.Ring.GoldenRing },
			{ Items.Ring.LifeRing.Id, Items.Ring.LifeRing },
			{ Items.Ring.MightRing.Id, Items.Ring.MightRing },
			{ Items.Ring.PowerRing.Id, Items.Ring.PowerRing },
			{ Items.Ring.RingOfHealing.Id, Items.Ring.RingOfHealing },
			{ Items.Ring.RingOfTheSkies.Id, Items.Ring.RingOfTheSkies },
			{ Items.Ring.StealthRing.Id, Items.Ring.StealthRing },
			{ Items.Ring.SwordRing.Id, Items.Ring.SwordRing },
			{ Items.Ring.TimeRing.Id, Items.Ring.TimeRing },
			{ Items.Ring.WeddingRing.Id, Items.Ring.WeddingRing },
        };
        #endregion

        #region Runes
        public static Dictionary<uint, Rune> Runes = new Dictionary<uint, Rune>
        {
			{ Items.Rune.AnimateDead.Id, Items.Rune.AnimateDead },
			{ Items.Rune.Antidote.Id, Items.Rune.Antidote },
			{ Items.Rune.Avalanche.Id, Items.Rune.Avalanche },
			{ Items.Rune.Chameleon.Id, Items.Rune.Chameleon },
			{ Items.Rune.ConvinceCreature.Id, Items.Rune.ConvinceCreature },
			{ Items.Rune.Desintegrate.Id, Items.Rune.Desintegrate },
			{ Items.Rune.DestroyField.Id, Items.Rune.DestroyField },
			{ Items.Rune.EnergyBomb.Id, Items.Rune.EnergyBomb },
			{ Items.Rune.EnergyField.Id, Items.Rune.EnergyField },
			{ Items.Rune.EnergyWall.Id, Items.Rune.EnergyWall },
			{ Items.Rune.Explosion.Id, Items.Rune.Explosion },
			{ Items.Rune.FireBomb.Id, Items.Rune.FireBomb },
			{ Items.Rune.FireField.Id, Items.Rune.FireField },
			{ Items.Rune.FireWall.Id, Items.Rune.FireWall },
			{ Items.Rune.Fireball.Id, Items.Rune.Fireball },
			{ Items.Rune.GreatFireball.Id, Items.Rune.GreatFireball },
			{ Items.Rune.HeavyMagicMissile.Id, Items.Rune.HeavyMagicMissile },
			{ Items.Rune.Icicle.Id, Items.Rune.Icicle },
			{ Items.Rune.IntenseHealing.Id, Items.Rune.IntenseHealing },
			{ Items.Rune.LightMagicMissile.Id, Items.Rune.LightMagicMissile },
			{ Items.Rune.MagicWall.Id, Items.Rune.MagicWall },
			{ Items.Rune.Paralyze.Id, Items.Rune.Paralyze },
			{ Items.Rune.PoisonBomb.Id, Items.Rune.PoisonBomb },
			{ Items.Rune.PoisonField.Id, Items.Rune.PoisonField },
			{ Items.Rune.PoisonWall.Id, Items.Rune.PoisonWall },
			{ Items.Rune.Soulfire.Id, Items.Rune.Soulfire },
			{ Items.Rune.Stalagmite.Id, Items.Rune.Stalagmite },
			{ Items.Rune.StoneShower.Id, Items.Rune.StoneShower },
			{ Items.Rune.SuddenDeath.Id, Items.Rune.SuddenDeath },
			{ Items.Rune.Thunderstorm.Id, Items.Rune.Thunderstorm },
			{ Items.Rune.UltimateHealing.Id, Items.Rune.UltimateHealing },
        };
        #endregion
    }
}
