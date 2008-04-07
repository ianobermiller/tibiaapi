using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace Tibia.Util
{
    public class Proxy
    {
        private byte[] xteaKey;

        private Socket socketClient;
        private Socket socketGame;

        private NetworkStream netStreamClient;
        private NetworkStream netStreamServer;
        private NetworkStream netStreamRemote;

        private static TcpListener tcpClient;
        private static TcpClient tcpServer;

        private const string Localhost = "127.0.0.1";
        private const int DefaultPort = 7171;

        private Objects.Client client;


        public void Initialize(Objects.Client c)
        {
            client = c;
            client.SetServer(Localhost, DefaultPort);


            //Get the xtea key
            xteaKey = client.ReadBytes(Addresses.Client.XTeaKey, 16);


            tcpClient = new TcpListener(IPAddress.Any, DefaultPort);
            tcpClient.Start();
            socketClient = tcpClient.AcceptSocket();

            if (socketClient.Connected)
            {
                //Connect the proxy to the login server.
                netStreamClient = new NetworkStream(socketClient);
                TcpClient tcpRemote = new TcpClient("62.146.31.166", DefaultPort);
                netStreamRemote = tcpRemote.GetStream();

                //The client is requesting the character list.
                Byte[] data = new byte[4096];
                int len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);

                //The character list along with the ip's of the worlds
                //are now being sent to the client.
                //Here we should decrypt this, parse it into data structures
                //and save them, change the ip addresses to 127.0.0.1,
                //and than encrypt it and send it to the client like normal.
                len = netStreamRemote.Read(data, 0, data.Length);
                netStreamClient.Write(data, 0, len);

                //The character has been selected. The client lets the
                //login server know of it's selection.
                len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);


                //Here we are looking at the character list in memory and
                //modifing the ip addresses. However this doesn't work.
                string oldip = SetCharacterListIp();

                //Wait for the client to connect to the proxy. Doesn't work.
                socketGame = tcpClient.AcceptSocket();
                netStreamClient = new NetworkStream(socketGame);

                //Client will send some data to the game world, relay it.
                len = netStreamClient.Read(data, 0, data.Length);

                //We connect to the game world and send the first
                //bit of data to the game world.
                tcpServer = new TcpClient(oldip, DefaultPort);
                netStreamServer = tcpServer.GetStream();
                netStreamServer.Write(data, 0, len);
            }
        }

        //Doesn't work, we need to work directly with the packets.
        //This code can easily be transformed to work with the packet data.
        private string SetCharacterListIp()
        {
            byte selectedchar = client.ReadByte(Addresses.Client.LoginSelectedChar);
            uint address = Addresses.Client.LoginCharList;

            string name;
            string servername;
            uint ipaddress = 0;
            byte[] ip = null;
            short port = 0;

            for (int i = 0; i < selectedchar + 1; ++i)
            {
                short namelength = client.ReadShort(address);
                address += 2;

                name = client.ReadString(address, (uint)namelength);
                address += (uint)namelength;

                short servernamelength = client.ReadShort(address);
                address += 2;

                servername = client.ReadString(address, (uint)servernamelength);
                address += (uint)servernamelength;

                ipaddress = address;
                ip = client.ReadBytes(address, 4);
                address += 4;

                port = client.ReadShort(address);
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

            return oldip;
        }
    }
}
