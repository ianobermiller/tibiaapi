using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class HooksEnableDisablePacket : PipePacket
    {
        public bool Enable { get; set; }

        public HooksEnableDisablePacket(Client client)
            : base(client)
        {
            Type = PipePacketType.HooksEnableDisable;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.HooksEnableDisable)
                return false;

            Type = PipePacketType.HooksEnableDisable;
            Enable = Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 2);
            msg.AddByte((byte)Type);

            msg.AddByte(Convert.ToByte(Enable));
            return msg.Data;
        }

        public static bool Send(Objects.Client client, bool injected)
        {
            HooksEnableDisablePacket p = new HooksEnableDisablePacket(client);
            p.Enable = injected;
            return p.Send();
        }

    }
}


