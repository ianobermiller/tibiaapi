using System;
using System.Drawing;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class UpdateCreatureTextPacket : PipePacket
    {
        public int CreatureId{get;set;}
        public string CreatureName{get;set;}
        public Location Location{get;set;}
        public string Text{get;set;}

        public UpdateCreatureTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.UpdateCreatureText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.UpdateCreatureText)
                return false;

            Type = PipePacketType.UpdateCreatureText;
            
            CreatureId = (int)msg.GetUInt32();
            CreatureName = msg.GetString();
            Location = new Location((int)msg.GetUInt32(), (int)msg.GetUInt32(), 0);
            Text = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            msg.AddUInt32((uint)CreatureId);
            msg.AddString(CreatureName);
            msg.AddUInt16((ushort)Location.X);
            msg.AddUInt16((ushort)Location.Y);
            msg.AddString(Text);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, int creatureId , string creatureName, Location location, string text)
        {
            UpdateCreatureTextPacket p = new UpdateCreatureTextPacket(client);

            p.CreatureId = creatureId;
            p.CreatureName = creatureName;
            p.Location = location;
            p.Text = text;

            return p.Send();
        }

    }
}




