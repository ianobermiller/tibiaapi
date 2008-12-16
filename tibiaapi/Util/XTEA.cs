using System;
using Tibia;

namespace Tibia.Util
{
    /// <summary>
    /// Contains functions for decrypting/encrypting packets
    /// using XTEA encryption
    /// </summary>
    [Obsolete("This class will be removed, please use NetworkMessage.")]
    public static class XTEA
    {
        /*
         * Sample to use for Testing: http://otfans.net/showthread.php?t=40446
         * Encode/Decode routines from: http://www.codeproject.com/KB/mobile/teaencryption.aspx
         * */

        public static byte[] RemoveAdlerChecksum(byte[] packet)
        {
            byte[] packet_WithoutCRC = new byte[packet.Length - 4];
            packet_WithoutCRC[0] = Tibia.Packets.Packet.Lo(packet.Length - 6);
            packet_WithoutCRC[1] = Tibia.Packets.Packet.Hi(packet.Length - 6);
            Array.Copy(packet, 6, packet_WithoutCRC, 2, packet.Length - 6);
            return packet_WithoutCRC;
        }

        public static byte[] AddAdlerChecksum(byte[] packet)
        {
            byte[] packet_WithCRC = new byte[packet.Length + 4];
            byte[] packet_WithoutHeader = new byte[packet.Length - 2];
            AdlerChecksum acs = new AdlerChecksum();
            Array.Copy(packet, 2, packet_WithoutHeader, 0, packet_WithoutHeader.Length);
            packet_WithCRC[0] = Tibia.Packets.Packet.Lo(packet.Length + 2);
            packet_WithCRC[1] = Tibia.Packets.Packet.Hi(packet.Length + 2);
            if (acs.MakeForBuff(packet_WithoutHeader))
            {
                Array.Copy(BitConverter.GetBytes(acs.ChecksumValue), 0, packet_WithCRC, 2, 4);
                Array.Copy(packet_WithoutHeader, 0, packet_WithCRC, 6, packet_WithoutHeader.Length);
                return packet_WithCRC;
            }
            else
                return null;
        }

        public static byte DecryptType(byte[] packet, byte[] key, bool hasAdler)
        {
            if (packet.Length == 0)
                return 0;
            byte[] packet_ready;

            if (hasAdler)
            {
                packet_ready = new byte[packet.Length - 4];
                Array.Copy(RemoveAdlerChecksum(packet), 0, packet_ready, 0, packet.Length - 4);
            }
            else
            {
                packet_ready = new byte[packet.Length];
                Array.Copy(packet, 0, packet_ready, 0, packet.Length);
            }

            uint[] keyprep = key.ToUintArray();

            byte[] start = new byte[8];
            Array.Copy(packet_ready, 2, start, 0, 8);
            uint[] startprep = start.ToUintArray();

            Decode(startprep, 0, keyprep);

            start = startprep.ToByteArray();

            return start[2];
        }

        /// <summary>
        /// Decrypt a packet using XTEA.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="key"></param>
        public static byte[] Decrypt(byte[] packet, byte[] key, bool hasAdler)
        {
            if (packet.Length == 0)
                return packet;

            byte[] packet_ready;
            if (hasAdler)
            {
                packet_ready = new byte[packet.Length - 4];
                Array.Copy(RemoveAdlerChecksum(packet), 0, packet_ready, 0, packet_ready.Length);
            }
            else
            {
                packet_ready = new byte[packet.Length];
                Array.Copy(packet, 0, packet_ready, 0, packet.Length);
            }

            // The first two bytes are the length
            byte[] payload = new byte[packet_ready.Length - 2];

            Array.Copy(packet_ready, 2, payload, 0, payload.Length);

            uint[] payloadprep = payload.ToUintArray();
            uint[] keyprep = key.ToUintArray();

            for (int i = 0; i < payloadprep.Length; i += 2)
            {
                Decode(payloadprep, i, keyprep);
            }

            // Remove the junk bytes
            byte[] decrypted = payloadprep.ToByteArray();
            int length = BitConverter.ToInt16(decrypted, 0) + 2;
            byte[] decryptedprep = new byte[length];
            Array.Copy(decrypted, decryptedprep, length);

            return decryptedprep;
            //return decrypted;
        }

        /// <summary>
        /// Encrypt a packet using XTEA.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="key"></param>
        public static byte[] Encrypt(byte[] packet, byte[] key, bool addAdler)
        {
            if (packet.Length == 0)
                return packet;

            uint[] keyprep = key.ToUintArray();

            // Pad the packet with extra bytes for encryption
            int pad = packet.Length % 8;

            byte[] packetprep;

            if (pad == 0)
                packetprep = new byte[packet.Length];
            else
                packetprep = new byte[packet.Length + (8 - pad)];

            Array.Copy(packet, packetprep, packet.Length);

            uint[] payloadprep = packetprep.ToUintArray();

            for (int i = 0; i < payloadprep.Length; i += 2)
            {
                Encode(payloadprep, i, keyprep);
            }

            byte[] encrypted = new byte[packetprep.Length + 2];

            Array.Copy(payloadprep.ToByteArray(), 0, encrypted, 2, packetprep.Length);

            Array.Copy(BitConverter.GetBytes((short)packetprep.Length), 0, encrypted, 0, 2);

            if (addAdler)
            {

                byte[] encrypted_ready = new byte[encrypted.Length + 4];
                Array.Copy(AddAdlerChecksum(encrypted), 0, encrypted_ready, 0, encrypted_ready.Length);
                return encrypted_ready;
            }
            else
                return encrypted;
        }

        private static void Encode(uint[] v, int index, uint[] k)
        {
            uint y = v[index];
            uint z = v[index + 1];
            uint sum = 0;
            uint delta = 0x9e3779b9;
            uint n = 32;

            while (n-- > 0)
            {
                y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
                sum += delta;
                z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
            }

            v[index] = y;
            v[index + 1] = z;
        }

        private static void Decode(uint[] v, int index, uint[] k)
        {
            try
            {
                uint n = 32;
                uint sum;
                uint y = v[index];
                uint z = v[index + 1];
                uint delta = 0x9e3779b9;

                sum = delta << 5;

                while (n-- > 0)
                {
                    z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
                    sum -= delta;
                    y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
                }

                v[index] = y;
                v[index + 1] = z;
            }
            catch
            {

            }
        }

        private static uint[] ToUintArray(this byte[] bytes)
        {
            uint[] uints = new uint[bytes.Length / 4];

            for (int i = 0; i < uints.Length; i++)
            {
                uints[i] = BitConverter.ToUInt32(bytes, i * 4);
            }

            return uints;
        }

        private static byte[] ToByteArray(this uint[] uints)
        {
            byte[] bytes = new byte[uints.Length * 4];

            for (int i = 0; i < uints.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(uints[i]), 0, bytes, i * 4, 4);
            }

            return bytes;
        }
    }
}
