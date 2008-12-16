using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a vip entry.
    /// </summary>
    public class Vip
    {
        protected Client client;
        protected uint address;

        public Vip(Client c, uint a)
        {
            client = c;
            address = a;
        }
        public uint Address
        {
            get { return address; }
            set { address = value; }
        }

        #region Get/Set Methods

        public int Id
        {
            get { return client.ReadInt32(address + Addresses.Vip.Distance_Id); }
            set { client.WriteInt32(address + Addresses.Vip.Distance_Icon, value); }
        }
        public string Name
        {
            get { return client.ReadString(address + Addresses.Vip.Distance_Name); }
            set { client.WriteString(address + Addresses.Vip.Distance_Name, value); }
        }
        public Constants.VipStatus Status
        {
            get { return (Constants.VipStatus)client.ReadByte(address + Addresses.Vip.Distance_Status); }
            set { client.WriteByte(address + Addresses.Vip.Distance_Status, (byte)value); }
        }
        public Constants.VipIcon Icon
        {
            get { return (Constants.VipIcon)client.ReadByte(address + Addresses.Vip.Distance_Icon); }
            set { client.WriteByte(address + Addresses.Vip.Distance_Icon, (byte)value); }
        }

        #endregion
    }
}
