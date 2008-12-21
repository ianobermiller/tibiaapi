using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class LookAtPacket : OutgoingPacket
    {

        public Objects.Location Position { get; set; }
        public ushort SpriteId { get; set; }
        public byte StackPosition { get; set; }

        public LookAtPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.LookAt;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.LookAt)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.LookAt;

            Position = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            StackPosition = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddUInt16(SpriteId);
            msg.AddByte(StackPosition);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Location position, ushort spriteId, byte stackPosition)
        {
            LookAtPacket p = new LookAtPacket(client);
            p.Position = position;
            p.SpriteId = spriteId;
            p.StackPosition = stackPosition;
            return p.Send();
        }
    }
}