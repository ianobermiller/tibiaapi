using System;

namespace Tibia.Util
{
    /// <summary>
    /// Contains functions for decrypting/encrypting packets
    /// using XTEA encryption
    /// </summary>
    public static class XTEA
    {
        /* --------------------------------------------------------------------
         * Sample to use for Testing (http://otfans.net/showthread.php?t=40446)
         * --------------------------------------------------------------------
         * 
         * Encrypted Character List Packet:
         * 98 00 EF 32 44 5B 2D 3D A4 7D E7 F1 EC 35 AA 20 C8 35 81 10 F1 40 14 30 C3 DA 76 8B 2E 2A E0 75 D0 04 AA A6 6A 75 7E 5C 1B FB 97 33 3D F8 A8 67 06 D3 BD AB 03 F3 E6 3B DE 89 65 22 56 0B 87 0E 2D 05 14 D3 92 B3 20 3A 0E 0E 9A DD 56 1A E9 73 D0 6D FC 50 49 28 72 85 B5 F2 39 51 C5 1B F8 50 0C FC E2 7E 82 F2 7C 77 CD 6D D4 A1 28 FD 9A B0 B8 F0 09 03 B1 F4 4E 3B 3F BC 9B 1E EE A0 50 97 39 1D 8A 9B 69 25 9D 42 04 4A 52 DD BD 2E A7 E6 C7 4F 3D 51 D3 74 F8 6C 39 48
         * 
         * XTEA Key Used:
         * 10 E4 24 5C 38 CB 8E 00 EB 17 BC 82 7C 5D 3C 4D
         * 
         * Decrypted Packet:
         * 8F 00 14 6F 00 33 30 30 0A 57 65 6C 63 6F 6D 65 20 74 6F 20 74 68 65 20 54 65 73 74 73 65 72 76 65 72 21 0A 0A 50 6C 65 61 73 65 20 72 65 70 6F 72 74 20 61 6C 6C 20 62 75 67 73 20 79 6F 75 20 64 65 74 65 63 74 20 74 6F 20 61 20 74 75 74 6F 72 2E 0A 0A 48 61 70 70 79 20 74 65 73 74 69 6E 67 21 0A 54 68 65 20 43 69 70 53 6F 66 74 20 54 65 61 6D 0A 64 01 0A 00 42 6C 61 63 6B 64 20 4E 65 6F 05 00 54 65 73 74 61 3E 92 4E CE 03 1C 00 00 CB D2 44 F5 DE 5F D4
         * where the last 7 bytes there are random trash
         * 
         * http://tpforums.org/forum/showthread.php?t=696
         * 1. Client generates a random XTEA1 in memory.
         * 2. (enciphered with RSA) Client sends key XTEA1, account and password to Loginserver.
         * 3. (enciphered with XTEA1) Loginserver answers Character list. Each character includes Gamserver info (server IP and port)
         * 4. Client closes connection with Loginserver.
         * 5. Client generates a random XTEA2 in memory.
6. (enciphered with RSA) Client sends key XTEA2, charactername and password to Gameserver
         * 7. (enciphered with XTEA2) Gameserver answers game info (first packet includes your powers, full map, stats and vip
         * 8. (enciphered with XTEA2) Client sends game commands
         * 9. (enciphered with XTEA2) Gameserver sends game info
         * */

        /// <summary>
        /// Decrypte a packet using XTEA.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="key"></param>
        public static void Decrypt(byte[] packet, byte[] key)
        {
            
        }

        /// <summary>
        /// Encrypt a packet using XTEA.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="key"></param>
        public static void Encrypt(byte[] packet, byte[] key)
        {

        }
    }
}
