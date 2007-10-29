using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Objects
{
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
            get { return client.readInt(address + Addresses.Vip.Distance_Id); }
            set { client.writeInt(address + Addresses.Vip.Distance_Icon, value); }
        }
        public string Name
        {
            get { return client.readString(address + Addresses.Vip.Distance_Name); }
            set { client.writeString(address + Addresses.Vip.Distance_Name, value); }
        }
        public Constants.VipStatus Status
        {
            get { return (Constants.VipStatus)client.readByte(address + Addresses.Vip.Distance_Status); }
            set { client.writeByte(address + Addresses.Vip.Distance_Status, (byte)value); }
        }
        #endregion
    }
}
