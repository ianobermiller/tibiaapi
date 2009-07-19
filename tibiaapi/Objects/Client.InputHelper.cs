using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class InputHelper
        {
            private Client client;

            internal InputHelper(Client client) { this.client = client; }

            /// <summary>
            /// Sends a string to the client
            /// </summary>
            /// <param name="s"></param>
            public void SendString(string s)
            {
                foreach (var c in s)
                    SendKey(Convert.ToInt32(c));
            }

            /// <summary>
            /// Sends a key to the client
            /// </summary>
            /// <param name="key"></param>
            public void SendKey(Keys key)
            {
                SendMessage(Hooks.WM_KEYDOWN, (int)key, 0);
                SendMessage(Hooks.WM_CHAR, (int)key, 0);
                SendMessage(Hooks.WM_KEYUP, (int)key, 0);
            }

            /// <summary>
            /// Sends a key to the client
            /// </summary>
            /// <param name="key"></param>
            public void SendKey(int key)
            {
                SendMessage(Hooks.WM_KEYDOWN, key, 0);
                SendMessage(Hooks.WM_CHAR, key, 0);
                SendMessage(Hooks.WM_KEYUP, key, 0);
            }

            /// <summary>
            /// Wrapper for SendMessage function
            /// </summary>
            /// <param name="MessageId"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            public void SendMessage(uint MessageId, int wParam, int lParam)
            {
                Util.WinApi.SendMessage(client.Window.Handle, MessageId, wParam, lParam);
            }

            /// <summary>
            /// Clicks with the mouse somewhere on the screen
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void Click(int x, int y)
            {
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, 0);
                int lpara = Util.WinApi.MakeLParam(x, y);
                SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara);
            }
        }
    }
}
