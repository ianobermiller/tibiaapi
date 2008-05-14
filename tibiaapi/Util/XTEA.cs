using System;

namespace Tibia.Util
{
    /// <summary>
    /// Contains functions for decrypting/encrypting packets
    /// using XTEA encryption
    /// </summary>
    public static class XTEA
    {
        /*
         * Sample to use for Testing: http://otfans.net/showthread.php?t=40446
         * Encode/Decode routines from: http://www.codeproject.com/KB/mobile/teaencryption.aspx
         * */

        public static byte DecryptType(byte[] packet, byte[] key)
        {
            if (packet.Length == 0)
                return 0;
            
            uint[] keyprep = ByteArrayToUintArray(key);

            byte[] start = new byte[8];
            Array.Copy(packet, 2, start, 0, 8);
            uint[] startprep = ByteArrayToUintArray(start);

            Decode(startprep, 0, keyprep);

            start = UintArrayToByteArray(startprep);

            return start[2];
        }

        /// <summary>
        /// Decrypt a packet using XTEA.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="key"></param>
        public static byte[] Decrypt(byte[] packet, byte[] key)
        {
            if (packet.Length == 0)
                return packet;

            // The first two bytes are the length
            byte[] payload = new byte[packet.Length - 2];

            Array.Copy(packet, 2, payload, 0, payload.Length);

            uint[] payloadprep = ByteArrayToUintArray(payload);
            uint[] keyprep = ByteArrayToUintArray(key);

            for (int i = 0; i < payloadprep.Length; i += 2)
            {
                Decode(payloadprep, i, keyprep);
            }

            // Remove the junk bytes
            byte[] decrypted = UintArrayToByteArray(payloadprep);
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
        public static byte[] Encrypt(byte[] packet, byte[] key)
        {
            if (packet.Length == 0)
                return packet;

            uint[] keyprep = ByteArrayToUintArray(key);

            // Pad the packet with extra bytes for encryption
            int pad = packet.Length % 8;
            byte[] packetprep = new byte[packet.Length + (8 - pad)];
            Array.Copy(packet, packetprep, packet.Length);

            uint[] payloadprep = ByteArrayToUintArray(packetprep);

            for (int i = 0; i < payloadprep.Length; i += 2)
            {
                Encode(payloadprep, i, keyprep);
            }

            byte[] encrypted = new byte[packetprep.Length + 2];

            Array.Copy(UintArrayToByteArray(payloadprep), 0, encrypted, 2, packetprep.Length);

            Array.Copy(BitConverter.GetBytes((short)packetprep.Length), 0, encrypted, 0, 2);

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

        private static uint[] ByteArrayToUintArray(byte[] bytes)
        {
            uint[] uints = new uint[bytes.Length / 4];

            for (int i = 0; i < uints.Length; i++)
            {
                uints[i] = BitConverter.ToUInt32(bytes, i * 4);
            }

            return uints;
        }

        private static byte[] UintArrayToByteArray(uint[] uints)
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
