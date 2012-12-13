using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerSkillsPacket : IncomingPacket
    {

        public byte Fist { get; set; }
        public byte FistPercent { get; set; }
        public byte FistBase { get; set; }
        public byte Club { get; set; }
        public byte ClubPercent { get; set; }
        public byte ClubBase { get; set; }
        public byte Sword { get; set; }
        public byte SwordPercent { get; set; }
        public byte SwordBase { get; set; }
        public byte Axe { get; set; }
        public byte AxePercent { get; set; }
        public byte AxeBase { get; set; }
        public byte Distance { get; set; }
        public byte DistancePercent { get; set; }
        public byte DistanceBase { get; set; }
        public byte Shield { get; set; }
        public byte ShieldPercent { get; set; }
        public byte ShieldBase { get; set; }
        public byte Fish { get; set; }
        public byte FishPercent { get; set; }
        public byte FishBase { get; set; }

        public PlayerSkillsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerSkills;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerSkills)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerSkills;

            Fist = msg.GetByte();
            if (Client.VersionNumber >= 954)
                FistBase = msg.GetByte();
            FistPercent = msg.GetByte();

            Club = msg.GetByte();
            if (Client.VersionNumber >= 954)
                ClubBase = msg.GetByte();
            ClubPercent = msg.GetByte();

            Sword = msg.GetByte();
            if (Client.VersionNumber >= 954)
                SwordBase = msg.GetByte();
            SwordPercent = msg.GetByte();

            Axe = msg.GetByte();
            if (Client.VersionNumber >= 954)
                AxeBase = msg.GetByte();
            AxePercent = msg.GetByte();

            Distance = msg.GetByte();
            if (Client.VersionNumber >= 954)
                DistanceBase = msg.GetByte();
            DistancePercent = msg.GetByte();

            Shield = msg.GetByte();
            if (Client.VersionNumber >= 954)
                ShieldBase = msg.GetByte();
            ShieldPercent = msg.GetByte();

            Fish = msg.GetByte();
            if (Client.VersionNumber >= 954)
                FishBase = msg.GetByte();
            FishPercent = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Fist);
            if (Client.VersionNumber >= 954)
                msg.AddByte(FistBase);
            msg.AddByte(FistPercent);

            msg.AddByte(Club);
            if (Client.VersionNumber >= 954)
                msg.AddByte(ClubBase);
            msg.AddByte(ClubPercent);

            msg.AddByte(Sword);
            if (Client.VersionNumber >= 954)
                msg.AddByte(SwordBase);
            msg.AddByte(SwordPercent);

            msg.AddByte(Axe);
            if (Client.VersionNumber >= 954)
                msg.AddByte(AxeBase);
            msg.AddByte(AxePercent);

            msg.AddByte(Distance);
            if (Client.VersionNumber >= 954)
                msg.AddByte(DistanceBase);
            msg.AddByte(DistancePercent);

            msg.AddByte(Shield);
            if (Client.VersionNumber >= 954)
                msg.AddByte(ShieldBase);
            msg.AddByte(ShieldPercent);

            msg.AddByte(Fish);
            if (Client.VersionNumber >= 954)
                msg.AddByte(FishBase);
            msg.AddByte(FishPercent);

        }
    }
}