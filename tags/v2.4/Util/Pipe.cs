using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;
using Tibia.Objects;
using Tibia.Packets;

namespace Tibia.Util
{
    public class Pipe
    {
        #region Variables
        private Client client;
        private NamedPipeServerStream pipe;
        private byte[] buffer = new byte[1024];
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
        public delegate void PipeListener(Packet packet);

        /// <summary>
        /// Called when connected.
        /// </summary>
        public PipeNotification OnConnected;

        /// <summary>
        ///  Called when data is received.
        /// </summary>
        public PipeListener OnReceive;

        /// <summary>
        ///  Called when data is sent.
        /// </summary>
        public PipeListener OnSend;
        #endregion

        /// <summary>
        ///  Creates a Pipe to interact with an injected DLL or another program.
        /// </summary>
        public Pipe(Client c, string name)
        {
            client = c;
            this.name = name;
            pipe = new NamedPipeServerStream(name, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
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
            pipe.EndRead(ar);
            // Call OnReceive asynchronously
            if (OnReceive != null)
                OnReceive.BeginInvoke(new Packet(client, buffer), null, null);
            pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), null);           
        }

        /// <summary>
        /// Sends packet to the destination.
        /// </summary>
        public void Send(Packet packet)
        {
            if (OnSend != null)
                OnSend.BeginInvoke(packet,null,null);
            pipe.Write(packet.Data, 0, packet.Data.Length);
        }

        /// <summary>
        /// Gets the name of the pipe.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the client object.
        /// </summary>
        public Client Client
        {
            get
            {
                return client;
            }
        }
    }
}
