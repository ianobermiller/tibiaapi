using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Pipes
{
    public class OnClickContextMenuPacket : PipePacket
    {
        int eventId;

        public int EventId
        {
            get { return eventId; }
        }
        public OnClickContextMenuPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.OnClickContextMenu;
            destination = PacketDestination.Pipe;
        }

        public OnClickContextMenuPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.OnClickContextMenu || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                eventId = p.GetLong();
                index = p.Index;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static OnClickContextMenuPacket Create(Client c,int EventId)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.OnClickContextMenu);
            p.AddLong(EventId);
            return new OnClickContextMenuPacket(c, p.GetPacket());
        }
    }
}
