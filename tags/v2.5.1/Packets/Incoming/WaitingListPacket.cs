using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class WaitingListPacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Time { get; set; }

        public WaitingListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.WaitingList;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.WaitingList)
                return false;

            Destination = destination;
            Type = IncomingPacketType.WaitingList;

            try
            {
                Message = msg.GetString();
                Time = msg.GetByte();
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

            msg.AddString(Message);
            msg.AddByte(Time);

            return msg.Packet;
        }
    }
}