using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class AddContextMenuPacket : PipePacket
    {
        int eventId;
        string text;
        ContextMenu.Type ctype;
        byte hasSeparator;

        public int EventId
        {
            get { return eventId; }
        }
        
        public string MenuText
        {
            get { return text; }
        }

        public ContextMenu.Type MenuType
        {
            get { return ctype; }
        }

        public bool HasSeparator
        {
            get { return Convert.ToBoolean(hasSeparator); }
        }

        public AddContextMenuPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.AddContextMenu;
            destination = PacketDestination.Pipe;
        }

        public AddContextMenuPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.AddContextMenu || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                eventId = p.GetLong();
                text = p.GetString();
                ctype = (ContextMenu.Type)p.GetByte();
                hasSeparator = p.GetByte();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static AddContextMenuPacket Create(Client c, int EventId, string MenuText,ContextMenu.Type Type, bool HasSeparator)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.AddContextMenu);
            p.AddLong(EventId);
            p.AddString(MenuText);
            p.AddByte((byte)Type);
            p.AddByte(Convert.ToByte(HasSeparator));

            return new AddContextMenuPacket(c, p.GetPacket());
        }
    }    
}
