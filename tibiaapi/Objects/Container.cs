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
        /// Create a new container object with the specified client, address, and number.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="n"></param>
        public Container(Client c, uint a, byte n)
        {
            client = c;
            address = a;
            number = n;
        }

        /// <summary>
        /// Get the container's address.
        /// </summary>
        /// <returns></returns>
        public uint GetAddress()
        {
            return address;
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
                byte itemCount = client.ReadByte(i + Addresses.Container.Distance_Item_Count);
                uint itemId = (uint) client.ReadInt(i + Addresses.Container.Distance_Item_Id);
                if (itemId > 0)
                    items.Add(new Item(
                        itemId,
                        itemCount,
                        new ItemLocation(number, slot),
                        true));
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
            byte[] packet = new byte[4];

            packet[0] = 0x02;
            packet[1] = 0x00;
            packet[2] = 0x88;
            packet[3] = number;

            return client.Send(packet);
        }

        /** Get and set various aspects of the container **/
        #region Get/Set Methods
        public int Id
        {
            get { return client.ReadInt(address + Addresses.Container.Distance_Id); }
            set { client.WriteInt(address + Addresses.Container.Distance_Id, value); Id = value; }
        }
        public bool IsOpen
        {
            get { return Convert.ToBoolean(client.ReadInt(address + Addresses.Container.Distance_IsOpen)); }
            set { client.WriteInt(address + Addresses.Container.Distance_IsOpen, Convert.ToByte(value)); IsOpen = value; }
        }
        public int Amount
        {
            get { return client.ReadInt(address + Addresses.Container.Distance_Amount); }
            set { client.WriteInt(address + Addresses.Container.Distance_Amount, value); Amount = value; }
        }
        public string Name
        {
            get { return client.ReadString(address + Addresses.Container.Distance_Name); }
            set { client.WriteString(address + Addresses.Container.Distance_Name, value); Name = value; }
        }
        public int Volume
        {
            get { return client.ReadInt(address + Addresses.Container.Distance_Volume); }
            set { client.WriteInt(address + Addresses.Container.Distance_Volume, value); Volume = value; }
        }
        #endregion
    }
}