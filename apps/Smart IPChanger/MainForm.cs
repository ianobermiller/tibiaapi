using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Tibia;
using Tibia.Objects;
using Tibia.Util;
using System.Xml;
using System.IO;

namespace SmartIPChanger
{
    public partial class MainForm : Form
    {
        // Todo:
        // move stuff into a separate class in TibiaAPI

        public static string SavedServersLocation = System.IO.Path.Combine(Tibia.Constants.TAConstants.AppDataPath, @"servers.xml");
        public MainForm()
        {
            InitializeComponent();
        }

        private void uxGo_Click(object sender, EventArgs e)
        {
            Go();
        }

        private void Go()
        {
            Client client = ClientChooser.ShowBox(new ClientChooserOptions() { ShowOTOption = false, Smart = false });
            
            if (client == null)
            {
                client = Client.Open();
                System.Threading.Thread.Sleep(1000);
            }
            string[] split = uxServer.Text.Split(":".ToCharArray());
            client.Login.SetOT(split[0], short.Parse(split[1]));
            SaveServer(split[0], split[1], client.Version);
        }

        private void CommonKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Go();
            }
        }

        private void uxForm_Load(object sender, EventArgs e)
        {
            bool first = true;
            foreach (LoginServer ls in GetSavedServers())
            {
                uxServer.Items.Add(ls.Server + ":" + ls.Port);
                if (first)
                {
                    uxServer.Text = ls.Server + ":" + ls.Port;
                    first = false;
                }
            }
            uxServer.SelectedIndex = uxServer.Items.Count - 1;
        }

        private static List<LoginServer> GetSavedServers()
        {
            List<LoginServer> serverList = new List<LoginServer>();
            XmlDocument document = new XmlDocument();
            XmlElement servers = null;
            if (File.Exists(SavedServersLocation))
            {
                document.Load(SavedServersLocation);
                servers = document["servers"];

                foreach (XmlElement server in servers)
                {
                    serverList.Add(new LoginServer(
                        server.GetAttribute("ip"),
                        short.Parse(server.GetAttribute("port")),
                        server.GetAttribute("version")));
                }
            }
            return serverList;
        }

        private static void SaveServer(string ip, string port, string version)
        {
            XmlDocument document = new XmlDocument();
            XmlElement servers = null;
            if (File.Exists(SavedServersLocation))
            {
                document.Load(SavedServersLocation);
                servers = document["servers"];
            }
            else
            {
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "", "");
                document.AppendChild(declaration);
                servers = document.CreateElement("servers");
                document.AppendChild(servers);
            }

            bool found = false;
            foreach (XmlElement otherserver in servers)
            {
                if (otherserver.GetAttribute("ip").Equals(ip) &&
                    otherserver.GetAttribute("port").Equals(port))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                XmlElement server = document.CreateElement("server");

                XmlAttribute ipAttr = document.CreateAttribute("ip");
                ipAttr.InnerText = ip;
                XmlAttribute portAttr = document.CreateAttribute("port");
                portAttr.InnerText = port;
                XmlAttribute versionAttr = document.CreateAttribute("version");
                versionAttr.InnerText = version;

                server.Attributes.Append(ipAttr);
                server.Attributes.Append(portAttr);
                server.Attributes.Append(versionAttr);
                servers.AppendChild(server);
            }

            if (!Directory.Exists(Tibia.Constants.TAConstants.AppDataPath))
                Directory.CreateDirectory(Tibia.Constants.TAConstants.AppDataPath);

            document.Save(SavedServersLocation);
        }
    }
}