using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Objects
{
    public class Hotkey
    {
        #region Variables
        private Client client;
        private byte number;
        #endregion

        #region Constructor
        public Hotkey(Client client, byte number)
        {
            if (number < 0 || number > Addresses.Hotkey.MaxHotkeys)
                throw new ArgumentOutOfRangeException("number", "Hotkey number must be between 0 and Addresses.Hotkey.MaxHotkeys.");
            this.client = client;
            this.number = number;
        }
        #endregion

        #region Properties
        public bool SendAutomatically
        {
            get
            {
                return Convert.ToBoolean(client.Memory.ReadByte(
                    Addresses.Hotkey.SendAutomaticallyStart + 
                    number * Addresses.Hotkey.SendAutomaticallyStep));
            }
            set
            {
                client.Memory.WriteByte(
                    Addresses.Hotkey.SendAutomaticallyStart + 
                    number * Addresses.Hotkey.SendAutomaticallyStep, Convert.ToByte(value));
            }
        }

        public string Text
        {
            get
            {
                return client.Memory.ReadString(
                    Addresses.Hotkey.TextStart +
                    number * Addresses.Hotkey.TextStep);
            }
            set
            {
                client.Memory.WriteString(
                    Addresses.Hotkey.TextStart +
                    number * Addresses.Hotkey.TextStep, value);
            }
        }

        public uint ObjectId
        {
            get
            {
                return client.Memory.ReadUInt32(
                    Addresses.Hotkey.ObjectStart +
                    number * Addresses.Hotkey.ObjectStep);
            }
            set
            {
                client.Memory.WriteUInt32(
                    Addresses.Hotkey.ObjectStart +
                    number * Addresses.Hotkey.ObjectStep, value);
            }
        }

        public Constants.HotkeyObjectUseType ObjectUseType
        {
            get
            {
                return (Constants.HotkeyObjectUseType)client.Memory.ReadUInt32(
                    Addresses.Hotkey.ObjectUseTypeStart +
                    number * Addresses.Hotkey.ObjectUseTypeStep);
            }
            set
            {
                client.Memory.WriteUInt32(
                    Addresses.Hotkey.ObjectUseTypeStart +
                    number * Addresses.Hotkey.ObjectUseTypeStep, (uint)value);
            }
        }

        public string Shortcut
        {
            get
            {
                int keyNum = (number + 1) % 12;
                if (keyNum == 0) keyNum = 12;
                string key = "F" + keyNum;
                string modifier = "";
                switch (number / 12)
                {
                    case 1:
                        modifier = "Shift + ";
                        break;
                    case 2:
                        modifier = "Control + ";
                        break;
                }
                return modifier + key;
            }
        }
        #endregion
    }
}
