    using System;
    using Tibia.Objects;
using System.Drawing;
using Tibia.Constants;

    namespace Tibia.Packets.Pipes
    {
    public class DisplayTextPacket : PipePacket
    {
        private string textname;
        private Location loc; //No use for Z though
        private int red;
        private int green;
        private int blue;
        private Color color;
        private ClientFont font;
        private string text;

        public string TextName
        {
            get { return textname; }
        }

        public Location Loc
        {
            get { return loc; }
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

        public DisplayTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.DisplayText;
            destination = PacketDestination.Pipe;
        }

        public DisplayTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.DisplayText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                textname = p.GetString();
                loc.X = p.GetInt();
                loc.Y = p.GetInt();
                loc.Z = 0; // No need
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

        public static DisplayTextPacket Create(Client c, string textId, Location loc, Color color, ClientFont font, string text)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.DisplayText);
            p.AddString(textId);
            p.AddInt(loc.X);
            p.AddInt(loc.Y);
            p.AddInt(color.R);
            p.AddInt(color.G);
            p.AddInt(color.B);
            p.AddInt((int)font);
            p.AddString(text);

            return new DisplayTextPacket(c, p.GetPacket());
        }

       
    }
}
