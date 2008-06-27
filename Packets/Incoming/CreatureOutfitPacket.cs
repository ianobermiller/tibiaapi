using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureOutfitPacket:Packet
    {
        private int creatureid;
        private Tibia.Constants.OutfitType outfit;
        private Tibia.Constants.OutfitAddon addon;
        private byte head;
        private byte body;
        private byte legs;
        private byte feet;

        public int CreatureId
        {
            get { return creatureid; }
        }

        public Tibia.Constants.OutfitType Outfit
        {
            get { return outfit; }
        }

        public Tibia.Constants.OutfitAddon Addon 
        { 
            get { return addon; } 
        }

        public byte Head
        {
            get { return head; }
        }

        public byte Body
        {
            get { return body; }
        }

        public byte Legs
        {
            get { return legs; }
        }

        public byte Feet
        {
            get { return feet; }
        }

        public CreatureOutfitPacket(Client c) : base(c)
        {
            type = PacketType.CreatureOutfit;
            destination = PacketDestination.Client;
        }
        public CreatureOutfitPacket(Client c, byte[] data)
            :this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureOutfit) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureid = p.GetLong();
                outfit = (Tibia.Constants.OutfitType)p.GetInt();
                head = p.GetByte();
                body = p.GetByte();
                legs = p.GetByte();
                feet = p.GetByte();
                addon = (Tibia.Constants.OutfitAddon)p.GetByte();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static CreatureOutfitPacket Create(Client c, Tibia.Objects.Creature creature, Tibia.Constants.OutfitType outfit, byte head, byte body, byte legs, byte feet, Tibia.Constants.OutfitAddon addon)
        {
            return Create(c, creature.Id, outfit, head, body, legs, feet, addon);
        }

        public static CreatureOutfitPacket Create(Client c, int id, Tibia.Constants.OutfitType outfit, byte head, byte body, byte legs, byte feet, Tibia.Constants.OutfitAddon addon)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureOutfit);
            p.AddLong(id);
            p.AddInt((int)outfit);
            p.AddByte(head);
            p.AddByte(body);
            p.AddByte(legs);
            p.AddByte(feet);
            p.AddByte((byte)addon);
            return new CreatureOutfitPacket(c, p.GetPacket());
        }
    }
}
