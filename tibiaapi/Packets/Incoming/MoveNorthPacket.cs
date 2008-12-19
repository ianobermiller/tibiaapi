using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class MoveNorthPacket : IncomingPacket
    {
        NetworkMessage stream = new NetworkMessage(0);
        private short m_skipTiles;

        private List<Objects.Item> items = new List<Tibia.Objects.Item> { };
        private List<PacketCreature> creatures = new List<PacketCreature> { };

        public List<Objects.Item> Items
        {
            get { return items; }
        }

        public List<PacketCreature> Creatures
        {
            get { return creatures; }
        }

        public MoveNorthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveNorth;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveNorth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveNorth;
            stream.AddByte((byte)Type);

            pos.Y--;

            try
            {
                setMapDescription(msg, pos.X - 8, pos.Y - 6, pos.Z, 18, 1);
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        private bool setMapDescription(NetworkMessage msg, int x, int y, int z, int width, int height)
        {
            int startz, endz, zstep;
            //calculate map limits
            if (z > 7)
            {
                startz = z - 2;
                endz = System.Math.Min(16 - 1, z + 2);
                zstep = 1;
            }
            else
            {
                startz = 7;
                endz = 0;
                zstep = -1;
            }

            for (int nz = startz; nz != endz + zstep; nz += zstep)
            {
                //pare each floor
                if (!setFloorDescription(msg, x, y, nz, width, height, z - nz))
                    return false;
            }

            return true;
        }

        private bool setFloorDescription(NetworkMessage msg, int x, int y, int z, int width, int height, int offset)
        {
            ushort skipTiles;

            for (int nx = 0; nx < width; nx++)
            {
                for (int ny = 0; ny < height; ny++)
                {
                    if (m_skipTiles == 0)
                    {
                        ushort tileOpt = msg.PeekUInt16();
                        //Decide if we have to skip tiles
                        // or if it is a real tile
                        if (tileOpt >= 0xFF00)
                        {
                            skipTiles = msg.GetUInt16();
                            stream.AddUInt16(skipTiles);

                            m_skipTiles = (short)(skipTiles & 0xFF);
                        }
                        else
                        {
                            //real tile so read tile
                            Objects.Location pos = new Tibia.Objects.Location(x + nx + offset, y + ny + offset, z);

                            if (!setTileDescription(msg, pos))
                            {
                                return false;
                            }
                            //read skip tiles info
                            skipTiles = msg.GetUInt16();
                            stream.AddUInt16(skipTiles);

                            m_skipTiles = (short)(skipTiles & 0xFF);
                        }
                    }
                    //skipping tiles...
                    else
                    {
                        m_skipTiles--;
                    }
                }
            }
            return true;
        }

        private bool setTileDescription(NetworkMessage msg, Objects.Location pos)
        {
            int n = 0;
            while (true)
            {
                //avoid infinite loop
                n++;

                ushort inspectTileId = msg.PeekUInt16();

                if (inspectTileId >= 0xFF00)
                {
                    //end of the tile
                    return true;
                }
                else
                {
                    if (n > 10)
                    {
                        return false;
                    }
                    //read tile things: items and creatures
                    internalGetThing(msg, pos);
                }
            }
        }

        private bool internalGetThing(NetworkMessage msg, Objects.Location pos)
        {
            //get thing type
            ushort thingId = msg.GetUInt16();
            stream.AddUInt16(thingId);

            PacketCreature c;

            if (thingId == 0x0061 || thingId == 0x0062)
            {

                c = new PacketCreature(Client);
                c.Location = pos;

                //creatures
                if (thingId == 0x0062) //creature is known
                {
                    c.Type = PacketCreatureType.Known;
                    c.Id = msg.GetUInt32();
                    stream.AddUInt32(c.Id); //creatureid
                }
                else if (thingId == 0x0061)
                { //creature is not known
                    //perhaps we have to remove a known creature
                    //uint32_t removeID = msg.getU32();
                    c.RemoveId = msg.GetUInt32();
                    stream.AddUInt32(c.RemoveId);
                    //add a new creature
                    //uint32_t creatureID = msg.getU32();

                    c.Type = PacketCreatureType.Unknown;
                    c.Id = msg.GetUInt32();
                    stream.AddUInt32(c.Id);

                    //creature->setName(msg.getString());

                    c.Name = msg.GetString();
                    stream.AddString(c.Name);
                }

                //read creature properties
                //creature->setHealth(msg.getU8());
                c.Health = msg.GetByte();
                stream.AddByte(c.Health);

                //uint8_t direction;
                c.Direction = msg.GetByte();
                stream.AddByte(c.Direction);

                c.Outfit = msg.GetOutfit();
                stream.AddOutfit(c.Outfit);

                //creature->setLightLevel(msg.getU8());
                //creature->setLightColor(msg.getU8());
                c.LightLevel = msg.GetByte();
                stream.AddByte(c.LightLevel);

                c.LightColor = msg.GetByte();
                stream.AddByte(c.LightColor);

                //creature->setSpeed(msg.getU16());
                c.Speed = msg.GetUInt16();
                stream.AddUInt16(c.Speed);

                //creature->setSkull(msg.getU8());
                //creature->setShield(msg.getU8());
                c.Skull = (Constants.Skull)msg.GetByte();
                stream.AddByte((byte)c.Skull);

                c.PartyShield = (PartyShield)msg.GetByte();
                stream.AddByte((byte)c.PartyShield);

                creatures.Add(c);

                return true;
            }
            else if (thingId == 0x0063)
            {
                //creature turn
                //uint32_t creatureID = msg.getU32();

                c = new PacketCreature(Client);
                c.Location = pos;
                c.Type = PacketCreatureType.Turn;
                c.Id = msg.GetUInt32();
                stream.AddUInt32(c.Id);
                //uint8_t direction;
                c.Direction = msg.GetByte();
                stream.AddByte(c.Direction);

                creatures.Add(c);

                return true;
            }
            else
            {
                //item
                Objects.Item item = new Tibia.Objects.Item(Client, thingId);

                if (item.HasExtraByte)
                {
                    item.Count = msg.GetByte();
                    stream.AddByte(item.Count);
                }

                item.Loc = new Tibia.Objects.ItemLocation(pos);
                items.Add(item);

                return true;
            }
        }

        public override byte[] ToByteArray()
        {
            return stream.Packet;
        }
    }
}
