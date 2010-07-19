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
            IsGround,
            TopOrder1,
            TopOrder2,
            TopOrder3,
            IsContainer,
            IsStackable,
            IsCorpse,
            IsUsable,
            IsRune,
            IsWritable,
            IsReadable,
            IsFluidContainer,
            IsSplash,
            Blocking,
            IsImmovable,
            BlocksMissiles,
            BlocksPath,
            IsPickupable,
            IsHangable,
            IsHangableHorizontal,
            IsHangableVertical,
            IsRotatable,
            IsLightSource,
            Floorchange,
            IsShifted,
            HasHeight,
            IsLayer,
            IsIdleAnimation,
            HasAutoMapColor,
            HasHelpLens,
            Unknown,
            IgnoreStackpos
        }

        private static readonly Dictionary<Flag, uint> flagOffsets860 = new Dictionary<Flag, uint>()
        { 
            { Flag.IsGround, 1 },
            { Flag.TopOrder1, 2 },
            { Flag.TopOrder2, 4 },
            { Flag.TopOrder3, 8 },
            { Flag.IsContainer, 16 },
            { Flag.IsStackable, 32 },
            { Flag.IsCorpse, 64 },
            { Flag.IsUsable, 128 },
            { Flag.IsWritable, 256 },
            { Flag.IsReadable, 512 },
            { Flag.IsFluidContainer, 1024 },
            { Flag.IsSplash, 2048 },
            { Flag.Blocking, 4096 },
            { Flag.IsImmovable, 8192 },
            { Flag.BlocksMissiles, 16384 },
            { Flag.BlocksPath, 32768 },
            { Flag.IsPickupable, 65536 },
            { Flag.IsHangable, 131072 },
            { Flag.IsHangableHorizontal, 262144 },
            { Flag.IsHangableVertical, 524288 },
            { Flag.IsRotatable, 1048576 },
            { Flag.IsLightSource, 2097152 },
            { Flag.Floorchange, 4194304 },
            { Flag.IsShifted, 8388608 },
            { Flag.HasHeight, 16777216 },
            { Flag.IsLayer, 33554432 },
            { Flag.IsIdleAnimation, 67108864 },
            { Flag.HasAutoMapColor, 134217728 },
            { Flag.HasHelpLens, 268435456 },
            { Flag.Unknown, 536870912 },
            { Flag.IgnoreStackpos, 1073741824 }
        };

        private static readonly Dictionary<Flag, uint> flagOffsetsPre860 = new Dictionary<Flag, uint>()
        { 
            { Flag.IsGround, 1 },
            { Flag.TopOrder1, 2 },
            { Flag.TopOrder2, 4 },
            { Flag.TopOrder3, 8 },
            { Flag.IsContainer, 16 },
            { Flag.IsStackable, 32 },
            { Flag.IsCorpse, 64 },
            { Flag.IsUsable, 128 },
            { Flag.IsRune, 256 },
            { Flag.IsWritable, 512 },
            { Flag.IsReadable, 1024 },
            { Flag.IsFluidContainer, 2048 },
            { Flag.IsSplash, 4096 },
            { Flag.Blocking, 8192 },
            { Flag.IsImmovable, 16384 },
            { Flag.BlocksMissiles, 32768 },
            { Flag.BlocksPath, 65536 },
            { Flag.IsPickupable, 131072 },
            { Flag.IsHangable, 262144 },
            { Flag.IsHangableHorizontal, 524288 },
            { Flag.IsHangableVertical, 1048576 },
            { Flag.IsRotatable, 2097152 },
            { Flag.IsLightSource, 4194304 },
            { Flag.Floorchange, 8388608 },
            { Flag.IsShifted, 16777216 },
            { Flag.HasHeight, 33554432 },
            { Flag.IsLayer, 67108864 },
            { Flag.IsIdleAnimation, 134217728 },
            { Flag.HasAutoMapColor, 268435456 },
            { Flag.HasHelpLens, 536870912 },
            { Flag.Unknown, 1073741824 },
            { Flag.IgnoreStackpos, 2147483648 }
        };

        public static uint GetFlagOffset(uint version, Flag flag)
        {
            uint offset;
            if (version >= 860)
            {
                // offset is set to zero if flag does not exist
                flagOffsets860.TryGetValue(flag, out offset);
            }
            else
            {
                flagOffsetsPre860.TryGetValue(flag, out offset);
            }
            return offset;
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
