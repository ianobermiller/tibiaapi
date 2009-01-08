using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MoveEastPacket : MapPacket
    {

        public MoveEastPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveEast;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveEast)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveEast;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            Client.PlayerLocation.X++;

            try
            {
                setMapDescription(msg, Client.PlayerLocation.X + 9, Client.PlayerLocation.Y - 6, Client.PlayerLocation.Z, 1, 14);
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
