using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class VipLoginPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public VipLoginPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.VipLogin;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.VipLogin)
                return false;

            Destination = destination;
            Type = IncomingPacketType.VipLogin;

            try
            {
                PlayerId = msg.GetUInt32();
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

            msg.AddUInt32(PlayerId);

            return msg.Packet;
        }
    }
}