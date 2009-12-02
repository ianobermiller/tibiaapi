using System;
using Tibia.Constants;

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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.WorldLight)
                return false;

            Destination = destination;
            Type = IncomingPacketType.WorldLight;

            try
            {
                LightLevel = msg.GetByte();
                LightColor = msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(LightLevel);
            msg.AddByte(LightColor);
        }
    }
}