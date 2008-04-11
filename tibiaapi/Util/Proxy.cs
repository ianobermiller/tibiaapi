using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Windows.Forms;

namespace Tibia.Util
{

    //The proxy seems to only work on the first character. Probably has to
    //do with the SetCharacterListIp method.

    public class Proxy
    {
        private Socket socketClient;

        private NetworkStream netStreamClient;
        private NetworkStream netStreamServer;
        private NetworkStream netStreamRemote;

        private static TcpListener tcpClient;
        private static TcpClient tcpServer;

        private const string Localhost = "127.0.0.1";
        private byte[] LocalhostBytes = new byte[]{ 127, 0, 0, 1 };
        private const int DefaultPort = 7171;
        private byte[] DefaultPortBytes = BitConverter.GetBytes((short)7171);

        private Objects.Client client;
        private CharListPacket charList;
        private byte selectedChar;

        private Thread clientthread;
        private Thread gamethread;

        byte[] data = new byte[4096];

        public int ClientSent = 0;
        public int ServerSent = 0;


        public void Initialize(Objects.Client c)
        {
            client = c;
            client.SetServer(Localhost, DefaultPort);

            //Listen for Tibia to connect to us.
            tcpClient = new TcpListener(IPAddress.Any, DefaultPort);
            tcpClient.Start();
            socketClient = tcpClient.AcceptSocket();

            if (socketClient.Connected)
            {
                netStreamClient = new NetworkStream(socketClient);

                //Connect the proxy to the login server.
                TcpClient tcpRemote = new TcpClient("62.146.31.166", DefaultPort);
                netStreamRemote = tcpRemote.GetStream();

                //The client is requesting the character list.
                int len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);

                //The character list along with the ip's of the worlds
                //are now being sent to the client.
                len = netStreamRemote.Read(data, 0, data.Length);

                //Process the charlist packet, changing IP's to localhost
                ProcessCharListPacket(data, len);

                // Write the packet back
                netStreamClient.Write(data, 0, len);

                //The character has been selected
                //Here we have to restart the listener because the client changes ports
                tcpClient.Stop();
                tcpClient.Start();
                socketClient = tcpClient.AcceptSocket();
                netStreamClient = new NetworkStream(socketClient);

                // Get the clients login info packet
                len = netStreamClient.Read(data, 0, data.Length);

                // Read the selection index from memory
                selectedChar = client.ReadByte(Addresses.Client.LoginSelectedChar);
                
                //We connect to the selected game world
                tcpServer = new TcpClient(charList.chars[selectedChar].worldIP, charList.chars[selectedChar].worldPort);
                netStreamServer = tcpServer.GetStream();

                // Write the login data to the game server
                netStreamServer.Write(data, 0, len);

                // Start asynchronous reading
                netStreamServer.BeginRead(data, 0, data.Length, (AsyncCallback)ServerRecieve, null); 
                netStreamClient.BeginRead(data, 0, data.Length, (AsyncCallback)ClientRecieve, null);
            }
        }

        private void ServerRecieve(IAsyncResult ar)
        {
            int bytesRead = netStreamServer.EndRead(ar);
            if (bytesRead > 0)
            {
                netStreamClient.Write(data, 0, bytesRead);
            }
            netStreamServer.BeginRead(data, 0, data.Length, (AsyncCallback)ServerRecieve, null);
        }

        private void ClientRecieve(IAsyncResult ar)
        {
            int bytesRead = netStreamClient.EndRead(ar);
            if (bytesRead > 0)
            {
                netStreamServer.Write(data, 0, bytesRead);
            }
            netStreamClient.BeginRead(data, 0, data.Length, (AsyncCallback)ClientRecieve, null);
        }

        private void ProcessCharListPacket(byte[] data, int length)
        {
            byte[] packet = new byte[length];
            byte[] key = client.ReadBytes(Addresses.Client.XTeaKey, 16);

            Array.Copy(data, packet, length);
            packet = XTEA.Decrypt(packet, key);

            // Make sure this is a login packet, not invalid login
            if (packet[2] == 0x14)
            {
                // Initialize the character list
                charList = new CharListPacket();
                int index = 0;

                charList.type = packet[2];
                
                index = 3; // MOTD length
                charList.lenMotd = BitConverter.ToInt16(packet, index);
                index += 2; // MOTD text
                charList.motd = Encoding.ASCII.GetString(packet, index, charList.lenMotd);
                index += charList.lenMotd + 1; // Number of chars (add one for the mysterious 0x64 byte)
                charList.numChars = packet[index];
                charList.chars = new CharListChar[charList.numChars];
                index += 1; // Length of first character's name

                for (int i = 0; i < charList.numChars; i++)
                {
                    charList.chars[i].lenCharName = BitConverter.ToInt16(packet, index);
                    index += 2; // Character name text
                    charList.chars[i].charName = Encoding.ASCII.GetString(packet, index, charList.chars[i].lenCharName);
                    index += charList.chars[i].lenCharName; // Length of world name
                    charList.chars[i].lenWorldName = BitConverter.ToInt16(packet, index);
                    index += 2; // World name text
                    charList.chars[i].worldName = Encoding.ASCII.GetString(packet, index, charList.chars[i].lenWorldName);
                    index += charList.chars[i].lenWorldName; // World IP Address
                    charList.chars[i].worldIP = IPBytesToString(packet, index);
                    Array.Copy(LocalhostBytes, 0, packet, index, 4);
                    index += 4; // World Port
                    charList.chars[i].worldPort = BitConverter.ToInt16(packet, index);
                    Array.Copy(DefaultPortBytes, 0, packet, index, 2);
                    index += 2; // Premium days or next chars name length
                }

                charList.premiumDays = BitConverter.ToInt16(packet, index);
            }

            packet = XTEA.Encrypt(packet, key);
            Array.Copy(packet, data, length);
        }

        /// <summary>
        /// Convert a 4 byte IP Address to a string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string IPBytesToString(byte[] data, int index)
        {
            return "" + data[index] + "." + data[index + 1] + "." + data[index + 2] + "." + data[index + 3];
        }
    }

    /// <summary>
    /// Stores the data from the server's character list packet.
    /// </summary>
    public struct CharListPacket
    {
        public byte type;
        public short lenMotd;
        public string motd;
        public byte numChars;
        public CharListChar[] chars;
        public short premiumDays;

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Type: " + type + "\n\r");
            s.Append("MOTD [" + lenMotd + "]: " + motd + "\n\r");
            s.Append("Characters: " + numChars + "\n\r");
            for(int i = 0; i < numChars; i++)
            {
                s.Append("Character " + i + "\n\r");
                s.Append("Name [" + chars[i].lenCharName + "]: " + chars[i].charName + "\n\r");
                s.Append("World Name [" + chars[i].lenWorldName + "]: " + chars[i].worldName + "\n\r");
                s.Append("World IP: " + chars[i].worldIP + "\n\r");
                s.Append("World Port: " + chars[i].worldPort + "\n\r");
            }
            return s.ToString();
        }
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
