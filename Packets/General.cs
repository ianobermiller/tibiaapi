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

        public static byte[] SetOutfit(Memory.Addresses.Creature.Outfit_t.Type outfitType, byte headColor, byte bodyColor, byte legsColor, byte feetColor, Memory.Addresses.Creature.Outfit_t.Addon addons)
        {
            byte[] packet = new byte[10];
				
            packet[0] = 0x08;
            packet[1] = 0x00;
            packet[2] = 0xD3;
            packet[3] = Packet.Lo(Convert.ToInt32(outfitType));
            packet[4] = Packet.Hi(Convert.ToInt32(outfitType));
            packet[5] = headColor;
            packet[6] = bodyColor;
            packet[7] = legsColor;
            packet[8] = feetColor;
            packet[9] = Convert.ToByte(addons);
				
			return packet;
        }
    }
}
