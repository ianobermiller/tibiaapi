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
            Constants.Party party = Party;
            return (party == Constants.Party.Member || party == Constants.Party.Leader);
        }
        
        /// <summary>
        /// Attack the creature.
        /// Sends a packet to the server and sets the red square around the creature.
        /// </summary>
        /// <returns></returns>
        public bool attack()
        {
            byte[] packet = new byte[7];
            int creatureId = Id;

            packet[0] = 0x05;
            packet[1] = 0x00;
            packet[2] = 0xA1;

            byte[] idBytes = BitConverter.GetBytes(creatureId);
            Array.Copy(idBytes, 0, packet, 3, idBytes.Length);
            client.writeInt(Addresses.Player.Target_ID, creatureId);
            return client.send(packet);
        }

        public uint Address
        {
            get { return address; }
            set { address = value; }
        }

        #region Get/Set Methods
        public int Id
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Id); }
            set { client.writeInt(address + Addresses.Creature.Distance_Id, value); }
        }
        public Constants.CreatureType Type
        {
            get { return (Constants.CreatureType)client.readByte(address + Addresses.Creature.Distance_Type); }
            set { client.writeByte(address + Addresses.Creature.Distance_Type, (byte)value); }
        }
        public string Name
        {
            get { return client.readString(address + Addresses.Creature.Distance_Name); }
            set { client.writeString(address + Addresses.Creature.Distance_Name, value); }
        }

        public int X
        {
            get { return client.readInt(address + Addresses.Creature.Distance_X); }
            set { client.writeInt(address + Addresses.Creature.Distance_X, value); }
        }
        public int Y
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Y); }
            set { client.writeInt(address + Addresses.Creature.Distance_Y, value); }
        }
        public int Z
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Z); }
            set { client.writeInt(address + Addresses.Creature.Distance_Z, value); }
        }
        public Location Location
        {
            get { return new Location(X, Y, Z); }
        }

        public int ScreenOffsetHoriz
        {
            get { return client.readInt(address + Addresses.Creature.Distance_ScreenOffsetHoriz); }
            set { client.writeInt(address + Addresses.Creature.Distance_ScreenOffsetHoriz, value); }
        }
        public int ScreenOffsetVert
        {
            get { return client.readInt(address + Addresses.Creature.Distance_ScreenOffsetVert); }
            set { client.writeInt(address + Addresses.Creature.Distance_ScreenOffsetVert, value); }
        }

        public bool IsWalking
        {
            get { return Convert.ToBoolean(client.readInt(address + Addresses.Creature.Distance_IsWalking)); }
            set { client.writeInt(address + Addresses.Creature.Distance_IsWalking, Convert.ToInt32(value)); IsWalking = value; }
        }
        public int WalkSpeed
        {
            get { return client.readInt(address + Addresses.Creature.Distance_WalkSpeed); }
            set { client.writeInt(address + Addresses.Creature.Distance_WalkSpeed, value); }
        }
        public Constants.TurnDirection Direction
        {
            get { return (Constants.TurnDirection)client.readInt(address + Addresses.Creature.Distance_Direction); }
            set { client.writeInt(address + Addresses.Creature.Distance_Direction, (int)value); }
        }
        public bool IsVisible
        {
            get { return Convert.ToBoolean(client.readInt(address + Addresses.Creature.Distance_IsVisible)); }
            set { client.writeInt(address + Addresses.Creature.Distance_IsVisible, Convert.ToInt32(value)); IsVisible = value; }
        }

        public int Light
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Light); }
            set { client.writeInt(address + Addresses.Creature.Distance_Light, value); }
        }
        public int LightColor
        {
            get { return client.readInt(address + Addresses.Creature.Distance_LightColor); }
            set { client.writeInt(address + Addresses.Creature.Distance_LightColor, value); }
        }
        public int HPBar
        {
            get { return client.readInt(address + Addresses.Creature.Distance_HPBar); }
            set { client.writeInt(address + Addresses.Creature.Distance_HPBar, value); }
        }

        public Constants.Skull Skull
        {
            get { return (Constants.Skull)client.readInt(address + Addresses.Creature.Distance_Skull); }
            set { client.writeInt(address + Addresses.Creature.Distance_Skull, (int)value); }
        }
        public Constants.Party Party
        {
            get { return (Constants.Party)client.readInt(address + Addresses.Creature.Distance_Party); }
            set { client.writeInt(address + Addresses.Creature.Distance_Party, (int)value); }
        }

        public Constants.OutfitType Outfit
        {
            get { return (Constants.OutfitType)client.readInt(address + Addresses.Creature.Distance_Outfit); }
            set { client.writeInt(address + Addresses.Creature.Distance_Outfit, (int)value); }
        }
        public int Color_Head
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Color_Head); }
            set { client.writeInt(address + Addresses.Creature.Distance_Color_Head, value); }
        }
        public int Color_Body
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Color_Body); }
            set { client.writeInt(address + Addresses.Creature.Distance_Color_Body, value); }
        }
        public int Color_Legs
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Color_Legs); }
            set { client.writeInt(address + Addresses.Creature.Distance_Color_Legs, value); }
        }
        public int Color_Feet
        {
            get { return client.readInt(address + Addresses.Creature.Distance_Color_Feet); }
            set { client.writeInt(address + Addresses.Creature.Distance_Color_Feet, value); }
        }
        public Constants.OutfitAddon Addon
        {
        	get { return (Constants.OutfitAddon)client.readInt(address + Addresses.Creature.Distance_Addon); }
        	set { client.writeInt(address + Addresses.Creature.Distance_Addon, (int)value); }
        }
        #endregion
    }
}
