using System;

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
    }
}
