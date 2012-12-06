using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ChangeFightModesPacket : OutgoingPacket
    {
        public byte FightMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte SafeMode { get; set; }

        public ChangeFightModesPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChangeFightModes;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChangeFightModes)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChangeFightModes;

            FightMode = msg.GetByte();
            ChaseMode = msg.GetByte();
            SafeMode = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddByte(FightMode);
            msg.AddByte(ChaseMode);
            msg.AddByte(SafeMode);
        }

        public static bool Send(Objects.Client client, byte fightMode, byte chaseMode, byte safeMode)
        {
            return new ChangeFightModesPacket(client) { FightMode = fightMode, ChaseMode = chaseMode, SafeMode = safeMode }.Send();
        }
    }
}