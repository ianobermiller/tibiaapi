using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerDataPacket : IncomingPacket
    {
        public ushort Health { get; set; }
        public ushort MaxHealth { get; set; }
        public uint Capacity { get; set; }
        public ulong Experience { get; set; }
        public ushort Level { get; set; }
        public byte LevelPercent { get; set; }
        public ushort Mana { get; set; }
        public ushort MaxMana { get; set; }
        public byte MagicLevel { get; set; }
        public byte MagicLevelPercent { get; set; }
        public byte Soul { get; set; }
        public ushort Stamina { get; set; }

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
            msg.GetUInt32(); //cap?

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
            MagicLevelPercent = msg.GetByte();
            msg.GetByte();

            Soul = msg.GetByte();

            Stamina = msg.GetUInt16();

            msg.GetUInt16(); //strength?
            msg.GetUInt16(); //eat
            msg.GetUInt16(); //offline training

            return true;
        }
    }
}