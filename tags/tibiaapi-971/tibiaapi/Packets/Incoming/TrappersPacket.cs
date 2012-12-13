using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TrappersPacket : IncomingPacket
    {

        public byte CreatureCount { get; set; }
        public System.Collections.Generic.List<PacketCreature> Creatures { get; set; }

        public TrappersPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Trappers;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Trappers)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Trappers;

            CreatureCount = msg.GetByte();

            for (int i = 0; i < CreatureCount; i++)
                Creatures.Add(new PacketCreature(null) { Id = msg.GetUInt32() });

                return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(CreatureCount);
            foreach (PacketCreature c in Creatures)
                msg.AddUInt32(c.Id);
        }
    }
}