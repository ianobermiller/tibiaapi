using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class VipAddPacket : Packet
    {
        private int id;
        private string name;
        private bool loggedin;
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public bool LoggedIn
        {
            get { return loggedin; }
        }
        public VipAddPacket(Client c)
            : base(c)
        {
            type = PacketType.VipAdd;
            destination = PacketDestination.Client;
        }
        public VipAddPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.VipAdd) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                id = p.GetLong();
                name = p.GetString();
                loggedin = p.GetByte()>0;
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static VipAddPacket Create(Client c,int id, string name, bool loggedin)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.VipAdd);
            p.AddLong(id);
            p.AddString(name);
            p.AddByte(Convert.ToByte(loggedin));
            return new VipAddPacket(c, p.GetPacket());
        }
    }
}