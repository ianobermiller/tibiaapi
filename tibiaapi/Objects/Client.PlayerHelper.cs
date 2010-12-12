using System.Collections.Generic;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class PlayerHelper
        {
            private Client client;

            internal PlayerHelper(Client client)
            {
                this.client = client;
            }

            #region Packet Methods

            /// <summary>
            /// Turn to the specified direction.
            /// </summary>
            /// <param name="direction"></param>
            /// <returns></returns>
            public bool Turn(Constants.Direction direction)
            {
                return Packets.Outgoing.TurnPacket.Send(client, direction);
            }

            /// <summary>
            /// Walk in the specified direction
            /// </summary>
            /// <param name="direction"></param>
            /// <returns></returns>
            public bool Walk(Constants.Direction direction)
            {
                return Packets.Outgoing.MovePacket.Send(client, direction);
            }

            /// <summary>
            /// Walk in the specified list of directions.
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public bool Walk(List<Constants.Direction> list)
            {
                return Packets.Outgoing.AutoWalkPacket.Send(client, list);
            }

            /// <summary>
            /// Stop all actions.
            /// </summary>
            public bool Stop()
            {
                // Make sure the client stops walking 
                // if a destination has been chosen
                GoToX = 0;

                return Packets.Outgoing.CancelMovePacket.Send(client);
            }

            /// <summary>
            /// Set the player's outfit. Sends a packet.
            /// </summary>
            public bool SetOutfit(Constants.OutfitType outfitType, byte headColor, byte bodyColor, byte legsColor, byte feetColor, Constants.OutfitAddon addons, byte mountId)
            {
                return Packets.Outgoing.SetOutfitPacket.Send(client, new Outfit((ushort)outfitType, headColor, bodyColor, legsColor, feetColor, (byte)addons, mountId));
            }

            /// <summary>
            /// Set the player's outfit. Sends a packet.
            /// </summary>
            public bool SetOutfit(Outfit outfit)
            {
                return Packets.Outgoing.SetOutfitPacket.Send(client, outfit);
            }

            #endregion

            /// <summary>
            /// Check if the specified flag is set. Wrapper for Flags.
            /// </summary>
            /// <param name="flag"></param>
            /// <returns></returns>
            public bool HasFlag(Constants.Flag flag)
            {
                return (Flags & (int)flag) == (int)flag;
            }

            #region Get/Set Properties

            public new uint Id
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Id); }
                set { client.Memory.WriteUInt32(Addresses.Player.Id, value); }
            }
            public ulong Experience
            {
                get { return client.Memory.ReadUInt64(Addresses.Player.Experience); }
                set { client.Memory.WriteUInt64(Addresses.Player.Experience, value); }
            }
            public uint Flags
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Flags); }
                set { client.Memory.WriteUInt32(Addresses.Player.Flags, value); }
            }
            public uint Level
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Level); }
                set { client.Memory.WriteUInt32(Addresses.Player.Level, value); }
            }
            public uint LevelPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.LevelPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.LevelPercent, value); }
            }
            public uint MagicLevel
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.MagicLevel); }
                set { client.Memory.WriteUInt32(Addresses.Player.MagicLevel, value); }
            }
            public uint MagicLevelPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.MagicLevelPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.MagicLevelPercent, value); }
            }

            public uint Mana
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Mana); }
                set { client.Memory.WriteUInt32(Addresses.Player.Mana, value); }
            }
            public uint ManaMax
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.ManaMax); }
                set { client.Memory.WriteUInt32(Addresses.Player.ManaMax, value); }
            }
            public uint Health
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Health); }
                set { client.Memory.WriteUInt32(Addresses.Player.Health, value); }
            }
            public uint HealthMax
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.HealthMax); }
                set { client.Memory.WriteUInt32(Addresses.Player.HealthMax, value); }
            }

            public uint Soul
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Soul); }
                set { client.Memory.WriteUInt32(Addresses.Player.Soul, value); }
            }
            public uint Capacity
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Capacity); }
                set { client.Memory.WriteUInt32(Addresses.Player.Capacity, value); }
            }
            public uint Stamina
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Stamina); }
                set { client.Memory.WriteUInt32(Addresses.Player.Stamina, value); }
            }

            public uint Fist
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Fist); }
                set { client.Memory.WriteUInt32(Addresses.Player.Fist, value); }
            }
            public uint FistPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.FistPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.FistPercent, value); }
            }
            public uint Club
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Club); }
                set { client.Memory.WriteUInt32(Addresses.Player.Club, value); }
            }
            public uint ClubPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.ClubPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.ClubPercent, value); }
            }
            public uint Sword
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Sword); }
                set { client.Memory.WriteUInt32(Addresses.Player.Sword, value); }
            }
            public uint SwordPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.SwordPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.SwordPercent, value); }
            }
            public uint Axe
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Axe); }
                set { client.Memory.WriteUInt32(Addresses.Player.Axe, value); }
            }
            public uint AxePercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.AxePercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.AxePercent, value); }
            }
            public uint Distance
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Distance); }
                set { client.Memory.WriteUInt32(Addresses.Player.Distance, value); }
            }
            public uint DistancePercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.DistancePercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.DistancePercent, value); }
            }
            public uint Shielding
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Shielding); }
                set { client.Memory.WriteUInt32(Addresses.Player.Shielding, value); }
            }
            public uint ShieldingPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.ShieldingPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.ShieldingPercent, value); }
            }
            public uint Fishing
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Fishing); }
                set { client.Memory.WriteUInt32(Addresses.Player.Fishing, value); }
            }
            public uint FishingPercent
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.FishingPercent); }
                set { client.Memory.WriteUInt32(Addresses.Player.FishingPercent, value); }
            }

            public uint GoToX
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.GoToX); }
                set { client.Memory.WriteUInt32(Addresses.Player.GoToX, value); }
            }
            public uint GoToY
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.GoToY); }
                set { client.Memory.WriteUInt32(Addresses.Player.GoToY, value); }
            }
            public uint GoToZ
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.GoToZ); }
                set { client.Memory.WriteUInt32(Addresses.Player.GoToZ, value); }
            }

            public uint RedSquare
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.RedSquare); }
                set { client.Memory.WriteUInt32(Addresses.Player.RedSquare, value); }
            }
            public uint GreenSquare
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.GreenSquare); }
                set { client.Memory.WriteUInt32(Addresses.Player.GreenSquare, value); }
            }
            public uint WhiteSquare
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.WhiteSquare); }
                set { client.Memory.WriteUInt32(Addresses.Player.WhiteSquare, value); }
            }

            public uint AccessN
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.AccessN); }
                set { client.Memory.WriteUInt32(Addresses.Player.AccessN, value); }
            }
            public uint AccessS
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.AccessS); }
                set { client.Memory.WriteUInt32(Addresses.Player.AccessS, value); }
            }

            public uint TargetId
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.TargetId); }
                set { client.Memory.WriteUInt32(Addresses.Player.TargetId, value); }
            }
            public uint TargetType
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.TargetType); }
                set { client.Memory.WriteUInt32(Addresses.Player.TargetType, value); }
            }
            public uint TargetBattlelistId
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.TargetBattlelistId); }
                set { client.Memory.WriteUInt32(Addresses.Player.TargetBattlelistId, value); }
            }
            public uint TargetBattlelistType
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.TargetBattlelistType); }
                set { client.Memory.WriteUInt32(Addresses.Player.TargetBattlelistType, value); }
            }
            public new uint Z
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Z); }
                set { client.Memory.WriteUInt32(Addresses.Player.Z, value); }
            }
            public new uint Y
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.Y); }
                set { client.Memory.WriteUInt32(Addresses.Player.Y, value); }
            }
            public new uint X
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.X); }
                set { client.Memory.WriteUInt32(Addresses.Player.X, value); }
            }
            public string WorldName
            {
                get { return client.Login.CharacterList[client.Login.SelectedChar].WorldName; }
            }

            /// <summary>
            /// The number of times a player has attacked
            /// </summary>
            public uint AttackCount
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.AttackCount); }
                set { client.Memory.WriteUInt32(Addresses.Player.AttackCount, value); }
            }

            /// <summary>
            /// The number of times a player has followed
            /// </summary>
            public uint FollowCount
            {
                get { return client.Memory.ReadUInt32(Addresses.Player.FollowCount); }
                set { client.Memory.WriteUInt32(Addresses.Player.FollowCount, value); }
            }

            #endregion
        }
    }
}
