using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MessagePacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Mode { get; set; }

        public MessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Message;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Message)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Message;

            Mode = msg.GetByte();

            switch (Mode)
            {
                case 6://channel management
                    msg.GetUInt16();//channelid
                    msg.GetString();//text
                    break;
                case 16://login
                case 17://admin
                case 18://game
                case 19://failure
                case 20://look
                case 28://status
                case 29://loot
                case 30://trade npc
                case 31://guild
                case 32://party management
                case 33://party
                case 37://hotkey use
                    Message = msg.GetString();//text
                    break;
                case 40://market
                    msg.GetString();//text
                    break;
                case 36://report
                    msg.GetString();//text
                    break;
                case 21://damage dealed
                case 22://damage received
                case 25://damage others
                    msg.GetLocation();
                    msg.GetUInt32();//value
                    msg.GetByte();//color
                    msg.GetUInt32();//value
                    msg.GetByte();//color
                    msg.GetString();//text
                    break;
                case 23://heal
                case 24://exp
                case 26://heal others
                case 27://exp others
                    msg.GetLocation();
                    msg.GetUInt32();//value
                    msg.GetByte();//color
                    msg.GetString();//text
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}