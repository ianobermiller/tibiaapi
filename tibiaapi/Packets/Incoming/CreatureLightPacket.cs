using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreatureLightPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte LightColor { get; set; }
        public byte LightLevel { get; set; }

        public CreatureLightPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureLight;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureLight)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureLight;

            CreatureId = msg.GetUInt32();
            LightLevel = msg.GetByte();
            LightColor = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddByte(LightLevel);
            msg.AddByte(LightColor);
        }
    }
}