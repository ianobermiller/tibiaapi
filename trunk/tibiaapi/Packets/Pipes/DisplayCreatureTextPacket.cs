using System;
using Tibia.Objects;
using System.Drawing;
using Tibia.Constants;

namespace Tibia.Packets.Pipes
{
    public class DisplayCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;
        Location textloc;
        Color color;
        int red;
        int green;
        int blue;
        ClientFont font; //TODO: Create Enum of possible text fonts
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

        public Color Color
        {
            get { return color; }
        }

        public ClientFont Font
        {
            get { return font; }
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
                textloc.X = p.GetShort();
                textloc.Y = p.GetShort();
                red = p.GetInt();
                green = p.GetInt();
                blue = p.GetInt();
                color = Color.FromArgb(red, green, blue);
                font = (ClientFont)p.GetInt();
                text = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DisplayCreatureTextPacket Create(Client c, int creatureID, string creatureName, Location loc, Color color, ClientFont font, string text)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.DisplayCreatureText);
            p.AddLong(creatureID);
            p.AddString(creatureName);
            p.AddShort(loc.X);
            p.AddShort(loc.Y);
            p.AddInt(color.R);
            p.AddInt(color.G);
            p.AddInt(color.B);
            p.AddInt((int)font);
            p.AddString(text);

            return new DisplayCreatureTextPacket(c, p.GetPacket());
        }
    }
}