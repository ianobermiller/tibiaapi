using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerInventoryPacket : IncomingPacket
    {
        public class InventoryStuff
        {
            public ushort Id { get; set; }
            public byte SubType { get; set; }
            public ushort Count { get; set; }
        }

        public System.Collections.Generic.List<InventoryStuff> StuffList { get; set; }

        public PlayerInventoryPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerInventory;
            Destination = PacketDestination.Client;
        }
        
        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerInventory)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerInventory;

            ushort Count = msg.GetUInt16();

            StuffList = new System.Collections.Generic.List<InventoryStuff>(Count);

            for (int i = 0; i < Count; i++)
            {
                StuffList.Add(new InventoryStuff
                {
                    Id = msg.GetUInt16(),
                    SubType = msg.GetByte(),
                    Count = msg.GetUInt16()
                });
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)StuffList.Count);
            foreach (InventoryStuff invs in StuffList)
            {
                msg.AddUInt16(invs.Id);
                msg.AddByte(invs.SubType);
                msg.AddUInt16(invs.Count);
            }
        }
    }
}