using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreatureSquarePacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public SquareColor Color { get; set; }

        public CreatureSquarePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSquare;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSquare)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSquare;

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
            CreatureSquarePacket p = new CreatureSquarePacket(client);
            p.CreatureId = creatureId;
            p.Color = color;
            return p.Send();
        }
    }
}