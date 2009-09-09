using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartAutoLooter
{
    public partial class uxFilterInput : Form
    {
        public uxFilterInput()
        {
            InitializeComponent();
        }

        public int Ratio
        {
            get { return int.Parse(uxRatio.Text); }
            set { uxRatio.Text = value.ToString(); }
        }

        public byte ContainerNumber
        {
            get { return (byte)(numericUpDownContainer.Value - 1); }
            set { numericUpDownContainer.Value = value + 1; }
        }

        private void uxRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
