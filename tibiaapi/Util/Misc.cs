using System;
using System.Drawing;

namespace Tibia
{
    class Misc
    {
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
        public static byte[] CreateNopArray(int len)
        {
            byte[] ar = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ar[i] = Addresses.Client.Nop;
            }
            return ar;
        }
    }
}
