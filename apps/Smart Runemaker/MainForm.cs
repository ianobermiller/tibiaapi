using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Tibia;
using Tibia.Objects;
using Tibia.Util;

namespace SmartRunemaker
{
    public partial class MainForm : Form
    {
        private Rune rune;
        private Client client;
        private Player player;
        
        public MainForm()
        {
           InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = ClientChooser.ShowBox();

            if (client == null || !client.LoggedIn)
            {
                MessageBox.Show("You must have at least one client open and logged in to start this program.");
                Application.Exit();
            }
            else
            {
                rune = RuneChooser.ShowBox();
                notifyIcon.Icon = new System.Drawing.Icon(GetType(), "icon.ico");
                start();
            }
        }

        private void start()
        {
            try
            {
                player = client.GetPlayer();
                uxShortTimer.Start();
                uxLongTimer.Start();
            }
            catch { }
        }

        private void EatFood()
        {
            Item food = client.Inventory.GetItems().FirstOrDefault(i => i.IsInList(Tibia.Constants.ItemLists.Food.Values));

            if (food != null)
                food.Use();
        }

        private void MakeRune()
        {
            // If we move an item from the ammo slot, store it here.
            Item itemMovedToAmmo = null; 

            // Make sure the player has enough mana
            if (player.Mana >= rune.Spell.ManaPoints)
            {
                // Find the first original
                Item original = client.Inventory.GetItems().FirstOrDefault(i=> i.Id == rune.OriginalItem.Id);

                // Make sure an original was found
                if (original != null)
                {
                    // Save the current location of the original
                    ItemLocation oldLocation = original.Location;

                    // The location where the item will be made
                    ItemLocation newLocation = null;

                    // Determine the location to make the item
                    if (client.Inventory.GetItemInSlot(
                        Tibia.Constants.SlotNumber.Right) != null)
                    {
                        newLocation = ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Right);
                    }
                    else if (client.Inventory.GetItemInSlot(
                        Tibia.Constants.SlotNumber.Left) != null)
                    {
                        newLocation = ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Left);
                    }

                    if (newLocation == null &&
                        client.Inventory.GetItemInSlot(
                        Tibia.Constants.SlotNumber.Ammo) != null)
                    {
                        // If no hands are free, but the ammo slot is, 
                        // move the right hand item to clear the ammo slot
                        newLocation = ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Right);
                        itemMovedToAmmo = client.Inventory.GetItemInSlot(Tibia.Constants.SlotNumber.Right);
                        itemMovedToAmmo.Move(ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Ammo));
                    }

                    // Move the original and say the magic words, make sure everything went well
                    Thread.Sleep(200);
                    original.Move(newLocation);
                    Thread.Sleep(200);
                    client.Console.Say(rune.Spell.Words);
                    Thread.Sleep(200);

                    // Build an item object for the newly created item
                    // We don't use getSlot because it could execute too fast, returning a blank
                    // rune or nothing at all. If we just send a packet, the server will catch up.
                    Item newItem = new Item(client, rune.Id, 0, "", newLocation);

                    // Move the rune back to it's original location
                    Thread.Sleep(300);
                    newItem.Move(oldLocation);
                    // Check if we moved an item to the ammo slot
                    // If we did, move it back
                    Thread.Sleep(200);
                    if (itemMovedToAmmo != null)
                    {
                        itemMovedToAmmo.Location = ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Ammo);
                        itemMovedToAmmo.Move(ItemLocation.FromSlot(Tibia.Constants.SlotNumber.Right));
                    }
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (client.LoggedIn)
            {
                if (player.Mana == player.Mana_Max)
                    MakeRune();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (client.LoggedIn)
            {
                EatFood();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void exitNotifyIconMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewForm preview = new PreviewForm();
            preview.showPreview(client);
        }
    }
}