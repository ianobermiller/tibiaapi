using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Objects
{
    public class Hotkey
    {
        protected Client client;
        protected uint address;
        protected byte number;

        /// <summary>
        /// Create a new container object with the specified client, address, and number.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="n"></param>
        public Hotkey(Client c, uint a, byte n)
        {
            client = c;
            address = a;
            number = n;
        }
    }
}
