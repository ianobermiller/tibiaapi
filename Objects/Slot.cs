using System;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a slot in memory.
    /// </summary>
    public class Slot
    {
        public enum SlotNumber
        {
            None = 0,
            Head = 1,
            Necklace = 2,
            Backpack = 3,
            Armor = 4,
            Right = 5,
            Left = 6,
            Legs = 7,
            Feet = 8,
            Ring = 9,
            Ammo = 10
        }

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
        public Item getSlot(SlotNumber s)
        {
            Item item;
            uint address = Memory.Addresses.Player.Slot_Head + 12 * ((uint)s - 1);
            byte count = client.ReadByte(address + Memory.Addresses.Player.Distance_Slot_Count);
            uint id = (uint) client.ReadInt(address);
            if (id > 0)
            {
                item = new Item(id, count, new ItemLocation(s), true);
            }
            else
            {
                item = new Item(false);
            }
            return item;
        }
    }

    
}
