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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeUp)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeUp;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            Client.PlayerLocation.Z--;

            try
            {
                //going to surface
                if (Client.PlayerLocation.Z == 7)
                {
                    //floor 7 and 6 already set
                    for (int i = 5; i >= 0; i--)
                        setFloorDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, i, 18, 14, 8 - i);
                }
                //underground, going one floor up (still underground)
                else if (Client.PlayerLocation.Z > 7)
                    setFloorDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, Client.PlayerLocation.Z - 2, 18, 14, 3);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            Client.PlayerLocation.X++;
            Client.PlayerLocation.Y++;

            return true;
        }
    }
}
