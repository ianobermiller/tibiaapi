using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Tibia.Exceptions
{
    /// <summary>
    /// Exception used mainly in Objects.Client when a player is not logged in.
    /// </summary>
    public class NotLoggedInException : InvalidOperationException { }

    /// <summary>
    /// Thrown when a method requires the client to be using a proxy, ie. sending a packet to the client.
    /// </summary>
    public class ProxyRequiredException : InvalidOperationException { }

    /// <summary>
    /// Thrown when a method expects the proxy to be connected, but it is not.
    /// </summary>
    public class ProxyDisconnectedException : InvalidOperationException { }

    public class Handler
    {
        public static void Handle(Exception e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("TibiaAPI has encountered an error. Please report the text of this error to the developer of this program. You can copy the text by pressing ctrl+c.");
            sb.AppendLine();
            sb.AppendLine(e.ToString());

            MessageBox.Show(sb.ToString(), "TibiaAPI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
