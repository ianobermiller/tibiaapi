using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class FloorChangeDownPacket : MapPacket
    {

        public FloorChangeDownPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FloorChangeDown;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeDown)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeDown;
            stream.AddByte((byte)Type);

            pos.Z++;

            try
            {
                //going from surface to underground
                if (pos.Z == 8)
                {
                    int j, i;
                    for (i = pos.Z, j = -1; i < pos.Z + 3; ++i, --j)
                        setFloorDescription(msg, pos.X - 8, pos.Y - 6, i, 18, 14, j);

                }
                //going further down
                else if (pos.Z > 8 && pos.Z < 14)
                    setFloorDescription(msg, pos.X - 8, pos.Y - 6, pos.Z + 2, 18, 14, -3);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            pos.X--;
            pos.Y--;

            return true;
        }
    }
}
