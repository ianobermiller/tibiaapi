using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class Pipe
    {
        #region Variables
        private Client client;
        private NamedPipeServerStream pipe;
        private byte[] buffer = new byte[2];
        private string name = string.Empty;
        #endregion

        #region Events
        /// <summary>
        /// A function prototype for Pipe notifications.
        /// </summary>
        /// <returns></returns>
        public delegate void PipeNotification();

        /// <summary>
        /// A generic function prototype for pipe events.
        /// </summary>
        /// <param name="packet">The packet that was received.</param>
        public delegate void PipeListener(NetworkMessage msg);

        /// <summary>
        /// Called when connected.
        /// </summary>
        public event PipeNotification OnConnected;

        /// <summary>
        ///  Called when data is received.
        /// </summary>
        public event PipeListener OnReceive;

        /// <summary>
        ///  Called when ContextMenuClick event is received.
        /// </summary>
        public event PipeListener OnContextMenuClick;

        /// <summary>
        ///  Called when an icon click event is received.
        /// </summary>
        public event PipeListener OnIconClick;

        /// <summary>
        ///  Called when a packet from the recv hook is received.
        /// </summary>
        public event PipeListener OnSocketRecv;

        /// <summary>
        ///  Called when a packet from the send hook is received.
        /// </summary>
        public event PipeListener OnSocketSend;

        /// <summary>
        ///  Called when data is sent.
        /// </summary>
        public event PipeListener OnSend;
        #endregion

        /// <summary>
        ///  Creates a Pipe to interact with an injected DLL or another program.
        /// </summary>
        public Pipe(Client client, string name)
        {
            this.client = client;
            this.name = name;
            pipe = new NamedPipeServerStream(name, PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipe.BeginWaitForConnection(new AsyncCallback(BeginWaitForConnection), null);
        }

        /// <summary>
        /// Returns the status of the pipe connection.
        /// </summary>
        public bool Connected
        {
            get { return pipe.IsConnected; }
        }


        private void BeginWaitForConnection(IAsyncResult ar)
        {
            pipe.EndWaitForConnection(ar);

            if (pipe.IsConnected)
            {
                // Call OnConnected asynchronously
                if (OnConnected != null)
                    OnConnected.BeginInvoke(null, null);

                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), null);
            }
        }

        private void BeginRead(IAsyncResult ar)
        {
            int read = pipe.EndRead(ar);

            if (read == 0)
                return;

            int length = BitConverter.ToInt16(buffer, 0)+2;

            if (length <= 2)
                return;

            byte[] received = new byte[length];
            Array.Copy(buffer, received, 2);
            
            int pos = 2;

            while (length - pos > 0)
            {
                pos += pipe.Read(received, pos, length - pos);
            }

            // Call OnReceive asynchronously
            if (OnReceive != null)
                OnReceive.BeginInvoke(new NetworkMessage(client, received, read), null, null);
            PipePacketType type = (PipePacketType)received[2];
            switch (type)
            {
                case PipePacketType.OnClickContextMenu:
                    if (OnContextMenuClick != null)
                        if(length == 7)
                            OnContextMenuClick.BeginInvoke(new NetworkMessage(client, received, length), null, null);
                    break;
                case PipePacketType.HookReceivedPacket:
                    if (OnSocketRecv != null)
                        OnSocketRecv.BeginInvoke(new NetworkMessage(client, received, length), null, null);
                    break;
                case PipePacketType.HookSentPacket:
                    if (OnSocketSend != null)
                        OnSocketSend.BeginInvoke(new NetworkMessage(client, received, length), null, null);
                    break;
                case PipePacketType.OnClickIcon:
                    if( OnIconClick !=null)
                        OnIconClick.BeginInvoke(new NetworkMessage(client, received, length),null,null);
                    break;
                default:
                    break;
            }

            pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), null);           
        }

        /// <summary>
        /// Sends packet to the destination.
        /// </summary>
        public void Send(NetworkMessage msg)
        {
            if (OnSend != null)
                OnSend.BeginInvoke(msg, null, null);

            pipe.Write(msg.Data, 0, msg.Length);
        }

        /// <summary>
        /// Gets the name of the pipe.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the client object.
        /// </summary>
        public Client Client
        {
            get { return client; }
        }
    }
}
