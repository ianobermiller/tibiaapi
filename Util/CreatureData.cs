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
    public static class CreatureData
    {
        private static List<string> DamageTypes = new List<string>();
        private static List<string> LootPossibilities = new List<string>();
        private static string MainFileName = "Creatures.cs";
        private static string ListsFileName = "CreatureLists.cs";
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

            foreach (DamageType d in Enum.GetValues(typeof(DamageType)))
                DamageTypes.Add(d.ToString());

            foreach (LootPossibility l in Enum.GetValues(typeof(LootPossibility)))
                LootPossibilities.Add(l.ToString());

            State = DataState.Starting;
            string url = "http://tibia.wikia.com/index.php?title=List_of_Creatures&printable=yes";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            Directory.CreateDirectory("pages");

            Thread = new Thread(new ThreadStart(delegate()
            {
                if (File.Exists(MainFileName))
                    File.Delete(MainFileName);
                if (File.Exists(ListsFileName))
                    File.Delete(ListsFileName);

                string html = GetHTML(request.GetResponse());

                ListsTextWriter = new StreamWriter(File.Create(ListsFileName)); ;
                ListsTextWriter.WriteLine("using System;");
                ListsTextWriter.WriteLine("using System.Collections.Generic;");
                ListsTextWriter.WriteLine("using Tibia.Objects;");
                ListsTextWriter.WriteLine("");
                ListsTextWriter.WriteLine("namespace Tibia.Constants {");
                ListsTextWriter.WriteLine(" public static class CreatureLists {");
                ListsTextWriter.WriteLine("     #region All Creatures");
                ListsTextWriter.WriteLine("     public static Dictionary<string, CreatureData> AllCreatures = new Dictionary<string, CreatureData> {");

                MainTextWriter = new StreamWriter(File.Create(MainFileName)); ;
                MainTextWriter.WriteLine("using System;");
                MainTextWriter.WriteLine("using System.Collections.Generic;");
                MainTextWriter.WriteLine("using Tibia.Objects;");
                MainTextWriter.WriteLine("");
                MainTextWriter.WriteLine("namespace Tibia.Constants {");
                MainTextWriter.WriteLine("  public static class Creatures {");

                ParseCreatures(html);

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

        public static string GetHTML(WebResponse response)
        {
            Stream respStream = response.GetResponseStream();
            string respBody = string.Empty;
            byte[] buf = new byte[8192];
            int count = 0;
            do
            {
                count = respStream.Read(buf, 0, buf.Length);
                if (count != 0)
                    respBody += Encoding.ASCII.GetString(buf, 0, count);
            }
            while (count > 0);
            return respBody;
        }

        private static void ParseCreatures(string html)
        {
            List<string> names = new List<string>();
            MatchCollection namesMatched = Regex.Matches(html, @"<td><a href=""/wiki/([^<]*)"" title=""", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            string[] removeIt = { "(Creature)", "(", ")", ".", "/", "-" };

            foreach (Match nameMatched in namesMatched)
            {
                string name = nameMatched.Groups[1].Value;
                foreach (string s in removeIt)
                    name = name.Replace(s, "");
                names.Add(name.Split('%')[0].Trim(" _".ToCharArray()));
            }

            Total = names.Count;
            Loaded = 0;
            Parsed = 0;
            State = DataState.Downloading;

            foreach (string name in names)
            {
                if (File.Exists("pages\\" + name) && !Reset)
                {
                    if (++Loaded >= Total)
                    {
                        State = DataState.Waiting;
                    }

                    continue;
                }

                WebClient c = new WebClient();
                c.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadCompleted);
                c.DownloadFileAsync(new Uri("http://tibia.wikia.com/index.php?title=" + name + "&printable=yes"), "pages\\" + name);
            }

            while (State != DataState.Waiting) Thread.Sleep(100);

            State = DataState.Parsing;

            foreach (string name in names)
            {
                MainTextWriter.Write("      public static CreatureData " + name.Replace("_", "") + " = new CreatureData(\"" + name.Replace('_', ' ') + "\", ");
                StreamReader s = new StreamReader("pages\\" + name);
                ParseCreature(s.ReadToEnd());
                ListsTextWriter.WriteLine("         { Creatures." + name.Replace("_", "") + ".Name, Creatures." + name.Replace("_", "") + " },");
                MainTextWriter.WriteLine("      );");
                Parsed++;
            }

            State = DataState.Idle;
        }

        private static void DownloadCompleted(object o, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (++Loaded >= Total)
                State = DataState.Waiting;
        }

        private static string RegexGetGroup(string input, string pattern)
        {
            return Regex.Match(input, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase).Groups[1].Value;
        }

        private static void ParseCreature(string html)
        {
            html = Regex.Replace(html, "<(.|\n)*?>", "");
            html = html.Replace("&nbsp;", "").Replace("&", "").Replace("and", ",").Replace("?", "");
            int hitPoints = -1;
            int.TryParse(RegexGetGroup(html, @"^(.*?) Hit points"), out hitPoints);
            int experiencePoints = -1;
            int.TryParse(RegexGetGroup(html, @"^(.*?) Experience points per kill"), out experiencePoints);
            string[] summon = RegexGetGroup(html, @"Summon/Convince:$\n^(.*?)$").Replace("(", "").Replace(")", "").Replace(" ", "/").Split('/');
            int summonMana = -1;
            int convinceMana = -1;
            if (summon.Length >= 2)
            {
                int.TryParse(RegexGetGroup(html, summon[0]), out summonMana);
                int.TryParse(RegexGetGroup(html, summon[1]), out convinceMana);
            }
            int maxDamage = -1;
            int.TryParse(RegexGetGroup(html, @"Est. Max. Damage:$\n^(.*?)\shp per turn$"), out maxDamage);
            bool canIllusion = (summon.Length > 2);
            string immunities = RegexGetGroup(html, @"Immune To:$\n^(.*?)$");
            bool canSeeInvisible = immunities.Contains("Invisibility");
            string strenghts = RegexGetGroup(html, @"Strong To:$\n^(.*?)$");
            string weakness = RegexGetGroup(html, @"Weak To:$\n^(.*?)$");
            string abilities = RegexGetGroup(html, @"Abilities:$\n^(.*?)$");
            string sounds = RegexGetGroup(html, @"Sounds:$\n^(.*?)$").Trim('.').Replace("\"", "");
            string loot = RegexGetGroup(html, @"Loot:$\n^(.*?)$");
            FrontAttack frontAttack = (abilities.Contains("wave") && abilities.Contains("beam")) ? FrontAttack.Both : (abilities.Contains("wave")) ? FrontAttack.Wave : (abilities.Contains("beam")) ? FrontAttack.Beam : (abilities.Contains("strike")) ? FrontAttack.Strike : FrontAttack.None;

            MainTextWriter.Write(hitPoints.ToString() + ", ");
            MainTextWriter.Write(experiencePoints.ToString() + ", ");
            MainTextWriter.Write(summonMana.ToString() + ", ");
            MainTextWriter.Write(convinceMana.ToString() + ", ");
            MainTextWriter.Write(maxDamage.ToString() + ", ");
            MainTextWriter.Write(canIllusion.ToString().ToLower() + ", ");
            MainTextWriter.Write(canSeeInvisible.ToString().ToLower() + ", ");
            MainTextWriter.WriteLine("FrontAttack." + frontAttack.ToString() + ", ");

            string buffer;

            buffer = "";
            MainTextWriter.Write("          new List<DamageType>() { ");

            foreach (string i in immunities.Split(','))
            {
                string s = i.Trim();

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s))
                {
                    buffer += "DamageType." + s + ",";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + "},");

            buffer = "";
            MainTextWriter.Write("          new List<DamageModifier>() { ");

            foreach (string i in strenghts.Split(','))
            {
                string s = i.Trim();
                int percent = -1;
                int.TryParse(RegexGetGroup(s, @"\((.*?)\)").Trim("-+%".ToCharArray()), out percent);

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s))
                {
                    buffer += "new DamageModifier(DamageType." + s + ", " + percent + "),";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + "},");

            buffer = "";
            MainTextWriter.Write("          new List<DamageModifier>() { ");

            foreach (string i in weakness.Split(','))
            {
                string s = i.Trim();
                int percent = -1;
                int.TryParse(RegexGetGroup(s, @"\((.*?)\)").Trim("-+%".ToCharArray()), out percent);

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s))
                {
                    buffer += "new DamageModifier(DamageType." + s + ", " + percent + "),";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + " },");

            buffer = "";
            MainTextWriter.Write("          new List<string>() { ");

            foreach (string i in sounds.Split(';'))
            {
                if (string.Compare(i, "none", true) == 0)
                    break;
                buffer += "\"" + i.Trim() + "\", ";
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + "},");

            buffer = "";
            MainTextWriter.Write("          new List<Loot>() { ");
            loot = loot.Replace("gp", "Gold Coin");

            foreach (string i in loot.Split(','))
            {
                string s = i.Trim().Split('.')[0];
                string p = Regex.Match(s, @"[\\(].*[\\)]", RegexOptions.Multiline | RegexOptions.IgnoreCase).ToString().Trim("()".ToCharArray());
                p = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p.Replace('-', ' ')).Replace(" ", "");
                string n = Regex.Match(s, @"[a-z]+[^\\(]*", RegexOptions.Multiline | RegexOptions.IgnoreCase).ToString().Trim();
                if (string.Compare(n, "nothing", true) == 0)
                    break;
                int max = 0;
                string[] l = s.Split(' ');
                if (l.Length > 0)
                    l = l[0].Split('-');
                if (l.Length > 1)
                    int.TryParse(l[1], out max);

                if (!LootPossibilities.Contains(p))
                    p = "Normal";

                s = s.Split(' ')[0].Replace("?", "");
                uint id = 0;
                Item item = ItemLists.AllItems.Find(delegate(Item find) { return string.Compare(find.Name, n, true) == 0; });

                if (item != null)
                    id = item.Id;

                buffer += "new Loot(\"" + n + "\", " + id + ", LootPossibility." + p + ", " + max + "),";
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            MainTextWriter.WriteLine(buffer + "}");
        }
    }

    public enum DataState
    {
        Idle,
        Starting,
        Downloading,
        Waiting,
        Parsing
    }
}
