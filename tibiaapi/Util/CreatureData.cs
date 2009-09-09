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

namespace Tibia.Util {
    public static class CreatureData {
        public static string FileName = "Creatures.cs";
        public static int TotalCreatures;
        public static int LoadedCreatures;
        public static int ParsedCreatures;
        public static DataState State = DataState.Idle;
        public static TextWriter TextWriter = null;
        public static List<string> DamageTypes = new List<string>();
        public static List<string> LootPossibilities = new List<string>();
        public static Thread Thread;

        public static void CreaturesToFile() {
            if (State != DataState.Idle)
                return;

            LoadedCreatures = 0;

            foreach (DamageType d in Enum.GetValues(typeof(DamageType)))
                DamageTypes.Add(d.ToString());

            foreach (LootPossibility l in Enum.GetValues(typeof(LootPossibility)))
                LootPossibilities.Add(l.ToString());

            State = DataState.Starting;
            string url = "http://tibia.wikia.com/index.php?title=List_of_Creatures&printable=yes";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            Directory.CreateDirectory("pages");

            Thread = new Thread(new ThreadStart(delegate() {
                if (File.Exists(FileName))
                    File.Delete(FileName);

                TextWriter = new StreamWriter(File.Create(FileName)); ;
                TextWriter.WriteLine("using System;");
                TextWriter.WriteLine("using System.Collections.Generic;");
                TextWriter.WriteLine("using Tibia.Objects;");
                TextWriter.WriteLine("");
                TextWriter.WriteLine("namespace Tibia.Constants {");
                TextWriter.WriteLine("  public static class Creatures {");
                ParseCreatures(GetHTML(request.GetResponse()));
                TextWriter.WriteLine("  }");
                TextWriter.WriteLine("}");
                TextWriter.WriteLine("");

            }));
            Thread.Start();
        }

        public static string GetHTML(WebResponse response) {
            Stream respStream = response.GetResponseStream();
            string respBody = string.Empty;
            byte[] buf = new byte[8192];
            int count = 0;
            do {
                count = respStream.Read(buf, 0, buf.Length);
                if (count != 0)
                    respBody += Encoding.ASCII.GetString(buf, 0, count);
            }
            while (count > 0);
            return respBody;
        }

        private static void ParseCreatures(string html) {
            List<string> names = new List<string>();
            MatchCollection namesMatched = Regex.Matches(html, @"<td><a href=""/wiki/([^<]*)"" title=""", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match nameMatched in namesMatched)
                names.Add(Regex.Replace(nameMatched.Groups[1].Value, "\\(.*\\)", "").Split('%')[0].Trim(" _".ToCharArray()).Replace(".", "").Replace("/", ""));

            TotalCreatures = names.Count;
            LoadedCreatures = 0;
            ParsedCreatures = 0;
            State = DataState.Downloading;

            foreach (string name in names) {
                WebClient c = new WebClient();
                c.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadCompleted);
                c.DownloadFileAsync(new Uri("http://tibia.wikia.com/index.php?title=" + name + "&printable=yes"), Environment.CurrentDirectory + "\\pages\\" + name);
            }

            while (State != DataState.Waiting) Thread.Sleep(100);

            State = DataState.Parsing;

            foreach (string name in names) {
                TextWriter.Write("      public static CreatureData " + name.Replace("_", "") + " = new CreatureData(\"" + name.Replace('_', ' ') + "\", ");
                StreamReader s = new StreamReader("pages\\" + name);
                ParseCreature(s.ReadToEnd());
                TextWriter.WriteLine("      );");
                ParsedCreatures++;
            }

            State = DataState.Idle;
        }

        private static void DownloadCompleted(object o, System.ComponentModel.AsyncCompletedEventArgs e) {
            if (++LoadedCreatures >= TotalCreatures)
                State = DataState.Waiting;
        }

        private static string RegexGetGroup(string input, string pattern) {
            return Regex.Match(input, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase).Groups[1].Value;
        }

        private static void ParseCreature(string html) {
            html = Regex.Replace(html, "<(.|\n)*?>", "");
            html = html.Replace("&nbsp;", "").Replace("&", "").Replace("and", ",").Replace("?", "");
            int hitPoints = -1;
            int.TryParse(RegexGetGroup(html, @"^(.*?) Hit points"), out hitPoints);
            int experiencePoints = -1;
            int.TryParse(RegexGetGroup(html, @"^(.*?) Experience points per kill"), out experiencePoints);
            string[] summon = RegexGetGroup(html, @"Summon/Convince:$\n^(.*?)$").Replace("(", "").Replace(")", "").Replace(" ", "/").Split('/');
            int summonMana = -1;
            int convinceMana = -1;
            if (summon.Length >= 2) {
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

            TextWriter.Write(hitPoints.ToString() + ", ");
            TextWriter.Write(experiencePoints.ToString() + ", ");
            TextWriter.Write(summonMana.ToString() + ", ");
            TextWriter.Write(convinceMana.ToString() + ", ");
            TextWriter.Write(maxDamage.ToString() + ", ");
            TextWriter.Write(canIllusion.ToString().ToLower() + ", ");
            TextWriter.Write(canSeeInvisible.ToString().ToLower() + ", ");
            TextWriter.WriteLine("FrontAttack." + frontAttack.ToString() + ", ");

            string buffer;

            buffer = "";
            TextWriter.Write("          new List<DamageType>() { ");

            foreach (string i in immunities.Split(',')) {
                string s = i.Trim();

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s)) {
                    buffer += "DamageType." + s + ",";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            TextWriter.WriteLine(buffer + "},");

            buffer = "";
            TextWriter.Write("          new List<DamageModifier>() { ");

            foreach (string i in strenghts.Split(',')) {
                string s = i.Trim();
                int percent = -1;
                int.TryParse(RegexGetGroup(s, @"\((.*?)\)").Trim("-+%".ToCharArray()), out percent);

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s)) {
                    buffer += "new DamageModifier(DamageType." + s + ", " + percent + "),";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            TextWriter.WriteLine(buffer + "},");

            buffer = "";
            TextWriter.Write("          new List<DamageModifier>() { ");

            foreach (string i in weakness.Split(',')) {
                string s = i.Trim();
                int percent = -1;
                int.TryParse(RegexGetGroup(s, @"\((.*?)\)").Trim("-+%".ToCharArray()), out percent);

                s = s.Split(' ')[0].Replace("?", "");
                if (DamageTypes.Contains(s)) {
                    buffer += "new DamageModifier(DamageType." + s + ", " + percent + "),";
                }
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            TextWriter.WriteLine(buffer + " },");

            buffer = "";
            TextWriter.Write("          new List<string>() { ");

            foreach (string i in sounds.Split(';')) {
                if (string.Compare(i, "none", true) == 0)
                    break;
                buffer += "\"" + i.Trim() + "\", ";
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            TextWriter.WriteLine(buffer + "},");

            buffer = "";
            TextWriter.Write("          new List<Loot>() { ");
            loot = loot.Replace("gp", "gold coin");

            foreach (string i in loot.Split(',')) {
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
                buffer += "new Loot(ItemLists.AllItems.Find(delegate(Item i) { return i.Name.Equals(\"" + n + "\", StringComparison.OrdinalIgnoreCase); }), LootPossibility." + p + ", " + max + "),";
            }

            buffer = buffer.Trim(',').Replace(",", ", ");
            TextWriter.WriteLine(buffer + "}");
        }
    }

    public enum DataState {
        Idle,
        Starting,
        Downloading,
        Waiting,
        Parsing
    }
}
