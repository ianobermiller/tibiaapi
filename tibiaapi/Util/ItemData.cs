using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Tibia.Objects;
using System.Threading;
using System.Globalization;
using Tibia.Constants;

namespace Tibia.Util
{
    public static class ItemData
    {
        private static string MainFileName = "ItemsData.cs";
        private static string ListsFileName = "ItemDataLists.cs";
        private static List<string> names;
        public static int Total;
        public static int Loaded;
        public static int Parsed;
        public static DataState State = DataState.Idle;
        public static Thread Thread;
        public static TextWriter MainTextWriter = null;
        public static TextWriter ListsTextWriter = null;
        public static bool Reset = false;

        public static void Run()
        {
            if (State != DataState.Idle)
                return;

            Loaded = 0;

            State = DataState.Starting;
            Directory.CreateDirectory("pages");

            Thread = new Thread(new ThreadStart(delegate()
            {
                if (File.Exists(MainFileName))
                {
                    File.Delete(MainFileName);
                }

                if (File.Exists(ListsFileName))
                {
                    File.Delete(ListsFileName);
                }

                names = new List<string>();
                ListsTextWriter = new StreamWriter(File.Create(ListsFileName)); ;
                ListsTextWriter.WriteLine("using System;");
                ListsTextWriter.WriteLine("using System.Collections.Generic;");
                ListsTextWriter.WriteLine("using Tibia.Objects;");
                ListsTextWriter.WriteLine("");
                ListsTextWriter.WriteLine("namespace Tibia.Constants {");
                ListsTextWriter.WriteLine(" public static class ItemDataLists {");
                ListsTextWriter.WriteLine("     #region All Items");
                ListsTextWriter.WriteLine("     public static Dictionary<string, ItemData> AllItems = new Dictionary<string, ItemData> {");

                MainTextWriter = new StreamWriter(File.Create(MainFileName)); ;
                MainTextWriter.WriteLine("using System;");
                MainTextWriter.WriteLine("using System.Collections.Generic;");
                MainTextWriter.WriteLine("using Tibia.Objects;");
                MainTextWriter.WriteLine("");
                MainTextWriter.WriteLine("namespace Tibia.Constants {");
                MainTextWriter.WriteLine("  public static class ItemsData {");

                Total = ItemLists.AllItems.Count;
                Loaded = 0;
                Parsed = 0;
                State = DataState.Downloading;

                foreach (var i in ItemLists.AllItems)
                {
                    if (File.Exists("pages\\" + i.Value.Name.Replace(' ', '_')) && !Reset)
                    {
                        if (++Loaded >= Total)
                        {
                            State = DataState.Waiting;
                        }

                        continue;
                    }

                    WebClient c = new WebClient();
                    c.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadCompleted);
                    c.DownloadFileAsync(new Uri("http://tibia.wikia.com/index.php?title=" + i.Value.Name.Replace(' ', '_') + "&printable=yes"), "pages\\" + i.Value.Name.Replace(' ', '_'));
                }

                while (State != DataState.Waiting)
                {
                    Thread.Sleep(100);
                }

                State = DataState.Parsing;

                foreach (var i in ItemLists.AllItems)
                {
                    if (names.Contains(i.Value.Name.Replace(" ", "").Replace("'", "")))
                    {
                        continue;
                    }

                    names.Add(i.Value.Name.Replace(" ", "").Replace("'", ""));
                    MainTextWriter.Write("      public static ItemData " + i.Value.Name.Replace(" ", "").Replace("'", "") + " = new ItemData(\"" + i.Value.Name.Replace('_', ' ') + "\", ");
                    MainTextWriter.Write(i.Value.Id + ", ");
                    StreamReader s = new StreamReader("pages\\" + i.Value.Name.Replace(' ', '_'));
                    ParseItem(s.ReadToEnd());
                    ListsTextWriter.WriteLine("         { ItemsData." + i.Value.Name.Replace(" ", "").Replace("'", "") + ".Name, ItemsData." + i.Value.Name.Replace(" ", "").Replace("'", "") + " },");
                    Parsed++;
                }

                State = DataState.Idle;
                ListsTextWriter.WriteLine("     };");
                ListsTextWriter.WriteLine("     #endregion");
                ListsTextWriter.WriteLine(" }");
                ListsTextWriter.WriteLine("}");
                ListsTextWriter.WriteLine("");

                MainTextWriter.WriteLine("  }");
                MainTextWriter.WriteLine("}");
                MainTextWriter.WriteLine("");

                MainTextWriter.Close();
                ListsTextWriter.Close();
            }));
            Thread.Start();
        }

        private static void DownloadCompleted(object o, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (++Loaded >= Total)
            {
                State = DataState.Waiting;
            }
        }

        private static string RegexGetGroup(string input, string pattern)
        {
            return Regex.Match(input, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase).Groups[1].Value;
        }

        private static void ParseItem(string html)
        {
            html = Regex.Replace(html, "<(.|\n)*?>", "");
            html = html.Replace("&nbsp;", "").Replace("&", "").Replace("and", ",").Replace("?", "");
            string attributes = RegexGetGroup(html, @"Attributes:$\n^(.*?)$");
            bool enchantable = attributes.Contains("Enchantable");
            double weight;
            double.TryParse(Regex.Replace(RegexGetGroup(html, @"Weight:$\n^(.*?)$"), "\\s[a-z]+\\.", "", RegexOptions.IgnoreCase), NumberStyles.Any, CultureInfo.CreateSpecificCulture("en-US"), out weight);
            int lootValue;
            int.TryParse(RegexGetGroup(html, @"Loot value:$\n^(.*?)$").Split('-')[0].Trim().Replace(",", ""), out lootValue);
            string droppedBy = RegexGetGroup(html, @"Dropped by:$\n^(.*?)$").Trim('.');
            MainTextWriter.Write(enchantable.ToString().ToLower() + ", ");
            MainTextWriter.Write(weight.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", ");
            MainTextWriter.Write(lootValue + ", ");

            string buffer;

            buffer = "";
            MainTextWriter.Write("new List<string>() { ");

            foreach (string i in droppedBy.Split(','))
            {
                string s = i.Trim();

                s = s.Split(' ')[0].Replace("?", "");
                buffer += "\"" + s + "\",";
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + " });");
        }
    }
}
