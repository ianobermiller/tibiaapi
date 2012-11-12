using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class SellObjectPacket : OutgoingPacket
    {
        public ushort ObjectType { get; set; }
        public byte Data { get; set; }
        public byte Amount { get; set; }
        public bool KeepEquipped { get; set; }

        public SellObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.SellObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.SellObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.SellObject;

            ObjectType = msg.GetUInt16();
            Data = msg.GetByte();
            Amount = msg.GetByte();
            KeepEquipped = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt16(ObjectType);
            msg.AddByte(Data);
            msg.AddByte(Amount);
            msg.AddByte(System.Convert.ToByte(KeepEquipped));
        }

        public static bool Send(Objects.Client client, ushort objectType, byte data, byte amount, bool keepEquipped)
        {
            SellObjectPacket p = new SellObjectPacket(client);

            p.ObjectType = objectType;
            p.Data = data;
            p.Amount = amount;
            p.KeepEquipped = keepEquipped;

            return p.Send();
        }
    }
}