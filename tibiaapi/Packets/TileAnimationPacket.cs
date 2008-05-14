using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class TileAnimationPacket : Packet
    {
        private Objects.Location loc;
        private byte anim;

        public Objects.Location Location
        {
            get { return loc; }
        }
        
        public byte Animation
        {
            get { return anim; }
        }

        public TileAnimationPacket()
        {
            type=PacketType.TileAnimation;
            destination= PacketDestination.Client;
        }

        public TileAnimationPacket(byte[] data) : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.TileAnimation) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                loc = p.GetLocation();
                anim = p.GetByte();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static TileAnimationPacket Create(Objects.Location loc, byte animation)
        {
            PacketBuilder p = new PacketBuilder(PacketType.TileAnimation);
            p.AddLocation(loc);
            p.AddByte(animation);
            TileAnimationPacket tap = new TileAnimationPacket(p.GetPacket());
            return tap;
        }
    }
}
