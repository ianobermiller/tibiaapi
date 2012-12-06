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
            uint count = client.Memory.ReadUInt32(client.Addresses.Vip.Count);
            List<Vip> players = new List<Vip>((int)count);

            //Read marker node
            uint nextNodeAddress = client.Memory.ReadUInt32(client.Memory.ReadUInt32(client.Addresses.Vip.MarkerNodePtr) + client.Addresses.Vip.DistanceNextNode);


            for (int i = 0; i < count; i++)
            {
                players.Add(new Vip(client, nextNodeAddress));
                nextNodeAddress = client.Memory.ReadUInt32(nextNodeAddress + client.Addresses.Vip.DistanceNextNode);
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
            return Packets.Outgoing.AddVipPacket.Send(client, name);
        }

        /// <summary>
        /// Removes Player from VIP
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemovePlayer(Vip vip)
        {
            return Packets.Outgoing.RemoveVipPacket.Send(client, (uint)vip.Id);
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
