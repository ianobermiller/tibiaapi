using System;

namespace Tibia
{
    public partial class Version
    {
        public static ushort CurrentVersion = 862;
        public static string CurrentVersionString = "8.62";

        public static string VersionToString(ushort version)
        {
            int main = version / 100;
            int secondary = version - main * 100;
            return String.Format("{0}.{1:00}", main, secondary);
        }

        public static ushort StringToVersion(string versionString)
        {
            string[] split = versionString.Split('.');
            int main = int.Parse(split[0]);
            int secondary = int.Parse(split[1]);
            return (ushort)(main * 100 + secondary);
        }

        public static void Set(string version)
        {
            CurrentVersion = StringToVersion(version);
            CurrentVersionString = version;
            switch (version)
            {
                case "8.70":
                    SetVersion870();
                    break;
                case "8.62":
                    SetVersion862();
                    break;
                case "8.61":
                    SetVersion861();
                    break;
                case "8.60":
                    SetVersion860();
                    break;
                case "8.57":
                    SetVersion857();
                    break;
                case "8.55":
                    SetVersion855();
                    break;
                case "8.54":
                    SetVersion854();
                    break;
                case "8.53":
                    SetVersion853();
                    break;
                case "8.52":
                    SetVersion852();
                    break;
                case "8.50":
                    SetVersion850();
                    break;
                case "8.42":
                    SetVersion842();
                    break;
                case "8.41":
                    SetVersion841();
                    break;
                case "8.40":
                    SetVersion840();
                    break;
                case "8.31":
                    SetVersion831();
                    break;
                case "8.22":
                    SetVersion822();
                    break;
                case "8.21":
                    SetVersion821();
                    break;
                case "8.20":
                    SetVersion820();
                    break;
                case "8.10":
                case "8.11":
                    SetVersion810and811();
                    break;
                case "8.00":
                    SetVersion800();
                    break;
                default:
                    throw new Exceptions.VersionNotSupportedException("Tibia version " + CurrentVersionString + " is not supported by TibiaAPI.");
            }

        }

    }
}
