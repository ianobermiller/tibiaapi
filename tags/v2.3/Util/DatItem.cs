using System;
using Tibia.Objects;

namespace Tibia
{
    public class DatItem
    {
        Client client;
        uint id;
        uint address;

        public DatItem(Client c, uint address, uint itemId)
        {
            client = c;
            this.address = address;
            id = itemId;
        }

        #region Properties
        public uint Address
        {
            get { return address; }
        }
        public uint Id
        {
            get { return id; }
        }
        public int Width
        {
            get { return client.ReadInt(address + Addresses.DatItem.Width); }
            set { client.WriteInt(address + Addresses.DatItem.Width, value); }
        }
        public int Height
        {
            get { return client.ReadInt(address + Addresses.DatItem.Height); }
            set { client.WriteInt(address + Addresses.DatItem.Height, value); }
        }
        public int Unknown1
        {
            get { return client.ReadInt(address + Addresses.DatItem.Unknown1); }
            set { client.WriteInt(address + Addresses.DatItem.Unknown1, value); }
        }
        public int Layers
        {
            get { return client.ReadInt(address + Addresses.DatItem.Layers); }
            set { client.WriteInt(address + Addresses.DatItem.Layers, value); }
        }
        public int PatternX
        {
            get { return client.ReadInt(address + Addresses.DatItem.PatternX); }
            set { client.WriteInt(address + Addresses.DatItem.PatternX, value); }
        }
        public int PatternY
        {
            get { return client.ReadInt(address + Addresses.DatItem.PatternY); }
            set { client.WriteInt(address + Addresses.DatItem.PatternY, value); }
        }
        public int PatternDepth
        {
            get { return client.ReadInt(address + Addresses.DatItem.PatternDepth); }
            set { client.WriteInt(address + Addresses.DatItem.PatternDepth, value); }
        }
        public int Phase
        {
            get { return client.ReadInt(address + Addresses.DatItem.Phase); }
            set { client.WriteInt(address + Addresses.DatItem.Phase, value); }
        }
        public int Sprites
        {
            get { return client.ReadInt(address + Addresses.DatItem.Sprites); }
            set { client.WriteInt(address + Addresses.DatItem.Sprites, value); }
        }
        public int Flags
        {
            get { return client.ReadInt(address + Addresses.DatItem.Flags); }
            set { client.WriteInt(address + Addresses.DatItem.Flags, value); }
        }
        public int WalkSpeed
        {
            get { return client.ReadInt(address + Addresses.DatItem.WalkSpeed); }
            set { client.WriteInt(address + Addresses.DatItem.WalkSpeed, value); }
        }
        public int TextLimit
        {
            get { return client.ReadInt(address + Addresses.DatItem.TextLimit); }
            set { client.WriteInt(address + Addresses.DatItem.TextLimit, value); }
        }
        public int LightRadius
        {
            get { return client.ReadInt(address + Addresses.DatItem.LightRadius); }
            set { client.WriteInt(address + Addresses.DatItem.LightRadius, value); }
        }
        public int LightColor
        {
            get { return client.ReadInt(address + Addresses.DatItem.LightColor); }
            set { client.WriteInt(address + Addresses.DatItem.LightColor, value); }
        }
        public int ShiftX
        {
            get { return client.ReadInt(address + Addresses.DatItem.ShiftX); }
            set { client.WriteInt(address + Addresses.DatItem.ShiftX, value); }
        }
        public int ShiftY
        {
            get { return client.ReadInt(address + Addresses.DatItem.ShiftY); }
            set { client.WriteInt(address + Addresses.DatItem.ShiftY, value); }
        }
        public int WalkHeight
        {
            get { return client.ReadInt(address + Addresses.DatItem.WalkHeight); }
            set { client.WriteInt(address + Addresses.DatItem.WalkHeight, value); }
        }
        public int Automap
        {
            get { return client.ReadInt(address + Addresses.DatItem.Automap); }
            set { client.WriteInt(address + Addresses.DatItem.Automap, value); }
        }
        public Addresses.DatItem.Help LensHelp
        {
            get { return (Addresses.DatItem.Help)client.ReadInt(address + Addresses.DatItem.LensHelp); }
            set { client.WriteInt(address + Addresses.DatItem.LensHelp, (int)value); }
        }
        #endregion

        #region Flags
        public bool GetFlag(Addresses.DatItem.Flag flag)
        {
            return (Flags & (int)flag) == (int)flag;
        }

        public void SetFlag(Addresses.DatItem.Flag flag, bool on)
        {
            if (on)
                Flags |= (int)flag;
            else
                Flags &= ~(int)flag;
        }
        #endregion

        #region Composite Properties
        public bool HasExtraByte()
        {
            return GetFlag(Tibia.Addresses.DatItem.Flag.IsStackable) ||
                   GetFlag(Tibia.Addresses.DatItem.Flag.IsRune) ||
                   GetFlag(Tibia.Addresses.DatItem.Flag.IsSplash) ||
                   GetFlag(Tibia.Addresses.DatItem.Flag.IsFluidContainer);
        }
        #endregion
    }
}
