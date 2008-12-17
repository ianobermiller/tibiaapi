using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ChannelListPacket : IncomingPacket
    {
        public byte NumberChannel { get; set; }
        public List<Objects.Channel> Channels { get; set; }

        public ChannelListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CHANNEL_LIST;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CHANNEL_LIST)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType_t.CHANNEL_LIST;
            NumberChannel = msg.GetByte();

            Channels = new List<Tibia.Objects.Channel>(NumberChannel);

            ushort id;

            for (int i = 0; i < NumberChannel; i++)
            {
                id = msg.GetUInt16();
                Channels.Add(new Tibia.Objects.Channel((ChatChannel)id, msg.GetString()));
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte((byte)Channels.Count);

            foreach (Objects.Channel c in Channels)
            {
                msg.AddUInt16((ushort)c.Id);
                msg.AddString(c.Name);
            }

            return msg.Packet;
        }
    }
}