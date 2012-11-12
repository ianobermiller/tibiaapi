using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EquipObjectPacket : OutgoingPacket
    {
        public ushort TypeId { get; set; }
        public byte Data { get; set; }

        public EquipObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EquipObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EquipObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EquipObject;

            TypeId = msg.GetUInt16();
            Data = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(TypeId);
            msg.AddByte(Data);
        }

        public static bool Send(Objects.Client client, ushort typeId, byte data)
        {
            EquipObjectPacket p = new EquipObjectPacket(client);
            p.TypeId = typeId;
            p.Data = data;
            return p.Send();
        }
    }
}