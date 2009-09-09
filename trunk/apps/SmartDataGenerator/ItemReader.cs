using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmartDataGenerator
{
    public struct ItemInfo
    {
        public ushort Id;
        public string Name;

        public ItemInfo(ushort id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public static class ItemsReader
    {
        public const byte NodeStart = 0xFE;
        public const byte NodeEnd = 0xFF;
        public const byte Escape = 0xFD;

        private static string fileName = "items.otb";
        public static Dictionary<ushort, ushort> ServerToClientDictionary = new Dictionary<ushort,ushort>();
        private static FileStream stream;
        private static byte[] buffer = new byte[128];

        public static void InitializeServerToClientDictionary()
        {
            stream = File.OpenRead(fileName);

            bool unparseNext = false;
            int cur;
            while ((cur = stream.ReadByte()) != -1)
            {
                switch (cur)
                {
                    case NodeStart:
                        if (unparseNext)
                        {
                            unparseNext = false;
                        }
                        else
                        {
                            int type = stream.ReadByte();
                            if (type >= 0 && type <= 18)
                                HandleItem(type);
                            break;
                        }
                        break;
                    case NodeEnd:
                        if (unparseNext)
                        {
                            unparseNext = false;
                        }
                        else
                        {

                        }
                        break;
                    case Escape:
                        unparseNext = true;
                        break;
                }
            }

            return;
        }

        private static void HandleItem(int itemGroup)
        {
            ushort serverId = 0;
            ushort clientId = 0;
            ushort flags = BitConverter.ToUInt16(ReadAndUnescape(4), 0);

            byte attr = ReadAndUnescape(1)[0];
            ushort len = BitConverter.ToUInt16(ReadAndUnescape(2), 0);

            if (attr == 0x10)
            {
                serverId = BitConverter.ToUInt16(ReadAndUnescape(2), 0);
            }
            attr = ReadAndUnescape(1)[0];
            if (attr == 0x11)
            {
                len = BitConverter.ToUInt16(ReadAndUnescape(2), 0);
                clientId = BitConverter.ToUInt16(ReadAndUnescape(2), 0);
            }

            if (clientId > 0 && !ServerToClientDictionary.ContainsKey(clientId))
            {
                ServerToClientDictionary.Add(serverId, clientId);
            }
        }

        private static byte[] ReadAndUnescape(int count)
        {
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
            {
                byte tmp = (byte)stream.ReadByte();

                if (tmp == Escape)
                {
                    buffer[i] = (byte)stream.ReadByte();
                }
                else
                {
                    buffer[i] = tmp;
                }
            }
            return buffer;
        }
    }

}
