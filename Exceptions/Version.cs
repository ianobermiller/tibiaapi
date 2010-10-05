using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Tibia.Exceptions
{
    /// <summary>
    /// Thrown when trying to access a resource that is not currently supported, ie. getting the addresses for an unsupported Tibia version.
    /// </summary>
    public class VersionNotSupportedException : InvalidOperationException
    {
        public VersionNotSupportedException(string message) : base(message) { }
    }

    public static class Assert
    {
        public static void VersionGreaterOrEqualTo(Objects.Client client, string requiredVersion)
        {
            int currVersion = VersionStringToInt(client.Version);
            int reqVersion = VersionStringToInt(requiredVersion);
            if (currVersion < reqVersion)
                throw new VersionNotSupportedException(
                    "This method requires a Tibia client version " +
                    requiredVersion + " or greater. Your current version, " +
                    client.Version + " is not supported.");
        }

        public static void VersionEqualTo(Objects.Client client, string requiredVersion)
        {
            int currVersion = VersionStringToInt(client.Version);
            int reqVersion = VersionStringToInt(requiredVersion);
            if (currVersion != reqVersion) 
                throw new VersionNotSupportedException(
                    "This method requires a Tibia client version " + 
                    requiredVersion + ". Your current version, " + 
                    client.Version + " is not supported.");
        }

        private static int VersionStringToInt(string version)
        {
            int converted = 0;
            int modifier = 1;
            string[] parts = version.Split(new char[] { '.' });
            for (int i = parts.Length - 1; i >= 0; i--)
            {
                converted += int.Parse(parts[i]) * modifier;
                modifier *= 100;
            }
            return converted;
        }
    }
}
