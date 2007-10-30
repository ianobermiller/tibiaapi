using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tibia
{
    public class DatReader
    {
        protected Objects.Client client;
        protected string file;
        protected byte tbyte;
        protected byte option;
        protected int Id;
        public DatReader(Objects.Client c, string f)
        {
            client = c;
            file = f;
        }
        public List<DatItem> ReadDatFile()
        {
            List<DatItem> Items = new List<DatItem>();
            for (int i = 0; i < 7900; i++)
            {
                Items[i].IsContainer = false;
                Items[i].ReadWriteInfo = 0;
                Items[i].IsFluidContainer = false;
                Items[i].IsStackable = false;
                Items[i].MultiType = false;
                Items[i].Useable = false;
                Items[i].IsNotMovable = false;
                Items[i].AlwaysOnTop = false;
                Items[i].IsGroundTile = false;
                Items[i].IsPickupAble = false;
                Items[i].Blocking = false;
                Items[i].BlockPickupable = false;
                Items[i].IsWalkable = false;
                Items[i].IsDoor = false;
                Items[i].IsDoorWithLock = false;
                Items[i].Speed = 0;
                Items[i].CanDecay = false;
                Items[i].HasExtraByte = false;
                Items[i].IsField = false;
                Items[i].IsDepot = false;
                Items[i].MoreAlwaysOnTop = false;
                Items[i].Useable2 = false;
            }
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader Reader = new BinaryReader(stream);
            try
            {
                Reader.ReadBytes(8);
                tbyte = Reader.ReadByte();
                Reader.ReadBytes(3);
                option = Reader.ReadByte();
                Id = 100;
                while (option != 0xFF)
                {
                    switch (option)
                    {
                        case 0x00: 
                            Items[Id].IsGroundTile = true;
                            Items[Id].Speed = Reader.ReadByte();
                            if (Items[Id].Speed == 0)
                            {
                                Items[Id].Blocking = true;
                            }
                            Reader.ReadByte();
                            break;
                        case 0x01:
                            Items[Id].MoreAlwaysOnTop = true;
                            break;
                        case 0x02:
                            Items[Id].AlwaysOnTop = true;
                            break;
                        case 0x03:
                            Items[Id].AlwaysOnTop = true;
                            Items[Id].IsWalkable = true;
                            break;
                        case 0x04:
                            Items[Id].IsContainer = true;
                            break;
                        case 0x05:
                            Items[Id].IsStackable = true;
                            break;
                        case 0x06:
                            Items[Id].Useable = true;
                            break;
                        case 0x07:
                            Items[Id].Useable2 = true;
                            break;
                        case 0x09:
                            Items[Id].ReadWriteInfo = 3;
                            Reader.ReadByte();
                            Reader.ReadByte();
                            break;
                        case 0x0A:
                            Items[Id].ReadWriteInfo = 1;
                            Reader.ReadByte();
                            Reader.ReadByte();
                            break;
                        case 0x0B:
                            Items[Id].IsFluidContainer = true;
                            break;
                        case 0x0C:
                            Items[Id].MultiType = true;
                            break;
                        case 0x0D:
                            Items[Id].Blocking = true;
                            break;
                        case 0x0E:
                            Items[Id].IsNotMovable = true;
                            break;
                        case 0x11:
                            Items[Id].IsPickupAble = true;
                            break;
                        case 0x16:
                            Reader.ReadUInt16();
                            Reader.ReadUInt16();
                            break;
                        case 0x19:
                            Reader.ReadUInt16();
                            Reader.ReadUInt16();
                            break;
                        case 0x1A:
                            Items[Id].BlockPickupable = false;
                            Reader.ReadByte();
                            Reader.ReadByte();
                            break;
                        case 0x1B:
                            Items[Id].CanDecay = false;
                            break;
                        default:
                            break;
                    }
                    option = Reader.ReadByte();
                }

            return Items;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
