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

        /// <summary>
        /// Get a list of all the creatures on the battlelist.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creature> GetCreatures()
        {
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.Step_Creatures)
            {
                if (client.ReadByte(i + Addresses.Creature.Distance_IsVisible) == 1)
                    yield return new Creature(client, i);
            }
        }

        /// <summary>
        /// Show invisible creatures permanently.
        /// </summary>
        public void ShowInvisible()
        {
            client.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Edited);
            client.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Edited);
        }

        /// <summary>
        /// Hide invisible creatures permanently.
        /// </summary>
        public void HideInvisible()
        {
            client.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Default);
            client.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Default);
        }
    }
    
}
