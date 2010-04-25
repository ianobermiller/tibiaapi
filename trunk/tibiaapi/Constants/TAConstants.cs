using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Constants
{
    public static class TAConstants
    {
        public static string CurrentTibiaVersion = "8.55";
        public static string AppDataPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TibiaAPI");
    }
}
