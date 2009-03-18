using System;
using System.Collections.Generic;
using System.Text;
using Tibia.Objects;

namespace Tibia.Packets.Incoming
{
    public abstract class MapPacket : IncomingPacket
    {
        protected short m_skipTiles;

        protected List<Tile> tiles = new List<Tile>();
        protected List<PacketCreature> creatures = new List<PacketCreature>();

        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        public List<PacketCreature> Creatures
        {
            get { return creatures; }
        }

        public MapPacket(Objects.Client c)
            : base(c)
        {
            Destination = PacketDestination.Client;
        }

        public override abstract bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg);

        protected bool setMapDescription(NetworkMessage msg, int x, int y, int z, int width, int height, NetworkMessage outMsg)
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
                if (!setFloorDescription(msg, x, y, nz, width, height, z - nz, outMsg))
                    return false;
            }

            return true;
        }

        protected bool setFloorDescription(NetworkMessage msg, int x, int y, int z, int width, int height, int offset, NetworkMessage outMsg)
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
                            outMsg.AddUInt16(skipTiles);

                            m_skipTiles = (short)(skipTiles & 0xFF);
                        }
                        else
                        {
                            //real tile so read tile
                            Objects.Location pos = new Tibia.Objects.Location(x + nx + offset, y + ny + offset, z);

                            if (!setTileDescription(msg, pos, outMsg))
                            {
                                return false;
                            }
                            //read skip tiles info
                            skipTiles = msg.GetUInt16();
                            outMsg.AddUInt16(skipTiles);

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

        protected bool setTileDescription(NetworkMessage msg, Objects.Location pos, NetworkMessage outMsg)
        {
            int n = 0;
            bool ret = true;
            Tile tile = new Tile(Client, 0, pos);
            while (true)
            {
                //avoid infinite loop
                n++;

                ushort inspectTileId = msg.PeekUInt16();

                if (inspectTileId >= 0xFF00)
                {
                    //end of the tile
                    ret = true;
                    break;
                }
                else
                {
                    if (n > 10)
                    {
                        ret = false;
                        break;
                    }
                    //read tile things: items and creatures
                    internalGetThing(msg, pos, tile, n, outMsg);
                }
            }

            tiles.Add(tile);
            return ret;
        }

        protected bool internalGetThing(NetworkMessage msg, Location pos, Tile tile, int n, NetworkMessage outMsg)
        {
            //get thing type
            ushort thingId = msg.GetUInt16();
            outMsg.AddUInt16(thingId);

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
                    outMsg.AddUInt32(c.Id); //creatureid
                }
                else if (thingId == 0x0061)
                { //creature is not known
                    //perhaps we have to remove a known creature
                    c.RemoveId = msg.GetUInt32();
                    outMsg.AddUInt32(c.RemoveId);

                    //add a new creature
                    c.Type = PacketCreatureType.Unknown;
                    c.Id = msg.GetUInt32();
                    outMsg.AddUInt32(c.Id);

                    c.Name = msg.GetString();
                    outMsg.AddString(c.Name);
                }

                //read creature properties
                c.Health = msg.GetByte();
                outMsg.AddByte(c.Health);

                c.Direction = msg.GetByte();
                outMsg.AddByte(c.Direction);

                c.Outfit = msg.GetOutfit();
                outMsg.AddOutfit(c.Outfit);

                c.LightLevel = msg.GetByte();
                outMsg.AddByte(c.LightLevel);

                c.LightColor = msg.GetByte();
                outMsg.AddByte(c.LightColor);

                c.Speed = msg.GetUInt16();
                outMsg.AddUInt16(c.Speed);

                c.Skull = (Constants.Skull)msg.GetByte();
                outMsg.AddByte((byte)c.Skull);

                c.PartyShield = (PartyShield)msg.GetByte();
                outMsg.AddByte((byte)c.PartyShield);

                creatures.Add(c);

                return true;
            }
            else if (thingId == 0x0063)
            {
                //creature turn
                c = new PacketCreature(Client);
                c.Location = pos;
                c.Type = PacketCreatureType.Turn;
                c.Id = msg.GetUInt32();
                outMsg.AddUInt32(c.Id);

                c.Direction = msg.GetByte();
                outMsg.AddByte(c.Direction);

                creatures.Add(c);

                return true;
            }
            else
            {
                //item
                Item item = new Item(Client, thingId, 0, "", ItemLocation.FromLocation(pos, (byte)n));

                if (item.HasExtraByte)
                {
                    item.Count = msg.GetByte();
                    outMsg.AddByte(item.Count);
                }

                if (n == 0) // first item is ground
                    tile.Ground = item;
                else
                    tile.Items.Add(item);

                return true;
            }
        }
    }
}
