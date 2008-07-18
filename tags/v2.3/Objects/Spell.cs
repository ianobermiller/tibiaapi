using System;
using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a spell.
    /// </summary>
    public class Spell
    {
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
            this.Add(Spells.AnimateDead);
            this.Add(Spells.Antidote);
            this.Add(Spells.AntidoteRune);
            this.Add(Spells.Berserk);
            this.Add(Spells.CancelInvisibility);
            this.Add(Spells.Challenge);
            this.Add(Spells.Chameleon);
            this.Add(Spells.ConjureArrow);
            this.Add(Spells.ConjureBolt);
            this.Add(Spells.ConjurePiercingBolt);
            this.Add(Spells.ConjureSniperArrow);
            this.Add(Spells.ConvinceCreature);
            this.Add(Spells.CreatureIllusion);
            this.Add(Spells.Desintegrate);
            this.Add(Spells.DestroyField);
            this.Add(Spells.EnchantSpear);
            this.Add(Spells.EnchantStaff);
            this.Add(Spells.EnergyBeam);
            this.Add(Spells.EnergyField);
            this.Add(Spells.EnergyStrike);
            this.Add(Spells.EnergyWall);
            this.Add(Spells.EnergyWave);
            this.Add(Spells.EnergyBomb);
            this.Add(Spells.EternalWinter);
            this.Add(Spells.EtherealSpear);
            this.Add(Spells.Explosion);
            this.Add(Spells.ExplosiveArrow);
            this.Add(Spells.FierceBerserk);
            this.Add(Spells.FindPerson);
            this.Add(Spells.FireField);
            this.Add(Spells.FireWall);
            this.Add(Spells.FireWave);
            this.Add(Spells.Fireball);
            this.Add(Spells.FireBomb);
            this.Add(Spells.FlameStrike);
            this.Add(Spells.Food);
            this.Add(Spells.ForceStrike);
            this.Add(Spells.GreatEnergyBeam);
            this.Add(Spells.GreatFireball);
            this.Add(Spells.GreatLight);
            this.Add(Spells.Groundshaker);
            this.Add(Spells.Haste);
            this.Add(Spells.HealFriend);
            this.Add(Spells.HeavyMagicMissile);
            this.Add(Spells.HellsCore);
            this.Add(Spells.IntenseHealing);
            this.Add(Spells.IntenseHealingRune);
            this.Add(Spells.Invisible);
            this.Add(Spells.Levitate);
            this.Add(Spells.Light);
            this.Add(Spells.LightHealing);
            this.Add(Spells.LightMagicMissile);
            this.Add(Spells.MagicRope);
            this.Add(Spells.MagicShield);
            this.Add(Spells.MagicWall);
            this.Add(Spells.MassHealing);
            this.Add(Spells.Paralyze);
            this.Add(Spells.PoisonBomb);
            this.Add(Spells.PoisonField);
            this.Add(Spells.PoisonWall);
            this.Add(Spells.PoisonedArrow);
            this.Add(Spells.PowerBolt);
            this.Add(Spells.RageoftheSkies);
            this.Add(Spells.Soulfire);
            this.Add(Spells.StrongHaste);
            this.Add(Spells.SuddenDeath);
            this.Add(Spells.SummonCreature);
            this.Add(Spells.UltimateExplosion);
            this.Add(Spells.UltimateHealing);
            this.Add(Spells.UltimateHealingRune);
            this.Add(Spells.UltimateLight);
            this.Add(Spells.UndeadLegion);
            this.Add(Spells.WhirlwindThrow);
            this.Add(Spells.WildGrowth);
            this.Add(Spells.WrathofNature);
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
