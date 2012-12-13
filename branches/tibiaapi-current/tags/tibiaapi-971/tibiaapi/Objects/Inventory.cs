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
            if (number < 0 || number > client.Addresses.Container.MaxContainers)
                throw new ArgumentOutOfRangeException("number", "number must be between 0 and client.Addresses.Container.MaxContainers");
            
            uint i = client.Addresses.Container.Start + (number * client.Addresses.Container.StepContainer);
            if (client.Memory.ReadByte(i + client.Addresses.Container.DistanceIsOpen) == 1)
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
            return Enumerable.Range(0, (int)client.Addresses.Container.MaxContainers)
                    .Select(containerNumber => new
                    {
                        Address = (uint)(client.Addresses.Container.Start + containerNumber * client.Addresses.Container.StepContainer),
                        Number = (byte)containerNumber,
                    })
                    .Where(pair => client.Memory.ReadByte(pair.Address + client.Addresses.Container.DistanceIsOpen) == 1)
                    .Select(pair => new Container(client, pair.Address, pair.Number));

        }

        public IEnumerable<Item> GetItems()
        {
            return GetSlotItems().Concat(GetContainerItems());
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
                long address = client.Addresses.Container.Start +
                              client.Addresses.Container.StepContainer * (int)location.ContainerId +
                              client.Addresses.Container.DistanceContainerSlotsBegin+
                              client.Addresses.Container.StepSlot * (int)location.Slot;
                return new Item(client,
                    client.Memory.ReadUInt32(address + client.Addresses.Container.DistanceContainerSlotItemId),
                    client.Memory.ReadByte(address + client.Addresses.Container.DistanceContainerSlotItemCount),
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
            uint address = client.Addresses.Player.SlotBegin + client.Addresses.Player.SlotStep * (10 - (uint)s);
            uint id = client.Memory.ReadUInt32(address+ client.Addresses.Player.DistanceSlotId);
            if (id > 0)
            {
                byte count = client.Memory.ReadByte(address + client.Addresses.Player.DistanceSlotCount);
                return new Item(client, id, count, "", ItemLocation.FromSlot(s));
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Item> GetSlotItems()
        {
            return Enumerable.Range(0, client.Addresses.Player.MaxSlots)
                    .Select(slotNumber => new
                    {
                        Address = (uint)(client.Addresses.Player.SlotBegin + slotNumber * client.Addresses.Player.SlotStep),
                        Number = slotNumber
                    })
                    .Select(slot => new
                    {
                        ItemID = client.Memory.ReadUInt32(slot.Address + client.Addresses.Player.DistanceSlotId),
                        ItemCount = client.Memory.ReadByte(slot.Address + client.Addresses.Player.DistanceSlotCount),
                        Position = slot.Number
                    })
                    .Where(ItemSlot => ItemSlot.ItemID > 0)
                    .Select(ItemSlot => new Item(
                        client,
                        ItemSlot.ItemID,
                        ItemSlot.ItemCount,
                        "",
                        ItemLocation.FromSlot((Constants.SlotNumber)10-ItemSlot.Position)));
        }

        /// <summary>
        /// If you just want to use an item, like eat a food, or check gp etc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItem(uint id)
        {
            return Packets.Outgoing.UseItemPacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, 0x0F);
        }

        /// <summary>
        /// Use an item on your self
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UseItemOnSelf(uint id)
        {
            return UseItemOnCreature(id, 0, client.Memory.ReadInt32(client.Addresses.Player.Id));
        }

        /// <summary>
        /// Use an item on a creature of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatureId"></param>
        /// <returns></returns>
        public bool UseItemOnCreature(uint id, byte stack, int creatureId)
        {
            return Packets.Outgoing.UseItemOnCreaturePacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, stack, (uint)creatureId);
        }

        /// <summary>
        /// Use an item on a tile (eg. fishing)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public bool UseItemOnTile(uint id, Tile onTile)
        {
            return Packets.Outgoing.UseItemWithPacket.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Ground.Id, 0);
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
