using System;
using System.Linq;
using Tibia.Packets;

namespace Tibia.Objects
{
    /// <summary>
    /// Creature object.
    /// </summary>
    public class Creature
    {
        protected Client client;
        protected uint address;

        /// <summary>
        /// Create a new creature object with the given client and address.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="address">The address.</param>
        public Creature(Client client, uint address)
        {
            this.client = client;
            this.address = address;
        }

        /// <summary>
        /// Check if a player (creature) is in your party.
        /// </summary>
        /// <returns>True if the player is a member or leader of your party. False otherwise.</returns>
        public bool InParty()
        {
            Constants.Party party = Party;
            return (party != Constants.Party.None
                && party != Constants.Party.Invitee
                && party != Constants.Party.Inviter);
        }

        private static Location AdjustLocation(Location loc, int xDiff, int yDiff)
        {
            int xNew = loc.X - xDiff;
            int yNew = loc.Y - yDiff;

            if (xNew > 17)
                xNew -= 18;
            else if (xNew < 0)
                xNew += 18;

            if (yNew > 13)
                yNew -= 14;
            else if (yNew < 0)
                yNew += 14;

            return new Location(xNew, yNew, loc.Z);
        }


        /// <summary>
        /// Check if have a path to the creature.
        /// </summary>
        /// <returns></returns>
        public bool IsReachable()
        {
            var tileList = client.Map.GetTilesOnSameFloor();
            var playerTile = tileList.GetTileWithPlayer(Client);
            var creatureTile = tileList.GetTileWithCreature(Id);

            if (playerTile == null || creatureTile == null)
                return false;

            int xDiff, yDiff;
            int playerZ = client.Memory.ReadInt32(Addresses.Player.Z);
            var creatures = client.BattleList.GetCreatures().Where(c => c.Z == playerZ);
            int playerId = client.Memory.ReadInt32(Addresses.Player.Id);

            xDiff = (int)playerTile.MemoryLocation.X - 8;
            yDiff = (int)playerTile.MemoryLocation.Y - 6;

            playerTile.MemoryLocation = AdjustLocation(playerTile.MemoryLocation, xDiff, yDiff);
            creatureTile.MemoryLocation = AdjustLocation(creatureTile.MemoryLocation, xDiff, yDiff);

            foreach (Tile tile in tileList)
            {
                tile.MemoryLocation = AdjustLocation(tile.MemoryLocation, xDiff, yDiff);

                if (tile.Ground.GetFlag(Addresses.DatItem.Flag.Blocking) || tile.Ground.GetFlag(Addresses.DatItem.Flag.BlocksPath) ||
                    tile.Items.Any(i => i.GetFlag(Addresses.DatItem.Flag.Blocking) || i.GetFlag(Addresses.DatItem.Flag.BlocksPath) || client.PathFinder.ModifiedItems.ContainsKey(i.Id)) ||
                    creatures.Any(c => tile.Objects.Any(o => o.Data == c.Id && o.Data != playerId && o.Data != this.Id)))
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 0;
                }
                else
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 1;
                }
            }

            return client.PathFinder.FindPath(playerTile.MemoryLocation, creatureTile.MemoryLocation);
        }

        /// <summary>
        /// Check if the current creature is actually yourself.
        /// </summary>
        /// <returns>True if it's yourself, false otherwise</returns>
        public bool IsSelf()
        {
            return (Id == client.Memory.ReadInt32(Addresses.Player.Id));
        }

        public void Approach()
        {
            Player p = client.GetPlayer();
            p.GoTo = Location;
        }

        /// <summary>
        /// Check if the creature is attacking the player.
        /// </summary>
        /// <returns></returns>
        public bool IsAttacking()
        {
            return BlackSquare != 0;
        }

        /// <summary>
        /// Attack the creature.
        /// Sends a packet to the server and sets the red square around the creature.
        /// </summary>
        /// <returns></returns>
        public bool Attack()
        {
            client.Memory.WriteInt32(Addresses.Player.TargetID, Id);
            return Packets.Outgoing.AttackPacket.Send(client, (uint)Id);
        }

        /// <summary>
        /// Look at the creature / player.
        /// Sends a packet to the server with the same effect as
        /// shift-clicking or left-right clicking a creature.
        /// </summary>
        /// <returns></returns>
        public bool Look()
        {
            return Packets.Outgoing.LookAtPacket.Send(client, Location, 0x63, 1);
        }

        /// <summary>
        /// Follow the creature / player.
        /// </summary>
        /// <returns></returns>
        public bool Follow()
        {
            client.Memory.WriteInt32(Addresses.Player.GreenSquare, Id);
            return Packets.Outgoing.FollowPacket.Send(client, (uint)Id);
        }

        /// <summary>
        /// Gets the distance between player and creature / player.
        /// </summary>
        /// <returns></returns>
        public double DistanceTo(Location l)
        {
            return Location.DistanceTo(l);
        }

        public uint Address
        {
            get { return address; }
            set { address = value; }
        }

        #region Get/Set Methods

        public Client Client
        {
            get { return client; }
        }

        public int Id
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceId); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceId, value); }
        }

        public CreatureData Data
        {
            get
            {
                if (Constants.CreatureLists.AllCreatures.ContainsKey(Name))
                {
                    return Constants.CreatureLists.AllCreatures[Name];
                }
                else
                {
                    return null;
                }
            }
        }

        public Constants.CreatureType Type
        {
            get { return (Constants.CreatureType)client.Memory.ReadByte(address + Addresses.Creature.DistanceType); }
            set { client.Memory.WriteByte(address + Addresses.Creature.DistanceType, (byte)value); }
        }

        public string Name
        {
            get { return client.Memory.ReadString(address + Addresses.Creature.DistanceName); }
            set { client.Memory.WriteString(address + Addresses.Creature.DistanceName, value); }
        }

        public int X
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceX); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceX, value); }
        }

        public int Y
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceY); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceY, value); }
        }

        public int Z
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceZ); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceZ, value); }
        }

        public Location Location
        {
            get { return new Location(X, Y, Z); }
        }

        public int ScreenOffsetHoriz
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceScreenOffsetHoriz); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceScreenOffsetHoriz, value); }
        }

        public int ScreenOffsetVert
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceScreenOffsetVert); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceScreenOffsetVert, value); }
        }

        public bool IsWalking
        {
            get { return Convert.ToBoolean(client.Memory.ReadByte(address + Addresses.Creature.DistanceIsWalking)); }
            set { client.Memory.WriteByte(address + Addresses.Creature.DistanceIsWalking, Convert.ToByte(value)); }
        }

        public int WalkSpeed
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceWalkSpeed); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceWalkSpeed, value); }
        }

        public Constants.Direction Direction
        {
            get { return (Constants.Direction)client.Memory.ReadInt32(address + Addresses.Creature.DistanceDirection); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceDirection, (int)value); }
        }

        public bool IsVisible
        {
            get { return Convert.ToBoolean(client.Memory.ReadInt32(address + Addresses.Creature.DistanceIsVisible)); }
            set { client.Memory.WriteByte(address + Addresses.Creature.DistanceIsVisible, Convert.ToByte(value)); }
        }

        public int Light
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceLight); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceLight, value); }
        }

        public int LightColor
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceLightColor); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceLightColor, value); }
        }

        public int HPBar
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceHPBar); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceHPBar, value); }
        }

        public int BlackSquare
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceBlackSquare); }
        }

        public Constants.Skull Skull
        {
            get { return (Constants.Skull)client.Memory.ReadInt32(address + Addresses.Creature.DistanceSkull); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceSkull, (int)value); }
        }

        public Constants.Party Party
        {
            get { return (Constants.Party)client.Memory.ReadInt32(address + Addresses.Creature.DistanceParty); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceParty, (int)value); }
        }

        public Constants.WarIcon WarIcon
        {
            get { return (Constants.WarIcon)client.Memory.ReadInt32(address + Addresses.Creature.DistanceWarIcon); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceWarIcon, (int)value); }
        }

        public Constants.OutfitType OutfitType
        {
            get { return (Constants.OutfitType)client.Memory.ReadInt32(address + Addresses.Creature.DistanceOutfit); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceOutfit, (int)value); }
        }

        public int Color_Head
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorHead); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorHead, value); }
        }

        public int Color_Body
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorBody); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorBody, value); }
        }

        public int Color_Legs
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorLegs); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorLegs, value); }
        }

        public int Color_Feet
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorFeet); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorFeet, value); }
        }

        public Constants.OutfitAddon Addon
        {
            get { return (Constants.OutfitAddon)client.Memory.ReadInt32(address + Addresses.Creature.DistanceAddon); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceAddon, (int)value); }
        }

        public Outfit Outfit
        {
            get
            {
                return new Outfit(this, (ushort)OutfitType, (byte)Color_Head, (byte)Color_Body,
                    (byte)Color_Legs, (byte)Color_Feet, (byte)Addon);
            }
            set
            {
                OutfitType = (Constants.OutfitType)value.LookType;
                Color_Head = (int)value.Head;
                Color_Body = (int)value.Body;
                Color_Legs = (int)value.Legs;
                Color_Feet = (int)value.Feet;
                Addon = (Constants.OutfitAddon)value.Addons;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}%", Name, HPBar.ToString());
        }
    }
}
