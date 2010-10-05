using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Constants;
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

        protected bool ParseMapDescription(NetworkMessage msg, int x, int y, int z, int width, int height, NetworkMessage outMsg)
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
                if (!ParseFloorDescription(msg, x, y, nz, width, height, z - nz, outMsg))
                    return false;
            }

            return true;
        }

        protected bool ParseFloorDescription(NetworkMessage msg, int x, int y, int z, int width, int height, int offset, NetworkMessage outMsg)
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

                            if (!ParseTileDescription(msg, pos, outMsg))
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

        protected bool ParseTileDescription(NetworkMessage msg, Objects.Location pos, NetworkMessage outMsg)
        {
            int n = 0;
            bool ret = true;
            Tile tile = new Tile(Client, 0, pos);
            while (true)
            {
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
                    ParseThing(msg, pos, tile, n, outMsg);
                }

                n++;
            }

            tiles.Add(tile);
            return ret;
        }

        protected bool ParseThing(NetworkMessage msg, Location pos, Tile tile, int n, NetworkMessage outMsg)
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

                c.PartyShield = (Constants.PartyShield)msg.GetByte();
                outMsg.AddByte((byte)c.PartyShield);

                if (Client.VersionNumber >= 853)
                {
                    if (thingId == 0x0061)
                    {
                        c.WarIcon = (Constants.WarIcon)msg.GetByte();
                        outMsg.AddByte((byte)c.WarIcon);
                    }

                    c.IsBlocking = msg.GetByte().Equals(0x01);
                    outMsg.AddByte(Convert.ToByte(c.IsBlocking));
                }

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
                UInt16 itemId;
                if (thingId == UInt16.MaxValue)
                {
                    itemId = msg.GetUInt16();
                    outMsg.AddUInt16(itemId);
                }
                else
                    itemId = thingId;

                Item item = new Item(Client, itemId, 0, "", ItemLocation.FromLocation(pos, (byte)n));

                if (item.HasExtraByte)
                {
                    item.Count = msg.GetByte();
                    outMsg.AddByte(item.Count);
                }

                if (n == 0)
                    tile.Ground = item;
                else
                    tile.Items.Add(item);

                return true;
            }
        }

        protected void GetMapDescription(int x, int y, int z, int width, int height, NetworkMessage msg)
        {
            int skip = -1;
            int startz, endz, zstep = 0;

            if (z > 7)
            {
                startz = z - 2;
                endz = Math.Min(16 - 1, z + 2);
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
                skip = GetFloorDescription(x, y, nz, width, height, z - nz, skip, msg);
            }

            if (skip >= 0)
            {
                msg.AddByte((byte)skip);
                msg.AddByte(0xFF);
            }
        }

        protected int GetFloorDescription(int x, int y, int z, int width, int height, int offset, int skip, NetworkMessage msg)
        {
            Tile tile;

            for (int nx = 0; nx < width; nx++)
            {
                for (int ny = 0; ny < height; ny++)
                {
                    tile = Client.Map.GetTile(new Location(x + nx + offset, y + ny + offset, z));
                    if (tile != null)
                    {
                        if (skip >= 0)
                        {
                            msg.AddByte((byte)skip);
                            msg.AddByte(0xFF);
                        }
                        skip = 0;


                        GetTileDescription(tile, msg);
                    }
                    else
                    {
                        skip++;
                        if (skip == 0xFF)
                        {
                            msg.AddByte(0xFF);
                            msg.AddByte(0xFF);
                            skip = -1;
                        }
                    }
                }
            }
            return skip;
        }

        protected void GetTileDescription(Tile tile, NetworkMessage msg)
        {
            if (tile != null)
            {
                List<TileObject> objects = tile.Objects;
                objects.Insert(0, new TileObject(tile.Ground.Id, tile.Ground.Count, 0));

                foreach (TileObject o in objects)
                {
                    if (o.Id <= 0)
                    {
                        return;
                    }
                    else if (o.Id == 0x0061 || o.Id == 0x0062 || o.Id == 0x0063)
                    {
                        // Add a creature
                        Creature c = Client.BattleList.GetCreatures().FirstOrDefault(cr => cr.Id == o.Data);

                        if (c == null)
                            throw new Exception("Creature does not exist.");

                        // Add as unknown
                        msg.AddUInt16((ushort)o.Id);

                        // No need to remove a creature
                        msg.AddUInt32(0);

                        // Add the creature id
                        msg.AddUInt32((uint)c.Id);

                        msg.AddString(c.Name);

                        msg.AddByte((byte)c.HPBar);

                        msg.AddByte((byte)c.Direction);

                        msg.AddOutfit(c.Outfit);

                        msg.AddByte((byte)c.Light);

                        msg.AddByte((byte)c.LightColor);

                        msg.AddUInt16((ushort)c.WalkSpeed);

                        msg.AddByte((byte)c.Skull);

                        msg.AddByte((byte)c.PartyShield);

                        if (Client.VersionNumber >= 853)
                        {
                            msg.AddByte((byte)c.WarIcon);
                            msg.AddByte(Convert.ToByte(c.IsBlocking));
                        }
                    }
                    else if (o.Id <= 9999)
                    {
                        // Add an item
                        Item item = new Item(Client, (uint)o.Id, (byte)o.Data, "",
                            ItemLocation.FromLocation(tile.Location, (byte)o.StackOrder));

                        msg.AddUInt16((ushort)o.Id);

                        try
                        {
                            if (item.HasExtraByte)
                            {
                                msg.AddByte(item.Count);
                            }
                        }
                        catch { }
                    }
                }
            }
        }
    }

    public class PacketCreature
    {
        public PacketCreatureType Type { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public byte Health { get; set; }
        public byte Direction { get; set; }
        public Objects.Outfit Outfit { get; set; }
        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }
        public ushort Speed { get; set; }
        public Skull Skull { get; set; }
        public PartyShield PartyShield { get; set; }
        public WarIcon WarIcon { get; set; }
        public bool IsBlocking { get; set; }
        public uint RemoveId { get; set; }
        public Objects.Location Location { get; set; }
        public Objects.Client Client { get; set; }

        public PacketCreature(Objects.Client client)
        {
            Client = client;
        }
    }
}
