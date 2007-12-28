using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tibia
{
    public partial class ClientChooser : Form
    {
        private static ClientChooser newClientChooser;
        private static Objects.Client client;
        
        public ClientChooser()
        {
            InitializeComponent();
            client = null;
        }

        /// <summary>
        /// Opens a box to pick a client. (Wrapper for Showbox(bool) with smart enabled).
        /// </summary>
        /// <returns></returns>
        public static Objects.Client ShowBox()
        {
            return ShowBox(String.Empty, true);
        }

        /// <summary>
        /// Opens a box to pick a client.
        /// </summary>
        /// <param name="title">The title of the box.</param>
        /// <param name="smart">If true, will not open a box if there is only one 
        /// client; just returns that client.</param>
        /// <returns></returns>
        public static Objects.Client ShowBox(string title, bool smart)
        {
            List<Objects.Client> clients = Objects.Client.GetClients();
            if (clients.Count == 0)
                return null;
            else if (clients.Count == 1)
                return clients[0];
            else
            {
                newClientChooser = new ClientChooser();
                newClientChooser.Text = title == string.Empty ? "Choose a client." : title;
                newClientChooser.uxClients.DataSource = clients;
                newClientChooser.ShowDialog();
                return client;
            }
        }

        private void uxChoose_Click(object sender, EventArgs e)
        {
            client = (Objects.Client)uxClients.SelectedItem;
            newClientChooser.Dispose();
        }
    }
}