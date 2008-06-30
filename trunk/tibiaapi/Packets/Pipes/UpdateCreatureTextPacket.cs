using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class UpdateCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;
        Location textLoc; //Used to make sure it's the right text.
        string newText;

        public int CreatureID
        {
            get { return creatureID; }
        }

        public string CreatureName
        {
            get { return creatureName; }
        }

        public Location TextLoc
        {
            get { return textLoc; }
        }

        public string NewText
        {
            get { return newText; }
        }

        public UpdateCreatureTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.UpdateCreatureText;
            destination = PacketDestination.Pipe;
        }

        public UpdateCreatureTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.UpdateCreatureText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();
                textLoc.X = p.GetShort();
                textLoc.Y = p.GetShort();
                newText = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static UpdateCreatureTextPacket Create(Client c, int CreatureID, string CreatureName, Location TextLoc, string NewText)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.UpdateCreatureText);
            p.AddLong(CreatureID);
            p.AddString(CreatureName);
            p.AddShort(TextLoc.X);
            p.AddShort(TextLoc.Y);
            p.AddString(NewText);

            return new UpdateCreatureTextPacket(c, p.GetPacket());
        }
    }
}
