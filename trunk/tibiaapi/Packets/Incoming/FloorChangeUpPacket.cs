using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class FloorChangeUpPacket : MapPacket
    {

        public FloorChangeUpPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FloorChangeUp ;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FloorChangeUp)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FloorChangeUp;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Z--;

            try
            {
                //going to surface
                if (Client.playerLocation.Z == 7)
                {
                    //floor 7 and 6 already set
                    for (int i = 5; i >= 0; i--)
                        ParseFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, i, 18, 14, 8 - i, outMsg);
                }
                //underground, going one floor up (still underground)
                else if (Client.playerLocation.Z > 7)
                    ParseFloorDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z - 2, 18, 14, 3, outMsg);

                return true;
            }
            finally
            {
                Client.playerLocation.X++;
                Client.playerLocation.Y++;
            }
        }

    }
}
