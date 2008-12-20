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
        /// Returns true of the provided location is adjacent to (touching)
        /// this location.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public bool IsAdjacentTo(Location loc)
        {
            return DistanceTo(loc) <= 1 &&
                loc.Z == Z;
        }

        /// <summary>
        /// Returns true of the provided location is close to (2sqm)
        /// this location.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public bool IsCloseTo(Location loc)
        {
            return DistanceTo(loc) <= 2 &&
                loc.Z == Z;
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
        /// Gets the distance between locations.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public int DistanceTo(Location l)
        {
            int xDist = Math.Abs(X - l.X);
            int yDist = Math.Abs(Y - l.Y);
            return (xDist > yDist ? xDist : yDist);
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

        public override bool Equals(object other)
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
}
