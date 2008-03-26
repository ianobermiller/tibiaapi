using System;

namespace Tibia.Objects
{
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

    public struct Tile
    {
        public Location Location;
        public uint Id;
        public uint Address;
        public uint Object;

        public Tile(uint i) : this(i, 0) { }
        public Tile(uint i, uint a) : this(i, a, new Location(),0) { }
        public Tile(uint i, uint a, Location l, uint o)
        {
            Location = l;
            Id = i;
            Address = a;
            Object = o;
        }
    }
}
