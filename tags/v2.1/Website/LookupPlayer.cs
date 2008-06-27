using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace Tibia
{
    public partial class Website
    {
        public delegate void LookupReceived(CharInfo info);
        private static HttpWebRequest request;

        public static void LookupPlayer(string playername, LookupReceived callback)
        {
            string url = "http://www.tibia.com/community/?subtopic=characters&name=" + playername;
            request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.BeginGetResponse((AsyncCallback)ResponseReceived, callback);
        }

        private static void ResponseReceived(IAsyncResult ar)
        {
            LookupReceived callback = (LookupReceived)ar.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
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

            callback(CharInfo.Parse(respBody));
        }

        public class CharInfo
        {
            public string Name;
            public string Sex;
            public string Profession;
            public int Level;
            public string World;
            public string Residence;
            public string House;
            public DateTime HousePaidUntil;
            public string GuildName;
            public string GuildTitle;
            public DateTime LastLogin;
            public string Comment;
            public string AccountStatus;

            public CharDeath[] deaths;

            public string RealName;
            public string Location;
            public DateTime Created;

            public CharStatus[] characters;

            public static CharInfo Parse(string html)
            {
                CharInfo i = new CharInfo();
                i.Name = Regex.Match(html, @"Name:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                i.Sex = Regex.Match(html, @"Sex:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                i.Profession = Regex.Match(html, @"Profession:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                i.Level = int.Parse(Regex.Match(html, @"Level:</TD><TD>([^<]*)</TD>").Groups[1].Value);
                i.World = Regex.Match(html, @"World:</TD><TD>([^<]*)<\/TD>").Groups[1].Value;
                i.Residence = Regex.Match(html, @"Residence:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                // Requires more complex parsing
                //i.LastLogin = DateTime.Parse(HttpUtility.HtmlDecode(Regex.Match(html, @"Last Login:<\/TD><TD>([^<]*)<\/TD>").Groups[1].Value));
                i.Comment = Regex.Match(html, @"Comment:</TD><TD>(.*?)</TD>", RegexOptions.Singleline).Groups[1].Value.Replace("<br />", string.Empty);
                i.AccountStatus = Regex.Match(html, @"Account&#160;Status:</TD><TD>([^<]*)</TD>").Groups[1].Value;


                i.RealName = Regex.Match(html, @"Real name:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                i.Location = Regex.Match(html, @"Location:</TD><TD>([^<]*)</TD>").Groups[1].Value;
                // Requires more complex parsing
                //i.Created = DateTime.Parse(HttpUtility.HtmlDecode(Regex.Match(html, @"Created:<\/TD><TD>([^<]*)<\/TD>").Groups[1].Value));
                //<TR BGCOLOR=#F1E0C6><TD WIDTH=25%>(.*?)</TD><TD>Killed at Level ([^ ]*) by
                // TODO finish this!
                return i;
            }
        }

        public class CharDeath
        {
            public DateTime time;
            public int AtLevel;
            public string[] KilledBy;
        }

        public class CharStatus
        {
            public string Name;
            public string World;
            public string Status;
        }
    }
}