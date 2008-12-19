using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class PlayerStatsPacket : IncomingPacket
    {
        public ushort Health { get; set; }
        public ushort MaxHealth { get; set; }
        public uint Capacity { get; set; }
        public uint Experience { get; set; }
        public ushort Level { get; set; }
        public byte LevelPercent { get; set; }
        public ushort Mana { get; set; }
        public ushort MaxMana { get; set; }
        public byte MagicLevel { get; set; }
        public byte MagicLevelPercent { get; set; }
        public byte Soul { get; set; }
        public ushort Stamina { get; set; }

        public PlayerStatsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerStatusUpdate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerStatusUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerStatusUpdate;

            try
            {
                Health = msg.GetUInt16();
                MaxHealth = msg.GetUInt16();
                Capacity = msg.GetUInt32();

                Experience = msg.GetUInt32();

                Level = msg.GetUInt16();

                LevelPercent = msg.GetByte();

                Mana = msg.GetUInt16();
                MaxMana = msg.GetUInt16();

                MagicLevel = msg.GetByte();
                MagicLevelPercent = msg.GetByte();
                Soul = msg.GetByte();

                Stamina = msg.GetUInt16();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt16(Health);
            msg.AddUInt16(MaxHealth);
            msg.AddUInt32(Capacity);

            msg.AddUInt32(Experience);

            msg.AddUInt16(Level);

            msg.AddByte(LevelPercent);

            msg.AddUInt16(Mana);
            msg.AddUInt16(MaxMana);

            msg.AddByte(MagicLevel);
            msg.AddByte(MagicLevelPercent);
            msg.AddByte(Soul);

            msg.AddUInt16(Stamina);

            return msg.Packet;
        }
    }
}
