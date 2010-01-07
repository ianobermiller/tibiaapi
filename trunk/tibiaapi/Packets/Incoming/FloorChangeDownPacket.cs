using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class FloorChangeDownPacket : MapPacket
    {

        public FloorChangeDownPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FloorChangeDown;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeDown)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeDown;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Z++;

            try
            {
                //going from surface to underground
                if (Client.playerLocation.Z == 8)
                {
                    int j, i;
                    for (i = Client.playerLocation.Z, j = -1; i < Client.playerLocation.Z + 3; ++i, --j)
                        SetFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, i, 18, 14, j, outMsg);

                }
                //going further down
                else if (Client.playerLocation.Z > 8 && Client.playerLocation.Z < 14)
                    SetFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z + 2, 18, 14, -3, outMsg);

                return true;
            }
            finally
            {
                Client.playerLocation.X--;
                Client.playerLocation.Y--;
            }
        }
    }
}
