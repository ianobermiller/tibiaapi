using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class OpenPrivatePlayerChatPacket : IncomingPacket
    {

        public string Name { get; set; }

        public OpenPrivatePlayerChatPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelOpenPrivate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ChannelOpenPrivate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelOpenPrivate;

            Name = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddString(Name);

            return msg.Packet;
        }
    }
}