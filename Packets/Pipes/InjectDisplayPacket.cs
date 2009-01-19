using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class InjectDisplayPacket : PipePacket
    {
        public bool Injected { get; set; }

        public InjectDisplayPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.InjectDisplayText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.InjectDisplayText)
                return false;

            Type = PipePacketType.InjectDisplayText;
            Injected = Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddByte(Convert.ToByte(Injected));
            return msg.Packet;
        }

        public static bool Send(Objects.Client client, bool injected)
        {
            InjectDisplayPacket p = new InjectDisplayPacket(client);
            p.Injected = injected;
            return p.Send();
        }

    }
}


