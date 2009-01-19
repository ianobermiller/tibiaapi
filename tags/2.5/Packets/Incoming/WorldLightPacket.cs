using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class WorldLightPacket : IncomingPacket
    {

        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }

        public WorldLightPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.WorldLight;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.WorldLight)
                return false;

            Destination = destination;
            Type = IncomingPacketType.WorldLight;

            LightLevel = msg.GetByte();
            LightColor = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte(LightLevel);
            msg.AddByte(LightColor);

            return msg.Packet;
        }
    }
}