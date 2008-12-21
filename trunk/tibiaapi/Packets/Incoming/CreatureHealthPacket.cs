using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureHealthPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte Health { get; set; }

        public CreatureHealthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureHealth;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureHealth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureHealth;

            try
            {
                CreatureId = msg.GetUInt32();
                Health = msg.GetByte();
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
            msg.AddByte(Health);

            return msg.Packet;
        }
    }
}