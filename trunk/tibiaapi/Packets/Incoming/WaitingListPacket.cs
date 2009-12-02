using System;
using Tibia.Constants;

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

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Message);
            msg.AddByte(Time);
        }
    }
}