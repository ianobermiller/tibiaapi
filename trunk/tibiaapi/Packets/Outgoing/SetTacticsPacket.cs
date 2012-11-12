using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class SetTacticsPacket : OutgoingPacket
    {
        public byte FightMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte SafeMode { get; set; }

        public SetTacticsPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.SetTactics;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.SetTactics)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.SetTactics;

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
            SetTacticsPacket p = new SetTacticsPacket(client);
            p.FightMode = fightMode;
            p.ChaseMode = chaseMode;
            p.SafeMode = safeMode;
            return p.Send();
        }
    }
}