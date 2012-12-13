using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class QuestLogPacket : IncomingPacket
    {
        public class QuestEntry
        {
            public ushort Id { get; set; }
            public string Name { get; set; }
            public bool IsCompleted { get; set; }
        }

        public System.Collections.Generic.List<QuestEntry> QuestList { get; set; }

        public QuestLogPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.QuestLog;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.QuestLog)
                return false;
            
            Destination = destination;
            Type = IncomingPacketType.QuestLog;
            
            ushort Count = msg.GetUInt16();
            QuestList = new System.Collections.Generic.List<QuestEntry>(Count);
            for (int i = 0; i < Count; i++)
            {
                QuestList.Add(new QuestEntry
                {
                    Id = msg.GetUInt16(),
                    Name = msg.GetString(),
                    IsCompleted = System.Convert.ToBoolean(msg.GetByte())
                });
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)QuestList.Count);
            foreach (QuestEntry qe in QuestList)
            {
                msg.AddUInt16(qe.Id);
                msg.AddString(qe.Name);
                msg.AddByte(System.Convert.ToByte(qe.IsCompleted));
            }
        }
    }
}