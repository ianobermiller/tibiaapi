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
        public Player(Client client, uint address) : base(client, address)
        {
        }

        #region Packet Methods

        /// <summary>
        /// Turn to the specified direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Turn(Constants.TurnDirection direction)
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = Convert.ToByte(0x6F + direction);
            return client.Send(packet);
        }

        /// <summary>
        /// Walk in the specified direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Walk(Constants.WalkDirection direction)
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = Convert.ToByte(0x65 + direction);
            return client.Send(packet);
        }

        /// <summary>
        /// Walk in the specified list of directions.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Walk(List<Constants.WalkDirection> list)
        {
            int len = 4 + list.Count;
            byte[] packet = new byte[len];
            packet[0] = Convert.ToByte(len);
            packet[1] = 0x00;
            packet[2] = 0x64;
            packet[3] = Convert.ToByte(list.Count);

            int i = 4;
            foreach (Constants.WalkDirection dir in list)
            {
                packet[i] = Convert.ToByte(dir);
            }

            return client.Send(packet);
        }

        /// <summary>
        /// Go to the specified location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool GoTo(Objects.Location location)
        {
            bool result = true;

            result &= client.WriteInt(Addresses.Player.GoTo_X, location.X);
            result &= client.WriteInt(Addresses.Player.GoTo_Y, location.Y);
            result &= client.WriteInt(Addresses.Player.GoTo_Z, location.Z);
            IsWalking = true;

            return result;
        }

        /// <summary>
        /// Stop all actions.
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            byte[] packet = new byte[3];
            packet[0] = 0x01;
            packet[1] = 0x00;
            packet[2] = 0xBE;
            return client.Send(packet);
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
            byte[] packet = new byte[10];

            packet[0] = 0x08;
            packet[1] = 0x00;
            packet[2] = 0xD3;
            packet[3] = Packet.Lo(Convert.ToInt32(outfitType));
            packet[4] = Packet.Hi(Convert.ToInt32(outfitType));
            packet[5] = headColor;
            packet[6] = bodyColor;
            packet[7] = legsColor;
            packet[8] = feetColor;
            packet[9] = Convert.ToByte(addons);
            
            return client.Send(packet);
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
            long expNeeded = Calculate.ExpForLevel(levelNeeded);
            long expToGo = expNeeded - Exp;
            return expToGo;
        }

        #region Get/Set Properties
        public new int Id
        {
            get { return client.ReadInt(Addresses.Player.Id); }
            set { client.WriteInt(Addresses.Player.Id, value); }
        }
        public int Exp
        {
            get { return client.ReadInt(Addresses.Player.Exp); }
            set { client.WriteInt(Addresses.Player.Exp, value); }
        }
        public int Flags
        {
            get { return client.ReadInt(Addresses.Player.Flags); }
            set { client.WriteInt(Addresses.Player.Flags, value); }
        }
        public int Level
        {
            get { return client.ReadInt(Addresses.Player.Level); }
            set { client.WriteInt(Addresses.Player.Level, value); }
        }
        public int Level_Percent
        {
            get { return client.ReadInt(Addresses.Player.Level_Percent); }
            set { client.WriteInt(Addresses.Player.Level_Percent, value); }
        }
        public int MagicLevel
        {
            get { return client.ReadInt(Addresses.Player.MagicLevel); }
            set { client.WriteInt(Addresses.Player.MagicLevel, value); }
        }
        public int MagicLevel_Percent
        {
            get { return client.ReadInt(Addresses.Player.MagicLevel_Percent); }
            set { client.WriteInt(Addresses.Player.MagicLevel_Percent, value); }
        }

        public int Mana
        {
            get { return client.ReadInt(Addresses.Player.Mana); }
            set { client.WriteInt(Addresses.Player.Mana, value); }
        }
        public int Mana_Max
        {
            get { return client.ReadInt(Addresses.Player.Mana_Max); }
            set { client.WriteInt(Addresses.Player.Mana_Max, value); }
        }
        public int HP
        {
            get { return client.ReadInt(Addresses.Player.HP); }
            set { client.WriteInt(Addresses.Player.HP, value); }
        }
        public int HP_Max
        {
            get { return client.ReadInt(Addresses.Player.HP_Max); }
            set { client.WriteInt(Addresses.Player.HP_Max, value); }
        }

        public int Soul
        {
            get { return client.ReadInt(Addresses.Player.Soul); }
            set { client.WriteInt(Addresses.Player.Soul, value); }
        }
        public int Cap
        {
            get { return client.ReadInt(Addresses.Player.Cap); }
            set { client.WriteInt(Addresses.Player.Cap, value); }
        }
        public int Stamina
        {
            get { return client.ReadInt(Addresses.Player.Stamina); }
            set { client.WriteInt(Addresses.Player.Stamina, value); }
        }

        public int Fist
        {
            get { return client.ReadInt(Addresses.Player.Fist); }
            set { client.WriteInt(Addresses.Player.Fist, value); }
        }
        public int Fist_Percent
        {
            get { return client.ReadInt(Addresses.Player.Fist_Percent); }
            set { client.WriteInt(Addresses.Player.Fist_Percent, value); }
        }
        public int Club
        {
            get { return client.ReadInt(Addresses.Player.Club); }
            set { client.WriteInt(Addresses.Player.Club, value); }
        }
        public int Club_Percent
        {
            get { return client.ReadInt(Addresses.Player.Club_Percent); }
            set { client.WriteInt(Addresses.Player.Club_Percent, value); }
        }
        public int Sword
        {
            get { return client.ReadInt(Addresses.Player.Sword); }
            set { client.WriteInt(Addresses.Player.Sword, value); }
        }
        public int Sword_Percent
        {
            get { return client.ReadInt(Addresses.Player.Sword_Percent); }
            set { client.WriteInt(Addresses.Player.Sword_Percent, value); }
        }
        public int Axe
        {
            get { return client.ReadInt(Addresses.Player.Axe); }
            set { client.WriteInt(Addresses.Player.Axe, value); }
        }
        public int Axe_Percent
        {
            get { return client.ReadInt(Addresses.Player.Axe_Percent); }
            set { client.WriteInt(Addresses.Player.Axe_Percent, value); }
        }
        public int Distance
        {
            get { return client.ReadInt(Addresses.Player.Distance); }
            set { client.WriteInt(Addresses.Player.Distance, value); }
        }
        public int Distance_Percent
        {
            get { return client.ReadInt(Addresses.Player.Distance_Percent); }
            set { client.WriteInt(Addresses.Player.Distance_Percent, value); }
        }
        public int Shielding
        {
            get { return client.ReadInt(Addresses.Player.Shielding); }
            set { client.WriteInt(Addresses.Player.Shielding, value); }
        }
        public int Shielding_Percent
        {
            get { return client.ReadInt(Addresses.Player.Shielding_Percent); }
            set { client.WriteInt(Addresses.Player.Shielding_Percent, value); }
        }
        public int Fishing
        {
            get { return client.ReadInt(Addresses.Player.Fishing); }
            set { client.WriteInt(Addresses.Player.Fishing, value); }
        }
        public int Fishing_Percent
        {
            get { return client.ReadInt(Addresses.Player.Fishing_Percent); }
            set { client.WriteInt(Addresses.Player.Fishing_Percent, value); }
        }

        public int GoTo_X
        {
            get { return client.ReadInt(Addresses.Player.GoTo_X); }
            set { client.WriteInt(Addresses.Player.GoTo_X, value); }
        }
        public int GoTo_Y
        {
            get { return client.ReadInt(Addresses.Player.GoTo_Y); }
            set { client.WriteInt(Addresses.Player.GoTo_Y, value); }
        }
        public int GoTo_Z
        {
            get { return client.ReadInt(Addresses.Player.GoTo_Z); }
            set { client.WriteInt(Addresses.Player.GoTo_Z, value); }
        }

        public int RedSquare
        {
            get { return client.ReadInt(Addresses.Player.RedSquare); }
            set { client.WriteInt(Addresses.Player.RedSquare, value); }
        }
        public int GreenSquare
        {
            get { return client.ReadInt(Addresses.Player.GreenSquare); }
            set { client.WriteInt(Addresses.Player.GreenSquare, value); }
        }
        public int WhiteSquare
        {
            get { return client.ReadInt(Addresses.Player.WhiteSquare); }
            set { client.WriteInt(Addresses.Player.WhiteSquare, value); }
        }

        public int AccessN
        {
            get { return client.ReadInt(Addresses.Player.AccessN); }
            set { client.WriteInt(Addresses.Player.AccessN, value); }
        }
        public int AccessS
        {
            get { return client.ReadInt(Addresses.Player.AccessS); }
            set { client.WriteInt(Addresses.Player.AccessS, value); }
        }

        public int Target_ID
        {
            get { return client.ReadInt(Addresses.Player.Target_ID); }
            set { client.WriteInt(Addresses.Player.Target_ID, value); }
        }
        public int Target_Type
        {
            get { return client.ReadInt(Addresses.Player.Target_Type); }
            set { client.WriteInt(Addresses.Player.Target_Type, value); }
        }
        public int Target_BList_ID
        {
            get { return client.ReadInt(Addresses.Player.Target_BList_ID); }
            set { client.WriteInt(Addresses.Player.Target_BList_ID, value); }
        }
        public int Target_BList_Type
        {
            get { return client.ReadInt(Addresses.Player.Target_BList_Type); }
            set { client.WriteInt(Addresses.Player.Target_BList_Type, value); }
        }
        #endregion
    }
}
