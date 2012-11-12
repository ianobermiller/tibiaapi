using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a player, which is just an extended version of creature.
    /// </summary>
    public class Player : Creature
    {
        /// <summary>
        /// Default constructor, same as Objects.Creature.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="address">The address.</param>
        public Player(Client client, uint address)
            : base(client, address) { }

        #region Packet Methods

        /// <summary>
        /// Turn to the specified direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Turn(Constants.Direction direction)
        {
            return client.Player.Turn(direction);
        }

        /// <summary>
        /// Walk in the specified direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Walk(Constants.Direction direction)
        {
            return client.Player.Walk(direction);
        }

        /// <summary>
        /// Walk in the specified list of directions.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Walk(List<Constants.Direction> list)
        {
            return client.Player.Walk(list);
        }

        /// <summary>
        /// Go to the specified location.
        /// </summary>
        /// <returns></returns>
        public Location GoTo
        {
            set
            {
                GoToX = (uint)value.X;
                GoToY = (uint)value.Y;
                GoToZ = (uint)value.Z;
                IsWalking = true;
            }
            get
            {
                return new Location((int)GoToX, (int)GoToY, (int)GoToZ);
            }
        }

        /// <summary>
        /// Stop all actions.
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            return client.Player.Stop();
        }

        /// <summary>
        /// Set the player's outfit. Sends a packet.
        /// </summary>
        public bool SetOutfit(Outfit outfit)
        {
            return client.Player.SetOutfit(outfit);
        }

        #endregion

        /// <summary>
        /// Check if the specified flag is set. Wrapper for Flags.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(Constants.Flag flag)
        {
            return client.Player.HasFlag(flag);
        }

        #region Get/Set Properties

        public new uint Id
        {
            get { return client.Player.Id; }
            set { client.Player.Id = value; }
        }
        public ulong Experience
        {
            get { return client.Player.Experience; }
            set { client.Player.Experience = value; }
        }
        public uint Flags
        {
            get { return client.Player.Flags; }
            set { client.Player.Flags = value; }
        }
        public uint Level
        {
            get { return client.Player.Level; }
            set { client.Player.Level = value; }
        }
        public uint LevelPercent
        {
            get { return client.Player.LevelPercent; }
            set { client.Player.LevelPercent = value; }
        }
        public uint MagicLevel
        {
            get { return client.Player.MagicLevel; }
            set { client.Player.MagicLevel = value; }
        }
        public uint MagicLevelPercent
        {
            get { return client.Player.MagicLevelPercent; }
            set { client.Player.MagicLevelPercent = value; }
        }

        public uint Mana
        {
            get { return client.Player.Mana; }
            set { client.Player.Mana = value; }
        }
        public uint ManaMax
        {
            get { return client.Player.ManaMax; }
            set { client.Player.ManaMax = value; }
        }
        public uint Health
        {
            get { return client.Player.Health; }
            set { client.Player.Health = value; }
        }
        public uint HealthMax
        {
            get { return client.Player.HealthMax; }
            set { client.Player.HealthMax = value; }
        }

        public uint Soul
        {
            get { return client.Player.Soul; }
            set { client.Player.Soul = value; }
        }
        public uint Capacity
        {
            get { return client.Player.Capacity; }
            set { client.Player.Capacity = value; }
        }
        public uint Stamina
        {
            get { return client.Player.Stamina; }
            set { client.Player.Stamina = value; }
        }
        public uint OfflineTraining
        {
            get { return client.Player.OfflineTraining; }
            set { client.Player.OfflineTraining = value; }
        }

        public uint Fist
        {
            get { return client.Player.Fist; }
            set { client.Player.Fist = value; }
        }
        public uint FistPercent
        {
            get { return client.Player.FistPercent; }
            set { client.Player.FistPercent = value; }
        }
        public uint Club
        {
            get { return client.Player.Club; }
            set { client.Player.Club = value; }
        }
        public uint ClubPercent
        {
            get { return client.Player.ClubPercent; }
            set { client.Player.ClubPercent = value; }
        }
        public uint Sword
        {
            get { return client.Player.Sword; }
            set { client.Player.Sword = value; }
        }
        public uint SwordPercent
        {
            get { return client.Player.SwordPercent; }
            set { client.Player.SwordPercent = value; }
        }
        public uint Axe
        {
            get { return client.Player.Axe; }
            set { client.Player.Axe = value; }
        }
        public uint AxePercent
        {
            get { return client.Player.AxePercent; }
            set { client.Player.AxePercent = value; }
        }
        public uint Distance
        {
            get { return client.Player.Distance; }
            set { client.Player.Distance = value; }
        }
        public uint DistancePercent
        {
            get { return client.Player.DistancePercent; }
            set { client.Player.DistancePercent = value; }
        }
        public uint Shielding
        {
            get { return client.Player.Shielding; }
            set { client.Player.Shielding = value; }
        }
        public uint ShieldingPercent
        {
            get { return client.Player.ShieldingPercent; }
            set { client.Player.ShieldingPercent = value; }
        }
        public uint Fishing
        {
            get { return client.Player.Fishing; }
            set { client.Player.Fishing = value; }
        }
        public uint FishingPercent
        {
            get { return client.Player.FishingPercent; }
            set { client.Player.FishingPercent = value; }
        }

        public uint GoToX
        {
            get { return client.Player.GoToX; }
            set { client.Player.GoToX = value; }
        }
        public uint GoToY
        {
            get { return client.Player.GoToY; }
            set { client.Player.GoToY = value; }
        }
        public uint GoToZ
        {
            get { return client.Player.GoToZ; }
            set { client.Player.GoToZ = value; }
        }

        public uint RedSquare
        {
            get { return client.Player.RedSquare; }
            set { client.Player.RedSquare = value; }
        }
        public uint GreenSquare
        {
            get { return client.Player.GreenSquare; }
            set { client.Player.GreenSquare = value; }
        }

        public uint TargetId
        {
            get { return client.Player.TargetId; }
            set { client.Player.TargetId = value; }
        }
        public new uint Z
        {
            get { return client.Player.Z; }
            set { client.Player.Z = value; }
        }
        public new uint Y
        {
            get { return client.Player.Y; }
            set { client.Player.Y = value; }
        }
        public new uint X
        {
            get { return client.Player.X; }
            set { client.Player.X = value; }
        }

        public string WorldName
        {
            get { return client.Player.WorldName; }
        }

        public uint AttackCount
        {
            get
            {
                return client.Player.AttackCount;
            }
            set
            {
                client.Player.AttackCount = value;
            }
        }
        public uint FollowCount
        {
            get
            {
                return client.Player.FollowCount;
            }
            set
            {
                client.Player.FollowCount = value;
            }
        }

        public uint XORKey
        {
            get { return client.Player.XORKey; }
            set { client.Player.XORKey = value; }
        }

        public uint WhiteSquare
        {
            get { return client.Player.AccessN; }
            set { client.Player.AccessN = value; }
        }
        public uint AccessN
        {
            get { return client.Player.AccessN; }
            set { client.Player.AccessN = value; }
        }
        public uint AccessS
        {
            get { return client.Player.AccessS; }
            set { client.Player.AccessS = value; }
        }
        public uint TargetType
        {
            get { return client.Player.TargetType; }
            set { client.Player.TargetType = value; }
        }
        public uint TargetBattlelistId
        {
            get { return client.Player.TargetBattlelistId; }
            set { client.Player.TargetBattlelistId = value; }
        }
        public uint TargetBattlelistType
        {
            get { return client.Player.TargetBattlelistType; }
            set { client.Player.TargetBattlelistType = value; }
        }
        #endregion
    }
}
