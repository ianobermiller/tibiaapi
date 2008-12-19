using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class TileRemoveThingPacket : IncomingPacket
    {
        public byte StackPosition { get; set; }
        public Objects.Location Position { get; set; }

        public TileRemoveThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TileRemoveThing;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileRemoveThing)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileRemoveThing;

            try
            {
                Position = msg.GetLocation();
                StackPosition = msg.GetByte();
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

            msg.AddLocation(Position);
            msg.AddByte(StackPosition);

            return msg.Packet;
        }
    }
}