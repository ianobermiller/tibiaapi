using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace Tibia.Util
{
    public class Irc
    {
        private string server = string.Empty;
        private int port = 6667;
        private string nick = string.Empty;
        private string user = string.Empty;
        private string realName = "TibiaAPI User";
        private string channel = string.Empty;

        TcpClient connection;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        byte[] readBuffer = new byte[8096];
        Queue<string[]> messageQueue = new Queue<string[]>();

        public delegate void CommandListener(string[] command);
        public CommandListener ReceivedCommand;

        #region Constructor
        public Irc(string user, string server)
        {
            this.user = user;
            nick = user;
            this.server = server;
        }
        #endregion

        #region Properties
        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        #endregion

        public void Connect()
        {
            connection = new TcpClient(server, port);
            stream = connection.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            stream.BeginRead(readBuffer, 0, readBuffer.Length, DataReceived, null);

            writer.WriteLine(String.Format("USER {0} 0 * :{1}", user, realName));
            writer.Flush();
            writer.WriteLine(String.Format("NICK {0}", nick));
            writer.Flush();
            writer.WriteLine(String.Format("JOIN {0}", channel));
            writer.Flush();
        }

        private void DataReceived(IAsyncResult ar)
        {
            int bytesRead = stream.EndRead(ar);

            if (bytesRead > 0)
            {
                string command = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(readBuffer, 0, bytesRead);
                string[] commandParts = command.Split(' ');

                messageQueue.Enqueue(commandParts);
            }

            ProcessMessageQueue();

            stream.BeginRead(readBuffer, 0, readBuffer.Length, DataReceived, null);
        }

        private void ProcessMessageQueue()
        {
            if (messageQueue.Count > 0)
            {
                string[] commandParts = messageQueue.Dequeue();

                if (ReceivedCommand != null) ReceivedCommand(commandParts);

                if (commandParts[0].Substring(0, 1) == ":")
                {
                    commandParts[0] = commandParts[0].Remove(0, 1);
                }

                if (commandParts[0] == "PING")
                {
                    // Server PING, send PONG back
                    Ping(commandParts);
                }
            }

            if (messageQueue.Count > 0)
            {
                ProcessMessageQueue();
            }
        }
        #region Ping
        private void Ping(string[] command)
        {
            string pingHash = "";
            for (int intI = 1; intI < command.Length; intI++)
            {
                pingHash += command[intI] + " ";
            }
            writer.WriteLine("PONG " + pingHash);
            writer.Flush();
        }
        #endregion
    }
}
