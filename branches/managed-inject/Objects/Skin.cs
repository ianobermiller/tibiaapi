using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;
using System.Drawing;

namespace Tibia.Objects
{
    public class Skin
    {
        Client client;
        List<uint> skinIds;

        internal Skin(Client client)
        {
            this.client = client;
            skinIds=new List<uint>();
        }

        public bool AddSkin(uint skinId, ushort posX, ushort posY, ushort width, ushort height, ushort guiId)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (skinIds.Contains(skinId))
                return false;

            skinIds.Add(skinId);
            return Packets.Pipes.AddSkinPacket.Send(client, skinId, posX, posY, width, height, guiId);
        }

        public bool UpdateSkin(uint skinId, ushort posX, ushort posY, ushort width, ushort height, ushort guiId)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (!skinIds.Contains(skinId))
                return false;

            return Packets.Pipes.UpdateSkinPacket.Send(client, skinId, posX, posY, width, height, guiId);
        }

        public bool RemoveSkin(uint skinId)
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            if (!skinIds.Contains(skinId))
                return false;

            skinIds.Remove(skinId);
            return Packets.Pipes.RemoveSkinPacket.Send(client, skinId);
        }

        public void RemoveAll()
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            skinIds.Clear();

            Packets.Pipes.RemoveAllSkinsPacket.Send(client);
        }
    }
}
