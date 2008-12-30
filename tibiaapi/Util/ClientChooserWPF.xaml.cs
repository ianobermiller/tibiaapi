using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using Tibia.Objects;
using Microsoft.Win32;

namespace Tibia.Util
{
    /// <summary>
    /// Interaction logic for ClientChooserWPF.xaml
    /// </summary>
    public partial class ClientChooserWPF : Window
    {
        private static ClientChooserWPF newClientChooser;
        private static Objects.Client client;

        private const string NewClientDefaultText = "New default client...";
        private const string NewClientCustomText = "New client (choose location)...";

        private const string LoginServerEnabled = "Enabled";
        private const string LoginServerDisabled = "Disabled";

        private string _filename = "";

        private ClientChooserOptions options;
        
        public ClientChooserWPF()
        {
            InitializeComponent();
            ShowInTaskbar = true;
            uxUseOT.IsExpanded = false;
            uxLoginServerLabel.Content = LoginServerDisabled;
            uxLoginServerLabel.Foreground = Brushes.PaleVioletRed;
            client = null;
        }

        /// <summary>
        /// Opens a box to pick a client.
        /// </summary>
        /// <returns></returns>
        public static Client ShowBox()
        {
            return ShowBox(new ClientChooserOptions());
        }
        /// <summary>
        /// Open a box to pick a client with the desired options.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Client ShowBox(ClientChooserOptions options)
        {
              List<Objects.Client> clients = null;
            if (options.LookUpClients)
                clients = Objects.Client.GetClients();
            if (options.Smart && options.LookUpClients && !options.ShowOTOption && clients != null && clients.Count == 1)
                return clients[0];
            else
            {
                newClientChooser = new ClientChooserWPF();
                newClientChooser.Title = String.IsNullOrEmpty(options.Title) ? "Choose a client." : options.Title;
                if (options.LookUpClients)
                {
                    foreach (Client c in clients)
                    {
                        newClientChooser.uxClients.Items.Add(c);
                    }
                    if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Tibia\tibia.exe")))
                    {
                        newClientChooser.uxClients.Items.Add(NewClientDefaultText);
                    }
                }

                if (options.SaveClientPath == true)
                {
                    if (File.Exists(options.SavedClientPathsLocation))
                    {
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            document.Load(options.SavedClientPathsLocation);
                            string path;
                            string version;
                            foreach (XmlElement clientPath in document["clientPaths"]){
                                path = clientPath.GetAttribute("location");
                                version = clientPath.GetAttribute("version");
                                newClientChooser.uxClients.Items.Add(new ClientPathInfo(path, version));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }


                /*###############################*/

                newClientChooser.uxClients.Items.Add(NewClientCustomText);
                newClientChooser.uxClients.SelectedIndex = 0;

                newClientChooser.uxUseOT.IsExpanded = options.UseOT;
                //newClientChooser.uxLoginServer.Text = options.Server + ":" + options.Port.ToString();
                foreach (string address in options.addresses){
                    newClientChooser.uxLoginServer.Items.Add(address);
                }
                if (newClientChooser.uxLoginServer.Items.Count > 0)
                    newClientChooser.uxLoginServer.SelectedIndex = 0;
                newClientChooser.options = options;
                newClientChooser.ShowDialog();
                return client;
            }
        }

        private void uxChoose_Click(object sender, RoutedEventArgs e)
        {
            ChooseClient(newClientChooser.options);
        }

        private void uxUseOT_Expanded(object sender, RoutedEventArgs e)
        {
            uxLoginServerLabel.Content = LoginServerEnabled;
            uxLoginServerLabel.Foreground = Brushes.Green;
            if (uxLoginServer != null)
                uxLoginServer.Focus();
        }

        private void uxUseOT_Collapsed(object sender, RoutedEventArgs e)
        {
            uxLoginServerLabel.Content = LoginServerDisabled;
            uxLoginServerLabel.Foreground = Brushes.PaleVioletRed;
        }

        private void ChooseClient(ClientChooserOptions options)
        {
            if (uxClients.SelectedItem.GetType() == typeof(string))
            {
                switch ((string)uxClients.SelectedItem)
                {
                    case NewClientDefaultText:
                        client = Client.Open();
                        break;
                    case NewClientCustomText:
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter =
                           "executable files (*.exe)|*.exe|All files (*.*)|*.*";
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                        dialog.Title = "Select a Tibia client executable";
                        if (dialog.ShowDialog() == true)
                        {
                            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(dialog.FileName);
                            if (fvi.ProductName.Equals("Tibia Player"))
                            {
                                client = Client.Open(dialog.FileName, options.Arguments);
                                _filename = dialog.FileName;
                                if (options.SaveClientPath == true)
                                {
                                    //Save file in XML format
                                    XmlDocument document = new XmlDocument();
                                    XmlElement clientPaths = null;
                                    bool exists = false;
                                    if (File.Exists(options.SavedClientPathsLocation))
                                    {
                                        document.Load(options.SavedClientPathsLocation);
                                        clientPaths = document["clientPaths"];
                                        foreach (XmlElement clientPath in clientPaths){
                                            if (clientPath.GetAttribute("location").Equals(dialog.FileName))
                                            {
                                                if (document["clientPaths"].FirstChild != clientPath)
                                                {
                                                    document["clientPaths"].RemoveChild(clientPath);
                                                    document["clientPaths"].InsertBefore(clientPath, document["clientPaths"].FirstChild);
                                                }
                                                document.Save(options.SavedClientPathsLocation);
                                                if (!clientPath.GetAttribute("version").Equals(fvi.FileVersion))
                                                {
                                                    clientPath.SetAttribute("version", fvi.FileVersion);
                                                    document.Save(options.SavedClientPathsLocation);
                                                    exists = true;
                                                    break;
                                                }
                                                exists = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        XmlDeclaration Declaration = document.CreateXmlDeclaration("1.0", "", "");
                                        document.AppendChild(Declaration);
                                        clientPaths = document.CreateElement("clientPaths");
                                        document.AppendChild(clientPaths);
                                    }
                                    if (!exists)
                                    {
                                        XmlElement clientPath = document.CreateElement("clientPath");
                                        XmlAttribute location = document.CreateAttribute("location");
                                        location.InnerText = dialog.FileName;
                                        XmlAttribute version = document.CreateAttribute("version");
                                        version.InnerText = fvi.FileVersion;

                                        clientPath.Attributes.Append(location);
                                        clientPath.Attributes.Append(version);
                                        clientPaths.AppendChild(clientPath);

                                        if (!Directory.Exists(Constants.TAConstants.AppDataPath))
                                            Directory.CreateDirectory(Constants.TAConstants.AppDataPath);

                                        document.Save(options.SavedClientPathsLocation);
                                    }
                                }

                            }
                            else
                            {
                                Microsoft.VisualBasic.Interaction.Beep();   
                                client = null;
                                return;
                            }
                        }
                        else
                        {
                            client = null;
                            return;
                        }
                        break;
                }
                
                //if (client != null)
                //    System.Threading.Thread.Sleep(1000);
            }
            else if (uxClients.SelectedItem.GetType() == typeof(ClientPathInfo))
            {
                client = Client.Open(((ClientPathInfo)uxClients.SelectedItem).Path, options.Arguments);
            }
            else
            {
                client = (Client)uxClients.SelectedItem;
            }

            // Set OT server
            if (client != null && uxUseOT.IsExpanded)
            {
                string[] explode = uxLoginServer.Text.Split(new char[]{':'});
                LoginServer ls = new LoginServer(explode[0], short.Parse(explode[1]));
                client.OpenTibiaServer = ls;
                client.SetOT(ls);
            }
            newClientChooser.Close();
        }



        public string FileName
        {
            get
            {
                return _filename;
            }
        }

        private void CommonKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ChooseClient(newClientChooser.options);
        }
    }
}
