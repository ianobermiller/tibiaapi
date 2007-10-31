using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a spell.
    /// </summary>
    public class Spell
    {
        /*** All the spells in CIPSoft Tibia ***/
        #region Spell List
        public static Spell AnimateDead = new Spell("Animate Dead", "adana mort", 600, Constants.SpellCategory.Summon, Constants.SpellType.Rune);
        public static Spell Antidote = new Spell("Antidote", "exana pox", 30, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell AntidoteRune = new Spell("Antidote Rune", "adana pox", 200, Constants.SpellCategory.Healing, Constants.SpellType.Rune);
        public static Spell Berserk = new Spell("Berserk", "exori", 120, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell CancelInvisibility = new Spell("Cancel Invisibility", "exana ina", 200, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Challenge = new Spell("Challenge", "exeta res", 30, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Chameleon = new Spell("Chameleon", "adevo ina", 600, Constants.SpellCategory.Support, Constants.SpellType.Rune);
        public static Spell ConjureArrow = new Spell("Conjure Arrow", "exevo con", 100, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell ConjureBolt = new Spell("Conjure Bolt", "exevo con mort", 140, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell ConjurePiercingBolt = new Spell("Conjure Piercing Bolt", "exevo con grav", 180, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell ConjureSniperArrow = new Spell("Conjure Sniper Arrow", "exevo con hur", 160, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell ConvinceCreature = new Spell("Convince Creature", "adeta sio", 200, Constants.SpellCategory.Summon, Constants.SpellType.Rune);
        public static Spell CreatureIllusion = new Spell("Creature Illusion", "utevo res ina \"{creatureName}\"", 100, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Desintegrate = new Spell("Desintegrate", "adito tera", 200, Constants.SpellCategory.Support, Constants.SpellType.Rune);
        public static Spell DestroyField = new Spell("Destroy Field", "adito grav", 120, Constants.SpellCategory.Support, Constants.SpellType.Rune);
        public static Spell EnchantSpear = new Spell("Enchant Spear", "exeta con", 350, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell EnchantStaff = new Spell("Enchant Staff", "exeta vis", 80, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell EnergyBeam = new Spell("Energy Beam", "exevo vis lux", 100, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell EnergyField = new Spell("Energy Field", "adevo grav vis", 320, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell EnergyStrike = new Spell("Energy Strike", "exori vis", 20, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell EnergyWall = new Spell("Energy Wall", "adevo mas grav vis", 1000, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell EnergyWave = new Spell("Energy Wave", "exevo mort hur", 250, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell Energybomb = new Spell("Energybomb", "adevo mas vis", 880, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell Envenom = new Spell("Envenom", "adevo res pox", 400, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell EtherealSpear = new Spell("Ethereal Spear", "exori con", 35, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell Explosion = new Spell("Explosion", "adevo mas hur", 720, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell ExplosiveArrow = new Spell("Explosive Arrow", "exevo con flam", 290, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell FierceBerserk = new Spell("Fierce Berserk", "exori gran", 340, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell FindPerson = new Spell("Find Person", "exiva \"{name}\"", 20, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell FireField = new Spell("Fire Field", "adevo grav flam", 240, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell FireWall = new Spell("Fire Wall", "adevo mas grav flam", 780, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell FireWave = new Spell("Fire Wave", "exevo flam hur", 80, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell Fireball = new Spell("Fireball", "adori flam", 160, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell Firebomb = new Spell("Firebomb", "adevo mas flam", 600, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell FlameStrike = new Spell("Flame Strike", "exori flam", 20, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell Food = new Spell("Food", "exevo pan", 120, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell ForceStrike = new Spell("Force Strike", "exori mort", 20, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell GreatEnergyBeam = new Spell("Great Energy Beam", "exevo gran vis lux", 200, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell GreatFireball = new Spell("Great Fireball", "adori gran flam", 480, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell GreatLight = new Spell("Great Light", "utevo gran lux", 60, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Groundshaker = new Spell("Groundshaker", "exori mas", 160, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell Haste = new Spell("Haste", "utani hur", 60, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell HealFriend = new Spell("Heal Friend", "exura sio \"{name}\"", 70, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell HeavyMagicMissile = new Spell("Heavy Magic Missile", "adori gran", 280, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell IntenseHealing = new Spell("Intense Healing", "exura gran", 40, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell IntenseHealingRune = new Spell("Intense Healing Rune", "adura gran", 240, Constants.SpellCategory.Healing, Constants.SpellType.Rune);
        public static Spell Invisible = new Spell("Invisible", "utana vid", 440, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Levitate = new Spell("Levitate", "exani hur \"{up|down}\"", 50, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell Light = new Spell("Light", "utevo lux", 20, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell LightHealing = new Spell("Light Healing", "exura", 25, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell LightMagicMissile = new Spell("Light Magic Missile", "adori", 120, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell MagicRope = new Spell("Magic Rope", "exani tera", 20, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell MagicShield = new Spell("Magic Shield", "utamo vita", 50, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell MagicWall = new Spell("Magic Wall", "adevo grav tera", 750, Constants.SpellCategory.Support, Constants.SpellType.Rune);
        public static Spell MassHealing = new Spell("Mass Healing", "exura gran mas res", 150, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell Paralyze = new Spell("Paralyze", "adana ani", 1400, Constants.SpellCategory.Support, Constants.SpellType.Rune);
        public static Spell PoisonBomb = new Spell("Poison Bomb", "adevo mas pox", 520, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell PoisonField = new Spell("Poison Field", "adevo grav pox", 200, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell PoisonStorm = new Spell("Poison Storm", "exevo gran mas pox", 600, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell PoisonWall = new Spell("Poison Wall", "adevo mas grav pox", 640, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell PoisonedArrow = new Spell("Poisoned Arrow", "exevo con pox", 130, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell PowerBolt = new Spell("Power Bolt", "exevo con vis", 700, Constants.SpellCategory.Supply, Constants.SpellType.Instant);
        public static Spell Soulfire = new Spell("Soulfire", "adevo res flam", 600, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell StrongHaste = new Spell("Strong Haste", "utani gran hur", 100, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell SuddenDeath = new Spell("Sudden Death", "adori vita vis", 880, Constants.SpellCategory.Attack, Constants.SpellType.Rune);
        public static Spell SummonCreature = new Spell("Summon Creature", "utevo res \"{creatureName}\"", 0, Constants.SpellCategory.Summon, Constants.SpellType.Instant);
        public static Spell UltimateExplosion = new Spell("Ultimate Explosion", "exevo gran mas vis", 1200, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell UltimateHealing = new Spell("Ultimate Healing", "exura vita", 160, Constants.SpellCategory.Healing, Constants.SpellType.Instant);
        public static Spell UltimateHealingRune = new Spell("Ultimate Healing Rune", "adura vita", 400, Constants.SpellCategory.Healing, Constants.SpellType.Rune);
        public static Spell UltimateLight = new Spell("Ultimate Light", "utevo vis lux", 140, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        public static Spell UndeadLegion = new Spell("Undead Legion", "exana mas mort", 500, Constants.SpellCategory.Summon, Constants.SpellType.Instant);
        public static Spell WhirlwindThrow = new Spell("Whirlwind Throw", "exori hur", 40, Constants.SpellCategory.Attack, Constants.SpellType.Instant);
        public static Spell WildGrowth = new Spell("Wild Growth", "exevo grav vita", 220, Constants.SpellCategory.Support, Constants.SpellType.Instant);
        #endregion

        public string Name;
        public string Words;
        public uint ManaPoints;
        public Constants.SpellType Type;
        public Constants.SpellCategory Category;

        /// <summary>
        /// Default spell constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="words"></param>
        /// <param name="mp"></param>
        /// <param name="category"></param>
        /// <param name="type"></param>
        public Spell(string name, string words, uint mp, Constants.SpellCategory category, Constants.SpellType type)
        {
            Name = name;
            Words = words;
            ManaPoints = mp;
            Category = category;
            Type = type;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// A list of spells. Usage: create a new SpellList object and use it to find a certain spell (say, from user input). Or, add this list to a list box.
    /// </summary>
    public class SpellList : List<Spell>
    {
        public SpellList()
        {
            this.Add(Spell.AnimateDead);
            this.Add(Spell.Antidote);
            this.Add(Spell.AntidoteRune);
            this.Add(Spell.Berserk);
            this.Add(Spell.CancelInvisibility);
            this.Add(Spell.Challenge);
            this.Add(Spell.Chameleon);
            this.Add(Spell.ConjureArrow);
            this.Add(Spell.ConjureBolt);
            this.Add(Spell.ConjurePiercingBolt);
            this.Add(Spell.ConjureSniperArrow);
            this.Add(Spell.ConvinceCreature);
            this.Add(Spell.CreatureIllusion);
            this.Add(Spell.Desintegrate);
            this.Add(Spell.DestroyField);
            this.Add(Spell.EnchantSpear);
            this.Add(Spell.EnchantStaff);
            this.Add(Spell.EnergyBeam);
            this.Add(Spell.EnergyField);
            this.Add(Spell.EnergyStrike);
            this.Add(Spell.EnergyWall);
            this.Add(Spell.EnergyWave);
            this.Add(Spell.Energybomb);
            this.Add(Spell.Envenom);
            this.Add(Spell.EtherealSpear);
            this.Add(Spell.Explosion);
            this.Add(Spell.ExplosiveArrow);
            this.Add(Spell.FierceBerserk);
            this.Add(Spell.FindPerson);
            this.Add(Spell.FireField);
            this.Add(Spell.FireWall);
            this.Add(Spell.FireWave);
            this.Add(Spell.Fireball);
            this.Add(Spell.Firebomb);
            this.Add(Spell.FlameStrike);
            this.Add(Spell.Food);
            this.Add(Spell.ForceStrike);
            this.Add(Spell.GreatEnergyBeam);
            this.Add(Spell.GreatFireball);
            this.Add(Spell.GreatLight);
            this.Add(Spell.Groundshaker);
            this.Add(Spell.Haste);
            this.Add(Spell.HealFriend);
            this.Add(Spell.HeavyMagicMissile);
            this.Add(Spell.IntenseHealing);
            this.Add(Spell.IntenseHealingRune);
            this.Add(Spell.Invisible);
            this.Add(Spell.Levitate);
            this.Add(Spell.Light);
            this.Add(Spell.LightHealing);
            this.Add(Spell.LightMagicMissile);
            this.Add(Spell.MagicRope);
            this.Add(Spell.MagicShield);
            this.Add(Spell.MagicWall);
            this.Add(Spell.MassHealing);
            this.Add(Spell.Paralyze);
            this.Add(Spell.PoisonBomb);
            this.Add(Spell.PoisonField);
            this.Add(Spell.PoisonStorm);
            this.Add(Spell.PoisonWall);
            this.Add(Spell.PoisonedArrow);
            this.Add(Spell.PowerBolt);
            this.Add(Spell.Soulfire);
            this.Add(Spell.StrongHaste);
            this.Add(Spell.SuddenDeath);
            this.Add(Spell.SummonCreature);
            this.Add(Spell.UltimateExplosion);
            this.Add(Spell.UltimateHealing);
            this.Add(Spell.UltimateHealingRune);
            this.Add(Spell.UltimateLight);
            this.Add(Spell.UndeadLegion);
            this.Add(Spell.WhirlwindThrow);
            this.Add(Spell.WildGrowth);
        }

        /// <summary>
        /// Find a spell by its partial name or words.
        /// </summary>
        /// <param name="spellNameOrWords"></param>
        /// <returns></returns>
        public Spell FindSpell(string spellNameOrWords)
        {
            return FindSpell(spellNameOrWords, false);
        }
        
        /// <summary>
        /// Find a spell by its name or words.
        /// </summary>
        /// <param name="spellNameOrWords"></param>
        /// <param name="wholeWord">if true, function only returns a spell whose words match exactly those in spellNameOrWords, if false, it returns a partial match as well</param>
        /// <returns></returns>
        public Spell FindSpell(string spellNameOrWords, bool wholeWord)
        {
            return this.Find(delegate(Spell s)
            {
                if (wholeWord)
                    return (s.Name.Equals(spellNameOrWords, System.StringComparison.CurrentCultureIgnoreCase)) ||
                        (s.Words.Equals(spellNameOrWords, System.StringComparison.CurrentCultureIgnoreCase));
                else
                    return (s.Name.IndexOf(spellNameOrWords, StringComparison.CurrentCultureIgnoreCase) != -1) ||
                        (s.Words.IndexOf(spellNameOrWords, StringComparison.CurrentCultureIgnoreCase) != -1);
            });
        }

        /// <summary>
        /// Find all the spells in the given category.
        /// </summary>
        /// <param name="spellCategory"></param>
        /// <returns></returns>
        public List<Spell> FindSpells(Constants.SpellCategory spellCategory)
        {
            return this.FindAll(delegate(Spell s)
            {
                return s.Category == spellCategory;
            });
        }

        /// <summary>
        /// Find all the spells of a given spell type.
        /// </summary>
        /// <param name="spellType"></param>
        /// <returns></returns>
        public List<Spell> FindSpells(Constants.SpellType spellType)
        {
            return this.FindAll(delegate(Spell s)
            {
                return s.Type == spellType;
            });
        }

        /// <summary>
        /// Find all the spells with required mana less than or equivalent to a given mana amount.
        /// </summary>
        /// <param name="requiredMana"></param>
        /// <returns></returns>
        public List<Spell> FindSpells(uint requiredMana)
        {
            return this.FindAll(delegate(Spell s)
            {
                return s.ManaPoints <= requiredMana;
            });
        }
    }
}
