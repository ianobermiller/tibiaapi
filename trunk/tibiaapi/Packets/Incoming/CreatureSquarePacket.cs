using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureSquarePacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public SquareColor Color { get; set; }

        public CreatureSquarePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSquare;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSquare)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSquare;

            try
            {
                CreatureId = msg.GetUInt32();
                Color = (SquareColor)msg.GetByte();
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

            msg.AddUInt32(CreatureId);
            msg.AddByte((byte)Color);

            return msg.Packet;
        }
    }
}