using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatePrivateChannelPacket : IncomingPacket
    {

        public string Name { get; set; }
        public ushort ChannelId { get; set; }

        public CreatePrivateChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PrivateChannelCreate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.PrivateChannelCreate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PrivateChannelCreate;

            ChannelId = msg.GetUInt16();
            Name = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt16(ChannelId);
            msg.AddString(Name);

            return msg.Packet;
        }
    }
}