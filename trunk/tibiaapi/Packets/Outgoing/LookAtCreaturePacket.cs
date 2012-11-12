using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class LookAtCreaturePacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public LookAtCreaturePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.LookAtCreature;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.LookAtCreature)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.LookAtCreature;

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
            LookAtCreaturePacket p = new LookAtCreaturePacket(client);
            p.CreatureId = creatureId;
            return p.Send();
        }
    }
}