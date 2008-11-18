using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Objects
{
    public class ContextMenu //should we deal with them separately like this or like screen class?
    {
        Client client;
        int eventId;
        string text;
        ContextMenu.Type type;
        bool hasSeparator;
        bool added=false;


        public ContextMenu(Client client,int EventId, string MenuText, Type Type, bool HasSeparator)
        {
            this.client = client;
            eventId = EventId;
            text = MenuText;
            type = Type;
            hasSeparator = HasSeparator;
        }

        public bool Add()
        {
            if (!added)
            {
                if (client.Pipe == null)
                {
                    client.InitializePipe();
                    client.PipeIsReady.WaitOne();
                }
                if (eventId < 0 || text == string.Empty) return false;

                client.Pipe.Send(Tibia.Packets.Pipes.AddContextMenuPacket.Create(client, eventId, text, type, hasSeparator));
                added = true;
                return true;
            }
            return false;
        }

        public bool Remove()
        {
            if (added)
            {
                if (client.Pipe == null)
                {
                    client.InitializePipe();
                    client.PipeIsReady.WaitOne();
                }
                if (eventId < 0 || text == string.Empty) return false;

                client.Pipe.Send(Tibia.Packets.Pipes.RemoveContextMenuPacket.Create(client, eventId, text, type, hasSeparator));
                added = false;
                return true;
            }
            return false;
        }

        public void RemoveAll()
        {
            if (client.Pipe == null)
            {
                client.InitializePipe();
                client.PipeIsReady.WaitOne();
            }

            client.Pipe.Send(Tibia.Packets.Pipes.RemoveAllContextMenusPacket.Create(client));
        }
    

        /// <summary>
        /// A generic function prototype for context menu events.
        /// </summary>
        public delegate void ContextMenuEvent();

        /// <summary>
        /// Called when the context menu is clicked.
        /// </summary>
        public ContextMenuEvent OnClick;

        //TODO:When pipe receive a OnClickContextMenuPacket,
        //check if the event id matches with the context menu and raise the event




        public enum Type : byte
        {
            AllMenus=0x00,
            SetOutfitContextMenu = 0x01,
            PartyActionContextMenu = 0x02,
            CopyNameContextMenu = 0x03
        }
    }
}
