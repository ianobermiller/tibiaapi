using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class DisplayCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;
        Location textloc;
        int colorRed;
        int colorGreen;
        int colorBlue;
        int textFont; //TODO: Create Enum of possible text fonts
        string text;

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
            get { return textloc; }
        }

        public int ColorRed
        {
            get { return colorRed; }
        }

        public int ColorBlue
        {
            get { return colorBlue; }
        }

        public int ColorGreen
        {
            get { return colorGreen; }
        }

        public int TextFont
        {
            get { return textFont; }
        }

        public string Text
        {
            get { return text; }
        }

        public DisplayCreatureTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.DisplayCreatureText;
            destination = PacketDestination.Pipe;
        }

        public DisplayCreatureTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.DisplayCreatureText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();
                textloc.X = p.GetInt();
                textloc.Y = p.GetInt();
                textloc.Z = p.GetInt();
                colorRed = p.GetInt();
                colorGreen = p.GetInt();
                colorBlue = p.GetInt();
                textFont = p.GetInt();
                text = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DisplayCreatureTextPacket Create(Client c, int CreatureID, string CreatureName, Location TextLoc, int ColorRed, int ColorGreen, int ColorBlue, int TextFont, string Text)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.DisplayCreatureText);
            p.AddLong(CreatureID);
            p.AddString(CreatureName);
            p.AddShort(TextLoc.X);
            p.AddShort(TextLoc.Y);
            p.AddInt(ColorRed);
            p.AddInt(ColorGreen);
            p.AddInt(ColorBlue);
            p.AddInt(TextFont);
            p.AddString(Text);

            return new DisplayCreatureTextPacket(c, p.GetPacket());
        }
    }
}