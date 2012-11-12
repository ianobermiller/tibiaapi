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
            FistPercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                FistBase = msg.GetByte();

            Club = msg.GetByte();
            ClubPercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                ClubBase = msg.GetByte();

            Sword = msg.GetByte();
            SwordPercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                SwordBase = msg.GetByte();

            Axe = msg.GetByte();
            AxePercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                AxeBase = msg.GetByte();

            Distance = msg.GetByte();
            DistancePercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                DistanceBase = msg.GetByte();

            Shield = msg.GetByte();
            ShieldPercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                ShieldBase = msg.GetByte();

            Fish = msg.GetByte();
            FishPercent = msg.GetByte();
            if (Client.VersionNumber >= 9540)
                FishBase = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Fist);
            msg.AddByte(FistPercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(FistBase);

            msg.AddByte(Club);
            msg.AddByte(ClubPercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(ClubBase);

            msg.AddByte(Sword);
            msg.AddByte(SwordPercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(SwordBase);

            msg.AddByte(Axe);
            msg.AddByte(AxePercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(AxeBase);

            msg.AddByte(Distance);
            msg.AddByte(DistancePercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(DistanceBase);

            msg.AddByte(Shield);
            msg.AddByte(ShieldPercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(ShieldBase);

            msg.AddByte(Fish);
            msg.AddByte(FishPercent);
            if (Client.VersionNumber >= 9540)
                msg.AddByte(FishBase);

        }
    }
}