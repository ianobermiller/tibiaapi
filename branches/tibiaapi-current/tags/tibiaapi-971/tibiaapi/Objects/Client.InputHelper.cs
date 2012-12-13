using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
                    SendMessage(Hooks.WM_CHAR, Convert.ToUInt32(c), 0);
            }

            /// <summary>
            /// Sends a key to the client
            /// </summary>
            /// <param name="key"></param>
            public void SendKey(Keys key)
            {
                SendMessage(Hooks.WM_KEYDOWN, (uint)key, 0);
                SendMessage(Hooks.WM_CHAR, (uint)key, 0);
                SendMessage(Hooks.WM_KEYUP, (uint)key, 0);
            }

            /// <summary>
            /// Sends a key to the client
            /// </summary>
            /// <param name="key"></param>
            public void SendKey(uint key)
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
            public void SendMessage(uint MessageId, uint wParam, uint lParam)
            {
                Util.WinApi.SendMessage(client.Window.Handle, MessageId, new UIntPtr(wParam), new UIntPtr(lParam));
            }

            /// <summary>
            /// Clicks with the mouse somewhere on the screen
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void Click(uint x, uint y)
            {
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, 0);
                uint lpara = Util.WinApi.MakeLParam(x, y);
                SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
                SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara);
            }
        }
    }
}
