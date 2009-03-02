using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class VipAddPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public VipAddPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.VipAdd;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.VipAdd)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.VipAdd;

            Name = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddString(Name);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, string name)
        {
            VipAddPacket p = new VipAddPacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}