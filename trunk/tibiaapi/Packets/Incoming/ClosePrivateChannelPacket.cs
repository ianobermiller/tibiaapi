using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ClosePrivateChannelPacket : IncomingPacket
    {

        public ushort ChannelId { get; set; }

        public ClosePrivateChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PrivateChannelClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PrivateChannelClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PrivateChannelClose;


            try
            {
                ChannelId = msg.GetUInt16();
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
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt16(ChannelId);

            return msg.Packet;
        }
    }
}