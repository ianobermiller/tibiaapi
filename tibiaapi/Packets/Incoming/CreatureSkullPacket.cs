using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureSkullPacket : Packet
    {
        private int creatureId;
        private Constants.Skull skull;
        public int CreatureID
        {
            get { return creatureId; }
        }
        public Constants.Skull Skull
        {
            get { return skull; }
        }
        public CreatureSkullPacket(Client c)
            : base(c)
        {
            type = PacketType.CreatureSkull;
            destination = PacketDestination.Client;
        }
        public CreatureSkullPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureSkull) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureId = p.GetLong();
                skull = (Constants.Skull)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static CreatureSkullPacket Create(Client c, Creature creature, Constants.Skull skull)
        {
            return Create(c, creature.Id, skull);
        }
        public static CreatureSkullPacket Create(Client c,int id, Constants.Skull skull)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureSkull);
            p.AddLong(id);
            p.AddByte((byte)skull);
            return new CreatureSkullPacket(c, p.GetPacket());
        }
    }
}