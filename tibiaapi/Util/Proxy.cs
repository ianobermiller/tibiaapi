using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Tibia.Util
{
    class Proxy
    {
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

        private void Initialize(Objects.Client c)
        {
            client = c;
            client.SetServer(Localhost, DefaultPort);

            tcpClient = new TcpListener(IPAddress.Any, DefaultPort);
            tcpClient.Start();
            socketClient = tcpClient.AcceptSocket();

            if (socketClient.Connected)
            {
                netStreamClient = new NetworkStream(socketClient);
                TcpClient tcpRemote = new TcpClient(client.ReadString(Addresses.Client.LoginServerStart), DefaultPort);
                netStreamRemote = tcpRemote.GetStream();

                // Wait for the first chunk of data between the server and client (login list usually)
                Byte[] data = new byte[1024]; int len = 0;
                len = netStreamClient.Read(data, 0, data.Length);
                netStreamRemote.Write(data, 0, len);
            }
        }
    }
}
