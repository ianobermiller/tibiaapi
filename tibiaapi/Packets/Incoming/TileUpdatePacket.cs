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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileUpdate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileUpdate;
            stream.AddByte((byte)Type);

            try
            {
                pos = msg.GetLocation();
                stream.AddLocation(pos);

                ushort thingId = msg.PeekUInt16();

                if (thingId == 0xFF01)
                {
                    stream.AddUInt16(msg.GetUInt16());
                    //if (!tile)
                    //{
                    //    RAISE_PROTOCOL_ERROR("Tile Update - !tile");
                    //}
                }
                else
                {
                    if (!setTileDescription(msg, pos))
                    {
                        //RAISE_PROTOCOL_ERROR("Tile Update - SetTileDescription");
                    }
                    stream.AddUInt16(msg.GetUInt16());
                }
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }
    }
}