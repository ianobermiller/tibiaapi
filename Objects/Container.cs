using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Container object.
    /// </summary>
    public class Container
    {
        protected Client _client;
        protected uint _address;
        protected byte _number;

        /// <summary>
        /// Create a new container object with the specified client, address, and number.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="n"></param>
        public Container(Client c, uint a, byte n)
        {
            _client = c;
            _address = a;
            _number = n;
        }

        /// <summary>
        /// Get the container's address.
        /// </summary>
        /// <returns></returns>
        public uint getAddress()
        {
            return _address;
        }

        /// <summary>
        /// Return a list of all the items in the container.
        /// </summary>
        /// <returns></returns>
        public List<Item> getItems()
        {
            byte slot = 0;
            int amount = Amount;
            List<Item> items = new List<Item>(amount);
            for (uint i = _address; i <= _address + Memory.Addresses.Container.Step_Slot * amount - 1; i += Memory.Addresses.Container.Step_Slot)
            {
                byte itemCount = _client.ReadByte(i + Memory.Addresses.Container.Distance_Item_Count);
                uint itemId = (uint) _client.ReadInt(i + Memory.Addresses.Container.Distance_Item_Id);
                if (itemId > 0)
                    items.Add(new Item(
                        itemId,
                        itemCount,
                        new ItemLocation(_number, slot),
                        true));
                slot++;
            }
            return items;
        }

        /** Get and set various aspects of the container **/
        #region Get/Set Methods
        public int Id
        {
            get { return _client.ReadInt(_address + Memory.Addresses.Container.Distance_Id); }
            set { _client.WriteInt(_address + Memory.Addresses.Container.Distance_Id, value); Id = value; }
        }
        public bool IsOpen
        {
            get { return Convert.ToBoolean(_client.ReadInt(_address + Memory.Addresses.Container.Distance_IsOpen)); }
            set { _client.WriteInt(_address + Memory.Addresses.Container.Distance_IsOpen, Convert.ToByte(value)); IsOpen = value; }
        }
        public int Amount
        {
            get { return _client.ReadInt(_address + Memory.Addresses.Container.Distance_Amount); }
            set { _client.WriteInt(_address + Memory.Addresses.Container.Distance_Amount, value); Amount = value; }
        }
        public string Name
        {
            get { return _client.ReadString(_address + Memory.Addresses.Container.Distance_Name); }
            set { _client.WriteString(_address + Memory.Addresses.Container.Distance_Name, value); Name = value; }
        }
        public int Volume
        {
            get { return _client.ReadInt(_address + Memory.Addresses.Container.Distance_Volume); }
            set { _client.WriteInt(_address + Memory.Addresses.Container.Distance_Volume, value); Volume = value; }
        }
        #endregion
    }
}