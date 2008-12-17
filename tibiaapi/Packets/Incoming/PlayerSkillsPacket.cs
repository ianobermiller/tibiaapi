using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class PlayerSkillsPacket : IncomingPacket
    {

        public byte Fist { get; set; }
        public byte FistPercent { get; set; }
        public byte Club { get; set; }
        public byte ClubPercent { get; set; }
        public byte Sword { get; set; }
        public byte SwordPercent { get; set; }
        public byte Axe { get; set; }
        public byte AxePercent { get; set; }
        public byte Distance { get; set; }
        public byte DistancePercent { get; set; }
        public byte Shield { get; set; }
        public byte ShieldPercent { get; set; }
        public byte Fish { get; set; }
        public byte FishPercent { get; set; }

        public PlayerSkillsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.PLAYER_SKILLS;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.PLAYER_SKILLS)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.PLAYER_SKILLS;

            Fist = msg.GetByte();
            FistPercent = msg.GetByte();
            Club = msg.GetByte();
            ClubPercent = msg.GetByte();
            Sword = msg.GetByte();
            SwordPercent = msg.GetByte();
            Axe = msg.GetByte();
            AxePercent = msg.GetByte();
            Distance = msg.GetByte();
            DistancePercent = msg.GetByte();
            Shield = msg.GetByte();
            ShieldPercent = msg.GetByte();
            Fish = msg.GetByte();
            FishPercent = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte(Fist);
            msg.AddByte(FistPercent);
            msg.AddByte(Club);
            msg.AddByte(ClubPercent);
            msg.AddByte(Sword);
            msg.AddByte(SwordPercent);
            msg.AddByte(Axe);
            msg.AddByte(AxePercent);
            msg.AddByte(Distance);
            msg.AddByte(DistancePercent);
            msg.AddByte(Shield);
            msg.AddByte(ShieldPercent);
            msg.AddByte(Fish);
            msg.AddByte(FishPercent);

            return msg.Packet;
        }
    }
}