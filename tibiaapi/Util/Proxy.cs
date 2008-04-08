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
        private const int DefaultPort = 7171;

        private Objects.Client client;


        private Thread clientthread;
        private Thread gamethread;


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
                Byte[] data = new byte[4096];
                int len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);

                //The character list along with the ip's of the worlds
                //are now being sent to the client.
                len = netStreamRemote.Read(data, 0, data.Length);
                netStreamClient.Write(data, 0, len);

                //The character has been selected. The client lets the
                //login server know of it's selection.
                len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);


                //Here we are looking at the character list in memory and
                //modifing the ip addresses.
                string oldip = SetCharacterListIp();

                //Wait for the client to connect to the proxy.
                socketClient = tcpClient.AcceptSocket();
                netStreamClient = new NetworkStream(socketClient);

                //We connect to the game world.
                tcpServer = new TcpClient(oldip, DefaultPort);
                netStreamServer = tcpServer.GetStream();

                clientthread = new Thread(new ThreadStart(ClientThread));
                gamethread = new Thread(new ThreadStart(GameThread));

                //These threads will be killed when the application
                //running these threads quits.
                clientthread.IsBackground = true;
                gamethread.IsBackground = true;

                clientthread.Start();
                gamethread.Start();
            }
        }

        private void ClientThread()
        {
            byte[] data = new byte[4096];
            int len = 0;

            while (socketClient.Connected)
            {
                len = netStreamClient.Read(data, 0, data.Length);

                if (len > 0)
                {
                    netStreamServer.Write(data, 0, len);

                    ClientSent += len;
                }
            }
        }

        private void GameThread()
        {
            byte[] data = new byte[4096];
            int len = 0;

            while (tcpServer.Connected)
            {
                len = netStreamServer.Read(data, 0, data.Length);

                if (len > 0)
                {
                    netStreamClient.Write(data, 0, len);

                    ServerSent += len;
                }
            }
        }

        private string SetCharacterListIp()
        {
            byte selectedchar = client.ReadByte(Addresses.Client.LoginSelectedChar);
            uint address = (uint)client.ReadInt(Addresses.Client.LoginCharList);
            
            string name;
            string servername;
            uint ipaddress = 0;
            byte[] ip = null;
            short port = 0;

            for (int i = 0; i < selectedchar + 1; ++i)
            {
                name = client.ReadString(address);
                address += 30;

                servername = client.ReadString(address);
                address += 30;

                ipaddress = address;
                ip = client.ReadBytes(address, 4);
                address += 4;

                address += 16;

                port = client.ReadShort(address);
                address += 2;

                address += 2;
            }

            StringBuilder ipbuilder = new StringBuilder(15);
            ipbuilder.Append(ip[0].ToString());
            ipbuilder.Append('.');
            ipbuilder.Append(ip[1].ToString());
            ipbuilder.Append('.');
            ipbuilder.Append(ip[2].ToString());
            ipbuilder.Append('.');
            ipbuilder.Append(ip[3].ToString());

            string oldip = ipbuilder.ToString();

            byte[] newip = new byte[4];
            newip[0] = 127;
            newip[1] = 0;
            newip[2] = 0;
            newip[3] = 1;

            client.WriteBytes(ipaddress, newip, 4);
            client.WriteString(ipaddress + 4, "127.0.0.1");
            client.WriteByte(ipaddress + 4 + 10, 0);

            return oldip;
        }
    }
}
