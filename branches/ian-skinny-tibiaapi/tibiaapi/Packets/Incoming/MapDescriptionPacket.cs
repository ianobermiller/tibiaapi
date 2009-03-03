using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MapDescriptionPacket : MapPacket
    {
        public MapDescriptionPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MapDescription;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MapDescription)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MapDescription;
            outMsg.AddByte((byte)Type);

            try
            {
                //the client send the player location here.
                Client.playerLocation = msg.GetLocation();
                outMsg.AddLocation(Client.playerLocation);
                setMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 18, 14, outMsg);
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
