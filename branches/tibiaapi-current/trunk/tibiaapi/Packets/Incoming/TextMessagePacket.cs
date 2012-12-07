using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public string Text { get; set; }
        public byte Mode { get; set; }
        public Objects.Location Location { get; set; }
        public uint NumValue { get; set; }
        public byte NumColor { get; set; }
        public uint PhysicalDmgValue { get; set; }
        public byte PhysicalDmgColor { get; set; }
        public uint MagicDmgValue { get; set; }
        public byte MagicDmgColor { get; set; }
        public ushort ChannelId { get; set; }

        public TextMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TextMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TextMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TextMessage;

            Mode = msg.GetByte();
            var speechType = Constants.Enums.GetSpeechTypeInfo(Client.VersionNumber, Mode).SpeechType;

            switch (speechType)
            {
                case SpeechType.ChannelManagement://channel management
                    ChannelId = msg.GetUInt16();//channelid
                    break;
                case SpeechType.DamageDealed:
                case SpeechType.DamageReceived:
                case SpeechType.DamageOthers:
                    Location = msg.GetLocation();
                    PhysicalDmgValue = msg.GetUInt32();//value
                    PhysicalDmgColor = msg.GetByte();//color
                    MagicDmgValue = msg.GetUInt32();//value
                    MagicDmgColor = msg.GetByte();//color
                    break;
                case SpeechType.Heal://heal
                case SpeechType.Exp://exp
                case SpeechType.HealOthers://heal others
                case SpeechType.ExpOthers://exp others
                    Location = msg.GetLocation();
                    NumValue = msg.GetUInt32();//value
                    NumColor = msg.GetByte();//color
                    break;
            }
            Text = msg.GetString();//text

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
              msg.AddByte(Mode);
            var speechType = Constants.Enums.GetSpeechTypeInfo(Client.VersionNumber, Mode).SpeechType;

            switch (speechType)
            {
                case SpeechType.ChannelManagement://channel management
                    msg.AddUInt16(ChannelId);//channelid
                    break;
                case SpeechType.DamageDealed:
                case SpeechType.DamageReceived:
                case SpeechType.DamageOthers:
                    msg.AddLocation(Location);
                     msg.AddUInt32(PhysicalDmgValue);//value
                     msg.AddByte(PhysicalDmgColor);//color
                     msg.AddUInt32(MagicDmgValue);//value
                     msg.AddByte(MagicDmgColor);//color
                    break;
                case SpeechType.Heal://heal
                case SpeechType.Exp://exp
                case SpeechType.HealOthers://heal others
                case SpeechType.ExpOthers://exp others
                      msg.AddLocation(Location);
                      msg.AddUInt32(NumValue);//value
                      msg.AddByte(NumColor);//color
                    break;
            }
            msg.AddString(Text);//text
        }
    }
}