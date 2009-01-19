using System;
using System.Collections.Generic;
using System.Reflection;
using Tibia.Objects;

namespace Tibia.Constants
{
    /// <summary>
    /// Contains tile ids.
    /// </summary>
    public static class Tiles
    {
        /// <summary>
        /// Hole tiles which are closed
        /// </summary>
        public static class ClosedHoles
        {
            public static uint ClosedHole = 593;
            public static uint ClosedHole2 = 579;
            public static uint ClosedHole3 = 592;

            public static List<uint> GetList()
            {
                List<uint> list = new List<uint>(3);
                list.Add(ClosedHole);
                list.Add(ClosedHole2);
                list.Add(ClosedHole3);
                return list;
            }
        }

        /// <summary>
        /// Tiles which lead you down
        /// </summary>
        public static class Down
        {
            public static uint Down1 = 370;
            public static uint Down2 = 409;
            public static uint Down3 = 428;
            public static uint Down4 = 7478;
            public static uint Down5 = 7522;
            public static uint Down6 = 868;
            public static uint Down7 = 7476;
            public static uint GrassHole = 293;
            public static uint GrassHole2 = 293;
            public static uint HoleOpen = 580;
            public static uint HoleOpen2 = 383;
            public static uint PitFall = 294;
            public static uint RampDown = 459;
            public static uint SewerGate = 430; //Requires use
            public static uint StairsDown = 410;
            public static uint StairsDown2 = 411;
            public static uint StairsDown3 = 429;
            public static uint StairsDown4 = 432;
            public static uint StairsDown5 = 433;
            public static uint TrapDoor = 369;
            public static uint TrapDoor2 = 408;

            public static List<uint> GetList()
            {
                List<uint> list = new List<uint>(17);
                list.Add(Down1 = 370);
                list.Add(Down2 = 409);
                list.Add(Down3 = 428);
                list.Add(Down4 = 7478);
                list.Add(Down5 = 7522);
                list.Add(Down6 = 868);
                list.Add(Down7 = 7476);
                list.Add(GrassHole = 293);
                list.Add(GrassHole2 = 293);
                list.Add(HoleOpen = 580);
                list.Add(HoleOpen2 = 383);
                list.Add(PitFall = 294);
                list.Add(RampDown = 459);
                list.Add(SewerGate = 430); //Requires use
                list.Add(StairsDown = 410);
                list.Add(StairsDown2 = 411);
                list.Add(StairsDown3 = 429);
                list.Add(StairsDown4 = 432);
                list.Add(StairsDown5 = 433);
                list.Add(TrapDoor = 369);
                list.Add(TrapDoor2 = 408);
                return list;
            }
        }

        /// <summary>
        /// Tiles which lead you up
        /// </summary>
        public static class Up
        {
            public static uint RopeHoleUp = 384;  // Requires use of rope
            public static uint LadderUp = 1929; // Requires use
            public static uint RampUp = 1960;
            public static uint StairsLeft = 1978;
            public static uint StairsNorth = 1977;
            public static uint StairsUp = 1958;
            public static uint StairsUp2 = 1928;
            public static uint RampLeft = 1952;
            public static uint RampNorth = 1956;
            public static uint RampRight = 1950;
            public static uint RampSouth = 1954;
            public static uint RampNorth2 = 1964;
            public static uint RampSouth2 = 1966;
            public static uint RampLeft2 = 1962;
            public static uint PyramidNW = 2196;
            public static uint PyramidNE = 2198;
            public static uint PyramidSE = 2192;
            public static uint PyramidSW = 2194;
            public static uint StairsNorth2 = 6915;
            public static uint StairsRight2 = 6911;
            public static uint StairsLeft2 = 6909;

            public static List<uint> GetList()
            {
                List<uint> list = new List<uint>(3);
                list.Add(RopeHoleUp = 384);
                list.Add(LadderUp = 1929);
                list.Add(RampUp = 1960);
                list.Add(StairsLeft = 1978);
                list.Add(StairsNorth = 1977);
                list.Add(StairsUp = 1958);
                list.Add(StairsUp2 = 1928);
                list.Add(RampLeft = 1952);
                list.Add(RampNorth = 1956);
                list.Add(RampRight = 1950);
                list.Add(RampSouth = 1954);
                list.Add(RampNorth2 = 1964);
                list.Add(RampSouth2 = 1966);
                list.Add(RampLeft2 = 1962);
                list.Add(PyramidNW = 2196);
                list.Add(PyramidNE = 2198);
                list.Add(PyramidSE = 2192);
                list.Add(PyramidSW = 2194);
                list.Add(StairsNorth2 = 6915);
                list.Add(StairsRight2 = 6911);
                list.Add(StairsLeft2 = 6909);
                return list;
            }
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
