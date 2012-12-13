using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class LookCreaturePacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public LookCreaturePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.LookCreature;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.LookCreature)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.LookCreature;

            CreatureId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
        }

        public static bool Send(Objects.Client client, uint creatureId)
        {
            return new LookCreaturePacket(client) { CreatureId = creatureId }.Send();
        }
    }
}