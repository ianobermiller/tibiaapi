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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveSouth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveSouth;
            stream = new NetworkMessage(Client, 0);
            stream.AddByte((byte)Type);

            Client.PlayerLocation.Y++;

            try
            {
                setMapDescription(msg, Client.PlayerLocation.X - 8, Client.PlayerLocation.Y + 7, Client.PlayerLocation.Z, 18, 1);
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
