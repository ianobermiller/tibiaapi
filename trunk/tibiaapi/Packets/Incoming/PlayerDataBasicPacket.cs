using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerDataBasicPacket : IncomingPacket
    {
        public bool Premium { get; set; }
        public byte Vocation { get; set; }
        public ushort SpellCount { get; set; }
        System.Collections.Generic.List<byte> Spells { get; set; }
        public PlayerDataBasicPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerDataBasic;
            Destination = PacketDestination.Client;
            Spells = new System.Collections.Generic.List<byte>();
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerDataBasic)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerDataBasic;

            Premium = System.Convert.ToBoolean(msg.GetByte());//ispremium
            Vocation=msg.GetByte();//profession

            SpellCount = msg.GetUInt16();
            for (int i = 0; i < SpellCount; i++)
                Spells.Add(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(System.Convert.ToByte(Premium));
            msg.AddByte(Vocation);
            msg.AddUInt16((ushort)Spells.Count);
            Spells.ForEach(spell => msg.AddByte(spell));
        }
    }
}