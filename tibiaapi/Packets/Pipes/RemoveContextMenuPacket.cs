using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveContextMenuPacket : PipePacket
    {
            int eventId;
            string text;
            ContextMenu.Type type;
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
                get { return type; }
            }

            public bool HasSeparator
            {
                get { return hasSeparator; }
            }

            public RemoveContextMenuPacket(Client c)
                : base(c)
            {
                type = PacketType.PipePacket;
                pipetype = PipePacketType.RemoveContextMenu;
                destination = PacketDestination.Pipe;
            }

            public RemoveContextMenuPacket(Client c, byte[] data)
                : this(c)
            {
                ParseData(data);
            }

            public new bool ParseData(byte[] packet)
            {
                if (base.ParseData(packet))
                {
                    if (pipetype != PipePacketType.RemoveContextMenu || type != PacketType.PipePacket) { return false; }
                    PacketBuilder p = new PacketBuilder(client, packet, 3);
                    eventId = p.GetLong();
                    text = p.GetString();
                    type = (ContextMenu.Type)p.GetByte();
                    hasSeparator = Convert.ToBoolean(p.GetByte());

                    index = p.Index;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static RemoveContextMenuPacket Create(Client c, int EventId, string MenuText, ContextMenu.Type Type, bool HasSeparator)
            {
                PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.RemoveContextMenu);
                p.AddLong(EventId);
                p.AddString(MenuText);
                p.AddByte((byte)Type);
                p.AddByte(Convert.ToByte(HasSeparator));

                return new RemoveContextMenuPacket(c, p.GetPacket());
            }
        
    }
}
