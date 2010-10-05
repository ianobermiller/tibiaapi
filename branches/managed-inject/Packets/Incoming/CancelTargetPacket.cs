using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CancelTargetPacket : IncomingPacket
    {
        public uint Count { get; set; }

        public CancelTargetPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CancelTarget;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CancelTarget)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.CancelTarget;

            if (Client.VersionNumber >= 860)
            {
                Count = msg.GetUInt32();
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            if (Client.VersionNumber >= 860)
            {
                msg.AddUInt32(Count);
            }
        }

        public static bool Send(Objects.Client client)
        {
            CancelTargetPacket p = new CancelTargetPacket(client);

            if (client.VersionNumber >= 860)
            {
                uint count = client.Player.AttackCount;
                p.Count = count;
            }
            return p.Send();
        }
    }
}