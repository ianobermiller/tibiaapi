using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        #region Constructors
        public AddressesCollection(Tibia.Objects.Client client)
            : this(client.Version, client.Process)
        { }

        public AddressesCollection(string version)
            : this(version, 0)
        { }

        public AddressesCollection(string version, Process p)
            : this(version, Convert.ToUInt32(p.MainModule.BaseAddress.ToInt32()))
        { }

        public AddressesCollection(string version, uint baseAddress)
        {            
            switch (version)
            {
                case "9.7.1.0":
                    SetVersion9_7_1_0(baseAddress);
                    break;
                default:
                    throw new Exceptions.VersionNotSupportedException("Tibia version " + version + " is not supported by TibiaAPI.");
            }
        }
        #endregion
    }
}
