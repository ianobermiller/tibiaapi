using System;
using System.Runtime.InteropServices;

namespace Tibia
{
    public class KeyboardHook
    {
        public Objects.Client client;
        public KeyboardHook(Objects.Client c)
        {
            client = c;
        }
        [DllImport("user32.dll", SetLastError=true)]
        public static extern int RegisterHotKey(Int32 hProcess, int id, int fsModifiers, int vk);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int UnregisterHotKey(Int32 hProcess, int id);
        private const int MOD_ALT = 1;
        private const int MOD_CONTROL = 2;
        private const int MOD_SHIFT = 4;
        private const int MOD_WIN = 8;
        short hotkeyID;
        public enum Keys : int
        {
            Insert = 0x2D,       
        }
        public void RegisterGlobalHotkey(Keys hotkey,int id, int modifiers)
        {
            try
            {
                if (RegisterHotKey(client.getProcess().Id, id, modifiers, (int)hotkey) == 0)
                {
                    throw new Exception("Doesnt work: " + Marshal.GetLastWin32Error().ToString());
                }
                
            }
            catch (Exception e)
            {
                UnregisterGlobalHotkey(id);
            }
        }
        public void UnregisterGlobalHotkey(int id)
        {
            UnregisterHotKey(client.getProcess().Id, id);
        }
    }
}
