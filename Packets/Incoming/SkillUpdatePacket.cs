using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class SkillUpdatePacket : Packet
    {
        private byte fist;
        private byte sword;
        private byte axe;
        private byte club;
        private byte distance;
        private byte shield;
        private byte fish;
        private byte fistper;
        private byte swordper;
        private byte axeper;
        private byte clubper;
        private byte distanceper;
        private byte shieldper;
        private byte fishper;
        public byte Fist
        {
            get { return fist; }
        }
        public byte Sword
        {
            get { return sword; }
        }
        public byte Axe
        {
            get { return axe; }
        }
        public byte Club
        {
            get { return club; }
        }
        public byte Distance
        {
            get { return distance; }
        }
        public byte Shield
        {
            get { return shield; }
        }
        public byte Fish
        {
            get { return fish; }
        }
        public byte FistPercent
        {
            get { return fistper; }
        }
        public byte SwordPercent
        {
            get { return swordper; }
        }
        public byte AxePercent
        {
            get { return axeper; }
        }
        public byte ClubPercent
        {
            get { return clubper; }
        }
        public byte DistancePercent
        {
            get { return distanceper; }
        }
        public byte ShieldPercent
        {
            get { return shieldper; }
        }
        public byte FishPercent
        {
            get { return fishper; }
        }
        public SkillUpdatePacket(Client c)
            : base(c)
        {
            type = PacketType.SkillUpdate;
            destination = PacketDestination.Client;
        }
        public SkillUpdatePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.SkillUpdate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                fist = p.GetByte();
                fistper = p.GetByte();
                club = p.GetByte();
                clubper = p.GetByte();
                sword = p.GetByte();
                swordper = p.GetByte();
                axe = p.GetByte();
                axeper = p.GetByte();
                distance = p.GetByte();
                distanceper = p.GetByte();
                shield = p.GetByte();
                shieldper = p.GetByte();
                fish = p.GetByte();
                fishper = p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SkillUpdatePacket Create(Client c, byte fist, byte fistper, byte club, byte clubper, byte sword, byte swordper, byte axe, byte axeper, byte distance, byte distanceper, byte shield, byte shieldper, byte fish, byte fishper)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.SkillUpdate);
            p.AddByte(fist);
            p.AddByte(fistper);
            p.AddByte(club);
            p.AddByte(clubper);
            p.AddByte(sword);
            p.AddByte(swordper);
            p.AddByte(axe);
            p.AddByte(axeper);
            p.AddByte(distance);
            p.AddByte(distanceper);
            p.AddByte(shield);
            p.AddByte(shieldper);
            p.AddByte(fish);
            p.AddByte(fishper);
            return new SkillUpdatePacket(c, p.GetPacket());
        }
    }
}