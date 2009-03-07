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
            get { return client.Memory.ReadInt32(address + Addresses.Vip.DistanceId); }
            set { client.Memory.WriteInt32(address + Addresses.Vip.DistanceIcon, value); }
        }

        public string Name
        {
            get { return client.Memory.ReadString(address + Addresses.Vip.DistanceName); }
            set { client.Memory.WriteString(address + Addresses.Vip.DistanceName, value); }
        }

        public Constants.VipStatus Status
        {
            get { return (Constants.VipStatus)client.Memory.ReadByte(address + Addresses.Vip.DistanceStatus); }
            set { client.Memory.WriteByte(address + Addresses.Vip.DistanceStatus, (byte)value); }
        }

        public Constants.VipIcon Icon
        {
            get { return (Constants.VipIcon)client.Memory.ReadByte(address + Addresses.Vip.DistanceIcon); }
            set { client.Memory.WriteByte(address + Addresses.Vip.DistanceIcon, (byte)value); }
        }

        #endregion
    }
}
