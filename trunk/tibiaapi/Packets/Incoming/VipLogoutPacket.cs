using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class VipLogoutPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public VipLogoutPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.VIP_LOGOUT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.VIP_LOGOUT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.VIP_LOGOUT;

            PlayerId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(PlayerId);

            return msg.Packet;
        }
    }
}