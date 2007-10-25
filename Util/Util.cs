using System;
using System.Runtime.InteropServices;

namespace Tibia
{
    public static class Packet
    {
        [DllImport("packet.dll")]
        private static extern bool SendPacket(uint processID, byte[] packet, byte encrypt, byte safeArray);

        /// <summary>
        /// Get the low byte of a short value (first in the packet)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte Lo(long value)
        {
            return BitConverter.GetBytes(value)[0];
        }

        /// <summary>
        /// Get the high byte of a short value (second in the packet)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte Hi(long value)
        {
            return BitConverter.GetBytes(value)[1];
        }

        /// <summary>
        /// Send a packet through the client using encryption and packet.dll.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacket(Objects.Client client, Byte[] packet)
        {
            return SendPacket(client, packet, true);
        }

        public static bool SendPacket(Objects.Client client, Byte[] packet, Boolean encrypt)
        {
            try
            {
                return SendPacket((uint)client.getProcess().Id, packet, Convert.ToByte(encrypt), 0);
            }
            catch (AccessViolationException ave)
            {
                return false;
            }
        }
    }
}
