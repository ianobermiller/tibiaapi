using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Tibia
{
    public partial class Website
    {
        private static HttpWebRequest request;

        public static string GetHTML(IAsyncResult ar)
        {
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
            return HttpUtility.HtmlDecode(Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value);
        }
    }
}
