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

        public static Objects.Client ShowBox()
        {
            List<Objects.Client> clients = Objects.Client.getClients();
            if (clients.Count == 0) return null;
            newClientChooser = new ClientChooser();
            newClientChooser.uxClients.DataSource = clients;
            newClientChooser.ShowDialog();
            return client;
        }

        private void uxChoose_Click(object sender, EventArgs e)
        {
            client = (Objects.Client)uxClients.SelectedItem;
            newClientChooser.Dispose();
        }
    }
}