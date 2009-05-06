using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Tibia.Objects;
using Tibia.Packets.RSA;
using Tibia.Packets;

namespace Tibia.Clientless
{
    public class GameServerConnection
    {
        Random rand = new Random();
        Socket gameSocket;
        byte[] xteaKey;
        string accName;
        string password;
        ushort version;
        bool ot;
        bool debug;
        byte os;
        string charName;
        string serverIp;
        ushort port;

        byte[] bufferServer = new byte[2];
        int readCount;
        ushort packetTrueLength;
        Queue<NetworkMessage> receiveQueue = new Queue<NetworkMessage>();

        public delegate void Notification(string message);


        #region Properties
        public uint[] XteaKey
        {
            get { return xteaKey.ToUInt32Array(); }
        }
        #endregion

        #region Constructors/Destructors
        public GameServerConnection(Constants.OperatingSystem opSystem, ushort version, string accountName,
            string password, bool openTibia, bool debug, CharacterLoginInfo charInfo) :
            this(opSystem, version, accountName, password, openTibia, debug, charInfo.CharName, charInfo.WorldIPString, charInfo.WorldPort) { }

        public GameServerConnection(Constants.OperatingSystem opSystem, ushort version, string accountName,
            string password, bool openTibia, bool debug, string charName, string serverIp, ushort port)
        {
            this.version = version;
            this.accName = accountName;
            this.password = password;
            this.ot = openTibia;
            this.debug = debug;
            this.os = (byte)opSystem;
            this.charName = charName;
            this.serverIp = serverIp;
            this.port = port;
        }
        #endregion

        public void ConnectCharacter()
        {
            try
            {
                gameSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                gameSocket.BeginConnect(serverIp, port, (AsyncCallback)GameServerConnected, null);
            }
            catch (Exception)
            {
            }
        }

        private void GameServerConnected(IAsyncResult ar)
        {
            try
            {
                gameSocket.EndConnect(ar);
                xteaKey = new byte[16];
                rand.NextBytes(xteaKey);
                gameSocket.Send(GameServerRequestPacket.Create(os, version, xteaKey, accName, charName, password, ot).Data);
                gameSocket.BeginReceive(bufferServer, 0, 2, SocketFlags.None, (AsyncCallback)GameServerReceive, null);
            }
            catch (Exception)
            {
                //System.Console.WriteLine(ex.ToString());
            }
        }

        private void GameServerReceive(IAsyncResult ar)
        {
            try
            {
                readCount = gameSocket.EndReceive(ar);
                if(readCount!=2)
                {
                    //some error
                }
                else
                {
                    packetTrueLength=BitConverter.ToUInt16(bufferServer,0);
                    NetworkMessage msg = new NetworkMessage(packetTrueLength+2);
                    Array.Copy(bufferServer, msg.GetBuffer(), 2);
                    while (readCount < packetTrueLength + 2)
                    {
                        readCount += gameSocket.Receive(msg.GetBuffer(), readCount, packetTrueLength + 2 - readCount, SocketFlags.None);
                    }

                    receiveQueue.Enqueue(msg);
                    ProcessReceiveQueue();

                    if(gameSocket.Connected)
                        gameSocket.BeginReceive(bufferServer, 0, 2, SocketFlags.None, (AsyncCallback)GameServerReceive, null);

                }
            }
            catch (Exception)
            {

            }
        }

        private void ProcessReceiveQueue()
        {

        }

        //public void SendPacket(byte[] packet)
        //{
        //    NetworkMessage msg = new NetworkMessage(packet);
        //    msg.XteaEncrypt(XteaKey);
        //    msg.InsertAdler32();
        //    if (gameSocket.Connected)
        //        gameSocket.Send(msg.Packet);
        //}

    }
}
