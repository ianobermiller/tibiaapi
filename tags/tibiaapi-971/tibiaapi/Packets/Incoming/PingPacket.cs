using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PingPacket : IncomingPacket
    {

        public PingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Ping;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Ping)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Ping;

            //no data


            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}