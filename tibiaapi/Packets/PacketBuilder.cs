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
        public const int MaxLength = 15360;
        private byte[] data;
        private PacketType type;
        private int index = 0;
        private Client client;

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

        /// <summary>
        /// Get/Set the current index in this packet. Set is the same as Seek(int).
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PacketBuilder(Client c)
        {
            client = c;
            data = new byte[MaxLength];
        }

        /// <summary>
        /// Start building a packet of the desired type.
        /// </summary>
        /// <param name="type"></param>
        public PacketBuilder(Client c, PacketType type)
            : this(c)
        {
            Type = type;
            index++;
        }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        public PacketBuilder(Client c, byte[] packet) : this(c, packet, 0, packet.Length) { }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start"></param>
        public PacketBuilder(Client c, byte[] packet, int start) : this(c, packet, start, packet.Length - start) { }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public PacketBuilder(Client c, byte[] packet, int start, int length)
            : this(c)
        {
            Array.Copy(packet, start, data, 0, length);
        }
        #endregion

        #region Add
        /// <summary>
        /// Add a byte at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int AddByte(byte b)
        {
            return AddBytes(new byte[] { b });
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b)
        {
            return AddBytes(b, b.Length);
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b, int length)
        {
            return AddBytes(b, 0, length);
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b, int sourceIndex, int length)
        {
            Array.Copy(b, sourceIndex, data, index, length);
            index += length;
            return length;
        }

        /// <summary>
        /// Add an "integer" (aka. ushort) at the current index and advance.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int AddInt(int i)
        {
            return AddBytes(BitConverter.GetBytes((ushort)i));
        }
        /// <summary>
        /// Add a "short" (aka. 2 byte int) at the current index and advance
        /// </summary>
        /// <param name="i">Value between -127 and 127</param>
        /// <returns></returns>
        public int AddShort(int i)
        {
            return AddBytes(BitConverter.GetBytes((short)i));
        }

        /// <summary>
        /// Add a "long" (aka. 4 byte int) at the current index and advance.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public int AddLong(int l)
        {
            return AddBytes(BitConverter.GetBytes(l));
        }

        /// <summary>
        /// Add the string length and the a string at the current index and advance.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int AddString(string s)
        {
            AddInt(s.Length);
            return AddBytes(Encoding.ASCII.GetBytes(s));
        }

        /// <summary>
        /// Add a location object at the current index and advance.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public int AddLocation(Location loc)
        {
            return AddBytes(loc.ToBytes());
        }

        /// <summary>
        /// Add an item location object at the current index and advance.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public int AddItemLocation(ItemLocation loc)
        {
            return AddBytes(loc.ToBytes());
        }

        /// <summary>
        /// Add an item at the current location and advance.
        /// Adds an extra byte if the count is significant (> 0).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddItem(Item item)
        {
            AddInt((int)item.Id);
            if (client.DatReader.GetItem(item.Id).HasExtraByte())
                AddByte(item.Count);
            return index;
        }
        #endregion

        #region Get
        /// <summary>
        /// Get the byte at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public byte GetByte()
        {
            return data[index++];
        }

        /// <summary>
        /// Get an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBytes(int length)
        {
            byte[] b = new byte[length];
            Array.Copy(data, index, b, 0, length);
            index += length;
            return b;
        }

        /// <summary>
        /// Get an "int" (aka. 2 byte unsigned short) at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public ushort GetInt()
        {
            ushort i = BitConverter.ToUInt16(data, index);
            index += 2;
            return i;
        }

        public short GetShort()
        {
            short i = BitConverter.ToInt16(data, index);
            index += 2;
            return i;
        }

        /// <summary>
        /// Get a "long" (aka. 4 byte integer) at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public int GetLong()
        {
            int l = BitConverter.ToInt32(data, index);
            index += 4;
            return l;
        }

        /// <summary>
        /// Get the string length and then the string at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            int length = GetInt();
            string s = Encoding.ASCII.GetString(data, index, length);
            index += length;
            return s;
        }

        /// <summary>
        /// Get the string  only at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public string GetString(int length)
        {
            string s = Encoding.ASCII.GetString(data, index, length);
            index += length;
            return s;
        }

        /// <summary>
        /// Get a location object at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public Location GetLocation()
        {
            Location loc = new Location();
            loc.X = GetInt();
            loc.Y = GetInt();
            loc.Z = GetByte();
            return loc;
        }

        /// <summary>
        /// Get an item location at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public ItemLocation GetItemLocation()
        {
            ItemLocation loc = null;
            byte tmp = PeekByte();
            if(tmp==0xFF)
            {
                Skip(2);
                byte tmp2=GetByte();
                if(tmp2>=0x40)
                {
                    Skip(1);
                    byte slot = GetByte();
                    loc = new ItemLocation(tmp2,slot);
                }else
                {
                    loc = new ItemLocation((Tibia.Constants.SlotNumber)tmp2);
                    Skip(2);
                }
            }
            else
            {
                loc = new ItemLocation(GetLocation());     
            }
            return loc;
        }

        /// <summary>
        /// Get an item from the current index and advance.
        /// Requires a DatReader object to check if the item has an extra byte.
        /// </summary>
        /// <returns></returns>
        public Item GetItem()
        {
            Item item = new Item(GetInt());
            if (client.DatReader.GetItem(item.Id).HasExtraByte())
                item.Count = GetByte();
            item.Client = client;
            return item;
        }

        /// <summary>
        /// Get the completed packet with the two byte length header attached.
        /// </summary>
        /// <returns></returns>
        public byte[] GetPacket()
        {
            byte[] b = new byte[index + 2];
            Array.Copy(BitConverter.GetBytes((short)index), b, 2);
            Array.Copy(data, 0, b, 2, index);
            return b;
        }
        #endregion

        #region Peek
        /// <summary>
        /// Get the byte at the current index.
        /// </summary>
        /// <returns></returns>
        public byte PeekByte()
        {
            return data[index++];
        }

        /// <summary>
        /// Get an array of bytes at the current index.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] PeekBytes(int length)
        {
            byte[] b = new byte[length];
            Array.Copy(data, index, b, 0, length);
            return b;
        }

        /// <summary>
        /// Get an "int" (aka. 2 byte unsigned short) at the current index.
        /// </summary>
        /// <returns></returns>
        public ushort PeekInt()
        {
            ushort i = BitConverter.ToUInt16(data, index);
            return i;
        }

        /// <summary>
        /// Get a "long" (aka. 4 byte integer) at the current index.
        /// </summary>
        /// <returns></returns>
        public int PeekLong()
        {
            int l = BitConverter.ToInt32(data, index);
            return l;
        }

        /// <summary>
        /// Get a string at the current index.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string PeekString(int length)
        {
            string s = Encoding.ASCII.GetString(data, index, length);
            return s;
        }

        /// <summary>
        /// Get a location object at the current index.
        /// </summary>
        /// <returns></returns>
        public Location PeekLocation()
        {
            Location loc = new Location();
            loc.X = GetInt();
            loc.Y = GetInt();
            loc.Z = GetByte();
            index -= 5;
            return loc;
        }
        #endregion

        #region Control
        /// <summary>
        /// Move the index to the specified value. Same as setting Index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Seek(int index)
        {
            this.index = index;
            return index;
        }

        /// <summary>
        /// Skip the index ahead the specified amount of bytes.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int Skip(int length)
        {
            index += length;
            return index;
        }
        #endregion
    }
}