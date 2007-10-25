using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Battle list object.
    /// </summary>
    public class BattleList
    {
        private Client client;

        /// <summary>
        /// Create a battlelist object.
        /// </summary>
        /// <param name="c"></param>
        public BattleList(Client c)
        {
            client = c;
        }

        public List<Creature> getCreatures(Predicate<Creature> match)
        {
            List<Creature> creatures = new List<Creature>();
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.Step_Creatures)
            {
                Creature creature = new Creature(client, i);
                if (match(creature))
                    creatures.Add(creature);
            }
            return creatures;

        }

        /// <summary>
        /// Get a list of all the creatures on the battlelist.
        /// </summary>
        /// <returns></returns>
        public List<Creature> getCreatures()
        {
            List<Creature> creatures = new List<Creature>();
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.Step_Creatures)
            {
                if (client.ReadByte(i + Addresses.Creature.Distance_IsVisible) == 1)
                    creatures.Add(new Creature(client, i));
            }
            return creatures;
        }

        /// <summary>
        /// Get a list of all the cratures with the specified string in the name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <returns></returns>
        public List<Creature> getCreatures(string creatureName)
        {
            return getCreatures(creatureName, false);
        }

        /// <summary>
        /// Get a list of all the creatures with the specified name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <param name="wholeWord"></param>
        /// <returns></returns>
        public List<Creature> getCreatures(string creatureName, bool wholeWord)
        {
            return getCreatures().FindAll(delegate(Creature c)
            {
                if (wholeWord)
                    return c.Name.Equals(creatureName, StringComparison.CurrentCultureIgnoreCase);
                else
                    return c.Name.IndexOf(creatureName, StringComparison.CurrentCultureIgnoreCase) != -1;
            });
        }

        /// <summary>
        /// Get the creature with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Creature getCreature(int id)
        {
            return getCreatures().Find(delegate(Creature c)
            {
                return c.Id == id;
            });
        }

        /// <summary>
        /// Get the first creature with the specified string in the name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <returns></returns>
        public Creature getCreature(string creatureName)
        {
            return getCreature(creatureName, false);
        }

        /// <summary>
        /// Get the first creature with the specified name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <param name="wholeWord"></param>
        /// <returns></returns>
        public Creature getCreature(string creatureName, bool wholeWord)
        {
            return getCreatures().Find(delegate(Creature c)
            {
                if (wholeWord)
                    return c.Name.Equals(creatureName, StringComparison.CurrentCultureIgnoreCase);
                else
                    return c.Name.IndexOf(creatureName, StringComparison.CurrentCultureIgnoreCase) != -1;
            });
        }

        public int showInvisible()
        {
            return showInvisible(Constants.OutfitType.OldMale);
        }

        /// <summary>
        /// Show invisible creatures by replacing their invisible outfit with a specified type.
        /// </summary>
        /// <param name="outfitType"></param>
        /// <returns></returns>
        public int showInvisible(Constants.OutfitType outfitType)
        {
            int replacedCount = 0;

            List<Creature> creatures = getCreatures(delegate(Creature c)
            {
                return c.Outfit == Convert.ToInt32(Constants.OutfitType.Invisible);
            });

            foreach(Creature c in creatures)
            {
                c.Outfit = Convert.ToInt32(outfitType);
                replacedCount++;
            }

            return replacedCount;
        }
    }
}
