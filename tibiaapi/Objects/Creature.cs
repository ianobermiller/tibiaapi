using System;
using System.Linq;
using Tibia.Util;


namespace Tibia.Objects
{
    /// <summary>
    /// Creature object.
    /// </summary>
    public class Creature
    {
        protected Client client;
        protected uint address;
        protected Timer tmrPositionTracker;

        protected Location lastLocation;

        private bool trackPosition;

        public bool TrackPosition
        {
            get => trackPosition;
            set
            {
                if (value && !trackPosition)
                {
                    StartTracking();
                }
                trackPosition = value;
            }
        }

        /// <summary>
        /// Create a new creature object with the given client and address.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="address">The address.</param>
        public Creature(Client client, uint address)
        {
            this.client = client;
            this.address = address;

            if (TrackPosition)
            {
                StartTracking();
            }
        }

        private void StartTracking()
        {
            lastLocation = Location;
            this.tmrPositionTracker = new Timer(50, false);
            tmrPositionTracker.Execute += TmrPositionTracker_Execute;
            tmrPositionTracker.Start();
        }

        private void TmrPositionTracker_Execute()
        {
            tmrPositionTracker.Stop();
            if (Location != lastLocation)
            {
                PositionChanged(new OnPositionChangedArgs { newLocation = Location, oldLocation = lastLocation });
                lastLocation = Location;
            }
            tmrPositionTracker.Start();
        }

        /// <summary>
        /// Check if a player (creature) is in your party.
        /// </summary>
        /// <returns>True if the player is a member or leader of your party. False otherwise.</returns>
        public bool InParty()
        {
            Constants.PartyShield party = PartyShield;
            return (party != Constants.PartyShield.None
                && party != Constants.PartyShield.Invitee
                && party != Constants.PartyShield.Inviter);
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
            uint playerZ = client.Player.Z;
            var creatures = client.BattleList.GetCreatures().Where(c => c.Z == playerZ);
            uint playerId = client.Player.Id;

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
            return (Id == client.Player.Id);
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
            if (client.VersionNumber >= 860)
            {
                if (client.Player.TargetId != Id)
                {
                    if (client.VersionNumber >= 872)
                    {
                        client.Player.AttackCount += 2;
                    }
                    else
                    {
                        client.Player.AttackCount += 1;
                    }
                }
            }
            client.Player.TargetId = Id;
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
            if (client.VersionNumber >= 860)
            {
                if (client.Player.TargetId != Id)
                {
                    if (client.VersionNumber >= 872)
                    {
                        client.Player.FollowCount += 2;
                    }
                    else
                    {
                        client.Player.FollowCount += 1;
                    }
                }
            }
            client.Player.GreenSquare = Id;
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

        public uint Id
        {
            get { return client.Memory.ReadUInt32(address + Addresses.Creature.DistanceId); }
            set { client.Memory.WriteUInt32(address + Addresses.Creature.DistanceId, value); }
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

        public Constants.PartyShield PartyShield
        {
            get { return (Constants.PartyShield)client.Memory.ReadInt32(address + Addresses.Creature.DistanceParty); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceParty, (int)value); }
        }

        public Constants.WarIcon WarIcon
        {
            get { return (Constants.WarIcon)client.Memory.ReadInt32(address + Addresses.Creature.DistanceWarIcon); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceWarIcon, (int)value); }
        }

        public bool IsBlocking
        {
            get { return Convert.ToBoolean(client.Memory.ReadInt32(address + Addresses.Creature.DistanceIsBlocking)); }
            set { client.Memory.WriteByte(address + Addresses.Creature.DistanceIsBlocking, Convert.ToByte(value)); }
        }

        public Constants.OutfitType OutfitType
        {
            get { return (Constants.OutfitType)client.Memory.ReadInt32(address + Addresses.Creature.DistanceOutfit); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceOutfit, (int)value); }
        }

        public int HeadColor
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorHead); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorHead, value); }
        }

        public int BodyColor
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorBody); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorBody, value); }
        }

        public int LegsColor
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorLegs); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorLegs, value); }
        }

        public int FeetColor
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceColorFeet); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceColorFeet, value); }
        }

        public Constants.OutfitAddon Addon
        {
            get { return (Constants.OutfitAddon)client.Memory.ReadInt32(address + Addresses.Creature.DistanceAddon); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceAddon, (int)value); }
        }

        public int MountId
        {
            get { return client.Memory.ReadInt32(address + Addresses.Creature.DistanceMountId); }
            set { client.Memory.WriteInt32(address + Addresses.Creature.DistanceMountId, (int)value); }
        }

        public Outfit Outfit
        {
            get
            {
                return new Outfit(this, (ushort)OutfitType, (byte)HeadColor, (byte)BodyColor,
                    (byte)LegsColor, (byte)FeetColor, (byte)Addon, (byte)MountId);
            }
            set
            {
                OutfitType = (Constants.OutfitType)value.LookType;
                HeadColor = (int)value.Head;
                BodyColor = (int)value.Body;
                LegsColor = (int)value.Legs;
                FeetColor = (int)value.Feet;
                Addon = (Constants.OutfitAddon)value.Addons;
                MountId = value.MountId;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}%", Name, HPBar.ToString());
        }

        public event EventHandler<OnPositionChangedArgs> OnPositionChanged;

        protected virtual void PositionChanged(OnPositionChangedArgs e)
        {
            OnPositionChanged?.Invoke(this, e);
        }
    }

    public class OnPositionChangedArgs : EventArgs
    {
        public Location oldLocation;
        public Location newLocation;
    }
}
