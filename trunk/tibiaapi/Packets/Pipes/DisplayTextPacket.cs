using System;
using System.Drawing;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class DisplayTextPacket : PipePacket
    {
        public string TextId{get;set;}
        public Location Location{get;set;}
        public Color Color{get;set;}
        public ClientFont Font{get;set;}
        public string Text{get;set;}

        public DisplayTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.DisplayText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.DisplayText)
                return false;

            Type = PipePacketType.DisplayText;
            
            TextId = msg.GetString();
            Location = new Location((int)msg.GetUInt32(), (int)msg.GetUInt32(), 0);
            Color = Color.FromArgb((int)msg.GetUInt32(), (int)msg.GetUInt32(), (int)msg.GetUInt32());
            Font = (ClientFont)msg.GetUInt32();
            Text = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddString(TextId);
            msg.AddUInt16((ushort)Location.X);
            msg.AddUInt16((ushort)Location.Y);
            msg.AddUInt16((ushort)Color.R);
            msg.AddUInt16((ushort)Color.G);
            msg.AddUInt16((ushort)Color.B);
            msg.AddUInt16((ushort)Font);
            msg.AddString(Text);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, string textId, Location location, Color color, ClientFont font, string text)
        {
            DisplayTextPacket p = new DisplayTextPacket(client);

            p.TextId = textId;
            p.Location = location;
            p.Color = color;
            p.Font = font;
            p.Text = text;

            return p.Send();
        }

    }
}



