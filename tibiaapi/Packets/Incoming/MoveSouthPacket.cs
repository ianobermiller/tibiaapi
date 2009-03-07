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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveSouth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveSouth;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Y++;

            try
            {
                setMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y + 7, Client.playerLocation.Z, 18, 1, outMsg);
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
