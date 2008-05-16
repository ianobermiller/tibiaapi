using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;


namespace Tibia.Util
{
    class Pipe
    {
        #region Variables
        private NamedPipeServerStream pipe;
        private byte[] buffer = new byte[1024];
        #endregion

        #region Events
        /// <summary>
        /// A function prototype for Pipe notifications.
        /// </summary>
        /// <returns></returns>
        public delegate void PipeNotification();

        /// <summary>
        /// A generic function prototype for packet events.
        /// </summary>
        /// <param name="packet">The packet that was received.</param>
        public delegate void PacketListener(PacketBuilder packet);

        /// <summary>
        /// Called when initiating a connection.
        /// </summary>
        public PipeNotification OnConnect;

        // <summary>
        // Called when connected.
        // </summary>
        public PipeNotification OnConnected;

        // <summary>
        //  Called when data is received.
        // </summary>
        public PacketListener OnReceive;

        // <summary>
        //  Called when data is sent.
        // </summary>
        public PacketListener OnSend;
        #endregion

        // <summary>
        //  Creates a Pipe to interact with an injected DLL or another program.
        // </summary>
        public Pipe(string name)
        {
            pipe = new NamedPipeServerStream(name);
            pipe.BeginWaitForConnection(new AsyncCallback(BeginWaitForConnection), null);
        }

        // <summary>
        // Returns the status of the pipe connection.
        // </summary>
        public bool IsConnected
        {
            get { return pipe.IsConnected(); }
        }


        private void BeginWaitForConnection(IAsyncResult AR)
        {
            OnConnect();
            pipe.WaitForConnection();
            if (IsConnected())
            {
                OnConnected();
                pipe.EndWaitForConnection();
                pipe.BeginRead(buffer, 0, 1023, new AsyncCallback(BeginRead), null);
            }
        }

        private void BeginRead(IAsyncResult AR)
        {
            pipe.EndRead(AR);
            Packets.PacketBuilder pb = new Packets.PacketBuilder(buffer);
            OnReceive(new Packets.PacketBuilder(buffer));
            pipe.BeginRead(buffer, 0, 1023, new AsyncCallback(BeginRead), null);           
        }

        // <summary>
        // Sends packet to the destination.
        // </summary>
        public void Send(Packets.PacketBuilder packet)
        {
            OnSend(packet);
            pipe.Write(packet.GetPacket(), 0, packet.Data.Length+2);
        }
    }
}
