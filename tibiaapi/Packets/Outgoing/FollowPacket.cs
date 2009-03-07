using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class FollowPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public FollowPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Follow;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Follow)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Follow;

            CreatureId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
        }

        public static bool Send(Objects.Client client,uint creatureId)
        {
            FollowPacket p = new FollowPacket(client);
            p.CreatureId = creatureId;
            return p.Send();
        }
    }
}