using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileUpdate;
            outMsg.AddByte((byte)Type);

            try
            {
                Objects.Location pos = msg.GetLocation();
                outMsg.AddLocation(pos);

                ushort thingId = msg.PeekUInt16();

                if (thingId == 0xFF01)
                {
                    outMsg.AddUInt16(msg.GetUInt16());
                    //if (!tile)
                    //{
                    //    RAISE_PROTOCOL_ERROR("Tile Update - !tile");
                    //}
                }
                else
                {
                    if (!setTileDescription(msg, pos, outMsg))
                    {
                        //RAISE_PROTOCOL_ERROR("Tile Update - SetTileDescription");
                    }
                    outMsg.AddUInt16(msg.GetUInt16());
                }
            }
            catch (Exception)
            {
                msg.Position = msgPosition;
                outMsg.Position = outMsgPosition;
                return false;
            }

            return true;
        }
    }
}