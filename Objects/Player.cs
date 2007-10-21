namespace Tibia.Objects
{
    /// <summary>
    /// Represents a player, which is just an extended version of creature.
    /// </summary>
    public class Player : Creature
    {
        #region Get/Set Properties
        public int Id
        {
            get { return client.ReadInt(Memory.Addresses.Player.Id); }
            set { client.WriteInt(Memory.Addresses.Player.Id, value); }
        }
        public int Exp
        {
            get { return client.ReadInt(Memory.Addresses.Player.Exp); }
            set { client.WriteInt(Memory.Addresses.Player.Exp, value); }
        }
        public int Flags
        {
            get { return client.ReadInt(Memory.Addresses.Player.Flags); }
            set { client.WriteInt(Memory.Addresses.Player.Flags, value); }
        }
        public int Level
        {
            get { return client.ReadInt(Memory.Addresses.Player.Level); }
            set { client.WriteInt(Memory.Addresses.Player.Level, value); }
        }
        public int Level_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Level_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Level_Percent, value); }
        }
        public int MagicLevel
        {
            get { return client.ReadInt(Memory.Addresses.Player.MagicLevel); }
            set { client.WriteInt(Memory.Addresses.Player.MagicLevel, value); }
        }
        public int MagicLevel_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.MagicLevel_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.MagicLevel_Percent, value); }
        }

        public int Mana
        {
            get { return client.ReadInt(Memory.Addresses.Player.Mana); }
            set { client.WriteInt(Memory.Addresses.Player.Mana, value); }
        }
        public int Mana_Max
        {
            get { return client.ReadInt(Memory.Addresses.Player.Mana_Max); }
            set { client.WriteInt(Memory.Addresses.Player.Mana_Max, value); }
        }
        public int HP
        {
            get { return client.ReadInt(Memory.Addresses.Player.HP); }
            set { client.WriteInt(Memory.Addresses.Player.HP, value); }
        }
        public int HP_Max
        {
            get { return client.ReadInt(Memory.Addresses.Player.HP_Max); }
            set { client.WriteInt(Memory.Addresses.Player.HP_Max, value); }
        }

        public int Soul
        {
            get { return client.ReadInt(Memory.Addresses.Player.Soul); }
            set { client.WriteInt(Memory.Addresses.Player.Soul, value); }
        }
        public int Cap
        {
            get { return client.ReadInt(Memory.Addresses.Player.Cap); }
            set { client.WriteInt(Memory.Addresses.Player.Cap, value); }
        }
        public int Stamina
        {
            get { return client.ReadInt(Memory.Addresses.Player.Stamina); }
            set { client.WriteInt(Memory.Addresses.Player.Stamina, value); }
        }

        public int Fist
        {
            get { return client.ReadInt(Memory.Addresses.Player.Fist); }
            set { client.WriteInt(Memory.Addresses.Player.Fist, value); }
        }
        public int Fist_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Fist_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Fist_Percent, value); }
        }
        public int Club
        {
            get { return client.ReadInt(Memory.Addresses.Player.Club); }
            set { client.WriteInt(Memory.Addresses.Player.Club, value); }
        }
        public int Club_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Club_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Club_Percent, value); }
        }
        public int Sword
        {
            get { return client.ReadInt(Memory.Addresses.Player.Sword); }
            set { client.WriteInt(Memory.Addresses.Player.Sword, value); }
        }
        public int Sword_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Sword_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Sword_Percent, value); }
        }
        public int Axe
        {
            get { return client.ReadInt(Memory.Addresses.Player.Axe); }
            set { client.WriteInt(Memory.Addresses.Player.Axe, value); }
        }
        public int Axe_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Axe_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Axe_Percent, value); }
        }
        public int Distance
        {
            get { return client.ReadInt(Memory.Addresses.Player.Distance); }
            set { client.WriteInt(Memory.Addresses.Player.Distance, value); }
        }
        public int Distance_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Distance_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Distance_Percent, value); }
        }
        public int Shielding
        {
            get { return client.ReadInt(Memory.Addresses.Player.Shielding); }
            set { client.WriteInt(Memory.Addresses.Player.Shielding, value); }
        }
        public int Shielding_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Shielding_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Shielding_Percent, value); }
        }
        public int Fishing
        {
            get { return client.ReadInt(Memory.Addresses.Player.Fishing); }
            set { client.WriteInt(Memory.Addresses.Player.Fishing, value); }
        }
        public int Fishing_Percent
        {
            get { return client.ReadInt(Memory.Addresses.Player.Fishing_Percent); }
            set { client.WriteInt(Memory.Addresses.Player.Fishing_Percent, value); }
        }

        public int GoTo_X
        {
            get { return client.ReadInt(Memory.Addresses.Player.GoTo_X); }
            set { client.WriteInt(Memory.Addresses.Player.GoTo_X, value); }
        }
        public int GoTo_Y
        {
            get { return client.ReadInt(Memory.Addresses.Player.GoTo_Y); }
            set { client.WriteInt(Memory.Addresses.Player.GoTo_Y, value); }
        }
        public int GoTo_Z
        {
            get { return client.ReadInt(Memory.Addresses.Player.GoTo_Z); }
            set { client.WriteInt(Memory.Addresses.Player.GoTo_Z, value); }
        }

        public int RedSquare
        {
            get { return client.ReadInt(Memory.Addresses.Player.RedSquare); }
            set { client.WriteInt(Memory.Addresses.Player.RedSquare, value); }
        }
        public int GreenSquare
        {
            get { return client.ReadInt(Memory.Addresses.Player.GreenSquare); }
            set { client.WriteInt(Memory.Addresses.Player.GreenSquare, value); }
        }
        public int WhiteSquare
        {
            get { return client.ReadInt(Memory.Addresses.Player.WhiteSquare); }
            set { client.WriteInt(Memory.Addresses.Player.WhiteSquare, value); }
        }

        public int AccessN
        {
            get { return client.ReadInt(Memory.Addresses.Player.AccessN); }
            set { client.WriteInt(Memory.Addresses.Player.AccessN, value); }
        }
        public int AccessS
        {
            get { return client.ReadInt(Memory.Addresses.Player.AccessS); }
            set { client.WriteInt(Memory.Addresses.Player.AccessS, value); }
        }

        public int Target_ID
        {
            get { return client.ReadInt(Memory.Addresses.Player.Target_ID); }
            set { client.WriteInt(Memory.Addresses.Player.Target_ID, value); }
        }
        public int Target_Type
        {
            get { return client.ReadInt(Memory.Addresses.Player.Target_Type); }
            set { client.WriteInt(Memory.Addresses.Player.Target_Type, value); }
        }
        public int Target_BList_ID
        {
            get { return client.ReadInt(Memory.Addresses.Player.Target_BList_ID); }
            set { client.WriteInt(Memory.Addresses.Player.Target_BList_ID, value); }
        }
        public int Target_BList_Type
        {
            get { return client.ReadInt(Memory.Addresses.Player.Target_BList_Type); }
            set { client.WriteInt(Memory.Addresses.Player.Target_BList_Type, value); }
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

        /// <summary>
        /// Check if the specified flag is set. Wrapper for Flags.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool hasFlag(Memory.Addresses.Player.Flags_t flag)
        {
            return (Flags & (int)flag) == (int)flag;
        }
    }
}
