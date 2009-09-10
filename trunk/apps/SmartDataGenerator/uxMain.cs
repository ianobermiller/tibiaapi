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
using System.Xml;
using System.Globalization;

namespace SmartDataGenerator
{
    public partial class uxMain : Form
    {
        private static string[] removeIt = { "(", ")", "-" };

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

        private static void WriteItemLine(ref StreamWriter writer, ref Dictionary<string, StreamWriter> writerList, ref List<uint> ids, ref List<string> names, string region, string name, uint id)
        {
            foreach (string s in removeIt)
            {
                name = name.Replace(s, "");
            }

            if (ids.Contains(id))
            {
                return;
            }

            string n = name.Replace(" ", "").Replace("'", "");

            byte mod = 0;

            while (names.Contains(n + ((mod > 0) ? mod.ToString() : "")))
            {
                mod++;
            }

            n += (mod > 0) ? mod.ToString() : "";
            names.Add(n);
            ids.Add(id);
            writer.WriteLine("public static Item " + n + " = new Item(" + id + ", \"" + name + "\");");
            writerList["All Items"].WriteLine("{ Items." + region + "." + n + ".Id, Items." + region + "." + n + " }, ");
            writerList[region].WriteLine("{ Items." + region + "." + n + ".Id, Items." + region + "." + n + " }, ");
        }

        private void uxLoadItemID_Click(object sender, EventArgs e)
        {
            #region Read XML/OTB

            Dictionary<string, List<ItemInfo>> itemsToAdd = new Dictionary<string, List<ItemInfo>>();
            ItemsReader.InitializeServerToClientDictionary();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("items.xml");
            XmlNodeList items = xDoc.GetElementsByTagName("item");

            for (int i = 0; i < items.Count; i++)
            {
                XmlNode item = items.Item(i);
                if (item.Attributes["id"] == null || item.Attributes["name"] == null)
                {
                    continue;
                }

                ushort serverId = ushort.Parse(item.Attributes["id"].Value);

                if (ItemsReader.ServerToClientDictionary.ContainsKey(serverId))
                {
                    ItemInfo info = new ItemInfo(ItemsReader.ServerToClientDictionary[serverId], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Attributes["name"].Value));
                    string category = "Others";

                    if (info.Name.Contains("Ladder"))
                    {
                        category = "UpUse";
                    }
                    else if ((new ushort[] { 384, 418, 8278, 8592 }).Contains(serverId))
                    {
                        category = "Rope";
                    }
                    else if (info.Name.Contains("Sewer"))
                    {
                        category = "DownUse";
                    }
                    else if ((new ushort[] { 468, 481, 483, 7932, 8579 }).Contains(serverId))
                    {
                        category = "Shovel";
                    }

                    for (int j = 0; j < item.ChildNodes.Count; j++)
                    {
                        XmlNode c = item.ChildNodes[j];

                        if (c.Attributes == null)
                        {
                            continue;
                        }

                        if (c.Attributes["key"] != null && c.Attributes["key"].Value == "weaponType")
                        {
                            category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.Attributes["value"].Value);
                        }
                        else if (c.Attributes["key"] != null && c.Attributes["key"].Value == "slotType")
                        {
                            if (c.Attributes["value"].Value == "feet")
                            {
                                category = "Boots";
                            }
                            else if (c.Attributes["value"].Value == "necklace")
                            {
                                category = "Neck";
                            }
                            else
                            {
                                category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.Attributes["value"].Value);
                            }
                        }
                        else if (c.Attributes["key"] != null && c.Attributes["key"].Value == "corpseType")
                        {
                            category = "Corpse";
                        }
                        else if (c.Attributes["key"] != null && c.Attributes["key"].Value == "floorchange")
                        {
                            if (c.Attributes["value"].Value == "down")
                            {
                                category = "Down";
                            }
                            else
                            {
                                category = "Up";
                            }
                        }
                    }

                    if (!itemsToAdd.ContainsKey(category))
                    {
                        itemsToAdd.Add(category, new List<ItemInfo>());
                    }

                    itemsToAdd[category].Add(info);
                }
            }

            #endregion

            StreamWriter writer = new StreamWriter(File.Create("Items.cs"));
            StreamWriter tileWriter = new StreamWriter(File.Create("Tiles.cs"));
            StreamWriter tileWriterLists = new StreamWriter(File.Create("TileLists.cs"));
            Dictionary<string, StreamWriter> writerList = new Dictionary<string, StreamWriter>();
            List<uint> ids = new List<uint>();
            List<string> names = new List<string>();
            bool regionOpen = false;
            bool runeRegion = false;
            string region = "";

            #region Headers

            writer.WriteLine("using System;");
            writer.WriteLine("using Tibia.Objects;");
            writer.WriteLine("");
            writer.WriteLine("namespace Tibia.Constants {");
            writer.WriteLine("public static class Items {");
            writer.WriteLine("public static int[] TreeArray = { 957, 958, 3608, 3609, 3613, 3614, 3615, 3616, 3617, 3618, 3619, 3620, 3621, 3622, 3623, 3624, 3625, 3626, 3631, 3632, 3633, 3634, 3635, 3636, 3637, 3638, 3639, 3640, 3641, 3647, 3649, 3687, 3688, 3689, 3691, 3692, 3694, 3742, 3743, 3744, 3745, 3750, 3751, 3752, 3753, 3754, 3755, 3756, 3757, 3758, 3759, 3760, 3761, 3762, 3780, 3871, 3872, 3873, 3877, 3878, 3884, 3885, 3899, 3901, 3902, 3903, 3905, 3908, 3909, 3910, 3911, 3920, 3921, 4433, 5091, 5092, 5093, 5094, 5095, 5155, 5156, 5389, 5390, 5391, 5392, 6094, 7020, 7021, 7022, 7023, 7024 };");
            writerList.Add("All Items", new StreamWriter(File.Create("AllItems.cs")));
            writerList["All Items"].WriteLine("#region All Items");
            writerList["All Items"].WriteLine("public static Dictionary<uint, Item> AllItems = new Dictionary<uint, Item>");
            writerList["All Items"].WriteLine("{");

            #endregion

            #region From Input

            foreach (string line in uxItems.Lines)
            {
                Match match;
                match = Regex.Match(line, @"^[a-z]+$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    if (itemsToAdd.ContainsKey(region))
                    {
                        foreach (ItemInfo info in itemsToAdd[region])
                        {
                            WriteItemLine(ref writer, ref writerList, ref ids, ref names, region, info.Name, info.Id);
                        }
                    }

                    if (regionOpen)
                    {
                        writer.WriteLine("}");
                        runeRegion = false;
                        writerList[region].WriteLine("};");
                        writerList[region].WriteLine("#endregion");
                    }

                    if (string.Compare(match.Value, "rune", true) == 0 || string.Compare(match.Value, "food", true) == 0)
                    {
                        runeRegion = true;
                    }

                    writer.WriteLine("");
                    writer.WriteLine("public static class " + match.Value + " {");
                    regionOpen = true;
                    region = match.Value;
                    writerList.Add(region, new StreamWriter(region.Replace(" ", "") + ".cs"));
                    writerList[region].WriteLine("");
                    writerList[region].WriteLine("#region " + region);
                    writerList[region].WriteLine("public static Dictionary<uint, Item> " + region.Replace(" ", "") + " = new Dictionary<uint, Item>");
                    writerList[region].WriteLine("{");
                    continue;
                }

                match = Regex.Match(line, @"\s[0-9]+$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    WriteItemLine(ref writer, ref writerList, ref ids, ref names, region, line.Replace(match.Value, ""), uint.Parse(match.Value.Trim()));
                }

                if (runeRegion)
                {
                    if (line.Length > 0)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            if (regionOpen)
            {
                writer.WriteLine("}");
                writerList[region].WriteLine("};");
                writerList[region].WriteLine("#endregion");
            }

            #endregion

            #region From OTB/XML

            foreach (string r in new string[] { "Corpse" })
            {
                if (itemsToAdd.ContainsKey(r))
                {
                    writerList.Add(r, new StreamWriter(r.Replace(" ", "") + ".cs"));
                    writer.WriteLine("public static class " + r + " {");
                    writerList[r].WriteLine("");
                    writerList[r].WriteLine("#region " + r);
                    writerList[r].WriteLine("public static Dictionary<uint, Item> " + r.Replace(" ", "") + " = new Dictionary<uint, Item>");
                    writerList[r].WriteLine("{");

                    foreach (ItemInfo info in itemsToAdd[r])
                    {
                        WriteItemLine(ref writer, ref writerList, ref ids, ref names, r, info.Name, info.Id);
                    }

                    writerList[r].WriteLine("};");
                    writerList[r].WriteLine("#endregion");
                    writer.WriteLine("}");
                }
            }

            #endregion

            #region Tiles

            tileWriter.WriteLine("using System;");
            tileWriter.WriteLine("using System.Collections.Generic;");
            tileWriter.WriteLine("using System.Reflection;");
            tileWriter.WriteLine("using Tibia.Objects;");
            tileWriter.WriteLine("");
            tileWriter.WriteLine("namespace Tibia.Constants");
            tileWriter.WriteLine("{");
            tileWriter.WriteLine("public static class Tiles");
            tileWriter.WriteLine("{");

            tileWriterLists.WriteLine("using System;");
            tileWriterLists.WriteLine("using System.Collections.Generic;");
            tileWriterLists.WriteLine("using System.Reflection;");
            tileWriterLists.WriteLine("using Tibia.Objects;");
            tileWriterLists.WriteLine("");
            tileWriterLists.WriteLine("namespace Tibia.Constants");
            tileWriterLists.WriteLine("{");
            tileWriterLists.WriteLine("public static class TileLists");
            tileWriterLists.WriteLine("{");

            foreach (string r in new string[] { "Up", "Down", "UpUse", "Rope", "DownUse", "Shovel" })
            {
                if (itemsToAdd.ContainsKey(r))
                {
                    tileWriter.WriteLine("public static class " + r);
                    tileWriter.WriteLine("{");
                    tileWriterLists.WriteLine("");
                    tileWriterLists.WriteLine("#region " + r);
                    tileWriterLists.WriteLine("public static List<uint> " + r.Replace(" ", "") + " = new List<uint>");
                    tileWriterLists.WriteLine("{");

                    foreach (ItemInfo info in itemsToAdd[r])
                    {
                        string name = info.Name;

                        foreach (string s in removeIt)
                        {
                            name = name.Replace(s, "");
                        }

                        string n = name.Replace(" ", "").Replace("'", "");

                        byte mod = 0;

                        while (names.Contains(n + ((mod > 0) ? mod.ToString() : "")))
                        {
                            mod++;
                        }

                        n += (mod > 0) ? mod.ToString() : "";
                        names.Add(n);
                        tileWriter.WriteLine("public static uint " + n + " = " + info.Id + ";");
                        tileWriterLists.WriteLine("Tiles." + r + "." + n + ", ");
                    }

                    tileWriterLists.WriteLine("};");
                    tileWriterLists.WriteLine("#endregion");
                    tileWriter.WriteLine("}");
                }
            }

            StreamReader footer = new StreamReader("footertiles.txt");
            tileWriter.Write(footer.ReadToEnd());
            footer.Close();

            tileWriter.WriteLine("");
            tileWriter.WriteLine("}");
            tileWriter.WriteLine("}");
            tileWriter.WriteLine("");

            tileWriterLists.WriteLine("}");
            tileWriterLists.WriteLine("}");
            tileWriterLists.WriteLine("");

            tileWriter.Close();
            tileWriterLists.Close();

            #endregion

            #region Save Lists

            writerList["All Items"].WriteLine("};");
            writerList["All Items"].WriteLine("#endregion");

            writer.WriteLine("}");
            writer.WriteLine("}");
            writer.WriteLine("");
            writer.Close();

            StreamWriter writerLists = new StreamWriter(File.Create("ItemLists.cs"));
            writerLists.WriteLine("using System;");
            writerLists.WriteLine("using System.Collections.Generic;");
            writerLists.WriteLine("using System.Text;");
            writerLists.WriteLine("using Tibia.Objects;");
            writerLists.WriteLine("");
            writerLists.WriteLine("namespace Tibia.Constants");
            writerLists.WriteLine("{");
            writerLists.WriteLine("public static class ItemLists");
            writerLists.WriteLine("{");

            foreach (var s in writerList)
            {
                string fileName = s.Key.Replace(" ", "") + ".cs";
                s.Value.Close();
                StreamReader reader = new StreamReader(fileName);
                writerLists.Write(reader.ReadToEnd());
                reader.Close();
                File.Delete(fileName);
            }

            writerLists.WriteLine("");

            footer = new StreamReader("footer.txt");
            writerLists.Write(footer.ReadToEnd());
            footer.Close();

            writerLists.WriteLine("");
            writerLists.WriteLine("}");
            writerLists.WriteLine("}");
            writerLists.WriteLine("");
            writerLists.Close();

            #endregion
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
