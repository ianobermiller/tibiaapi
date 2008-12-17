using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class PlayerIconsPacket : IncomingPacket
    {

        public ushort Icons { get; set; }

        public PlayerIconsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.PLAYER_ICONS;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.PLAYER_ICONS)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.PLAYER_ICONS;

            Icons = msg.GetUInt16();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt16(Icons);

            return msg.Packet;
        }
    }
}