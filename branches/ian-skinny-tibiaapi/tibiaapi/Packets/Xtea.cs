using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets
{
    public static class Xtea
    {
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
                Array.Copy(AdlerChecksum.AddTo(encrypted), 0, encrypted_ready, 0, encrypted_ready.Length);
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
