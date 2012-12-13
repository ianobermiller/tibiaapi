using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerDataPacket : IncomingPacket
    {
        public ushort Health { get; set; }
        public ushort MaxHealth { get; set; }
        public uint Capacity { get; set; }
        public uint TotalCapacity { get; set; }
        public ulong Experience { get; set; }
        public ushort Level { get; set; }
        public byte LevelPercent { get; set; }
        public ushort Mana { get; set; }
        public ushort MaxMana { get; set; }
        public byte MagicLevel { get; set; }
        public byte BaseMagicLevel { get; set; }
        public byte MagicLevelPercent { get; set; }
        public byte Soul { get; set; }
        public ushort Stamina { get; set; }
        public ushort BaseSpeed { get; set; }
        public ushort Regeneration { get; set; }
        public ushort OfflineTrainingTime { get; set; }

        public PlayerDataPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerData;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerData)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerData;

            Health = msg.GetUInt16();
            MaxHealth = msg.GetUInt16();

            Capacity = msg.GetUInt32();
            TotalCapacity = msg.GetUInt32();

            if (Client.VersionNumber >= 870)
            {
                Experience = msg.GetUInt64();
            }
            else
            {
                Experience = msg.GetUInt32();
            }

            Level = msg.GetUInt16();
            LevelPercent = msg.GetByte();

            Mana = msg.GetUInt16();
            MaxMana = msg.GetUInt16();

            MagicLevel = msg.GetByte();
            if (Client.VersionNumber >= 954)
                BaseMagicLevel = msg.GetByte();
            else
                BaseMagicLevel = MagicLevel;
            MagicLevelPercent = msg.GetByte();

            Soul = msg.GetByte();

            Stamina = msg.GetUInt16();

            if (Client.VersionNumber >= 954)
                BaseSpeed = msg.GetUInt16();
            Regeneration = msg.GetUInt16(); 
            OfflineTrainingTime = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(Health);
            msg.AddUInt16(MaxHealth);

            msg.AddUInt32(Capacity);
            msg.AddUInt32(TotalCapacity);

            if (Client.VersionNumber >= 870)
            {
                msg.AddUInt64(Experience);
            }
            else
            {
                msg.AddUInt32((uint)Experience);
            }

            msg.AddUInt16(Level);
            msg.AddByte(LevelPercent);

            msg.AddUInt16(Mana);
            msg.AddUInt16(MaxMana);

            msg.AddByte(MagicLevel);
            if (Client.VersionNumber >= 954)
                msg.AddByte(BaseMagicLevel);
            msg.AddByte(MagicLevelPercent);

            msg.AddByte(Soul);

            msg.AddUInt16(Stamina);

            if (Client.VersionNumber >= 954)
                msg.AddUInt16(BaseSpeed);
            msg.AddUInt16(Regeneration);
            msg.AddUInt16(OfflineTrainingTime);
        }
    }
}