using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class EventTriggerPacket : PipePacket
    {
        public EventType eventType { get; set; }

        public EventTriggerPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.EventTriggers;
        }

        ///Hooking the EventTriggerPointer in TibiaAPI_Inject.dll crashes the client when an
        ///event is triggered, so I'm guessing this function can be removed?
        /*public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.EventTriggers)
                return false;

            Type = PipePacketType.EventTriggers;

            eventType = msg.GetByte();

            return true;
        }*/

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 21);
            msg.AddByte((byte)Type);
            msg.AddUInt32((uint)eventType);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, EventType eventType)
        {
            EventTriggerPacket p = new EventTriggerPacket(client);

            p.eventType = eventType;

            return p.Send();
        }
    }
}
