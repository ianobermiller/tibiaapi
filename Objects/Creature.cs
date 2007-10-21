using System;

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
        /// <param name="c"></param>
        /// <param name="a"></param>
        public Creature(Client c, uint a)
        {
            client = c;
            address = a;
        }

        /// <summary>
        /// Check if a player (creature) is in your party.
        /// </summary>
        /// <returns>True if the player is a member or leader of your party. False otherwise.</returns>
        public bool inParty()
        {
            Memory.Addresses.Creature.Party_t party = Party;
            return (party == Tibia.Memory.Addresses.Creature.Party_t.Member || party == Tibia.Memory.Addresses.Creature.Party_t.Leader);
        }

        public uint Address
        {
            get { return address; }
            set { address = value; }
        }

        #region Get/Set Methods
        public int Id
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Id); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Id, value); }
        }
        public Memory.Addresses.Creature.CreatureType_t Type
        {
            get { return (Memory.Addresses.Creature.CreatureType_t)client.ReadByte(address + Memory.Addresses.Creature.Distance_Type); }
            set { client.WriteByte(address + Memory.Addresses.Creature.Distance_Type, (byte)value); }
        }
        public string Name
        {
            get { return client.ReadString(address + Memory.Addresses.Creature.Distance_Name); }
            set { client.WriteString(address + Memory.Addresses.Creature.Distance_Name, value); }
        }

        public int X
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_X); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_X, value); }
        }
        public int Y
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Y); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Y, value); }
        }
        public int Z
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Z); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Z, value); }
        }
        public Location Location
        {
            get { return new Location(X, Y, Z); }
        }

        public int ScreenOffsetHoriz
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_ScreenOffsetHoriz); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_ScreenOffsetHoriz, value); }
        }
        public int ScreenOffsetVert
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_ScreenOffsetVert); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_ScreenOffsetVert, value); }
        }

        public bool IsWalking
        {
            get { return Convert.ToBoolean(client.ReadInt(address + Memory.Addresses.Creature.Distance_IsWalking)); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_IsWalking, Convert.ToInt32(value)); IsWalking = value; }
        }
        public int WalkSpeed
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_WalkSpeed); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_WalkSpeed, value); }
        }
        public Memory.Addresses.Creature.Direction_t Direction
        {
            get { return (Memory.Addresses.Creature.Direction_t)client.ReadInt(address + Memory.Addresses.Creature.Distance_Direction); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Direction, (int)value); }
        }
        public bool IsVisible
        {
            get { return Convert.ToBoolean(client.ReadInt(address + Memory.Addresses.Creature.Distance_IsVisible)); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_IsVisible, Convert.ToInt32(value)); IsVisible = value; }
        }

        public int Light
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Light); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Light, value); }
        }
        public int LightColor
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_LightColor); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_LightColor, value); }
        }
        public int HPBar
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_HPBar); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_HPBar, value); }
        }

        public Memory.Addresses.Creature.Skull_t Skull
        {
            get { return (Memory.Addresses.Creature.Skull_t)client.ReadInt(address + Memory.Addresses.Creature.Distance_Skull); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Skull, (int)value); }
        }
        public Memory.Addresses.Creature.Party_t Party
        {
            get { return (Memory.Addresses.Creature.Party_t)client.ReadInt(address + Memory.Addresses.Creature.Distance_Party); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Party, (int)value); }
        }

        public int Outfit
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Outfit); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Outfit, value); }
        }
        public int Color_Head
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Color_Head); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Color_Head, value); }
        }
        public int Color_Body
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Color_Body); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Color_Body, value); }
        }
        public int Color_Legs
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Color_Legs); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Color_Legs, value); }
        }
        public int Color_Feet
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Color_Feet); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Color_Feet, value); }
        }
        public int Addon
        {
            get { return client.ReadInt(address + Memory.Addresses.Creature.Distance_Addon); }
            set { client.WriteInt(address + Memory.Addresses.Creature.Distance_Addon, value); }
        }
        #endregion
    }
}
