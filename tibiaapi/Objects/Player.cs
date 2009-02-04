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
                client.WriteInt32(Addresses.Player.GoTo_X, value.X);
                client.WriteInt32(Addresses.Player.GoTo_Y, value.Y);
                client.WriteInt32(Addresses.Player.GoTo_Z, value.Z);
                IsWalking = true;
            }
            get
            {
                return new Location(
                    client.ReadInt32(Addresses.Player.GoTo_X),
                    client.ReadInt32(Addresses.Player.GoTo_Y),
                    client.ReadInt32(Addresses.Player.GoTo_Z)
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
        /// <summary>
        /// Get the experience still needed for the next level.
        /// </summary>
        /// <returns></returns>
        public long ExpLeft()
        {
            return ExpLeft(Level + 1);
        }

        /// <summary>
        /// Get experience still needed for a specified level.
        /// </summary>
        /// <param name="levelNeeded"></param>
        /// <returns></returns>
        public long ExpLeft(int levelNeeded)
        {
            return Calculate.ExpForLevel(levelNeeded) - Exp;
        }

        #region Get/Set Properties

        public new int Id
        {
            get { return client.ReadInt32(Addresses.Player.Id); }
            set { client.WriteInt32(Addresses.Player.Id, value); }
        }
        public int Exp
        {
            get { return client.ReadInt32(Addresses.Player.Exp); }
            set { client.WriteInt32(Addresses.Player.Exp, value); }
        }
        public int Flags
        {
            get { return client.ReadInt32(Addresses.Player.Flags); }
            set { client.WriteInt32(Addresses.Player.Flags, value); }
        }
        public int Level
        {
            get { return client.ReadInt32(Addresses.Player.Level); }
            set { client.WriteInt32(Addresses.Player.Level, value); }
        }
        public int Level_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Level_Percent); }
            set { client.WriteInt32(Addresses.Player.Level_Percent, value); }
        }
        public int MagicLevel
        {
            get { return client.ReadInt32(Addresses.Player.MagicLevel); }
            set { client.WriteInt32(Addresses.Player.MagicLevel, value); }
        }
        public int MagicLevel_Percent
        {
            get { return client.ReadInt32(Addresses.Player.MagicLevel_Percent); }
            set { client.WriteInt32(Addresses.Player.MagicLevel_Percent, value); }
        }

        public int Mana
        {
            get { return client.ReadInt32(Addresses.Player.Mana); }
            set { client.WriteInt32(Addresses.Player.Mana, value); }
        }
        public int Mana_Max
        {
            get { return client.ReadInt32(Addresses.Player.Mana_Max); }
            set { client.WriteInt32(Addresses.Player.Mana_Max, value); }
        }
        public int HP
        {
            get { return client.ReadInt32(Addresses.Player.HP); }
            set { client.WriteInt32(Addresses.Player.HP, value); }
        }
        public int HP_Max
        {
            get { return client.ReadInt32(Addresses.Player.HP_Max); }
            set { client.WriteInt32(Addresses.Player.HP_Max, value); }
        }

        public int Soul
        {
            get { return client.ReadInt32(Addresses.Player.Soul); }
            set { client.WriteInt32(Addresses.Player.Soul, value); }
        }
        public int Cap
        {
            get { return client.ReadInt32(Addresses.Player.Cap); }
            set { client.WriteInt32(Addresses.Player.Cap, value); }
        }
        public int Stamina
        {
            get { return client.ReadInt32(Addresses.Player.Stamina); }
            set { client.WriteInt32(Addresses.Player.Stamina, value); }
        }

        public int Fist
        {
            get { return client.ReadInt32(Addresses.Player.Fist); }
            set { client.WriteInt32(Addresses.Player.Fist, value); }
        }
        public int Fist_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Fist_Percent); }
            set { client.WriteInt32(Addresses.Player.Fist_Percent, value); }
        }
        public int Club
        {
            get { return client.ReadInt32(Addresses.Player.Club); }
            set { client.WriteInt32(Addresses.Player.Club, value); }
        }
        public int Club_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Club_Percent); }
            set { client.WriteInt32(Addresses.Player.Club_Percent, value); }
        }
        public int Sword
        {
            get { return client.ReadInt32(Addresses.Player.Sword); }
            set { client.WriteInt32(Addresses.Player.Sword, value); }
        }
        public int Sword_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Sword_Percent); }
            set { client.WriteInt32(Addresses.Player.Sword_Percent, value); }
        }
        public int Axe
        {
            get { return client.ReadInt32(Addresses.Player.Axe); }
            set { client.WriteInt32(Addresses.Player.Axe, value); }
        }
        public int Axe_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Axe_Percent); }
            set { client.WriteInt32(Addresses.Player.Axe_Percent, value); }
        }
        public int Distance
        {
            get { return client.ReadInt32(Addresses.Player.Distance); }
            set { client.WriteInt32(Addresses.Player.Distance, value); }
        }
        public int Distance_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Distance_Percent); }
            set { client.WriteInt32(Addresses.Player.Distance_Percent, value); }
        }
        public int Shielding
        {
            get { return client.ReadInt32(Addresses.Player.Shielding); }
            set { client.WriteInt32(Addresses.Player.Shielding, value); }
        }
        public int Shielding_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Shielding_Percent); }
            set { client.WriteInt32(Addresses.Player.Shielding_Percent, value); }
        }
        public int Fishing
        {
            get { return client.ReadInt32(Addresses.Player.Fishing); }
            set { client.WriteInt32(Addresses.Player.Fishing, value); }
        }
        public int Fishing_Percent
        {
            get { return client.ReadInt32(Addresses.Player.Fishing_Percent); }
            set { client.WriteInt32(Addresses.Player.Fishing_Percent, value); }
        }

        public int GoTo_X
        {
            get { return client.ReadInt32(Addresses.Player.GoTo_X); }
            set { client.WriteInt32(Addresses.Player.GoTo_X, value); }
        }
        public int GoTo_Y
        {
            get { return client.ReadInt32(Addresses.Player.GoTo_Y); }
            set { client.WriteInt32(Addresses.Player.GoTo_Y, value); }
        }
        public int GoTo_Z
        {
            get { return client.ReadInt32(Addresses.Player.GoTo_Z); }
            set { client.WriteInt32(Addresses.Player.GoTo_Z, value); }
        }

        public int RedSquare
        {
            get { return client.ReadInt32(Addresses.Player.RedSquare); }
            set { client.WriteInt32(Addresses.Player.RedSquare, value); }
        }
        public int GreenSquare
        {
            get { return client.ReadInt32(Addresses.Player.GreenSquare); }
            set { client.WriteInt32(Addresses.Player.GreenSquare, value); }
        }
        public int WhiteSquare
        {
            get { return client.ReadInt32(Addresses.Player.WhiteSquare); }
            set { client.WriteInt32(Addresses.Player.WhiteSquare, value); }
        }

        public int AccessN
        {
            get { return client.ReadInt32(Addresses.Player.AccessN); }
            set { client.WriteInt32(Addresses.Player.AccessN, value); }
        }
        public int AccessS
        {
            get { return client.ReadInt32(Addresses.Player.AccessS); }
            set { client.WriteInt32(Addresses.Player.AccessS, value); }
        }

        public int Target_ID
        {
            get { return client.ReadInt32(Addresses.Player.Target_ID); }
            set { client.WriteInt32(Addresses.Player.Target_ID, value); }
        }
        public int Target_Type
        {
            get { return client.ReadInt32(Addresses.Player.Target_Type); }
            set { client.WriteInt32(Addresses.Player.Target_Type, value); }
        }
        public int Target_BList_ID
        {
            get { return client.ReadInt32(Addresses.Player.Target_BList_ID); }
            set { client.WriteInt32(Addresses.Player.Target_BList_ID, value); }
        }
        public int Target_BList_Type
        {
            get { return client.ReadInt32(Addresses.Player.Target_BList_Type); }
            set { client.WriteInt32(Addresses.Player.Target_BList_Type, value); }
        }
        public new int Z
        {
            get { return client.ReadInt32(Addresses.Player.Z); }
            set { client.WriteInt32(Addresses.Player.Z, value); }
        }
        public new int Y
        {
            get { return client.ReadInt32(Addresses.Player.Y); }
            set { client.WriteInt32(Addresses.Player.Y, value); }
        }
        public new int X
        {
            get { return client.ReadInt32(Addresses.Player.X); }
            set { client.WriteInt32(Addresses.Player.X, value); }
        }
        #endregion
    }
}
