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
            Type = IncomingPacketType.PlayerFlagUpdate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.PlayerFlagUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerFlagUpdate;

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