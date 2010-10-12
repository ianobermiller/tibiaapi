using System;
using System.IO.Pipes;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class Pipe
    {
        #region Variables
        private Client client;

        private NamedPipeServerStream pipeRecv;
        private NamedPipeServerStream pipeSend;

        byte[] buffer = new byte[1024 * 1024];
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
        public Pipe(Client client)
        {
            this.client = client;

            string name = "InjectedDllPipe_" + client.Process.Id + "_";

            pipeRecv = new NamedPipeServerStream(name + "1", PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipeRecv.BeginWaitForConnection(BeginWaitForConnection, pipeRecv);

            pipeSend = new NamedPipeServerStream(name + "2", PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipeSend.BeginWaitForConnection(BeginWaitForConnection, pipeSend);
        }

        /// <summary>
        /// Returns the status of the pipe connection.
        /// </summary>
        public bool Connected
        {
            get { return pipeRecv.IsConnected; }
        }

        private void BeginWaitForConnection(IAsyncResult ar)
        {
            NamedPipeServerStream pipe = ar.AsyncState as NamedPipeServerStream;
            pipe.EndWaitForConnection(ar);

            if (pipe.IsConnected)
            {
                // Call OnConnected asynchronously
                if (OnConnected != null)
                    OnConnected.BeginInvoke(null, null);

                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
        }

        private void BeginRead(IAsyncResult ar)
        {
            NamedPipeServerStream pipe = ar.AsyncState as NamedPipeServerStream;
            int length = pipeRecv.EndRead(ar);

            if (length > 0)
            {
                // Call OnReceive asynchronously
                if (OnReceive != null)
                    OnReceive.BeginInvoke(new NetworkMessage(client, buffer, length), null, null);

                NetworkMessage message = new NetworkMessage(client, buffer, length);
                PipePacketType type = (PipePacketType)message.GetByte();
                switch (type)
                {
                    case PipePacketType.OnClickContextMenu:
                        if (OnContextMenuClick != null)
                            OnContextMenuClick.BeginInvoke(message, null, null);
                        break;
                    case PipePacketType.HookReceivedPacket:
                        if (OnSocketRecv != null)
                            OnSocketRecv.BeginInvoke(message, null, null);
                        break;
                    case PipePacketType.HookSentPacket:
                        if (OnSocketSend != null)
                            OnSocketSend.BeginInvoke(message, null, null);
                        break;
                    case PipePacketType.OnClickIcon:
                        if (OnIconClick != null)
                            OnIconClick.BeginInvoke(message, null, null);
                        break;
                    default:
                        break;
                }
            }

            pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
        }

        /// <summary>
        /// Sends packet to the destination.
        /// </summary>
        public void Send(NetworkMessage msg)
        {
            if (OnSend != null)
                OnSend.BeginInvoke(msg, null, null);

            pipeSend.BeginWrite(msg.Data, 0, msg.Length, BeginWrite, null);
        }

        void BeginWrite(IAsyncResult ar)
        {
            pipeSend.EndWrite(ar);
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
