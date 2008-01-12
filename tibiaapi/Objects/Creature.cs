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
        public bool InParty()
        {
            Constants.Party party = Party;
            return (party == Constants.Party.Member || party == Constants.Party.Leader);
        }

        /// <summary>
        /// Check if the creature is attacking the player.
        /// </summary>
        /// <returns></returns>
        public bool IsAttacking()
        {
            return BlackSquare > (Environment.TickCount - client.GetStartTime());
        }

        /// <summary>
        /// Attack the creature.
        /// Sends a packet to the server and sets the red square around the creature.
        /// </summary>
        /// <returns></returns>
        public bool Attack()
        {
            byte[] packet = new byte[7];
            int creatureId = Id;

            packet[0] = 0x05;
            packet[1] = 0x00;
            packet[2] = 0xA1;

            byte[] idBytes = BitConverter.GetBytes(creatureId);
            Array.Copy(idBytes, 0, packet, 3, idBytes.Length);
            client.WriteInt(Addresses.Player.Target_ID, creatureId);
            return client.Send(packet);
        }

        /// <summary>
        /// Look at the creature / player.
        /// Sends a packet to the server with the same effect as
        /// shift-clicking or left-right clicking a creature.
        /// </summary>
        /// <returns></returns>
        public bool Look()
        {
            byte[] packet = new byte[11];
            int creatureId = Id;

            packet[00] = 0x09;
            packet[01] = 0x00;
            packet[02] = 0x8C;

            int x = X;
            int y = Y;

            packet[03] = Packet.Lo(x);
            packet[04] = Packet.Hi(x);
            packet[05] = Packet.Lo(y);
            packet[06] = Packet.Hi(y);
            packet[07] = Convert.ToByte(Z);

            packet[08] = 0x63;
            packet[09] = 0x00;
            packet[10] = 0x01;

            return client.Send(packet);
        }

        public uint Address
        {
            get { return address; }
            set { address = value; }
        }

        #region Get/Set Methods
        public int Id
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Id); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Id, value); }
        }
        public Constants.CreatureType Type
        {
            get { return (Constants.CreatureType)client.ReadByte(address + Addresses.Creature.Distance_Type); }
            set { client.WriteByte(address + Addresses.Creature.Distance_Type, (byte)value); }
        }
        public string Name
        {
            get { return client.ReadString(address + Addresses.Creature.Distance_Name); }
            set { client.WriteString(address + Addresses.Creature.Distance_Name, value); }
        }

        public int X
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_X); }
            set { client.WriteInt(address + Addresses.Creature.Distance_X, value); }
        }
        public int Y
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Y); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Y, value); }
        }
        public int Z
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Z); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Z, value); }
        }
        public Location Location
        {
            get { return new Location(X, Y, Z); }
        }

        public int ScreenOffsetHoriz
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_ScreenOffsetHoriz); }
            set { client.WriteInt(address + Addresses.Creature.Distance_ScreenOffsetHoriz, value); }
        }
        public int ScreenOffsetVert
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_ScreenOffsetVert); }
            set { client.WriteInt(address + Addresses.Creature.Distance_ScreenOffsetVert, value); }
        }

        public bool IsWalking
        {
            get { return Convert.ToBoolean(client.ReadByte(address + Addresses.Creature.Distance_IsWalking)); }
            set { client.WriteByte(address + Addresses.Creature.Distance_IsWalking, Convert.ToByte(value)); }
        }
        public int WalkSpeed
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_WalkSpeed); }
            set { client.WriteInt(address + Addresses.Creature.Distance_WalkSpeed, value); }
        }
        public Constants.TurnDirection Direction
        {
            get { return (Constants.TurnDirection)client.ReadInt(address + Addresses.Creature.Distance_Direction); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Direction, (int)value); }
        }
        public bool IsVisible
        {
            get { return Convert.ToBoolean(client.ReadInt(address + Addresses.Creature.Distance_IsVisible)); }
            set { client.WriteInt(address + Addresses.Creature.Distance_IsVisible, Convert.ToInt32(value)); }
        }

        public int Light
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Light); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Light, value); }
        }
        public int LightColor
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_LightColor); }
            set { client.WriteInt(address + Addresses.Creature.Distance_LightColor, value); }
        }
        public int HPBar
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_HPBar); }
            set { client.WriteInt(address + Addresses.Creature.Distance_HPBar, value); }
        }

        public int BlackSquare
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_BlackSquare); }
        }

        public Constants.Skull Skull
        {
            get { return (Constants.Skull)client.ReadInt(address + Addresses.Creature.Distance_Skull); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Skull, (int)value); }
        }
        public Constants.Party Party
        {
            get { return (Constants.Party)client.ReadInt(address + Addresses.Creature.Distance_Party); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Party, (int)value); }
        }

        public Constants.OutfitType Outfit
        {
            get { return (Constants.OutfitType)client.ReadInt(address + Addresses.Creature.Distance_Outfit); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Outfit, (int)value); }
        }
        public int Color_Head
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Color_Head); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Color_Head, value); }
        }
        public int Color_Body
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Color_Body); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Color_Body, value); }
        }
        public int Color_Legs
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Color_Legs); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Color_Legs, value); }
        }
        public int Color_Feet
        {
            get { return client.ReadInt(address + Addresses.Creature.Distance_Color_Feet); }
            set { client.WriteInt(address + Addresses.Creature.Distance_Color_Feet, value); }
        }
        public Constants.OutfitAddon Addon
        {
        	get { return (Constants.OutfitAddon)client.ReadInt(address + Addresses.Creature.Distance_Addon); }
        	set { client.WriteInt(address + Addresses.Creature.Distance_Addon, (int)value); }
        }
        #endregion
    }
}
