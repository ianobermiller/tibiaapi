using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a vip entry.
    /// </summary>
    public class Vip
    {
        private Client client;
        private uint address;

        internal Vip(Client client, uint address)
        {
            this.client = client;
            this.address = address;
        }


        #region Get/Set Methods

        public int Id
        {
            get { return client.Memory.ReadInt32(address + client.Addresses.Vip.DistanceId); }
            set { client.Memory.WriteInt32(address + client.Addresses.Vip.DistanceIcon, value); }
        }

        public string Name
        {
            get { return client.Memory.ReadTextField(address + client.Addresses.Vip.DistanceNameField); }
        }

        public string Description
        {
            get { return client.Memory.ReadTextField(address + client.Addresses.Vip.DistanceDescriptionField); }
        }

        public Constants.VipStatus Status
        {
            get { return (Constants.VipStatus)client.Memory.ReadByte(address + client.Addresses.Vip.DistanceStatus); }
            set { client.Memory.WriteByte(address + client.Addresses.Vip.DistanceStatus, (byte)value); }
        }

        public Constants.VipIcon Icon
        {
            get { return (Constants.VipIcon)client.Memory.ReadByte(address + client.Addresses.Vip.DistanceIcon); }
            set { client.Memory.WriteByte(address + client.Addresses.Vip.DistanceIcon, (byte)value); }
        }

        public bool NotifyOnLogin
        {
            get { return Convert.ToBoolean(client.Memory.ReadByte(address + client.Addresses.Vip.DistanceNotify)); }
            set { client.Memory.WriteByte(address + client.Addresses.Vip.DistanceNotify, Convert.ToByte(value)); }
        }

        #endregion
    }
}
