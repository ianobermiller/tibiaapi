using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class LogoutPacket : Packet
    {
        public LogoutPacket(Client c) : base(c)
        {
            type = PacketType.Logout;
            destination = PacketDestination.Server;
        }

        public LogoutPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Logout) return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LogoutPacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.Logout);
            return new LogoutPacket(c, p.GetPacket());
        }
    }
}
