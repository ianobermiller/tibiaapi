using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class UpdateCreatureTextPacket : PipePacket
    {
        int creatureID;
        Location textLoc; //Used to make sure it's the right text.
        string newText;

        public int CreatureID
        {
            get { return creatureID; }
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
                textLoc.X = p.GetInt();
                textLoc.Y = p.GetInt();
                textLoc.Z = p.GetInt();
                newText = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static UpdateCreatureTextPacket Create(Client c, int CreatureID, Location TextLoc, string NewText)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.UpdateCreatureText);
            p.AddLong(CreatureID);
            p.AddInt(TextLoc.X);
            p.AddInt(TextLoc.Y);
            p.AddInt(TextLoc.Z);
            p.AddString(NewText);

            return new UpdateCreatureTextPacket(c, p.GetPacket());
        }
    }
}
