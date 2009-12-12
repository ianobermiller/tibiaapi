using System;
using System.Linq;
using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Incoming
{
    public class MapDescriptionPacket : MapPacket
    {
        public MapDescriptionPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MapDescription;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MapDescription)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MapDescription;
            outMsg.AddByte((byte)Type);

            try
            {
                Client.playerLocation = msg.GetLocation();
                outMsg.AddLocation(Client.playerLocation);
                SetMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 18, 14, outMsg);
            }
            catch (Exception)
            {
                msg.Position = msgPosition;
                outMsg.Position = outMsgPosition;
                return false;
            }

            return true;   
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            Tile playerTile = Client.Map.GetTileWithPlayer();
            msg.AddByte((byte)Type);
            msg.AddLocation(playerTile.Location);

            //GetMapDescription(playerLocation.X - 8, playerLocation.Y - 6, playerLocation.Z, 18, 14, msg);

            int startz, endz, zstep;

            if (playerTile.Location.Z > 7)
            {
                startz = playerTile.Location.Z - 2;
                endz = Math.Min(15, playerTile.Location.Z + 2);
                zstep = 1;
            }
            else
            {
                startz = 7;
                endz = 0;
                zstep = -1;
            }

            uint mapStart = Client.Memory.ReadUInt32(Addresses.Map.MapPointer);

            for (int z = startz; z != endz + zstep; z += zstep)
            {
                for (int x = 0; x < 18; x++)
                {
                    for (int y = 0; y < 14; y++)
                    {
                        Location memLoc = new Location(x, y, z);
                        uint num = memLoc.ToTileNumber();
                        Tile pCurrent = new Tile(Client, mapStart + (Addresses.Map.StepTile * num), num);

                        foreach (TileObject o in pCurrent.Objects)
                        {
                            if (o.Id == 0x0063)
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

                                msg.AddByte((byte)c.WarIcon);

                                msg.AddByte(Convert.ToByte(c.IsBlocking));
                            }
                            else
                            {
                                Item item = new Item(Client, (uint)o.Id);

                                msg.AddUInt16((ushort)item.Id);

                                if (item.HasExtraByte)
                                {
                                    msg.AddByte(item.Count);
                                }
                            }
                        }

                        msg.AddByte(0x00);
                        msg.AddByte(0xFF);
                    }
                }
            }

        }
    }
}
