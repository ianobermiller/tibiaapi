using System;
using Tibia.Constants;

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

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
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

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Position);
            msg.AddByte(StackPosition);
        }
    }
}