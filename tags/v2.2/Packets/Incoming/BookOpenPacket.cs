using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class BookOpenPacket : Packet
    {
        private Item book;
        private int maxlength;
        private string text;
        private string author;
        private string date;
        public Item Book
        {
            get { return book; }
        }
        public int MaxLength
        {
            get { return maxlength; }
        }
        public string Text
        {
            get { return text; }
        }
        public string Author
        {
            get { return author; }
        }
        public string Date
        {
            get { return date; }
        }
        public BookOpenPacket(Client c)
            : base(c)
        {
            type = PacketType.BookOpen;
            destination = PacketDestination.Client;
        }
        public BookOpenPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.BookOpen) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                p.Skip(4);
                book = p.GetItem();
                maxlength = p.GetInt();
                text = p.GetString();
                author = p.GetString();
                date = p.GetString();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static BookOpenPacket Create(Client c,Item book,int maxsize, string text, string author, string date)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.BookOpen);
            p.Skip(4);
            p.AddItem(book);
            p.AddInt(maxsize);
            p.AddString(text);
            p.AddString(author);
            p.AddString(date);
            return new BookOpenPacket(c, p.GetPacket());
        }
    }
}