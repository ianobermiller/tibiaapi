using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class PassLeadershipPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public PassLeadershipPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.PassLeadership;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.PassLeadership)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.PassLeadership;

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
            return new PassLeadershipPacket(client) { CreatureId = creatureId }.Send();
        }
    }
}