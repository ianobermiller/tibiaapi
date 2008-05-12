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

        public delegate void CommandListener(IrcMessage message);
        public CommandListener ReceivedCommand;

        public delegate void MessageListener(IrcMessage message);
        public MessageListener ReceivedMessage;

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
                string[] commandParts = command.Split('\n');
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
                for (int i = 0; i < commandParts.Length - 1; i++)
                {
                    IrcMessage message = new IrcMessage(commandParts[i]);
                    if (ReceivedCommand != null) ReceivedCommand(message);
                    if (commandParts[0].Length > 0)
                    {
                        switch (message.Type)
                        {
                            case MessageType.PING:
                                Ping(message);
                                break;
                            case MessageType.PRIVMSG:
                                ReceivedMessage(message);
                                break;
                        }
                    }
                }
            }

            if (messageQueue.Count > 0)
            {
                ProcessMessageQueue();
            }
        }
        public void Say(string Message)
        {
            writer.WriteLine(MessageType.PRIVMSG.ToString() +" "+ this.Channel + " :" + Message);
            writer.Flush();
        }
        #region Ping
        private void Ping(IrcMessage message)
        {
            string[] data = message.RawData.Split(' ');
            string pingHash = "";
            for (int intI = 1; intI < data.Length; intI++)
            {
                pingHash += data[intI] + " ";
            }
            writer.WriteLine("PONG " + pingHash);
            writer.Flush();
        }
        #endregion
    }
    public class IrcMessage
    {
        private string from;
        private string to;
        private string message;
        private string rawdata;
        private MessageType msgtype;

        public string From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        public string To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public string RawData
        {
            get { return this.rawdata; }
            set { this.rawdata = value; }
        }

        public MessageType Type
        {
            get { return this.msgtype; }
            set { this.msgtype = value; }
        }

        public IrcMessage(string Message)
        {
            this.rawdata = Message;
            Parse(Message);
        }

        public IrcMessage()
        {

        }

        public IrcMessage Parse(string Data)
        {
            IrcMessage result = new IrcMessage(); ;
            string line;
            string[] lines;

            int exppos;
            int colpos;
            if (Data[0] == ':')
            {
                line = Data.Substring(1);
            }
            else
            {
                line = Data;
            }
            lines = line.Split(new char[] { ' ' });
            try
            {
                result.msgtype = (MessageType)Enum.Parse(typeof(MessageType), lines[1], true);
            }
            catch (Exception e)
            {
                result.msgtype = MessageType.NULL;
            }
                exppos = lines[0].IndexOf("!");
            colpos = line.IndexOf(" :");
            to = lines[2];
            if (colpos != -1)
            {
                this.message = line.Substring(colpos + 2);
                this.message = this.message.Replace("\r\n", "");
            }
            if (exppos != -1)
            {
                this.from = lines[0].Substring(0, exppos);
            }
            return null;
        }
    }
    public enum MessageType
    {
        PRIVMSG,
        NOTICE,
        PING,
        NULL
    }
}
