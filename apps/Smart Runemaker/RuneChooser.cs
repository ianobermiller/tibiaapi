using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartRunemaker
{
    public partial class RuneChooser : Form
    {
        private static RuneChooser newRuneChooser;
        private static Tibia.Objects.Rune rune;
        
        public RuneChooser()
        {
            InitializeComponent();
            rune = null;
        }

        public static Tibia.Objects.Rune ShowBox()
        {
            IList<Tibia.Objects.Rune> runeList = new List<Tibia.Objects.Rune>(Tibia.Constants.ItemLists.Runes.Values);
            newRuneChooser = new RuneChooser();
            newRuneChooser.uxRunes.DataSource = runeList;
            newRuneChooser.uxRunes.SelectedItem = Tibia.Constants.Items.Rune.SuddenDeath.Id;
            newRuneChooser.ShowDialog();
            return rune;
        }

        private void uxChoose_Click(object sender, EventArgs e)
        {
            rune = (Tibia.Objects.Rune)uxRunes.SelectedItem;
            newRuneChooser.Dispose();
        }
    }
}