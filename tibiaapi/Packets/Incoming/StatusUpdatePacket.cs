using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class StatusUpdatePacket:Packet
    {
        private int hp, maxHp, cap, exp, level, mp, maxMp, stamina;
        private byte xpbar, magicLevel, soul;

        #region Properties
        public int Health
        {
            get { return hp; }
        }


        public int MaxHealth
        {
            get { return maxHp; }
        }

        public int Mana
        {
            get { return mp; }
        }

        public int MaxMana
        {
            get { return maxMp; }
        }

        public int Cap
        {
            get { return cap; }
        }

        public int Exp
        {
            get { return exp; }
        }

        public int Stamina
        {
            get { return stamina; }
        }

        public byte XPBar
        {
            get { return xpbar; }
        }

        public byte MagicLevel
        {
            get { return magicLevel; }
        }

        public byte SoulPoints
        {
            get { return soul; }
        }
        #endregion

        public StatusUpdatePacket(Client c) : base(c)
        {
            type = PacketType.StatusUpdate;
            destination = PacketDestination.Client;
        }

        public StatusUpdatePacket(Client c, byte[] data,bool longcap)
            :this(c)
        {
            ParseData(data,longcap);
        }

        public new bool ParseData(byte[] packet,bool longcap)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.StatusUpdate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                hp = p.GetInt();
                maxHp = p.GetInt();
                if (longcap)                
                    cap = p.GetLong();
                else
                    cap = p.GetInt();
                exp = p.GetLong();
                level = p.GetInt();
                xpbar = p.GetByte();
                mp = p.GetInt();
                maxMp = p.GetInt();
                magicLevel = p.GetByte();
                p.GetByte(); // ?
                soul = p.GetByte();
                stamina = p.GetInt();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a status update packet.
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="maxHp"></param>
        /// <param name="cap"></param>
        /// <param name="exp"></param>
        /// <param name="level"></param>
        /// <param name="xpbar">percentage, 0 - 100</param>
        /// <param name="mana"></param>
        /// <param name="maxMana"></param>
        /// <param name="magicLvl"></param>
        /// <param name="soul"></param>
        /// <param name="stamina">in seconds</param>
        /// <returns></returns>
        public static StatusUpdatePacket Create(Client c, int hp, int maxHp, int cap, bool longcap, int exp, int level, byte xpbar, int mana, int maxMana, byte magicLvl, byte soul, int stamina)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.StatusUpdate);
            p.AddInt(hp);
            p.AddInt(maxHp);
            if (longcap)
                p.AddLong(cap);
            else
                p.AddInt(cap);
            p.AddLong(exp);
            p.AddInt(level);
            p.AddByte(xpbar);
            p.AddInt(mana);
            p.AddInt(maxMana);
            p.AddByte(magicLvl);
            p.AddByte(0x00); // ?
            p.AddByte(soul);
            p.AddInt(stamina);
            return new StatusUpdatePacket(c, p.GetPacket(),longcap);
        }
    }
}
