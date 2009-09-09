using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia.Util;
using System.IO;
using System.Text.RegularExpressions;

namespace SmartDataGenerator
{
    public partial class uxMain : Form
    {
        public uxMain()
        {
            InitializeComponent();
        }

        private void uxStart_Click(object sender, EventArgs e)
        {
            if (CreatureData.State == DataState.Idle)
            {
                CreatureData.Run();
                uxStart.Text = "Stop";
                uxTimer.Start();
            }
            else
            {
                CreatureData.Thread.Abort();
                CreatureData.ListsTextWriter.Close();
                CreatureData.MainTextWriter.Close();
                CreatureData.State = DataState.Idle;
                uxStart.Text = "Start";
                uxTimer.Stop();
                uxMessage.Text = "Idle";
            }
        }

        private void uxTimer_Tick(object sender, EventArgs e)
        {
            int percent = 0;
            if (CreatureData.State != DataState.Idle && CreatureData.Total > 0)
                percent = 100 * (3 * CreatureData.Loaded + CreatureData.Parsed) / (CreatureData.Total * 4);
            string message = CreatureData.State + " - " + ((CreatureData.State == DataState.Downloading) ? CreatureData.Loaded : CreatureData.Parsed) + "/" + CreatureData.Total + " (" + percent + "%)";
            uxMessage.Text = message;
            uxProgress.Value = percent;
        }

        private void uxMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CreatureData.Thread != null)
            {
                CreatureData.Thread.Abort();
                if (CreatureData.MainTextWriter != null)
                {
                    CreatureData.MainTextWriter.Close();
                    CreatureData.ListsTextWriter.Close();
                }
            }

            if (ItemData.Thread != null)
            {
                ItemData.Thread.Abort();
                if (ItemData.MainTextWriter != null)
                {
                    ItemData.MainTextWriter.Close();
                    ItemData.ListsTextWriter.Close();
                }
            }
        }
        private void uxLoadItemID_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(File.Create("Items.cs"));
            StreamWriter writerList = new StreamWriter(File.Create("ItemLists.cs"));
            List<uint> ids = new List<uint>();
            bool regionOpen = false;
            bool runeRegion = false;
            string region = "";
            string[] removeIt = { "(", ")" };
            writer.WriteLine("using System;");
            writer.WriteLine("using Tibia.Objects;");
            writer.WriteLine("");
            writer.WriteLine("namespace Tibia.Constants {");
            writer.WriteLine("  public static class Items {");
            writer.WriteLine("      public static int[] TreeArray = { 957, 958, 3608, 3609, 3613, 3614, 3615, 3616, 3617, 3618, 3619, 3620, 3621, 3622, 3623, 3624, 3625, 3626, 3631, 3632, 3633, 3634, 3635, 3636, 3637, 3638, 3639, 3640, 3641, 3647, 3649, 3687, 3688, 3689, 3691, 3692, 3694, 3742, 3743, 3744, 3745, 3750, 3751, 3752, 3753, 3754, 3755, 3756, 3757, 3758, 3759, 3760, 3761, 3762, 3780, 3871, 3872, 3873, 3877, 3878, 3884, 3885, 3899, 3901, 3902, 3903, 3905, 3908, 3909, 3910, 3911, 3920, 3921, 4433, 5091, 5092, 5093, 5094, 5095, 5155, 5156, 5389, 5390, 5391, 5392, 6094, 7020, 7021, 7022, 7023, 7024 };");

            foreach (string line in uxItems.Lines)
            {
                Match match;
                match = Regex.Match(line, @"^[a-z]+$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    if (regionOpen)
                    {
                        writer.WriteLine("      }");
                        runeRegion = false;
                    }

                    if (string.Compare(match.Value, "rune", true) == 0 || string.Compare(match.Value, "food", true) == 0)
                        runeRegion = true;

                    writer.WriteLine("      public static class " + match.Value + " {");
                    regionOpen = true;
                    region = match.Value;
                    continue;
                }

                match = Regex.Match(line, @"\s[0-9]+$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    string name = line.Replace(match.Value, "");

                    foreach (string s in removeIt)
                        name = name.Replace(s, "");

                    if (ids.Contains(uint.Parse(match.Value.Trim())))
                    {
                        MessageBox.Show("Duplicate item ID: " + match.Value.Trim());
                        continue;
                    }

                    ids.Add(uint.Parse(match.Value.Trim()));
                    writer.WriteLine("          public static Item " + name.Replace(" ", "").Replace("'", "") + " = new Item(" + match.Value.Trim() + ", \"" + name + "\");");
                    writerList.WriteLine("{ Items." + region + "." + name.Replace(" ", "").Replace("'", "") + ".Id, Items." + region + "." + name.Replace(" ", "").Replace("'", "") + " }, ");
                    continue;
                }

                if (runeRegion)
                    if (line.Length > 0)
                        writer.WriteLine("          " + line);
            }

            if (regionOpen)
                writer.WriteLine("      }");
            writer.WriteLine("  }");
            writer.WriteLine("}");
            writer.WriteLine("");
            writerList.Close();
            writer.Close();
        }

        private void uxItemStart_Click(object sender, EventArgs e)
        {
            if (ItemData.State == DataState.Idle)
            {
                ItemData.Run();
                uxItemStart.Text = "Stop";
                uxItemTimer.Start();
            }
            else
            {
                ItemData.Thread.Abort();
                ItemData.ListsTextWriter.Close();
                ItemData.MainTextWriter.Close();
                ItemData.State = DataState.Idle;
                uxItemStart.Text = "Start";
                uxItemTimer.Stop();
                uxMessage.Text = "Idle";
            }
        }

        private void uxItemTimer_Tick(object sender, EventArgs e)
        {
            int percent = 0;
            if (ItemData.State != DataState.Idle && ItemData.Total > 0)
                percent = 100 * (3 * ItemData.Loaded + ItemData.Parsed) / (ItemData.Total * 4);
            string message = ItemData.State + " - " + ((ItemData.State == DataState.Downloading) ? ItemData.Loaded : ItemData.Parsed) + "/" + ItemData.Total + " (" + percent + "%)";
            uxItemMessage.Text = message;
            uxItemProgress.Value = percent;
        }
    }
}
