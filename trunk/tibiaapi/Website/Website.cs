using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Tibia
{
    public partial class Website
    {
        public static string GetHTML(HttpWebResponse r)
        {
            StreamReader readStream = new StreamReader(r.GetResponseStream());
            string respBody = readStream.ReadToEnd();
            readStream.Close();
            return respBody;
        }

        public static string Match(string html, string pattern)
        {
            return Prepare(
                Regex.Match(
                    html,
                    pattern,
                    RegexOptions.IgnoreCase | RegexOptions.Singleline)
                .Groups[1].Value);
        }

        public static string Prepare(string text)
        {
            return System.Web.HttpUtility.HtmlDecode(text) // Decode html character entities
                .Replace((char)0xA0, ' '); // Replace non-breaking spaces
        }
    }
}
