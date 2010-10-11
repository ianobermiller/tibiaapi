using System;
using System.Drawing;
using Tibia.Constants;

namespace Tibia.Objects
{
    [Serializable]
    public class DrawingItem
    {
        public int Surface = 1;
        public Point Position = Point.Empty;
        public int Size = 32;
        public uint Id = 100;
        public int Count = 0;
        public int ItemSubType = 0;
        public Color EdgeColor = Color.Black;
        public Rectangle ClippingRectangle = new Rectangle(0, 0, 32, 32);
        public DrawingItemTextFormat TextFormat = new DrawingItemTextFormat();

        public class DrawingItemTextFormat
        {
            public ClientFont Font = ClientFont.NormalBorder;
            public Color Color = Color.White;
            public int Alignment = 2;
            public int Force = 0;
        }
    }
}
