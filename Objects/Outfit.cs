using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Objects
{
    public class Outfit
    {
        public ushort LookType { get; set; }
        public ushort LookItem { get; set; }
        public byte Head { get; set; }
        public byte Body { get; set; }
        public byte Legs { get; set; }
        public byte Feet { get; set; }
        public byte Addons { get; set; }

        public Outfit(ushort looktype, ushort lookitem)
        {
            LookItem = lookitem;
            LookType = looktype;
        }

        public Outfit(ushort looktype, byte head, byte body, byte legs, byte feet, byte addons)
        {
            LookType = looktype;
            Head = head;
            Body = body;
            Legs = legs;
            Feet = feet;
            Addons = addons;
        }

        public byte[] ToByteArray()
        {
            byte[] temp;

            if (LookType != 0)
            {
                temp = new byte[7];
                Array.Copy(BitConverter.GetBytes(LookType), temp, 2);
                temp[2] = Head;
                temp[3] = Body;
                temp[4] = Legs;
                temp[5] = Feet;
                temp[6] = Addons;
            }
            else
            {
                temp = new byte[4];
                Array.Copy(BitConverter.GetBytes(LookType), temp, 2);
                Array.Copy(BitConverter.GetBytes(LookItem), 0, temp, 2, 2);
            }

            return temp;
        }
    }
}
