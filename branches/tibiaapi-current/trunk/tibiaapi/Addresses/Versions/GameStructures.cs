using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace Tibia
{
    public partial class Version
    {
        internal class v971
        {
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct Creature
            {
                public uint id;
                private fixed byte name[32];
                public uint z;
                public uint y;
                public uint x;
                public uint screenOffsetVertical;
                public uint screenOffsetHorizontal;
                public uint faceDirection;
                private fixed byte unk[20];
                public uint isWalking;
                public uint walkDirection;
                private fixed byte unk2[8];
                public uint outfit;
                public uint colorHead;
                public uint colorBody;
                public uint colorLegs;
                public uint colorFeet;
                public uint addon;
                public uint mountId;
                public uint light;
                public uint lightColor;
                private fixed byte unk3[4];
                public uint blackSquare;
                public uint hpBar;
                public uint walkSpeed;
                public uint isBlocking;
                public uint skull;
                public uint party;
                private fixed byte unk4[8];
                public uint warIcon;
                public uint isVisible;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct ContainerHeader
            {
                public uint hasParent;
                private fixed byte unk1[8];
                public uint id;
                public fixed byte name[32];
                public uint ammount;
                public uint isOpen;
                public uint volume;
                //container slots start here
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct SimpleItem
            {
                public uint dataEx;
                public uint data;
                public uint id;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct VipNode
            {
                public uint previousNode;
                public uint nextNode;
                private fixed byte unk1[8];
                public uint id;
                public byte icon;
                public byte notify;
                private fixed byte unk2[2];
                public fixed byte nameTextField[28];
                public fixed byte descriptionTextField[28];
                public uint status;
            }
            
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct PlayerProperties1
            {
                public uint flags;
                private fixed byte unk1[44];
                public uint offlineTraining;
                public uint greenSquare;
                private fixed byte unk2[4];
                public uint xorKey;
                public uint manaMax;
                public fixed byte unk3[8];
                public uint experience;
                private fixed byte unk4[40];
                public uint level;
                public uint soul;
                public uint magicLevel;
                private fixed byte unk5[4];
                public uint magicLevelPercent;
                public uint redSquare;
                public uint mana;
                //uint unk3
                //PlayerSkills (percentages)
                //uint unk4[3]
                //levelPercent
                //stamina
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct PlayerSkills
            {
                public uint fist;
                public uint club;
                public uint sword;
                public uint axe;
                public uint distance;
                public uint shielding;
                public uint fishing;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct PlayerProperties2
            {
                public uint capacity;
                public uint gotoY;
                public uint healthMax;
                public uint gotoX;
                public uint id;
                public uint x;
                public uint y;
                public uint z;
            }
            
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct PlayerSlots
            {
                public fixed byte ammo[12];
                public fixed byte ring[12];
                public fixed byte feet[12];
                public fixed byte legs[12];
                public fixed byte left[12];
                public fixed byte right[12];
                public fixed byte armor[12];
                public fixed byte backpack[12];
                public fixed byte neck[12];
                public fixed byte head[12];
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct DatMemory
            {
                public fixed byte marketName[32];
                public uint width;
                public uint height;
                public uint maxSizeInPixels;
                public uint layers;
                public uint patternX;
                public uint patternY;
                public uint patternDepth;
                public uint phase;
                public uint sprite;
                public ulong flags;
                public uint walkSpeed;
                public uint textLimit;
                public uint lightRadius;
                public uint lightColor;
                public uint shiftX;
                public uint shiftY;
                public uint walkHeight;
                public uint automap;
                public uint lensHelp;
                public uint clothSlot;
                public uint marketCategory;
                public uint marketTradeAs;
                public uint marketShowAs;
                public uint marketRestrictProfession;
                public uint marketRestrictLevel;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct CharListEntry
            {
                private uint unknown;
                public fixed byte charNameTextField[28];
                public fixed byte worldNameTextField[28];
                public uint isPreview;
                public uint worldIP;
                public uint worldPort;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct LoginInfo
            {
                public uint charListBegin;
                public uint charListEnd;
                private fixed byte unk1[12];
                public fixed byte accountTextField[28];
                public fixed byte passwordTextField[28];
                public uint selectedChar;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            unsafe public struct MapTile
            {
                public uint count;
                public fixed uint stackOrder[10];
                public fixed uint items[10 * 3];
                public uint effect;

            }
        }
    }
}