using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Addresses
{
    public static class DatItem
    {
        public static uint Width = 0;
        public static uint Height = 4;
        public static uint Unknown1 = 8;
        public static uint Layers = 12;
        public static uint PatternX = 16;
        public static uint PatternY = 20;
        public static uint PatternDepth = 24;
        public static uint Phase = 28;
        public static uint Sprite = 32;
        public static uint Flags = 36;
        public static uint CanLookAt = 40;
        public static uint WalkSpeed = 44;
        public static uint TextLimit = 48; // If it is readable/writable
        public static uint LightRadius = 52;
        public static uint LightColor = 56;
        public static uint ShiftX = 60;
        public static uint ShiftY = 64;
        public static uint WalkHeight = 68;
        public static uint Automap = 72; // Minimap color
        public static uint LensHelp = 76;

        public enum Flag : uint
        {
            IsGround = 1,
            TopOrder1 = 2,
            TopOrder2 = 4,
            TopOrder3 = 8,
            IsContainer = 16,
            IsStackable = 32,
            IsCorpse = 64,
            IsUsable = 128,
            IsRune = 256,
            IsWritable = 512,
            IsReadable = 1024,
            IsFluidContainer = 2048,
            IsSplash = 4096,
            Blocking = 8192,
            IsImmovable = 16384,
            BlocksMissiles = 32768,
            BlocksPath = 65536,
            IsPickupable = 131072,
            IsHangable = 262144,
            IsHangableHorizontal = 524288,
            IsHangableVertical = 1048576,
            IsRotatable = 2097152,
            IsLightSource = 4194304,
            Floorchange = 8388608,
            IsShifted = 16777216,
            HasHeight = 33554432,
            IsLayer = 67108864,
            IsIdleAnimation = 134217728,
            HasAutoMapColor = 268435456,
            HasHelpLens = 536870912,
            Unknown = 1073741824,
            IgnoreStackpos = 2147483648
        }

        public enum Help
        {
            IsLadder = 0x44C,
            IsSewer = 0x44D,
            IsDoor = 0x450,
            IsDoorWithLock = 0x451,
            IsRopeSpot = 0x44E,
            IsSwitch = 0x44F,
            IsStairs = 0x452,
            IsMailbox = 0x453,
            IsDepot = 0x454,
            IsTrash = 0x455,
            IsHole = 0x456,
            HasSpecialDescription = 0x457,
            IsReadOnly = 0x458
        }
    }
}
