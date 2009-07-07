using System;
using System.Collections.Generic;
using Tibia.Packets;

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
        /// Go to the specified location.
        /// </summary>
        /// <returns></returns>
        public Location GoTo
        {
            set
            {
                client.Memory.WriteInt32(Addresses.Player.GoToX, value.X);
                client.Memory.WriteInt32(Addresses.Player.GoToY, value.Y);
                client.Memory.WriteInt32(Addresses.Player.GoToZ, value.Z);
                IsWalking = true;
            }
            get
            {
                return new Location(
                    client.Memory.ReadInt32(Addresses.Player.GoToX),
                    client.Memory.ReadInt32(Addresses.Player.GoToY),
                    client.Memory.ReadInt32(Addresses.Player.GoToZ)
                );
            }
        }

        /// <summary>
        /// Stop all actions.
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            // Make sure the client stops walking 
            // if a destination has been chosen
            GoTo_X = 0;

            return Packets.Outgoing.CancelMovePacket.Send(client);
        }

        /// <summary>
        /// Set the player's outfit. Sends a packet.
        /// </summary>
        /// <param name="outfitType"></param>
        /// <param name="headColor"></param>
        /// <param name="bodyColor"></param>
        /// <param name="legsColor"></param>
        /// <param name="feetColor"></param>
        /// <param name="addons"></param>
        /// <returns></returns>
        public bool SetOutfit(Constants.OutfitType outfitType, byte headColor, byte bodyColor, byte legsColor, byte feetColor, Constants.OutfitAddon addons)
        {
            return Packets.Outgoing.SetOutfitPacket.Send(client, new Outfit((ushort)outfitType, headColor, bodyColor, legsColor, feetColor, (byte)addons));
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

        public new int Id
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Id); }
            set { client.Memory.WriteInt32(Addresses.Player.Id, value); }
        }
        public int Exp
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Exp); }
            set { client.Memory.WriteInt32(Addresses.Player.Exp, value); }
        }
        public int Flags
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Flags); }
            set { client.Memory.WriteInt32(Addresses.Player.Flags, value); }
        }
        public int Level
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Level); }
            set { client.Memory.WriteInt32(Addresses.Player.Level, value); }
        }
        public int Level_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.LevelPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.LevelPercent, value); }
        }
        public int MagicLevel
        {
            get { return client.Memory.ReadInt32(Addresses.Player.MagicLevel); }
            set { client.Memory.WriteInt32(Addresses.Player.MagicLevel, value); }
        }
        public int MagicLevel_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.MagicLevelPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.MagicLevelPercent, value); }
        }

        public int Mana
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Mana); }
            set { client.Memory.WriteInt32(Addresses.Player.Mana, value); }
        }
        public int Mana_Max
        {
            get { return client.Memory.ReadInt32(Addresses.Player.ManaMax); }
            set { client.Memory.WriteInt32(Addresses.Player.ManaMax, value); }
        }
        public int HP
        {
            get { return client.Memory.ReadInt32(Addresses.Player.HP); }
            set { client.Memory.WriteInt32(Addresses.Player.HP, value); }
        }
        public int HP_Max
        {
            get { return client.Memory.ReadInt32(Addresses.Player.HPMax); }
            set { client.Memory.WriteInt32(Addresses.Player.HPMax, value); }
        }

        public int Soul
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Soul); }
            set { client.Memory.WriteInt32(Addresses.Player.Soul, value); }
        }
        public int Cap
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Cap); }
            set { client.Memory.WriteInt32(Addresses.Player.Cap, value); }
        }
        public int Stamina
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Stamina); }
            set { client.Memory.WriteInt32(Addresses.Player.Stamina, value); }
        }

        public int Fist
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Fist); }
            set { client.Memory.WriteInt32(Addresses.Player.Fist, value); }
        }
        public int Fist_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.FistPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.FistPercent, value); }
        }
        public int Club
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Club); }
            set { client.Memory.WriteInt32(Addresses.Player.Club, value); }
        }
        public int Club_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.ClubPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.ClubPercent, value); }
        }
        public int Sword
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Sword); }
            set { client.Memory.WriteInt32(Addresses.Player.Sword, value); }
        }
        public int Sword_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.SwordPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.SwordPercent, value); }
        }
        public int Axe
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Axe); }
            set { client.Memory.WriteInt32(Addresses.Player.Axe, value); }
        }
        public int Axe_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.AxePercent); }
            set { client.Memory.WriteInt32(Addresses.Player.AxePercent, value); }
        }
        public int Distance
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Distance); }
            set { client.Memory.WriteInt32(Addresses.Player.Distance, value); }
        }
        public int Distance_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.DistancePercent); }
            set { client.Memory.WriteInt32(Addresses.Player.DistancePercent, value); }
        }
        public int Shielding
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Shielding); }
            set { client.Memory.WriteInt32(Addresses.Player.Shielding, value); }
        }
        public int Shielding_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.ShieldingPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.ShieldingPercent, value); }
        }
        public int Fishing
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Fishing); }
            set { client.Memory.WriteInt32(Addresses.Player.Fishing, value); }
        }
        public int Fishing_Percent
        {
            get { return client.Memory.ReadInt32(Addresses.Player.FishingPercent); }
            set { client.Memory.WriteInt32(Addresses.Player.FishingPercent, value); }
        }

        public int GoTo_X
        {
            get { return client.Memory.ReadInt32(Addresses.Player.GoToX); }
            set { client.Memory.WriteInt32(Addresses.Player.GoToX, value); }
        }
        public int GoTo_Y
        {
            get { return client.Memory.ReadInt32(Addresses.Player.GoToY); }
            set { client.Memory.WriteInt32(Addresses.Player.GoToY, value); }
        }
        public int GoTo_Z
        {
            get { return client.Memory.ReadInt32(Addresses.Player.GoToZ); }
            set { client.Memory.WriteInt32(Addresses.Player.GoToZ, value); }
        }

        public int RedSquare
        {
            get { return client.Memory.ReadInt32(Addresses.Player.RedSquare); }
            set { client.Memory.WriteInt32(Addresses.Player.RedSquare, value); }
        }
        public int GreenSquare
        {
            get { return client.Memory.ReadInt32(Addresses.Player.GreenSquare); }
            set { client.Memory.WriteInt32(Addresses.Player.GreenSquare, value); }
        }
        public int WhiteSquare
        {
            get { return client.Memory.ReadInt32(Addresses.Player.WhiteSquare); }
            set { client.Memory.WriteInt32(Addresses.Player.WhiteSquare, value); }
        }

        public int AccessN
        {
            get { return client.Memory.ReadInt32(Addresses.Player.AccessN); }
            set { client.Memory.WriteInt32(Addresses.Player.AccessN, value); }
        }
        public int AccessS
        {
            get { return client.Memory.ReadInt32(Addresses.Player.AccessS); }
            set { client.Memory.WriteInt32(Addresses.Player.AccessS, value); }
        }

        public int Target_ID
        {
            get { return client.Memory.ReadInt32(Addresses.Player.TargetID); }
            set { client.Memory.WriteInt32(Addresses.Player.TargetID, value); }
        }
        public int Target_Type
        {
            get { return client.Memory.ReadInt32(Addresses.Player.TargetType); }
            set { client.Memory.WriteInt32(Addresses.Player.TargetType, value); }
        }
        public int Target_BList_ID
        {
            get { return client.Memory.ReadInt32(Addresses.Player.TargetBListID); }
            set { client.Memory.WriteInt32(Addresses.Player.TargetBListID, value); }
        }
        public int Target_BList_Type
        {
            get { return client.Memory.ReadInt32(Addresses.Player.TargetBListType); }
            set { client.Memory.WriteInt32(Addresses.Player.TargetBListType, value); }
        }
        public new int Z
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Z); }
            set { client.Memory.WriteInt32(Addresses.Player.Z, value); }
        }
        public new int Y
        {
            get { return client.Memory.ReadInt32(Addresses.Player.Y); }
            set { client.Memory.WriteInt32(Addresses.Player.Y, value); }
        }
        public new int X
        {
            get { return client.Memory.ReadInt32(Addresses.Player.X); }
            set { client.Memory.WriteInt32(Addresses.Player.X, value); }
        }
        public string WorldName
        {
            get
            {
                return client.Login.CharacterList[client.Login.SelectedChar].WorldName;
            }
        }

        #endregion
    }
}
