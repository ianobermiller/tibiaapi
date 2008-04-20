using System;
using System.Runtime.InteropServices;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Represents a generic packet. Also contains helper methods for sending packets.
    /// </summary>
    public class Packet
    {
        protected byte[] data;
        protected PacketType type;
        protected PacketDestination destination;
        protected short specifiedLength;

        /// <summary>
        /// Default constructor.
        /// For derived classes, this should isntantiate a new data array
        /// so users can create new packets.
        /// </summary>
        public Packet()
        {

        }

        /// <summary>
        /// Create a new packet object and parse the specified data packet.
        /// </summary>
        /// <param name="data"></param>
        public Packet(byte[] data)
        {
            ParseData(data);
        }

        /// <summary>
        /// Parse the given data packet to create this object.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public bool ParseData(byte[] packet)
        {
            data = packet;
            if (data.Length > 3)
            {
                type = (PacketType)data[2];
                specifiedLength = BitConverter.ToInt16(data, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get/Set the unencrypted bytes associated with this packet object.
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Get/Set the type of the packet (specified in the third byte of the data).
        /// </summary>
        public PacketType Type
        {
            get { return type; }
            set
            {
                type = value;
                if (data != null && data.Length > 3)
                {
                    data[2] = (byte)type;
                }
            }
        }

        /// <summary>
        /// Get/Set the length (specified by the first two bytes of the data).
        /// </summary>
        public short Length
        {
            get
            {
                return specifiedLength;
            }
            set
            {
                specifiedLength = value;
                if (data != null && data.Length > 3)
                {
                    Array.Copy(BitConverter.GetBytes(value), data, 2);
                }
            }
        }

        /// <summary>
        /// Get/Set the packet destination
        /// </summary>
        public PacketDestination Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        #region Sending Packets with packet.dll
        [DllImport("packet.dll")]
        private static extern bool SendPacket(uint processID, byte[] packet, byte encrypt, byte safeArray);

        /// <summary>
        /// Send a packet through the client using encryption and packet.dll.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketWithDLL(Objects.Client client, Byte[] packet)
        {
            return SendPacketWithDLL(client, packet, true);
        }

        /// <summary>
        /// Send a packet through the client using packet.dll.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static bool SendPacketWithDLL(Objects.Client client, Byte[] packet, Boolean encrypt)
        {
            try
            {
                return SendPacket((uint)client.Process.Id, packet, Convert.ToByte(encrypt), 0);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Static Helper Methods
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

        /// <summary>Convert a string of hex digits (ex: E4 CA B2) to a byte array.</summary>
        /// <param name="s">The string containing the hex digits (with or without spaces).</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", string.Empty);
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        public static string HexStringToASCII(string value)
        {
            byte[] temp = HexStringToByteArray(value);
            string loc="";
            for (int i = 0; i < temp.Length; i++)
            {
                loc+=Tibia.Packets.Packet.HexByteToChar(temp[i]);
            }
            return loc;
        }
        /// <summary>
        /// Convert a byte to a char
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static char HexByteToChar(byte value)
        {
            if (value < 32 || value == 127 || value == 141 || value == 143 || value == 144 || value == 157)
            {
                return (char)'.';
            }
            else
            {
                return (char)value;
            }
        }
        
        /// <summary>
        /// Converts a char to a hex byte
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte CharToHexByte(char value)
        {
            return (byte)value;
        }

        /// <summary>
        /// Converts a integer to a hex string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHexString(int value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            return ByteArrayToHexString(temp,temp.Length);
        }

        /// <summary>
        /// Converts a hex string to a integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int HexStringToInt(string value)
        {
            return Int32.Parse(value, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data">The array of bytes to be translated into a string of hex digits.</param>
        /// <param name="length">The length of data to convert</param>
        /// <returns>Returns a well formatted string of hex digits with spacing.</returns>
        public static string ByteArrayToHexString(byte[] data, int length)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            for (int i = 0; i < length; i++)
                sb.Append(Convert.ToString(data[i], 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        /// <summary>Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data">The array of bytes to be translated into a string of hex digits.</param>
        /// <returns>Returns a well formatted string of hex digits with spacing.</returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            return ByteArrayToHexString(data, data.Length);
        }

        /// <summary>
        /// Convert a 4 byte IP Address to a string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string IPBytesToString(byte[] data, int index)
        {
            return "" + data[index] + "." + data[index + 1] + "." + data[index + 2] + "." + data[index + 3];
        }
        #endregion
    }
}
