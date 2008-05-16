using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class LogoutPacket : Packet
    {
        public LogoutPacket()
        {
            type = PacketType.Logout;
            destination = PacketDestination.Server;
        }

        public LogoutPacket(byte[] data)
            : this()
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

        public static LogoutPacket Create()
        {
            PacketBuilder p = new PacketBuilder(PacketType.Logout);
            return new LogoutPacket(p.GetPacket());
        }
    }
}
