using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class QuestLogPacket : IncomingPacket
    {
        public QuestLogPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.QuestLog;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.QuestLog)
                return false;

            Destination = destination;
            Type = IncomingPacketType.QuestLog;

            ushort Count = msg.GetUInt16();
            for (int i = 0; i < Count; i++)
            {
                msg.GetUInt16();
                msg.GetString();
                msg.GetByte();
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}