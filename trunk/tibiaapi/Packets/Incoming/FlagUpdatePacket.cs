using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class FlagUpdatePacket : Packet
    {
        public int flags;
        public bool HasFlag(Constants.Flag flag)
        {
            return (flags & (int)flag) == (int)flag;
        }
        public FlagUpdatePacket(Client c)
            : base(c)
        {
            type = PacketType.FlagUpdate;
            destination = PacketDestination.Client;
        }
        public FlagUpdatePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.FlagUpdate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                flags = p.GetInt();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        //TODO

        /*public static FlagUpdatePacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.FlagUpdate);
            
            return new FlagUpdatePacket(c, p.GetPacket());
        }*/
    }
}