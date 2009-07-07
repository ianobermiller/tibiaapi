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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveNorth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveNorth;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Y--;

            try
            {
                SetMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 18, 1, outMsg);
            }
            catch (Exception)
            {
                msg.Position = msgPosition;
                outMsg.Position = outMsgPosition;
                return false;
            }

            return true;
        }
    }
}
