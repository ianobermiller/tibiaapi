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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeDown)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeDown;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            Client.playerLocation.Z++;

            try
            {
                //going from surface to underground
                if (Client.playerLocation.Z == 8)
                {
                    int j, i;
                    for (i = Client.playerLocation.Z, j = -1; i < Client.playerLocation.Z + 3; ++i, --j)
                        setFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, i, 18, 14, j);

                }
                //going further down
                else if (Client.playerLocation.Z > 8 && Client.playerLocation.Z < 14)
                    setFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z + 2, 18, 14, -3);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            Client.playerLocation.X--;
            Client.playerLocation.Y--;

            return true;
        }
    }
}
