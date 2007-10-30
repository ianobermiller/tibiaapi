using System;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a slot in memory.
    /// </summary>
    public class Slot
    {
        private Client client;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="c">client</param>
        public Slot(Client c)
        {
            client = c;
        }

        /// <summary>
        /// Get the item in the specified slot. Is also implemented as a helper method in Objects.Client for ease of use.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Item GetSlot(Constants.SlotNumber s)
        {
            Item item;
            uint address = Addresses.Player.Slot_Head + 12 * ((uint)s - 1);
            byte count = client.readByte(address + Addresses.Player.Distance_Slot_Count);
            uint id = (uint) client.readInt(address);
            if (id > 0)
            {
                item = new Item(id, count, new ItemLocation(s), true);
            }
            else
            {
                item = new Item(client, false);
            }
            return item;
        }
    }

    
}
