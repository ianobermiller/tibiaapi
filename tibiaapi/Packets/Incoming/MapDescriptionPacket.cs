using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MapDescriptionPacket : MapPacket
    {
        public MapDescriptionPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MapDescription;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MapDescription)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MapDescription;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            try
            {
                pos = msg.GetLocation();
                stream.AddLocation(pos);
                setMapDescription(msg, pos.X - 8, pos.Y - 6, pos.Z, 18, 14);
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
