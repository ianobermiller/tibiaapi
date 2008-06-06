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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Convert the location to bytes to be placed into a packet.
        /// </summary>
        /// <returns>A byte array of length 5 containing the coordinates.</returns>
        public byte[] ToBytes()
        {
            byte[] bytes = new byte[5];
            Array.Copy(BitConverter.GetBytes((ushort)X), 0, bytes, 0, 2);
            Array.Copy(BitConverter.GetBytes((ushort)Y), 0, bytes, 2, 2);
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
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        /// <summary>
        /// Return a Location structure from a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Location Parse(string s)
        {
            try
            {
                string replace = "~!@#$%^&*()_+`-={}|:\"<>?[]\\;',./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                foreach (char c in replace)
                {
                    s = s.Replace(c, ' ');
                }
                s = s.Trim();
                while (s.IndexOf("  ") != -1)
                    s = s.Replace("  ", " ");
                string[] split = s.Split(" ".ToCharArray());
                Location loc = new Location(
                    int.Parse(split[0]),
                    int.Parse(split[1]),
                    int.Parse(split[2])
                );
                return loc;
            }
            catch
            {
                return Location.GetInvalid();
            }
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
    public class LoginServer
    {
        public string Server = string.Empty;
        public short Port = 7171;

        public LoginServer() { }

        public LoginServer(string server)
        {
            Server = server;
        }

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

    public struct Channel
    {
        public Packets.ChatChannel Id;
        public string Name;

        public Channel(Packets.ChatChannel id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public struct Position
    {
        private int top;
        private int bottom;
        private int left;
        private int right;
        private int width;
        private int height;
        public int Top
        {
            get { return top; }
        }
        public int Bottom
        {
            get { return bottom; }
        }
        public int Left
        {
            get { return left; }
        }
        public int Rigth
        {
            get { return right; }
        }
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }
        public Position(Util.WinApi.RECT r)
        {
            top = r.top;
            bottom = r.bottom;
            left = r.left;
            right = r.right;
            width = r.right - r.left;
            height = r.bottom - r.top;
        }
    }
}
