using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MoveWestPacket : MapPacket
    {
        public MoveWestPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveWest;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveWest)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveWest;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            Client.PlayerLocation.X--;

            try
            {
                setMapDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y - 6, Client.PlayerLocation.Z, 1, 14);
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
