using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Create general use packets.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns></returns>
        public static byte[] Logout()
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = 0x14;
            return packet;
        }

        /// <summary>
        /// Stop all actions.
        /// </summary>
        /// <returns></returns>
        public static byte[] Stop()
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = 0xBE;
            return packet;
        }

        public static byte[] SetOutfit(int Outfit, byte Head, byte Body, byte Legs, byte Feet, byte Addons)
        {
            byte[] packet = new byte[9];
            packet[0] = 0x08;
            packet[1] = 0x00;
            packet[2] = 0xD3;
            packet[3] = Packet.Lo(Outfit);
            packet[4] = Packet.Hi(Outfit);
            packet[5] = Head;
            packet[6] = Body;
            packet[7] = Legs;
            packet[8] = Feet;
            packet[9] = Addons;
        }
    }
}
