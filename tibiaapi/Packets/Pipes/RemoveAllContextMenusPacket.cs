using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveAllContextMenusPacket : PipePacket
    {
        public RemoveAllContextMenusPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.RemoveAllContextMenus;
            destination = PacketDestination.Pipe;
        }

        public RemoveAllContextMenusPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveAllContextMenus || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                index = p.Index;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveAllContextMenusPacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.RemoveAllContextMenus);
            return new RemoveAllContextMenusPacket(c, p.GetPacket());
        }
    }
}
