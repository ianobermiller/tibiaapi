using System;
using System.Collections.Generic;
using System.Reflection;
using Tibia.Objects;

namespace Tibia.Constants
{
    public static class Tiles
    {
        public static class Up
        {
            public static uint Stairs = 1947;
            public static uint Ramp = 1950;
            public static uint Ramp1 = 1952;
            public static uint Ramp2 = 1954;
            public static uint Ramp3 = 1956;
            public static uint Stairs1 = 1958;
            public static uint Ramp4 = 1960;
            public static uint Ramp5 = 1962;
            public static uint Ramp6 = 1964;
            public static uint Ramp7 = 1966;
            public static uint Ramp8 = 2192;
            public static uint Ramp9 = 2194;
            public static uint Ramp10 = 2196;
            public static uint Ramp11 = 2198;
            public static uint Stairs2 = 5257;
            public static uint Stairs3 = 5259;
            public static uint IceRamp = 6909;
            public static uint IceRamp1 = 6911;
            public static uint IceRamp2 = 6913;
            public static uint IceRamp3 = 6915;
            public static uint StoneStairs = 855;
            public static uint StoneStairs1 = 856;
            public static uint CaveEntrance = 7181;
            public static uint CorkscrewStairs = 8657;
        }
        public static class Down
        {
            public static uint Pitfall = 294;
            public static uint Trapdoor = 369;
            public static uint Trapdoor1 = 370;
            public static uint Hole = 385;
            public static uint Hole1 = 394;
            public static uint Trapdoor2 = 411;
            public static uint Trapdoor3 = 412;
            public static uint Stairs4 = 413;
            public static uint Stairs5 = 414;
            public static uint Stairs6 = 428;
            public static uint Trapdoor4 = 432;
            public static uint Ladder = 433;
            public static uint Trapdoor5 = 434;
            public static uint Stairs7 = 437;
            public static uint Stairs8 = 438;
            public static uint Stairs9 = 469;
            public static uint OpenTrapdoor = 476;
            public static uint Hole2 = 594;
            public static uint Hole3 = 595;
            public static uint Hole4 = 601;
            public static uint Hole5 = 600;
            public static uint Hole6 = 604;
            public static uint Hole7 = 605;
            public static uint Hole8 = 607;
            public static uint Hole9 = 609;
            public static uint Hole10 = 610;
            public static uint Hole11 = 615;
            public static uint Trapdoor6 = 1156;
            public static uint StoneStairway = 566;
            public static uint StoneStairway1 = 567;
            public static uint Stairs10 = 4823;
            public static uint Stairs11 = 859;
            public static uint Stairs12 = 4825;
            public static uint Trapdoor7 = 5081;
            public static uint Trapdoor8 = 5691;
            public static uint Hole12 = 5731;
            public static uint Ladder1 = 5763;
            public static uint Hole13 = 6127;
            public static uint Hole14 = 6128;
            public static uint Hole15 = 6129;
            public static uint Hole16 = 6130;
            public static uint Hole17 = 6173;
            public static uint Hole18 = 6917;
            public static uint Hole19 = 6918;
            public static uint Hole20 = 6919;
            public static uint Hole21 = 6920;
            public static uint Hole22 = 6921;
            public static uint Hole23 = 6922;
            public static uint Hole24 = 6923;
            public static uint Hole25 = 6924;
            public static uint TrapDoor = 7053;
            public static uint Coffin = 166;
            public static uint Coffin1 = 167;
            public static uint Hole26 = 868;
            public static uint Trapdoor9 = 4824;
            public static uint Hole27 = 7755;
            public static uint CorkscrewStairs1 = 8658;
            public static uint OpenTrapdoor1 = 8709;
        }
        public static class UpUse
        {
            public static uint Ladder2 = 1948;
        }
        public static class Rope
        {
            public static uint RopeSpot = 386;
            public static uint StoneTile = 421;
        }
        public static class DownUse
        {
            public static uint SewerGrate = 435;
        }
        public static class Shovel
        {
            public static uint LooseStonePile = 593;
            public static uint LooseStonePile1 = 606;
            public static uint LooseIcePile = 608;
            public static uint BurriedHole = 867;
        }
        /// <summary>
        /// Water tiles given as ranges
        /// </summary>
        public static class Water
        {
            /// <summary>
            /// The start of the tile ids that contain fish.
            /// </summary>
            public static uint FishStart = 4597;
            /// <summary>
            /// The end of the tile ids that contain fish.
            /// </summary>
            public static uint FishEnd = 4602;

            /// <summary>
            /// The start of the tile ids that are water but do not contain fish.
            /// </summary>
            public static uint NoFishStart = 4609;
            /// <summary>
            /// The end of the tile ids that are water but do not contain fish.
            /// </summary>
            public static uint NoFishEnd = 4614;

            /// <summary>
            /// Get a list of all water tiles containing fish
            /// </summary>
            /// <returns></returns>
            public static List<uint> GetFishIds()
            {
                List<uint> tileIds = new List<uint>();

                for (uint i = FishStart; i <= FishEnd; i++)
                {
                    tileIds.Add(i);
                }

                return tileIds;
            }

            /// <summary>
            /// Get a list of all water tiles not containing fish
            /// </summary>
            /// <returns></returns>
            public static List<uint> GetNoFishIds()
            {
                List<uint> tileIds = new List<uint>();

                for (uint i = NoFishStart; i <= NoFishEnd; i++)
                {
                    tileIds.Add(i);
                }

                return tileIds;
            }
        }
    }
}

