using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            NetworkMessage msg = new NetworkMessage(Client);
            msg.Position = 2;
            msg.AddBytes(ToByteArray());
            msg.InsertPacketHeader();

            Client.Dll.Pipe.Send(msg);

            return true;
        }
    }
}
