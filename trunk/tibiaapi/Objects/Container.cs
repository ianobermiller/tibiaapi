using System;
using System.Linq;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Container object.
    /// </summary>
    public class Container
    {
        private Client client;
        private uint address;
        private byte number;

        /// <summary>
        /// Create a new container object with the specified client, address, and number.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="address">The address of the container.</param>
        /// <param name="number">The number of the container (0 based).</param>
        public Container(Client client, uint address, byte number)
        {
            this.client = client;
            this.address = address;
            this.number = number;
        }

        /// <summary>
        /// Return a list of all the items in the container.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            int amount = Amount;
            uint slotsAddress = address + client.Addresses.Container.DistanceContainerSlotsBegin;
            return Enumerable.Range(0, amount)
                    .Select(slotnum => new
                    {
                        Number = slotnum,
                        Address = slotsAddress + slotnum * client.Addresses.Container.StepSlot
                    })
                    .Select(slot => new
                    {
                        ItemID = client.Memory.ReadUInt32(slot.Address + client.Addresses.Container.DistanceContainerSlotItemId),
                        ItemCount = client.Memory.ReadByte(slot.Address + client.Addresses.Container.DistanceContainerSlotItemCount),
                        Position = slot.Number
                    })
                    .Where(ItemSlot => ItemSlot.ItemID > 0)
                    .Select(ItemSlot => new Item(
                        client,
                        ItemSlot.ItemID,
                        ItemSlot.ItemCount,
                        "",
                        ItemLocation.FromContainer(number, (byte)ItemSlot.Position)));
        }

        /// <summary>
        /// Open this containers parent container
        /// </summary>
        /// <returns></returns>
        public void OpenParent()
        {
            if (HasParent)
                Packets.Outgoing.UpContainerPacket.Send(client, number);
        }

        /// <summary>
        /// Closes the container.
        /// </summary>
        /// <returns>True if it succeeded or if the container is aleady closed</returns>
        public void Close()
        {
            if (IsOpen)
                Packets.Outgoing.CloseContainerPacket.Send(client, number);
        }

        /// <summary>
        /// Rename this container by sending a packet. 
        /// Doesn't require the user to move the container.
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        public void Rename(string newName)
        {
            //if (client.IO.UsingProxy)
            var p = new Packets.Incoming.OpenContainerPacket(client) { Id = number, 
                                                                ItemId = (ushort)Id,
                                                                Name = newName,
                                                                Capacity =(byte) Volume,
                                                                HasParent = Convert.ToByte(HasParent),
                                                                Items = GetItems().ToList() };
            p.Send();
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Number.ToString(), Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is Container)
                return ((Container)obj).Number == Number;
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region Get/Set Properties
        /// <summary>
        /// Gets the client this container is associated with.
        /// </summary>
        public Client Client
        {
            get { return client; }
        }

        /// <summary>
        /// Gets the container's number.
        /// </summary>
        public byte Number
        {
            get { return number; }
        }

        /// <summary>
        /// Get the container's address.
        /// </summary>
        public uint Address
        {
            get { return address; }
        }

        /// <summary>
        /// Gets the container's id.
        /// </summary>
        public int Id
        {
            get { return client.Memory.ReadInt32(address + client.Addresses.Container.DistanceId); }
            set { client.Memory.WriteInt32(address + client.Addresses.Container.DistanceId, value); }
        }

        /// <summary>
        /// Gets whether the container is open. Setting this value often times
        /// crashes the Tibia client.
        /// </summary>
        public bool IsOpen
        {
            get { return Convert.ToBoolean(client.Memory.ReadInt32(address + client.Addresses.Container.DistanceIsOpen)); }
            set { client.Memory.WriteInt32(address + client.Addresses.Container.DistanceIsOpen, Convert.ToByte(value)); }
        }

        /// <summary>
        /// Gets the amount of items that are currently in the container.
        /// </summary>
        public int Amount
        {
            get { return client.Memory.ReadInt32(address + client.Addresses.Container.DistanceAmount); }
            set { client.Memory.WriteInt32(address + client.Addresses.Container.DistanceAmount, value); }
        }

        /// <summary>
        /// Gets the name or caption of the container. Setting this value usually
        /// does not update the UI immediately.
        /// </summary>
        public string Name
        {
            get { return client.Memory.ReadString(address + client.Addresses.Container.DistanceName); }
            set 
            {
                if (value.Length > 31)
                    throw new ArgumentOutOfRangeException("value.Length > 31");
                else
                {
                    client.Memory.WriteString(address + client.Addresses.Container.DistanceName, value);
                    Tibia.Util.WinApi.RECT clientRect = new Tibia.Util.WinApi.RECT();
                    Tibia.Util.WinApi.GetClientRect(client.Process.MainWindowHandle, out clientRect);
                    client.Input.SendMessage(Hooks.WM_SIZE, 0, Tibia.Util.WinApi.MakeLParam((uint)clientRect.right, (uint)clientRect.bottom));
                }
            }
        }

        /// <summary>
        /// The total amount of items this container can contain.
        /// </summary>
        public int Volume
        {
            get { return client.Memory.ReadInt32(address + client.Addresses.Container.DistanceVolume); }
            set { client.Memory.WriteInt32(address + client.Addresses.Container.DistanceVolume, value); }
        }

        public bool HasParent
        {
            get { return Convert.ToBoolean(client.Memory.ReadInt32(address + client.Addresses.Container.DistanceHasParent)); }
        }

        #endregion
    }
}