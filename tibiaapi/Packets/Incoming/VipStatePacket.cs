using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class VipStatePacket : IncomingPacket
    {

        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
        public byte PlayerState { get; set; }

        public VipStatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.VIP_STATE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.VIP_STATE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.VIP_STATE;

            PlayerId = msg.GetUInt32();
            PlayerName = msg.GetString();
            PlayerState = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(PlayerId);
            msg.AddString(PlayerName);
            msg.AddByte(PlayerState);

            return msg.Packet;
        }
    }
}