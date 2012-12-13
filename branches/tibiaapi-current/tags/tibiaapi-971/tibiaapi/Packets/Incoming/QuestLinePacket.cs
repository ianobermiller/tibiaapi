using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class QuestLinePacket : IncomingPacket
    {
        public class MissionEntry
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public ushort QuestId { get; set; }
        public System.Collections.Generic.List<MissionEntry> MissionList { get; set; }

        public QuestLinePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.QuestLine;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.QuestLine)
                return false;

            Destination = destination;
            Type = IncomingPacketType.QuestLine;

            QuestId=msg.GetUInt16();
            
            byte Count = msg.GetByte();
            MissionList = new System.Collections.Generic.List<MissionEntry>(Count);

            for (int i = 0; i < Count; i++)
            {
                MissionList.Add(new MissionEntry { Name = msg.GetString(), Description = msg.GetString() });
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)QuestId);
            msg.AddUInt16((ushort)MissionList.Count);
            foreach (MissionEntry me in MissionList)
            {
                msg.AddString(me.Name);
                msg.AddString(me.Description);
            }
        }
    }
}