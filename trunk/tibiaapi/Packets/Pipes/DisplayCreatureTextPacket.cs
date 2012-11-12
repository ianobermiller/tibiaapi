using System;
using System.Drawing;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class DisplayCreatureTextPacket : PipePacket
    {
        public uint CreatureId{get;set;}
        public string CreatureName{get;set;}
        public Location Location{get;set;}
        public Color Color{get;set;}
        public ClientFont Font{get;set;}
        public string Text{get;set;}

        public DisplayCreatureTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.DisplayCreatureText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.DisplayCreatureText)
                return false;

            Type = PipePacketType.DisplayCreatureText;
            
            CreatureId = msg.GetUInt32();
            CreatureName = msg.GetString();
            Location = new Location((int)msg.GetUInt32(), (int)msg.GetUInt32(), 0);
            Color = Color.FromArgb((int)msg.GetUInt32(), (int)msg.GetUInt32(), (int)msg.GetUInt32());
            Font = (ClientFont)msg.GetUInt32();
            Text = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 21 + CreatureName.Length + Text.Length);
            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddString(CreatureName);
            msg.AddUInt16((ushort)Location.X);
            msg.AddUInt16((ushort)Location.Y);
            msg.AddUInt16((ushort)Color.R);
            msg.AddUInt16((ushort)Color.G);
            msg.AddUInt16((ushort)Color.B);
            msg.AddUInt16((ushort)Font);
            msg.AddString(Text);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint creatureId , string creatureName, Location location, Color color, ClientFont font, string text)
        {
            DisplayCreatureTextPacket p = new DisplayCreatureTextPacket(client);

            p.CreatureId = creatureId;
            p.CreatureName = creatureName;
            p.Location = location;
            p.Color = color;
            p.Font = font;
            p.Text = text;

            return p.Send();
        }

    }
}



