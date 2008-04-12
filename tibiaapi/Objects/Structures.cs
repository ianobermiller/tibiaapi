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

        public Location(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
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
