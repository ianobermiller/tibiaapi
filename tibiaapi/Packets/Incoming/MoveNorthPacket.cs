using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MoveNorthPacket : MapPacket
    {
        public MoveNorthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveNorth;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveNorth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveNorth;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            pos.Y--;

            try
            {
                setMapDescription(msg, pos.X - 8, pos.Y - 6, pos.Z, 18, 1);
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
