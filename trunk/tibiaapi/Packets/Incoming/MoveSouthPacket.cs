using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MoveSouthPacket : MapPacket
    {
        public MoveSouthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveSouth ;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveSouth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveSouth;
            stream.AddByte((byte)Type);

            pos.Y++;

            try
            {
                setMapDescription(msg, pos.X - 8, pos.Y + 7, pos.Z, 18, 1);
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
