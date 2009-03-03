using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets
{
    public class Xtea
    {
        public Xtea() { }

        public bool XteaEncrypt(ref byte[] buffer, ref int length, int index, uint[] xteaKey)
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
                XTEAEncrypt(buffer, i, 8, xteaKey);

            return true;
        }

        public bool XteaDecrypt(ref byte[] buffer, ref int length, int index, uint[] xteaKey)
        {
            if (length <= index || (length - index) % 8 > 0 || xteaKey == null)
                return false;

            for (int i = index; i < length; i += 8)
                XTEADecrypt(buffer, i, 8, xteaKey);

            int decrpytMsgLen = (int)BitConverter.ToUInt16(buffer, index) + 2;
            length = decrpytMsgLen + index;

            return true;
        }

        private void XTEAEncrypt(byte[] p_data, int p_offset, int p_count, uint[] o_key)
        {
            // defintions that are used to create the cipher text
            uint x_sum = 0, x_delta = 0x9e3779b9, x_count = 32;
            // convert the plaintext data into 32 bit words
            uint[] x_words = ConvertBytesToUints(p_data, p_offset, p_count);
            // main part of the XTEA Cipher
            while (x_count-- > 0)
            {
                x_words[0] += (x_words[1] << 4 ^ x_words[1] >> 5) + x_words[1] ^ x_sum
                    + o_key[x_sum & 3];
                x_sum += x_delta;
                x_words[1] += (x_words[0] << 4 ^ x_words[0] >> 5) + x_words[0] ^ x_sum
                    + o_key[x_sum >> 11 & 3];
            }

            Array.Copy(ConvertUintstoBytes(x_words), 0, p_data, p_offset, p_count);
        }

        private void XTEADecrypt(byte[] p_data, int p_offset, int p_count, uint[] o_key)
        {
            // defintions that are used to restore the plaintext text
            uint x_count = 32, x_sum = 0xC6EF3720, x_delta = 0x9E3779B9;
            // convert the data into 32 bit words
            uint[] x_words = ConvertBytesToUints(p_data, p_offset, p_count);

            // main part of the XTEA Cipher
            while (x_count-- > 0)
            {
                x_words[1] -= (x_words[0] << 4 ^ x_words[0] >> 5) + x_words[0] ^ x_sum
                    + o_key[x_sum >> 11 & 3];
                x_sum -= x_delta;
                x_words[0] -= (x_words[1] << 4 ^ x_words[1] >> 5) + x_words[1] ^ x_sum
                    + o_key[x_sum & 3];
            }

            // convert the unit[] into a byte[]
            Array.Copy(ConvertUintstoBytes(x_words), 0, p_data, p_offset, p_count);
        }

        private uint[] ConvertBytesToUints(byte[] p_data, int p_offset, int p_count)
        {
            // allocate an array - each uint requires 4 bytes
            uint[] x_result = new uint[p_count / 4];
            // run through the data and create the unsigned ints from
            // the array of bytes
            for (int i = p_offset, j = 0; i < p_offset + p_count; i += 4, j++)
            {
                x_result[j] = BitConverter.ToUInt32(p_data, i);
            }
            // return the array of 32-bit unsigned ints
            return x_result;
        }

        private byte[] ConvertUintstoBytes(uint[] p_data)
        {
            // convert the unit[] into a byte[]
            byte[] x_result = new byte[p_data.Length * 4];
            // run through the data and create the bytes from
            // the array of unsigned ints
            for (int i = 0, j = 0; i < p_data.Length; i++, j += 4)
            {
                byte[] x_interim = BitConverter.GetBytes(p_data[i]);
                Array.Copy(x_interim, 0, x_result, j, x_interim.Length);
            }
            // return the array of 8-bit bytes
            return x_result;
        }
    }
}
