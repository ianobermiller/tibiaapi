using System;
using Tibia.Constants;

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
            Type = IncomingPacketType.PlayerSkillsUpdate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerSkillsUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerSkillsUpdate;

            try
            {
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
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
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
        }
    }
}