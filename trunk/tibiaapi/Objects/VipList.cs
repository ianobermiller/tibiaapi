using System;
using System.Collections.Generic;
using Tibia.Packets;

namespace Tibia.Objects
{
    public class VipList : IEnumerable<Vip>
    {
        private Client client;

        public VipList(Client c)
        {
            client = c;
        }

        public List<Vip> GetPlayers()
        {
            List<Vip> players = new List<Vip>();
            for (uint i = Addresses.Vip.Start; i < Addresses.Vip.End; i += Addresses.Vip.StepPlayers)
            {
                players.Add(new Vip(client,i));
            }
            return players;
        }

        /// <summary>
        /// Adds Player to VIP
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddPlayer(string name)
        {
            return Packets.Outgoing.VipAddPacket.Send(client, name);
        }

        /// <summary>
        /// Removes Player from VIP
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemovePlayer(Vip vip)
        {
            return Packets.Outgoing.VipRemovePacket.Send(client, (uint)vip.Id);
        }

        #region IEnumerable<Vip> Members

        IEnumerator<Vip> IEnumerable<Vip>.GetEnumerator()
        {
            return GetPlayers().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetPlayers().GetEnumerator();
        }

        #endregion
    }
}
