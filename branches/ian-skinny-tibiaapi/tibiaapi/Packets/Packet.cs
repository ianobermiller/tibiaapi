using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Tibia.Util;

namespace Tibia.Packets
{
    public class Packet
    {
        public uint PacketId { get; set; }
        public bool Forward { get; set; }
        public PacketDestination Destination { get; set; }
        public Objects.Client Client { get; set; }

        public Packet(Objects.Client c)
        {
            Client = c;
            Forward = true;
        }

        public virtual byte[] ToByteArray() 
        {
            return null;
        }

        public bool Send() 
        {
            if (Client.UsingProxy)
            {
                NetworkMessage msg = new NetworkMessage(Client);
                msg.AddBytes(ToByteArray());
                msg.InsetLogicalPacketHeader();
                msg.PrepareToSend();

                if (Destination == PacketDestination.Client)
                    Client.Proxy.SendToClient(msg);
                else if (Destination == PacketDestination.Server)
                    Client.Proxy.SendToServer(msg);
                else
                    return false;

                return true;

            }
            else if (Destination == PacketDestination.Server)
            {
                // send with dll.
                byte[] packet = ToByteArray();
                byte[] sendPacket = new byte[packet.Length + 2];
                Array.Copy(packet, 0, sendPacket, 2, packet.Length);
                Array.Copy(BitConverter.GetBytes((ushort)packet.Length), sendPacket, 2);

                return SendPacketWithDLL(Client, sendPacket);
            }

            return false;
        }

        #region Sending Packets with packet.dll
        [DllImport("packet.dll")]
        private static extern bool SendPacket(uint processID, byte[] packet);

        /// <summary>
        /// Send a packet through the client using packet.dll.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketWithDLL(Objects.Client client, Byte[] packet)
        {
            try
            {
                return SendPacket((uint)client.Process.Id, packet);
            }
            catch (DllNotFoundException)
            {
                throw new Exceptions.PacketDllNotFoundException();
            }
            catch (AccessViolationException)
            {
                return true;
            }
        }
        #endregion

        #region Sending Packets with Stepler's Method(http://www.tpforums.org/forum/showthread.php?t=2832)
        /// <summary>
        /// Send a packet through the client by writing some code in memory and running it.
        /// The packet must not contain any header(no length nor Adler checksum) and be unencrypted
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketByMemory(Objects.Client client, Byte[] packet)
        {
            if (client.LoggedIn)
            {
                if (!client.IsSendCodeWritten)
                    if (!client.WriteSocketSendCode()) return false;


                byte[] packet_=new byte[packet.Length+2];
                Array.Copy(BitConverter.GetBytes(packet.Length), packet_, 2);
                Array.Copy(packet, 0, packet_, 2, packet.Length);

                byte[] encPacket = XTEA.Encrypt(packet_, client.XteaKey.ToByteArray(), true);
                uint pSize=(uint)(encPacket.Length + 4);
                byte[] readyPacket = new byte[pSize];
                Array.Copy(BitConverter.GetBytes(encPacket.Length), readyPacket, 4);
                Array.Copy(encPacket, 0, readyPacket, 4, encPacket.Length);


                /*
                NetworkMessage msg = new NetworkMessage(client);
                msg.AddBytes(packet);
                msg.PrepareToSend();

                byte[] readyPacket = msg.Packet;
                uint bufferSize=(uint)(4+readyPacket.Length);
                byte[] buffer = new byte[bufferSize];
                Array.Copy(BitConverter.GetBytes(bufferSize), buffer, 4);
                Array.Copy(readyPacket, 0, buffer, 4, readyPacket.Length);*/

                IntPtr pRemote = Tibia.Util.WinApi.VirtualAllocEx(client.ProcessHandle, IntPtr.Zero, /*bufferSize*/
                                            pSize,
                                            Tibia.Util.WinApi.MEM_COMMIT | Tibia.Util.WinApi.MEM_RESERVE,
                                            Tibia.Util.WinApi.PAGE_EXECUTE_READWRITE);

                if (pRemote != IntPtr.Zero)
                {
                    if (client.WriteBytes(pRemote.ToInt64(), readyPacket, pSize))
                    {
                        IntPtr threadHandle = Tibia.Util.WinApi.CreateRemoteThread(client.ProcessHandle, IntPtr.Zero, 0,
                            client.SenderAddress, pRemote, 0, IntPtr.Zero);
                        Tibia.Util.WinApi.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                        Tibia.Util.WinApi.CloseHandle(threadHandle);
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }


        #endregion


        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        { 
            return false;
        }
    }

    public static class XTEA
    {
        public static byte[] AddAdlerChecksum(byte[] packet)
        {
            byte[] packet_WithCRC = new byte[packet.Length + 4];
            byte[] packet_WithoutHeader = new byte[packet.Length - 2];
            AdlerChecksum acs = new AdlerChecksum();
            Array.Copy(packet, 2, packet_WithoutHeader, 0, packet_WithoutHeader.Length);
            packet_WithCRC[0] = BitConverter.GetBytes((ushort)(packet.Length + 2))[0];
            packet_WithCRC[1] = BitConverter.GetBytes((ushort)(packet.Length + 2))[1];
            if (acs.MakeForBuff(packet_WithoutHeader))
            {
                Array.Copy(BitConverter.GetBytes(acs.ChecksumValue), 0, packet_WithCRC, 2, 4);
                Array.Copy(packet_WithoutHeader, 0, packet_WithCRC, 6, packet_WithoutHeader.Length);
                return packet_WithCRC;
            }
            else
                return null;
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

    public class AdlerChecksum
    {
        // parameters
        #region
        /// <summary>
        /// AdlerBase is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerBase = 0xFFF1;
        /// <summary>
        /// AdlerStart is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerStart = 0x0001;
        /// <summary>
        /// AdlerBuff is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerBuff = 0x0400;
        /// Adler-32 checksum value
        private uint m_unChecksumValue = 0;
        #endregion
        /// <value>
        /// ChecksumValue is property which enables the user
        /// to get Adler-32 checksum value for the last calculation 
        /// </value>
        public uint ChecksumValue
        {
            get
            {
                return m_unChecksumValue;
            }
        }
        /// <summary>
        /// Calculate Adler-32 checksum for buffer
        /// </summary>
        /// <param name="bytesBuff">Bites array for checksum calculation</param>
        /// <param name="unAdlerCheckSum">Checksum start value (default=1)</param>
        /// <returns>Returns true if the checksum values is successflly calculated</returns>
        public bool MakeForBuff(byte[] bytesBuff, uint unAdlerCheckSum)
        {
            if (Object.Equals(bytesBuff, null))
            {
                m_unChecksumValue = 0;
                return false;
            }
            int nSize = bytesBuff.GetLength(0);
            if (nSize == 0)
            {
                m_unChecksumValue = 0;
                return false;
            }
            uint unSum1 = unAdlerCheckSum & 0xFFFF;
            uint unSum2 = (unAdlerCheckSum >> 16) & 0xFFFF;
            for (int i = 0; i < nSize; i++)
            {
                unSum1 = (unSum1 + bytesBuff[i]) % AdlerBase;
                unSum2 = (unSum1 + unSum2) % AdlerBase;
            }
            m_unChecksumValue = (unSum2 << 16) + unSum1;
            return true;
        }
        /// <summary>
        /// Calculate Adler-32 checksum for buffer
        /// </summary>
        /// <param name="bytesBuff">Bites array for checksum calculation</param>
        /// <returns>Returns true if the checksum values is successflly calculated</returns>
        public bool MakeForBuff(byte[] bytesBuff)
        {
            return MakeForBuff(bytesBuff, AdlerStart);
        }
        /// Equals determines whether two files (buffers) 
        /// have the same checksum value (identical).
        /// </summary>
        /// <param name="obj">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the value of checksum is the same
        /// as this instance; otherwise, false
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            AdlerChecksum other = (AdlerChecksum)obj;
            return (this.ChecksumValue == other.ChecksumValue);
        }
        /// <summary>
        /// operator== determines whether AdlerChecksum objects are equal.
        /// </summary>
        /// <param name="objA">A AdlerChecksum object for comparison</param>
        /// <param name="objB">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the values of its operands are equal</returns>
        public static bool operator ==(AdlerChecksum objA, AdlerChecksum objB)
        {
            if (Object.Equals(objA, null) && Object.Equals(objB, null)) return true;
            if (Object.Equals(objA, null) || Object.Equals(objB, null)) return false;
            return objA.Equals(objB);
        }
        /// <summary>
        /// operator!= determines whether AdlerChecksum objects are not equal.
        /// </summary>
        /// <param name="objA">A AdlerChecksum object for comparison</param>
        /// <param name="objB">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the values of its operands are not equal</returns>
        public static bool operator !=(AdlerChecksum objA, AdlerChecksum objB)
        {
            return !(objA == objB);
        }
        /// <summary>
        /// GetHashCode returns hash code for this instance.
        /// </summary>
        /// <returns>hash code of AdlerChecksum</returns>
        public override int GetHashCode()
        {
            return ChecksumValue.GetHashCode();
        }
        /// <summary>
        /// ToString is a method for current AdlerChecksum object
        /// representation in textual form.
        /// </summary>
        /// <returns>Returns current checksum or
        /// or "Unknown" if checksum value is unavailable 
        /// </returns>
        public override string ToString()
        {
            if (ChecksumValue != 0)
                return ChecksumValue.ToString();
            return "Unknown";
        }

    }
}
