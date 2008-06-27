using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class PartyInvitePacket : Packet
    {
        private int creatureId;
        private PartyType partytype;
        public int CreatureID
        {
            get { return creatureId; }
        }
        public PartyType PartyType
        {
            get { return partytype; }
        }
        public PartyInvitePacket(Client c)
            : base(c)
        {
            type = PacketType.PartyInvite;
            destination = PacketDestination.Client;
        }
        public PartyInvitePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.PartyInvite) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureId = p.GetLong();
                partytype = (PartyType)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static PartyInvitePacket Create(Client c, Creature creature, PartyType partytype)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.PartyInvite);
            p.AddLong(creature.Id);
            p.AddByte((byte)partytype);
            return new PartyInvitePacket(c, p.GetPacket());
        }
    }
}