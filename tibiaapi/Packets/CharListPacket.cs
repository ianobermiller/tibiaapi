using System;
using System.Text;

namespace Tibia.Packets
{
    /// <summary>
    /// Represents a character list packet from the server
    /// </summary>
    public class CharListPacket : Packet
    {
        public short lenMotd;
        public string motd;
        public byte charCount;
        public CharListChar[] chars;
        public short premiumDays;

        public CharListPacket()
        {
            type = PacketType.CharList;
            destination = PacketDestination.Client;
        }

        public CharListPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet, byte[] newServer, byte[] newPort)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CharList) return false;
                int index = 0;

                index = 3; // MOTD length
                lenMotd = BitConverter.ToInt16(packet, index);
                index += 2; // MOTD text
                motd = Encoding.ASCII.GetString(packet, index, lenMotd);
                index += lenMotd + 1; // Number of chars (add one for the mysterious 0x64 byte)
                charCount = packet[index];
                chars = new CharListChar[charCount];
                index += 1; // Length of first character's name

                for (int i = 0; i < charCount; i++)
                {
                    chars[i].lenCharName = BitConverter.ToInt16(packet, index);
                    index += 2; // Character name text
                    chars[i].charName = Encoding.ASCII.GetString(packet, index, chars[i].lenCharName);
                    index += chars[i].lenCharName; // Length of world name
                    chars[i].lenWorldName = BitConverter.ToInt16(packet, index);
                    index += 2; // World name text
                    chars[i].worldName = Encoding.ASCII.GetString(packet, index, chars[i].lenWorldName);
                    index += chars[i].lenWorldName; // World IP Address
                    chars[i].worldIP = IPBytesToString(packet, index);
                    Array.Copy(newServer, 0, packet, index, 4);
                    index += 4; // World Port
                    chars[i].worldPort = BitConverter.ToInt16(packet, index);
                    Array.Copy(newPort, 0, packet, index, 2);
                    index += 2; // Premium days or next chars name length
                }

                premiumDays = BitConverter.ToInt16(packet, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Type: " + type + "\n\r");
            s.Append("MOTD [" + lenMotd + "]: " + motd + "\n\r");
            s.Append("Characters: " + charCount + "\n\r");
            for (int i = 0; i < charCount; i++)
            {
                s.Append("Character " + i + "\n\r");
                s.Append("Name [" + chars[i].lenCharName + "]: " + chars[i].charName + "\n\r");
                s.Append("World Name [" + chars[i].lenWorldName + "]: " + chars[i].worldName + "\n\r");
                s.Append("World IP: " + chars[i].worldIP + "\n\r");
                s.Append("World Port: " + chars[i].worldPort + "\n\r");
            }
            return s.ToString();
        }

        /// <summary>
        /// Represents one character in the server's character list packet.
        /// </summary>
        public struct CharListChar
        {
            public short lenCharName;
            public string charName;
            public short lenWorldName;
            public string worldName;
            public string worldIP;
            public short worldPort;
        }
    }
}
