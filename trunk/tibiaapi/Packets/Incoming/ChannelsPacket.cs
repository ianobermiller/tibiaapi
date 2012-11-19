using System;
using System.Collections.Generic;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelsPacket : IncomingPacket
    {
        public List<Objects.Channel> Channels { get; set; }

        public ChannelsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Channels;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Channels)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.Channels;

            byte count = msg.GetByte();
            Channels = new List<Tibia.Objects.Channel> { };

            for (int i = 0; i < count; i++)
            {
                ushort id = msg.GetUInt16();
                Channels.Add(new Tibia.Objects.Channel((ChatChannel)id, msg.GetString()));
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)Channels.Count);

            foreach (Objects.Channel c in Channels)
            {
                msg.AddUInt16((ushort)c.Id);
                msg.AddString(c.Name);
            }
        }
    }
}