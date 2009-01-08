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

            Client.PlayerLocation.Z++;

            try
            {
                //going from surface to underground
                if (Client.PlayerLocation.Z == 8)
                {
                    int j, i;
                    for (i = Client.PlayerLocation.Z, j = -1; i < Client.PlayerLocation.Z + 3; ++i, --j)
                        setFloorDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, i, 18, 14, j);

                }
                //going further down
                else if (Client.PlayerLocation.Z > 8 && Client.PlayerLocation.Z < 14)
                    setFloorDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, Client.PlayerLocation.Z + 2, 18, 14, -3);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            Client.PlayerLocation.X--;
            Client.PlayerLocation.Y--;

            return true;
        }
    }
}
