using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class FyiMessagePacket : IncomingPacket
    {

        public string Message { get; set; }

        public FyiMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FyiMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FyiMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FyiMessage;

            try
            {
                Message = msg.GetString();
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

            return msg.Packet;
        }
    }
}