using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class UpdateTilePacket : MapPacket
    {
        public UpdateTilePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.UpdateTile;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.UpdateTile)
                return false;

            Destination = destination;
            Type = IncomingPacketType.UpdateTile;
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