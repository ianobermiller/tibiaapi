
﻿using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TopRowPacket : MapPacket
    {
        public TopRowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TopRow;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TopRow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TopRow;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.Y--;

            //return ReadArea(msg, Client.playerLocation, 0, 0, (MAPSIZE_X - 1), 0, outMsg);
            return ParseMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 18, 1, outMsg);
        }
    }
}