using System;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Constants
{
    /// <summary>
    /// Contains tile ids.
    /// </summary>
    public static class Tiles
    {
        public static uint ClosedHole = 593;

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
