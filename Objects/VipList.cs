using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    public class VipList
    {
        private Client client;

        public VipList(Client c)
        {
            client = c;
        }

        public List<Vip> getPlayers()
        {
            List<Vip> players = new List<Vip>();
            for (uint i = Addresses.VipList.Start; i < Addresses.VipList.End; i += Addresses.VipList.Step_Players)
            {
                players.Add(new Vip(client,i));
            }
            return players;
        }
        /// <summary>
        /// Get with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vip getPlayer(int id)
        {
            return getPlayers().Find(delegate(Vip v)
                {
                    return v.Id == id;
                });
        }
        /// <summary>
        /// Get with specific name
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public Vip getPlayer(string playerName)
        {
            return getPlayers().Find(delegate(Vip v)
            {
                return v.Name.Equals(playerName, StringComparison.CurrentCultureIgnoreCase);
            });
        }
        /// <summary>
        /// Gets a list of online players in viplist
        /// </summary>
        /// <returns></returns>
        public List<Vip> getOnline()
        {
            List<Vip> players = new List<Vip>();
            for (uint i = Addresses.VipList.Start; i < Addresses.VipList.End; i += Addresses.VipList.Step_Players)
            {
                Vip vip = new Vip(client, i);
                if (vip.Status == Constants.VipStatus.Online)
                {
                    players.Add(vip);
                }
            }
            return players;
        }

        /// <summary>
        /// Gets list of player with specific icon
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public List<Vip> getPlayers(Constants.VipIcon icon)
        {
            List<Vip> players = new List<Vip>();
            for (uint i = Addresses.VipList.Start; i < Addresses.VipList.End; i += Addresses.VipList.Step_Players)
            {
                Vip vip = new Vip(client, i);
                if (vip.Icon == icon)
                {
                    players.Add(vip);
                }
            }
            return players;
        }
    }
}
