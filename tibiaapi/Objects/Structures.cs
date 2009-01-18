using System;
using System.Text;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a Login Server
    /// </summary>
    public class LoginServer
    {
        public string Server { get; set; }
        public short Port { get; set; }

        public LoginServer() 
        {
            Server = "";
            Port = 7171;
        }

        public LoginServer(string server)
        {
            Port = 7171;
            Server = server;
        }

        public LoginServer(string server, short port)
        {
            Server = server;
            Port = port;
        }

        public override string ToString()
        {
            return Server + ":" + Port;
        }
    }

    public struct Channel
    {
        public Packets.ChatChannel Id;
        public string Name;

        public Channel(Packets.ChatChannel id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public struct Rect
    {
        private int top;
        private int bottom;
        private int left;
        private int right;
        private int width;
        private int height;

        public int Top
        {
            get { return top; }
        }

        public int Bottom
        {
            get { return bottom; }
        }

        public int Left
        {
            get { return left; }
        }

        public int Rigth
        {
            get { return right; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public Rect(Util.WinApi.RECT r)
        {
            top = r.top;
            bottom = r.bottom;
            left = r.left;
            right = r.right;
            width = r.right - r.left;
            height = r.bottom - r.top;
        }
    }

    public struct ClientPathInfo
    {
        public string Path;
        public string Version;

        public ClientPathInfo(string path, string version)
        {
            Path = path;
            Version = version;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Path, Version);
        }
    }

    public struct CharList
    {
        public string CharName { get; set; }
        public string WorldName { get; set; }
        public uint WorldIP { get; set; }
        public string WorldIPString { get; set; }
        public ushort WorldPort { get; set; }
    }


}
