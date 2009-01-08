using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class PrivateChannelCreatePacket : IncomingPacket
    {

        public string Name { get; set; }
        public ushort ChannelId { get; set; }

        public PrivateChannelCreatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PrivateChannelCreate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PrivateChannelCreate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PrivateChannelCreate;


            try
            {
                ChannelId = msg.GetUInt16();
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

            msg.AddUInt16(ChannelId);
            msg.AddString(Name);

            return msg.Packet;
        }
    }
}