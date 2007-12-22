using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents the player's inventory.
    /// </summary>
    public class Inventory
    {
        private Client client;

        /// <summary>
        /// Create a new inventory object with the specified client.
        /// </summary>
        /// <param name="c"></param>
        public Inventory(Client c)
        {
            client = c;
        }

        /// <summary>
        /// Return a list of all the containers open in the inventory. Use getContainers().Count to find how many are open.
        /// </summary>
        /// <returns></returns>
        public List<Container> GetContainers()
        {
            byte containerNumber = 0;
            List<Container> containers = new List<Container>();
            for (uint i = Addresses.Container.Start; i < Addresses.Container.End; i += Addresses.Container.Step_Container)
            {
                if (client.ReadByte(i + Addresses.Container.Distance_IsOpen) == 1)
                    containers.Add(new Container(client, i, containerNumber));
                containerNumber++;
            }
            return containers;
        }

        /// <summary>
        /// Find an item in the player's inventory.
        /// </summary>
        /// <param name="match">A delegate that returns true when a matched item is found.</param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(Predicate<Item> match)
        {
            Item item = new Item(client, false);
            List<Container> containers = GetContainers();
            foreach (Container c in containers)
            {
                item = c.GetItems().Find(match);
                if (item != null) return item;
            }
            return item;
        }

        /// <summary>
        /// Find an item in the player's inventory by its id.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(uint itemId)
        {
            return FindItem(delegate(Item i)
            {
                return i.Id == itemId;
            });
        }

        /// <summary>
        /// Find an item from a list in the player's inventory. Ex. findItem(new Tibia.Contstants.ItemList.Food()).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem<T>(List<T> list) where T : Item
        {
            return FindItem(delegate(Item i)
            {
                return i.IsInList(list);
            });
        }

        /// <summary>
        /// Get the item at the specified location.
        /// TODO: Add functionality for ItemLocationType.Ground 
		  /// (would interface with map reading, which has yet to be completed)
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Item GetItem(ItemLocation location)
        {
            if (location.type == Tibia.Constants.ItemLocationType.SLOT)
            {
                return client.GetSlot(location.slot);
            }
            else if (location.type == Tibia.Constants.ItemLocationType.CONTAINER)
            {
                long address = Addresses.Container.Start +
                              Addresses.Container.Step_Container * (int)location.container +
                              Addresses.Container.Step_Slot * (int)location.slot;
                return new Item((uint)client.ReadInt(address + Addresses.Container.Distance_Item_Id),
                                     client.ReadByte(address + Addresses.Container.Distance_Item_Count),
                                     location, true);
            }
            return null;
        }
    }
}
