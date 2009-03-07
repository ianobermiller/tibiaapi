using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class FightModesPacket : OutgoingPacket
    {
        public byte FightMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte SafeMode { get; set; }

        public FightModesPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.FightModes;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.FightModes)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.FightModes;

            FightMode = msg.GetByte();
            ChaseMode = msg.GetByte();
            SafeMode = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddByte(FightMode);
            msg.AddByte(ChaseMode);
            msg.AddByte(SafeMode);
        }

        public static bool Send(Objects.Client client, byte fightMode, byte chaseMode, byte safeMode)
        {
            FightModesPacket p = new FightModesPacket(client);
            p.FightMode = fightMode;
            p.ChaseMode = chaseMode;
            p.SafeMode = safeMode;
            return p.Send();
        }
    }
}