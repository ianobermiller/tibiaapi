using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;

        public int CreatureID
        {
            get { return creatureID; }
        }

        public string CreatureName
        {
            get { return creatureName; }
        }

        public RemoveCreatureTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.RemoveCreatureText;
            destination = PacketDestination.Pipe;
        }

        public RemoveCreatureTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveCreatureText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveCreatureTextPacket Create(Client c, int CreatureID, string CreatureName)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.RemoveCreatureText);
            p.AddLong(CreatureID);
            p.AddString(CreatureName);

            return new RemoveCreatureTextPacket(c, p.GetPacket());
        }
    }
}
