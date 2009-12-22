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
public static uint WoodenStairs = 1958;
public static uint Ramp4 = 1960;
public static uint Ramp5 = 1962;
public static uint Ramp6 = 1964;
public static uint Ramp7 = 1966;
public static uint Ramp8 = 2192;
public static uint Ramp9 = 2194;
public static uint Ramp10 = 2196;
public static uint Ramp11 = 2198;
public static uint Ramp12 = 1969;
public static uint Ramp13 = 1971;
public static uint Ramp14 = 1973;
public static uint Ramp15 = 1975;
public static uint StoneStairs = 1977;
public static uint StoneStairs1 = 1978;
public static uint Stairs1 = 5257;
public static uint Stairs2 = 5258;
public static uint Stairs3 = 5259;
public static uint Ramp16 = 6909;
public static uint Ramp17 = 6911;
public static uint Ramp18 = 6913;
public static uint Ramp19 = 6915;
public static uint StoneStairs2 = 855;
public static uint StoneStairs3 = 856;
public static uint Ramp20 = 7542;
public static uint Ramp21 = 7544;
public static uint Ramp22 = 7546;
public static uint Ramp23 = 7548;
public static uint WoodenStairs1 = 7881;
public static uint Ramp24 = 7887;
public static uint StoneStairs4 = 7888;
public static uint Roof = 5033;
public static uint Roof1 = 5035;
public static uint Roof2 = 5037;
public static uint Roof3 = 5039;
public static uint CorkscrewStairs = 8657;
public static uint Ramp25 = 8830;
public static uint Ramp26 = 8831;
}
public static class Down
{
public static uint Grass = 293;
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
public static uint Ladder1 = 482;
public static uint Ladder2 = 483;
public static uint Trapdoor7 = 484;
public static uint Stairway = 485;
public static uint Stairs10 = 566;
public static uint Stairs11 = 567;
public static uint Pitfall1 = 1066;
public static uint Pitfall2 = 1067;
public static uint EarthHole = 1080;
public static uint Stairs12 = 4823;
public static uint Stairs13 = 859;
public static uint Stairs14 = 4825;
public static uint Stairs15 = 4826;
public static uint Stairs16 = 5081;
public static uint Trapdoor8 = 5544;
public static uint Trapdoor9 = 5691;
public static uint Hole12 = 5731;
public static uint Trapdoor10 = 5763;
public static uint Ramp27 = 6127;
public static uint Ramp28 = 6128;
public static uint Ramp29 = 6129;
public static uint Ramp30 = 6130;
public static uint Trapdoor11 = 6172;
public static uint Trapdoor12 = 6173;
public static uint IceHut = 6754;
public static uint IceHut1 = 6755;
public static uint IceHut2 = 6756;
public static uint Ramp31 = 6917;
public static uint Ramp32 = 6918;
public static uint Ramp33 = 6919;
public static uint Ramp34 = 6920;
public static uint Ramp35 = 6921;
public static uint Ramp36 = 6922;
public static uint Ramp37 = 6923;
public static uint Ramp38 = 6924;
public static uint Trapdoor13 = 7053;
public static uint WoodenCoffin = 166;
public static uint WoodenCoffin1 = 167;
public static uint LargeHole = 867;
public static uint Hole13 = 868;
public static uint LargeHole1 = 874;
public static uint Stairs17 = 4824;
public static uint CaveEntrance = 7181;
public static uint CaveEntrance1 = 7182;
public static uint CaveEntrance2 = 7476;
public static uint CaveEntrance3 = 7477;
public static uint CaveEntrance4 = 7478;
public static uint CaveEntrance5 = 7479;
public static uint Hole14 = 7515;
public static uint Hole15 = 7516;
public static uint Hole16 = 7517;
public static uint Hole17 = 7518;
public static uint SomethingCrawling = 7519;
public static uint Hole18 = 7520;
public static uint Hole19 = 7521;
public static uint LargeHole2 = 7522;
public static uint Trapdoor14 = 369;
public static uint Trapdoor15 = 370;
public static uint Trapdoor16 = 411;
public static uint Trapdoor17 = 7767;
public static uint Trapdoor18 = 413;
public static uint Stairs18 = 414;
public static uint Stairs19 = 428;
public static uint Trapdoor19 = 432;
public static uint Trapdoor20 = 433;
public static uint Trapdoor21 = 7768;
public static uint Hole20 = 967;
public static uint TunnelEntrance = 7550;
public static uint Ramp39 = 7729;
public static uint Ramp40 = 7730;
public static uint Ramp41 = 7731;
public static uint Ramp42 = 7732;
public static uint Ramp43 = 7733;
public static uint Ramp44 = 7734;
public static uint Ramp45 = 7735;
public static uint Ramp46 = 7736;
public static uint Hole21 = 7737;
public static uint Hole22 = 7755;
public static uint Trapdoor22 = 7767;
public static uint Trapdoor23 = 7768;
public static uint WaterVortex = 7804;
public static uint LargeHole3 = 8144;
public static uint CorkscrewStairs1 = 8658;
public static uint Stairs20 = 8690;
public static uint OpenTrapdoor1 = 8709;
public static uint CorkscrewStairs2 = 8932;
}
public static class UpUse
{
public static uint Ladder3 = 1948;
public static uint Ladder4 = 1968;
public static uint RopeLadder = 5542;
public static uint Ladder5 = 7771;
public static uint Ladder6 = 9116;
}
public static class Rope
{
public static uint DirtFloor = 386;
public static uint StoneTile = 421;
public static uint DirtFloor1 = 386;
public static uint DirtFloor2 = 7762;
}
public static class DownUse
{
public static uint SewerGrate = 435;
public static uint SewerGrate1 = 7750;
}
public static class Shovel
{
public static uint StonePile = 593;
public static uint LooseStonePile = 606;
public static uint LooseIcePile = 608;
public static uint LooseStonePile1 = 7749;
public static uint Hole23 = 7749;
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

