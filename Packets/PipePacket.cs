using Tibia.Constants;

namespace Tibia.Packets
{
    public class PipePacket
    {
        public PipePacketType Type { get; set; }
        public Objects.Client Client { get; set; }
        public PacketDestination Destination { get; set; }

        public PipePacket(Objects.Client client)
        {
            Client = client;
            Destination = PacketDestination.Pipe;
        }

        public virtual byte[] ToByteArray() { return null; }
        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination) { return false; }

        public bool Send()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client);
            msg.AddBytes(ToByteArray());
            Client.Dll.Pipe.Send(msg);
            return true;
        }
    }
}
