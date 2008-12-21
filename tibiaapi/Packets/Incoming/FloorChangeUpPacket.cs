using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class FloorChangeUpPacket : MapPacket
    {

        public FloorChangeUpPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FloorChangeUp ;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeUp)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeUp;
            stream.AddByte((byte)Type);

            pos.Z--;

            try
            {
                //going to surface
                if (pos.Z == 7)
                {
                    //floor 7 and 6 already set
                    for (int i = 5; i >= 0; i--)
                        setFloorDescription(msg, pos.X - 8, pos.Y - 6, i, 18, 14, 8 - i);
                }
                //underground, going one floor up (still underground)
                else if (pos.Z > 7)
                    setFloorDescription(msg, pos.X - 8, pos.Y - 6, pos.Z - 2, 18, 14, 3);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            pos.X++;
            pos.Y++;

            return true;
        }
    }
}
