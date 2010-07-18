using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TileUpdatePacket : MapPacket
    {
        public TileUpdatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TileUpdate;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.TileUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileUpdate;
            outMsg.AddByte((byte)Type);

            Objects.Location pos = msg.GetLocation();
            outMsg.AddLocation(pos);

            ushort thingId = msg.PeekUInt16();

            if (thingId == 0xFF01)
            {
                outMsg.AddUInt16(msg.GetUInt16());
            }
            else
            {
                ParseTileDescription(msg, pos, outMsg);
                outMsg.AddUInt16(msg.GetUInt16());
            }

            return true;
        }
    }
}