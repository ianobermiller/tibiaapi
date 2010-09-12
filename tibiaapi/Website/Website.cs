using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Tibia
{
    public partial class Website
    {
        public static string GetHTML(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
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
