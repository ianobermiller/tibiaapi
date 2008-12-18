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
        public uint Address { get; set; }

        public Vip(Client client, uint address)
        {
            this.client = client;
            Address = address;
        }


        #region Get/Set Methods

        public int Id
        {
            get { return client.ReadInt32(Address + Addresses.Vip.Distance_Id); }
            set { client.WriteInt32(Address + Addresses.Vip.Distance_Icon, value); }
        }

        public string Name
        {
            get { return client.ReadString(Address + Addresses.Vip.Distance_Name); }
            set { client.WriteString(Address + Addresses.Vip.Distance_Name, value); }
        }

        public Constants.VipStatus Status
        {
            get { return (Constants.VipStatus)client.ReadByte(Address + Addresses.Vip.Distance_Status); }
            set { client.WriteByte(Address + Addresses.Vip.Distance_Status, (byte)value); }
        }

        public Constants.VipIcon Icon
        {
            get { return (Constants.VipIcon)client.ReadByte(Address + Addresses.Vip.Distance_Icon); }
            set { client.WriteByte(Address + Addresses.Vip.Distance_Icon, (byte)value); }
        }

        #endregion
    }
}
