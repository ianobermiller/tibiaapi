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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MapDescription)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MapDescription;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            try
            {
                //the client send the player location here.
                Client.PlayerLocation = msg.GetLocation();
                stream.AddLocation(Client.PlayerLocation);
                setMapDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, Client.PlayerLocation.Z, 18, 14);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }
    }
}
