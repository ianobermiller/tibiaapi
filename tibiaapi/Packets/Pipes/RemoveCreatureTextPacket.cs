using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveCreatureTextPacket : PipePacket
    {
        public int CreatureId{get;set;}
        public string CreatureName { get; set; }

        public RemoveCreatureTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveCreatureText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveCreatureText)
                return false;

            Type = PipePacketType.RemoveCreatureText;
            CreatureId = (int)msg.GetUInt32();
            CreatureName = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddUInt32((uint)CreatureId);
            msg.AddString(CreatureName);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, int creatureId, string creatureName)
        {
            RemoveCreatureTextPacket p = new RemoveCreatureTextPacket(client);
            p.CreatureId = creatureId;
            p.CreatureName = creatureName;
            return p.Send();
        }

    }
}

