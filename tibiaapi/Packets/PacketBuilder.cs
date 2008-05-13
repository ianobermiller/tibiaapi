using System;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Class for building and parsing packets.
    /// </summary>
    public class PacketBuilder
    {
        public const int MaxLength = 8096;
        private byte[] data;
        private PacketType type;
        private int index = 0;

        #region Properties
        /// <summary>
        /// Get/Set the unencrypted bytes associated with this packetbuilder object.
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
                    data[0] = (byte)type;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PacketBuilder()
        {
            data = new byte[MaxLength];
        }

        /// <summary>
        /// Start building a packet of the desired type.
        /// </summary>
        /// <param name="type"></param>
        public PacketBuilder(PacketType type) : this()
        {
            Type = type;
            index++;
        }

        /// <summary>
        /// Start parsing the given packet (do not include the first two length bytes).
        /// </summary>
        /// <param name="packet"></param>
        public PacketBuilder(byte[] packet) : this(packet, 0, packet.Length) { }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public PacketBuilder(byte[] packet, int start, int length) : this()
        {
            Array.Copy(packet, start, data, 0, length);
        }
        #endregion

        #region Add
        public int AddByte(byte b)
        {
            return AddBytes(new byte[] { b });
        }

        public int AddBytes(byte[] b)
        {
            return AddBytes(b, b.Length);
        }

        public int AddBytes(byte[] b, int length)
        {
            Array.Copy(b, 0, data, index, length);
            index += length;
            return length;
        }

        public int AddInt(int i)
        {
            return AddBytes(BitConverter.GetBytes((short)i));
        }

        public int AddLong(int l)
        {
            return AddBytes(BitConverter.GetBytes(l));
        }

        public int AddString(string s)
        {
            return AddString(s, s.Length);
        }

        public int AddString(string s, int length)
        {
            return AddBytes(Encoding.ASCII.GetBytes(s));
        }
        #endregion

        #region Get
        public byte GetByte()
        {
            return data[index++];
        }

        public byte[] GetBytes(int length)
        {
            byte[] b = new byte[length];
            Array.Copy(data, index, b, 0, length);
            index += length;
            return b;
        }

        public int GetInt()
        {
            int i = BitConverter.ToInt16(data, index);
            index += 2;
            return i;
        }

        public int GetLong()
        {
            int l = BitConverter.ToInt32(data, index);
            index += 4;
            return l;
        }

        public string GetString(int length)
        {
            string s = Encoding.ASCII.GetString(data, index, length);
            index += length;
            return s;
        }

        public byte[] GetPacket()
        {
            byte[] b = new byte[index];
            Array.Copy(BitConverter.GetBytes((short)index), b, 2);
            Array.Copy(data, 0, b, 2, index);
            return b;
        }
        #endregion
    }
}
