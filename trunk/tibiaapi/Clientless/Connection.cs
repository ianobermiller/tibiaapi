using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Tibia.Objects;
using Tibia.Packets.RSA;
using Tibia.Packets;
using Tibia.Constants;

namespace Tibia.Clientless
{
    public class Connection
    {
        Random rand = new Random();
        Socket socket;
        byte[] xteaKey;

        private CharList[] charList;
        private static LoginServer[] loginServers = new LoginServer[] {
            new LoginServer("login01.tibia.com", 7171),
            new LoginServer("login02.tibia.com", 7171),
            new LoginServer("login03.tibia.com", 7171),
            new LoginServer("login04.tibia.com", 7171),
            new LoginServer("login05.tibia.com", 7171),
            new LoginServer("tibia01.cipsoft.com", 7171),
            new LoginServer("tibia02.cipsoft.com", 7171),
            new LoginServer("tibia03.cipsoft.com", 7171),
            new LoginServer("tibia04.cipsoft.com", 7171),
            new LoginServer("tibia05.cipsoft.com", 7171)
        };
        string AccName;
        string Password;
        ushort version;
        bool ot;
        bool debug;
        byte OS;


        byte[] dataLoginServer=new byte[1000];
        int LoginServerIndex;
        int MaxLoginServers;
        bool retry;


        /*
        Socket gameSocket;

        byte[] bufferServer = new byte[2];
        private byte[] bufferServer = new byte[2];
        private int readBytesServer;
        private int packetSizeServer;
        private bool writingServer;
        private Queue<NetworkMessage> serverSendQueue = new Queue<NetworkMessage> { };
        private Queue<NetworkMessage> serverReceiveQueue = new Queue<NetworkMessage> { };
        private ushort portServer = 0;
        private bool isFirstMsg;

        private byte[] bufferClient = new byte[2];
        private int readBytesClient;
        private int packetSizeClient;
        private bool writingClient;
        private DateTime lastClientWrite = DateTime.UtcNow;
        private Queue<NetworkMessage> clientSendQueue = new Queue<NetworkMessage> { };
        private Queue<NetworkMessage> clientReceiveQueue = new Queue<NetworkMessage> { };

        private bool acceptingConnection;
        private uint[] xteaKey;
        private bool isConnected;*/


        public Connection(OperationalSystem OpSystem, ushort Version, string AccountName, string Password, bool OpenTibia, bool Debug) :
            this(OpSystem,Version, AccountName, Password, OpenTibia, loginServers, Debug) { }

        public Connection(OperationalSystem OpSystem,ushort Version, string AccountName, string Password, bool OpenTibia,LoginServer[] ls,bool Debug)
        {
            version = Version;
            AccName = AccountName;
            this.Password = Password;
            ot = OpenTibia;
            debug = Debug;
            loginServers = ls;
            MaxLoginServers = ls.Length;
            OS = (byte)OpSystem;
        }

        public void GetCharacters(bool RetryIfError)
        {
            retry = RetryIfError;
            LoginServerIndex = 0;
            TryLoginServer(LoginServerIndex);
        }

        private void TryLoginServer(int index)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(loginServers[LoginServerIndex].Server,
                loginServers[LoginServerIndex].Port, (AsyncCallback)LoginServerConnected, null);
        }

        private void LoginServerConnected(IAsyncResult ar)
        {
            try
            {
                socket.EndConnect(ar);
                rand.NextBytes(xteaKey);
                socket.Send(LoginServerRequestPacket.CreateLoginServerRequestPacket(version, Signatures.Tibia840,
                    AccName, Password).Packet);
                socket.BeginReceive(dataLoginServer, 0, dataLoginServer.Length, SocketFlags.None, (AsyncCallback)LoginServerReceived, null);
            }
            catch(Exception ex)
            {

            }
        }

        private void LoginServerReceived(IAsyncResult ar)
        {
            try
            {
                int dataLength = socket.EndReceive(ar);
                if (dataLength > 0)
                {
                    byte[] tmp = new byte[dataLength];
                    Array.Copy(dataLoginServer, tmp, dataLength);
                    NetworkMessage msg = new NetworkMessage(tmp);
                    msg.PrepareToRead();
                    msg.GetUInt16(); 
                    while (msg.Position < msg.Length)
                    {
                        byte cmd = msg.GetByte();

                        switch (cmd)
                        {
                            case 0x0A: //Error message
                                /*Notification*/
                                msg.GetString();
                                break;
                            case 0x0B: //For your information
                                /*Notification*/
                                msg.GetString();
                                break;
                            case 0x14: //MOTD
                                /*Notification*/
                                msg.GetString();
                                break;
                            case 0x1E: //Patching exe/dat/spr messages
                            case 0x1F:
                            case 0x20:
                                /*Notification  "A new client is avalible, please download it first!"*/
                                return;
                            case 0x28: //Select other login server
                                if (retry)
                                {
                                    if (LoginServerIndex < MaxLoginServers - 1)
                                        TryLoginServer(LoginServerIndex++);
                                    else { }//tried all login servers and couldnt get character list, throw a notification
                                }
                                break;
                            case 0x64: //character list
                                int nChar = (int)msg.GetByte();
                                charList = new CharList[nChar];

                                for (int i = 0; i < nChar; i++)
                                {
                                    charList[i].CharName = msg.GetString();
                                    charList[i].WorldName = msg.GetString();
                                    charList[i].WorldIP = msg.GetUInt32();
                                    charList[i].WorldPort = msg.GetUInt16();
                                }
                                //socket.BeginDisconnect(true, (AsyncCallback)LoginServerDisconnect, null);
                                return;
                            default:
                                //Notify about an unknown message
                                //socket.BeginDisconnect(true, (AsyncCallback)LoginServerDisconnect, null);
                                break;
                        }
                    }
                }
                else //we didn't receive anything
                {   
                    if(retry)
                    {
                        if(LoginServerIndex<MaxLoginServers-1)
                            TryLoginServer(LoginServerIndex++);
                        else {}//tried all login servers and couldnt get character list, throw a notification
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }        

        public class Signatures //4 bytes each file(tibia.dat,tibia.spr,tibia.pic)
        {
            public static byte[] Tibia840 = new byte[12]{0x7A,0x60,0x3D,0x49,
                                                        0x7C,0x4E,0x3D,0x49,
                                                        0x78,0x41,0x14,0x49};
        }


    }
}
