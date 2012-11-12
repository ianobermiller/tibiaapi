using System;
using System.Collections.Generic;
using Tibia.Packets;

namespace Tibia.Objects
{
    public class CreatureData
    {
        public string Name;
        public int HitPoints;
        public int ExperiencePoints;
        public int SummonMana;
        public int ConvinceMana;
        public int MaxDamage;
        public bool CanIllusion;
        public bool CanSeeInvisible;
        public FrontAttack FrontAttack;
        public List<DamageType> Immunities;
        public List<DamageModifier> Strengths;
        public List<DamageModifier> Weaknesses;
        public List<string> Sounds;
        public List<Loot> Loot;

        public CreatureData(string name, int hitPoints, int experiencePoints, int summonMana, int convinceMana, int maxDamage, bool canIllusion, bool canSeeInvisible, FrontAttack frontAttack, List<DamageType> immunities, List<DamageModifier> strenghts, List<DamageModifier> weakness, List<string> sounds, List<Loot> loot)
        {
            Name = name;
            HitPoints = hitPoints;
            ExperiencePoints = experiencePoints;
            SummonMana = summonMana;
            ConvinceMana = convinceMana;
            MaxDamage = maxDamage;
            CanIllusion = canIllusion;
            CanSeeInvisible = canSeeInvisible;
            Immunities = immunities;
            Strengths = strenghts;
            Weaknesses = weakness;
            Sounds = sounds;
            FrontAttack = frontAttack;
            Loot = loot;
        }

        public DamageType GetWeakness()
        {
            List<DamageType> damages = new List<DamageType>();
            foreach (DamageType d in Enum.GetValues(typeof(DamageType)))
                damages.Add(d);

            return GetWeakness(damages);
        }

        public DamageType GetWeakness(List<DamageType> types)
        {
            if (Weaknesses.Count > 0)
            {
                Weaknesses.Sort();
                foreach (DamageModifier d in Weaknesses)
                    if (types.Contains(d.DamageType))
                        return d.DamageType;
            }

            if (Strengths.Count > 0)
            {
                Strengths.Sort();
                Strengths.Reverse();

                foreach (DamageModifier d in Strengths)
                    if (types.Contains(d.DamageType))
                        return d.DamageType;
            }

            foreach (DamageType d in types)
            {
                if (!Immunities.Contains(d))
                    return d;
            }

            return DamageType.Physical;
        }
    }

    public enum DamageType
    {
        Death,
        Drown,
        Earth,
        Energy,
        Fire,
        Holy,
        Ice,
        LifeDrain,
        ManaDrain,
        Paralysis,
        Physical
    }

    public class DamageModifier : IComparable<DamageModifier>
    {
        public DamageType DamageType;
        public int Percent;

        public DamageModifier(DamageType type, int percent)
        {
            DamageType = type;
            Percent = percent;
        }

        public int CompareTo(DamageModifier other)
        {
            return Percent.CompareTo(other.Percent);
        }
    }

    public enum LootPossibility
    {
        Always,
        Normal,
        Rare,
        SemiRare,
        VeryRare
    }

    public enum FrontAttack
    {
        None,
        Strike,
        Beam,
        Wave,
        Both
    }

    public struct Loot
    {
        string Name;
        uint Id;
        LootPossibility Possibility;
        int MaxAmmount;

        public Loot(string name, uint id, LootPossibility possibility, int maxAmmout)
        {
            Name = name;
            Id = id;
            Possibility = possibility;
            MaxAmmount = maxAmmout;
        }
    }

}
