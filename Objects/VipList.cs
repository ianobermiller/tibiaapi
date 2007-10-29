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
            for (uint i = Addresses.Vip.Start; i < Addresses.Vip.End; i += Addresses.Vip.Step_Players)
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
            for (uint i = Addresses.Vip.Start; i < Addresses.Vip.End; i += Addresses.Vip.Step_Players)
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
            for (uint i = Addresses.Vip.Start; i < Addresses.Vip.End; i += Addresses.Vip.Step_Players)
            {
                Vip vip = new Vip(client, i);
                if (vip.Icon == icon)
                {
                    players.Add(vip);
                }
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
            byte[] packet = { };
            int packetLength, payloadLength;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            packetLength = 5 + name.Length;
            payloadLength = packetLength - 2;
            packet = new byte[packetLength];
            packet[00] = Packet.Lo(payloadLength);
            packet[01] = Packet.Hi(payloadLength);
            packet[02] = 0xDC;
            packet[03] = Packet.Lo(name.Length);
            packet[04] = Packet.Hi(name.Length);
            Array.Copy(enc.GetBytes(name), 0, packet, 5, name.Length);
            return client.send(packet);
        }
    }
}
