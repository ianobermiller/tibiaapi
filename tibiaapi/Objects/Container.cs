using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Container object.
    /// </summary>
    public class Container
    {
        protected Client client;
        protected uint address;
        protected byte number;

        /// <summary>
        /// Create a new container object with the specified number.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="number">The number of the container (0 based).</param>
        public Container(Client client, byte number)
        {
            this.client = client;
            this.number = number;
            address = Addresses.Container.Start + (number * Addresses.Container.Step_Container);
        }

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
        public List<Item> GetItems()
        {
            byte slot = 0;
            int amount = Amount;
            List<Item> items = new List<Item>(amount);
            for (uint i = address; i <= address + Addresses.Container.Step_Slot * amount - 1; i += Addresses.Container.Step_Slot)
            {

                uint itemId = client.ReadUInt32(i + Addresses.Container.Distance_Item_Id);
                if (itemId > 0)
                    items.Add(new Item(client, itemId,
                        client.ReadByte(i + Addresses.Container.Distance_Item_Count)
                        , "", new ItemLocation(number, slot), true));
                
                slot++;
            }

            return items;
        }

        /// <summary>
        /// Open this containers parent container
        /// </summary>
        /// <returns></returns>
        public bool OpenParent()
        {
            return Packets.Outgoing.UpArrowContainerPacket.Send(client, number);
        }

        /// <summary>
        /// Closes the container.
        /// </summary>
        /// <returns>True if it succeeded or if the container is aleady closed</returns>
        public bool Close()
        {
            //Return true if it is not open because there was no error
            if (!IsOpen)
                return true;

            return Packets.Outgoing.CloseContainerPacket.Send(client, number);
        }

        /// <summary>
        /// Rename this container by sending a packet. 
        /// Doesn't require the user to move the container.
        /// Only caveat is that you can no longer go back up a container.
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        public bool Rename(string newName)
        {
            //TODO: Find if the container has parent.
            if (client.UsingProxy)
                return Packets.Incoming.OpenContainerPacket.Send(client, number, (ushort)Id, newName, (byte)Volume, 0, GetItems());
            else
                return false;
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
            get { return client.ReadInt32(address + Addresses.Container.Distance_Id); }
            set { client.WriteInt32(address + Addresses.Container.Distance_Id, value); }
        }

        /// <summary>
        /// Gets whether the container is open. Setting this value often times
        /// crashes the Tibia client.
        /// </summary>
        public bool IsOpen
        {
            get { return Convert.ToBoolean(client.ReadInt32(address + Addresses.Container.Distance_IsOpen)); }
            set { client.WriteInt32(address + Addresses.Container.Distance_IsOpen, Convert.ToByte(value)); }
        }

        /// <summary>
        /// Gets the amount of items that are currently in the container.
        /// </summary>
        public int Amount
        {
            get { return client.ReadInt32(address + Addresses.Container.Distance_Amount); }
            set { client.WriteInt32(address + Addresses.Container.Distance_Amount, value); }
        }

        /// <summary>
        /// Gets the name or caption of the container. Setting this value usually
        /// does not update the UI immediately.
        /// </summary>
        public string Name
        {
            get { return client.ReadString(address + Addresses.Container.Distance_Name); }
            set { client.WriteString(address + Addresses.Container.Distance_Name, value); }
        }

        /// <summary>
        /// The total amount of items this container can contain.
        /// </summary>
        public int Volume
        {
            get { return client.ReadInt32(address + Addresses.Container.Distance_Volume); }
            set { client.WriteInt32(address + Addresses.Container.Distance_Volume, value); }
        }
        #endregion
    }
}