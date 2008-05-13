using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class StatusUpdatePacket:Packet
    {
        private int hp, mp, cap, exp, level, stamina;
        private byte xpbar,soul;

        public int Health
        {
            get { return hp; }
        }

        public int Mana
        {
            get { return mp; }
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

        public byte SoulPoints
        {
            get { return soul; }
        }
        
        public StatusUpdatePacket()
        {
            type = PacketType.StatusUpdate;
            destination = PacketDestination.Client;
        }
        public StatusUpdatePacket(byte[] data)
            :this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.StatusUpdate) return false;
                int index = 3;
                hp = BitConverter.ToInt16(packet, index);
                index += 4;
                cap = BitConverter.ToInt16(packet, index);
                index += 2;
                exp = BitConverter.ToInt32(packet, index);
                index += 4;
                level = BitConverter.ToInt16(packet, index);
                index += 2;
                xpbar = packet[index];
                index += 1;
                mp = BitConverter.ToInt16(packet, index);
                index += 6;
                soul = packet[index];
                stamina = BitConverter.ToInt16(packet, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static StatusUpdatePacket Create(int hp, int mana, int level, int exp, int cap, int stamina, byte soul, byte xpbar)
        {
            PacketBuilder pkt = new PacketBuilder();
            pkt.AddInt(23);
            pkt.AddByte((byte)PacketType.StatusUpdate);
            pkt.AddInt(hp);
            pkt.AddInt(hp);
            pkt.AddInt(cap);
            pkt.AddLong(exp);
            pkt.AddInt(level);
            pkt.AddByte(xpbar);
            pkt.AddInt(mana);
            pkt.AddInt(mana);
            pkt.AddByte(0x00);
            pkt.AddByte(0x00);
            pkt.AddByte(soul);
            pkt.AddInt(stamina);
            StatusUpdatePacket sup = new StatusUpdatePacket(pkt.Data);
            return sup;
        }
    }
}
