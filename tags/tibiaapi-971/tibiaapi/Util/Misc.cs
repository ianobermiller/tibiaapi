using System;
using System.Drawing;

namespace Tibia
{
    public class Misc
    {
        /// <summary>
        /// Obtains the appropiate System.Drawing.Color representation given the health percentage of a creature
        /// </summary>
        /// <param name="hp">Health percent of the creature</param>
        /// <returns>A System.Drawing.Color instance representing the color</returns>
        public static Color HpPercentToColor(int hp)
        {
            int color;
            if (hp >= 95)
                color = 0x00CC00;
            else if (hp >= 60)
                color = 0x60C060;
            else if (hp >= 30)
                color = 0xC0C000;
            else if (hp >= 10)
                color = 0xC03030;
            else if (hp >= 4)
                color = 0xBF0000;
            else if (hp >= 0)
                color = 0x5F0000;
            else
                color = 0;
            return Color.FromArgb((int) (color + 0xFF000000));
        }

        public static Color GetAutomapColor(int i)
        {
            switch (i)
            {
                case 0x0C: // Foliage, dark green
                    return Color.FromArgb(0, 0x66, 0);
                case 0x18: // Grass, green
                    return Color.FromArgb(0, 0xcc, 0);
                case 0x1e: // Swamp, bright green
                    return Color.FromArgb(0, 0xFF, 0);
                case 0x28: // Water, blue
                    return Color.FromArgb(0x33, 0, 0xcc);
                case 0x56: // Stone wall, dark grey
                    return Color.FromArgb(0x66, 0x66, 0x66);
                case 0x72: // Not sure, maroon
                    return Color.FromArgb(0x99, 0x33, 0);
                case 0x79: // Dirt, brown
                    return Color.FromArgb(0x99, 0x66, 0x33);
                case 0x81: // Paths, tile floors, other floors
                    return Color.FromArgb(0x99, 0x99, 0x99);
                case 0xB3: // Ice, light blue
                    return Color.FromArgb(0xcc, 0xff, 0xff);
                case 0xBA: // Walls, red
                    return Color.FromArgb(0xff, 0x33, 0);
                case 0xC0: // Lava, orange
                    return Color.FromArgb(0xff, 0x66, 0);
                case 0xCF: // Sand, tan
                    return Color.FromArgb(0xff, 0xcc, 0x99);
                case 0xD2: // Ladder, yellow
                    return Color.FromArgb(0xff, 0xff, 0);
                case 0: // Nothing, black
                    return Color.Black;
                default: // Unknown, white
                    //this.Text = "" + b;
                    return Color.White;
            }
        }

        /// <summary>
        /// Creates a byte array of No Operation machine instructions (NOPs).
        /// </summary>
        /// <param name="len">Length of the byte array</param>
        /// <returns>A byte array consisting of NOPs</returns>
        public static byte[] CreateNopArray(int len)
        {
            
            byte[] ar = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ar[i] = 0x90;
            }
            return ar;
        }
    }
}
