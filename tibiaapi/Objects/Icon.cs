using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Objects
{
    public class Icon
    {
        Client client;
        List<uint> iconIds;

        internal Icon(Client client)
        {
            this.client = client;
            iconIds=new List<uint>();
        }

        internal void AddInternalEvents()
        {
            client.Dll.Pipe.OnIconClick +=new Tibia.Packets.Pipe.PipeListener(Pipe_OnIconClick);
        }

        private void Pipe_OnIconClick(Tibia.Packets.NetworkMessage msg)
        {
            msg.Position = 3;
            if (Click != null)
                Click.BeginInvoke((int)msg.GetUInt32(), null, null);
        }

        public bool AddIcon(uint iconId, ushort posX, ushort posY, ushort size, ushort itemId, ushort itemCount, ClientFont font, Color color)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (iconIds.Contains(iconId) || color == null)
                return false;

            return Packets.Pipes.AddIconPacket.Send(client,iconId, posX, posY, size, itemId, itemCount, font, color);
        }

        public bool UpdateIcon(uint iconId, ushort posX, ushort posY, ushort size, ushort itemId, ushort itemCount, ClientFont font, Color color)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (!iconIds.Contains(iconId) || color == null)
                return false;

            return Packets.Pipes.UpdateIconPacket.Send(client, iconId, posX, posY, size, itemId, itemCount, font, color);
        }

        public bool RemoveIcon(uint iconId)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (!iconIds.Contains(iconId))
                return false;

            return Packets.Pipes.RemoveIconPacket.Send(client, iconId);
        }

        public void RemoveAll()
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            iconIds.Clear();

            Packets.Pipes.RemoveAllIconsPacket.Send(client);
        }


        /// <summary>
        /// A generic function prototype for context menu events.
        /// </summary>
        public delegate void IconEvent(int iconId);

        /// <summary>
        /// Called when the context menu is clicked.
        /// </summary>
        public event IconEvent Click;


    }
}
