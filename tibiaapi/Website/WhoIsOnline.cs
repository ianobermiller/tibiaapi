using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;

namespace Tibia
{
    public partial class Website
    {
        public delegate void WhoIsOnlineReceived(List<CharOnline> chars);

        public static void WhoIsOnline(string worldName, WhoIsOnlineReceived callback)
        {
            string url = "http://www.tibia.com/community/?subtopic=whoisonline&world=" + worldName;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.BeginGetResponse(delegate(IAsyncResult ar)
            {
                string html = GetHTML(ar);

                MatchCollection matches = Regex.Matches(html, @"<TD WIDTH=70%><[^<]*>([^<]*)</A></TD><TD WIDTH=10%>([^<]*)</TD><TD WIDTH=20%>([^<]*)</TD></TR>");
                List<CharOnline> chars = new List<CharOnline>(matches.Count);
                CharOnline co;

                for(int i = 0; i < matches.Count; i++)
                {
                    co = new CharOnline();
                    co.Name = HttpUtility.HtmlDecode(matches[i].Groups[1].Value);
                    co.Level = int.Parse(matches[i].Groups[2].Value);
                    co.Vocation = matches[i].Groups[3].Value;
                    chars.Add(co);
                }

                callback(chars);
            }, request);
        }

        public class CharOnline
        {
            public string Name;
            public int Level;
            public string Vocation;
        }
    }
}
