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
                int index = 3;
                loc.X = BitConverter.ToInt16(packet, index);
                index += 2;
                loc.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                loc.Z = packet[index];
                index += 1;
                anim = packet[index];
                return true;
            }
            else
            {
                return false;
            }
        }
        public static TileAnimationPacket Create(Objects.Location loc, byte Animation)
        {
            byte[] packet = new byte[9];
            packet[0] = 0x07;
            packet[2] = (byte)PacketType.TileAnimation;
            Array.Copy(BitConverter.GetBytes((short)(loc.X)), 0, packet, 3, 2);
            Array.Copy(BitConverter.GetBytes((short)(loc.Y)), 0, packet, 5, 2);
            packet[7] = (byte)loc.Z;
            packet[8] = Animation;
            TileAnimationPacket tap = new TileAnimationPacket(packet);
            return tap;
        }
    }
}
