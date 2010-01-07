using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class VipLogoutPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public VipLogoutPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.VipLogout;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.VipLogout)
                return false;

            Destination = destination;
            Type = IncomingPacketType.VipLogout;

            PlayerId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(PlayerId);
        }
    }
}