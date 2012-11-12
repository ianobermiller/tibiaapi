using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;

namespace InventoryTest
{
    public partial class uxForm : Form
    {
        Client client;
        System.Threading.Timer timer;
        List<Tibia.Objects.Container> containers;
        ContextMenuStrip cmsContainer;

        public uxForm()
        {
            InitializeComponent();
            containers = new List<Tibia.Objects.Container>();
            cmsContainer = new ContextMenuStrip();
            cmsContainer.Items.Add("Open parent", null, new EventHandler(OpenParent));
            cmsContainer.Items.Add("Close", null, new EventHandler(Close));
            cmsContainer.Items.Add("Rename", null, new EventHandler(Rename));
        }

        private void uxForm_Load(object sender, EventArgs e)
        {

            client = Tibia.Util.ClientChooser.ShowBox();
            if (client == null)
            {
                MessageBox.Show("No active client.");
                Application.Exit();
                return;
            }
            // Start Proxy to test Container.Rename()
            //client.IO.StartProxy();
        }

        void UpdateList(object o)
        {

            uxInventoryTV.BeginInvoke(new EventHandler(delegate
            {
                uxInventoryTV.Nodes.Clear();
                if (client.Status == Tibia.Constants.LoginStatus.LoggedIn)
                {
                    containers = new List<Tibia.Objects.Container>(client.Inventory.GetContainers());
                    foreach (Tibia.Objects.Container c in containers)
                    {
                        TreeNode tncontainer = new TreeNode(c.Number.ToString());
                                           
                        foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(c))
                        {
                            tncontainer.Nodes.Add(new TreeNode(pd.Name + ":" + pd.GetValue(c)));
                        }

                        tncontainer.ContextMenuStrip = cmsContainer;
                        tncontainer.Tag = c;

                        TreeNode tnslots = new TreeNode("Slots");
                        int k=0;
                        foreach (Item i in c.GetItems())
                        {
                            tnslots.Nodes.Add(new TreeNode( k.ToString() + " - " + i.Id.ToString(), new TreeNode[]{
                                new TreeNode("Id:"+i.Id),
                                new TreeNode("Count:"+i.Count)
                            }));
                            
                            k++;
                        }
                        tncontainer.Nodes.Add(tnslots);
                        uxInventoryTV.Nodes.Add(tncontainer);
                    }
                }
            }));

        }

        private void uxUpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateList(null);
        }

        private void uxTimerEnableChk_CheckedChanged(object sender, EventArgs e)
        {
            if (uxTimerEnableChk.Checked)
            {
                timer = new System.Threading.Timer(new TimerCallback(UpdateList), null, 0, (long)uxMilisecondsNUD.Value);
            }
            else
            {
                timer.Dispose();
            }

        }


        private void OpenParent(object sender, EventArgs e)
        {
            if (uxInventoryTV.SelectedNode != null && uxInventoryTV.SelectedNode.Tag is Tibia.Objects.Container)
                ((Tibia.Objects.Container)uxInventoryTV.SelectedNode.Tag).OpenParent();
        }
        private void Close(object sender, EventArgs e)
        {
            if (uxInventoryTV.SelectedNode != null && uxInventoryTV.SelectedNode.Tag is Tibia.Objects.Container)
                ((Tibia.Objects.Container)uxInventoryTV.SelectedNode.Tag).Close();
        }
        private void Rename(object sender, EventArgs e)
        {
            if (uxInventoryTV.SelectedNode != null && uxInventoryTV.SelectedNode.Tag is Tibia.Objects.Container)
                ((Tibia.Objects.Container)uxInventoryTV.SelectedNode.Tag).Rename("THIS IS A TEST");
        }
    }
}
