using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class VipRemovePacket : OutgoingPacket
    {
        public uint Id { get; set; }

        public VipRemovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.VipRemove;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.VipRemove)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.VipRemove;

            Id = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(Id);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client,uint id)
        {
            VipRemovePacket p = new VipRemovePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}