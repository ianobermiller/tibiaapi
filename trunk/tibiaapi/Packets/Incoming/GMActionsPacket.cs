using Tibia.Constants;
using System.Collections.Generic;

namespace Tibia.Packets.Incoming
{
    public class GMActionsPacket : IncomingPacket
    {
        public List<byte> Actions { get; set; }
        public GMActionsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.GMActions;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.GMActions)
                return false;

            var numViolationReasons = (Client.VersionNumber >= 854) ? 20 : 32;

            for (int i = 0; i < numViolationReasons; i++)
                Actions.Add(msg.GetByte());


            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            var numViolationReasons = (Client.VersionNumber >= 854) ? 20 : 32;

            Actions.ForEach(b => msg.AddByte(b)); 
        }
    }
}
