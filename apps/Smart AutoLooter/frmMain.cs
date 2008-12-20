using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia;
using Tibia.Objects;
using Tibia.Packets;
using Tibia.Util;
using Tibia.Packets.Incoming;
using Tibia.Packets.Outgoing;
using System.Threading;

namespace SmartAutoLooter
{
    public partial class frmMain : Form
    {
        Client _client;
        bool _autoLoot;
        bool _autoOpenBodys;
        bool _autoEatFoodFromBodys;
        List<LootItem> _lootItems = new List<LootItem> { };
        AutoLootWait_t _autoLootWait = AutoLootWait_t.STOP;
        byte _container;
        static AutoResetEvent _autoLootAutoEvent = new AutoResetEvent(false); 

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _client = Tibia.Util.ClientChooser.ShowBox();

            if (_client != null)
            {
                if (!_client.LoggedIn)
                {

                    _client.Exited += new EventHandler(_client_Exited);

                    _client.StartProxy();
                    //autoloot
                    _client.Proxy.ReceivedTextMessageIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedTextMessageIncomingPacket);
                    _client.Proxy.ReceivedContainerUpdateItemIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedContainerUpdateItemIncomingPacket);
                    _client.Proxy.ReceivedContainerRemoveItemIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedContainerRemoveItemIncomingPacket);
                    _client.Proxy.ReceivedContainerAddItemIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedContainerAddItemIncomingPacket);
                    _client.Proxy.ReceivedTileAddThingIncomingPacket += new Proxy.IncomingPacketListener(_proxy_ReceivedTileAddThingIncomingPacket);
                    _client.Proxy.ReceivedContainerOpenIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedContainerOpenIncomingPacket);

                    _client.Proxy.ReceivedTurnOutgoingPacket += new Proxy.OutgoingPacketListener(Proxy_ReceivedTurnOutgoingPacket);
                }
                else
                    MessageBox.Show("Please start this program before login.");
            }
            else
                Application.Exit();

        }

        bool Proxy_ReceivedTurnOutgoingPacket(OutgoingPacket packet)
        {
            Tibia.Packets.Outgoing.TurnPacket p = (Tibia.Packets.Outgoing.TurnPacket)packet;

            return true;
        }

        void _client_Exited(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool isLootContainer(byte id)
        {
            return _lootItems.Find(delegate(LootItem lootItem) { return lootItem.Container == id; }) == null;
        }

        bool Proxy_ReceivedContainerOpenIncomingPacket(IncomingPacket packet)
        {
            if (_autoLoot && _lootItems.Count > 0)
            {
                ContainerOpenPacket p = (ContainerOpenPacket)packet;

                bool lootContainer = isLootContainer(p.Id);

                if (lootContainer && !Tibia.Constants.ItemLists.Backpacks.ContainsKey(p.ItemId))
                    Scheduler.addTask(new Action(autoLoot), null, 200);
                else if (!lootContainer)
                {
                    p.Name = "Container " + (p.Id + 1);

                    if (_autoLootWait == AutoLootWait_t.OPEN_NEW_LOOT_CONTAINER)
                    {
                        _autoLootWait = AutoLootWait_t.STOP;
                        Scheduler.addTask(new Action(autoLoot), null, 100);
                    }
                }
            }

            return true;
        }

        void autoLoot()
        {
            var containers = _client.Inventory.GetContainers();

            foreach (Tibia.Objects.Container c in containers)
            {
                if (isLootContainer(c.Number) && !Tibia.Constants.ItemLists.Backpacks.ContainsKey((uint)c.Id))
                {
                    var cItems = c.GetItems();

                    foreach (Item item in cItems)
                    {
                        var lootItem = _lootItems.Find(delegate(LootItem lItem) { return lItem.Id == item.Id; });

                        if (lootItem != null)
                        {
                            var lootContainer = _client.Inventory.GetContainer(lootItem.Container);

                            if (lootContainer != null)
                            {
                                if (item.GetFlag(Tibia.Addresses.DatItem.Flag.IsStackable))
                                {
                                    //o item é "stackable"
                                    var lootContainerItem = lootContainer.GetItem(delegate(Item lCItem) { return lCItem.Id == item.Id && lCItem.Count < 100; });

                                    if (lootContainerItem != null && (lootContainerItem.Count + item.Count <= 100 || lootContainer.Amount < lootContainer.Volume))
                                    {
                                        item.Move(lootContainerItem);

                                        //change others items location;
                                        foreach (Item i in cItems)
                                        {
                                            if (i.Loc.stackOrder > item.Loc.stackOrder)
                                            {
                                                i.Loc.stackOrder--;
                                                i.Loc.position--;
                                            }
                                        }

                                        _container = lootContainer.Number;
                                        _autoLootWait = AutoLootWait_t.MOVE_ITEM;
                                        _autoLootAutoEvent.WaitOne();

                                        //sleep.. para a msg chegar no client..
                                        Thread.Sleep(50);
                                    }
                                    else if (lootContainerItem == null && lootContainer.Amount < lootContainer.Volume)
                                    {
                                        item.Move(new ItemLocation(lootContainer.Number, (byte)(lootContainer.Volume - 1)));

                                        //change others items location...
                                        foreach (Item i in cItems)
                                        {
                                            if (i.Loc.stackOrder > item.Loc.stackOrder)
                                            {
                                                i.Loc.stackOrder--;
                                                i.Loc.position--;
                                            }
                                        }

                                        _container = lootContainer.Number;
                                        _autoLootWait = AutoLootWait_t.MOVE_ITEM;
                                        _autoLootAutoEvent.WaitOne();

                                        //sleep.. para a msg chegar no client..
                                        Thread.Sleep(50);
                                    }
                                    else
                                    {

                                        if (lootContainerItem != null)
                                        {
                                            item.Move(lootContainerItem);
                                            _container = lootContainer.Number;
                                            _autoLootWait = AutoLootWait_t.MOVE_ITEM;
                                            _autoLootAutoEvent.WaitOne();

                                        }

                                        //abrir um novo container...
                                        var newContainer = lootContainer.GetItem(delegate(Item newItemContainer) { return newItemContainer.GetFlag(Tibia.Addresses.DatItem.Flag.IsContainer); });

                                        if (newContainer != null)
                                        {
                                            newContainer.OpenContainer(lootContainer.Number);

                                            _container = lootContainer.Number;
                                            _autoLootWait = AutoLootWait_t.OPEN_NEW_LOOT_CONTAINER;
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (lootContainer.Amount < lootContainer.Volume)
                                    {
                                        item.Move(new ItemLocation(lootContainer.Number, (byte)(lootContainer.Volume - 1)));

                                        //change others items location...
                                        foreach (Item i in cItems)
                                        {
                                            if (i.Loc.stackOrder > item.Loc.stackOrder)
                                            {
                                                i.Loc.stackOrder--;
                                                i.Loc.position--;
                                            }
                                        }


                                        //esperar a chegada do item move?
                                        _container = lootContainer.Number;
                                        _autoLootWait = AutoLootWait_t.MOVE_ITEM;
                                        _autoLootAutoEvent.WaitOne();

                                        //sleep.. para a msg chegar no client..
                                        Thread.Sleep(50);
                                    }
                                    else
                                    {
                                        //abrir um novo container
                                        var newContainer = lootContainer.GetItem(delegate(Item newItemContainer) { return newItemContainer.GetFlag(Tibia.Addresses.DatItem.Flag.IsContainer); });

                                        if (newContainer != null)
                                        {
                                            newContainer.OpenContainer(lootContainer.Number);
                                            _container = lootContainer.Number;
                                            _autoLootWait = AutoLootWait_t.OPEN_NEW_LOOT_CONTAINER;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //abrir bag se tiver.. no mesmo container?

                    var bag = cItems.Find(delegate(Item i) { return i.GetFlag(Tibia.Addresses.DatItem.Flag.IsContainer); });

                    if (bag != null)
                    {
                        bag.OpenContainer(c.Number);
                        return;
                    }
                    else if (_autoEatFoodFromBodys)
                    {
                        var food = cItems.Find(delegate(Item i) { return Tibia.Constants.ItemLists.Foods.ContainsKey(i.Id); });

                        if (food != null)
                        {
                            food.Use();

                            _container = c.Number;
                            _autoLootWait = AutoLootWait_t.EAT_FOOD;
                            _autoLootAutoEvent.WaitOne();

                            //sleep.. para a msg chegar no client..
                            Thread.Sleep(50);
                        }
                    }
                }
            }
        }



        bool _proxy_ReceivedTileAddThingIncomingPacket(Tibia.Packets.IncomingPacket packet)
        {
            if (_autoOpenBodys)
            {
                TileAddThingPacket p = (TileAddThingPacket)packet;

                if (p.Item != null && p.Position.IsCloseTo(_client.Proxy.GetPlayer().Location))
                {
                    if (p.Item.GetFlag(Tibia.Addresses.DatItem.Flag.IsContainer) &&
                        p.Item.GetFlag(Tibia.Addresses.DatItem.Flag.IsCorpse))
                    {
                        p.Item.OpenContainer((byte)_client.Inventory.GetContainers().Count);
                    }
                }
            }

            return true;
        }

        bool Proxy_ReceivedContainerAddItemIncomingPacket(IncomingPacket packet)
        {
            if (_autoLootWait == AutoLootWait_t.MOVE_ITEM)
            {
                ContainerAddItemPacket p = (ContainerAddItemPacket)packet;
                if (p.Container == _container)
                {
                    _autoLootWait = AutoLootWait_t.STOP;
                    _autoLootAutoEvent.Set();
                }
            }

            return true;
        }

        bool Proxy_ReceivedContainerUpdateItemIncomingPacket(IncomingPacket packet)
        {
            if (_autoLootWait == AutoLootWait_t.EAT_FOOD || _autoLootWait == AutoLootWait_t.MOVE_ITEM)
            {
                ContainerUpdateItemPacket p = (ContainerUpdateItemPacket)packet;

                if (p.Container == _container)
                {
                    _autoLootWait = AutoLootWait_t.STOP;
                    _autoLootAutoEvent.Set();
                }
            }

            return true;
        }

        bool Proxy_ReceivedContainerRemoveItemIncomingPacket(IncomingPacket packet)
        {
            if (_autoLootWait == AutoLootWait_t.EAT_FOOD)
            {
                ContainerRemoveItemPacket p = (ContainerRemoveItemPacket)packet;

                if (p.Container == _container)
                {
                    _autoLootWait = AutoLootWait_t.STOP;
                    _autoLootAutoEvent.Set();
                }
            }

            return true;
        }

        bool Proxy_ReceivedTextMessageIncomingPacket(IncomingPacket packet)
        {

            if (_autoLootWait == AutoLootWait_t.EAT_FOOD)
            {
                TextMessagePacket p = (TextMessagePacket)packet;

                if (p.Message == "You are full.")
                {
                    _autoLootWait = AutoLootWait_t.STOP;
                    _autoLootAutoEvent.Set();
                }
            }

            return true;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAutoLootAdd frm = new frmAutoLootAdd();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LootItem lootItem = new LootItem(frm.ItemId, frm.ContainerNumber, frm.Comment);
                listBoxItems.Items.Add(lootItem);
                _lootItems.Add(lootItem);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lootItems.Remove((LootItem)listBoxItems.SelectedItem);
            listBoxItems.Items.Remove(listBoxItems.SelectedItem);
        }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            _autoLoot = checkBoxEnable.Checked;
        }

        private void checkBoxOpenBodys_CheckedChanged(object sender, EventArgs e)
        {
            _autoOpenBodys = checkBoxOpenBodys.Checked;
        }

        private void checkBoxEatFood_CheckedChanged(object sender, EventArgs e)
        {
            _autoEatFoodFromBodys = checkBoxEatFood.Checked;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Smart AutoLooter Files (*.salf)|*.salf";
            dialog.Title = "Save File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Tibia.Packets.NetworkMessage msg = new NetworkMessage(0);

                msg.AddUInt16((ushort)_lootItems.Count);

                foreach (LootItem i in _lootItems)
                {
                    msg.AddUInt16(i.Id);
                    msg.AddByte(i.Container);
                    msg.AddString(i.Comment);
                }

                System.IO.File.WriteAllBytes(dialog.FileName, msg.Packet);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Smart AutoLooter Files (*.salf)|*.salf";
            dialog.Title = "Open File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Tibia.Packets.NetworkMessage msg = new NetworkMessage(System.IO.File.ReadAllBytes(dialog.FileName));

                try
                {
                    ushort count = msg.GetUInt16();

                    for (int i = 0; i < count; i++)
                    {
                        LootItem lootItem = new LootItem(msg.GetUInt16(), msg.GetByte(), msg.GetString());
                        listBoxItems.Items.Add(lootItem);
                        _lootItems.Add(lootItem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lootItems.Clear();
            listBoxItems.Items.Clear();
        }


    }

    public class LootItem
    {
        public ushort Id { get; set; }
        public byte Container { get; set; }
        public string Comment { get; set; }

        public LootItem() { }
        public LootItem(ushort id, byte container, string comment)
        {
            Id = id;
            Container = container;
            Comment = comment;
        }

        public override string ToString()
        {
            return Id.ToString() + " Container " + (Container + 1).ToString() + " (" + Comment + ")";
        }

    }

    public enum AutoLootWait_t
    {
        STOP,
        MOVE_ITEM,
        OPEN_NEW_LOOT_CONTAINER,
        OPEN_BAG,
        EAT_FOOD,
    }

}
