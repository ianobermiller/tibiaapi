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

        
        public ClientChooserWPF()
        {
            InitializeComponent();
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
            List<Objects.Client> clients = Objects.Client.GetClients();
            if (options.Smart && !options.ShowOTOption && clients.Count == 1)
                return clients[0];
            else
            {
                newClientChooser = new ClientChooserWPF();
                newClientChooser.Title = options.Title == string.Empty ? "Choose a client." : options.Title;
                foreach (Client c in clients)
                    newClientChooser.uxClients.Items.Add(c);
                newClientChooser.uxClients.Items.Add(NewClientDefaultText);
                newClientChooser.uxClients.Items.Add(NewClientCustomText);
                newClientChooser.uxClients.SelectedIndex = 0;

                newClientChooser.uxUseOT.IsExpanded = options.UseOT;
                newClientChooser.uxLoginServer.Text = options.Server + ":" + options.Port.ToString();

                newClientChooser.ShowDialog();
                return client;
            }
        }

        private void uxChoose_Click(object sender, RoutedEventArgs e)
        {
            ChooseClient();
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

        private void ChooseClient()
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
                        client = (dialog.ShowDialog() == true)
                           ? Client.Open(dialog.FileName) : null;
                        break;
                }
                if (client != null)
                    System.Threading.Thread.Sleep(1000);
            }
            else
            {
                client = (Client)uxClients.SelectedItem;
            }

            // Set OT server
            if (client != null && uxUseOT.IsExpanded)
            {
                string[] explode = uxLoginServer.Text.Split(":".ToCharArray());
                LoginServer ls = new LoginServer(explode[0], short.Parse(explode[1]));
                client.OpenTibiaServer = ls;
                client.SetOT(ls);
            }
            newClientChooser.Close();
        }

        private void CommonKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ChooseClient();
        }
    }
}
