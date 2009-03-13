using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets
{
    public static class Xtea
    {
        public static bool XteaEncrypt(ref byte[] buffer, ref int length, int index, uint[] xteaKey)
        {
            if (xteaKey == null)
                return false;

            int msgSize = length - index;

            int pad = msgSize % 8;
            if (pad > 0)
            {
                msgSize += (8 - pad);
                length = index + msgSize;
            }

            for (int i = index; i < length; i += 8)
                XTEAEncrypt(buffer, i, xteaKey);

            return true;
        }

        public static bool XteaDecrypt(ref byte[] buffer, ref int length, int index, uint[] xteaKey)
        {
            if (length <= index || (length - index) % 8 > 0 || xteaKey == null)
                return false;

            for (int i = index; i < length; i += 8)
                XTEADecrypt(buffer, i, xteaKey);

            int decrpytMsgLen = (int)BitConverter.ToUInt16(buffer, index) + 2;
            length = decrpytMsgLen + index;

            return true;
        }

        private unsafe static void XTEAEncrypt(byte[] buffer, int offset, uint[] key)
        {
            // defintions that are used to create the cipher text
            uint x_sum = 0, x_delta = 0x9e3779b9, x_count = 32;

            // main part of the XTEA Cipher
            fixed (byte* bufferPtr = buffer)
            {
                uint* words = (uint*)(bufferPtr + offset);

                while (x_count-- > 0)
                {
                    words[0] += (words[1] << 4 ^ words[1] >> 5) + words[1] ^ x_sum
                        + key[x_sum & 3];
                    x_sum += x_delta;
                    words[1] += (words[0] << 4 ^ words[0] >> 5) + words[0] ^ x_sum
                        + key[x_sum >> 11 & 3];
                }
            }
        }

        private unsafe static void XTEADecrypt(byte[] buffer, int offset, uint[] key)
        {
            // defintions that are used to restore the plaintext text
            uint x_count = 32, x_sum = 0xC6EF3720, x_delta = 0x9E3779B9;

            fixed (byte* bufferPtr = buffer)
            {
                uint* words = (uint*)(bufferPtr + offset);

                // main part of the XTEA Cipher
                while (x_count-- > 0)
                {
                    words[1] -= (words[0] << 4 ^ words[0] >> 5) + words[0] ^ x_sum
                        + key[x_sum >> 11 & 3];
                    x_sum -= x_delta;
                    words[0] -= (words[1] << 4 ^ words[1] >> 5) + words[1] ^ x_sum
                        + key[x_sum & 3];
                }
            }
        }
    }
}
