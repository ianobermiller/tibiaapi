using System;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Represents a player, which is just an extended version of creature.
    /// </summary>
    public class Player : Creature
    {
        #region Get/Set Properties
        public new int Id
        {
            get { return client.readInt(Addresses.Player.Id); }
            set { client.writeInt(Addresses.Player.Id, value); }
        }
        public int Exp
        {
            get { return client.readInt(Addresses.Player.Exp); }
            set { client.writeInt(Addresses.Player.Exp, value); }
        }
        public int Flags
        {
            get { return client.readInt(Addresses.Player.Flags); }
            set { client.writeInt(Addresses.Player.Flags, value); }
        }
        public int Level
        {
            get { return client.readInt(Addresses.Player.Level); }
            set { client.writeInt(Addresses.Player.Level, value); }
        }
        public int Level_Percent
        {
            get { return client.readInt(Addresses.Player.Level_Percent); }
            set { client.writeInt(Addresses.Player.Level_Percent, value); }
        }
        public int MagicLevel
        {
            get { return client.readInt(Addresses.Player.MagicLevel); }
            set { client.writeInt(Addresses.Player.MagicLevel, value); }
        }
        public int MagicLevel_Percent
        {
            get { return client.readInt(Addresses.Player.MagicLevel_Percent); }
            set { client.writeInt(Addresses.Player.MagicLevel_Percent, value); }
        }

        public int Mana
        {
            get { return client.readInt(Addresses.Player.Mana); }
            set { client.writeInt(Addresses.Player.Mana, value); }
        }
        public int Mana_Max
        {
            get { return client.readInt(Addresses.Player.Mana_Max); }
            set { client.writeInt(Addresses.Player.Mana_Max, value); }
        }
        public int HP
        {
            get { return client.readInt(Addresses.Player.HP); }
            set { client.writeInt(Addresses.Player.HP, value); }
        }
        public int HP_Max
        {
            get { return client.readInt(Addresses.Player.HP_Max); }
            set { client.writeInt(Addresses.Player.HP_Max, value); }
        }

        public int Soul
        {
            get { return client.readInt(Addresses.Player.Soul); }
            set { client.writeInt(Addresses.Player.Soul, value); }
        }
        public int Cap
        {
            get { return client.readInt(Addresses.Player.Cap); }
            set { client.writeInt(Addresses.Player.Cap, value); }
        }
        public int Stamina
        {
            get { return client.readInt(Addresses.Player.Stamina); }
            set { client.writeInt(Addresses.Player.Stamina, value); }
        }

        public int Fist
        {
            get { return client.readInt(Addresses.Player.Fist); }
            set { client.writeInt(Addresses.Player.Fist, value); }
        }
        public int Fist_Percent
        {
            get { return client.readInt(Addresses.Player.Fist_Percent); }
            set { client.writeInt(Addresses.Player.Fist_Percent, value); }
        }
        public int Club
        {
            get { return client.readInt(Addresses.Player.Club); }
            set { client.writeInt(Addresses.Player.Club, value); }
        }
        public int Club_Percent
        {
            get { return client.readInt(Addresses.Player.Club_Percent); }
            set { client.writeInt(Addresses.Player.Club_Percent, value); }
        }
        public int Sword
        {
            get { return client.readInt(Addresses.Player.Sword); }
            set { client.writeInt(Addresses.Player.Sword, value); }
        }
        public int Sword_Percent
        {
            get { return client.readInt(Addresses.Player.Sword_Percent); }
            set { client.writeInt(Addresses.Player.Sword_Percent, value); }
        }
        public int Axe
        {
            get { return client.readInt(Addresses.Player.Axe); }
            set { client.writeInt(Addresses.Player.Axe, value); }
        }
        public int Axe_Percent
        {
            get { return client.readInt(Addresses.Player.Axe_Percent); }
            set { client.writeInt(Addresses.Player.Axe_Percent, value); }
        }
        public int Distance
        {
            get { return client.readInt(Addresses.Player.Distance); }
            set { client.writeInt(Addresses.Player.Distance, value); }
        }
        public int Distance_Percent
        {
            get { return client.readInt(Addresses.Player.Distance_Percent); }
            set { client.writeInt(Addresses.Player.Distance_Percent, value); }
        }
        public int Shielding
        {
            get { return client.readInt(Addresses.Player.Shielding); }
            set { client.writeInt(Addresses.Player.Shielding, value); }
        }
        public int Shielding_Percent
        {
            get { return client.readInt(Addresses.Player.Shielding_Percent); }
            set { client.writeInt(Addresses.Player.Shielding_Percent, value); }
        }
        public int Fishing
        {
            get { return client.readInt(Addresses.Player.Fishing); }
            set { client.writeInt(Addresses.Player.Fishing, value); }
        }
        public int Fishing_Percent
        {
            get { return client.readInt(Addresses.Player.Fishing_Percent); }
            set { client.writeInt(Addresses.Player.Fishing_Percent, value); }
        }

        public int GoTo_X
        {
            get { return client.readInt(Addresses.Player.GoTo_X); }
            set { client.writeInt(Addresses.Player.GoTo_X, value); }
        }
        public int GoTo_Y
        {
            get { return client.readInt(Addresses.Player.GoTo_Y); }
            set { client.writeInt(Addresses.Player.GoTo_Y, value); }
        }
        public int GoTo_Z
        {
            get { return client.readInt(Addresses.Player.GoTo_Z); }
            set { client.writeInt(Addresses.Player.GoTo_Z, value); }
        }

        public int RedSquare
        {
            get { return client.readInt(Addresses.Player.RedSquare); }
            set { client.writeInt(Addresses.Player.RedSquare, value); }
        }
        public int GreenSquare
        {
            get { return client.readInt(Addresses.Player.GreenSquare); }
            set { client.writeInt(Addresses.Player.GreenSquare, value); }
        }
        public int WhiteSquare
        {
            get { return client.readInt(Addresses.Player.WhiteSquare); }
            set { client.writeInt(Addresses.Player.WhiteSquare, value); }
        }

        public int AccessN
        {
            get { return client.readInt(Addresses.Player.AccessN); }
            set { client.writeInt(Addresses.Player.AccessN, value); }
        }
        public int AccessS
        {
            get { return client.readInt(Addresses.Player.AccessS); }
            set { client.writeInt(Addresses.Player.AccessS, value); }
        }

        public int Target_ID
        {
            get { return client.readInt(Addresses.Player.Target_ID); }
            set { client.writeInt(Addresses.Player.Target_ID, value); }
        }
        public int Target_Type
        {
            get { return client.readInt(Addresses.Player.Target_Type); }
            set { client.writeInt(Addresses.Player.Target_Type, value); }
        }
        public int Target_BList_ID
        {
            get { return client.readInt(Addresses.Player.Target_BList_ID); }
            set { client.writeInt(Addresses.Player.Target_BList_ID, value); }
        }
        public int Target_BList_Type
        {
            get { return client.readInt(Addresses.Player.Target_BList_Type); }
            set { client.writeInt(Addresses.Player.Target_BList_Type, value); }
        }
        #endregion

        /// <summary>
        /// Default constructor, same as Objects.Creature.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        public Player(Client c, uint a) : base(c, a)
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
        /// TODO: Finish
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
            // Finish
            return client.Send(packet);
        }

        /// <summary>
        /// Go to the specified location.
        /// TODO
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static byte[] GoTo(Objects.Location location)
        {
            return null;
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
    }
}
