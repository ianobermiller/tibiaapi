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
        private Item lastFound = null;

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
            // containers.Reverse();
            return containers;
        }

        /// <summary>
        /// Used after calling FindItem, gets the next item (eg. for stacking)
        /// </summary>
        /// <returns>FindNext().Found == false if nothing found.</returns>
        public Item FindNext()
        {
            if (lastFound != null)
            {
                Item item = null;
                List<Container> containers = GetContainers();
                foreach (Container c in containers)
                {
                    item = c.GetItems().Find(delegate(Item i)
                    {
                        return i.Id == lastFound.Id;
                    });
                    if (item != null)
                    {
                        if ((item.Loc.container == lastFound.Loc.container &&
                            item.Loc.position > lastFound.Loc.position) ||
                            (item.Loc.container > lastFound.Loc.container))
                        {
                            lastFound = item;
                        }
                    }
                }
            }
            else
            {
                lastFound.Found = false;
            }
            return lastFound;
        }

        /// <summary>
        /// Find an item in the player's inventory.
        /// </summary>
        /// <param name="match">A delegate that returns true when a matched item is found.</param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(Predicate<Item> match)
        {
            Item item = null;
            List<Container> containers = GetContainers();
            foreach (Container c in containers)
            {
                foreach (Item i in c.GetItems())
                {
                    if (match(i))
                    {
                        item = i;
                        lastFound = item;
                    }
                }
            }
            lastFound = (item == null ? new Item(client, false) : item);
            return lastFound;
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
        /// <param name="second">If true, return the second item found.</param>
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
                return new Item(
                    (uint)client.ReadInt(address + Addresses.Container.Distance_Item_Id),
                    client.ReadByte(address + Addresses.Container.Distance_Item_Count),
                    location,
                    client,
                    true);
            }
            return null;
        }

        public bool Stack(uint itemId)
        {
            Item first = null;
            Item second = null;
            List<Container> containers = GetContainers();
            foreach (Container c in containers)
            {
                foreach (Item i in c.GetItems())
                {
                    if (i.Id == itemId)
                    {
                        if (first == null)
                            first = i;
                        else
                        {
                            second = i;
                            break;
                        }
                    }
                }
            }
            if (first != null && second != null && first.Found && second.Found &&
                !(first.Loc.container == second.Loc.container && second.Count == 100))
            {
                second.Move(first);
                return true;
            }
            else
                return false;
        }
    }
}
