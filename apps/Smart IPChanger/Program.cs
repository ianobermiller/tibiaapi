using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Tibia;
using Tibia.Objects;
using Tibia.Util;

namespace SmartIPChanger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string server = string.Empty;
                string port = string.Empty;

                if (args.Length == 1)
                {
                    server = args[0];
                    port = "7171";
                }
                else if (args.Length == 2)
                {
                    server = args[0];
                    port = args[1];
                }
                else
                    Application.Exit();

                Client client = ClientChooser.ShowBox();
                if (client == null)
                {
                    if (File.Exists("tibia.exe"))
                        client = Client.Open(Application.StartupPath + "\\tibia.exe");
                    else
                        client = Client.Open();
                    System.Threading.Thread.Sleep(1000);
                }
                client.SetOT(server, short.Parse(port));
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new uxForm());
            }
        }
    }
}