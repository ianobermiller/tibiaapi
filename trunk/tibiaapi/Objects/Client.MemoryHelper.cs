using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class MemoryHelper
        {
            private Client client;

            internal MemoryHelper(Client client) { this.client = client; }

            public byte[] ReadBytes(long address, uint bytesToRead)
            {
                return Util.Memory.ReadBytes(client.ProcessHandle, address, bytesToRead);
            }

            public byte ReadByte(long address)
            {
                return Util.Memory.ReadByte(client.ProcessHandle, address);
            }

            public short ReadInt16(long address)
            {
                return Util.Memory.ReadInt16(client.ProcessHandle, address);
            }

            public ushort ReadUInt16(long address)
            {
                return Util.Memory.ReadUInt16(client.ProcessHandle, address);
            }

            public int ReadInt32(long address)
            {
                return Util.Memory.ReadInt32(client.ProcessHandle, address);
            }

            public uint ReadUInt32(long address)
            {
                return Util.Memory.ReadUInt32(client.ProcessHandle, address);
            }

            public double ReadDouble(long address)
            {
                return Util.Memory.ReadDouble(client.ProcessHandle, address);
            }

            public string ReadString(long address)
            {
                return Util.Memory.ReadString(client.ProcessHandle, address);
            }

            public string ReadString(long address, uint length)
            {
                return Util.Memory.ReadString(client.ProcessHandle, address, length);
            }

            public bool WriteBytes(long address, byte[] bytes, uint length)
            {
                return Util.Memory.WriteBytes(client.ProcessHandle, address, bytes, length);
            }

            public bool WriteInt16(long address, short value)
            {
                return Util.Memory.WriteInt16(client.ProcessHandle, address, value);
            }

            public bool WriteUInt16(long address, ushort value)
            {
                return Util.Memory.WriteUInt16(client.ProcessHandle, address, value);
            }

            public bool WriteInt32(long address, int value)
            {
                return Util.Memory.WriteInt32(client.ProcessHandle, address, value);
            }

            public bool WriteUInt32(long address, uint value)
            {
                return Util.Memory.WriteUInt32(client.ProcessHandle, address, value);
            }

            public bool WriteDouble(long address, double value)
            {
                return Util.Memory.WriteDouble(client.ProcessHandle, address, value);
            }

            public bool WriteByte(long address, byte value)
            {
                return Util.Memory.WriteByte(client.ProcessHandle, address, value);
            }

            public bool WriteString(long address, string str)
            {
                return Util.Memory.WriteString(client.ProcessHandle, address, str);
            }

            public bool WriteStringNoEncoding(long address, string str)
            {
                return Util.Memory.WriteStringNoEncoding(client.ProcessHandle, address, str);
            }
        }
    }
}
