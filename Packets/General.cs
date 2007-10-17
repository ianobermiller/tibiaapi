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
    }
}
