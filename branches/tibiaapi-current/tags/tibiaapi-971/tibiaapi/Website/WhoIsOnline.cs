using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Web;

namespace Tibia
{
    public partial class Website
    {
        public delegate void WhoIsOnlineReceived(List<CharOnline> chars);

        public static void WhoIsOnline(string worldName, WhoIsOnlineReceived callback)
        {
            string url = "http://www.tibia.com/community/?subtopic=worlds&world=" + worldName;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            new Thread(delegate()
            {
                HttpWebResponse r = (HttpWebResponse)request.GetResponse();
                string html = GetHTML(r);
                string nameMatch = "<a href=\"http://www\\.tibia\\.com/community/\\?subtopic=characters&name=[^<]*\" >([^<]*)</a></td>";
                string levelMatch = "<td style=\"width:10%;\" >([^<]*)</td>";
                string vocationMatch = "<td style=\"width:20%;\" >([^<]*)</td></tr>";
                MatchCollection matches = Regex.Matches(html, nameMatch + levelMatch + vocationMatch);
                List<CharOnline> chars = new List<CharOnline>(matches.Count);
                CharOnline co;

                for (int i = 0; i < matches.Count; i++)
                {
                    co = new CharOnline();
                    co.Name = Prepare(matches[i].Groups[1].Value);
                    co.Level = int.Parse(matches[i].Groups[2].Value);
                    co.Vocation = Prepare(matches[i].Groups[3].Value);
                    chars.Add(co);
                }

                callback(chars);
            }).Start();
        }

        public class CharOnline
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public string Vocation { get; set; }
        }
    }
}
