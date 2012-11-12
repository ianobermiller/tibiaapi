using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MarkCreaturePacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public SquareColor Color { get; set; }

        public MarkCreaturePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MarkCreature;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MarkCreature)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MarkCreature;

            CreatureId = msg.GetUInt32();
            Color = (SquareColor)msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddByte((byte)Color);
        }

        public static bool Send(Objects.Client client, uint creatureId, SquareColor color)
        {
            MarkCreaturePacket p = new MarkCreaturePacket(client);
            p.CreatureId = creatureId;
            p.Color = color;
            return p.Send();
        }
    }
}