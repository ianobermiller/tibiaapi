using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ChannelOpenPrivatePacket : IncomingPacket
    {
        public string Name { get; set; }

        public ChannelOpenPrivatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelOpenPrivate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelOpenPrivate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelOpenPrivate;

            try
            {
                Name = msg.GetString();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);
            msg.AddString(Name);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, string name)
        {
            ChannelOpenPrivatePacket p = new ChannelOpenPrivatePacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}