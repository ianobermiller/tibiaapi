using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class ContainerClosePacket : IncomingPacket
    {
        public byte Id { get; set; }

        public ContainerClosePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerClose;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int postion = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerClose)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerClose;

            try
            {
                Id = msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = postion;
                return false;
            }


            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);
            msg.AddByte(Id);

            return msg.Packet;
        }
    }
}