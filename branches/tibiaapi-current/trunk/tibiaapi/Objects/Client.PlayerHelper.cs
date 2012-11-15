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
                return Packets.Outgoing.RotatePacket.Send(client, direction);
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

                return Packets.Outgoing.CancelPacket.Send(client);
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
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Id); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Id, value); }
            }
            public ulong Experience
            {
                get { return client.Memory.ReadUInt64(client.Addresses.Player.Experience); }
                set { client.Memory.WriteUInt64(client.Addresses.Player.Experience, value); }
            }
            public uint Flags
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Flags); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Flags, value); }
            }
            public uint Level
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Level); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Level, value); }
            }
            public uint LevelPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.LevelPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.LevelPercent, value); }
            }
            public uint MagicLevel
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.MagicLevel); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.MagicLevel, value); }
            }
            public uint MagicLevelPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.MagicLevelPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.MagicLevelPercent, value); }
            }
            public uint Mana
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Mana) ^ XORKey; }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Mana, value ^ XORKey); }
            }
            public uint ManaMax
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.ManaMax) ^ XORKey; }
                set { client.Memory.WriteUInt32(client.Addresses.Player.ManaMax, value ^ XORKey); }
            }
            public uint Health
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Health) ^ XORKey; }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Health, value ^ XORKey); }
            }
            public uint HealthMax
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.HealthMax) ^ XORKey; }
                set { client.Memory.WriteUInt32(client.Addresses.Player.HealthMax, value ^ XORKey); }
            }
            public uint Soul
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Soul); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Soul, value); }
            }
            public uint Capacity
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Capacity) ^ XORKey; }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Capacity, value ^ XORKey); }
            }
            public uint Stamina
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Stamina); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Stamina, value); }
            }
            public uint OfflineTraining
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.OfflineTraining); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.OfflineTraining, value); }
            }
            public uint Fist
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Fist); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Fist, value); }
            }
            public uint FistPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.FistPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.FistPercent, value); }
            }
            public uint Club
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Club); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Club, value); }
            }
            public uint ClubPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.ClubPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.ClubPercent, value); }
            }
            public uint Sword
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Sword); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Sword, value); }
            }
            public uint SwordPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.SwordPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.SwordPercent, value); }
            }
            public uint Axe
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Axe); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Axe, value); }
            }
            public uint AxePercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.AxePercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.AxePercent, value); }
            }
            public uint Distance
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Distance); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Distance, value); }
            }
            public uint DistancePercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.DistancePercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.DistancePercent, value); }
            }
            public uint Shielding
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Shielding); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Shielding, value); }
            }
            public uint ShieldingPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.ShieldingPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.ShieldingPercent, value); }
            }
            public uint Fishing
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Fishing); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Fishing, value); }
            }
            public uint FishingPercent
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.FishingPercent); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.FishingPercent, value); }
            }
            public uint GoToX
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.GoToX); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.GoToX, value); }
            }
            public uint GoToY
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.GoToY); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.GoToY, value); }
            }
            public uint GoToZ
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.GoToZ); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.GoToZ, value); }
            }
            public uint RedSquare
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.RedSquare); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.RedSquare, value); }
            }
            public uint GreenSquare
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.GreenSquare); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.GreenSquare, value); }
            }
            public uint TargetId
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.TargetId); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.TargetId, value); }
            }

            public new uint Z
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Z); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Z, value); }
            }
            public new uint Y
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.Y); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.Y, value); }
            }
            public new uint X
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.X); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.X, value); }
            }
            public string WorldName
            {
                get { return client.Login.CharacterList[client.Login.SelectedChar].WorldName; }
            }
            public uint AttackCount
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.AttackCount); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.AttackCount, value); }
            }
            public uint FollowCount
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.FollowCount); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.FollowCount, value); }
            }

            public uint XORKey
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.XORKey); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.XORKey, value); }
            }


            [System.Obsolete]
            public uint WhiteSquare
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.WhiteSquare); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.WhiteSquare, value); }
            }
            [System.Obsolete]
            public uint TargetType
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.TargetType); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.TargetType, value); }
            }
            [System.Obsolete]
            public uint TargetBattlelistId
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.TargetBattlelistId); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.TargetBattlelistId, value); }
            }
            [System.Obsolete]
            public uint TargetBattlelistType
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.TargetBattlelistType); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.TargetBattlelistType, value); }
            }
            [System.Obsolete]
            public uint AccessN
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.AccessN); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.AccessN, value); }
            }
            [System.Obsolete]
            public uint AccessS
            {
                get { return client.Memory.ReadUInt32(client.Addresses.Player.AccessS); }
                set { client.Memory.WriteUInt32(client.Addresses.Player.AccessS, value); }
            }

            #endregion
        }
    }
}
