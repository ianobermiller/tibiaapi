using System;
using System.Collections.Generic;

namespace Tibia.Packets
{
    /// <summary>
    /// Create packets to alter your position.
    /// </summary>
    public static class Position
    {
        /// <summary>
        /// Turn to the specified direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static byte[] Turn(Memory.Addresses.Creature.Direction_t direction)
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = Convert.ToByte(0x6F + direction);
            return packet;
        }
        
        /// <summary>
        /// Walk in the specified direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static byte[] Walk(WalkDirection direction)
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = Convert.ToByte(0x65 + direction);
            return packet;
        }

        /// <summary>
        /// Walk in the specified list of directions.
        /// TODO: Finish
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static byte[] Walk(List<WalkDirection> list)
        {
            int len = 4 + list.Count;
            byte[] packet = new byte[len];
            packet[0] = Convert.ToByte(len);
            packet[1] = 0x00;
            packet[2] = 0x64;
            packet[3] = Convert.ToByte(list.Count);
            // Finish
            return packet;
        }

        /// <summary>
        /// Go to the specified location. Not actually a packet, just memory writing.
        /// TODO
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static byte[] GoTo(Objects.Location location)
        {
            return null;
        }

        public enum WalkDirection
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
            UpRight = 5,
            DownRight = 6,
            DownLeft = 7,
            UpLeft = 8
        }
    }
}
