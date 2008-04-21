using System;
using System.Text;

namespace Tibia.Objects
{
    /// <summary>
    /// A simple X, Y, Z location
    /// </summary>
    public struct Location
    {
        public int X, Y, Z;

        /// <summary>
        /// Create a new location given the coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Location(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Convert the location to bytes to be placed into a packet.
        /// </summary>
        /// <returns>A byte array of length 5 containing the coordinates.</returns>
        public byte[] ToBytes()
        {
            byte[] bytes = new byte[5];
            Array.Copy(BitConverter.GetBytes((short)X), 0, bytes, 0, 2);
            Array.Copy(BitConverter.GetBytes((short)Y), 0, bytes, 2, 2);
            bytes[4] = (byte)Z;
            return bytes;
        }

        /// <summary>
        /// Get and invalid instance of this struct. Used for function overloading.
        /// </summary>
        /// <returns></returns>
        public static Location GetInvalid()
        {
            return new Location(-1, -1, -1);
        }

        /// <summary>
        /// Checks if this struct is valid (all coordinates non-negative).
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return X >= 0 &&
                Y >= 0 &&
                Z >= 0;
        }

        /// <summary>
        /// Returns the string representation of this struct.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(16);
            builder.AppendLine("X = " + X.ToString());
            builder.AppendLine("Y = " + Y.ToString());
            builder.AppendLine("Z = " + Z.ToString());

            return builder.ToString();
        }

        public override bool  Equals(object other)
        {
            return other is Location && Equals((Location)other);
        }

        public bool Equals(Location other)
        {
            return other.X == X && 
                other.Y == Y && 
                other.Z == Z;
        }
    }

    /// <summary>
    /// Represents a tile in the map structure
    /// </summary>
    public struct Tile
    {
        public Location Location;
        public uint Number;
        public uint Address;
        public uint Id;

        public Tile(uint n) : this(n, 0) { }
        public Tile(uint n, uint a) : this(n, a, new Location(), 0) { }
        public Tile(uint n, uint a, Location l, uint i)
        {
            Location = l;
            Number = n;
            Address = a;
            Id = i;
        }

        /// <summary>
        /// Returns the string representation of this struct.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(16);
            builder.AppendLine("Number = " + Number.ToString());
            builder.AppendLine("Address = " + Address.ToString());
            builder.AppendLine("Id = " + Id.ToString());

            return builder.ToString();
        }
    }

    /// <summary>
    /// Represents a Login Server
    /// </summary>
    public struct LoginServer
    {
        public string Server;
        public short Port;

        public LoginServer(string server, short port)
        {
            Server = server;
            Port = port;
        }

        public override string ToString()
        {
            return Server + ":" + Port;
        }
    }
}
