using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MountPacket : OutgoingPacket
    {
        public bool Mount { get; set; }

        public MountPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Mount;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Mount)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Mount;

            Mount = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(System.Convert.ToByte(Mount));
        }

        public static bool Send(Objects.Client client, bool mount)
        {
            MountPacket p = new MountPacket(client);
            p.Mount = mount;
            return p.Send();
        }
    }
}