using System;
using System.Collections.Generic;
using Tibia.Packets;
using System.Text.RegularExpressions;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents the player's inventory.
    /// </summary>
    public class Inventory
    {
        protected Client client;
        protected Item lastFound = null;

        /// <summary>
        /// Create a new inventory object with the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public Inventory(Client client)
        {
            this.client = client;
        }

        public Container GetContainer(byte number)
        {
            uint i = Addresses.Container.Start + (number * Addresses.Container.Step_Container);
            if (client.ReadByte(i + Addresses.Container.Distance_IsOpen) == 1)
            {
                return new Container(client, i, number);
            }
            return null;
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
                {
                    containers.Add(new Container(client, i, containerNumber));
                }
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
        /// <param name="checkSlot">If true, also checks the player's slots for the item.</param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(Predicate<Item> match, bool checkSlot)
        {
            Item item = null;

            // Check slots first (if applicable)
            if (checkSlot)
            {
                item = FindItemInSlot(match);
                if (item.Found)
                {
                    lastFound = item;
                    return lastFound;
                }
            }

            // Then check containers
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
            }, true);
        }

        /// <summary>
        /// Find an item in the player's inventory by its id.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="checkSlot">If true, also checks the player's slots for the item.</param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(uint itemId, bool checkSlot)
        {
            return FindItem(delegate(Item i)
            {
                return i.Id == itemId;
            }, checkSlot);
        }

        /// <summary>
        /// Find an item in the player's inventory by its id.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(Item item)
        {
            return FindItem(item.Id);
        }

        /// <summary>
        /// Find an item in the player's inventory by its id.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="checkSlot">If true, also checks the player's slots for the item.</param>
        /// <returns>Item object describing the item and its location.</returns>
        public Item FindItem(Item item, bool checkSlot)
        {
            return FindItem(item.Id, checkSlot);
        }

        /// <summary>
        /// Find an item from a list in the player's inventory. Ex. findItem(new Tibia.Contstants.ItemList.Food()).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public Item FindItem<T>(ICollection<T> list) where T : Item
        {
            return FindItem(delegate(Item i)
            {
                return i.IsInList(list);
            }, true);
        }

        /// <summary>
        /// Find an item from a list in the player's inventory. Ex. findItem(new Tibia.Contstants.ItemList.Food()).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="checkSlot">If true, also checks the player's slots for the item.</param>
        public Item FindItem<T>(ICollection<T> list, bool checkSlot) where T : Item
        {
            return FindItem(delegate(Item i)
            {
                return i.IsInList(list);
            }, checkSlot);
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
            if (location.type == Tibia.Constants.ItemLocationType.Slot)
            {
                return GetSlot(location.slot);
            }
            else if (location.type == Tibia.Constants.ItemLocationType.Container)
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

        /// <summary>
        /// Stacks the first two items that are found and are stackable.
        /// </summary>
        /// <param name="itemId">Id of the items to be stacked.</param>
        /// <returns>True if successful and if it stacked something.</returns>
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
                first.Loc.container == second.Loc.container && !(second.Count == 100))
            {
                second.Move(first);
                System.Threading.Thread.Sleep(100);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// If you just want to use an item, like eat a food, or check gp etc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItem(uint id)
        {
            return client.Send(Packets.ItemUsePacket.Create(client, ItemLocation.Hotkey(), id, 0, 0x0F));
        }

        /// <summary>
        /// If you just want to use an item, like eat a food, or check gp etc
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UseItem(Item item)
        {
            return UseItem(item.Id);
        }

        /// <summary>
        /// Use a item on a creature, like sd'ing your enemy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public bool UseItem(uint id, Creature onCreature)
        {
            return UseItem(id, onCreature.Id);
        }

        /// <summary>
        /// Use a item on a creature, like sd'ing your enemy.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public bool UseItem(Item item, Creature onCreature)
        {
            return UseItem(item.Id, onCreature.Id);
        }

        /// <summary>
        /// Use an item on your self
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItemOnSelf(uint id)
        {
            return UseItem(id, client.ReadInt(Addresses.Player.Id));
        }

        /// <summary>
        /// Use an item on your self
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UseItemOnSelf(Item item)
        {
            return UseItem(item.Id, client.ReadInt(Addresses.Player.Id));
        }

        /// <summary>
        /// Use an item on a creature of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatureId"></param>
        /// <returns></returns>
        public bool UseItem(uint id, int creatureId)
        {
            byte stack = 0;
            byte count = 0;
            if (id == Constants.Items.Bottle.Vial.Id) stack = count;
            return client.Send(Packets.ItemUseBattlelistPacket.Create(
                client, ItemLocation.Hotkey(), id, stack, creatureId));
        }

        /// <summary>
        /// Use an item on a creature of the given id
        /// </summary>
        /// <param name="item"></param>
        /// <param name="creatureId"></param>
        /// <returns></returns>
        public bool UseItem(Item item, int creatureId)
        {
            return UseItem(item.Id, creatureId);
        }

        /// <summary>
        /// Use an item on a location (tile).
        /// Automatically finds the tile information.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onLocation"></param>
        /// <returns></returns>
        public bool UseItem(uint id, Location onLocation)
        {
            MapSquare square = client.Map.CreateMapSquare(onLocation);
            return UseItem(id, square.Tile);
        }

        /// <summary>
        /// Use an item on a location (tile).
        /// Automatically finds the tile information.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onLocation"></param>
        /// <returns></returns>
        public bool UseItem(Item item, Location onLocation)
        {
            return UseItem(item.Id, onLocation);
        }

        /// <summary>
        /// Use an item on a tile (eg. fishing)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool UseItem(uint id, Tile onTile)
        {
            return client.Send(Packets.ItemUseOnPacket.Create(
                client, ItemLocation.Hotkey(), id, 0, onTile.Location, onTile.Id, 0));
        }

        /// <summary>
        /// Use an item on a tile (eg. fishing)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool UseItem(Item item, Tile onTile)
        {
            return UseItem(item.Id, onTile);
        }

        /// <summary>
        /// Get the item in the specified slot.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Item GetSlot(Constants.SlotNumber s)
        {
            Item item;
            uint address = Addresses.Player.Slot_Head + 12 * ((uint)s - 1);
            byte count = client.ReadByte(address + Addresses.Player.Distance_Slot_Count);
            uint id = (uint)client.ReadInt(address);
            if (id > 0)
            {
                item = new Item(id, count, new ItemLocation(s), client, true);
            }
            else
            {
                item = new Item(client, false);
            }
            return item;
        }

        /// <summary>
        /// Search the equipment slots for an item with the specified id
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public Item FindItemInSlot(Predicate<Item> match)
        {
            Item item = null;
            uint address = Addresses.Player.Slot_Head;
            for (int i = 0; i < Addresses.Player.Max_Slots; i++, address += 12)
            {
                item = new Item(
                    Convert.ToUInt32(client.ReadInt(address)), 
                    client.ReadByte(address +  + Addresses.Player.Distance_Slot_Count), 
                    new ItemLocation((Constants.SlotNumber) i), 
                    client, 
                    false);
                if (match(item))
                {
                    item.Found = true;
                    return item;
                }
            }
            return item;
        }

        /// <summary>
        /// Number the containers using the default format "[#] ContainerName"
        /// </summary>
        public void NumberContainers()
        {
            NumberContainers("[\\#]");
        }

        /// <summary>
        /// Number the containers using a custom prefix.
        /// Insert "\#" wherever you want the number to appear.
        /// </summary>
        /// <param name="format"></param>
        public void NumberContainers(string format)
        {
            foreach (Container c in GetContainers())
            {
                if (!new Regex("[a-zA-Z]").IsMatch(c.Name.Substring(0, 1)))
                {
                    int index = c.Name.IndexOf(" ") + 1;
                    c.Name = c.Name.Substring(index);
                }
                c.Rename(format.Replace("\\#", c.Number.ToString()) + " " + c.Name);
            }
        }

        /// <summary>
        /// Gets the client associated with this object.
        /// </summary>
        public Client Client
        {
            get { return client; }
        }
    }
}
