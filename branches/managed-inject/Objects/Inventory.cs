using System;
using System.Linq;
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
        private Client client;

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
            if (number < 0 || number > Addresses.Container.MaxContainers)
                throw new ArgumentOutOfRangeException("number", "number must be between 0 and Addresses.Container.MaxContainers");
            
            uint i = Addresses.Container.Start + (number * Addresses.Container.StepContainer);
            if (client.Memory.ReadByte(i + Addresses.Container.DistanceIsOpen) == 1)
            {
                return new Container(client, i, number);
            }
            return null;
        }

        /// <summary>
        /// Return a list of all the containers open in the inventory. Use getContainers().Count to find how many are open.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Container> GetContainers()
        {
            byte containerNumber = 0;
            for (uint i = Addresses.Container.Start; i < Addresses.Container.End; i += Addresses.Container.StepContainer)
            {
                if (client.Memory.ReadByte(i + Addresses.Container.DistanceIsOpen) == 1)
                {
                    yield return new Container(client, i, containerNumber);
                }
                containerNumber++;
            }
        }

        public IEnumerable<Item> GetItems()
        {
            return GetSlotItems().Union(GetContainerItems());
        }

        public IEnumerable<Item> GetContainerItems()
        {
            foreach (Container c in GetContainers())
            {
                foreach (Item i in c.GetItems())
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Get the item at the specified location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Item GetItem(ItemLocation location)
        {
            if (location.Type == Tibia.Constants.ItemLocationType.Slot)
            {
                return GetItemInSlot(location.Slot);
            }
            else if (location.Type == Tibia.Constants.ItemLocationType.Container)
            {
                long address = Addresses.Container.Start +
                              Addresses.Container.StepContainer * (int)location.ContainerId +
                              Addresses.Container.StepSlot * (int)location.Slot;
                return new Item(client,
                    client.Memory.ReadUInt32(address + Addresses.Container.DistanceItemId),
                    client.Memory.ReadByte(address + Addresses.Container.DistanceItemCount),
                    "", location);
            }
            return null;
        }

        /// <summary>
        /// Get the item in the specified slot.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Item GetItemInSlot(Constants.SlotNumber s)
        {
            uint address = Addresses.Player.SlotHead + 12 * ((uint)s - 1);
            uint id = client.Memory.ReadUInt32(address);
            if (id > 0)
            {
                byte count = client.Memory.ReadByte(address + Addresses.Player.DistanceSlotCount);
                return new Item(client, id, count, "", ItemLocation.FromSlot(s));
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Item> GetSlotItems()
        {
            uint address = Addresses.Player.SlotHead;
            for (int i = 0; i < Addresses.Player.MaxSlots; i++, address += 12)
            {
                uint id = client.Memory.ReadUInt32(address);
                if (id > 0)
                {
                    yield return new Item(client,
                        id,
                        client.Memory.ReadByte(address + +Addresses.Player.DistanceSlotCount), "",
                        ItemLocation.FromSlot((Constants.SlotNumber)i));
                }
            }
        }

        /// <summary>
        /// If you just want to use an item, like eat a food, or check gp etc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItem(uint id)
        {
            return Packets.Outgoing.ItemUsePacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, 0x0F);
        }

        /// <summary>
        /// Use an item on your self
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItemOnSelf(uint id)
        {
            return UseItemOnCreature(id, 0, client.Memory.ReadInt32(Addresses.Player.Id));
        }

        /// <summary>
        /// Use an item on a creature of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatureId"></param>
        /// <returns></returns>
        public bool UseItemOnCreature(uint id, byte stack, int creatureId)
        {
            return Packets.Outgoing.ItemUseBattlelistPacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, stack, (uint)creatureId);
        }

        /// <summary>
        /// Use an item on a tile (eg. fishing)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool UseItemOnTile(uint id, Tile onTile)
        {
            return Packets.Outgoing.ItemUseOnPacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Ground.Id, 0);
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
