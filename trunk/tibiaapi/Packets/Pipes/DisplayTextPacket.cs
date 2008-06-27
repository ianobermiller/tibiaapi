    using System;
    using Tibia.Objects;

    namespace Tibia.Packets.Pipes
    {
    public class DisplayTextPacket : PipePacket
    {
        string textname;
        Location loc; //No use for Z though
        int red;
        int green;
        int blue;
        int font;
        string text;

        public string TextName
        {
            get { return textname; }
        }

        public Location Loc
        {
            get { return loc; }
        }

        public int Red
        {
            get { return red; }
        }

        public int Green
        {
            get { return green; }
        }

        public int Blue
        {
            get { return blue; }
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
                font = p.GetInt();
                text = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DisplayTextPacket Create(Client c, string TextName, Location Loc, int Red, int Green, int Blue, int Font, string Text)
        {
            //Testing for correct values
            if (Red > 0xFF)
                Red = 0xFF;
            if (Green > 0xFF)
                Green = 0xFF;
            if (Blue > 0xFF)
                Blue = 0xFF;
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.DisplayText);
            p.AddString(TextName);
            p.AddInt(Loc.X);
            p.AddInt(Loc.Y);
            p.AddInt(Red);
            p.AddInt(Green);
            p.AddInt(Blue);
            p.AddInt(Font);
            p.AddString(Text);

            return new DisplayTextPacket(c, p.GetPacket());
        }

       
    }
}
