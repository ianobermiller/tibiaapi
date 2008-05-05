using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

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
    }
}
